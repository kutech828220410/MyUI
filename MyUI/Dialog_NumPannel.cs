﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
using DrawingClass;
namespace MyUI
{
    public partial class Dialog_NumPannel : MyDialog
    {
        public bool flag_dot = false;
        private bool flag_Init_Text = false;
        public static Form form;
        private Point location = new Point(0, 0);
        private int _Y_offset = 0;
        private int _X_offset = 0;
        public new Point Location
        {
            get
            {
                return this.location;
            }
            set
            {

                this.location = value;
            }
        }
        public double Value
        {
            get
            {
                return Texts.StringToDouble();
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
                this.Invoke(new Action(delegate { this.rJ_TextBox_Value.Texts = value; }));      
            }
        }

        private string _Init_Text = "";
        public string Init_Text
        {
            get
            {
                return _Init_Text;
            }
            set
            {
                _Init_Text = value;
                flag_Init_Text = true;
            }
        }
        public string Title
        {
            get
            {
                return this.rJ_Lable_Title.Text;
            }
            private set
            {
                this.Invoke(new Action(delegate { this.rJ_Lable_Title.Text = value; }));
            }
        }
        public string Content
        {
            get
            {
                return this.rJ_Lable_Content.Text;
            }
            private set
            {
                this.Invoke(new Action(delegate { this.rJ_Lable_Content.Text = value; }));
            }
        }
        public Font TitleFont
        {
            get
            {
                return rJ_Lable_Title.Font;
            }
            set
            {
                rJ_Lable_Title.Font = value;
            }
        }
        public Font ContentFont
        {
            get
            {
                return rJ_Lable_Content.Font;
            }
            set
            {
                rJ_Lable_Content.Font = value;
            }
        }
        public bool X_Visible
        {
            set
            {
                rJ_Button_X.Visible = value;
                rJ_Button_Cancel.Enabled = value;
            }
        }
        private int Value_buf = 0;
        private string Title_buf = "";
        private string Content_buf = "";

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
                OnClick_Dot();
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
        public Dialog_NumPannel(string title)
        {
            InitializeComponent();
            Title_buf = title;
            this.LoadFinishedEvent += Dialog_NumPannel_LoadFinishedEvent;
        }
        public Dialog_NumPannel(string title , string content)
        {
            InitializeComponent();
            Title_buf = title;
            Content_buf = content;
            this.LoadFinishedEvent += Dialog_NumPannel_LoadFinishedEvent;
        }
        public Dialog_NumPannel(string title, int value)
        {
            InitializeComponent();
            Value_buf = value;
            Title_buf = title;
             
            this.LoadFinishedEvent += Dialog_NumPannel_LoadFinishedEvent;
        }

    

        public Dialog_NumPannel(int value)
        {
            InitializeComponent();
            Value_buf = value;
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
            this.rJ_Button_X.MouseDownEvent += RJ_Button_X_MouseDownEvent;
            this.rJ_Button_Enter.MouseDownEvent += RJ_Button_Enter_MouseDownEvent;
            this.rJ_Button_dot.MouseDownEvent += RJ_Button_dot_MouseDownEvent;
            this.DialogResult = DialogResult.None;
            this.OnClick_CE();
            this.Value = Value_buf;
            this.Title = Title_buf;
            this.Content = Content_buf;

            //this.rJ_TextBox_Value.Texts = _Init_Text;
            this.rJ_TextBox_Value.Texts = this.Value.ToString();
            Size size_title = Draw.MeasureText(Title_buf, TitleFont);
            Size size_content = Draw.MeasureText(Content_buf, ContentFont);

            if (Title_buf.StringIsEmpty()) this.rJ_Lable_Title.Height = 0;
            else
            {
                this.rJ_Lable_Title.Height += size_title.Height;
                this.panel_top.Height += size_title.Height;
                this.Height += size_title.Height;
            }
            if (Content_buf.StringIsEmpty()) this.rJ_Lable_Content.Height = 0;
            else
            {
                this.panel_top.Height += size_content.Height;
                this.Height += size_content.Height;
            }
            if (this.location.X != 0 && this.location.Y != 0 || _X_offset != 0 || _Y_offset != 0)
            {
                this.StartPosition = FormStartPosition.WindowsDefaultLocation;
                base.Location = this.location;
                base.Location = new Point((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width) / 2 + _X_offset, (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - this.Height) / 2 + _Y_offset);
            }
       
        }

     

        public void Set_Location_Offset(int x_offset, int y_offset)
        {
            _X_offset = x_offset;
            _Y_offset = y_offset;
        }
        public override void Refresh()
        {
            rJ_TextBox_Value.Refresh();
            base.Refresh();
        }
        private void Dialog_NumPannel_LoadFinishedEvent(EventArgs e)
        {
            rJ_TextBox_Value.Refresh();

        }

        private void OnClick_Num(int num)
        {
            string temp = Texts;
            temp = temp + num.ToString();
            if (temp.Substring(0, 1) == "0" && temp.Length >= 2)
            {
               if(temp.Contains(".") == false) temp = temp.Remove(0, 1);
            }
            //temp = temp.StringToDouble().ToString();
            if (temp == "-1" && temp.Contains(".")) temp = "0";
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
        private void OnClick_Dot()
        {
            if(flag_dot == false)
            {
                Texts += '.';
                flag_dot = true;
            }
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
        private void RJ_Button_Enter_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Enter();
        }
        private void RJ_Button_dot_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Dot();
        }
        private void RJ_Button_X_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Cancel();
        }
        private void Dialog_NumPannel_FormClosed(object sender, FormClosedEventArgs e)
        {
           // this.Close();
        }
 
       
    }
}
