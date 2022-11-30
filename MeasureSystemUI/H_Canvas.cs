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
using MyUI;
using Basic;
using AxAxOvkBase;
namespace MeasureSystemUI
{
    public partial class H_Canvas : UserControl
    {
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
        private MyThread MyThread_Loop;
        private AxAxCanvas AxCanvas;
        public AxOvkBase.AxImageBW8 AxImageBW8;
        public AxOvkBase.AxImageC24 AxImageC24;
        private AxOvkImage.AxImageCopier AxImageCopier;
        private bool flag_Init = false;
        private bool flag_Canvas_Init = false;
        private Form Activeform;
        private LowerMachine PLC;
        private Size Size_InitPanel;
        private float ZoomX = 0;
        private float ZoomY = 0;
        private int CanvasHandle;
        #region Event
        private int InUsedEventNum = 0;
        private int EventNum = 0;
        public void AddMouseEvent(OnCanvasMouseDownEventHandler MouseDownEvent, OnCanvasMouseMoveEventHandler MouseMoveEvent, OnCanvasMouseUpEventHandler MouseUpEvent, ref int EventNum, ref int CanvasHandle)
        {
            this.OnCanvasMouseDownEvent += MouseDownEvent;
            this.OnCanvasMouseMoveEvent += MouseMoveEvent;
            this.OnCanvasMouseUpEvent += MouseUpEvent;
            this.EventNum++;
            EventNum = this.EventNum;
            int CanvasHandle_buf = CanvasHandle;
            this.Invoke(new Action(delegate { CanvasHandle_buf = (int)this.Handle; }));
            CanvasHandle = CanvasHandle_buf;
        }
        public delegate void OnCanvasMouseDownEventHandler(int x, int y, float ZoomX, float ZoomY, ref int InUsedEventNum, int InUsedCanvasHandle);
        public event OnCanvasMouseDownEventHandler OnCanvasMouseDownEvent;
        public delegate void OnCanvasMouseMoveEventHandler(int x, int y, float ZoomX, float ZoomY);
        public event OnCanvasMouseMoveEventHandler OnCanvasMouseMoveEvent;
        public delegate void OnCanvasMouseUpEventHandler(int x, int y, float ZoomX, float ZoomY);
        public event OnCanvasMouseUpEventHandler OnCanvasMouseUpEvent;

        public delegate void OnCanvasDrawEventHandler(long HDC, float ZoomX, float ZoomY ,int CanvasHandle);
        public event OnCanvasDrawEventHandler OnCanvasDrawEvent;
        #endregion
        #region 元件參數
        [Browsable(false)]
        public long SrcImageHandle
        {
            set
            {
                if (PLC_10_SrcImageHandle != null) PLC_10_SrcImageHandle.DoubleValue = value;
            }
            get
            {
                if (PLC_10_SrcImageHandle != null) return PLC_10_SrcImageHandle.DoubleValue;
                return -1;
                  
            }
        }
        [Browsable(false)]
        public long VegaHandle
        {
            get
            {
                if (ImageType == ImageTypeEnum.BW8)
                {
                    if (AxImageBW8 != null)
                    return AxImageBW8.VegaHandle;
                }
                else if (ImageType == ImageTypeEnum.C24)
                {
                    if (AxImageC24 != null)
                    return AxImageC24.VegaHandle;
                }
                return 0;
                
            }
        }
        [Browsable(false)]
        public int ImageWidth
        {
            get
            {
                if (ImageType == ImageTypeEnum.BW8)
                {
                    if (AxImageBW8 != null)
                    return AxImageBW8.ImageWidth;
                }
                else if (ImageType == ImageTypeEnum.C24)
                {
                    if (AxImageC24 != null)
                    return AxImageC24.ImageWidth;
                }
                return 0;
            }
            set
            {
                if (ImageType == ImageTypeEnum.BW8)
                {
                    if (AxImageBW8 != null)
                    AxImageBW8.ImageWidth = value;
                }
                else if (ImageType == ImageTypeEnum.C24)
                {
                    if (AxImageC24 != null)
                    AxImageC24.ImageWidth = value;
                }
            }
        }
        [Browsable(false)]
        public int ImageHeight
        {
            get
            {
                if (ImageType == ImageTypeEnum.BW8)
                {
                    if (AxImageBW8 != null)
                    return AxImageBW8.ImageHeight;
                }
                else if (ImageType == ImageTypeEnum.C24)
                {
                    if (AxImageC24 != null)
                    return AxImageC24.ImageHeight;
                }
                return 0;
            }
            set
            {
                if (ImageType == ImageTypeEnum.BW8)
                {
                    if (AxImageBW8 != null)
                    AxImageBW8.ImageWidth = value;
                }
                else if (ImageType == ImageTypeEnum.C24)
                {
                    if (AxImageC24 != null)
                    AxImageC24.ImageWidth = value;
                }
            }
        }
        [Browsable(false)]
        public int CanvasWidth
        {
            get
            {
                if (this.AxCanvas != null) return this.AxCanvas.CanvasWidth;
                return 0;
            }
            set
            {
                if (this.AxCanvas != null) this.AxCanvas.CanvasWidth = value;
            }
        }
        [Browsable(false)]
        public int CanvasHeight
        {
            get
            {
                if (this.AxCanvas != null) return this.AxCanvas.CanvasHeight;
                return 0;
            }
            set
            {
                if (this.AxCanvas != null) this.AxCanvas.CanvasHeight = value;
            }
        }
        #endregion
        #region 參數設定
        public enum ImageTypeEnum : int
        {
            BW8, C24
        }
        private ImageTypeEnum _ImageTypeEnum = ImageTypeEnum.BW8;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public ImageTypeEnum ImageType
        { 
            get
            {
                return _ImageTypeEnum;
            }
            set
            {
                _ImageTypeEnum = value;
            }
        }

        private string _CanvasName = "CanvasName";
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public string CanvasName
        {
            get
            {
                return _CanvasName;
            }
            set
            {
                _CanvasName = value;
            }
        }

        private int _CycleTime = 1;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public int CycleTime
        {
            get
            {
                return _CycleTime;
            }
            set
            {
                _CycleTime = value;
            }
        }
        private bool _更新畫布按鈕要顯示 = true;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool 更新畫布按鈕要顯示
        {
            get
            {
                return _更新畫布按鈕要顯示;
            }
            set
            {
                plC_Button_更新畫布.Visible = value;
                _更新畫布按鈕要顯示 = value;
            }
        }
        private bool _清除畫布按鈕要顯示 = true;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool 清除畫布按鈕要顯示
        {
            get
            {
                return _清除畫布按鈕要顯示;
            }
            set
            {
                plC_Button_清除畫布.Visible = value;
                _清除畫布按鈕要顯示 = value;
            }
        }
        private bool _繪製影像按鈕要顯示 = true;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool 繪製影像按鈕要顯示
        {
            get
            {
                return _繪製影像按鈕要顯示;
            }
            set
            {
                plC_Button_繪製影像.Visible = value;
                _繪製影像按鈕要顯示 = value;
            }
        }
        private bool _儲存圖片按鈕要顯示 = true;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool 儲存圖片按鈕要顯示
        {
            get
            {
                return _儲存圖片按鈕要顯示;
            }
            set
            {
                plC_Button_儲存圖片.Visible = value;
                _儲存圖片按鈕要顯示 = value;
            }
        }
        private bool _讀取圖片按鈕要顯示 = true;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool 讀取圖片按鈕要顯示
        {
            get
            {
                return _讀取圖片按鈕要顯示;
            }
            set
            {
                plC_Button_讀取圖片.Visible = value;
                _讀取圖片按鈕要顯示 = value;
            }
        }
        private bool _複製影像按鈕要顯示 = true;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool 複製影像按鈕要顯示
        {
            get
            {
                return _複製影像按鈕要顯示;
            }
            set
            {
                plC_Button_複製影像.Visible = value;
                _複製影像按鈕要顯示 = value;
            }
        }
        private bool _控制面板要顯示 = true;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool 控制面板要顯示
        {
            get
            {
                return _控制面板要顯示;
            }
            set
            {
                panel_Control.Visible = value;
                _控制面板要顯示 = value;
            }
        }
        private bool _獨立執行緒 = true;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool 獨立執行緒
        {
            get
            {
                return _獨立執行緒;
            }
            set
            {
                _獨立執行緒 = value;
            }
        }
        #endregion
        #region 數值位置
        private string __00_HDC = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _00_HDC
        {
            get { return __00_HDC; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __00_HDC = value;
                else __00_HDC = "";
            }
        }
 
        private string __01_ZoomX = "K100";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _01_ZoomX
        {
            get { return __01_ZoomX; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "K") divice_OK = true;
                }

                if (divice_OK) __01_ZoomX = value;
                else __01_ZoomX = "";
            }
        }

        private string __02_ZoomY = "K100";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _02_ZoomY
        {
            get { return __02_ZoomY; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "K") divice_OK = true;
                }

                if (divice_OK) __02_ZoomY = value;
                else __02_ZoomY = "";
            }
        }

        private string __10_SrcImageHandle = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _10_SrcImageHandle
        {
            get { return __10_SrcImageHandle; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "K") divice_OK = true;
                }

                if (divice_OK) __10_SrcImageHandle = value;
                else __10_SrcImageHandle = "";
            }
        }
        private string __11_VegaHandle = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _11_VegaHandle
        {
            get { return __11_VegaHandle; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "K") divice_OK = true;
                }

                if (divice_OK) __11_VegaHandle = value;
                else __11_VegaHandle = "";
            }
        }
        #endregion
        #region 旗標位置
        private string __00_更新畫布 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _00_更新畫布
        {
            get { return __00_更新畫布; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __00_更新畫布 = value;
                else __00_更新畫布 = "";
            }
        }
        private string __01_清除畫布 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _01_清除畫布
        {
            get { return __01_清除畫布; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __01_清除畫布 = value;
                else __01_清除畫布 = "";
            }
        }
        private string __02_繪製影像 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _02_繪製影像
        {
            get { return __02_繪製影像; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __02_繪製影像 = value;
                else __02_繪製影像 = "";
            }
        }
        private string __03_複製影像 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _03_複製影像
        {
            get { return __03_複製影像; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __03_複製影像 = value;
                else __03_複製影像 = "";
            }
        }
        private string __04_鎖定長寬比 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _04_鎖定長寬比
        {
            get { return __04_鎖定長寬比; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __04_鎖定長寬比 = value;
                else __04_鎖定長寬比 = "";
            }
        }
        #endregion



        public H_Canvas()
        {
            InitializeComponent();
            //this.FindForm().Load += new System.EventHandler(this.Form_Load);
        }

      
        private PLC_Device _PLC_00_HDC;
        public PLC_Device PLC_00_HDC
        {
            get
            {
                _PLC_00_HDC.DoubleValue = AxCanvas.hDC;
                return _PLC_00_HDC;
            }
        }
        private PLC_Device PLC_01_ZoomX;
        private PLC_Device PLC_02_ZoomY;
        private PLC_Device PLC_10_SrcImageHandle;
        private PLC_Device PLC_11_VegaHandle;


        private PLC_Device PLC_00_更新畫布;
        private PLC_Device PLC_01_清除畫布;
        private PLC_Device PLC_02_繪製影像;
        private PLC_Device PLC_03_複製影像;
        private PLC_Device PLC_04_鎖定長寬比;
        public void Run(Form Activeform, LowerMachine pLC)
        {
            this.Activeform = Activeform;
            this.PLC = pLC;
          
            if (this._00_HDC != string.Empty)
            {
                this._PLC_00_HDC = new PLC_Device(this._00_HDC);
                this._PLC_00_HDC.SetComment(string.Format("{0} : HDC", this.CanvasName));
                
                // this.PLC_00_HDC.DoubleValue = this.AxCanvas.hDC;
            }
            if (this._01_ZoomX != string.Empty)
            {
                this.PLC_01_ZoomX = new PLC_Device(this._01_ZoomX);
                plC_NumBox_ZoomX.寫入元件位置 = this._01_ZoomX;
                plC_NumBox_ZoomX.讀取元件位置 = this._01_ZoomX;
                plC_NumBox_ZoomX.Run(this.PLC);
                this.PLC_01_ZoomX.SetComment(string.Format("{0} : ZoomX", this.CanvasName));
                if (PLC_01_ZoomX.Value <= 0) PLC_01_ZoomX.Value = 100;
            }
            if (this._02_ZoomY != string.Empty)
            {
                this.PLC_02_ZoomY = new PLC_Device(this._02_ZoomY);
                plC_NumBox_ZoomY.寫入元件位置 = this._02_ZoomY;
                plC_NumBox_ZoomY.讀取元件位置 = this._02_ZoomY;
                plC_NumBox_ZoomY.Run(this.PLC);
                this.PLC_02_ZoomY.SetComment(string.Format("{0} : ZoomY", this.CanvasName));
                if (PLC_02_ZoomY.Value <= 0) PLC_02_ZoomY.Value = 100;
            }
            if (this._10_SrcImageHandle != string.Empty)
            {
                this.PLC_10_SrcImageHandle = new PLC_Device(this._10_SrcImageHandle);
                this.PLC_10_SrcImageHandle.SetComment(string.Format("{0} : SrcImageHandle", this.CanvasName));
            }
            if (this._11_VegaHandle != string.Empty)
            {
                this.PLC_11_VegaHandle = new PLC_Device(this._11_VegaHandle);
                this.PLC_11_VegaHandle.SetComment(string.Format("{0} : VegaHandle", this.CanvasName));
            }

            if (this._00_更新畫布 != string.Empty)
            {
                plC_Button_更新畫布.寫入元件位置 = this._00_更新畫布;
                plC_Button_更新畫布.讀取元件位置 = this._00_更新畫布;
                plC_Button_更新畫布.Run(this.PLC);
                this.PLC_00_更新畫布 = new PLC_Device(this._00_更新畫布);
                this.PLC_00_更新畫布.Bool = false;

                this.PLC_00_更新畫布.SetComment(string.Format("{0} : 更新畫布", this.CanvasName));
            }
            if (this._01_清除畫布 != string.Empty)
            {
                plC_Button_清除畫布.寫入元件位置 = this._01_清除畫布;
                plC_Button_清除畫布.讀取元件位置 = this._01_清除畫布;
                plC_Button_清除畫布.Run(this.PLC);
                this.PLC_01_清除畫布 = new PLC_Device(this._01_清除畫布);
                this.PLC_01_清除畫布.Bool = false;

                this.PLC_01_清除畫布.SetComment(string.Format("{0} : 清除畫布", this.CanvasName));
            }
            if (this._02_繪製影像 != string.Empty)
            {
                plC_Button_繪製影像.寫入元件位置 = this._02_繪製影像;
                plC_Button_繪製影像.讀取元件位置 = this._02_繪製影像;
                plC_Button_繪製影像.Run(this.PLC);
                this.PLC_02_繪製影像 = new PLC_Device(this._02_繪製影像);
                this.PLC_02_繪製影像.Bool = false;

                this.PLC_02_繪製影像.SetComment(string.Format("{0} : 繪製影像", this.CanvasName));
            }
            if (this._03_複製影像 != string.Empty)
            {
                plC_Button_複製影像.寫入元件位置 = this._03_複製影像;
                plC_Button_複製影像.讀取元件位置 = this._03_複製影像;
                plC_Button_複製影像.Run(this.PLC);
                this.PLC_03_複製影像 = new PLC_Device(this._03_複製影像);
                this.PLC_03_複製影像.Bool = false;

                this.PLC_03_複製影像.SetComment(string.Format("{0} : 複製影像", this.CanvasName));
            }
            if (this._04_鎖定長寬比 != string.Empty)
            {
                plC_CheckBox_鎖定長寬比.寫入元件位置 = this._04_鎖定長寬比;
                plC_CheckBox_鎖定長寬比.讀取元件位置 = this._04_鎖定長寬比;
                plC_CheckBox_鎖定長寬比.Run(this.PLC);
                this.PLC_04_鎖定長寬比 = new PLC_Device(this._04_鎖定長寬比);

                this.PLC_04_鎖定長寬比.SetComment(string.Format("{0} : 鎖定長寬比", this.CanvasName));

            }

            this.Init();

            if (this.獨立執行緒)
            {
                this.MyThread_Loop = new MyThread(Activeform);
                this.MyThread_Loop.AutoRun(true);
                this.MyThread_Loop.Add_Method(this.Run);
                this.MyThread_Loop.SetSleepTime(this.CycleTime);
                this.MyThread_Loop.Trigger();
            }

        }
        public Basic.MyThread.MethodDelegate Get_Method()
        {
            return this.Run;
        }
        public void DrawString(string text, Font font, AxOvkBase.TxAxColor fontcolor, AxOvkBase.TxAxColor forecolor, PointF po, double zoomX, double zoomY)
        {
            this.AxCanvas.BrushColor = forecolor;
            this.AxCanvas.FontColor = fontcolor;
            this.AxCanvas.DrawText(text, (int)po.X, (int)po.Y, (float)zoomX, (float)zoomY, 0, 0);
        }
        public void Canvas_Init()
        {
            if (!flag_Canvas_Init)
            {
              

                try
                {
                    if (this.AxCanvas == null) this.AxCanvas = new AxAxCanvas();
                    ((System.ComponentModel.ISupportInitialize)(this.AxCanvas)).BeginInit();
                    panel_Canvas.Controls.Add(this.AxCanvas);
                    ((System.ComponentModel.ISupportInitialize)(this.AxCanvas)).EndInit();


                    this.AxCanvas.Dock = DockStyle.Fill;
                    this.AxCanvas.Location = new System.Drawing.Point(0, 0);
                    this.Size_InitPanel = new Size(this.panel_Canvas.Width, this.panel_Canvas.Height);
                    this.AxCanvas.OnCanvasMouseDown += AxCanvas_OnCanvasMouseDown;
                    this.AxCanvas.OnCanvasMouseMove += AxCanvas_OnCanvasMouseMove;
                    this.AxCanvas.OnCanvasMouseUp += AxCanvas_OnCanvasMouseUp;

                    this.AxCanvas.CanvasWidth = this.panel_Canvas.Width;
                    this.AxCanvas.CanvasHeight = this.panel_Canvas.Height;
                    this.AxCanvas.RefreshCanvas();
                }
                catch
                {

                }
                flag_Canvas_Init = true;

            }
        }
        private void Init()
        {    
            if (this.PLC != null && this.Activeform != null && !flag_Init)
            {
                if (this.AxImageBW8 == null) this.AxImageBW8 = new AxOvkBase.AxImageBW8();
                if (this.AxImageC24 == null) this.AxImageC24 = new AxOvkBase.AxImageC24();
                if (this.AxImageCopier == null) this.AxImageCopier = new AxOvkImage.AxImageCopier();


                this.PLC_11_VegaHandle.DoubleValue = this.VegaHandle;
                this.Invoke(new Action(delegate { this.CanvasHandle = (int)this.Handle; }));
                flag_Init = true;
            }       
        }
        private void Run()
        {
      
            if (flag_Init)
            {
                if (this.PLC_04_鎖定長寬比.Bool)
                {
                    if (this.PLC_01_ZoomX.Value != this.PLC_02_ZoomY.Value) this.PLC_02_ZoomY.Value = this.PLC_01_ZoomX.Value;
                }
                if (this.PLC_03_複製影像.Bool)
                {
                    this.ImageCopy();
                    this.PLC_02_繪製影像.Bool = true;
                    this.PLC_00_更新畫布.Bool = true;
                    this.PLC_03_複製影像.Bool = false;
                }
                if (this.PLC_01_清除畫布.Bool)
                {
                    this.AxCanvas.ClearCanvas();
                    this.PLC_00_更新畫布.Bool = true;
                    this.PLC_01_清除畫布.Bool = false;
                }
                if (this.PLC_02_繪製影像.Bool)
                {       
                    if (this.ImageWidth != 0 && this.ImageHeight != 0)
                    {
                        //this.AxCanvas.ClearCanvas();
                        this.GetImageScale(ref ZoomX, ref ZoomY);
                        this.AxCanvas.DrawSurface(this.VegaHandle, ZoomX, ZoomY, 0, 0);
                        if (this.OnCanvasDrawEvent != null) this.OnCanvasDrawEvent(this.AxCanvas.hDC, ZoomX, ZoomY, this.CanvasHandle);

                    }               
                    this.PLC_00_更新畫布.Bool = true;
                }
                if (this.PLC_00_更新畫布.Bool)
                {
                    this.AxCanvas.RefreshCanvas();
                    this.PLC_00_更新畫布.Bool = false;
                    this.PLC_02_繪製影像.Bool = false;
                }
            }
            
        }

        #region Function
        public bool IsBusy()
        {
            return this.PLC_00_更新畫布.Bool;
        }

        public long GetHDC()
        {   
            return PLC_00_HDC.DoubleValue;
        }
        public void SetCanvasSize(Size Size)
        {
            this.AxCanvas.CanvasWidth = Size.Width;
            this.AxCanvas.CanvasHeight = Size.Height;
        }
        public bool LoadImage(string filename)
        {
            if (ImageType == ImageTypeEnum.BW8)
            {
                if (AxImageBW8.LoadFile(filename))
                {
                    this.PLC_01_清除畫布.Bool = true;
                    this.PLC_02_繪製影像.Bool = true;
                    return true;
                }
                return false;
            }
            else if (ImageType == ImageTypeEnum.C24)
            {
                if (AxImageC24.LoadFile(filename))
                {
                    this.PLC_01_清除畫布.Bool = true;
                    this.PLC_02_繪製影像.Bool = true;
                    return true;
                }
                return false;
            }
            return false;
        }
        public bool SaveImage(string filename)
        {
            if (ImageType == ImageTypeEnum.BW8)
            {
                return AxImageBW8.SaveFile(filename, AxOvkBase.TxAxImageType.AX_IMAGE_FILE_TYPE_GREYLEVEL_BMP);
            }
            else if (ImageType == ImageTypeEnum.C24)
            {
                return AxImageC24.SaveFile(filename, AxOvkBase.TxAxImageType.AX_IMAGE_FILE_TYPE_FULLCOLOR_BMP);
            }
            return false;
        }
        public void GetImageScale(ref float Scale_X, ref float Scale_Y)
        {         
            double T_Width, T_Height;
            T_Width = this.Size_InitPanel.Width * (double)this.PLC_01_ZoomX.Value / 100D;
            T_Height = this.Size_InitPanel.Height * (double)this.PLC_02_ZoomY.Value / 100D;
            this.AxCanvas.CanvasWidth = (int)T_Width;
            this.AxCanvas.CanvasHeight = (int)T_Height;
            Scale_X = (float)(T_Width / this.ImageWidth);
            Scale_Y = (float)(T_Height / this.ImageHeight);
        }
        public void SetImageSize(int Width, int Height)
        {
            if (ImageType == ImageTypeEnum.BW8)
            {
                if (AxImageBW8 != null)
                    AxImageBW8.SetSize(Width, Height);
            }
            else if (ImageType == ImageTypeEnum.C24)
            {
                if (AxImageC24 != null)
                    AxImageC24.SetSize(Width, Height);
            }

           // this.ImageWidth = Width;
           // this.ImageHeight = Height;
        }
        public void ImageCopy(long SrcImageHandle)
        {
            this.SrcImageHandle = SrcImageHandle;
            this.AxImageCopier.SrcImageHandle = SrcImageHandle;
            this.AxImageCopier.DstImageHandle = this.VegaHandle;
            this.AxImageCopier.Copy();
        }
        public void ImageCopy()
        {
            this.AxImageCopier.SrcImageHandle = this.PLC_10_SrcImageHandle.DoubleValue;
            this.AxImageCopier.DstImageHandle = this.VegaHandle;
            this.AxImageCopier.Copy();
        }
        public void SetImagePtr(int Width, int Height, long Ptr)
        {
            if (ImageType == ImageTypeEnum.BW8)
            {
                if (AxImageBW8 != null)
                {
                    AxImageBW8.SetSurfacePtr(Width, Height, Ptr);
                }
            }
            else if (ImageType == ImageTypeEnum.C24)
            {
                if (AxImageC24 != null)
                {
                    AxImageC24.SetSurfacePtr(Width, Height, Ptr);
                }
            }
        }
        public void DrawSurface(long SurfaceHandle, float ZoomX, float ZoomY)
        {
            this.AxCanvas.DrawSurface(SurfaceHandle, ZoomX, ZoomY, 0, 0);
        }
        public void RefreshCanvas()
        {
            //this.PLC_02_繪製影像.Bool = true;
            //this.PLC_00_更新畫布.Bool = true;
            this.GetImageScale(ref ZoomX, ref ZoomY);
            this.AxCanvas.DrawSurface(this.VegaHandle, ZoomX, ZoomY, 0, 0);
            if (this.OnCanvasDrawEvent != null) this.OnCanvasDrawEvent(this.AxCanvas.hDC, ZoomX, ZoomY, this.CanvasHandle);
            this.AxCanvas.RefreshCanvas();
        }
        public void ClearCanvas()
        {
            this.PLC_01_清除畫布.Bool = true;
        }
        public void Add_Method(MyThread.MethodDelegate method)
        {
            this.MyThread_Loop.Add_Method(method);
        }


        public static void FindCanvas( Form form, ref List<H_Canvas> List_Canvas)
        {
            foreach (Control ctl in form.Controls)
            {
                FindSubControl(ctl, "GetAll", ref List_Canvas);
            }
        }
        public static void FindCanvas(string CanvasName , Form form ,ref List<H_Canvas> List_Canvas)
        {
            foreach (Control ctl in form.Controls)
            {
                FindSubControl(ctl, CanvasName,ref List_Canvas);
            }
        }
        private static void FindSubControl(Control ctl, string CanvasName, ref List<H_Canvas> List_Canvas)
        {
            if (ctl is PLC_ScreenPage)
            {
                PLC_ScreenPage ctl_temp = (PLC_ScreenPage)ctl;
                foreach (Control temp in ctl_temp.Controls)
                {
                    FindSubControl(temp, CanvasName, ref  List_Canvas);
                }
            }
            else if (ctl.Controls.Count > 0)
            {
                foreach (Control sub_ctl in ctl.Controls)
                {
                    FindSubControl(sub_ctl, CanvasName, ref  List_Canvas);
                }
            }

            if (ctl is H_Canvas)
            {
                H_Canvas ctl_temp = (H_Canvas)ctl;
                if (CanvasName == ctl_temp.CanvasName)
                {
                    List_Canvas.Add((H_Canvas)ctl);
                }
                else if(CanvasName == "GetAll")
                {
                    List_Canvas.Add((H_Canvas)ctl);
                }
            }

        }
        #endregion
        private void H_Canvas_Load(object sender, EventArgs e)
        {

  
        }

        int AxCanvas_MousePoX;
        int AxCanvas_MousePoY;
        void AxCanvas_OnCanvasMouseDown(object sender, IAxCanvasEvents_OnCanvasMouseDownEvent e)
        {
            this.InUsedEventNum = 0;
            if (this.OnCanvasMouseDownEvent != null) this.OnCanvasMouseDownEvent(e.x, e.y, ZoomX, ZoomY, ref InUsedEventNum, this.CanvasHandle);
            if(InUsedEventNum == 0)
            {
                if(AxCanvas.HorzScrollMax > 0 || AxCanvas.VertScrollMax > 0)
                {
                    InUsedEventNum = -1;
                    Cursor = Cursors.NoMove2D;
                    AxCanvas_MousePoX = e.x;
                    AxCanvas_MousePoY = e.y;
                }
            }
        }
        void AxCanvas_OnCanvasMouseMove(object sender, IAxCanvasEvents_OnCanvasMouseMoveEvent e)
        {
            if (this.OnCanvasMouseMoveEvent != null)
            {
                this.OnCanvasMouseMoveEvent(e.x, e.y, ZoomX, ZoomY);
            }
            if (InUsedEventNum == -1)
            {
                AxCanvas.HorzScrollValue += (AxCanvas_MousePoX - e.x);
                AxCanvas.VertScrollValue += (AxCanvas_MousePoY - e.y);
            }
        }
        void AxCanvas_OnCanvasMouseUp(object sender, IAxCanvasEvents_OnCanvasMouseUpEvent e)
        {
            if (this.OnCanvasMouseUpEvent != null) this.OnCanvasMouseUpEvent(e.x, e.y, ZoomX, ZoomY);
            Cursor = Cursors.Default;
            this.InUsedEventNum = 0;
        }

        private void plC_Button_儲存圖片_btnClick(object sender, EventArgs e)
        {
            if (saveFileDialog_Image.ShowDialog(this) == DialogResult.OK)
            {
                this.SaveImage(saveFileDialog_Image.FileName);
            }
        }
        private void plC_Button_讀取圖片_btnClick(object sender, EventArgs e)
        {
            if (openFileDialog_Image.ShowDialog(this) == DialogResult.OK)
            {
                this.LoadImage(openFileDialog_Image.FileName);
            }
        }

        private void timer_init_Tick(object sender, EventArgs e)
        {
            if (this.IsHandleCreated && this.PLC != null && this.Activeform != null && !flag_Init)
            {
                timer_init.Enabled = false;
            }
        }
        private void Form_Load(object sender, EventArgs e)
        {
 
        }
    }
}
