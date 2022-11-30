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
    public partial class CilentUI : UserControl
    {
        public CilentUI()
        {
            InitializeComponent();
        }
        String StreamName = ".\\TCPCilent.pro";
        [Serializable]
        private class Properties
        {
            public string PORT = "8080";
            public string IP = "192.168.100.1";
            public bool 使用DNS解析 = false;
            public string DNS域名 = "";
            public bool AutoConnect = false;
            public bool RefreshUI = false;
        }
        private Properties properties = new Properties();

        private Form Active_Form;
        private ChatSocket client = new ChatSocket();

 
        public void Writeline(string line)
        {
            client.Writeline(line);
        }
        public void WriteByte(byte[] value)
        {
            client.WriteByte(value);
        }
        public void WriteChar(char[] value)
        {
            client.WriteChar(value);
        }
        public bool Readline(ref string line)
        {
            return client.Readline(ref line);
        }
        public void Run(Form form)
        {
            Active_Form = form;               
            Active_Form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
            timer.Enabled = true;
  
        }
        private void LoadProperties()
        {
            Object temp = new object();
            if (FileIO.LoadProperties(ref temp, StreamName))
            {
                try
                {
                    properties = (Properties)temp.DeepClone();

                    CallBackUI.textbox.字串更換(properties.IP, textBox_IP);
                    CallBackUI.textbox.字串更換(properties.PORT, textBox_PORT);
                    CallBackUI.textbox.字串更換(properties.DNS域名, textBox_DNS域名);
                    CallBackUI.checkbox.Checked(properties.AutoConnect, checkBox_自動連線);
                    CallBackUI.checkbox.Checked(properties.RefreshUI, checkBox_UI更新);
                    CallBackUI.checkbox.Checked(properties.使用DNS解析, checkBox_使用DNS解析);

                }
                catch
                {

                }
            }
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
            properties.PORT = textBox_PORT.Text;
        }

        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            FileIO.SaveProperties(properties, StreamName);
        }

        private void exButton_開始連結_btnClick(object sender, EventArgs e)
        {
            if (client == null) client = new ChatSocket();
            if (client.isDead)
            {
                sub_connenet();
            }
            else
            {
                sub_disconnent();
            }
       
        }
        void sub_connenet()
        {
            client = new ChatSocket();
            client.SetRichTextBox(richTextBox_狀態視窗);
            client.SetRefreshUI(properties.RefreshUI);
            client.CreateCilent(properties.IP, properties.PORT, "");
            exButton_開始連結.Set_LoadState(true);
        }
        void sub_disconnent()
        {
            client.CilentDispose();
            //client = null;
            exButton_開始連結.Set_LoadState(false);
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
                if (properties.AutoConnect) sub_connenet();
                this.timer.Enabled = false;
            }
     
        }

    }
}
