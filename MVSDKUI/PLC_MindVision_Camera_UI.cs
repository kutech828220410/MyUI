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
using MyUI;
namespace MVSDKUI
{
    [Designer(typeof(ComponentSet.JLabelExDesigner))]
    public partial class PLC_MindVision_Camera_UI : UserControl
    {
        #region Event
        public event MindVision_Camera_UI.OnSufaceDrawEventHandler OnSufaceDrawEvent
        {
            add
            {
                this.Camera.OnSufaceDrawEvent += value;
            }
            remove
            {
                this.Camera.OnSufaceDrawEvent -= value;
            }
        }

        #endregion
        #region 參數設定
        [ReadOnly(false), Browsable(true), Category("屬性"), Description(""), DefaultValue("")]
        public string CameraName
        {
            get
            {
                return this.Camera.CameraName;
            }
            set
            {
                this.Camera.CameraName = value;
            }
        }
        private bool _Show_Canvas = true;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool Show_Canvas
        {
            get
            {
                return _Show_Canvas;
            }
            set
            {
                this.Camera.Visible = value;
                _Show_Canvas = value;
            }
        }
        private bool _Show_Control = true;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public bool Show_Control
        {
            get
            {
                return _Show_Control;
            }
            set
            {
                this.panel_Control.Visible = value;
                _Show_Control = value;
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
        private MVSDKUI.MindVision_Camera_UI.enum_Rotate _旋轉角度 = MindVision_Camera_UI.enum_Rotate.Deg0;
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public MVSDKUI.MindVision_Camera_UI.enum_Rotate 旋轉角度
        {
            get
            {
                return _旋轉角度;
            }
            set
            {
                _旋轉角度 = value;
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
        private PLC_Device PLC_Device_00_EISutter = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _00_EISutter
        {
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z" || temp == "K") divice_OK = true;
                }
                if (!divice_OK) return;
                this.PLC_Device_00_EISutter.SetAdress(value);
            }
            get
            {
                return this.PLC_Device_00_EISutter.GetAdress();
            }
        }

        private PLC_Device PLC_Device_01_VGain = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _01_VGain
        {
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z" || temp == "K") divice_OK = true;
                }
                if (!divice_OK) return;
                this.PLC_Device_01_VGain.SetAdress(value);
            }
            get
            {
                return this.PLC_Device_01_VGain.GetAdress();
            }
        }

        private PLC_Device PLC_Device_02_Sharpness = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _02_Sharpness
        {
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z" || temp == "K") divice_OK = true;
                }
                if (!divice_OK) return;
                this.PLC_Device_02_Sharpness.SetAdress(value);
            }
            get
            {
                return this.PLC_Device_02_Sharpness.GetAdress();
            }
        }

        private PLC_Device PLC_Device_10_取像時間 = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _10_取像時間
        {
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z" || temp == "K") divice_OK = true;
                }
                if (!divice_OK) return;
                this.PLC_Device_10_取像時間.SetAdress(value);
            }
            get
            {
                return this.PLC_Device_10_取像時間.GetAdress();
            }
        }

        private PLC_Device PLC_Device_20_ActiveSurfaceHandle = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("數值位置"), Description(""), DefaultValue("")]
        public string _20_ActiveSurfaceHandle
        {
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "R" || temp == "D" || temp == "F" || temp == "Z" || temp == "K") divice_OK = true;
                }
                if (!divice_OK) return;
                this.PLC_Device_20_ActiveSurfaceHandle.SetAdress(value);
            }
            get
            {
                return this.PLC_Device_20_ActiveSurfaceHandle.GetAdress();
            }
        }
        #endregion
        #region 旗標位置
        private PLC_Device PLC_Device_00_IsConneted = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public string _00_IsConneted
        {
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }
                if (!divice_OK) return;
                this.PLC_Device_00_IsConneted.SetAdress(value);
            }
            get
            {
                return this.PLC_Device_00_IsConneted.GetAdress();
            }
        }
        private PLC_Device PLC_Device_01_Open = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public string _01_Open
        {
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }
                if (!divice_OK) return;
                this.PLC_Device_01_Open.SetAdress(value);
            }
            get
            {
                return this.PLC_Device_01_Open.GetAdress();
            }
        }

        private PLC_Device PLC_Device_10_READY = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public string _10_READY
        {
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }
                if (!divice_OK) return;
                this.PLC_Device_10_READY.SetAdress(value);
            }
            get
            {
                return this.PLC_Device_10_READY.GetAdress();
            }
        }
        private PLC_Device PLC_Device_11_TRIGGER = new PLC_Device("");
        [ReadOnly(false), Browsable(true), Category("旗標位置"), Description(""), DefaultValue("")]
        public string _11_TRIGGER
        {
            set
            {
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "X" || temp == "Y" || temp == "M" || temp == "S" || temp == "T") divice_OK = true;
                }
                if (!divice_OK) return;
                this.PLC_Device_11_TRIGGER.SetAdress(value);
            }
            get
            {
                return this.PLC_Device_11_TRIGGER.GetAdress();
            }
        }
        #endregion
        [ReadOnly(false), Browsable(false)]
        public long Frame
        {
            get
            {
                return this.Camera.Frame;
            }
        }
        [ReadOnly(false), Browsable(false)]
        public double EIShuter
        {
            get
            {
                return this.Camera.EIShuter;
            }
            set
            {
                this.Camera.EIShuter = value;
            }
        }
        [ReadOnly(false), Browsable(false)]
        public int VGain
        {
            get
            {
                return this.Camera.VGain;
            }
            set
            {
                this.Camera.VGain = value;
            }
        }
        [ReadOnly(false), Browsable(false)]
        public int Sharpness
        {
            get
            {
                return this.Camera.Sharpness;
            }
            set
            {
                this.Camera.Sharpness = value;
            }
        }
        [ReadOnly(false), Browsable(false)]
        public bool StreamIsSuspend
        {
            set
            {
                this.Camera.StreamIsSuspend = value;
            }
            get
            {
                return this.Camera.StreamIsSuspend;
            }
        }
        [ReadOnly(false), Browsable(false)]
        public long ActiveSurfaceHandle
        {
            get
            {
                return this.Camera.ActiveSurfaceHandle;
            }
        }
        [ReadOnly(false), Browsable(false)]
        public int ImageWidth
        {
            get
            {
                return this.Camera.ImageWidth;
            }
        }
        [ReadOnly(false), Browsable(false)]
        public int ImageHeight
        {
            get
            {
                return this.Camera.ImageHeight;
            }
        }

        private LowerMachine PLC;
        private Form Activeform;
        private MyThread MyThread_Snap;
        private bool flag_Init = false;

        public PLC_MindVision_Camera_UI()
        {
            InitializeComponent();
        }

        public void Run(Form form, LowerMachine pLC)
        {
            this.PLC = pLC;
            this.Activeform = form;
            this.Init();
            if (this.CycleTime >= 0)
            {
                this.MyThread_Snap = new MyThread(this.Activeform);
                this.MyThread_Snap.Add_Method(this.Method);
                this.MyThread_Snap.AutoRun(true);
                this.MyThread_Snap.SetSleepTime(this.CycleTime);
                this.MyThread_Snap.Trigger();
            }
        }

        public void Run()
        {
            if (this.PLC != null && this.Activeform != null)
            {
                this.PLC_Device_00_IsConneted.Bool = this.Camera.IsPortCreated;
                this.PLC_Device_20_ActiveSurfaceHandle.DoubleValue = this.ActiveSurfaceHandle;

                if (this.PLC_Device_01_Open.Bool)
                {
                    if (!this.PLC_Device_00_IsConneted.Bool)
                    {

                    }
                    this.PLC_Device_01_Open.Bool = false;
                }

            }
        }
        public void Method()
        {
            this.Run();
            this.sub_Program_Method_Snap();
        }
        private void Init()
        {
            if (!this.flag_Init)
            {
                this.PLC_Device_00_IsConneted.Bool = false;
                this.PLC_Device_00_IsConneted.SetComment(string.Format("{0} : IsConneted", this.CameraName));
                this.plC_Button_IsConneted.Load_PLC_Device(this.PLC_Device_00_IsConneted);
                this.plC_Button_IsConneted.Run(this.PLC);

                this.PLC_Device_01_Open.Bool = false;
                this.PLC_Device_01_Open.SetComment(string.Format("{0} : Open", this.CameraName));

                this.PLC_Device_10_READY.Bool = false;
                this.PLC_Device_10_READY.SetComment(string.Format("{0} : READY", this.CameraName));
                this.plC_Button_READY.Load_PLC_Device(this.PLC_Device_10_READY);
                this.plC_Button_READY.Run(this.PLC);

                this.PLC_Device_11_TRIGGER.Bool = false;
                this.PLC_Device_11_TRIGGER.SetComment(string.Format("{0} : TRIGGER", this.CameraName));
                this.plC_Button_TRIGGER.Load_PLC_Device(this.PLC_Device_11_TRIGGER);
                this.plC_Button_TRIGGER.Run(this.PLC);

                this.PLC_Device_00_EISutter.SetComment(string.Format("{0} : EISutter", this.CameraName));
                this.plC_TrackBarHorizontal_EISutter.Load_PLC_Device(this.PLC_Device_00_EISutter);
                this.plC_TrackBarHorizontal_EISutter.Run(this.PLC);

                this.PLC_Device_01_VGain.SetComment(string.Format("{0} : VGain", this.CameraName));
                this.plC_TrackBarHorizontal_VGain.Load_PLC_Device(this.PLC_Device_01_VGain);
                this.plC_TrackBarHorizontal_VGain.Run(this.PLC);

                this.PLC_Device_02_Sharpness.SetComment(string.Format("{0} : Sharpness", this.CameraName));
                this.plC_TrackBarHorizontal_Sharpness.Load_PLC_Device(this.PLC_Device_02_Sharpness);
                this.plC_TrackBarHorizontal_Sharpness.Run(this.PLC);

                this.PLC_Device_10_取像時間.SetComment(string.Format("{0} : 取像時間", this.CameraName));
                this.plC_NumBox_取像時間.Load_PLC_Device(this.PLC_Device_10_取像時間);
                this.plC_NumBox_取像時間.Run(this.PLC);

                this.PLC_Device_20_ActiveSurfaceHandle.SetComment(string.Format("{0} : ActiveSurfaceHandle", this.CameraName));
                this.plC_NumBox_ActiveSurfaceHandle.Load_PLC_Device(this.PLC_Device_20_ActiveSurfaceHandle);
                this.plC_NumBox_ActiveSurfaceHandle.Run(this.PLC);

                if (this.Camera.Init(this.Activeform))
                {
                    this.Camera.SnapAndWait(false);
                    this.Camera.Rotate = this.旋轉角度;
                    this.Camera.HorzFlip = this.水平翻轉;
                    this.Camera.VertFlip = this.垂直翻轉;
                }

                this.flag_Init = true;
            }
        }

        #region Function
        public void Set_Config()
        {
            this.EIShuter = this.PLC_Device_00_EISutter.Value;
            this.VGain = this.PLC_Device_01_VGain.Value;
            this.Sharpness = this.PLC_Device_02_Sharpness.Value;
        }
        public void Snap()
        {
            this.Camera.Snap();
        }
        public int SnapAndWait(bool MessageBoxShow)
        {
            return this.SnapAndWait(3000, MessageBoxShow);
        }
        public int SnapAndWait()
        {
            return this.SnapAndWait(3000, true);
        }
        public int SnapAndWait(int TimeOut, bool MessageBoxShow)
        {
            this.EIShuter = this.PLC_Device_00_EISutter.Value;
            this.VGain = this.PLC_Device_01_VGain.Value;
            this.Sharpness = this.PLC_Device_02_Sharpness.Value;
            return this.Camera.SnapAndWait(TimeOut, MessageBoxShow);
        }
        public void Set_Trigger_Mode(PLC_MindVision_Camera_UI Trigger_Mode)
        {
            this.Set_Trigger_Mode(Trigger_Mode);
        }
        #endregion
        #region Method_Snap
        double cnt_Program_Method_Snap_StartTime;
        int cnt_Program_Method_Snap = 65534;
        void sub_Program_Method_Snap()
        {
            if (cnt_Program_Method_Snap == 65534)
            {
                this.PLC_Device_10_READY.Bool = true;
                this.PLC_Device_11_TRIGGER.Bool = false;
                cnt_Program_Method_Snap = 65535;
            }
            if (cnt_Program_Method_Snap == 65535) cnt_Program_Method_Snap = 1;
            if (cnt_Program_Method_Snap == 1) cnt_Program_Method_Snap_檢查按下(ref cnt_Program_Method_Snap);
            if (cnt_Program_Method_Snap == 2) cnt_Program_Method_Snap_初始化(ref cnt_Program_Method_Snap);
            if (cnt_Program_Method_Snap == 3) cnt_Program_Method_Snap_取像一次(ref cnt_Program_Method_Snap);
            if (cnt_Program_Method_Snap == 4) cnt_Program_Method_Snap_等待取像完成(ref cnt_Program_Method_Snap);
            if (cnt_Program_Method_Snap == 5) cnt_Program_Method_Snap = 65500;
            if (cnt_Program_Method_Snap > 1) cnt_Program_Method_Snap_ReadyON_取消(ref cnt_Program_Method_Snap);

            if (cnt_Program_Method_Snap == 65500)
            {
                this.PLC_Device_10_READY.Bool = true;
                if (!this.checkBox_LIVE.Checked) this.PLC_Device_11_TRIGGER.Bool = false;
                cnt_Program_Method_Snap = 65535;
            }
        }
        void cnt_Program_Method_Snap_檢查按下(ref int cnt)
        {
            if (PLC_Device_11_TRIGGER.Bool) cnt++;
        }
        void cnt_Program_Method_Snap_ReadyON_取消(ref int cnt)
        {
            if (PLC_Device_10_READY.Bool) cnt = 65500;
        }
        void cnt_Program_Method_Snap_初始化(ref int cnt)
        {
            this.PLC_Device_10_READY.Bool = false;
            this.cnt_Program_Method_Snap_StartTime = this.Camera.CurrentTime;
            this.EIShuter = this.PLC_Device_00_EISutter.Value;
            this.VGain = this.PLC_Device_01_VGain.Value;
            this.Sharpness = this.PLC_Device_02_Sharpness.Value;
            cnt++;
        }
        void cnt_Program_Method_Snap_取像一次(ref int cnt)
        {
            this.Camera.SnapAndWait();
            cnt++;
        }
        void cnt_Program_Method_Snap_等待取像完成(ref int cnt)
        {
            this.PLC_Device_10_取像時間.Value = (int)(this.Camera.CurrentTime - this.cnt_Program_Method_Snap_StartTime);
            cnt++;
        }
        #endregion

    }
}
