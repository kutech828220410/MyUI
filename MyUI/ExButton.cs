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
namespace MyUI
{
    [System.Drawing.ToolboxBitmap(typeof(Button))]
    public partial class ExButton : UserControl
    {
        private LowerMachine PLC;
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
        private bool _FLAG_讀取_buf = false;
        private bool FLAG_讀取
        {
            get
            {
                           if (_FLAG_讀取) sub_按鈕狀態設為ON();
                if (!_FLAG_讀取) sub_按鈕狀態設為OFF();
                return _FLAG_讀取;
            }
            set
            {         
                _FLAG_讀取 = value;
                if (_FLAG_讀取) sub_按鈕狀態設為ON();
                if (!_FLAG_讀取) sub_按鈕狀態設為OFF();
            }
        }
        public bool FLAG_buf = false;

        public ExButton()
        {
            InitializeComponent();     
        }

       #region 自訂屬性
        public enum StyleEnum : int
        {
            經典 = 0,自定義
        }
        StyleEnum _Style = new StyleEnum();
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public StyleEnum Style 
        {
            get
            {
                if (_Style == StyleEnum.經典)
                {
                    image_OFF = Resource1.經典_OFF;
                    image_ON = Resource1.經典_ON;
                }
                return _Style;
            }
            set
            {
                if(value == StyleEnum.經典)
                {
                    image_OFF = Resource1.經典_OFF;
                    image_ON = Resource1.經典_ON;
                }
                顯示狀態 = false;
                _Style = value;
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
        private bool _音效 = false;
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
                if (文字鎖住) _ON文字內容 = OFF_文字內容;
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
                if (字型鎖住) _ON_文字字體 = _OFF_文字字體;
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
        private bool _文字鎖住 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue(1)]
        public bool 文字鎖住
        {
            get { return _文字鎖住; }
            set
            {
                if(文字鎖住)ON_文字內容 = OFF_文字內容;
                _文字鎖住 = value;
            }
        }
        private bool _字型鎖住 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue(1)]
        public bool 字型鎖住
        {
            get { return _字型鎖住; }
            set
            {
               if(文字鎖住) ON_文字字體 = OFF_文字字體;
                _字型鎖住 = value;
            }
        }     
        private bool _讀寫鎖住 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue(1)]
        public bool 讀寫鎖住
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
        public StatusEnum 按鈕型態 { get; set; }
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
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
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
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) _讀取元件位置 = value;
                else _讀取元件位置 = "";
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
        #endregion

        public event EventHandler btnClick;
       protected void OnbtnClick(EventArgs e)
       {
           if (btnClick != null)
               btnClick(this, e);
       }
       private void label1_Click(object sender, EventArgs e)
       {

           OnbtnClick(e);
       }
       private void label1_MouseDown(object sender, MouseEventArgs e)
       {
           if (音效)
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
           }
           but_press = true;
           IsRunOnce = false;
       }
       private void label1_MouseUp(object sender, MouseEventArgs e)
       {
           if (IsRunOnce) but_press = false;
       }
       private void label1_MouseLeave(object sender, EventArgs e)
       {
           but_press = false;
       }
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
           Push_Once();
           _FLAG_讀取_buf = FLAG;
           按鈕狀態計算();
           按鈕狀態計算();
        //   if (按鈕型態 == StatusEnum.交替型) IN = true;
          
       }
       public bool LoadState()
       {
           return FLAG_讀取;
       }
       public void Push_Once()
       {
           IN = true;
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

       private delegate void CaptureDelegate();
       CaptureDelegate captureDelegate;
       void LabelCapture()
       {
           if (IsRunOnce) but_press = label1.Capture;
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
       public void Run()
       {         
           if(captureDelegate ==null) captureDelegate = new CaptureDelegate(LabelCapture);
           if (this.IsHandleCreated) Invoke(captureDelegate);
       
           按鈕狀態計算();
       }
       private bool but_press = false;
       private bool IN = false;
       private bool M0 = false;
       private bool M1 = false;
       private bool M2 = false;
       private bool M3 = false;
       private bool OUT = false;

       private void 按鈕狀態計算()
       {

           bool PLC_要讀取及寫入 = false;
           object value = new object();
 
           if (寫入元件位置 != "" || 讀取元件位置 != "" && PLC != null)
           {
               if ((寫入元件位置 == "" || 寫入元件位置 == null) && 讀取元件位置 != "") 寫入元件位置 = 讀取元件位置;
               if ((讀取元件位置 == "" || 讀取元件位置 == null) && 寫入元件位置 != "") 讀取元件位置 = 寫入元件位置;
               PLC_要讀取及寫入 = true;
           }
           if (PLC_要讀取及寫入)
           {
               if(LadderProperty.DEVICE.TestDevice(讀取元件位置))
               {
                   string temp = 讀取元件位置.Remove(1);
                   PLC.properties.Device.Get_Device(讀取元件位置, out value);
                   _FLAG_讀取_buf = (bool)value;
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
               if (but_press || IN) M0 = true;
               else M0 = false;

               if ((!_FLAG_讀取_buf || M1) && M0 && !M2) M1 = true;
               else M1 = false;
               if ((_FLAG_讀取_buf || M2) && M0 && !M1) M2 = true;
               else M2 = false;

               if ((M1 || M3) && !M2) M3 = true;
               else M3 = false;
               if (M3) OUT = true;
               else OUT = false;
           }
           FLAG_寫入 = OUT;
           IN = false;
           if (讀寫鎖住 && !PLC_要讀取及寫入)
           {
               _FLAG_讀取_buf = FLAG_寫入;
           }
           if (_FLAG_讀取_buf != FLAG_讀取 || !IsRunOnce)
           {
               FLAG_讀取 = _FLAG_讀取_buf;
           }

     
           if (PLC_要讀取及寫入)
           {
               if (LadderProperty.DEVICE.TestDevice(寫入元件位置))
               {
                   PLC.properties.Device.Set_Device(寫入元件位置, FLAG_寫入);
               }
           }
           IsRunOnce = true;
       }



    }
}
