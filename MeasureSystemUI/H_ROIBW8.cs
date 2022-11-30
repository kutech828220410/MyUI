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
    public partial class H_ROIBW8 : UserControl
    {
        private AxOvkBase.AxROIBW8 AxROIBW8;

        private List<int> List_EventNum = new List<int>();
        private List<int> List_EventHandle = new List<int>();
        private bool flag_Init = false;
        private bool flag_Thread_Init = false;
        private Form Activeform;
        private LowerMachine PLC;
        PLC_UI_Init PLC_UI_Init;
        private List<H_Canvas> List_H_Canvas = new List<H_Canvas>();
        private List<H_Thread> List_H_Thread = new List<H_Thread>();

        #region Event
        public delegate void OnCanvasDrawEventHandler(long HDC, float ZoomX, float ZoomY, int CanvasHandle);
        public event OnCanvasDrawEventHandler OnCanvasDrawEvent;
        #endregion

        #region 元件參數
        [Browsable(false)]
        public long VegaHandle
        {
            get
            {
                if (this.AxROIBW8 != null) return this.AxROIBW8.VegaHandle;
                return -1;
            }
        }
        [Browsable(false)]
        public long ParentHandle
        {
            get
            {
                if (this.AxROIBW8 != null) return this.AxROIBW8.ParentHandle;
                return -1;
            }
            set
            {
                if (this.AxROIBW8 != null) this.AxROIBW8.ParentHandle = value;
            }
        }
        [Browsable(false)]
        public int OrgX
        {
            get
            {
                if (this.AxROIBW8 != null) return this.AxROIBW8.OrgX;
                return -1;
            }
            set
            {
                if (this.AxROIBW8 != null) this.AxROIBW8.OrgX = value;
            }
        }
        [Browsable(false)]
        public int OrgY
        {
            get
            {
                if (this.AxROIBW8 != null) return this.AxROIBW8.OrgY;
                return -1;
            }
            set
            {
                if (this.AxROIBW8 != null) this.AxROIBW8.OrgY = value;
            }
        }
        [Browsable(false)]
        public int ROIWidth
        {
            get
            {
                if (this.AxROIBW8 != null) return this.AxROIBW8.ROIWidth;
                return -1;
            }
            set
            {
                if (this.AxROIBW8 != null) this.AxROIBW8.ROIWidth = value;
            }
        }
        [Browsable(false)]
        public int ROIHeight
        {
            get
            {
                if (this.AxROIBW8 != null) return this.AxROIBW8.ROIHeight;
                return -1;
            }
            set
            {
                if (this.AxROIBW8 != null) this.AxROIBW8.ROIHeight = value;
            }
        }
        [Browsable(false)]
        public long TopParentHandle
        {
            get
            {
                if (this.AxROIBW8 != null) return this.AxROIBW8.TopParentHandle;
                return -1;
            }
        }
        [Browsable(false)]
        public int TotalOrgX
        {
            get
            {
                if (this.AxROIBW8 != null) return this.AxROIBW8.TotalOrgX;
                return -1;
            }
        }
        [Browsable(false)]
        public int TotalOrgY
        {
            get
            {
                if (this.AxROIBW8 != null) return this.AxROIBW8.TotalOrgY;
                return -1;
            }
        }
        [Browsable(false)]
        public int TotalWidth
        {
            get
            {
                if (this.AxROIBW8 != null) return this.AxROIBW8.TotalWidth;
                return -1;
            }
        }
        [Browsable(false)]
        public int TotalHeight
        {
            get
            {
                if (this.AxROIBW8 != null) return this.AxROIBW8.TotalHeight;
                return -1;
            }
        }
        [Browsable(false)]
        public bool ShowTitle
        {
            get
            {
                if (this.AxROIBW8 != null) return this.AxROIBW8.ShowTitle;
                return false;
            }
            set
            {
                if (this.AxROIBW8 != null) this.AxROIBW8.ShowTitle = value;
            }
        }
        [Browsable(false)]
        public bool ShowPlacement
        {
            get
            {
                if (this.AxROIBW8 != null) return this.AxROIBW8.ShowPlacement;
                return false;
            }
            set
            {
                if (this.AxROIBW8 != null) this.AxROIBW8.ShowPlacement = value;
            }
        }
        [Browsable(false)]
        public bool ShowCenterCross
        {
            get
            {
                if (this.AxROIBW8 != null) return this.AxROIBW8.ShowCenterCross;
                return false;
            }
            set
            {
                if (this.AxROIBW8 != null) this.AxROIBW8.ShowCenterCross = value;
            }
        }
        [Browsable(false)]
        public string Title
        {
            get
            {
                if (this.AxROIBW8 != null) return this.AxROIBW8.Title;
                return "";
            }
            set
            {
                if (this.AxROIBW8 != null) this.AxROIBW8.Title = value;
            }
        }
        #endregion

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
        #region 參數設定
        private string _UIName = "UIName";
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public string UIName
        {
            get
            {
                return _UIName;
            }
            set
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(delegate { this.label_UIName.Text = value; }));
                }
                _UIName = value;
            }
        }

        private string _關聯CanvasName = "";
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public string 關聯CanvasName
        {
            get
            {
                return _關聯CanvasName;
            }
            set
            {

                _關聯CanvasName = value;
            }
        }
        private string _關聯ThreadName = "";
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public string 關聯ThreadName
        {
            get
            {
                return _關聯ThreadName;
            }
            set
            {

                _關聯ThreadName = value;
            }
        }
        private string _ROI標題 = "";
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public string ROI標題
        {
            get
            {
                return _ROI標題;
            }
            set
            {

                _ROI標題 = value;
            }
        }

        private bool _ROI標題要顯示 = true;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool ROI標題要顯示
        {
            get
            {
                return _ROI標題要顯示;
            }
            set
            {

                _ROI標題要顯示 = value;
            }
        }

        private bool _ROI框架資訊要顯示 = true;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool ROI框架資訊要顯示
        {
            get
            {
                return _ROI框架資訊要顯示;
            }
            set
            {

                _ROI框架資訊要顯示 = value;
            }
        }

        private bool _ROI中心位置要顯示 = true;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool ROI中心位置要顯示
        {
            get
            {
                return _ROI中心位置要顯示;
            }
            set
            {

                _ROI中心位置要顯示 = value;
            }
        }
        private Color _拖曳量測框顏色 = Color.Yellow;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public Color 拖曳量測框顏色
        {
            get
            {
                return _拖曳量測框顏色;
            }
            set
            {

                _拖曳量測框顏色 = value;
            }
        }
        #endregion
        #region 數值位置
        private PLC_Device PLC_00_VegaHandle;
        private string __00_VegaHandle = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _00_VegaHandle
        {
            get { return __00_VegaHandle; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __00_VegaHandle = value;
                else __00_VegaHandle = "";
            }
        }
        private PLC_Device PLC_01_ParentHandle;
        private string __01_ParentHandle = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _01_ParentHandle
        {
            get { return __01_ParentHandle; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __01_ParentHandle = value;
                else __01_ParentHandle = "";
            }
        }
        private PLC_Device PLC_02_OrgX;
        private string __02_OrgX = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _02_OrgX
        {
            get { return __02_OrgX; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __02_OrgX = value;
                else __02_OrgX = "";
            }
        }
        private PLC_Device PLC_03_OrgY;
        private string __03_OrgY = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _03_OrgY
        {
            get { return __03_OrgY; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __03_OrgY = value;
                else __03_OrgY = "";
            }
        }
        private PLC_Device PLC_04_ROIWidth;
        private string __04_ROIWidth = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _04_ROIWidth
        {
            get { return __04_ROIWidth; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __04_ROIWidth = value;
                else __04_ROIWidth = "";
            }
        }
        private PLC_Device PLC_05_ROIHeight;
        private string __05_ROIHeight = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _05_ROIHeight
        {
            get { return __05_ROIHeight; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __05_ROIHeight = value;
                else __05_ROIHeight = "";
            }
        }
        #endregion
        #region 旗標位置
        private PLC_Device PLC_00_繪製量測框;
        private string __00_繪製量測框 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _00_繪製量測框
        {
            get { return __00_繪製量測框; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __00_繪製量測框 = value;
                else __00_繪製量測框 = "";
            }
        }

        private PLC_Device PLC_01_繪製OK結果;
        private string __01_繪製OK結果 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _01_繪製OK結果
        {
            get { return __01_繪製OK結果; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __01_繪製OK結果 = value;
                else __01_繪製OK結果 = "";
            }
        }

        private PLC_Device PLC_02_繪製NG結果;
        private string __02_繪製NG結果 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _02_繪製NG結果
        {
            get { return __02_繪製NG結果; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __02_繪製NG結果 = value;
                else __02_繪製NG結果 = "";
            }
        }

        private PLC_Device PLC_10_初始化;
        private string __10_初始化 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _10_初始化
        {
            get { return __10_初始化; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __10_初始化 = value;
                else __10_初始化 = "";
            }
        }
        #endregion
        public H_ROIBW8()
        {
            InitializeComponent();      
        }

        public void Run(Form Activeform, LowerMachine pLC, PLC_UI_Init PLC_UI_Init)
        {
            this.Activeform = Activeform;
            this.PLC = pLC;
            this.PLC_UI_Init = PLC_UI_Init;
            #region 數值位置初始化
            if (this._00_VegaHandle != string.Empty)
            {
                this.PLC_00_VegaHandle = new PLC_Device(this._00_VegaHandle);
                plC_NumBox_VagaHandle.寫入元件位置 = this._00_VegaHandle;
                plC_NumBox_VagaHandle.讀取元件位置 = this._00_VegaHandle;
                plC_NumBox_VagaHandle.Run(this.PLC);
            }
            if (this._01_ParentHandle != string.Empty)
            {
                this.PLC_01_ParentHandle = new PLC_Device(this._01_ParentHandle);
                plC_NumBox_ParentHandle.寫入元件位置 = this._01_ParentHandle;
                plC_NumBox_ParentHandle.讀取元件位置 = this._01_ParentHandle;
                plC_NumBox_ParentHandle.Run(this.PLC);
            }
            if (this._02_OrgX != string.Empty)
            {
                this.PLC_02_OrgX = new PLC_Device(this._02_OrgX);
                if (PLC_02_OrgX.Value == 0) PLC_02_OrgX.Value = 100;
                plC_NumBox_OrgX.寫入元件位置 = this._02_OrgX;
                plC_NumBox_OrgX.讀取元件位置 = this._02_OrgX;
                plC_NumBox_OrgX.Run(this.PLC);

            }
            if (this._03_OrgY != string.Empty)
            {
                this.PLC_03_OrgY = new PLC_Device(this._03_OrgY);
                if (PLC_03_OrgY.Value == 0) PLC_03_OrgY.Value = 100;
                plC_NumBox_OrgY.寫入元件位置 = this._03_OrgY;
                plC_NumBox_OrgY.讀取元件位置 = this._03_OrgY;
                plC_NumBox_OrgY.Run(this.PLC);
            }
            if (this._04_ROIWidth != string.Empty)
            {
                this.PLC_04_ROIWidth = new PLC_Device(this._04_ROIWidth);
                if (PLC_04_ROIWidth.Value == 0) PLC_04_ROIWidth.Value = 200;
                plC_NumBox_ROIWidth.寫入元件位置 = this._04_ROIWidth;
                plC_NumBox_ROIWidth.讀取元件位置 = this._04_ROIWidth;
                plC_NumBox_ROIWidth.Run(this.PLC);
            }
            if (this._05_ROIHeight != string.Empty)
            {
                this.PLC_05_ROIHeight = new PLC_Device(this._05_ROIHeight);
                if (PLC_05_ROIHeight.Value == 0) PLC_05_ROIHeight.Value = 200;
                plC_NumBox_ROIHeight.寫入元件位置 = this._05_ROIHeight;
                plC_NumBox_ROIHeight.讀取元件位置 = this._05_ROIHeight;
                plC_NumBox_ROIHeight.Run(this.PLC);
            }
            #endregion
            #region 旗標位置初始化
            if (this._00_繪製量測框 != string.Empty)
            {
                this.PLC_00_繪製量測框 = new PLC_Device(this._00_繪製量測框);
                plC_Button_繪製量測框.寫入元件位置 = this._00_繪製量測框;
                plC_Button_繪製量測框.讀取元件位置 = this._00_繪製量測框;
                plC_Button_繪製量測框.Run(this.PLC);
                this.PLC_00_繪製量測框.Bool = false;
            }

            if (this._01_繪製OK結果 != string.Empty)
            {
                this.PLC_01_繪製OK結果 = new PLC_Device(this._01_繪製OK結果);
                plC_Button_繪製OK結果.寫入元件位置 = this._01_繪製OK結果;
                plC_Button_繪製OK結果.讀取元件位置 = this._01_繪製OK結果;
                plC_Button_繪製OK結果.Run(this.PLC);
                this.PLC_01_繪製OK結果.Bool = false;
            }

            if (this._02_繪製NG結果 != string.Empty)
            {
                this.PLC_02_繪製NG結果 = new PLC_Device(this._02_繪製NG結果);
                plC_Button_繪製NG結果.寫入元件位置 = this._02_繪製NG結果;
                plC_Button_繪製NG結果.讀取元件位置 = this._02_繪製NG結果;
                plC_Button_繪製NG結果.Run(this.PLC);
                this.PLC_02_繪製NG結果.Bool = false;
            }

            if (this._10_初始化 != string.Empty)
            {
                this.PLC_10_初始化 = new PLC_Device(this._10_初始化);
                plC_Button_初始化.寫入元件位置 = this._10_初始化;
                plC_Button_初始化.讀取元件位置 = this._10_初始化;
                plC_Button_初始化.Run(this.PLC);
                this.PLC_10_初始化.Bool = false;
            }
            #endregion

            H_Canvas.FindCanvas(this.關聯CanvasName, Activeform, ref this.List_H_Canvas);
            int EventNum = 0;
            int EventHandle = 0;
            for (int i = 0; i < this.List_H_Canvas.Count; i++)
            {
                this.List_H_Canvas[i].AddMouseEvent(this.OnCanvasMousDown, this.OnCanvasMousMove, this.OnCanvasMousUp, ref EventNum, ref EventHandle);
                this.List_EventNum.Add(EventNum);
                this.List_EventHandle.Add(EventHandle);
                this.List_H_Canvas[i].OnCanvasDrawEvent += this.OnCanvasDraw;
            }
            this.PLC_UI_Init.Add_Method(Thread_Init);
        }
        private void Thread_Init()
        {
            if (!this.flag_Thread_Init)
            {
                H_Thread.FindThread(this.關聯ThreadName, Activeform, ref this.List_H_Thread);
                for (int i = 0; i < this.List_H_Thread.Count; i++)
                {
                    this.List_H_Thread[i].AddMethod(Run);
                }
                this.flag_Thread_Init = true;
            }
        }
        private void Run()
        {
            this.Init();
            if (this.PLC_10_初始化.Bool)
            {
                this.ROIBW8_Init();
                this.PLC_10_初始化.Bool = false;
            }
        }
        private void Init()
        {
            if (this.IsHandleCreated && this.PLC != null && this.Activeform != null && !flag_Init)
            {
                if (this.AxROIBW8 == null) this.AxROIBW8 = new AxOvkBase.AxROIBW8();
                this.PLC_00_VegaHandle.DoubleValue = AxROIBW8.VegaHandle;             
                this.flag_Init = true;
            }
        }
        public void ROIBW8_Init()
        {
            this.ParentHandle = this.PLC_01_ParentHandle.DoubleValue;
            this.OrgX = this.PLC_02_OrgX.Value;
            this.OrgY = this.PLC_03_OrgY.Value;
            this.ROIWidth = this.PLC_04_ROIWidth.Value;
            this.ROIHeight = this.PLC_05_ROIHeight.Value;
        }
        private void OnCanvasDraw(long HDC, float ZoomX, float ZoomY, int CanvasHandle)
        {
            if (this.OnCanvasDrawEvent != null) this.OnCanvasDrawEvent(HDC, ZoomX, ZoomY, CanvasHandle);
            if (PLC_00_繪製量測框.Bool)
            {
                this.ParentHandle = this.PLC_01_ParentHandle.DoubleValue;
                this.OrgX = this.PLC_02_OrgX.Value;
                this.OrgY = this.PLC_03_OrgY.Value;
                this.ROIWidth = this.PLC_04_ROIWidth.Value;
                this.ROIHeight = this.PLC_05_ROIHeight.Value;

                this.Title = this.ROI標題;
                this.ShowTitle = this.ROI標題要顯示;
                this.ShowPlacement = this.ROI框架資訊要顯示;
                this.ShowCenterCross = this.ROI中心位置要顯示;
                this.DrawFrame(HDC, ZoomX, ZoomY, 拖曳量測框顏色);
            }
            if (PLC_01_繪製OK結果.Bool)
            {
                this.ParentHandle = this.PLC_01_ParentHandle.DoubleValue;
                this.OrgX = this.PLC_02_OrgX.Value;
                this.OrgY = this.PLC_03_OrgY.Value;
                this.ROIWidth = this.PLC_04_ROIWidth.Value;
                this.ROIHeight = this.PLC_05_ROIHeight.Value;
                this.DrawSnap(HDC, ZoomX, ZoomY, Color.Lime);
            }
            if (PLC_02_繪製NG結果.Bool)
            {
                this.ParentHandle = this.PLC_01_ParentHandle.DoubleValue;
                this.OrgX = this.PLC_02_OrgX.Value;
                this.OrgY = this.PLC_03_OrgY.Value;
                this.ROIWidth = this.PLC_04_ROIWidth.Value;
                this.ROIHeight = this.PLC_05_ROIHeight.Value;
                this.DrawSnap(HDC, ZoomX, ZoomY, Color.Red);
            }

        }
        #region Function
        void DrawFrame(long HDC, float ZoomX, float ZoomY, Color color)
        {
            this.AxROIBW8.DrawFrame(HDC, ZoomX, ZoomY, 0, 0, (color.B << 16) | (color.G << 8) | color.R);
        }
        void DrawSnap(long HDC, float ZoomX, float ZoomY, Color color)
        {
            this.AxROIBW8.DrawSnap(HDC, ZoomX, ZoomY, 0, 0, (color.B << 16) | (color.G << 8) | color.R);
        }
        #endregion
        #region CanvasMouseEvent
        private int GetEventNum(int CanvasHandle)
        {
            for (int i = 0; i < this.List_EventNum.Count; i++)
            {
                if (this.List_EventHandle[i] == CanvasHandle) 
                {
                    return this.List_EventNum[i];
                }
            }
            return -1;
        }
        private bool flag_AxROIBW8_量測框架 = false;
        private AxOvkBase.TxAxHitHandle AxROIBW8_量測框架_nLockHandle = new AxOvkBase.TxAxHitHandle();
        private void OnCanvasMousDown(int x, int y, float ZoomX, float ZoomY, ref int InUsedEventNum, int InUsedCanvasHandle)
        {
            if (InUsedEventNum == 0)
            {
                if (PLC_00_繪製量測框.Bool)
                {
                    this.AxROIBW8_量測框架_nLockHandle = this.AxROIBW8.HitTest(x, y, ZoomX, ZoomY, 0, 0);
                    if (AxROIBW8_量測框架_nLockHandle != AxOvkBase.TxAxHitHandle.AX_HANDLE_NONE)
                    {
                        this.flag_AxROIBW8_量測框架 = true;
                        InUsedEventNum = this.GetEventNum(InUsedCanvasHandle);
                    }
                }
            }
        }
        private void OnCanvasMousMove(int x, int y, float ZoomX, float ZoomY)
        {
            if (flag_AxROIBW8_量測框架)
            {
                this.AxROIBW8.DragROI(this.AxROIBW8_量測框架_nLockHandle, x, y, ZoomX, ZoomY, 0, 0);
                this.PLC_02_OrgX.Value = this.AxROIBW8.OrgX;
                this.PLC_03_OrgY.Value = this.AxROIBW8.OrgY;
                this.PLC_04_ROIWidth.Value = this.AxROIBW8.ROIWidth;
                this.PLC_05_ROIHeight.Value = this.AxROIBW8.ROIHeight;
            }           
        }
        private void OnCanvasMousUp(int x, int y, float ZoomX, float ZoomY)
        {
            this.flag_AxROIBW8_量測框架 = false;
        }

        #endregion

    }
}
