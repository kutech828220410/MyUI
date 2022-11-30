using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using Basic;
using LadderProperty;

namespace LadderForm
{
    public partial class TransferSetup : Form
    {
        private Keyboard Keys = new Keyboard();
        private MyConvert myConvert = new MyConvert();
        private static TransferSetup transferSetup;
        private static readonly object synRoot = new object();
        public static bool 視窗已建立;
        public static TransferSetup GetForm(Point P0)
        {
            lock (synRoot)
            {
                if (transferSetup == null)
                {
                    transferSetup = new TransferSetup();
                }
                transferSetup.StartPosition = FormStartPosition.Manual;
                P0.X -= transferSetup.Size.Width / 2;
                P0.Y -= transferSetup.Size.Height / 2;
                transferSetup.Location = P0;
                視窗已建立 = true;
            }
            return transferSetup;
        }
        public static TransferSetup GetForm()
        {
            lock (synRoot)
            {
                if (transferSetup == null)
                {
                    transferSetup = new TransferSetup();
                }
            }
            return transferSetup;
        }
        private void TransferSetup_Load(object sender, EventArgs e)
        {
            transferSetup.topMachine_Panel.timer_程序執行.Enabled = true;
        }
        private void TransferSetup_FormClosed(object sender, FormClosedEventArgs e)
        {
            視窗已建立 = false;
            //transferSetup.Hide();
            transferSetup = null;
        }
        private void TransferSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            //topMachine_Panel.Dispose();
            transferSetup.topMachine_Panel.timer_程序執行.Enabled = false;
            視窗已建立 = false;
            e.Cancel = true;
            transferSetup.Hide();
            //transferSetup = null;
        }
        public TransferSetup()
        {
            InitializeComponent();
        }
        public static void close()
        {
            transferSetup = null;
        }


    }
    
}
