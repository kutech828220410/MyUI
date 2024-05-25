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
    public partial class Dialog_TextBox : Form
    {
        public string Value
        {
            get
            {
                return this.rJ_TextBox1.Text;
            }
            set
            {
                this.rJ_TextBox1.Text = value;
            }
        }
        public string TitleText
        {
            get
            {
                return label_Title.Text;
            }
            set
            {
                label_Title.Text = value;
            }
        }
        public Font TitleFont
        {
            get
            {
                return label_Title.Font;
            }
            set
            {
                label_Title.Font = value;
            }
        }
        public Color TitleForeColor
        {
            get
            {
                return label_Title.ForeColor;
            }
            set
            {
                label_Title.ForeColor = value;
            }
        }
        public Color TitleBackColor
        {
            get
            {
                return label_Title.BackColor;
            }
            set
            {
                label_Title.BackColor = value;
            }
        }
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
                this.Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        public Dialog_TextBox(string titleText)
        {
            
            InitializeComponent();
            this.TitleText = titleText;
            int ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int ScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point((ScreenWidth - this.Width) / 2, (ScreenHeight - this.Height) / 2);
            this.rJ_Button_OK.MouseDownEvent += RJ_Button_OK_MouseDownEvent;
            this.rJ_Button_Cancel.MouseDownEvent += RJ_Button_Cancel_MouseDownEvent;
        }

        private void RJ_Button_Cancel_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }));
        }
        private void RJ_Button_OK_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }));
        }

        private void rJ_Button_OK_Click(object sender, EventArgs e)
        {
      
        }
        private void rJ_Button_Cancel_Click(object sender, EventArgs e)
        {
      
        }
    }
}
