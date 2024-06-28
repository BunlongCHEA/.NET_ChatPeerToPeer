namespace TCP_Chat_Client
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
            this.listBox_Message = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBox_TypeChat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBox_Username = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox_ClientOnline = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBox_CliServIP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBox_CliServPort = new System.Windows.Forms.TextBox();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.btn_Send = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listBox_Message
            // 
            this.listBox_Message.FormattingEnabled = true;
            this.listBox_Message.ItemHeight = 16;
            this.listBox_Message.Location = new System.Drawing.Point(449, 163);
            this.listBox_Message.Name = "listBox_Message";
            this.listBox_Message.Size = new System.Drawing.Size(849, 276);
            this.listBox_Message.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(446, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Write Your Chat:";
            // 
            // txtBox_TypeChat
            // 
            this.txtBox_TypeChat.Location = new System.Drawing.Point(565, 104);
            this.txtBox_TypeChat.Multiline = true;
            this.txtBox_TypeChat.Name = "txtBox_TypeChat";
            this.txtBox_TypeChat.Size = new System.Drawing.Size(556, 47);
            this.txtBox_TypeChat.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(446, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Enter Your Username:";
            // 
            // txtBox_Username
            // 
            this.txtBox_Username.Location = new System.Drawing.Point(590, 19);
            this.txtBox_Username.Name = "txtBox_Username";
            this.txtBox_Username.Size = new System.Drawing.Size(174, 22);
            this.txtBox_Username.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Online Status (Refresh Every 10 seconds)";
            // 
            // listBox_ClientOnline
            // 
            this.listBox_ClientOnline.FormattingEnabled = true;
            this.listBox_ClientOnline.ItemHeight = 16;
            this.listBox_ClientOnline.Location = new System.Drawing.Point(17, 51);
            this.listBox_ClientOnline.Name = "listBox_ClientOnline";
            this.listBox_ClientOnline.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_ClientOnline.Size = new System.Drawing.Size(416, 532);
            this.listBox_ClientOnline.TabIndex = 7;
            this.listBox_ClientOnline.SelectedIndexChanged += new System.EventHandler(this.listBox_ClientOnline_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(830, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Enter Server IP:";
            // 
            // txtBox_CliServIP
            // 
            this.txtBox_CliServIP.Location = new System.Drawing.Point(947, 19);
            this.txtBox_CliServIP.Name = "txtBox_CliServIP";
            this.txtBox_CliServIP.Size = new System.Drawing.Size(174, 22);
            this.txtBox_CliServIP.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(830, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "Enter Server Port:";
            // 
            // txtBox_CliServPort
            // 
            this.txtBox_CliServPort.Location = new System.Drawing.Point(947, 57);
            this.txtBox_CliServPort.Name = "txtBox_CliServPort";
            this.txtBox_CliServPort.Size = new System.Drawing.Size(174, 22);
            this.txtBox_CliServPort.TabIndex = 16;
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(1138, 19);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(155, 60);
            this.btn_Connect.TabIndex = 18;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(1138, 104);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(155, 47);
            this.btn_Send.TabIndex = 19;
            this.btn_Send.Text = "Send";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(449, 449);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(849, 134);
            this.txtLog.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 600);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btn_Send);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBox_CliServPort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBox_CliServIP);
            this.Controls.Add(this.listBox_Message);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBox_TypeChat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBox_Username);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox_ClientOnline);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_Message;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBox_TypeChat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBox_Username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox_ClientOnline;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBox_CliServIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBox_CliServPort;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.TextBox txtLog;
    }
}

