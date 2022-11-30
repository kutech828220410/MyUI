using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
namespace MyUI
{
    public partial class Dialog_NumPannel : Form
    {
        public static Form form;
        public int Value
        {
            get
            {
                return Texts.StringToInt32();
            }
            set
            {
                if (value < 0) value = 0;
                Texts = value.ToString();
            }
        }
        public string Texts
        {
            get
            {
                return this.rJ_TextBox_Value.Text;
            }
            private set
            {
                this.Invoke(new Action(delegate { this.rJ_TextBox_Value.Text = value; }));      
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.NumPad0 || keyData == Keys.D0)
            {
                OnClick_Num(0);
            }
            else if (keyData == Keys.NumPad1 || keyData == Keys.D1)
            {
                OnClick_Num(1);
            }
            else if (keyData == Keys.NumPad2 || keyData == Keys.D2)
            {
                OnClick_Num(2);
            }
            else if (keyData == Keys.NumPad3 || keyData == Keys.D3)
            {
                OnClick_Num(3);
            }
            else if (keyData == Keys.NumPad4 || keyData == Keys.D4)
            {
                OnClick_Num(4);
            }
            else if (keyData == Keys.NumPad5 || keyData == Keys.D5)
            {
                OnClick_Num(5);
            }
            else if (keyData == Keys.NumPad6 || keyData == Keys.D6)
            {
                OnClick_Num(6);
            }
            else if (keyData == Keys.NumPad7 || keyData == Keys.D7)
            {
                OnClick_Num(7);
            }
            else if (keyData == Keys.NumPad8 || keyData == Keys.D8)
            {
                OnClick_Num(8);
            }
            else if (keyData == Keys.NumPad9 || keyData == Keys.D9)
            {
                OnClick_Num(9);
            }
            else if (keyData == Keys.OemPeriod || keyData == Keys.Decimal)
            {

            }
            else if (keyData == Keys.Back)
            {
                OnClick_BackSpace();
            }
            else if (keyData == Keys.Escape)
            {
                OnClick_Cancel();
                return true;
            }
            else if (keyData == Keys.Enter || keyData == Keys.Return)
            {
                OnClick_Enter();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        public Dialog_NumPannel()
        {
            InitializeComponent();
        }
        public DialogResult ShowDialog()
        {
            if(form == null)
            {
                base.ShowDialog();
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    base.ShowDialog();
                }));
            }
         
            return this.DialogResult;
        }
        private void Dialog_NumPannel_Load(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.OnClick_CE();
        }


        private void OnClick_Num(int num)
        {
            string temp = Texts;
            temp = temp + num.ToString();
            temp = temp.StringToInt32().ToString();
            if (temp == "-1") temp = "0";
            Texts = temp;
        }
        private void OnClick_CE()
        {
            Texts = "0";
        }
        private void OnClick_BackSpace()
        {
            string temp = Texts;
            if (temp.Length > 0) temp = temp.Remove(temp.Length - 1);
            temp = temp.StringToInt32().ToString();
            if (temp == "-1") temp = "0";
            Texts = temp;
        }
        private void OnClick_Enter()
        {
            this.Invoke(new Action(delegate 
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }));
         }
        private void OnClick_Cancel()
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }));
        }
        private void rJ_Button_1_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(1);
        }
        private void rJ_Button_2_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(2);
        }
        private void rJ_Button_3_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(3);
        }
        private void rJ_Button_4_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(4);
        }
        private void rJ_Button_5_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(5);
        }
        private void rJ_Button_6_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(6);
        }
        private void rJ_Button_7_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(7);
        }
        private void rJ_Button_8_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(8);
        }
        private void rJ_Button_9_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(9);
        }
        private void rJ_Button_0_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(0);
        }
        private void rJ_Button_BackSpace_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_BackSpace();
        }
        private void rJ_Button_CE_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_CE();
        }
        private void rJ_Button_Cancel_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Cancel();
        }
        private void rJ_Button_Enter_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Enter();
        }

        private void Event_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
        private void rJ_TextBox_Value_Leave(object sender, EventArgs e)
        {
        }

        private void Dialog_NumPannel_FormClosed(object sender, FormClosedEventArgs e)
        {
           // this.Close();
        }

        private void rJ_Button_X_Click(object sender, EventArgs e)
        {
           
        }

        private void rJ_Button_X_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Cancel();
        }
    }
}
