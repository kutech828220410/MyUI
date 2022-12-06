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
    public partial class PLC_NumBox : TextBox
    {
        private MyConvert myConvert = new MyConvert();
        private LowerMachine PLC;
        private bool flag_init = false;
        #region 自訂屬性
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
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z") divice_OK = true;
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
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z") divice_OK = true;
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
        #endregion
        public override System.Windows.Forms.Layout.LayoutEngine LayoutEngine
        {
            get
            {
                if (!flag_init) 數字轉換(0.ToString(), textBox1);
                return base.LayoutEngine;
            }
        }
        private delegate void BaseTextDelegate(string str);
        void BaseText(string str)
        {
            base.Text = str;
        }
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
        BaseTextDelegate baseTextDelegate;
        void 數字轉換(string value, TextBox textBox1)
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
            CallBackUI.textbox.字串更換(value, textBox1);
            if (this.IsHandleCreated)
            {
                baseTextDelegate = new BaseTextDelegate(BaseText);
                Invoke(baseTextDelegate, value.Replace(".", ""));
            }

        }
        public PLC_NumBox()
        {
            InitializeComponent();
            // init_finish = true;
        }
        bool Enable_buf = true;
        private delegate void enableUI(bool enable);
        public void EnableUI(bool enable)
        {
            this.Enabled = enable;
        }
        public void GetValue(ref Int64 value)
        {
            object temp = new object();
            if (_讀取元件位置 != "" && _讀取元件位置 != null && PLC != null)
            {
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
        }
        public void GetValue(ref int value)
        {
            object temp = new object();
            if (_讀取元件位置 != "" && _讀取元件位置 != null && PLC != null)
            {
                if (字元長度 == WordLengthEnum.單字元)
                {
                    PLC.properties.Device.Get_Device(_讀取元件位置, out temp);
                    value = (int)temp;
                }
            }
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
        private delegate void FormDelegate(MyUI.數字鍵盤 form);
        void form_show(MyUI.數字鍵盤 form)
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
                        this.Text = MyUI.數字鍵盤.value;
                    }
                }
            }

        }
        private void textBox1_Click(object sender, EventArgs e)
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
                FormDelegate formDelegate = new FormDelegate(form_show);
                Invoke(formDelegate, form);
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
        public void Run(LowerMachine pLC)
        {
            if (PLC == null)
            {
                if (Set_PLC(pLC))
                {
                    PLC.Add_UI_Method(Run);
                }
            }
        }
        object value;
        enableUI Delegate;
        public void Run()
        {
            flag_init = true;
            if (_讀取元件位置 != "" && _讀取元件位置 != null && PLC != null)
            {

                if (字元長度 == WordLengthEnum.單字元)
                {
                    PLC.properties.Device.Get_Device(_讀取元件位置, out value);
                }
                else if (字元長度 == WordLengthEnum.雙字元)
                {
                    value = PLC.properties.Device.Get_DoubleWord(_讀取元件位置);
                }
                Text = value.ToString();

            }
            if (PLC != null && _致能讀取位置 != null && _致能讀取位置 != "")
            {
                if (LadderProperty.DEVICE.TestDevice(_致能讀取位置))
                {
                    PLC.properties.Device.Get_Device(_致能讀取位置, out value);
                    Delegate = new enableUI(EnableUI);
                    if (value is bool)
                    {
                        if (Enable_buf != (bool)value)
                        {
                            Invoke(Delegate, (bool)value);
                            Enable_buf = (bool)value;
                        }
                    }
                }
            }
        }
    }
}
