using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic
{
    public partial class Dialog_IP_Config : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                this.DialogResult = DialogResult.Yes;
                int IPA = rJ_TextBox_IPA.Text.StringToInt32();
                int IPB = rJ_TextBox_IPB.Text.StringToInt32();
                int IPC = rJ_TextBox_IPC.Text.StringToInt32();
                int IPD = rJ_TextBox_IPD.Text.StringToInt32();
                int port = rJ_TextBox_Port.Text.StringToInt32();
                if (!portVisibale) port = 0;
                if (IPA < 0 || IPA > 255) { MyMessageBox.ShowDialog("請輸入正確 IP"); return true; }
                if (IPB < 0 || IPB > 255) { MyMessageBox.ShowDialog("請輸入正確 IP"); return true; }
                if (IPC < 0 || IPC > 255) { MyMessageBox.ShowDialog("請輸入正確 IP"); return true; }
                if (IPD < 0 || IPD > 255) { MyMessageBox.ShowDialog("請輸入正確 IP"); return true; }
                if (port < 0) { MyMessageBox.ShowDialog("請輸入正確 Port"); return true; }

                this.IP = $"{IPA}.{IPB}.{IPC}.{IPD}";
                this.Port = port;
                this.DialogResult = DialogResult.Yes;
                this.Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private string iP = "";
        private int port = 0;
        private bool portVisibale = true;

        public string IP { get => iP; private set => iP = value; }
        public int Port { get => port; private set => port = value; }
        public bool PortVisibale { get => portVisibale; set => portVisibale = value; }

        public Dialog_IP_Config()
        {
            InitializeComponent();
        }
        private void Dialog_IP_Config_Load(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            rJ_Lable_Port.Visible = portVisibale;
            rJ_TextBox_Port.Visible = portVisibale;
            this.TopLevel = true;
            this.TopMost = true;
        
        }
        private void rJ_Button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
        private void rJ_Button_OK_Click(object sender, EventArgs e)
        {
            int IPA = rJ_TextBox_IPA.Text.StringToInt32();
            int IPB = rJ_TextBox_IPB.Text.StringToInt32();
            int IPC = rJ_TextBox_IPC.Text.StringToInt32();
            int IPD = rJ_TextBox_IPD.Text.StringToInt32();
            int port = rJ_TextBox_Port.Text.StringToInt32();
            if (!portVisibale) port = 0;
            if (IPA < 0 || IPA > 255) { MyMessageBox.ShowDialog("請輸入正確 IP"); return; }
            if (IPB < 0 || IPB > 255) { MyMessageBox.ShowDialog("請輸入正確 IP"); return; }
            if (IPC < 0 || IPC > 255) { MyMessageBox.ShowDialog("請輸入正確 IP"); return; }
            if (IPD < 0 || IPD > 255) { MyMessageBox.ShowDialog("請輸入正確 IP"); return; }
            if (port < 0) { MyMessageBox.ShowDialog("請輸入正確 Port"); return; }

            this.IP = $"{IPA}.{IPB}.{IPC}.{IPD}";
            this.Port = port;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
        private void rJ_TextBox_IPA__TextChanged(object sender, EventArgs e)
        {

        }
        private void rJ_TextBox_IPB__TextChanged(object sender, EventArgs e)
        {

        }
        private void rJ_TextBox_IPC__TextChanged(object sender, EventArgs e)
        {

        }
        private void rJ_TextBox_IPD__TextChanged(object sender, EventArgs e)
        {

        }
        private void rJ_TextBox_Port__TextChanged(object sender, EventArgs e)
        {

        }
        private void rJ_TextBox_IPA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar <= 57 && (int)e.KeyChar >= 48) || (int)e.KeyChar == 8) // 8 > BackSpace
            {

                e.Handled = false;
            }
            else e.Handled = true;
        }
        private void rJ_TextBox_IPB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar <= 57 && (int)e.KeyChar >= 48) || (int)e.KeyChar == 8) // 8 > BackSpace
            {

                e.Handled = false;
            }
            else e.Handled = true;
        }
        private void rJ_TextBox_IPC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar <= 57 && (int)e.KeyChar >= 48) || (int)e.KeyChar == 8) // 8 > BackSpace
            {

                e.Handled = false;
            }
            else e.Handled = true;
        }
        private void rJ_TextBox_IPD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar <= 57 && (int)e.KeyChar >= 48) || (int)e.KeyChar == 8) // 8 > BackSpace
            {

                e.Handled = false;
            }
            else e.Handled = true;
        }
        private void rJ_TextBox_Port_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar <= 57 && (int)e.KeyChar >= 48) || (int)e.KeyChar == 8) // 8 > BackSpace
            {

                e.Handled = false;
            }
            else e.Handled = true;
        }

        private void rJ_TextBox_IPA_Enter(object sender, EventArgs e)
        {
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
        }
        private void rJ_TextBox_IPB_Enter(object sender, EventArgs e)
        {
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
        }
        private void rJ_TextBox_IPC_Enter(object sender, EventArgs e)
        {
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
        }
        private void rJ_TextBox_IPD_Enter(object sender, EventArgs e)
        {
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
        }
        private void rJ_TextBox_Port_Enter(object sender, EventArgs e)
        {
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
        }
    }
}

