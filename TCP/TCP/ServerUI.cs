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
    public partial class ServerUI : UserControl
    {
        private Form Active_Form;
        public ChatServer chatServer;
        String StreamName = "TCPServer.pro";
        [Serializable]
        private class Properties
        {
            public string PORT = "8080";
            public bool AutoConnect = false;
            public bool RefreshUI = false;
        }
        private Properties properties = new Properties();


        public ServerUI()
        {
            InitializeComponent();
        }
        public void Run(Form form)
        {
            Active_Form = form;             
            Active_Form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
            ChatSetting.port = Convert.ToInt32(textBox_PORT.Text);
            chatServer = new ChatServer(richTextBox_狀態視窗);
            LoadProperties();
            if (properties.AutoConnect) sub_connenet();
        }
        private void LoadProperties()
        {
            Object temp = new object();
            if (FileIO.LoadProperties(ref temp, StreamName))
            {
                try
                {
                    properties = (Properties)temp.DeepClone();
                    CallBackUI.textbox.字串更換(properties.PORT, textBox_PORT);
                    CallBackUI.checkbox.Checked(properties.AutoConnect, checkBox_自動連線);
                    CallBackUI.checkbox.Checked(properties.RefreshUI, checkBox_UI更新);
                }
                catch
                {

                }
            }
        }
        private void textBox_PORT_TextChanged(object sender, EventArgs e)
        {
            properties.PORT = textBox_PORT.Text;
        }

        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            FileIO.SaveProperties(properties, StreamName);
        }
        private void exButton_開始監聽_btnClick(object sender, EventArgs e)
        {
            sub_connenet();
        }
        void sub_connenet()
        {
            if (!chatServer.Server已建立)
            {
                ChatSetting.port = int.Parse(properties.PORT);
                chatServer.SetRefreshUI(properties.RefreshUI);
                chatServer.Server開始執行();
            }
            if (chatServer.Server已建立)
            {
                List<String> str_temp = new List<string>();
                foreach (IPAddress ip in chatServer.iphostentry.AddressList)
                {
                    str_temp.Add(ip.ToString());
                }
                CallBackUI.comobox.寫入DataSoure(str_temp.ToArray(), comboBox_IPList);
            }
            exButton_開始監聽.Set_LoadState(chatServer.Server已建立);
        }
        public void Writeline(string line)
        {
            chatServer.Writeline(line);
        }

        private void checkBox_UI更新_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_UI更新.Checked)
            {
                properties.RefreshUI = true;
            }
            else
            {
                properties.RefreshUI = false;
            }
            if (exButton_開始監聽.LoadState())
            {
                chatServer.SetRefreshUI(properties.RefreshUI);
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
   
    }
}
