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
using DrawingClass;

namespace MyUI
{
    public partial class UserControl_NumPanel : UserControl
    {
        public delegate void SureClickBtnClickHandle(object sender, int Value);
        public event SureClickBtnClickHandle SureClick;
        public delegate void CancelBtnClickHandle(object sender);
        public event CancelBtnClickHandle CancelClick;

        public int Value
        {
            get
            {
                return Texts.StringToInt32();
            }
            set
            {
                if (this.IsHandleCreated == false) return;
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
        public string Title
        {
            get
            {
                return this.rJ_Lable_Title.Text;
            }
            set
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
            set
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
                if (this.IsHandleCreated == false) return;
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
                if (this.IsHandleCreated == false) return;
                rJ_Lable_Content.Font = value;
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
        public UserControl_NumPanel()
        {
            InitializeComponent();
            this.Load += UserControl_NumPanel_Load;

        }
        public UserControl_NumPanel(string title)
        {
            InitializeComponent();
            Title_buf = title;
            this.Load += UserControl_NumPanel_Load;
       

        }
        public UserControl_NumPanel(string title, string content)
        {
            InitializeComponent();
            Title_buf = title;
            Content_buf = content;
            this.Load += UserControl_NumPanel_Load;
   
        }
        public UserControl_NumPanel(string title, int value)
        {
            InitializeComponent();
            Value_buf = value;
            Title_buf = title;
            this.Load += UserControl_NumPanel_Load;
        }
        public UserControl_NumPanel(int value)
        {
            InitializeComponent();
            Value_buf = value;
            this.Load += UserControl_NumPanel_Load;
        }

        public void Init()
        {
            this.rJ_TextBox_Value.Refresh();
            this.Refresh();
            this.Focus();
        }
        #region Event
        private void UserControl_NumPanel_Load(object sender, EventArgs e)
        {
            this.OnClick_CE();
            this.Value = Value_buf;
            this.Title = Title_buf;
            this.Content = Content_buf;
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
        

            this.rJ_Button_CE.MouseDownEvent += RJ_Button_CE_MouseDownEvent;
            this.rJ_Button_BackSpace.MouseDownEvent += RJ_Button_BackSpace_MouseDownEvent;
            this.rJ_Button_Cancel.MouseDownEvent += RJ_Button_Cancel_MouseDownEvent;
            this.rJ_Button_Enter.MouseDownEvent += RJ_Button_Enter_MouseDownEvent;

            this.rJ_Button_0.MouseDownEvent += RJ_Button_0_MouseDownEvent;
            this.rJ_Button_1.MouseDownEvent += RJ_Button_1_MouseDownEvent;
            this.rJ_Button_2.MouseDownEvent += RJ_Button_2_MouseDownEvent;
            this.rJ_Button_3.MouseDownEvent += RJ_Button_3_MouseDownEvent;
            this.rJ_Button_4.MouseDownEvent += RJ_Button_4_MouseDownEvent;
            this.rJ_Button_5.MouseDownEvent += RJ_Button_5_MouseDownEvent;
            this.rJ_Button_6.MouseDownEvent += RJ_Button_6_MouseDownEvent;
            this.rJ_Button_7.MouseDownEvent += RJ_Button_7_MouseDownEvent;
            this.rJ_Button_8.MouseDownEvent += RJ_Button_8_MouseDownEvent;
            this.rJ_Button_9.MouseDownEvent += RJ_Button_9_MouseDownEvent;
       
        }

        private void RJ_Button_0_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(0);
        }
        private void RJ_Button_9_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(9);
        }
        private void RJ_Button_8_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(8);
        }
        private void RJ_Button_7_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(7);
        }
        private void RJ_Button_6_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(6);
        }
        private void RJ_Button_5_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(5);
        }
        private void RJ_Button_4_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(4);
        }
        private void RJ_Button_3_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(3);
        }
        private void RJ_Button_2_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(2);
        }
        private void RJ_Button_1_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Num(1);
        }
        private void RJ_Button_Enter_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Enter();
        }
        private void RJ_Button_Cancel_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_Cancel();
        }
        private void RJ_Button_BackSpace_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_BackSpace();
        }
        private void RJ_Button_CE_MouseDownEvent(MouseEventArgs mevent)
        {
            OnClick_CE();
        }
        #endregion



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
                if (SureClick != null) SureClick(this, this.Value);
            }));
        }
        private void OnClick_Cancel()
        {
            this.Invoke(new Action(delegate
            {
                if (CancelClick != null) CancelClick(this);
            }));
        }

    }
}
