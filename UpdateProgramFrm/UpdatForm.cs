using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using TCP_Server;
namespace UpdateProgram
{
   
    public partial class UpdatForm : Form
    {
        Form form;
        SoketFile.Sender SoketFileSender;
        string receiveData = "";
        string StateText_buf = "";
        public UpdatForm()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.form = this.FindForm();
            uDP_Cilent.Run(form);
            SoketFileSender = new SoketFile.Sender(TCP.UDP_Cilent.client ,exButton_發送檔案);
            if (!backgroundWorker.IsBusy) backgroundWorker.RunWorkerAsync();
        }
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                exButton_發送檔案.Run();
                if (TCP.UDP_Cilent.client != null)
                {
                    TCP.UDP_Cilent.client.Readline(ref receiveData);
                    if (receiveData.IndexOf("**") >= 0 || SoketFileSender.PaketWait)
                    {
                        SoketFileSender.AddReceiveData(receiveData);
                    }
                }               
                SoketFileSender.Run();
                if (StateText_buf != SoketFileSender.StateText)
                {
                    this.Invoke(new Action(delegate { label_State.Text = SoketFileSender.StateText; }));
                    StateText_buf = SoketFileSender.StateText;
                }
               
                Thread.Sleep(1);
            }
        }

        private void but_瀏覽_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                SoketFileSender.FileName = openFileDialog.FileName;
                textBox_檔案路徑.Text = SoketFileSender.FileName;
            }
        }  
    }
}
