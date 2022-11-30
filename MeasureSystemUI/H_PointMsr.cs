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

namespace MeasureSystemUI
{
    public partial class H_PointMsr : UserControl
    {
        AxOvkMsr.AxPointMsr AxPointMsr;
        private bool flag_Init = false;
        private bool flag_Thread_Init = false;
        private Form Activeform;
        private LowerMachine PLC;
        PLC_UI_Init PLC_UI_Init;

        private List<int> List_EventNum = new List<int>();
        private List<int> List_EventHandle = new List<int>();
        private List<H_Thread> List_H_Thread = new List<H_Thread>();
        private List<H_Canvas> List_H_Canvas = new List<H_Canvas>();

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
                if (this.AxPointMsr != null) return this.AxPointMsr.VegaHandle;
                return -1;
            }
        }
        [Browsable(false)]
        public long SrcImageHandle
        {
            get
            {
                if (this.AxPointMsr != null) return this.AxPointMsr.SrcImageHandle;
                return -1;
            }
            set
            {
                if (this.AxPointMsr != null) this.AxPointMsr.SrcImageHandle = value;
            }
        }
        [Browsable(false)]
        public int DeriThreshold
        {
            get
            {
                if (this.AxPointMsr != null) return this.AxPointMsr.DeriThreshold;
                return -1;
            }
            set
            {
                if (this.AxPointMsr != null)
                {
                    this.AxPointMsr.DeriThreshold = value;
                }
            }
        }
        [Browsable(false)]
        public int Hysteresis
        {
            get
            {
                if (this.AxPointMsr != null) return this.AxPointMsr.Hysteresis;
                return -1;
            }
            set
            {
                if (this.AxPointMsr != null)
                {
                    this.AxPointMsr.Hysteresis = value;
                }
            }
        }
        [Browsable(false)]
        public int MinGreyStep
        {
            get
            {
                if (this.AxPointMsr != null) return this.AxPointMsr.MinGreyStep;
                return -1;
            }
            set
            {
                if (this.AxPointMsr != null)
                {
                    this.AxPointMsr.MinGreyStep = value;
                }
            }
        }
        [Browsable(false)]
        public int SmoothFactor
        {
            get
            {
                if (this.AxPointMsr != null) return this.AxPointMsr.SmoothFactor;
                return -1;
            }
            set
            {
                if (this.AxPointMsr != null)
                {
                    this.AxPointMsr.SmoothFactor = value;
                }
            }
        }
        [Browsable(false)]
        public int HalfProfileThickness
        {
            get
            {
                if (this.AxPointMsr != null) return this.AxPointMsr.HalfProfileThickness;
                return -1;
            }
            set
            {
                if (this.AxPointMsr != null)
                {
                    this.AxPointMsr.HalfProfileThickness = value;
                }
            }
        }
        [Browsable(false)]
        public int StartX
        {
            get
            {
                if (this.AxPointMsr != null) return this.AxPointMsr.StartX;
                return -1;
            }
            set
            {
                if (this.AxPointMsr != null)
                {
                    this.AxPointMsr.StartX = value;
                }
            }
        }
        [Browsable(false)]
        public int StartY
        {
            get
            {
                if (this.AxPointMsr != null) return this.AxPointMsr.StartY;
                return -1;
            }
            set
            {
                if (this.AxPointMsr != null)
                {
                    this.AxPointMsr.StartY = value;
                }
            }
        }
        [Browsable(false)]
        public int EndX
        {
            get
            {
                if (this.AxPointMsr != null) return this.AxPointMsr.EndX;
                return -1;
            }
            set
            {
                if (this.AxPointMsr != null)
                {
                    this.AxPointMsr.EndX = value;
                }
            }
        }
        [Browsable(false)]
        public int EndY
        {
            get
            {
                if (this.AxPointMsr != null) return this.AxPointMsr.EndY;
                return -1;
            }
            set
            {
                if (this.AxPointMsr != null)
                {
                    this.AxPointMsr.EndY = value;
                }
            }
        }
        [Browsable(false)]
        public int EdgeOrder
        {
            get
            {
                if (this.AxPointMsr != null) return this.GetEdgeOrder();
                return -1;
            }
            set
            {
                if (this.AxPointMsr != null)
                {
                    this.SetEdgeOrder(value);
                }
            }
        }
        [Browsable(false)]
        public int EdgeType
        {
            get
            {
                if (this.AxPointMsr != null) return this.GetEdgeType();
                return -1;
            }
            set
            {
                if (this.AxPointMsr != null)
                {
                    this.SetEdgeType(value);
                }
            }
        }
        [Browsable(false)]
        public bool EdgeIsFitted
        {
            get
            {
                if (this.AxPointMsr != null) return this.AxPointMsr.EdgeIsFitted;
                return false;
            }
        }
        [Browsable(false)]
        public int DetectedEdges
        {
            get
            {
                if (this.AxPointMsr != null) return this.AxPointMsr.DetectedEdges;
                return -1;
            }
        }
        [Browsable(false)]
        public int EdgeIndex
        {
            get
            {
                if (this.AxPointMsr != null) return this.AxPointMsr.EdgeIndex;
                return -1;
            }
            set
            {
                if (this.AxPointMsr != null)
                {
                    this.AxPointMsr.EdgeIndex = value;
                }       
            }
        }
        [Browsable(false)]
        public float MeasuredX
        {
            get
            {
                if (this.AxPointMsr != null) return this.AxPointMsr.MeasuredX;
                return -1;
            }
        }
        [Browsable(false)]
        public float MeasuredY
        {
            get
            {
                if (this.AxPointMsr != null) return this.AxPointMsr.MeasuredY;
                return -1;
            }
        }
        #endregion
        public H_PointMsr()
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
        private PLC_Device PLC_01_SrcImageHandle;
        private string __01_SrcImageHandle = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _01_SrcImageHandle
        {
            get { return __01_SrcImageHandle; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __01_SrcImageHandle = value;
                else __01_SrcImageHandle = "";
            }
        }
        private PLC_Device PLC_02_變化銳利度;
        private string __02_變化銳利度 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _02_變化銳利度
        {
            get { return __02_變化銳利度; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __02_變化銳利度 = value;
                else __02_變化銳利度 = "";
            }
        }
        private PLC_Device PLC_03_延伸變化強度;
        private string __03_延伸變化強度 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _03_延伸變化強度
        {
            get { return __03_延伸變化強度; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __03_延伸變化強度 = value;
                else __03_延伸變化強度 = "";
            }
        }
        private PLC_Device PLC_04_灰階變化面積;
        private string __04_灰階變化面積 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _04_灰階變化面積
        {
            get { return __04_灰階變化面積; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __04_灰階變化面積 = value;
                else __04_灰階變化面積 = "";
            }
        }
        private PLC_Device PLC_05_抑制雜訊;
        private string __05_抑制雜訊 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _05_抑制雜訊
        {
            get { return __05_抑制雜訊; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __05_抑制雜訊 = value;
                else __05_抑制雜訊 = "";
            }
        }
        private PLC_Device PLC_06_垂直量測寬度;
        private string __06_垂直量測寬度 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _06_垂直量測寬度
        {
            get { return __06_垂直量測寬度; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __06_垂直量測寬度 = value;
                else __06_垂直量測寬度 = "";
            }
        }
        private PLC_Device PLC_07_量測排序方式;
        private string __07_量測排序方式 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _07_量測排序方式
        {
            get { return __07_量測排序方式; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __07_量測排序方式 = value;
                else __07_量測排序方式 = "";
            }
        }
        private PLC_Device PLC_08_量測顏色變化;
        private string __08_量測顏色變化 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _08_量測顏色變化
        {
            get { return __08_量測顏色變化; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __08_量測顏色變化 = value;
                else __08_量測顏色變化 = "";
            }
        }
        private PLC_Device PLC_10_StartX;
        private string __10_StartX = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _10_StartX
        {
            get { return __10_StartX; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __10_StartX = value;
                else __10_StartX = "";
            }
        }
        private PLC_Device PLC_11_StartY;
        private string __11_StartY = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _11_StartY
        {
            get { return __11_StartY; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __11_StartY = value;
                else __11_StartY = "";
            }
        }
        private PLC_Device PLC_12_EndX;
        private string __12_EndX = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _12_EndX
        {
            get { return __12_EndX; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __12_EndX = value;
                else __12_EndX = "";
            }
        }
        private PLC_Device PLC_13_EndY;
        private string __13_EndY = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _13_EndY
        {
            get { return __13_EndY; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __13_EndY = value;
                else __13_EndY = "";
            }
        }
        private PLC_Device PLC_20_MeasuredX;
        private string __20_MeasuredX = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _20_MeasuredX
        {
            get { return __20_MeasuredX; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __20_MeasuredX = value;
                else __20_MeasuredX = "";
            }
        }
        private PLC_Device PLC_21_MeasuredY;
        private string __21_MeasuredY = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _21_MeasuredY
        {
            get { return __21_MeasuredY; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __21_MeasuredY = value;
                else __21_MeasuredY = "";
            }
        }
        private PLC_Device PLC_22_量測點總數;
        private string __22_量測點總數 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _22_量測點總數
        {
            get { return __22_量測點總數; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __22_量測點總數 = value;
                else __22_量測點總數 = "";
            }
        }
        private PLC_Device PLC_23_量測點索引值;
        private string __23_量測點索引值 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _23_量測點索引值
        {
            get { return __23_量測點索引值; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __23_量測點索引值 = value;
                else __23_量測點索引值 = "";
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

        private PLC_Device PLC_01_開始量測;
        private string __01_開始量測 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _01_開始量測
        {
            get { return __01_開始量測; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __01_開始量測 = value;
                else __01_開始量測 = "";
            }
        }

        private PLC_Device PLC_02_初始化;
        private string __02_初始化 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _02_初始化
        {
            get { return __02_初始化; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __02_初始化 = value;
                else __02_初始化 = "";
            }
        }

        private PLC_Device PLC_10_繪製量測點;
        private string __10_繪製量測點 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _10_繪製量測點
        {
            get { return __10_繪製量測點; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __10_繪製量測點 = value;
                else __10_繪製量測點 = "";
            }
        }

        private PLC_Device PLC_11_繪製灰階變化及微分圖形;
        private string __11_繪製灰階變化及微分圖形 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _11_繪製灰階變化及微分圖形
        {
            get { return __11_繪製灰階變化及微分圖形; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __11_繪製灰階變化及微分圖形 = value;
                else __11_繪製灰階變化及微分圖形 = "";
            }
        }
        #endregion
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
                this.PLC_00_VegaHandle.DoubleValue = 0;
            }

            if (this._01_SrcImageHandle != string.Empty)
            {
                this.PLC_01_SrcImageHandle = new PLC_Device(this._01_SrcImageHandle);
                plC_NumBox_SrcImageHandle.寫入元件位置 = this._01_SrcImageHandle;
                plC_NumBox_SrcImageHandle.讀取元件位置 = this._01_SrcImageHandle;
                plC_NumBox_SrcImageHandle.Run(this.PLC);
            }

            if (this._02_變化銳利度 != string.Empty)
            {
                this.PLC_02_變化銳利度 = new PLC_Device(this._02_變化銳利度);
                plC_NumBox_變化銳利度.寫入元件位置 = this._02_變化銳利度;
                plC_NumBox_變化銳利度.讀取元件位置 = this._02_變化銳利度;
                plC_NumBox_變化銳利度.Run(this.PLC);
            }

            if (this._03_延伸變化強度 != string.Empty)
            {
                this.PLC_03_延伸變化強度 = new PLC_Device(this._03_延伸變化強度);
                plC_NumBox_延伸變化強度.寫入元件位置 = this._03_延伸變化強度;
                plC_NumBox_延伸變化強度.讀取元件位置 = this._03_延伸變化強度;
                plC_NumBox_延伸變化強度.Run(this.PLC);
            }

            if (this._04_灰階變化面積 != string.Empty)
            {
                this.PLC_04_灰階變化面積 = new PLC_Device(this._04_灰階變化面積);
                plC_NumBox_灰階變化面積.寫入元件位置 = this._04_灰階變化面積;
                plC_NumBox_灰階變化面積.讀取元件位置 = this._04_灰階變化面積;
                plC_NumBox_灰階變化面積.Run(this.PLC);
            }

            if (this._05_抑制雜訊 != string.Empty)
            {
                this.PLC_05_抑制雜訊 = new PLC_Device(this._05_抑制雜訊);
                plC_NumBox_抑制雜訊.寫入元件位置 = this._05_抑制雜訊;
                plC_NumBox_抑制雜訊.讀取元件位置 = this._05_抑制雜訊;
                plC_NumBox_抑制雜訊.Run(this.PLC);
            }

            if (this._06_垂直量測寬度 != string.Empty)
            {
                this.PLC_06_垂直量測寬度 = new PLC_Device(this._06_垂直量測寬度);
                plC_NumBox_垂直量測寬度.寫入元件位置 = this._06_垂直量測寬度;
                plC_NumBox_垂直量測寬度.讀取元件位置 = this._06_垂直量測寬度;
                plC_NumBox_垂直量測寬度.Run(this.PLC);
            }

            if (this._07_量測排序方式 != string.Empty)
            {
                this.PLC_07_量測排序方式 = new PLC_Device(this._07_量測排序方式);
                plC_ComboBox_量測排序方式.寫入元件位置 = this._07_量測排序方式;
                plC_ComboBox_量測排序方式.讀取元件位置 = this._07_量測排序方式;
                plC_ComboBox_量測排序方式.Run(this.PLC);
            }
            if (this._08_量測顏色變化 != string.Empty)
            {
                this.PLC_08_量測顏色變化 = new PLC_Device(this._08_量測顏色變化);
                plC_ComboBox_量測顏色變化.寫入元件位置 = this._08_量測顏色變化;
                plC_ComboBox_量測顏色變化.讀取元件位置 = this._08_量測顏色變化;
                plC_ComboBox_量測顏色變化.Run(this.PLC);
            }

            if (this._10_StartX != string.Empty)
            {
                this.PLC_10_StartX = new PLC_Device(this._10_StartX);
                plC_NumBox_StartX.寫入元件位置 = this._10_StartX;
                plC_NumBox_StartX.讀取元件位置 = this._10_StartX;
                plC_NumBox_StartX.Run(this.PLC);
            }

            if (this._11_StartY != string.Empty)
            {
                this.PLC_11_StartY = new PLC_Device(this._11_StartY);
                plC_NumBox_StartY.寫入元件位置 = this._11_StartY;
                plC_NumBox_StartY.讀取元件位置 = this._11_StartY;
                plC_NumBox_StartY.Run(this.PLC);
            }

            if (this._12_EndX != string.Empty)
            {
                this.PLC_12_EndX = new PLC_Device(this._12_EndX);
                plC_NumBox_EndX.寫入元件位置 = this._12_EndX;
                plC_NumBox_EndX.讀取元件位置 = this._12_EndX;
                plC_NumBox_EndX.Run(this.PLC);
            }

            if (this._13_EndY != string.Empty)
            {
                this.PLC_13_EndY = new PLC_Device(this._13_EndY);
                plC_NumBox_EndY.寫入元件位置 = this._13_EndY;
                plC_NumBox_EndY.讀取元件位置 = this._13_EndY;
                plC_NumBox_EndY.Run(this.PLC);
            }
            if ((this.PLC_10_StartX.Value == this.PLC_12_EndX.Value) && (this.PLC_11_StartY.Value == this.PLC_13_EndY.Value))
            {
                this.PLC_10_StartX.Value = 100;
                this.PLC_12_EndX.Value = 100;

                this.PLC_11_StartY.Value = 500;
                this.PLC_13_EndY.Value = 500;
            }
            if (this._20_MeasuredX != string.Empty)
            {
                this.PLC_20_MeasuredX = new PLC_Device(this._20_MeasuredX);
                plC_NumBox_MeasuredX.寫入元件位置 = this._20_MeasuredX;
                plC_NumBox_MeasuredX.讀取元件位置 = this._20_MeasuredX;
                plC_NumBox_MeasuredX.Run(this.PLC);
                this.PLC_20_MeasuredX.Value = 0;
            }

            if (this._21_MeasuredY != string.Empty)
            {
                this.PLC_21_MeasuredY = new PLC_Device(this._21_MeasuredY);
                plC_NumBox_MeasuredY.寫入元件位置 = this._21_MeasuredY;
                plC_NumBox_MeasuredY.讀取元件位置 = this._21_MeasuredY;
                plC_NumBox_MeasuredY.Run(this.PLC);
                this.PLC_21_MeasuredY.Value = 0;
            }

            if (this._22_量測點總數 != string.Empty)
            {
                this.PLC_22_量測點總數 = new PLC_Device(this._22_量測點總數);
                plC_NumBox_量測點總數.寫入元件位置 = this._22_量測點總數;
                plC_NumBox_量測點總數.讀取元件位置 = this._22_量測點總數;
                plC_NumBox_量測點總數.Run(this.PLC);
                this.PLC_22_量測點總數.Value = 0;
            }

            if (this._23_量測點索引值 != string.Empty)
            {
                this.PLC_23_量測點索引值 = new PLC_Device(this._23_量測點索引值);
                plC_NumBox_量測點索引值.寫入元件位置 = this._23_量測點索引值;
                plC_NumBox_量測點索引值.讀取元件位置 = this._23_量測點索引值;
                plC_NumBox_量測點索引值.Run(this.PLC);
                this.PLC_23_量測點索引值.Value = 0;
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
            if (this._01_開始量測 != string.Empty)
            {
                this.PLC_01_開始量測 = new PLC_Device(this._01_開始量測);
                plC_Button_開始量測.寫入元件位置 = this._01_開始量測;
                plC_Button_開始量測.讀取元件位置 = this._01_開始量測;
                plC_Button_開始量測.Run(this.PLC);
                this.PLC_01_開始量測.Bool = false;
            }
            if (this._02_初始化 != string.Empty)
            {
                this.PLC_02_初始化 = new PLC_Device(this._02_初始化);
                plC_Button_初始化.寫入元件位置 = this._02_初始化;
                plC_Button_初始化.讀取元件位置 = this._02_初始化;
                plC_Button_初始化.Run(this.PLC);
                this.PLC_02_初始化.Bool = false;
            }
            if (this._10_繪製量測點 != string.Empty)
            {
                this.PLC_10_繪製量測點 = new PLC_Device(this._10_繪製量測點);
                plC_CheckBox_繪製量測點.寫入元件位置 = this._10_繪製量測點;
                plC_CheckBox_繪製量測點.讀取元件位置 = this._10_繪製量測點;
                plC_CheckBox_繪製量測點.Run(this.PLC);
            }
            if (this._11_繪製灰階變化及微分圖形 != string.Empty)
            {
                this.PLC_11_繪製灰階變化及微分圖形 = new PLC_Device(this._11_繪製灰階變化及微分圖形);
                plC_CheckBox_繪製灰階變化及微分圖形.寫入元件位置 = this._11_繪製灰階變化及微分圖形;
                plC_CheckBox_繪製灰階變化及微分圖形.讀取元件位置 = this._11_繪製灰階變化及微分圖形;
                plC_CheckBox_繪製灰階變化及微分圖形.Run(this.PLC);
            }
            #endregion

            H_Canvas.FindCanvas(this.關聯CanvasName, Activeform, ref this.List_H_Canvas);
            int EventNum = 0;
            int EventHandle = 0;
            for (int i = 0; i < this.List_H_Canvas.Count; i++)
            {
                this.List_H_Canvas[i].AddMouseEvent(this.OnCanvasMouseDown, this.OnCanvasMouseMove, this.OnCanvasMouseUp, ref EventNum, ref EventHandle);
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
            if(this.PLC_01_開始量測.Bool)
            {
                this.AxPointMsr_Init();
                this.DetectPrimitives();
                this.PLC_22_量測點總數.Value = 0;
                if (this.EdgeIsFitted)
                {
                    this.PLC_22_量測點總數.Value = this.DetectedEdges;
                    this.PLC_23_量測點索引值.Value = 0;
                    this.PLC_20_MeasuredX.Value = (int)(this.MeasuredX * 10000);
                    this.PLC_21_MeasuredY.Value = (int)(this.MeasuredY * 10000);
                }
                this.PLC_01_開始量測.Bool = false;
            }
            if (this.PLC_23_量測點索引值.Value != this.EdgeIndex)
            {
                this.EdgeIndex = this.PLC_23_量測點索引值.Value;
                this.PLC_20_MeasuredX.Value = (int)(this.MeasuredX * 10000);
                this.PLC_21_MeasuredY.Value = (int)(this.MeasuredY * 10000);
            }
            if (this.PLC_02_初始化.Bool)
            {
                this.AxPointMsr_Init();
                this.PLC_02_初始化.Bool = false;
            }
      
        }
        private void Init()
        {
            if (this.IsHandleCreated && this.PLC != null && this.Activeform != null && !flag_Init)
            {
                if (this.AxPointMsr == null) this.AxPointMsr = new AxOvkMsr.AxPointMsr();
                this.PLC_00_VegaHandle.DoubleValue = AxPointMsr.VegaHandle;
                this.flag_Init = true;
            }
        }
        private void OnCanvasDraw(long HDC, float ZoomX, float ZoomY, int CanvasHandle)
        {
            if (this.OnCanvasDrawEvent != null) this.OnCanvasDrawEvent(HDC, ZoomX, ZoomY, CanvasHandle);
            if (this.PLC_00_繪製量測框.Bool)
            {
                this.DrawFrame(HDC, ZoomX, ZoomY);            
            }
            if (this.PLC_10_繪製量測點.Bool)
            {
               this.DrawFittedPrimitives(HDC, ZoomX, ZoomY);            
            }
            if(this.PLC_11_繪製灰階變化及微分圖形.Bool)
            {
                this.DrawMixedProfile(HDC);
            }
        }
        #region Funtion
        public void AxPointMsr_Init()
        {
            this.SrcImageHandle = this.PLC_01_SrcImageHandle.DoubleValue;
            this.DeriThreshold = this.PLC_02_變化銳利度.Value;
            this.Hysteresis = this.PLC_03_延伸變化強度.Value;
            this.MinGreyStep = this.PLC_04_灰階變化面積.Value;
            this.SmoothFactor = this.PLC_05_抑制雜訊.Value;
            this.HalfProfileThickness = this.PLC_06_垂直量測寬度.Value;

            this.EdgeOrder = this.PLC_07_量測排序方式.Value;
            this.EdgeType = this.PLC_08_量測顏色變化.Value;

            this.StartX = this.PLC_10_StartX.Value;
            this.StartY = this.PLC_11_StartY.Value;
            this.EndX = this.PLC_12_EndX.Value;
            this.EndY = this.PLC_13_EndY.Value;

          

        }
        private int GetEdgeOrder()
        {
            if (this.AxPointMsr.EdgeOrder == AxOvkMsr.TxAxTransitionOrder.AX_MEASURE_BEST_SHARPNESS_EDGE) return 0;
            else if (this.AxPointMsr.EdgeOrder == AxOvkMsr.TxAxTransitionOrder.AX_MEASURE_BEST_SMOOTHNESS_EDGE) return 1;
            else if (this.AxPointMsr.EdgeOrder == AxOvkMsr.TxAxTransitionOrder.AX_MEASURE_NTH_FROM_BEGIN) return 2;
            else if (this.AxPointMsr.EdgeOrder == AxOvkMsr.TxAxTransitionOrder.AX_MEASURE_NTH_FROM_END) return 3;
            return -1;
        }
        private void SetEdgeOrder(int value)
        {
            if (value == 0)
            {
                this.AxPointMsr.EdgeOrder = AxOvkMsr.TxAxTransitionOrder.AX_MEASURE_BEST_SHARPNESS_EDGE;
            }
            else if (value == 1)
            {
                this.AxPointMsr.EdgeOrder = AxOvkMsr.TxAxTransitionOrder.AX_MEASURE_BEST_SMOOTHNESS_EDGE;
            }
            else if (value == 2)
            {
                this.AxPointMsr.EdgeOrder = AxOvkMsr.TxAxTransitionOrder.AX_MEASURE_NTH_FROM_BEGIN;
            }
            else if (value == 3)
            {
                this.AxPointMsr.EdgeOrder = AxOvkMsr.TxAxTransitionOrder.AX_MEASURE_NTH_FROM_END;
            }
        }
        private int GetEdgeType()
        {
            if (this.AxPointMsr.EdgeType == AxOvkMsr.TxAxTransitionType.AX_MEASURE_B2W_TRANSITION) return 0;
            else if (this.AxPointMsr.EdgeType == AxOvkMsr.TxAxTransitionType.AX_MEASURE_W2B_TRANSITION) return 1;
            else if (this.AxPointMsr.EdgeType == AxOvkMsr.TxAxTransitionType.AX_MEASURE_B2W_OR_W2B_TRANSITION) return 2;
            else if (this.AxPointMsr.EdgeType == AxOvkMsr.TxAxTransitionType.AX_MEASURE_NONE_TRANSITION) return 3;
            return -1;
        }
        private void SetEdgeType(int value)
        {
            if (value == 0)
            {
                this.AxPointMsr.EdgeType = AxOvkMsr.TxAxTransitionType.AX_MEASURE_B2W_TRANSITION;
            }
            else if (value == 1)
            {
                this.AxPointMsr.EdgeType = AxOvkMsr.TxAxTransitionType.AX_MEASURE_W2B_TRANSITION;
            }
            else if (value == 2)
            {
                this.AxPointMsr.EdgeType = AxOvkMsr.TxAxTransitionType.AX_MEASURE_B2W_OR_W2B_TRANSITION;
            }
            else if (value == 3)
            {
                this.AxPointMsr.EdgeType = AxOvkMsr.TxAxTransitionType.AX_MEASURE_NONE_TRANSITION;
            }
        }
        public void DetectPrimitives()
        {
            this.AxPointMsr.DetectPrimitives();
        }
        public void DrawFrame(long HDC, float ZoomX, float ZoomY)
        {
            this.AxPointMsr.DrawFrame(HDC, ZoomX, ZoomY, 0, 0);
        }
        public void SetLineSegmentHorizontal()
        {
            this.AxPointMsr.SetLineSegmentHorizontal();
        }
        public void SetLineSegmentVertical()
        {
            this.AxPointMsr.SetLineSegmentVertical();
        }
        public void DrawFittedPrimitives(long HDC, float ZoomX, float ZoomY)
        {
            for (int i = 0; i < this.DetectedEdges; i++)
            {
                this.EdgeIndex = i;
                this.AxPointMsr.DrawFittedPrimitives(HDC, ZoomX, ZoomY, 0, 0);
            }
       
        }
        public void DrawMixedProfile(long HDC)
        {
            this.AxPointMsr.DrawMixedProfile(HDC);
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
        private bool flag_AxPointMsr_量測框架 = false;
        private AxOvkMsr.TxAxPointMsrDragHandle AxPointMsr_量測框架_nLockHandle = new AxOvkMsr.TxAxPointMsrDragHandle();
        private void OnCanvasMouseDown(int x, int y, float ZoomX, float ZoomY, ref int InUsedEventNum, int InUsedCanvasHandle)
        {
            if (InUsedEventNum == 0)
            {
                if (PLC_00_繪製量測框.Bool)
                {
                    this.AxPointMsr_量測框架_nLockHandle = this.AxPointMsr.HitTest(x, y, ZoomX, ZoomY, 0, 0);
                    if (this.AxPointMsr_量測框架_nLockHandle != AxOvkMsr.TxAxPointMsrDragHandle.AX_POINTMSR_NONE)
                    {
                        this.flag_AxPointMsr_量測框架 = true;
                        InUsedEventNum = this.GetEventNum(InUsedCanvasHandle);
                    }
                }
            }
        }
        private void OnCanvasMouseMove(int x, int y, float ZoomX, float ZoomY)
        {
            if (flag_AxPointMsr_量測框架)
            {
                this.AxPointMsr.DragFrame(this.AxPointMsr_量測框架_nLockHandle, x, y, ZoomX, ZoomY, 0, 0);
                this.PLC_10_StartX.Value = this.AxPointMsr.StartX;
                this.PLC_11_StartY.Value = this.AxPointMsr.StartY;
                this.PLC_12_EndX.Value = this.AxPointMsr.EndX;
                this.PLC_13_EndY.Value = this.AxPointMsr.EndY;
            }
        }
        private void OnCanvasMouseUp(int x, int y, float ZoomX, float ZoomY)
        {
            this.flag_AxPointMsr_量測框架 = false;
        }

        #endregion
    }
}
