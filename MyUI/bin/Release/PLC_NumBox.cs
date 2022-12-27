using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LadderConnection;
using Basic;
namespace MyUI
{
    [Designer(typeof(ComponentSet.JLabelExDesigner))]  
    [System.Drawing.ToolboxBitmap(typeof(NumericUpDown))]
    public partial class PLC_NumBox : UserControl
    {
        private delegate void FormDelegate(MyUI.數字鍵盤 form);
        private MyConvert myConvert = new MyConvert();
        private LowerMachine PLC;
        private bool flag_init = false;
        private string Value_Str_Buf = "0";
        private Int64 Int64_Value
        {
            get
            {
                Int64 temp = 0;
                Int64.TryParse(Value_Str_Buf,out temp);
                return temp;
            }
            set
            {
                Value_Str_Buf = value.ToString();
            }
        }
        private int Int_Value
        {
            get
            {
                int temp = 0;
                int.TryParse(Value_Str_Buf, out temp);
                return temp;
            }
            set
            {

                Value_Str_Buf = value.ToString();
            }
        }
        private bool Enable_buf = true;
        private object value;
        [Browsable(false)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                數字轉換(value.ToString(), textBox1);
            }
        }
        [ReadOnly(false), Browsable(false)]
        public int Value
        {
            get
            {
                return this.GetValue();
            }
            set
            {
                this.SetValue(value);
            }

        }
        public override Font Font
        {
            get => base.Font;
            set
            {
                this.Height = this.textBox1.Height + 2;
                base.Font = value;
            }
        }
        #region 自訂屬性
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Color mForeColor
        {
            get
            {
                return this.textBox1.ForeColor;
            }
            set
            {
                this.textBox1.ForeColor = value;
            }
        }
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Color mBackColor
        {
            get
            {
                return this.textBox1.BackColor;
            }
            set
            {
                this.textBox1.BackColor = value;
            }
        }
        private bool _顯示螢幕小鍵盤 = true;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 顯示螢幕小鍵盤
        {
            get { return _顯示螢幕小鍵盤; }
            set
            {
                _ReadOnly = !value;
                _顯示螢幕小鍵盤 = value;
            }
        }
        private bool _顯示微調按鈕 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 顯示微調按鈕
        {
            get { return _顯示微調按鈕; }
            set
            {
                this.button_UP.Visible = value;
                this.button_DOWN.Visible = value;
                _顯示微調按鈕 = value;
            }
        }
        private int _微調數值 = 1;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int 微調數值
        {
            get { return _微調數值; }
            set
            {
                _微調數值 = value;
            }
        }
        private bool _密碼欄位 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 密碼欄位
        {
            get { return _密碼欄位; }
            set
            {
                if (value)
                {
                    textBox1.PasswordChar = '*';
                    textBox1.UseSystemPasswordChar = true;
                }
                else
                {
                    textBox1.UseSystemPasswordChar = false;
                }
                _密碼欄位 = value;
            }
        }
        private bool _ReadOnly = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool ReadOnly
        {
            get
            {
                textBox1.ReadOnly = _ReadOnly;
                return _ReadOnly;
            }
            set
            {
                _顯示螢幕小鍵盤 = false;
                _ReadOnly = value;
                textBox1.ReadOnly = _ReadOnly;

            }
        }
        private string _致能讀取位置 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 致能讀取位置
        {
            get { return _致能讀取位置; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _致能讀取位置 = value;
                else _致能讀取位置 = "";
            }
        }
        private string _數值更動旗標 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 數值更動旗標
        {
            get { return _數值更動旗標; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _數值更動旗標 = value;
                else _數值更動旗標 = "";
            }
        }
        private bool _音效 = true;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 音效
        {
            get { return _音效; }
            set
            {
                _音效 = value;
            }
        }

        private int _小數點位置 = 0;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int 小數點位置
        {
            get
            {
                if (!flag_init) 數字轉換(0.ToString(), textBox1);
                return _小數點位置;
            }
            set
            {
                if (!flag_init) 數字轉換(0.ToString(), textBox1);
                _小數點位置 = value;
            }
        }

        private string _寫入元件位置 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 寫入元件位置
        {
            get { return _寫入元件位置; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z" || temp == "K") divice_OK = true;
                }

                if (divice_OK) _寫入元件位置 = value;
                else _寫入元件位置 = "";
            }
        }

        private string _讀取元件位置 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 讀取元件位置
        {
            get { return _讀取元件位置; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z" || temp == "K") divice_OK = true;
                }

                if (divice_OK) _讀取元件位置 = value;
                else _讀取元件位置 = "";
            }
        }

        public enum WordLengthEnum : int
        {
            單字元, 雙字元
        }
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public WordLengthEnum 字元長度 { get; set; }

        string _寫入位置註解 = "";
        [ReadOnly(false), Browsable(true), Category("註解"), Description(""), DefaultValue("")]
        public string 寫入位置註解
        {
            get
            {
                return _寫入位置註解;
            }
            set
            {
                _寫入位置註解 = value;
                if (this.PLC != null && this.寫入元件位置 != "" && value != "")
                {
                    PLC.properties.Device.Set_Device(this.寫入元件位置, "*" + value);
                }
            }
        }
        string _讀取位置註解 = "";
        [ReadOnly(false), Browsable(true), Category("註解"), Description(""), DefaultValue("")]
        public string 讀取位置註解
        {
            get
            {
                return _讀取位置註解;
            }
            set
            {
                _讀取位置註解 = value;
                if (this.PLC != null && this.讀取元件位置 != "" && value != "")
                {
                    PLC.properties.Device.Set_Device(this.讀取元件位置, "*" + value);
                }
            }
        }
        #endregion
     

        private void 數字轉換(string value, TextBox textBox1)
        {

            bool 有負號 = false;
            if (value.IndexOf("-") == 0)
            {
                value = value.Substring(1);
                有負號 = true;
            }
            if (_小數點位置 != 0)
            {
                if (value.Length <= _小數點位置)
                {
                    while (true)
                    {
                        if (value.Length >= _小數點位置) break;
                        value = "0" + value;
                    }
                    value = "0." + value;
                }
                if (value.IndexOf(".") < 0)
                {
                    value = value.Insert(value.Length - _小數點位置, ".");
                }
            }
            if (有負號) value = "-" + value;
            string text = textBox1.Text;
            if (text != value)
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(delegate
                    {
                        textBox1.Text = value;
                        base.Text = value.Replace(".", "");
                    }));
                }
                else
                {
                    textBox1.Text = value;
                    base.Text = value.Replace(".", "");
                }
            }
          

        }
        private void form_show(MyUI.數字鍵盤 form)
        {
            form.ShowDialog();
            if (MyUI.數字鍵盤.Enter)
            {
                if (MyUI.數字鍵盤.value != "" && MyUI.數字鍵盤.value != null)
                {
                    if (this.小數點位置 > 0)
                    {
                        MyUI.數字鍵盤.value = MyUI.數字鍵盤.value.Replace(".", "");
                    }
                    if (寫入元件位置 != "" && 寫入元件位置 != null && PLC != null)
                    {
                        if (字元長度 == WordLengthEnum.單字元)
                        {
                            Int32 value = 0;
                            if (!Int32.TryParse(MyUI.數字鍵盤.value, out value))
                            {
                                value = Int32.MaxValue;
                            }
                            PLC.properties.Device.Set_Device(寫入元件位置, value);
                        }
                        else if (字元長度 == WordLengthEnum.雙字元)
                        {
                            Int64 value = 0;
                            if (!Int64.TryParse(MyUI.數字鍵盤.value, out value))
                            {
                                value = Int64.MaxValue;
                            }
                            PLC.properties.Device.Set_DoubleWord(寫入元件位置, value);
                        }
                        if (PLC != null && _數值更動旗標 != null && _數值更動旗標 != "")
                        {
                            if (LadderProperty.DEVICE.TestDevice(_數值更動旗標))
                            {
                                PLC.properties.Device.Set_Device(_數值更動旗標, true);
                            }
                        }

                    }
                    else
                    {
                        Value_Str_Buf = MyUI.數字鍵盤.value;
                    }
                }
            }

        }
     
        public PLC_NumBox()
        {
            InitializeComponent();
        }

        #region Function
        public int GetValue()
        {
            int value = 0;
            this.GetValue(ref value);
            return value;
        }
        public void GetValue(ref Int64 value)
        {    
            if (_讀取元件位置 != "" && _讀取元件位置 != null && PLC != null)
            {
                object temp = new object();
                if (字元長度 == WordLengthEnum.單字元)
                {
                    PLC.properties.Device.Get_Device(_讀取元件位置, out temp);
                    value = (Int64)temp;
                }
                else if (字元長度 == WordLengthEnum.雙字元)
                {
                    value = PLC.properties.Device.Get_DoubleWord(_讀取元件位置);
                }
            }
            else
            {
                value = Int64_Value;
            }
        }
        public void GetValue(ref int value)
        {

            if (_讀取元件位置 != string.Empty && PLC != null)
            {
                value = PLC.properties.Device.Get_DataFast_Ex(_讀取元件位置);
            }
            else
            {
                value = Int_Value;
            }
        }
        public void SetValue(int value)
        {
            if (_寫入元件位置 != string.Empty && PLC != null)
            {
                PLC.properties.Device.Set_DataFast_Ex(_寫入元件位置, value);
            }
            else
            {
                Int_Value = value;
            }
        }
        public void SetValue(Int64 value)
        {
            if (_寫入元件位置 != "" && _寫入元件位置 != null && PLC != null)
            {
                if (字元長度 == WordLengthEnum.雙字元)
                {
                    PLC.properties.Device.Set_DoubleWord(_寫入元件位置, value);
                }
            }
            else
            {
                Int64_Value = value;
            }
        }
        public bool Set_PLC(LowerMachine pLC)
        {
            if (pLC != null)
            {
                this.PLC = pLC;
                return true;
            }
            return false;
        }
        public void Load_PLC_Device(PLC_Device pLC_Device)
        {
            this.讀取元件位置 = pLC_Device.GetAdress();
            this.寫入元件位置 = pLC_Device.GetAdress();
        }

        public void Run(LowerMachine pLC)
        {
            if (PLC == null)
            {
                if (Set_PLC(pLC))
                {
                    PLC.Add_UI_Method(Run);
                    this.寫入位置註解 = this.寫入位置註解;
                    this.讀取位置註解 = this.讀取位置註解;
                }
            }
        }
        public void Run(LowerMachine pLC , Basic.MyThread myThread)
        {
            if (PLC == null)
            {
                if (Set_PLC(pLC))
                {
                    myThread.Add_Method(Run);
                    //PLC.Add_UI_Method(Run);

                    this.寫入位置註解 = this.寫入位置註解;
                    this.讀取位置註解 = this.讀取位置註解;
                }
            }
        }
        public void Run()
        {
            flag_init = true;
            if (PLC != null)
            {
                if (_讀取元件位置 != string.Empty)
                {

                    if (字元長度 == WordLengthEnum.單字元)
                    {
                        value = PLC.properties.Device.Get_DataFast_Ex(_讀取元件位置);
                    }
                    else if (字元長度 == WordLengthEnum.雙字元)
                    {
                        value = PLC.properties.Device.Get_DoubleWord(_讀取元件位置);
                    }
                    Value_Str_Buf = value.ToString();
                }
                string text = this.Text;
                if (this.Text != Value_Str_Buf)
                {
                    this.Text = Value_Str_Buf;
                }
        
                if (_致能讀取位置 != string.Empty)
                {
                    if (LadderProperty.DEVICE.TestDevice(_致能讀取位置))
                    {
                        PLC.properties.Device.Get_Device(_致能讀取位置, out value);
                        if (value is bool)
                        {
                            if (Enable_buf != (bool)value)
                            {
                                this.Invoke(new Action(delegate
                                {
                                    this.Enabled = (bool)value;
                                }));
                                Enable_buf = (bool)value;
                            }
                        }
                    }
                }
            }
           
        }
        public void ShowKeyBoard()
        {
            if (顯示螢幕小鍵盤 && !MyUI.數字鍵盤.視窗已建立)
            {
                MyUI.數字鍵盤.小數點位置 = this.小數點位置;
                MyUI.數字鍵盤.音效 = 音效;
                MyUI.數字鍵盤 form = MyUI.數字鍵盤.GetForm();
                Form main_form = this.FindForm();
                Point p0 = this.PointToScreen(new Point());
                Point p1 = new Point(this.Location.X + Size.Width, this.Location.Y + Size.Height);
                p0.X = main_form.Location.X + (main_form.Width / 2) - (form.Width / 2);
                p0.Y = main_form.Location.Y + (main_form.Height / 2) - (form.Height / 2);
                form.SetPosition(p0);
                form.TopLevel = true;//將表單顯示在最上層。
                form.Activate();//啟動表單並且給予焦點。
                form.Init_Text = this.textBox1.Text;
                FormDelegate formDelegate = new FormDelegate(form_show);
                Invoke(formDelegate, form);
            }
        }
        #endregion
        #region Event
        public override System.Windows.Forms.Layout.LayoutEngine LayoutEngine
        {
            get
            {
                if (!flag_init) 數字轉換(0.ToString(), textBox1);
                return base.LayoutEngine;
            }
        }
        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Focus();
            this.ShowKeyBoard();
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar <= 57 && (int)e.KeyChar >= 48) || (int)e.KeyChar == 8) // 8 > BackSpace
            {
                e.Handled = false;
            }
            else if ((int)e.KeyChar == 13)
            {
                this.Text = textBox1.Text;
            }
            else e.Handled = true;
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
        }
        private void button_DOWN_Click(object sender, EventArgs e)
        {
            this.SetValue(this.GetValue() - this.微調數值);
        }
        private void button_UP_Click(object sender, EventArgs e)
        {
            this.SetValue(this.GetValue() + this.微調數值);
        }


        #endregion


    }
}
