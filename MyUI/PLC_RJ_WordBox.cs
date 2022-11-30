using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LadderConnection;
using Basic;

namespace MyUI
{
    public class PLC_RJ_WordBox : RJ_TextBox
    {
        private MyConvert myConvert = new MyConvert();
        private LowerMachine PLC;
        private bool flag_init = false;
        readonly private char[] init_str = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        #region 自訂屬性
        private bool _多行顯示 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 多行顯示
        {
            get { return _多行顯示; }
            set
            {
                this.Multiline = value;
                _多行顯示 = value;
            }
        }
        public override RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }
            set
            {
                base.RightToLeft = value;
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

        private bool _ReadOnly = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool ReadOnly
        {
            get
            {
                //textBox1.ReadOnly = _ReadOnly;
                return _ReadOnly;
            }
            set
            {
                _顯示螢幕小鍵盤 = false;
                _ReadOnly = value;
                //textBox1.ReadOnly = _ReadOnly;

            }
        }
        private string _中文致能旗標 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 中文致能旗標
        {
            get { return _中文致能旗標; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _中文致能旗標 = value;
                else _中文致能旗標 = "";
            }
        }
        private string _致能讀取位置 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 致能讀取位置
        {
            get { return _致能讀取位置; }
            set
            {
                value = value.ToUpper();
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
                value = value.ToUpper();
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

        private string _起始元件位置 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 起始元件位置
        {
            get { return _起始元件位置; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z") divice_OK = true;
                }

                if (divice_OK) _起始元件位置 = value;
                else _起始元件位置 = "";
            }
        }

        private string _字節數DATA = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 字節數DATA
        {
            get { return _字節數DATA; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z" || temp == "K") divice_OK = true;
                }

                if (divice_OK) _字節數DATA = value;
                else _字節數DATA = "";
            }
        }
        #endregion
        object object_字節數DATA;
        int int_字節數DATA;
        private void Get字節數()
        {
            if (_字節數DATA != "" && _字節數DATA != null && PLC != null)
            {

                PLC.properties.Device.Get_Device(_字節數DATA, out object_字節數DATA);
                if (object_字節數DATA is int) int_字節數DATA = (int)object_字節數DATA;


            }
        }
        private void GetInitStr()
        {
            string value = "";
            Get字節數();
            for (int i = 0; i < int_字節數DATA; i++)
            {
                int j = i % 26;
                value += init_str[j];
            }


            if (this.IsHandleCreated)
            {
                this.Invoke(new Action(delegate
                {
                    this.Text = value;
                }));
                baseTextDelegate = new BaseTextDelegate(BaseText);
                Invoke(baseTextDelegate, value);
            }
            else
            {
                this.Text = value;
            }
        }
        private delegate void BaseTextDelegate(string str);
        BaseTextDelegate baseTextDelegate;
        void BaseText(string str)
        {
            this.Texts = str;
        }
        [Browsable(false)]
        public override string Text
        {
            get
            {               
                return this.Texts;
            }
            set
            {

                this.Invoke(new Action(delegate
                {
                    this.Texts = value;
                }));
                if (this.IsHandleCreated)
                {
                    baseTextDelegate = new BaseTextDelegate(BaseText);
                    Invoke(baseTextDelegate, value.Replace(".", ""));
                }
            }
        }
        bool Enable_buf = true;
        private delegate void enableUI(bool enable);
        public void EnableUI(bool enable)
        {
            this.Enabled = enable;
        }

        public PLC_RJ_WordBox()
        {

        }
        private delegate void FormDelegate(MyUI.字母鍵盤 form);
        void form_show(MyUI.字母鍵盤 form)
        {
            form.ShowDialog();
            this.Text = 字母鍵盤.InitText;
            SetString(this.Text);
        }
        string out_string = "";
        List<byte> out_byte = new List<byte>();
        int value_int;
        string DeviceName;
        int DeviceIndex;

        public string GetString()
        {
            this.Get字節數();
            if (_起始元件位置 != "" && _起始元件位置 != null && PLC != null)
            {

                out_string = "";
                out_byte.Clear();
                DeviceName = _起始元件位置.Substring(0, 1);
                DeviceIndex = int.Parse(_起始元件位置.Substring(1, _起始元件位置.Length - 1));
                for (int i = 0; i < int_字節數DATA; i++)
                {
                    value_int = PLC.properties.Device.Get_DataFast(DeviceName, DeviceIndex + i);
                    if (value_int != 0)
                    {
                        if (!bool_中文致能旗標) if (value_int < 0 || value_int > 255) value_int = 63;
                        out_byte.Add((byte)(value_int));
                    }
                }
                if (bool_中文致能旗標)
                {
                    out_string = System.Text.Encoding.UTF8.GetString(out_byte.ToArray());
                }
                else
                {
                    for (int i = 0; i < out_byte.Count; i++)
                    {
                        out_string += myConvert.Int32ToASCII((int)out_byte[i]);
                    }

                }

            }
            return out_string;
        }
        public void SetString(string str)
        {
            Get字節數();
            if (bool_中文致能旗標)
            {
                byte[] byte_temp = System.Text.Encoding.UTF8.GetBytes(str);
                for (int i = 0; i < int_字節數DATA; i++)
                {
                    if (i < byte_temp.Length) PLC.properties.Device.Set_Device(LadderProperty.DEVICE.DeviceOffset(_起始元件位置, i), (int)byte_temp[i]);
                    else PLC.properties.Device.Set_Device(LadderProperty.DEVICE.DeviceOffset(_起始元件位置, i), 0);
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
                char[] char_temp = str.ToCharArray();
                for (int i = 0; i < int_字節數DATA; i++)
                {
                    if (i < char_temp.Length) PLC.properties.Device.Set_Device(LadderProperty.DEVICE.DeviceOffset(_起始元件位置, i), (int)char_temp[i]);
                    else PLC.properties.Device.Set_Device(LadderProperty.DEVICE.DeviceOffset(_起始元件位置, i), 0);
                }
                if (PLC != null && _數值更動旗標 != null && _數值更動旗標 != "")
                {
                    if (LadderProperty.DEVICE.TestDevice(_數值更動旗標))
                    {
                        PLC.properties.Device.Set_Device(_數值更動旗標, true);
                    }
                }
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
        string str_temp;
        bool bool_中文致能旗標 = false;
        object object_中文致能旗標 = new object();
        bool Focused = false;
        public void Run()
        {
            flag_init = true;
            str_temp = "";

            if (PLC.properties.Device.Get_Device(_中文致能旗標, out object_中文致能旗標))
            {
                if (object_中文致能旗標 is bool)
                {
                    bool_中文致能旗標 = (bool)object_中文致能旗標; ;
                }
            }
            if (!Focused)
            {
                if (_起始元件位置 != "" && _起始元件位置 != null && PLC != null)
                {
                    Text = GetString();
                }
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
