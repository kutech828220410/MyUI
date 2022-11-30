using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using TCP_Server;
using Basic;

namespace TCP
{
    public partial class UDP_Cilent : UserControl
    {
        private bool _IsConnected = false;
        public bool IsConnected
        {
            private set
            {
                this._IsConnected = value;
            }
            get
            {
                return this._IsConnected;
            }
        }
 
        public UDP_Cilent()
        {
            InitializeComponent();
        }
        String StreamName = ".\\UDPCilent.pro";
        [Serializable]
        private class Properties
        {
            public string Remote_port = "4800";
            public string Local_port = "4801";
            public string IP = "192.168.100.90";
            public bool 使用DNS解析 = false;
            public string DNS域名 = "";
            public bool AutoConnect = false;
            public bool RefreshUI = false;
        }
        private Properties properties = new Properties();

        private Form Active_Form;
   

        public void Writeline(string line)
        {           
            client.Writeline(line);
        }
        public void WriteByte(byte[] value)
        {
            client.WriteByte(value);
        }
        public bool Readline(ref string line)
        {
            return client.Readline(ref line);
        }
        public void ClearReadline()
        {
             client.ClearReadBuf();
        }
        static public ChatSocket client;
        public void Run(Form form)
        {
            Active_Form = form;
            Active_Form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
            if (client == null) client = new ChatSocket();
            client.SetRichTextBox(this.richTextBox_狀態視窗);
            LoadProperties();
            if (properties.AutoConnect) Connect();
           // timer.Enabled = true;

        }
        public void Run(Form form, ChatSocket Client)
        {
            Active_Form = form;
            Active_Form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
            if (client == null) client = new ChatSocket();
            client.SetRichTextBox(this.richTextBox_狀態視窗);
            LoadProperties();
            if (properties.AutoConnect) Connect();
           // timer.Enabled = true;
        }
        public void LoadProperties()
        {
            Object temp = new object();
            if (FileIO.LoadProperties(ref temp, StreamName))
            {
                try
                {
                    properties = (Properties)temp.DeepClone();
                    this.RefreshUI();

                }
                catch
                {

                }
            }
        }
        public void SaveProperties()
        {
            FileIO.SaveProperties(properties, StreamName);
        }
        private void checkBox_使用DNS解析_CheckedChanged(object sender, EventArgs e)
        {
            properties.使用DNS解析 = checkBox_使用DNS解析.Checked;
            if (checkBox_使用DNS解析.Checked)
            {
                try
                {
                    IPHostEntry IPinfo = Dns.GetHostEntry(textBox_DNS域名.Text);
                    textBox_IP.Text = IPinfo.AddressList[0].ToString();
                }
                catch
                {
                    MessageBox.Show("解析失敗!");
                }
            }
        }
        private void textBox_DNS域名_TextChanged(object sender, EventArgs e)
        {
            if (checkBox_使用DNS解析.Checked)
            {
                try
                {
                    properties.DNS域名 = textBox_DNS域名.Text;
                    IPHostEntry IPinfo = Dns.GetHostEntry(textBox_DNS域名.Text);
                    textBox_IP.Text = IPinfo.AddressList[0].ToString();
                }
                catch
                {
                    MessageBox.Show("解析失敗!");
                }
            }
        }
        private void textBox_IP_TextChanged(object sender, EventArgs e)
        {
            properties.IP = textBox_IP.Text;
        }
        private void textBox_PORT_TextChanged(object sender, EventArgs e)
        {
            properties.Remote_port = textBox_RemotePort.Text;
        }

        private void textBox_LocalPort_TextChanged(object sender, EventArgs e)
        {
            properties.Local_port = textBox_LocalPort.Text;
        }
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            //FileIO.SaveProperties(properties, StreamName);
            if (client != null) client.Close();
        }

        private void exButton_開始連結_btnClick(object sender, EventArgs e)
        {
            if (client == null) client = new ChatSocket();
            if (client.isDead)
            {
                this.Connect();
            }
            else
            {
                this.Disconnected();
            }

        }

        public void Connect(string IP , string Remote_port ,string Local_port)
        {
            client.SetRichTextBox(richTextBox_狀態視窗);
            client.SetRefreshUI(properties.RefreshUI);
            client.CreateCilent(properties.IP, properties.Remote_port, properties.Local_port, "");

            List<String> str_temp = new List<string>();
            foreach (IPAddress ip in client.iphostentry.AddressList)
            {
                str_temp.Add(ip.ToString());
            }
            CallBackUI.comobox.寫入DataSoure(str_temp.ToArray(), comboBox_IPList);

            FileIO.SaveProperties(properties, StreamName);
            exButton_開始連結.Set_LoadState(true);
            this.IsConnected = true;
        }
        public void Connect()
        {
            this.Connect(properties.IP, properties.Remote_port, properties.Local_port);
            //client = new ChatSocket();
       
        }
        public void Disconnected()
        {
            client.Close();
            client.CilentDispose();
            //client = null;
            exButton_開始連結.Set_LoadState(false);
            this.IsConnected = false;
        }
        private void checkBox_UI更新_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_UI更新.Checked)
            {
                properties.RefreshUI = true;
            }
            else
            {
                properties.RefreshUI = false;
            }
            if (exButton_開始連結.LoadState())
            {
                client.SetRefreshUI(properties.RefreshUI);
            }
        }
        private void checkBox_自動連線_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_自動連線.Checked)
            {
                properties.AutoConnect = true;
            }
            else
            {
                properties.AutoConnect = false;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if(this.Parent.IsHandleCreated)
            {
                LoadProperties();
                if (properties.AutoConnect) Connect();
                timer.Enabled = false;
            }
           
        }
        public void RefreshUI()
        {
            this.Invoke(new Action(delegate
            {
                textBox_IP.Text = properties.IP;
                textBox_RemotePort.Text = properties.Remote_port;
                textBox_LocalPort.Text = properties.Local_port;
                textBox_DNS域名.Text = properties.DNS域名;
                checkBox_自動連線.Checked = properties.AutoConnect;
                checkBox_UI更新.Checked = properties.RefreshUI;
                checkBox_使用DNS解析.Checked = properties.使用DNS解析;

            }));
        }

        private void UDP_Cilent_Load(object sender, EventArgs e)
        {
            LoadProperties();
        }
    }
}
