using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using LadderConnection;
using System.Media;
using System.Runtime.InteropServices;
namespace MyUI
{
     [System.Drawing.ToolboxBitmap(typeof(Button))]
     [Designer(typeof(ComponentSet.JLabelExDesigner))]  
    public partial class PLC_Button : UserControl
    {
        private Basic.Keyboard Keys = new Basic.Keyboard();
        public bool Is_ButPress
        {
            get
            {
                return this.but_press;
            }
        }
        public bool _but_press = false;
        public bool but_press
        {
            get
            {
                return this._but_press;
            }
            set
            {
                this._but_press = value;

            }
        }
        public bool but_press_buf = false;
        private bool IN = false;
        private bool M0 = false;
        private bool M1 = false;
        private bool M2 = false;
        private bool M3 = false;
        private bool M4 = false;
        private bool OUT = false;
        private bool _PLC_要讀取 = false;
        private bool PLC_要讀取
        {
            get
            {
                return (_讀取元件位置 != "");
            }
        }
        private bool _PLC_要寫入 = false;
        private bool PLC_要寫入
        {
            get
            {
                return (_寫入元件位置 != "");
            }
        }
        private object value = new object();
        private bool flag_init = false;
        private bool Value_Buf = false;
        public LowerMachine PLC;
        private bool IsRunOnce = false;
        private bool _FLAG_寫入 = false;
        private bool FLAG_寫入
        {
            get
            {
                return _FLAG_寫入;
            }
            set
            {
                _FLAG_寫入 = value;
            }
        }
        private bool _FLAG_讀取 = false;
        private bool _FLAG_讀取_buf = true;
        private bool FLAG_讀取
        {
            get
            {
                //if (this._FLAG_讀取)
                //{
                //    if(!this.讀取位元反向)
                //    {
                //        this.sub_按鈕狀態設為ON();
                //    }
                //    else
                //    {
                //        this.sub_按鈕狀態設為OFF();
                //    }
                //}
                //if (!this._FLAG_讀取)
                //{
                //    if (!this.讀取位元反向)
                //    {
                //        this.sub_按鈕狀態設為OFF();
                //    }
                //    else
                //    {
                //        this.sub_按鈕狀態設為ON();                        
                //    }
                //}
                
                return _FLAG_讀取;
            }
            set
            {
                this._FLAG_讀取 = value;
                if (this._FLAG_讀取)
                {
                    if (!this.讀取位元反向)
                    {
                        this.sub_按鈕狀態設為ON();
                    }
                    else
                    {
                        this.sub_按鈕狀態設為OFF();
                    }
                }
                if (!this._FLAG_讀取)
                {
                    if (!this.讀取位元反向)
                    {
                        this.sub_按鈕狀態設為OFF();
                    }
                    else
                    {
                        this.sub_按鈕狀態設為ON();
                    }
                }
            }
        }
        public bool FLAG_buf = false;
        private bool Enable_buf = true;
        private bool Visible_buf = true;

        public delegate void CaptureDelegate();
        public CaptureDelegate captureDelegate;
        public void LabelCapture()
        {
            if (IsRunOnce &&  !_事件驅動) but_press = label1.Capture;
        }
        public bool Bool
        {
            get
            {
                if (flag_init) return this.GetValue();
                else return false;
              
            }
            set
            {
                if (flag_init) this.SetValue(value);
            }
        }
        public bool 起始狀態
        {
            get
            {
                return Value_Buf;
            }
            set
            {
                Value_Buf = value;
            }
        }
        public PLC_Button()
        {
            InitializeComponent();
        }
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

        bool _顯示 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性-MessageBox"), Description(""), DefaultValue("")]
        public bool 顯示
        {
            get
            {
                return _顯示;
            }
            set
            {
                _顯示 = value;
            }
        }
        string _標題 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性-MessageBox"), Description(""), DefaultValue("")]
        public string 標題
        {
            get
            {
                return _標題;
            }
            set
            {
                _標題 = value;
            }
        }
        string _內容 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性-MessageBox"), Description(""), DefaultValue("")]
        public string 內容
        {
            get
            {
                return _內容;
            }
            set
            {
                _內容 = value;
            }
        }
        MessageBoxIcon _Icon = MessageBoxIcon.Warning;
        [ReadOnly(false), Browsable(true), Category("自訂屬性-MessageBox"), Description(""), DefaultValue("")]
        public MessageBoxIcon Icon
        {
            get
            {
                return _Icon;
            }
            set
            {
                _Icon = value;
            }
        }
        private string _旗標位置 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性-MessageBox"), Description(""), DefaultValue("")]
        public virtual string 旗標位置
        {
            get { return _旗標位置; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "M" || temp == "S") divice_OK = true;
                }

                if (divice_OK) _旗標位置 = value;
                else _旗標位置 = "";
            }
        }

        public enum StyleEnum : int
        {
            經典 = 0, Light_Rect_綠, Light_Rect_紅, Light_Rect_黃, Light_Rect_藍, Light_Rect_灰, Light_Rect_紫, Lamp_紅, Lamp_綠, Lamp_黃, 右箭頭, 左箭頭, 上箭頭, 下箭頭, 自定義
        }
        StyleEnum _Style = new StyleEnum();
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public StyleEnum Style
        {
            get
            {
                ImageStatus(_Style);
                return _Style;
            }
            set
            {
                ImageStatus(value);
                顯示狀態 = false;
                _Style = value;
            }
        }
        void ImageStatus(StyleEnum value)
        {
            if (value == StyleEnum.經典)
            {
                image_OFF = Resource1.經典_OFF;
                image_ON = Resource1.經典_ON;
            }
            else if (value == StyleEnum.Light_Rect_綠)
            {
                image_OFF = Resource1.Light_Rect_綠_OFF;
                image_ON = Resource1.Light_Rect_綠_ON;
            }
            else if (value == StyleEnum.Light_Rect_紅)
            {
                image_OFF = Resource1.Light_Rect_紅_OFF;
                image_ON = Resource1.Light_Rect_紅_ON;
            }
            else if (value == StyleEnum.Light_Rect_黃)
            {
                image_OFF = Resource1.Light_Rect_黃_OFF;
                image_ON = Resource1.Light_Rect_黃_ON;
            }
            else if (value == StyleEnum.Light_Rect_藍)
            {
                image_OFF = Resource1.Light_Rect_藍_OFF;
                image_ON = Resource1.Light_Rect_藍_ON;
            }
            else if (value == StyleEnum.Light_Rect_灰)
            {
                image_OFF = Resource1.Light_Rect_灰_OFF;
                image_ON = Resource1.Light_Rect_灰_ON;
            }
            else if (value == StyleEnum.Light_Rect_紫)
            {
                image_OFF = Resource1.Light_Rect_紫_OFF;
                image_ON = Resource1.Light_Rect_紫_ON;
            }
            else if (value == StyleEnum.Lamp_紅)
            {
                image_OFF = Resource1.Lamp_紅_OFF;
                image_ON = Resource1.Lamp_紅_ON;
            }
            else if (value == StyleEnum.Lamp_綠)
            {
                image_OFF = Resource1.Lamp_綠_OFF;
                image_ON = Resource1.Lamp_綠_ON;
            }
            else if (value == StyleEnum.Lamp_黃)
            {
                image_OFF = Resource1.Lamp_黃_OFF;
                image_ON = Resource1.Lamp_黃_ON;
            }
            else if (value == StyleEnum.左箭頭)
            {
                image_OFF = Resource1.左箭頭_OFF_01;
                image_ON = Resource1.左箭頭_ON_01;
            }
            else if (value == StyleEnum.右箭頭)
            {
                image_OFF = Resource1.右箭頭_OFF_01;
                image_ON = Resource1.右箭頭_ON_01;
            }
            else if (value == StyleEnum.上箭頭)
            {
                image_OFF = Resource1.上箭頭_OFF_01;
                image_ON = Resource1.上箭頭_ON_01;
            }
            else if (value == StyleEnum.下箭頭)
            {
                image_OFF = Resource1.下箭頭_OFF_01;
                image_ON = Resource1.下箭頭_ON_01;
            }
        }
        string _提示文字 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 提示文字
        {
            get
            {
                return _提示文字;
            }
            set
            {
                _提示文字 = value;
            }
        }

        bool _讀取位元反向 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 讀取位元反向
        {
            get
            {
                return _讀取位元反向;
            }
            set
            {
                _讀取位元反向 = value;
            }
        }

        private bool _顯示狀態 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 顯示狀態
        {
            get
            {
                if (_顯示狀態) sub_按鈕狀態設為ON();
                else sub_按鈕狀態設為OFF();
                return _顯示狀態;
            }
            set
            {
                _顯示狀態 = value;
                if (value) sub_按鈕狀態設為ON();
                else sub_按鈕狀態設為OFF();
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

        private Image image_OFF;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description("按鈕狀態OFF圖片"), DefaultValue(1)]
        public Image 狀態OFF圖片
        {
            get { return image_OFF; }
            set
            {
                image_OFF = value;

            }
        }
        private Image image_ON;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description("按鈕狀態ON圖片"), DefaultValue(1)]
        public Image 狀態ON圖片
        {
            get { return image_ON; }
            set
            {
                image_ON = value;

            }
        }
        private string _OFF文字內容 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string OFF_文字內容
        {
            get { return _OFF文字內容; }
            set
            {
                _OFF文字內容 = value;
                if (_文字鎖住) ON_文字內容 = _OFF文字內容;
                label1.Text = _OFF文字內容;
            }
        }
        private Font _OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Font OFF_文字字體
        {
            get { return _OFF_文字字體; }
            set
            {
                _OFF_文字字體 = value;
                if (_字型鎖住) _ON_文字字體 = _OFF_文字字體;
                label1.Font = _OFF_文字字體;
            }
        }
        private Color _OFF_文字顏色 = Color.Black;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Color OFF_文字顏色
        {
            get { return _OFF_文字顏色; }
            set
            {
                _OFF_文字顏色 = value;
                label1.ForeColor = _OFF_文字顏色;
            }
        }
        private string _ON文字內容 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string ON_文字內容
        {
            get { return _ON文字內容; }
            set
            {
                _ON文字內容 = value;
                if (_文字鎖住) _OFF文字內容 = _ON文字內容;
                label1.Text = _ON文字內容;
            }
        }
        private Font _ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Font ON_文字字體
        {
            get { return _ON_文字字體; }
            set
            {
                _ON_文字字體 = value;
                if (_字型鎖住) _OFF_文字字體 = _ON_文字字體;
            }
        }
        private Color _ON_文字顏色 = Color.White;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Color ON_文字顏色
        {
            get { return _ON_文字顏色; }
            set
            {
                _ON_文字顏色 = value;
                label1.ForeColor = _ON_文字顏色;
            }
        }
        private Color _OFF_背景顏色 = Control.DefaultBackColor;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Color OFF_背景顏色
        {
            get { return _OFF_背景顏色; }
            set
            {
                _OFF_背景顏色 = value;
                label1.ForeColor = _OFF_背景顏色;
            }
        }
        private Color _ON_背景顏色 = Control.DefaultBackColor;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Color ON_背景顏色
        {
            get { return _ON_背景顏色; }
            set
            {
                _ON_背景顏色 = value;
                label1.ForeColor = _ON_背景顏色;
            }
        }

        private bool _事件驅動 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue(1)]
        public bool 事件驅動
        {
            get
            {
                return _事件驅動;
            }
            set
            {
                _事件驅動 = value;
            }
        }
        private bool _文字鎖住 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue(1)]
        public bool 文字鎖住
        {
            get
            {
                if (_文字鎖住) ON_文字內容 = OFF_文字內容;
                return _文字鎖住;
            }
            set
            {
                if (value) ON_文字內容 = OFF_文字內容;
                _文字鎖住 = value;
            }
        }
        private bool _字型鎖住 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue(1)]
        public bool 字型鎖住
        {
            get
            {
                if (_字型鎖住) ON_文字字體 = OFF_文字字體;
                return _字型鎖住;
            }
            set
            {
                if (value) ON_文字字體 = OFF_文字字體;
                _字型鎖住 = value;
            }
        }
        private bool _讀寫鎖住 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue(1)]
        public virtual bool 讀寫鎖住
        {
            get { return _讀寫鎖住; }
            set
            {
                _讀寫鎖住 = value;
            }
        }
        public enum StatusEnum : int
        {
            保持型, 交替型
        }
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual StatusEnum 按鈕型態 { get; set; }

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

        private string _寫入元件位置 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 寫入元件位置
        {
            get { return _寫入元件位置; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _寫入元件位置 = value;
                else _寫入元件位置 = "";
            }
        }
        private string _讀取元件位置 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 讀取元件位置
        {
            get { return _讀取元件位置; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _讀取元件位置 = value;
                else _讀取元件位置 = "";
            }
        }

        public enum PressEnum : int
        {
            Mouse_左鍵, Key_上, Key_下, Key_左, Key_右, Key_右三角引號, Key_左三角引號, Key_PageUp, Key_PageDown, Key_Esc, Key_Enter
        }
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual PressEnum 按鍵方式 { get; set; }

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

        private string _顯示讀取位置 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public virtual string 顯示讀取位置
        {
            get { return _顯示讀取位置; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _顯示讀取位置 = value;
                else _顯示讀取位置 = "";
            }
        }
        #endregion

        #region Function
        public void sub_按鈕狀態設為ON()
        {
            if (this.IsHandleCreated)
            {
                this.Invoke(new Action(delegate
                {
                    label1.BackgroundImage = image_ON;
                    label1.Text = this.ON_文字內容;
                    label1.Font = ON_文字字體;
                    label1.ForeColor = ON_文字顏色;
                    label1.BackColor = this.ON_背景顏色;
                    label1.BackgroundImageLayout = ImageLayout.Stretch;
                }));
            }
            else
            {
                label1.BackgroundImage = image_ON;
                label1.Text = this.ON_文字內容;
                label1.Font = ON_文字字體;
                label1.ForeColor = ON_文字顏色;
                label1.BackColor = this.ON_背景顏色;
                label1.BackgroundImageLayout = ImageLayout.Stretch;
            }
     
        }
        public void sub_按鈕狀態設為OFF()
        {
            if (this.IsHandleCreated)
            {
                this.Invoke(new Action(delegate
                {
                    label1.BackgroundImage = image_OFF;
                    label1.Text = this.OFF_文字內容;
                    label1.Font = OFF_文字字體;
                    label1.ForeColor = OFF_文字顏色;
                    label1.BackColor = this.OFF_背景顏色;
                    label1.BackgroundImageLayout = ImageLayout.Stretch;
                }));
            }
            else
            {
                label1.BackgroundImage = image_OFF;
                label1.Text = this.OFF_文字內容;
                label1.Font = OFF_文字字體;
                label1.ForeColor = OFF_文字顏色;
                label1.BackColor = this.OFF_背景顏色;
                label1.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }
        public bool Load_WriteState()
        {
            按鈕狀態計算();
            return FLAG_寫入;
        }
        public void Set_LoadState(bool FLAG)
        {
            _FLAG_讀取_buf = FLAG;
            按鈕狀態計算();
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
        public void EnableCheck()
        {
            if (PLC != null && _致能讀取位置 != null && _致能讀取位置 != "")
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
        public void VisibleCheck()
        {
            if (PLC != null && _顯示讀取位置 != null && _顯示讀取位置 != "")
            {
                if (LadderProperty.DEVICE.TestDevice(_顯示讀取位置))
                {
                    PLC.properties.Device.Get_Device(_顯示讀取位置, out value);
                    if (value is bool)
                    {
                        if (Visible_buf != (bool)value)
                        {
                            this.Invoke(new Action(delegate
                            {
                                this.Visible = (bool)value;
                            }));
                            Visible_buf = (bool)value;
                        }
                    }
                }
            }
        }
        public void SetVisible(bool value)
        {
            if (PLC != null && _顯示讀取位置 != null && _顯示讀取位置 != "")
            {
                if (LadderProperty.DEVICE.TestDevice(_顯示讀取位置))
                {
                    PLC.properties.Device.Set_Device(_顯示讀取位置, value);
                }
            }
        }

        public virtual void Run(LowerMachine pLC)
        {
            if (PLC == null)
            {
                if (Set_PLC(pLC))
                {
                    PLC.Add_UI_Method(Run);
                    if (this.提示文字 != "") this.toolTip.SetToolTip(label1, this.提示文字);
                    this.寫入位置註解 = this.寫入位置註解;
                    this.讀取位置註解 = this.讀取位置註解;
                }
            }
        }
        public virtual void Run(LowerMachine pLC, Basic.MyThread myThread)
        {
            if (PLC == null)
            {
                if (Set_PLC(pLC))
                {
                    myThread.Add_Method(Run);
                    //PLC.Add_UI_Method(Run);
                    if (this.提示文字 != "") this.toolTip.SetToolTip(label1, this.提示文字);
                    this.寫入位置註解 = this.寫入位置註解;
                    this.讀取位置註解 = this.讀取位置註解;
                    this.Visible_buf = this.Visible;
                }
            }
        }
        public bool GetValue()
        {
            bool flag = false;
            this.GetValue(ref flag);
            return flag;
        }
        public bool GetValue(ref bool value)
        {
            if (PLC_要讀取)
            {
                value = PLC.properties.Device.Get_DeviceFast_Ex(_讀取元件位置);
            }
            else
            {
                value = Value_Buf;
            }
            return value;
        }
        public void SetValue(bool value)
        {
            if (PLC_要寫入)
            {
                PLC.properties.Device.Set_DeviceFast_Ex(_寫入元件位置, value);
            }
            else
            {
                but_press = false;
                Value_Buf = value;
            }
        }
        public virtual void Run()
        {
            if (按鍵方式 == PressEnum.Mouse_左鍵)
            {
                if (captureDelegate == null) captureDelegate = new CaptureDelegate(LabelCapture);
                Basic.ControlExtensions.InvokeOnUiThreadIfRequired(this.label1, LabelCapture);
            }
            else if (按鍵方式 == PressEnum.Key_上)
            {
                if (Keys.Key_Up) but_press = true;
                else but_press = false;
            }
            else if (按鍵方式 == PressEnum.Key_下)
            {
                if (Keys.Key_Down) but_press = true;
                else but_press = false;
            }
            else if (按鍵方式 == PressEnum.Key_左)
            {
                if (Keys.Key_Left) but_press = true;
                else but_press = false;
            }
            else if (按鍵方式 == PressEnum.Key_右)
            {
                if (Keys.Key_Right) but_press = true;
                else but_press = false;
            }
            else if (按鍵方式 == PressEnum.Key_PageUp)
            {
                if (Keys.Key_PageUp) but_press = true;
                else but_press = false;
            }
            else if (按鍵方式 == PressEnum.Key_PageDown)
            {
                if (Keys.Key_PageDown) but_press = true;
                else but_press = false;
            }
            else if (按鍵方式 == PressEnum.Key_右三角引號)
            {
                if (Keys.Key_OemPeriod) but_press = true;
                else but_press = false;
            }
            else if (按鍵方式 == PressEnum.Key_左三角引號)
            {
                if (Keys.Key_Oemcomma) but_press = true;
                else but_press = false;
            }
            else if (按鍵方式 == PressEnum.Key_Esc)
            {
                if (Keys.Key_Esc) but_press = true;
                else but_press = false;
            }
            else if (按鍵方式 == PressEnum.Key_Enter)
            {
                if (Keys.Key_Enter) but_press = true;
                else but_press = false;
            }
            按鈕狀態計算();
        }
        public void Load_PLC_Device(PLC_Device pLC_Device)
        {
            this.讀取元件位置 = pLC_Device.GetAdress();
            this.寫入元件位置 = pLC_Device.GetAdress();
        }


        #endregion
        private void Init()
        {
            if (!flag_init)
            {
                if (PLC != null)
                {
                    if ((_讀取元件位置 == "" || _讀取元件位置 == null) && _寫入元件位置 != "")
                    {
                        _讀取元件位置 = _寫入元件位置;
                    }
                }
                flag_init = true;
            }
        }
        private void 按鈕狀態計算()
        {
            Init();
            if (PLC_要讀取)
            {
                if (LadderProperty.DEVICE.TestDevice(_讀取元件位置))
                {
                    PLC.properties.Device.Get_Device(_讀取元件位置, out value);
                    if (value is bool)
                    {
                        _FLAG_讀取_buf = (bool)value;
                        Value_Buf = _FLAG_讀取_buf;
                    }
                }
            }
            _FLAG_讀取_buf = Value_Buf;

            this.EnableCheck();
            this.VisibleCheck();
            if (but_press || IN)
            {
                if (顯示)
                {
                    DialogResult Result = MessageBox.Show(內容, 標題, MessageBoxButtons.YesNo, Icon, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (Result == DialogResult.Yes)
                    {
                        if (_旗標位置 != null || _旗標位置 != "") PLC.properties.Device.Set_Device(_旗標位置, true);
                    }
                }
            }
            if (按鈕型態 == StatusEnum.保持型)
            {
                if (but_press || IN) M0 = true;
                else M0 = false;

                if (M0) OUT = true;
                else OUT = false;
            }
            else if (按鈕型態 == StatusEnum.交替型)
            {
                if (PLC_要寫入)
                {
                    PLC.properties.Device.Get_Device(_寫入元件位置, out value);
                    if (value is bool)
                    {
                        Value_Buf = (bool)value;
                    }
                }
                if (but_press || IN) M0 = true;
                else M0 = false;

                if ((!Value_Buf || M1) && M0 && !M2) M1 = true;
                else M1 = false;
                if ((Value_Buf || M2) && M0 && !M1) M2 = true;
                else M2 = false;

                if ((M1 || M2)&& !M4) M3 = true;
                else M3 = false;
                if ((M1 || M2)) M4 = true;
                else M4 = false;
                if (M3 && M1) OUT = true;
                if (M3 && M2) OUT = false;
            }
            FLAG_寫入 = OUT;
            IN = false;
            if (讀寫鎖住 && !PLC_要寫入)
            {
                _FLAG_讀取_buf = FLAG_寫入;
            }
            if (_FLAG_讀取_buf != FLAG_讀取 || !IsRunOnce)
            {
                FLAG_讀取 = _FLAG_讀取_buf;
            }

            if ((but_press != but_press_buf))
            {
                if (PLC_要寫入)
                {
                    if (LadderProperty.DEVICE.TestDevice(_寫入元件位置))
                    {
                        if(_寫入元件位置.Substring(0, 1) == "X")
                        {
                            PLC.properties.device_system.Set_Device(_寫入元件位置, FLAG_寫入);
                        }
                        else
                        {
                            PLC.properties.Device.Set_Device(_寫入元件位置, FLAG_寫入);
                        }
                       
                    }
                }
                Value_Buf = FLAG_寫入;
                but_press_buf = but_press;
            }
 

            IsRunOnce = true;
        }

        #region Event
        private bool flag_btnClick_done = false;
        public event EventHandler btnClick;
        protected void OnbtnClick(EventArgs e)
        {
            if (btnClick != null) btnClick(this, e);
        }
        private void label1_Click(object sender, EventArgs e)
        {
            OnbtnClick(e);
        }
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (按鍵方式 == PressEnum.Mouse_左鍵)
            {
                if(e.Button == MouseButtons.Left)
                {
                    if (音效)
                    {
                        this.BeginInvoke(new Action(delegate
                        {
                            System.Media.SoundPlayer sp = null;
                            try
                            {
                                sp = new System.Media.SoundPlayer();
                                sp.Stop();
                                sp.Stream = Resource1.BEEP;
                                sp.Play();
                            }
                            finally
                            {
                                if (sp != null) sp.Dispose();
                            }
                        }));

                    }
                    if (!_事件驅動) but_press = true;
                    IsRunOnce = false;
                }
              
            }
        }
        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            if (按鍵方式 == PressEnum.Mouse_左鍵)
            {
                if (IsRunOnce) if (!_事件驅動) but_press = false;
            }
        }
        private void label1_MouseLeave(object sender, EventArgs e)
        {
            if (按鍵方式 == PressEnum.Mouse_左鍵)
            {
                if (!_事件驅動) but_press = false;
            }
        }
        #endregion
    }
}
