namespace TCP_Chat_Server
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox_StatusOnline = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBox_ServerIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBox_ServerPort = new System.Windows.Forms.TextBox();
            this.btn_StartServer = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listBox_StatusOnline
            // 
            this.listBox_StatusOnline.FormattingEnabled = true;
            this.listBox_StatusOnline.ItemHeight = 16;
            this.listBox_StatusOnline.Location = new System.Drawing.Point(12, 56);
            this.listBox_StatusOnline.Name = "listBox_StatusOnline";
            this.listBox_StatusOnline.Size = new System.Drawing.Size(466, 372);
            this.listBox_StatusOnline.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Friend Online Status (Refresh Every 10 seconds)";
            // 
            // txtBox_ServerIP
            // 
            this.txtBox_ServerIP.Location = new System.Drawing.Point(672, 21);
            this.txtBox_ServerIP.Name = "txtBox_ServerIP";
            this.txtBox_ServerIP.ReadOnly = true;
            this.txtBox_ServerIP.Size = new System.Drawing.Size(230, 22);
            this.txtBox_ServerIP.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(528, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Server IP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(528, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Server Port:";
            // 
            // txtBox_ServerPort
            // 
            this.txtBox_ServerPort.Location = new System.Drawing.Point(672, 68);
            this.txtBox_ServerPort.Name = "txtBox_ServerPort";
            this.txtBox_ServerPort.ReadOnly = true;
            this.txtBox_ServerPort.Size = new System.Drawing.Size(230, 22);
            this.txtBox_ServerPort.TabIndex = 4;
            // 
            // btn_StartServer
            // 
            this.btn_StartServer.Location = new System.Drawing.Point(606, 108);
            this.btn_StartServer.Name = "btn_StartServer";
            this.btn_StartServer.Size = new System.Drawing.Size(243, 65);
            this.btn_StartServer.TabIndex = 6;
            this.btn_StartServer.Text = "Start Server";
            this.btn_StartServer.UseVisualStyleBackColor = true;
            this.btn_StartServer.Click += new System.EventHandler(this.btn_StartServer_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(499, 191);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(487, 237);
            this.txtLog.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 441);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btn_StartServer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBox_ServerPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBox_ServerIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox_StatusOnline);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_StatusOnline;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBox_ServerIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBox_ServerPort;
        private System.Windows.Forms.Button btn_StartServer;
        private System.Windows.Forms.TextBox txtLog;
    }
}

