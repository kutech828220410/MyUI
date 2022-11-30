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
    public partial class Dialog_Loading : Form
    {
        public bool IsAbort = false;
        public int StepValue
        {
            get
            {
                return this.rJ_ProgressBar.Value;
            }
            set
            {
                this.Invoke(new Action(delegate
                {
                    this.rJ_ProgressBar.Value = value;
                    Application.DoEvents();
                }));
            }
        }
        private int _StepNum = 0;
        public int StepNum
        {
            get
            {
                return this.rJ_ProgressBar.Maximum;
            }
            set
            {
                this.rJ_ProgressBar.Maximum = value;
            }
        }
        public Dialog_Loading()
        {
            InitializeComponent();
     
        }
        public void Run(int StepNum , int LocationX , int LocationY)
        {
            this.Refresh_Processbar();
            this.Show();
            this.StepNum = StepNum;
            this.Location = new Point(LocationX, LocationY);
            this.IsAbort = false;
        }

        private void Dialog_Loading_Load(object sender, EventArgs e)
        {

        }

        public void Refresh_Processbar()
        {
            rJ_ProgressBar.Refresh();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.IsAbort = true;
        }
    }
}
