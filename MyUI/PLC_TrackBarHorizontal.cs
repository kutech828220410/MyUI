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
   　[System.Drawing.ToolboxBitmap(typeof(PLC_TrackBarHorizontal), "PLC_TrackBarHorizontal.bmp")]
    public partial class PLC_TrackBarHorizontal : UserControl
    {
        public PLC_TrackBarHorizontal()
        {
            InitializeComponent();
        }
        private MyConvert myConvert = new MyConvert();
        private LowerMachine PLC;
        private bool flag_init = false;
        #region 隱藏屬性
        [Browsable(false)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
        [Browsable(false)]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }
        [Browsable(false)]
        public override ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
            set
            {
                base.BackgroundImageLayout = value;
            }
        }
        [Browsable(false)]
        public override Cursor Cursor
        {
            get
            {
                return base.Cursor;
            }
            set
            {
                base.Cursor = value;
            }
        }
        [Browsable(false)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }
        [Browsable(false)]
        public override System.Drawing.Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }
        [Browsable(false)]
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

        #endregion
        #region 自訂屬性
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
        private int _小數點位置 = 0;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int 小數點位置
        {
            get
            {
                數字轉換(0.ToString(), label_Value);
                return _小數點位置;
            }
            set
            {
                數字轉換(0.ToString(), label_Value);
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
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "Z" || temp == "F") divice_OK = true;
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
                value = value.ToUpper();
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
        [ReadOnly(true), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public WordLengthEnum 字元長度 { get; set; }
        public enum WordLengthEnum : int
        {
            單字元, 雙字元
        }

        public TickStyle _TickStyle = TickStyle.BottomRight;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public TickStyle TickStyle
        {
            get { return _TickStyle; }
            set
            {
                this.trackBar1.TickStyle = _TickStyle;
                _TickStyle = value;
            }
        }
        private int _刻度間隔 = 10;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int 刻度間隔
        {
            get { return _刻度間隔; }
            set
            {
                this.trackBar1.TickFrequency = value;
                _刻度間隔 = value;
            }
        }
        private int _刻度最大值 = 100;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int 刻度最大值
        {
            get { return _刻度最大值; }
            set
            {
                this.trackBar1.Maximum = value;
                _刻度最大值 = value;
            }
        }
        private int _刻度最小值 = 0;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int 刻度最小值
        {
            get { return _刻度最小值; }
            set
            {
                this.trackBar1.Minimum = value;
                _刻度最小值 = value;
            }
        }

        private string _標題內容 = "Title";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 標題內容
        {
            get { return _標題內容; }
            set
            {
                label_Title.Text = value;
                _標題內容 = value;
            }
        }
        private bool _顯示標題 = true;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 顯示標題
        {
            get { return _顯示標題; }
            set
            {
                label_Title.Visible = value;
                _顯示標題 = value;
            }
        }
        private bool _顯示數值 = true;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 顯示數值
        {
            get { return _顯示數值; }
            set
            {
                label_Value.Visible = value;
                _顯示數值 = value;
            }
        }
        private Font _標題字體 = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Font 標題字體
        {
            get { return _標題字體; }
            set
            {
                _標題字體 = value;
                label_Title.Font = _標題字體;
            }
        }
        private Font _數值字體 = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Font 數值字體
        {
            get { return _數值字體; }
            set
            {
                _數值字體 = value;
                label_Value.Font = _數值字體;
            }
        }
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
        [ReadOnly(false), Browsable(false)]
        public int Value
        {
            get
            {
                if (this.IsHandleCreated) return this.GetValue();
                else return 0;
            }
            set
            {
                if (this.IsHandleCreated) this.SetValue(value);
            }
        }

        public override System.Windows.Forms.Layout.LayoutEngine LayoutEngine
        {
            get
            {
                if (!flag_init) 數字轉換(0.ToString(), label_Value);
                return base.LayoutEngine;
            }
        }
        bool Enable_buf = true;
        private delegate void enableUI(bool enable);
        public void EnableUI(bool enable)
        {
            this.Enabled = enable;
        }
        int trackBar_value_buf = 999;
        public int GetValue()
        {
            int value = 0;
            this.GetValue(ref value);
            return value;
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
        public void SetValue(int value)
        {
            if (_寫入元件位置 != string.Empty && PLC != null)
            {
                PLC.properties.Device.Set_DataFast_Ex(_寫入元件位置, value);
            }
            else
            {
               // Int_Value = value;
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
                //Int64_Value = value;
            }
        }
        public void Load_PLC_Device(PLC_Device pLC_Device)
        {
            this.讀取元件位置 = pLC_Device.GetAdress();
            this.寫入元件位置 = pLC_Device.GetAdress();
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
        public void Run()
        {
            flag_init = true;
            if (PLC != null && _致能讀取位置 != null && _致能讀取位置 != "")
            {
                if (LadderProperty.DEVICE.TestDevice(_致能讀取位置))
                {
                    PLC.properties.Device.Get_Device(_致能讀取位置, out value);
                    this.寫入位置註解 = this.寫入位置註解;
                    this.讀取位置註解 = this.讀取位置註解;
                    enableUI Delegate = new enableUI(EnableUI);
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
            if (_讀取元件位置 != "" && _讀取元件位置 != null && _寫入元件位置 != "" && _寫入元件位置 != null && PLC != null)
            {

                if (字元長度 == WordLengthEnum.單字元)
                {
                    PLC.properties.Device.Get_Device(_讀取元件位置, out value);
                }
                else if (字元長度 == WordLengthEnum.雙字元)
                {
                    value = PLC.properties.Device.Get_DoubleWord(_讀取元件位置);
                }
                if (trackBar_value_buf != (int)value)
                {
                    if ((int)value > trackBar1.Maximum)
                    {
                        value = trackBar1.Maximum;
                        PLC.properties.Device.Set_Device(_寫入元件位置, (int)value);
                    }
                    if ((int)value < trackBar1.Minimum)
                    {
                        value = trackBar1.Minimum;
                        PLC.properties.Device.Set_Device(_寫入元件位置, (int)value);
                    }

                    trackBar_value_buf = (int)value;
                    this.Invoke(new Action(delegate
                    {
                        數字轉換(trackBar_value_buf.ToString(), label_Value);
                        trackBar1.Value = trackBar_value_buf;
                    }));                          
                }
            }
        }
        void 數字轉換(string value, Label label)
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
            label.Text = value.ToString();
        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            if (_讀取元件位置 != "" && _讀取元件位置 != null && _寫入元件位置 != "" && _寫入元件位置 != null && PLC != null)
            {
                PLC.properties.Device.Set_Device(_寫入元件位置, (int)trackBar1.Value);
            }
            else
            {

            }
            if (this.ScrollEvent != null) ScrollEvent(this.GetValue());
        }

        #region Event

        public delegate void ScrollEventHandler(int value);
        public event ScrollEventHandler ScrollEvent;
   
        #endregion
    }
}
