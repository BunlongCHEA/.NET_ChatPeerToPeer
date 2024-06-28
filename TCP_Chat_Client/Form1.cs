using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_Chat_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private TcpClient tcpClient;
        private string username;
        private string serverIP;
        private int serverPort;
        private int clientPort;
        //Dictionary ensure each username(use as key) is unique and updates the existing entry
        //instead of adding duplicates as we have multiple message send at once
        private Dictionary<string, OtherClient> otherClientDic = new Dictionary<string, OtherClient>();

        private TcpListener tcpListener;
        private Thread sendStatusThread;
        private Thread receStatusThread;
        private Thread cleanupThread;
        private Thread peerListenThread;

        private string prevSelectedUsername;
        private string prevSelectedIP;
        private int prevSelectedPort;

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            tcpClient.Close();
            tcpListener.Stop();
            //sendStatusThread?.Join();
            //receStatusThread?.Join(); 
            //cleanupThread?.Join(); 
            //peerListenThread?.Join();
            LogMessage("Client disconnected.");
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            //Define all information from UI text
            username = txtBox_Username.Text;
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Username is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            serverIP = txtBox_CliServIP.Text;
            serverPort = Convert.ToInt32(txtBox_CliServPort.Text);
            clientPort = new Random().Next(5000, 5100);

            //Connect to Server IP and Port
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
                tcpClient = new TcpClient();
                tcpClient.Connect(endPoint);
                LogMessage("Connected to server.");

                //Send Client Online Status to Server
                sendStatusThread = new Thread(SendStatusProc);
                sendStatusThread.Start();

                //Receive Other-Client Online Status from Server
                receStatusThread = new Thread(ReceStatusProc);
                receStatusThread.Start();

                //Remove and clear data-dictionary who offline 
                cleanupThread = new Thread(CleanOtherClientsData);
                cleanupThread.Start();

                //Start the client to client message
                peerListenThread = new Thread(ListenForPeers);
                peerListenThread.Start();
            }
            catch(Exception ex)
            {
                LogMessage($"Error connecting to server: {ex.Message}");
            }
        }

        private void SendStatusProc()
        {
            //Procedure and Status string to send to Server every 10 sec
            NetworkStream stream = tcpClient.GetStream();
            while(tcpClient.Connected)
            {
                string msg = $"{username},{GetLocalIPAddress()},{clientPort}";
                byte[] bufferSend = Encoding.ASCII.GetBytes(msg);
                stream.Write(bufferSend, 0, bufferSend.Length);

                Thread.Sleep(10000);
            }
        }

        private void ReceStatusProc()
        {
            NetworkStream stream = tcpClient.GetStream();
            byte[] bufferRead = new byte[1024];
            int byteRead;

            try
            {
                while (true)
                {
                    byteRead = stream.Read(bufferRead, 0, bufferRead.Length);
                    string recMsg = Encoding.ASCII.GetString(bufferRead, 0, byteRead);
                    string[] splitPart = recMsg.Split(',');

                    //Add to dictionary with key username to compare & update for next received msg 
                    if (splitPart.Length == 4 )
                    {
                        string username = splitPart[0].Trim();
                        string ipAddress = splitPart[1].Trim();
                        int port = Convert.ToInt32(splitPart[2].Trim());
                        DateTime lastUpdate = Convert.ToDateTime(splitPart[3].Trim());

                        var otherClients = new OtherClient
                        {
                            Username = username,
                            IPAddress = ipAddress,
                            Port = port,
                            LastUpdate = lastUpdate
                        };
                        //Mapping each array with key 'username'
                        lock (otherClientDic)
                        {
                            otherClientDic[username] = otherClients;
                        }

                        //Initiate display listBox_ClientOnline with the received message in form
                        //{LastUpdate} : {Username} - {IPAddress}:{Port}
                        DisplayStatus();
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Error receiving message: {ex.Message}");
            }
        }

        private void DisplayStatus()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(DisplayStatus));
                return;
            }

            //listBox_ClientOnline display based on data-dictionary that has remove and override
            listBox_ClientOnline.Items.Clear();
            lock (otherClientDic)
            {
                foreach (var client in otherClientDic.Values)
                {
                    //Display using form from data GetDisplayString()
                    listBox_ClientOnline.Items.Add(client.GetDisplayString());
                }
            }

            //- Use stored state from previously selected client, and add to the lost selected state
            //that refresh every 10 seconds.
            //- Function listBox_ClientOnline_SelectedIndexChanged - will used for selected
            //new client
            if (!string.IsNullOrEmpty(prevSelectedUsername) && !string.IsNullOrEmpty(prevSelectedIP) 
                && prevSelectedPort > 0 )
            {
                foreach (var item in listBox_ClientOnline.Items)
                {
                    string[] parts = item.ToString().Split('@');
                    if (parts.Length == 4)
                    {
                        string user = parts[1].Trim();
                        string ip = parts[2].Trim();
                        int port = Convert.ToInt32(parts[3].Trim());
                        if (user == prevSelectedUsername && ip == prevSelectedIP && port == prevSelectedPort)
                        {
                            listBox_ClientOnline.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
        }

        private void CleanOtherClientsData()
        {
            while (true)
            {
                Thread.Sleep(10000);
                DateTime now = DateTime.Now;
                List<string> removeOtherClientData = new List<string>();

                lock (otherClientDic)
                {
                    //Client who offline for more than 12 second, will be add key to remove-list
                    foreach (var clientDic in otherClientDic)
                    {
                        if ( (now - clientDic.Value.LastUpdate).TotalSeconds > 12 )
                        {
                            removeOtherClientData.Add(clientDic.Key);
                        }
                    }

                    //Get remove-list above and use it to remove from data-dictionary who offline
                    foreach (var user in removeOtherClientData)
                    {
                        otherClientDic.Remove(user);
                    }
                }
                //listBox_ClientOnline display based on data-dictionary that has remove and override
                DisplayStatus();
            }
        }

        private void ListenForPeers()
        {
            //Listen TCP message from other client
            tcpListener = new TcpListener(IPAddress.Any, clientPort);
            tcpListener.Start();
            LogMessage("Listening for peer connections...");

            try
            {
                while (true)
                {
                    TcpClient peerClient = tcpListener.AcceptTcpClient();
                    Thread peerThread = new Thread(() => HandlePeerClient(peerClient));
                    peerThread.Start();
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Error in peer listening: {ex.Message}");
            }
        }

        private void HandlePeerClient(TcpClient peerClient)
        {
            //Receive message from other client and display on listBox_Message
            NetworkStream nstream = peerClient.GetStream();
            byte[] bufferPeer = new byte[1024];
            int byteRead;

            try
            {
                while ( (byteRead = nstream.Read(bufferPeer, 0, bufferPeer.Length)) != 0 )
                {
                    string peerMsg = Encoding.ASCII.GetString(bufferPeer, 0, byteRead);
                    //Display on listBox_Message
                    DisplayPeerMessage(peerMsg);
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Error in peer handling: {ex.Message}");
            }
            finally
            {
                peerClient.Close();
            }

        }

        private void DisplayPeerMessage(string peerMsg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(DisplayPeerMessage), peerMsg);
                return;
            }

            listBox_Message.Items.Add(peerMsg);
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            //Client send message to other client by selected from listBox_ClientOnline
            if (listBox_ClientOnline.SelectedItems != null)
            {
                string selectedClient = listBox_ClientOnline.SelectedItem.ToString();
                //LogMessage(selectedClient);
                //Split from string form at listBox_ClientOnline, to send message
                string[] splitPart = selectedClient.Split(new[] { "@" }, StringSplitOptions.RemoveEmptyEntries);
                //LogMessage($"{splitPart[0]}, {splitPart[1]}, {splitPart[2]}, {splitPart[3]}");

                if (splitPart.Length == 4)
                {
                    string user = splitPart[1].Trim();
                    string ip = splitPart[2].Trim();
                    int port = Convert.ToInt32(splitPart[3].Trim());

                    string peerMessage = $"{username}: {txtBox_TypeChat.Text}";
                    SendMessageToPeer(ip, port, peerMessage);
                }
                else
                {
                    LogMessage("Error: Unable to parse selected client info.");
                }
            }
        }

        private void SendMessageToPeer(string ip, int port, string peerMessage)
        {
            try
            {
                //Use client ip and port from split text above for sending message
                TcpClient peerClient = new TcpClient(ip, port);
                NetworkStream nstream = peerClient.GetStream();
                byte[] bufferSendPeer = Encoding.ASCII.GetBytes(peerMessage);
                nstream.Write(bufferSendPeer, 0, bufferSendPeer.Length);
            }
            catch (Exception ex)
            {
                LogMessage($"Error sending message to peer: {ex.Message}");
            }
        }

        private void listBox_ClientOnline_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Stored selected a new client, so that we can use back when listBox_ClientOnline
            //refresh and lost selected state. This will avoid repeat selected the same client
            //without interruption
            if (listBox_ClientOnline.SelectedItem != null)
            {
                string selectedClient = listBox_ClientOnline.SelectedItem.ToString();
                //Split from string form at listBox_ClientOnline, to send message
                string[] splitPart = selectedClient.Split(new[] { "@" }, StringSplitOptions.RemoveEmptyEntries);

                if (splitPart.Length == 4)
                {
                    //Store the state of the selected client
                    prevSelectedUsername = splitPart[1].Trim();
                    prevSelectedIP = splitPart[2].Trim();
                    prevSelectedPort = Convert.ToInt32(splitPart[3].Trim());
                }
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

    public class OtherClient
    {
        public string Username { get; set; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public DateTime LastUpdate { get; set; }
        public string GetDisplayString()
        {
            return $"{LastUpdate} @ {Username} @ {IPAddress}@{Port}";
        }
    }
}
