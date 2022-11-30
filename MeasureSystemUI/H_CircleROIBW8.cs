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
    public partial class H_CircleROIBW8 : UserControl
    {
        AxOvkBase.AxCircleROIBW8 AxCircleROIBW8;

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
                if (this.AxCircleROIBW8 != null) return this.AxCircleROIBW8.VegaHandle;
                return -1;
            }
        }
        [Browsable(false)]
        public long ParentHandle
        {
            get
            {
                if (this.AxCircleROIBW8 != null) return this.AxCircleROIBW8.ParentHandle;
                return -1;
            }
            set
            {
                if (this.AxCircleROIBW8 != null) this.AxCircleROIBW8.ParentHandle = value;
            }
        }
        [Browsable(false)]
        public float CenterX
        {
            get
            {
                if (this.AxCircleROIBW8 != null) return this.AxCircleROIBW8.CenterX;
                return -1;
            }
            set
            {
                if (this.AxCircleROIBW8 != null) this.AxCircleROIBW8.CenterX = value;
            }
        }
        [Browsable(false)]
        public float CenterY
        {
            get
            {
                if (this.AxCircleROIBW8 != null) return this.AxCircleROIBW8.CenterY;
                return -1;
            }
            set
            {
                if (this.AxCircleROIBW8 != null) this.AxCircleROIBW8.CenterY = value;
            }
        }
        [Browsable(false)]
        public float Radius
        {
            get
            {
                if (this.AxCircleROIBW8 != null) return this.AxCircleROIBW8.Radius;
                return -1;
            }
            set
            {
                if (this.AxCircleROIBW8 != null) this.AxCircleROIBW8.Radius = value;
            }
        }
        [Browsable(false)]
        public int ParentOrgX
        {
            get
            {
                if (this.AxCircleROIBW8 != null) return this.AxCircleROIBW8.ParentOrgX;
                return -1;
            }
        }
        [Browsable(false)]
        public int ParentOrgY
        {
            get
            {
                if (this.AxCircleROIBW8 != null) return this.AxCircleROIBW8.ParentOrgY;
                return -1;
            }
        }
        [Browsable(false)]
        public int ParentWidth
        {
            get
            {
                if (this.AxCircleROIBW8 != null) return this.AxCircleROIBW8.ParentWidth;
                return -1;
            }
        }
        [Browsable(false)]
        public int ParentHeight
        {
            get
            {
                if (this.AxCircleROIBW8 != null) return this.AxCircleROIBW8.ParentHeight;
                return -1;
            }
        }
        [Browsable(false)]
        public long TopParentHandle
        {
            get
            {
                if (this.AxCircleROIBW8 != null) return this.AxCircleROIBW8.TopParentHandle;
                return -1;
            }
        }
        [Browsable(false)]
        public int TotalOrgX
        {
            get
            {
                if (this.AxCircleROIBW8 != null) return this.AxCircleROIBW8.TotalOrgX;
                return -1;
            }
        }
        [Browsable(false)]
        public int TotalOrgY
        {
            get
            {
                if (this.AxCircleROIBW8 != null) return this.AxCircleROIBW8.TotalOrgY;
                return -1;
            }
        }
        [Browsable(false)]
        public int TotalWidth
        {
            get
            {
                if (this.AxCircleROIBW8 != null) return this.AxCircleROIBW8.TotalWidth;
                return -1;
            }
        }
        [Browsable(false)]
        public int TotalHeight
        {
            get
            {
                if (this.AxCircleROIBW8 != null) return this.AxCircleROIBW8.TotalHeight;
                return -1;
            }
        }
        [Browsable(false)]
        public bool ShowTitle
        {
            get
            {
                if (this.AxCircleROIBW8 != null) return this.AxCircleROIBW8.ShowTitle;
                return false;
            }
            set
            {
                if (this.AxCircleROIBW8 != null) this.AxCircleROIBW8.ShowTitle = value;
            }
        }
        [Browsable(false)]
        public string Title
        {
            get
            {
                if (this.AxCircleROIBW8 != null) return this.AxCircleROIBW8.Title;
                return "";
            }
            set
            {
                if (this.AxCircleROIBW8 != null) this.AxCircleROIBW8.Title = value;
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
        private PLC_Device PLC_02_CenterX;
        private string __02_CenterX = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _02_CenterX
        {
            get { return __02_CenterX; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __02_CenterX = value;
                else __02_CenterX = "";
            }
        }
        private PLC_Device PLC_03_CenterY;
        private string __03_CenterY = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _03_CenterY
        {
            get { return __03_CenterY; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __03_CenterY = value;
                else __03_CenterY = "";
            }
        }
        private PLC_Device PLC_04_Radius;
        private string __04_Radius = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _04_Radius
        {
            get { return __04_Radius; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __04_Radius = value;
                else __04_Radius = "";
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

        public H_CircleROIBW8()
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
            if (this._02_CenterX != string.Empty)
            {
                this.PLC_02_CenterX = new PLC_Device(this._02_CenterX);
                if (PLC_02_CenterX.Value == 0) PLC_02_CenterX.Value = 100;
                plC_NumBox_CenterX.寫入元件位置 = this._02_CenterX;
                plC_NumBox_CenterX.讀取元件位置 = this._02_CenterX;
                plC_NumBox_CenterX.Run(this.PLC);

            }
            if (this._03_CenterY != string.Empty)
            {
                this.PLC_03_CenterY = new PLC_Device(this._03_CenterY);
                if (PLC_03_CenterY.Value == 0) PLC_03_CenterY.Value = 100;
                plC_NumBox_CenterY.寫入元件位置 = this._03_CenterY;
                plC_NumBox_CenterY.讀取元件位置 = this._03_CenterY;
                plC_NumBox_CenterY.Run(this.PLC);
            }
            if (this._04_Radius != string.Empty)
            {
                this.PLC_04_Radius = new PLC_Device(this._04_Radius);
                if (PLC_04_Radius.Value == 0) PLC_04_Radius.Value = 200;
                plC_NumBox_Radius.寫入元件位置 = this._04_Radius;
                plC_NumBox_Radius.讀取元件位置 = this._04_Radius;
                plC_NumBox_Radius.Run(this.PLC);
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
                this.AxCircleROIBW8_Init();
                this.PLC_10_初始化.Bool = false;
            }
        }
        private void Init()
        {
            if (this.IsHandleCreated && this.PLC != null && this.Activeform != null && !flag_Init)
            {
                if (this.AxCircleROIBW8 == null) this.AxCircleROIBW8 = new AxOvkBase.AxCircleROIBW8();
                this.PLC_00_VegaHandle.DoubleValue = AxCircleROIBW8.VegaHandle;
                this.flag_Init = true;
            }
        }
        public void AxCircleROIBW8_Init()
        {
            this.ParentHandle = this.PLC_01_ParentHandle.DoubleValue;
            this.CenterX = this.PLC_02_CenterX.Value;
            this.CenterY = this.PLC_03_CenterY.Value;
            this.Radius = this.PLC_04_Radius.Value;
        }
        private void OnCanvasDraw(long HDC, float ZoomX, float ZoomY, int CanvasHandle)
        {
            if (this.OnCanvasDrawEvent != null) this.OnCanvasDrawEvent(HDC, ZoomX, ZoomY, CanvasHandle);
            if (PLC_00_繪製量測框.Bool)
            {
                this.ParentHandle = this.PLC_01_ParentHandle.DoubleValue;
                this.CenterX = this.PLC_02_CenterX.Value;
                this.CenterY = this.PLC_03_CenterY.Value;
                this.Radius = this.PLC_04_Radius.Value;
                this.Title = this.ROI標題;
                this.ShowTitle = this.ROI標題要顯示;

                this.DrawFrame(HDC, ZoomX, ZoomY, 拖曳量測框顏色);
            }
            if (PLC_01_繪製OK結果.Bool)
            {
                this.ParentHandle = this.PLC_01_ParentHandle.DoubleValue;
                this.CenterX = this.PLC_02_CenterX.Value;
                this.CenterY = this.PLC_03_CenterY.Value;
                this.Radius = this.PLC_04_Radius.Value;
                this.DrawCircle(HDC, ZoomX, ZoomY, Color.Lime);
            }
            if (PLC_02_繪製NG結果.Bool)
            {
                this.ParentHandle = this.PLC_01_ParentHandle.DoubleValue;
                this.CenterX = this.PLC_02_CenterX.Value;
                this.CenterY = this.PLC_03_CenterY.Value;
                this.Radius = this.PLC_04_Radius.Value;
                this.DrawCircle(HDC, ZoomX, ZoomY, Color.Red);
            }

        }
        #region Function
        void DrawFrame(long HDC, float ZoomX, float ZoomY, Color color)
        {
            this.AxCircleROIBW8.DrawFrame(HDC, ZoomX, ZoomY, 0, 0, (color.B << 16) | (color.G << 8) | color.R);
        }
        void DrawCircle(long HDC, float ZoomX, float ZoomY, Color color)
        {
            this.AxCircleROIBW8.DrawCircle(HDC, ZoomX, ZoomY, 0, 0, (color.B << 16) | (color.G << 8) | color.R);
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
        private bool flag_AxCircleROIBW8_量測框架 = false;
        private AxOvkBase.TxAxCircleRoiHitHandle AxCircleROIBW8_量測框架_nLockHandle = new AxOvkBase.TxAxCircleRoiHitHandle();
        private void OnCanvasMousDown(int x, int y, float ZoomX, float ZoomY, ref int InUsedEventNum, int InUsedCanvasHandle)
        {
            if (InUsedEventNum == 0)
            {
                if (PLC_00_繪製量測框.Bool)
                {
                    this.AxCircleROIBW8_量測框架_nLockHandle = this.AxCircleROIBW8.HitTest(x, y, ZoomX, ZoomY, 0, 0);
                    if (AxCircleROIBW8_量測框架_nLockHandle != AxOvkBase.TxAxCircleRoiHitHandle.AX_CIRCLEROI_HANDLE_NONE)
                    {
                        this.flag_AxCircleROIBW8_量測框架 = true;
                        InUsedEventNum = this.GetEventNum(InUsedCanvasHandle);
                    }
                }
            }
        }
        private void OnCanvasMousMove(int x, int y, float ZoomX, float ZoomY)
        {
            if (flag_AxCircleROIBW8_量測框架)
            {
                this.AxCircleROIBW8.DragROI(this.AxCircleROIBW8_量測框架_nLockHandle, x, y, ZoomX, ZoomY, 0, 0);
                this.PLC_02_CenterX.Value = (int)this.AxCircleROIBW8.CenterX;
                this.PLC_03_CenterY.Value = (int)this.AxCircleROIBW8.CenterY;
                this.PLC_04_Radius.Value = (int)this.AxCircleROIBW8.Radius;
            }
        }
        private void OnCanvasMousUp(int x, int y, float ZoomX, float ZoomY)
        {
            this.flag_AxCircleROIBW8_量測框架 = false;
        }

        #endregion
    }
}
