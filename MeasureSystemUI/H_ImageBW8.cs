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
    public partial class H_ImageBW8 : UserControl
    {
        private AxOvkBase.AxImageBW8 AxImageBW8;
        private bool flag_Init = false;
        private bool flag_Thread_Init = false;
        private Form Activeform;
        private LowerMachine PLC;
        PLC_UI_Init PLC_UI_Init;
        private List<H_Thread> List_H_Thread = new List<H_Thread>();
        private List<H_Canvas> List_H_Canvas = new List<H_Canvas>();

        #region 元件參數
        [Browsable(false)]
        public long VegaHandle
        {
            get
            {
                if (this.AxImageBW8 != null) return this.AxImageBW8.VegaHandle;
                return -1;
            }
        }
        #endregion
        #region Event
        public delegate void OnCanvasDrawEventHandler(long HDC, float ZoomX, float ZoomY, int CanvasHandle);
        public event OnCanvasDrawEventHandler OnCanvasDrawEvent;
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
        private PLC_Device PLC_01_ImageWidth;
        private string __01_ImageWidth = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _01_ImageWidth
        {
            get { return __01_ImageWidth; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __01_ImageWidth = value;
                else __01_ImageWidth = "";
            }
        }
        private PLC_Device PLC_02_ImageHeight;
        private string __02_ImageHeight = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _02_ImageHeight
        {
            get { return __02_ImageHeight; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __02_ImageHeight = value;
                else __02_ImageHeight = "";
            }
        }

        private PLC_Device PLC_10_Ptr;
        private string __10_Ptr = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _10_Ptr
        {
            get { return __10_Ptr; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __10_Ptr = value;
                else __10_Ptr = "";
            }
        }

        private PLC_Device PLC_11_PtrWidth;
        private string __11_PtrWidth = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _11_PtrWidth
        {
            get { return __11_PtrWidth; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __11_PtrWidth = value;
                else __11_PtrWidth = "";
            }
        }

        private PLC_Device PLC_12_PtrHeight;
        private string __12_PtrHeight = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _12_PtrHeight
        {
            get { return __12_PtrHeight; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F") divice_OK = true;
                }

                if (divice_OK) __12_PtrHeight = value;
                else __12_PtrHeight = "";
            }
        }
        #endregion
        #region 旗標位置
        private PLC_Device PLC_00_SetPtr;
        private string __00_SetPtr = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _00_SetPtr
        {
            get { return __00_SetPtr; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __00_SetPtr = value;
                else __00_SetPtr = "";
            }
        }

        private PLC_Device PLC_01_繪製影像;
        private string __01_繪製影像 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _01_繪製影像
        {
            get { return __01_繪製影像; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __01_繪製影像 = value;
                else __01_繪製影像 = "";
            }
        }
        #endregion

        public H_ImageBW8()
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
                this.PLC_00_VegaHandle.DoubleValue = 0;
            }
            if (this._01_ImageWidth != string.Empty)
            {
                this.PLC_01_ImageWidth = new PLC_Device(this._01_ImageWidth);
                plC_NumBox_ImageWidth.寫入元件位置 = this._01_ImageWidth;
                plC_NumBox_ImageWidth.讀取元件位置 = this._01_ImageWidth;
                plC_NumBox_ImageWidth.Run(this.PLC);
                this.PLC_01_ImageWidth.Value = 0;
            }
            if (this._02_ImageHeight != string.Empty)
            {
                this.PLC_02_ImageHeight = new PLC_Device(this._02_ImageHeight);
                plC_NumBox_ImageHeight.寫入元件位置 = this._02_ImageHeight;
                plC_NumBox_ImageHeight.讀取元件位置 = this._02_ImageHeight;
                plC_NumBox_ImageHeight.Run(this.PLC);
                this.PLC_02_ImageHeight.Value = 0;

            }
            if (this._10_Ptr != string.Empty)
            {
                this.PLC_10_Ptr = new PLC_Device(this._10_Ptr);
                plC_NumBox_Ptr.寫入元件位置 = this._10_Ptr;
                plC_NumBox_Ptr.讀取元件位置 = this._10_Ptr;
                plC_NumBox_Ptr.Run(this.PLC);
                this.PLC_10_Ptr.Value = 0;
            }
            if (this._11_PtrWidth != string.Empty)
            {
                this.PLC_11_PtrWidth = new PLC_Device(this._11_PtrWidth);
                plC_NumBox_PtrWidth.寫入元件位置 = this._11_PtrWidth;
                plC_NumBox_PtrWidth.讀取元件位置 = this._11_PtrWidth;
                plC_NumBox_PtrWidth.Run(this.PLC);
                this.PLC_11_PtrWidth.Value = 0;
            }
            if (this._12_PtrHeight != string.Empty)
            {
                this.PLC_12_PtrHeight = new PLC_Device(this._12_PtrHeight);
                plC_NumBox_PtrHeight.寫入元件位置 = this._12_PtrHeight;
                plC_NumBox_PtrHeight.讀取元件位置 = this._12_PtrHeight;
                plC_NumBox_PtrHeight.Run(this.PLC);
                this.PLC_12_PtrHeight.Value = 0;
            }
            #endregion
            #region 旗標位置初始化
            if (this._00_SetPtr != string.Empty)
            {
                this.PLC_00_SetPtr = new PLC_Device(this._00_SetPtr);
                plC_Button_SetPtr.寫入元件位置 = this._00_SetPtr;
                plC_Button_SetPtr.讀取元件位置 = this._00_SetPtr;
                plC_Button_SetPtr.Run(this.PLC);
                this.PLC_00_SetPtr.Bool = false;
            }
            if (this._01_繪製影像 != string.Empty)
            {
                this.PLC_01_繪製影像 = new PLC_Device(this._01_繪製影像);
                plC_Button_繪製影像.寫入元件位置 = this._01_繪製影像;
                plC_Button_繪製影像.讀取元件位置 = this._01_繪製影像;
                plC_Button_繪製影像.Run(this.PLC);
                this.PLC_01_繪製影像.Bool = false;
            }
            #endregion

            H_Canvas.FindCanvas(this.關聯CanvasName, Activeform, ref this.List_H_Canvas);
            for (int i = 0; i < this.List_H_Canvas.Count; i++)
            {
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
            if (this.PLC_00_SetPtr.Bool)
            {
                this.SetSurfacePtr(this.PLC_11_PtrWidth.Value, this.PLC_12_PtrHeight.Value, this.PLC_10_Ptr.DoubleValue);
                this.PLC_00_SetPtr.Bool = false;
            }
        }
        private void Init()
        {
            if (this.IsHandleCreated && this.PLC != null && this.Activeform != null && !flag_Init)
            {
                if (this.AxImageBW8 == null) this.AxImageBW8 = new AxOvkBase.AxImageBW8();
                this.PLC_00_VegaHandle.DoubleValue = AxImageBW8.VegaHandle;
                this.flag_Init = true;
            }
        }
        private void OnCanvasDraw(long HDC, float ZoomX, float ZoomY, int CanvasHandle)
        {
            if (this.OnCanvasDrawEvent != null) this.OnCanvasDrawEvent(HDC, ZoomX, ZoomY, CanvasHandle);
            if (this.PLC_01_繪製影像.Bool)
            {
                for (int i = 0; i < this.List_H_Canvas.Count; i++)
                {
                    if ((int)this.List_H_Canvas[i].Handle == CanvasHandle)
                    {
                        this.List_H_Canvas[i].SetImageSize(this.AxImageBW8.ImageWidth, this.AxImageBW8.ImageHeight);
                        this.DrawImage(HDC, ZoomX, ZoomY);
                    }
                    
                }
                
            }
        }

        #region Funtion
        public void SetSurfacePtr(int Width, int Height, long SurfacePtr)
        {
            this.AxImageBW8.SetSurfacePtr(Width, Height, SurfacePtr);
        }
        public void DrawImage(long HDC, float ZoomX, float ZoomY)
        {
            this.AxImageBW8.DrawImage(HDC, ZoomX, ZoomY, 0, 0);
        }
        public bool LoadImage(string filename)
        {
            bool flag = this.AxImageBW8.LoadFile(filename);
            if(flag)
            {
                this.PLC_01_ImageWidth.Value = this.AxImageBW8.ImageWidth;
                this.PLC_02_ImageHeight.Value = this.AxImageBW8.ImageHeight;
            }
            return flag;
        }
        public bool SaveImage(string filename)
        {
            return this.AxImageBW8.SaveFile(filename, AxOvkBase.TxAxImageType.AX_IMAGE_FILE_TYPE_GREYLEVEL_BMP);
        }
        #endregion

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
    }
}
