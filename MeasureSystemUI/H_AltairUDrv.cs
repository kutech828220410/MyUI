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
    public partial class H_AltairUDrv : UserControl
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
        private MyThread MyThread_Snap;
        private PLC_Method PLC_Method_Snap;
        private AxAxAltairUDrv.AxAxAltairU AxAltairU;
        private Form Activeform;
        private LowerMachine PLC;
        private bool flag_Init = false;
        private bool flag_SnapFinished = false;
        #region Event
        public delegate void OnSurfaceFilledEventHandler(long SurfaceHandle);
        public event OnSurfaceFilledEventHandler OnSurfaceFilledEvent;
        #endregion
        #region 參數設定
        private bool _序號鎖住 = true;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool 序號鎖住
        {
            get
            {
                return _序號鎖住;
            }
            set
            {
                plC_NumBox_序號.ReadOnly = value;
                if(!value)
                {
                    plC_NumBox_序號.顯示螢幕小鍵盤 = true;
                }
                _序號鎖住 = value;
            }
        }
        private bool _水平翻轉 = false;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool 水平翻轉
        {
            get
            {
                return _水平翻轉;
            }
            set
            {
                _水平翻轉 = value;
            }
        }
        private bool _垂直翻轉 = false;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool 垂直翻轉
        {
            get
            {
                return _垂直翻轉;
            }
            set
            {
                _垂直翻轉 = value;
            }
        }
        private bool _長曝光模式 = false;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool 長曝光模式
        {
            get
            {
                return _長曝光模式;
            }
            set
            {
                _長曝光模式 = value;
            }
        }
        private bool _亮度補償 = false;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool 亮度補償
        {
            get
            {
                return _亮度補償;
            }
            set
            {
                _亮度補償 = value;
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
        #endregion
        #region 數值位置
        private string __00_序號 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _00_序號
        {
            get { return __00_序號; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z" || temp == "K") divice_OK = true;
                }

                if (divice_OK) __00_序號 = value;
                else __00_序號 = "";
            }
        }

        private string __01_ActiveSurfaceHandle = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _01_ActiveSurfaceHandle
        {
            get { return __01_ActiveSurfaceHandle; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z") divice_OK = true;
                }

                if (divice_OK) __01_ActiveSurfaceHandle = value;
                else __01_ActiveSurfaceHandle = "";
            }
        }

        private string __02_光源亮度 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _02_光源亮度
        {
            get { return __02_光源亮度; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z" || temp == "K") divice_OK = true;
                }

                if (divice_OK) __02_光源亮度 = value;
                else __02_光源亮度 = "";
            }
        }

        private string __03_電子快門 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _03_電子快門
        {
            get { return __03_電子快門; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z" || temp == "K") divice_OK = true;
                }

                if (divice_OK) __03_電子快門 = value;
                else __03_電子快門 = "";
            }
        }

        private string __04_視訊增益 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _04_視訊增益
        {
            get { return __04_視訊增益; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z" || temp == "K") divice_OK = true;
                }

                if (divice_OK) __04_視訊增益 = value;
                else __04_視訊增益 = "";
            }
        }

        private string __10_取像時間 = "";
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _10_取像時間
        {
            get { return __10_取像時間; }
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z" || temp == "K") divice_OK = true;
                }

                if (divice_OK) __10_取像時間 = value;
                else __10_取像時間 = "";
            }
        }
        #endregion
        #region 旗標位置
        private string __00_相機初始化 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _00_相機初始化
        {
            get { return __00_相機初始化; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __00_相機初始化 = value;
                else __00_相機初始化 = "";
            }
        }

        private string __01_相機已建立 = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _01_相機已建立
        {
            get { return __01_相機已建立; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __01_相機已建立 = value;
                else __01_相機已建立 = "";
            }
        }

        private string __02_TRIGGER = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _02_TRIGGER
        {
            get { return __02_TRIGGER; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __02_TRIGGER = value;
                else __02_TRIGGER = "";
            }
        }

        private string __03_READY = "";
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public virtual string _03_READY
        {
            get { return __03_READY; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }

                if (divice_OK) __03_READY = value;
                else __03_READY = "";
            }
        }
        #endregion

        public H_AltairUDrv()
        {
            InitializeComponent();

        }
     
        private PLC_Device PLC_00_相機初始化;
        private PLC_Device PLC_01_相機已建立;
        private PLC_Device PLC_02_TRIGGER;
        private PLC_Device PLC_03_READY;

        private PLC_Device PLC_00_序號;
        private PLC_Device PLC_01_ActiveSurfaceHandle;
        private PLC_Device PLC_02_光源亮度;
        private PLC_Device PLC_03_電子快門;
        private PLC_Device PLC_04_視訊增益;
        private PLC_Device PLC_10_取像時間;
        private void Init()
        {
            if (!flag_Init)
            {
                if (this._00_序號 != string.Empty)
                {
                    plC_NumBox_序號.寫入元件位置 = this._00_序號;
                    plC_NumBox_序號.讀取元件位置 = this._00_序號;
                    plC_NumBox_序號.Run(this.PLC);                
                    this.PLC_00_序號 = new PLC_Device(this._00_序號);

                    this.PLC_00_序號.SetComment(string.Format("{0} : 序號", this.Name));
                }
                if (this._01_ActiveSurfaceHandle != string.Empty)
                {
                    plC_NumBox_ActiveSurfaceHandle.寫入元件位置 = this._01_ActiveSurfaceHandle;
                    plC_NumBox_ActiveSurfaceHandle.讀取元件位置 = this._01_ActiveSurfaceHandle;
                    plC_NumBox_ActiveSurfaceHandle.Run(this.PLC);                 
                    this.PLC_01_ActiveSurfaceHandle = new PLC_Device(this._01_ActiveSurfaceHandle);
                    this.PLC_01_ActiveSurfaceHandle.DoubleValue = 0;

                    this.PLC_01_ActiveSurfaceHandle.SetComment(string.Format("{0} : ActiveSurfaceHandle", this.Name));

                }
                if (this._02_光源亮度 != string.Empty)
                {
                    plC_TrackBarHorizontal_光源亮度.寫入元件位置 = this._02_光源亮度;
                    plC_TrackBarHorizontal_光源亮度.讀取元件位置 = this._02_光源亮度;
                    plC_TrackBarHorizontal_光源亮度.Run(this.PLC);      
                    this.PLC_02_光源亮度 = new PLC_Device(this._02_光源亮度);

                    this.PLC_02_光源亮度.SetComment(string.Format("{0} : 光源亮度", this.Name));
                }
                if (this._03_電子快門 != string.Empty)
                {
                    plC_TrackBarHorizontal_電子快門.寫入元件位置 = this._03_電子快門;
                    plC_TrackBarHorizontal_電子快門.讀取元件位置 = this._03_電子快門;
                    plC_TrackBarHorizontal_電子快門.Run(this.PLC);      
                    this.PLC_03_電子快門 = new PLC_Device(this._03_電子快門);

                    this.PLC_03_電子快門.SetComment(string.Format("{0} : 電子快門", this.Name));

                }
                if (this._04_視訊增益 != string.Empty)
                {
                    plC_TrackBarHorizontal_視訊增益.寫入元件位置 = this._04_視訊增益;
                    plC_TrackBarHorizontal_視訊增益.讀取元件位置 = this._04_視訊增益;
                    plC_TrackBarHorizontal_視訊增益.Run(this.PLC);   
                    this.PLC_04_視訊增益 = new PLC_Device(this._04_視訊增益);

                    this.PLC_04_視訊增益.SetComment(string.Format("{0} : 視訊增益", this.Name));

                }
                if (this._10_取像時間 != string.Empty)
                {
                    plC_NumBox_取像時間.寫入元件位置 = this._10_取像時間;
                    plC_NumBox_取像時間.讀取元件位置 = this._10_取像時間;
                    plC_NumBox_取像時間.Run(this.PLC);      
                    this.PLC_10_取像時間 = new PLC_Device(this._10_取像時間);

                    this.PLC_10_取像時間.SetComment(string.Format("{0} : 取像時間", this.Name));

                }

                if (this._02_TRIGGER != string.Empty)
                {
                    plC_Button_TRIGGER.寫入元件位置 = this._02_TRIGGER;
                    plC_Button_TRIGGER.讀取元件位置 = this._02_TRIGGER;
                    plC_Button_TRIGGER.Run(this.PLC);              
                    this.PLC_02_TRIGGER = new PLC_Device(this._02_TRIGGER);

                    this.PLC_02_TRIGGER.SetComment(string.Format("{0} : TRIGGER", this.Name));

                }
                if (this._03_READY != string.Empty)
                {
                    plC_Button_READY.寫入元件位置 = this._03_READY;
                    plC_Button_READY.讀取元件位置 = this._03_READY;
                    plC_Button_READY.Run(this.PLC);         
                    this.PLC_03_READY = new PLC_Device(this._03_READY);

                    this.PLC_03_READY.SetComment(string.Format("{0} : READY", this.Name));

                }
                if (this._00_相機初始化 != string.Empty)
                {
                    plC_Button_相機初始化.寫入元件位置 = this._00_相機初始化;
                    plC_Button_相機初始化.讀取元件位置 = this._00_相機初始化;
                    plC_Button_相機初始化.Run(this.PLC);     
                    this.PLC_00_相機初始化 = new PLC_Device(this._00_相機初始化);

                    this.PLC_00_相機初始化.SetComment(string.Format("{0} : 相機初始化", this.Name));

                }
                if (this._01_相機已建立 != string.Empty)
                {
                    plC_Button_相機已建立.寫入元件位置 = this._01_相機已建立;
                    plC_Button_相機已建立.讀取元件位置 = this._01_相機已建立;
                    plC_Button_相機已建立.Run(this.PLC);
                    this.PLC_01_相機已建立 = new PLC_Device(this._01_相機已建立);
                    this.PLC_01_相機已建立.Bool = false;
                    if (this._03_READY != string.Empty) this.PLC_03_READY.Bool = true;

                    this.PLC_01_相機已建立.SetComment(string.Format("{0} : 相機已建立", this.Name));

                }
                this.Invoke(new Action(delegate
                {
                    if (this.AxAltairU == null) this.AxAltairU = new AxAxAltairUDrv.AxAxAltairU();
                    this.AxAltairU.Dock = DockStyle.Fill;

                    ((System.ComponentModel.ISupportInitialize)(this.AxAltairU)).BeginInit();
                    panel_CCD.Controls.Add(this.AxAltairU);
                    ((System.ComponentModel.ISupportInitialize)(this.AxAltairU)).EndInit();


                    this.checkBox_水平翻轉.Checked = this.水平翻轉;
                    this.checkBox_垂直翻轉.Checked = this.垂直翻轉;
                    this.checkBox_長曝光模式.Checked = this.長曝光模式;
                    this.checkBox_亮度補償.Checked = this.亮度補償;

                }));
            
                Activeform.FormClosed += H_AltairUDrv_FormClosing;
                this.AxAltairU.HorzFlip = this.水平翻轉;
                this.AxAltairU.VertFlip = this.垂直翻轉;
                this.AxAltairU.LongExpMode = this.長曝光模式;
                this.AxAltairU.LowLightMode = this.亮度補償;


                this.AxAltairU.WatchDogTimerState = AxAltairUDrv.TxAxauWatchDogTimerState.AXAU_WATCH_DOG_TIMER_STATE_ENABLED;
                this.AxAltairU.ChannelState = AxAltairUDrv.TxAxauChannelState.AXAU_CHANNELSTATE_STANDBY;
                this.AxAltairU.OnSurfaceFilled += AxAltairU_OnSurfaceFilled;
      
                flag_Init = true;
            }
        }
        public void Run(Form Activeform, LowerMachine pLC)
        {
            this.Activeform = Activeform;
            this.PLC = pLC;

            this.PLC_Method_Snap = new PLC_Method(this._02_TRIGGER, this._03_READY);
            this.PLC_Method_Snap.AddStepMethod(1, Method_Snap_取像一次);
            this.PLC_Method_Snap.AddStepMethod(2, Method_Snap_等待取像完成);
            this.PLC_Method_Snap.SetFinishMethod(Method_Snap_Finished);

            this.MyThread_Snap = new MyThread(Activeform);
            this.MyThread_Snap.Add_Method(this.Run);
            this.MyThread_Snap.Add_Method(this.PLC_Method_Snap.GetMainMethod());
            this.MyThread_Snap.AutoRun(true);
            this.MyThread_Snap.SetSleepTime(this.CycleTime);
            this.MyThread_Snap.Trigger();
        }
        public void AddMethod(Basic.MyThread.MethodDelegate method)
        {
            this.MyThread_Snap.Add_Method(method);
        }
        public void Run()
        {
            if (this.PLC != null && this.Activeform != null)
            {
                this.Init();
                if (this.PLC_00_相機初始化.Bool)
                {
                    this.AxAltairU.SerialNumber = this.PLC_00_序號.Value;
                    if (this.AxAltairU.CreateChannel())
                    {
                        this.PLC_01_相機已建立.Bool = true;
                    }
                    this.PLC_00_相機初始化.Bool = false;
                }
            }
        }

        public void Snap()
        {
            this.AxAltairU.DacCh1Value = this.PLC_02_光源亮度.Value;
            this.AxAltairU.DacCh1Switch = true;
            this.AxAltairU.ElShutter = this.PLC_03_電子快門.Value;
            this.AxAltairU.VGain = this.PLC_04_視訊增益.Value;
            this.AxAltairU.Snap(1);
              
        }


        private void H_AltairUDrv_FormClosing(object sender, EventArgs e)
        {
            if (AxAltairU.IsPortCreated) AxAltairU.DestroyChannel();
        }
        private void H_AltairUDrv_Load(object sender, EventArgs e)
        {
        }
        private void AxAltairU_OnSurfaceFilled(object sender, AxAxAltairUDrv.IAxAltairUEvents_OnSurfaceFilledEvent e)
        {          
            if(OnSurfaceFilledEvent != null)this.OnSurfaceFilledEvent(e.surfaceHandle);
            this.AxAltairU.DacCh1Switch = false;
            this.PLC_01_ActiveSurfaceHandle.DoubleValue = AxAltairU.ActiveSurfaceHandle;
            this.flag_SnapFinished = true;
        }
        private void checkBox_水平翻轉_CheckedChanged(object sender, EventArgs e)
        {
            this.水平翻轉 = checkBox_水平翻轉.Checked;
            this.AxAltairU.HorzFlip = this.水平翻轉;   
        }

        private void checkBox_垂直翻轉_CheckedChanged(object sender, EventArgs e)
        {
            this.垂直翻轉 = checkBox_垂直翻轉.Checked;
            this.AxAltairU.VertFlip = this.垂直翻轉;
        }

        private void checkBox_長曝光模式_CheckedChanged(object sender, EventArgs e)
        {
            this.長曝光模式 = checkBox_長曝光模式.Checked;
            this.AxAltairU.LongExpMode = this.長曝光模式;        
        }

        private void checkBox_亮度補償_CheckedChanged(object sender, EventArgs e)
        {
            this.亮度補償 = checkBox_亮度補償.Checked;
            this.AxAltairU.LowLightMode = this.亮度補償;
        }
        #region Method_Snap

        void Method_Snap_取像一次(ref int cnt)
        {
            flag_SnapFinished = false;
            this.Snap();
            cnt++;
        }
        void Method_Snap_等待取像完成(ref int cnt)
        {
            if (this.flag_SnapFinished)
            {
                cnt = 65500;
            }
        }
        void Method_Snap_Finished()
        {
            this.PLC_10_取像時間.Value = (int)this.PLC_Method_Snap.GetCycleTime();
        }
        #endregion


    }
}
