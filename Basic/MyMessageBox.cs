using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace Basic
{
    public partial class MyMessageBox : Form
    {
        public static Form form;
        public static MyMessageBox _myMessageBox = null;
        public static bool IsMessageBoxCreate = false;
        public static bool 音效 = true;
        public static DialogResult ShowDialog(string Content)
        {
            return ShowDialog(new string[] { Content });
        }
        public static DialogResult ShowDialog(string Content, enum_BoxType enum_BoxType, enum_Button enum_Button)
        {
            return ShowDialog(new string[] { Content }, "  ", enum_BoxType, enum_Button);
        }
        public static DialogResult ShowDialog(string Content, enum_BoxType enum_BoxType)
        {
            return ShowDialog(new string[] { Content }, "  ", enum_BoxType);
        }
        public static DialogResult ShowDialog(string Content, string Title)
        {
            return ShowDialog(new string[] { Content }, "  ");
        }
        public static DialogResult ShowDialog(string Content, string Title, enum_BoxType enum_BoxType, enum_Button enum_Button)
        {
            return ShowDialog(new string[] { Content }, Title, enum_BoxType, enum_Button);
        }
        public static DialogResult ShowDialog(string Content, string Title, enum_BoxType enum_BoxType)
        {
            return ShowDialog(new string[] { Content }, Title, enum_BoxType);
        }

        public static DialogResult ShowDialog(string[] Content)
        {
            MyMessageBox _MyMessageBox = null;
            if (form == null)
            {
                _MyMessageBox = new MyMessageBox(" ", Content, enum_BoxType.None, enum_Button.Confirm);
                _MyMessageBox.Invoke(new Action(delegate
                {
                    _MyMessageBox.ShowDialog();
                }));
            }
            else
            {
           
                form.Invoke(new Action(delegate
                {
                    _MyMessageBox = new MyMessageBox(" ", Content, enum_BoxType.None, enum_Button.Confirm);
                    _MyMessageBox.ShowDialog();
                }));
            }
            return _MyMessageBox.Result;

        }
        public static DialogResult ShowDialog(string[] Content, enum_BoxType enum_BoxType, enum_Button enum_Button)
        {

            MyMessageBox _MyMessageBox = null;
            if (form == null)
            {
                _MyMessageBox = new MyMessageBox(" ", Content, enum_BoxType, enum_Button);
                _MyMessageBox.Invoke(new Action(delegate
                {
                    _MyMessageBox.ShowDialog();
                }));
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    _MyMessageBox = new MyMessageBox(" ", Content, enum_BoxType, enum_Button);
                    _MyMessageBox.ShowDialog();
                }));
            }
            return _MyMessageBox.Result;
        }
        public static DialogResult ShowDialog(string[] Content, enum_BoxType enum_BoxType)
        {

            MyMessageBox _MyMessageBox = null;
            if (form == null)
            {
                _MyMessageBox = new MyMessageBox(" ", Content, enum_BoxType, enum_Button.Confirm);
                _MyMessageBox.Invoke(new Action(delegate
                {
                    _MyMessageBox.ShowDialog();
                }));
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    _MyMessageBox = new MyMessageBox(" ", Content, enum_BoxType, enum_Button.Confirm);
                    _MyMessageBox.ShowDialog();
                }));
            }
            return _MyMessageBox.Result;
        }
        public static DialogResult ShowDialog(string[] Content, string Title)
        {

            MyMessageBox _MyMessageBox = null;
            if (form == null)
            {
                _MyMessageBox = new MyMessageBox(Title, Content, enum_BoxType.None, enum_Button.Confirm);
                _MyMessageBox.Invoke(new Action(delegate
                {
                    _MyMessageBox.ShowDialog();
                }));
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    _MyMessageBox = new MyMessageBox(Title, Content, enum_BoxType.None, enum_Button.Confirm);
                    _MyMessageBox.ShowDialog();
                }));
            }
            return _MyMessageBox.Result;
        }
        public static DialogResult ShowDialog(string[] Content, string Title, enum_BoxType enum_BoxType, enum_Button enum_Button)
        {

            MyMessageBox _MyMessageBox = null;
            if (form == null)
            {
                _MyMessageBox = new MyMessageBox(" ", Content, enum_BoxType, enum_Button);
                _MyMessageBox.Invoke(new Action(delegate
                {
                    _MyMessageBox.ShowDialog();
                }));
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    _MyMessageBox = new MyMessageBox(" ", Content, enum_BoxType, enum_Button);
                    _MyMessageBox.ShowDialog();
                }));
            }
            return _MyMessageBox.Result;
        }
        public static DialogResult ShowDialog(string[] Content, string Title, enum_BoxType enum_BoxType)
        {
            MyMessageBox _MyMessageBox = null;
            if (form == null)
            {
                _MyMessageBox = new MyMessageBox(" ", Content, enum_BoxType, enum_Button.Confirm);
                _MyMessageBox.Invoke(new Action(delegate
                {
                    _MyMessageBox.ShowDialog();
                }));
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    _MyMessageBox = new MyMessageBox(" ", Content, enum_BoxType, enum_Button.Confirm);
                    _MyMessageBox.ShowDialog();
                }));
            }
            return _MyMessageBox.Result;
        }

        private void Get_message(string[] Content, ref string message)
        {
            Graphics g = this.label_Content.CreateGraphics();
            int temp = -1;
            if (Content.Length == 1)
            {
                message = Content[0];
                MaxTextWidth = (int)g.MeasureString(Content[0], this.label_Content.Font).Width;
            }
            else
            {

                for (int i = 0; i < Content.Length; i++)
                {
                    message += (i + 1).ToString("00") + " .) " + Content[i] + "\n\r";
                    temp = (int)g.MeasureString(message, this.label_Content.Font).Width;
                    if (temp > MaxTextWidth) this.MaxTextWidth = temp;
                }
                
       
            }
            
        }

        public DialogResult Result = DialogResult.None;
        private enum_Button _enum_Button;
        public enum enum_Button
        {
            Confirm_Cancel, Confirm, Cancel
        }
        public enum enum_BoxType
        {
            Warning, Asterisk, Error, None
        }
        private int MaxTextWidth;
        private MyMessageBox(string Title, string[] Content, enum_BoxType enum_BoxType, enum_Button enum_Button)
        {
            InitializeComponent();
            this.TopMost = true;
            this.TopLevel = true;
            this.Text = Title;

            string message = "";
            this.Get_message(Content, ref message);
            this.label_Content.Text = message;
            this.Width = MaxTextWidth + 150;
            this.Height += this.label_Content.PreferredHeight;
            if (enum_BoxType == enum_BoxType.Warning)
            {
                this.panel_Image.BackgroundImage = Basic.Properties.Resources.Warning;
            }
            else if (enum_BoxType == enum_BoxType.Asterisk)
            {
                this.panel_Image.BackgroundImage = Basic.Properties.Resources.Asterisk;
            }
            else if (enum_BoxType == enum_BoxType.Error)
            {
                this.panel_Image.BackgroundImage = Basic.Properties.Resources.Error;
            }
            else if (enum_BoxType == enum_BoxType.None)
            {
                this.panel_ImageBox.Visible = false;
            }
            if(enum_Button == MyMessageBox.enum_Button.Confirm)
            {
                this.plC_Button_Cancel.Visible = false;
            }
            if (enum_Button == MyMessageBox.enum_Button.Cancel)
            {
                this.plC_Button_Confirm.Visible = false;
            }

            this._enum_Button = enum_Button;
            this.plC_Button_Confirm.音效 = 音效;
            this.plC_Button_Cancel.音效 = 音效;
        }
        private void plC_Button_Confirm_btnClick(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                Result = DialogResult.Yes;
                this.Close();
            }));
      
        }
        private void plC_Button_Cancel_btnClick(object sender, EventArgs e)
        {
            this.Invoke(new Action(delegate
            {
                Result = DialogResult.No;
                this.Close();
            }));
            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void MyMessageBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this._enum_Button == enum_Button.Cancel || this._enum_Button== enum_Button.Confirm)
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    this.Close();
                }
            }
          
        }
    }
}
