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
    public partial class Dialog_Prcessbar : Form
    {
        private int maxValue = 100;
        public string State
        {
            set
            {
                this.Invoke(new Action(delegate
                {
                    this.label_State.Text = value;
                    this.Invalidate();
                    Application.DoEvents();
                }));
            }
            get
            {
                return this.label_State.Text;
            }
        }
        public int Value
        {
            set
            {
                //if (value < this.rJ_ProgressBar.Minimum) return;
                //if (value > this.rJ_ProgressBar.Maximum) return;
                this.rJ_ProgressBar.Value = value + 1;
                Application.DoEvents();

            }
        }
        public Dialog_Prcessbar(int MaxValue)
        {
            InitializeComponent();
            this.maxValue = MaxValue;
            this.Show();
        }

        private void Dialog_Prcessbar_Load(object sender, EventArgs e)
        {
            this.TopLevel = true;
            this.TopMost = true;
            this.rJ_ProgressBar.Maximum = this.maxValue;

            this.rJ_Button_取消.MouseClickEvent += RJ_Button_取消_MouseClickEvent;
            this.rJ_Button_取消.MouseDownEvent += RJ_Button_取消_MouseDownEvent;
        }

        private void RJ_Button_取消_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }));

        }

        private void RJ_Button_取消_MouseClickEvent(MouseEventArgs mevent)
        {

        }
    }
}
