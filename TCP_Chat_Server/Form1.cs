using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace TCP_Chat_Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private TcpListener listener;
        private List<ClientInfo> clientList = new List<ClientInfo>();
        private object objLock = new object();

        private bool serverRunning;
        private Thread listenThread;
        private Thread listenUpdateAndRemoveStatus;

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //The method sets serverRunning to false, stops the listener,
            //and waits for the accept and cleanup threads to join,
            //ensuring they finish their current tasks gracefully.
            serverRunning = false;
            listener.Stop();
            //listenThread?.Join();
            //listenUpdateAndRemoveStatus?.Join();
            LogMessage("Server stopped.");
        }

        private void btn_StartServer_Click(object sender, EventArgs e)
        {
            try
            {
                //listener and listenThread will keep receive status message from client part
                //(IPAddress.Any)  IPAddress.Parse("127.0.0.1")
                int ServerPort = 5000;
                string LocalIPAddress = GetLocalIPAddress();
                IPAddress ServerIP = IPAddress.Parse(LocalIPAddress);

                listener = new TcpListener(ServerIP, ServerPort);
                listener.Start();
                LogMessage("Server started.");
                txtBox_ServerIP.Text = ServerIP.ToString();
                txtBox_ServerPort.Text = ServerPort.ToString();
                serverRunning = true;

                listenThread = new Thread(listenClient);
                listenThread.Start();

                //While this will maintain UI list-box by update and remove the remain status client
                listenUpdateAndRemoveStatus = new Thread(RemoveClientStatusOffline);
                listenUpdateAndRemoveStatus.Start();
            }
            catch (Exception ex)
            {
                LogMessage($"Error starting server: {ex.Message}");
            }
        }

        private void listenClient()
        {
            while (serverRunning)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Thread clientThr = new Thread(() => ProcedureClient(client) );
                    clientThr.Start();
                }
                catch (Exception ex)
                {
                    LogMessage($"Error accepting client: {ex.Message}");
                }
            }
        }

        private void ProcedureClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] bufferRead = new byte[1024];
            int bytesRead; 
            
            try
            {
                //Receive status from client part every 10 second - SendStatusProc()
                while ( serverRunning && (bytesRead = stream.Read(bufferRead, 0, bufferRead.Length)) != 0)
                {
                    string msg = Encoding.ASCII.GetString(bufferRead, 0, bytesRead);
                    string[] splitPart = msg.Split(',');

                    //Message will be separate by comma, with (username, IP, Port)
                    if (splitPart.Length == 3)
                    {
                        ClientInfo clInfo = new ClientInfo
                        {
                            //Receive client status in array with ',' as separator
                            username = splitPart[0].Trim(),
                            IPAddress = splitPart[1].Trim(),
                            Port = Convert.ToInt32(splitPart[2].Trim()),
                            LastUpdate = DateTime.Now,
                            tcpClient = client
                        };

                        //MessageBox.Show($"{clInfo.username}, {clInfo.IPAddress}, {clInfo.Port}, {clInfo.LastUpdate}");

                        //Multiple thread from client, so need to access thread 1 at the time
                        lock (objLock)
                        {
                            //Remove the old client from list before add new one, to avoid duplicate
                            clientList.RemoveAll(c => c.username == clInfo.username);
                            clientList.Add(clInfo);
                        }

                        //Invoke Thread and update status to UI (listBox_StatusOnline)
                        UpdateClientStatusOnline();
                        //Send Online Status Back to each client to display who online
                        SendStatusOnlineToClient(clInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Error handling client: {ex.Message}");
            }
            finally
            {
                client.Close();
                LogMessage($"Client disconnected.");
            }
        }

        private void UpdateClientStatusOnline()
        {
            //If call different Thread will invoke new one; if main UI Thread, no invoke
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateClientStatusOnline));
                return;
            }

            lock (objLock)
            {
                //clear all listBox_StatusOnline and add new one
                listBox_StatusOnline.Items.Clear();
                foreach (var cl in clientList)
                {
                    listBox_StatusOnline.Items.Add($"{cl.LastUpdate} : {cl.username} - {cl.IPAddress}:{cl.Port}");
                }
            }
        }

        private void SendStatusOnlineToClient(ClientInfo clInfo)
        {
            //String format to send to multiple Client
            string msg = $"{clInfo.username},{clInfo.IPAddress},{clInfo.Port},{clInfo.LastUpdate}";

            //Call cl.tcpClient to direct access to its own TCP client to send and
            //avoid send its own status 
            lock (objLock)
            {
                foreach (var cl in  clientList)
                {
                    if (cl.username != clInfo.username)
                    {
                        try
                        {
                            NetworkStream stream = cl.tcpClient.GetStream();
                            byte[] bufferSend = Encoding.ASCII.GetBytes(msg);
                            stream.Write(bufferSend, 0, bufferSend.Length);
                        }
                        catch (Exception ex)
                        {
                            LogMessage($"Error sending message to client {cl.username}: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void RemoveClientStatusOffline()
        {
            while (serverRunning)
            {
                lock (objLock)
                {
                    //Remove the client who offline for more than 12 second
                    DateTime now = DateTime.Now;
                    clientList.RemoveAll( c => (now - c.LastUpdate).TotalSeconds > 12 );
                }

                UpdateClientStatusOnline();
                //Wait and run every 10 sec
                Thread.Sleep(10000);
            }
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No Network-Adapters with an IPv4 address in the system!");
        }

        private void LogMessage(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action<string>(LogMessage), message);
                return;
            }
            txtLog.AppendText($"{DateTime.Now}: {message}{Environment.NewLine}");
        }
    }
    public class ClientInfo
    {
        public string username { get; set; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public DateTime LastUpdate { get; set; }
        //Allows the server to send messages directly to an active specific client's network stream
        public TcpClient tcpClient { get; set; }
    }
}
