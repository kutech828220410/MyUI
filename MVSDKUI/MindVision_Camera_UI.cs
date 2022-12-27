using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using Basic;
using CameraHandle = System.Int32;
using MvApi = MVSDK.MvApi;
using MVSDK;
namespace MVSDKUI
{
    [Designer(typeof(ComponentSet.JLabelExDesigner))]
    public partial class MindVision_Camera_UI : UserControl
    {
        #region Event
        public delegate void OnSufaceDrawEventHandler(long ActiveSurfaceHandle, int ImageWidth, int ImageHeight);
        public event OnSufaceDrawEventHandler OnSufaceDrawEvent;
        #endregion

        #region 自訂屬性
        private string _CameraName = "CCD00";
        [ReadOnly(false), Browsable(true), Category("屬性"), Description(""), DefaultValue("")]
        public string CameraName
        {
            get
            {
                return this._CameraName;
            }
            set
            {
                this._CameraName = value;
            }
        }

        public enum enum_ImageDepth : int
        {
            _8Bit = 1, _24Bit = 3
        }
        private enum_ImageDepth _enum_ImageDepth = MindVision_Camera_UI.enum_ImageDepth._8Bit;
        [ReadOnly(false), Browsable(true), Category("屬性"), Description(""), DefaultValue("")]
        public enum_ImageDepth ImageDepth
        {
            get
            {
                return this._enum_ImageDepth;
            }
            set
            {
                this._enum_ImageDepth = value;
            }
        }

        #endregion

        private int _CameraHandle = 0;
        [ReadOnly(false), Browsable(false)]
        public int CameraHandle
        {
            get
            {
                return this._CameraHandle;
            }
            private set
            {
                this._CameraHandle = value;
            }
        }
        private long _ActiveSurfaceHandle = 0;
        [ReadOnly(false), Browsable(false)]
        public long ActiveSurfaceHandle
        {
            get
            {
                return this._ActiveSurfaceHandle;
            }
            private set
            {
                this._ActiveSurfaceHandle = value;
            }
        }
        private int _ImageWidth = 0;
        [ReadOnly(false), Browsable(false)]
        public int ImageWidth
        {
            get
            {
                return this._ImageWidth;
            }
            private set
            {
                this._ImageWidth = value;
            }
        }
        private int _ImageHeight = 0;
        [ReadOnly(false), Browsable(false)]
        public int ImageHeight
        {
            get
            {
                return this._ImageHeight;
            }
            private set
            {
                this._ImageHeight = value;
            }
        }
        private bool _StreamIsSuspend = false;
        [ReadOnly(false), Browsable(false)]
        public bool StreamIsSuspend
        {
            get
            {
                return this._StreamIsSuspend;
            }
            set
            {
                this._StreamIsSuspend = value;
            }
        }
        private bool _IsPortCreated = false;
        [ReadOnly(false), Browsable(false)]
        public bool IsPortCreated
        {
            get
            {
                return this._IsPortCreated;
            }
            private set
            {
                this._IsPortCreated = value;
            }
        }
        private long _Frame = 0;
        [ReadOnly(false), Browsable(false)]
        public long Frame
        {
            get
            {
                return this._Frame;
            }
            private set
            {
                this._Frame = value;
            }
        }
        [ReadOnly(false), Browsable(false)]
        public enum Trigger_Mode : int
        {
            Soft = 1, External = 2
        }

        [ReadOnly(false), Browsable(false)]
        public double EIShuter
        {
            get
            {
                double exposurTime = 0;
                if (this.IsHandleCreated) MvApi.CameraGetExposureTime(this.CameraHandle, ref exposurTime);
                return exposurTime;
            }
            set
            {
                if(this.IsHandleCreated)
                {
                    MvApi.CameraSetExposureTime(this.CameraHandle, value);
                }
     
            }
        }
        [ReadOnly(false), Browsable(false)]
        public int VGain
        {
            get
            {
                int Contrast = 0;
                if (this.IsHandleCreated) MvApi.CameraGetContrast(this.CameraHandle, ref Contrast);
                Contrast -= 100;
                return Contrast;
            }
            set
            {
                if (value < -100) value = -100;
                if (value > 100) value = 100;
                value += 100;
                if (this.IsHandleCreated) MvApi.CameraSetContrast(this.CameraHandle, value);
            }
        }
        [ReadOnly(false), Browsable(false)]
        public int Sharpness
        {
            get
            {
                int sharpness = 0;
                if (this.IsHandleCreated) MvApi.CameraGetSharpness(this.CameraHandle, ref sharpness);
                return sharpness;
            }
            set
            {
                if (this.IsHandleCreated) MvApi.CameraSetSharpness(this.CameraHandle, value);
            }
        }
        [ReadOnly(false), Browsable(false)]
        public bool HorzFlip
        {
            get
            {
                uint pbEnable = 0;
                if (this.IsHandleCreated) MvApi.CameraGetMirror(this.CameraHandle, 0, ref pbEnable);
                return (pbEnable == 1);
            }
            set
            {
                if (this.IsHandleCreated) MvApi.CameraSetMirror(this.CameraHandle, 0, value ? (uint)1 : (uint)0);
            }
        }
        [ReadOnly(false), Browsable(false)]
        public bool VertFlip
        {
            get
            {
                uint pbEnable = 0;
                if (this.IsHandleCreated) MvApi.CameraGetMirror(this.CameraHandle, 1, ref pbEnable);
                return (pbEnable == 1);
            }
            set
            {
                if (this.IsHandleCreated) MvApi.CameraSetMirror(this.CameraHandle, 1, value ? (uint)1 : (uint)0);
            }
        }
        public enum enum_Rotate : int
        {
            Deg0, Deg90, Deg180, Deg270,
        }
        [ReadOnly(false), Browsable(false)]
        public enum_Rotate Rotate
        {
            get
            {
                int iRot = 0;
                if (this.IsHandleCreated) MvApi.CameraGetRotate(this.CameraHandle, out iRot);
                return (enum_Rotate)iRot;
            }
            set
            {
                if (this.IsHandleCreated) MvApi.CameraSetRotate(this.CameraHandle, (int)value);
            }
        }

        private Form ActiveForm;
        private Stopwatch stopwatch = new Stopwatch();
        private IntPtr ImageBufferPtr = IntPtr.Zero;
        private tSdkCameraCapbility CameraCapability;
        private bool ExitCaptureThread = false;
        private Thread CaptureThread;
        public double CurrentTime
        {
            get
            {
                return this.stopwatch.Elapsed.TotalMilliseconds;
            }
        }
        private CAMERA_SNAP_PROC m_CaptureCallback;
        private IntPtr m_iCaptureCallbackCtx;

        public MindVision_Camera_UI()
        {
            InitializeComponent();
        }
        private void CaptureThreadProc()
        {
            long handle;
            CameraSdkStatus eStatus;
            tSdkFrameHead FrameHead;
            IntPtr uRawBuffer;//rawbuffer由SDK内部申请。应用层不要调用delete之类的释放函数
            while (this.ExitCaptureThread == false)
            {
                //eStatus = MvApi.CameraGetImageBuffer(this.CameraHandle, out FrameHead, out uRawBuffer, 500);
                handle = (long)MvApi.CameraGetImageBufferEx(this.CameraHandle, ref _ImageWidth, ref _ImageHeight, 10);
                if (handle != 0)//如果是触发模式，则有可能超时
                {
                    //图像处理，将原始输出转换为RGB格式的位图数据，同时叠加白平衡、饱和度、LUT等ISP处理。
                    //MvApi.CameraImageProcess(this.CameraHandle, uRawBuffer, this.ImageBufferPtr, ref FrameHead);
                    ////叠加十字线、自动曝光窗口、白平衡窗口信息(仅叠加设置为可见状态的)。    
                    //MvApi.CameraImageOverlay(this.CameraHandle, this.ImageBufferPtr, ref FrameHead);
                    ////调用SDK封装好的接口，显示预览图像
                    //MvApi.CameraDisplayRGB24(this.CameraHandle, this.ImageBufferPtr, ref FrameHead);
                    //成功调用CameraGetImageBuffer后必须释放，下次才能继续调用CameraGetImageBuffer捕获图像。
                    // MvApi.CameraReleaseImageBuffer(this.CameraHandle, handle);
                    this.ActiveSurfaceHandle = (long)handle;
                    this.ImageWidth = _ImageWidth;
                    this.ImageHeight = _ImageHeight;
                    if (this.OnSufaceDrawEvent != null) this.OnSufaceDrawEvent(this.ActiveSurfaceHandle, this.ImageWidth, this.ImageHeight);
                    this.StreamIsSuspend = true;
                    this.Frame++;
                }
            }
        }
        public void ImageCaptureCallback(CameraHandle hCamera, IntPtr pFrameBuffer, ref tSdkFrameHead pFrameHead, IntPtr pContext)
        {
            //图像处理，将原始输出转换为RGB格式的位图数据，同时叠加白平衡、饱和度、LUT等ISP处理。
            MvApi.CameraImageProcess(hCamera, pFrameBuffer, ImageBufferPtr, ref pFrameHead);
            ////叠加十字线、自动曝光窗口、白平衡窗口信息(仅叠加设置为可见状态的)。   
            //MvApi.CameraImageOverlay(hCamera, ImageBufferPtr, ref pFrameHead);
            ////调用SDK封装好的接口，显示预览图像
            //MvApi.CameraDisplayRGB24(hCamera, ImageBufferPtr, ref pFrameHead);
            //MvApi.CameraFlipFrameBuffer(pFrameBuffer, ref pFrameHead, 1);

            this.ImageWidth = pFrameHead.iWidth;
            this.ImageHeight = pFrameHead.iHeight;
            this.ActiveSurfaceHandle = (long)ImageBufferPtr;

            if (this.OnSufaceDrawEvent != null) this.OnSufaceDrawEvent(this.ActiveSurfaceHandle, this.ImageWidth, this.ImageHeight);
            this.StreamIsSuspend = true;
            this.Frame++;

        }
        private void CameraGrabberFrameCallback(IntPtr Grabber, IntPtr pFrameBuffer, ref tSdkFrameHead pFrameHead, IntPtr Context)
        {
            this.ActiveSurfaceHandle = (long)pFrameBuffer;
            this.ImageWidth = pFrameHead.iWidth;
            this.ImageHeight = pFrameHead.iHeight;
            // if (this.OnSufaceDrawEvent != null) this.OnSufaceDrawEvent(this.ActiveSurfaceHandle, this.ImageWidth, this.ImageHeight);
            this.StreamIsSuspend = true;
            this.Frame++;
        }
        protected pfnCameraGrabberFrameCallback m_FrameCallback;
        protected IntPtr m_Grabber = IntPtr.Zero;
        protected tSdkCameraDevInfo m_DevInfo;
        #region Function
        public bool Init(Form form)
        {
            this.ActiveForm = form;
            this.stopwatch.Start();
            form.FormClosing += Form_FormClosing;
            #region 未選用程式
            //tSdkCameraDevInfo[] tSdkCameraDevInfos;
            //MvApi.CameraEnumerateDevice(out tSdkCameraDevInfos);
            //if (tSdkCameraDevInfos == null)
            //{
            //    MyMessageBox.ShowDialog("未搜尋到任何相機!");
            //    return false;
            //}
            //for (int i = 0; i < tSdkCameraDevInfos.Length; i++)
            //{
            //    string str_camName = MvApi.CStrToString(tSdkCameraDevInfos[i].acFriendlyName);
            //    if(str_camName == this.CameraName)
            //    {
            //        this._CameraHandle = (int)tSdkCameraDevInfos[i].uInstance;
            //        CameraSdkStatus cameraSdkStatus = CameraSdkStatus.CAMERA_AIA_ERROR;
            //        cameraSdkStatus = MvApi.CameraInitEx2(this.CameraName, out this._CameraHandle);
            //        if (cameraSdkStatus == CameraSdkStatus.CAMERA_STATUS_SUCCESS)
            //        {

            //            this.Invoke(new Action(delegate
            //            {
            //                MvApi.CameraDisplayInit(this.CameraHandle, PreviewBox.Handle);
            //                MvApi.CameraSetDisplaySize(this.CameraHandle, PreviewBox.Width, PreviewBox.Height);
            //                int tabindex = (int)emSdkPropSheetMask.PROP_SHEET_INDEX_TRIGGER_SET;
            //                MvApi.CameraCreateSettingPage(this.CameraHandle, this.Handle, this.CameraName, null, (IntPtr)null, (uint)(-1 & ~(1 << tabindex)));
            //            }));



            //            this.Set_Trigger_Mode(Trigger_Mode.External);

            //            MvApi.CameraSetAeState(this.CameraHandle, 0);
            //            if (this.ImageDepth == enum_ImageDepth._24Bit)
            //            {
            //                ImageBufferPtr = Marshal.AllocHGlobal(this.CameraCapability.sResolutionRange.iWidthMax * this.CameraCapability.sResolutionRange.iHeightMax * 3 + 1024);
            //                MvApi.CameraSetIspOutFormat(this.CameraHandle, (uint)MVSDK.emImageFormat.CAMERA_MEDIA_TYPE_OCCUPY24BIT);
            //            }
            //            else if (this.ImageDepth == enum_ImageDepth._8Bit)
            //            {
            //                ImageBufferPtr = Marshal.AllocHGlobal(this.CameraCapability.sResolutionRange.iWidthMax * this.CameraCapability.sResolutionRange.iHeightMax * 3 + 1024);
            //                MvApi.CameraSetIspOutFormat(this.CameraHandle, (uint)MVSDK.emImageFormat.CAMERA_MEDIA_TYPE_MONO8);
            //            }
            //            MvApi.CameraPlay(this.CameraHandle);

            //            //MvApi.CameraSetExtTrigSignalType(this.CameraHandle, 0);


            //            CAMERA_SNAP_PROC pCaptureCallOld = null;
            //            m_CaptureCallback = new CAMERA_SNAP_PROC(ImageCaptureCallback);
            //            MvApi.CameraSetCallbackFunction(this.CameraHandle, m_CaptureCallback, m_iCaptureCallbackCtx, ref pCaptureCallOld);



            //            this.ExitCaptureThread = false;
            //            //this.CaptureThread = new Thread(new ThreadStart(this.CaptureThreadProc));
            //            //this.CaptureThread.Start();
            //            this.IsPortCreated = true;
            //            return true;
            //        }
            //        else
            //        {
            //            MessageBox.Show(string.Format("相機初始化失敗({0})", this.CameraName));
            //            return false;
            //        }
            //    }
            //}
            //return false;
            #endregion
            if (CameraHandle != 0)
            {
                if (MvApi.CameraInitEx2(this.CameraName, out this._CameraHandle) == CameraSdkStatus.CAMERA_STATUS_SUCCESS)
                {
                    MvApi.CameraGetCapability(this.CameraHandle, out CameraCapability);


                    this.Invoke(new Action(delegate
                    {
                        MvApi.CameraDisplayInit(this.CameraHandle, PreviewBox.Handle);
                        MvApi.CameraSetDisplaySize(this.CameraHandle, PreviewBox.Width, PreviewBox.Height);
                        int tabindex = (int)emSdkPropSheetMask.PROP_SHEET_INDEX_TRIGGER_SET;
                        MvApi.CameraCreateSettingPage(this.CameraHandle, this.Handle, this.CameraName, null, (IntPtr)null, (uint)(-1 & ~(1 << tabindex)));
                    }));


                    this.Set_Trigger_Mode(Trigger_Mode.External);

                    MvApi.CameraSetAeState(this.CameraHandle, 0);
                    if (this.ImageDepth == enum_ImageDepth._24Bit)
                    {
                        ImageBufferPtr = Marshal.AllocHGlobal(this.CameraCapability.sResolutionRange.iWidthMax * this.CameraCapability.sResolutionRange.iHeightMax * 3 + 1024);
                        MvApi.CameraSetIspOutFormat(this.CameraHandle, (uint)MVSDK.emImageFormat.CAMERA_MEDIA_TYPE_OCCUPY24BIT);
                    }
                    else if (this.ImageDepth == enum_ImageDepth._8Bit)
                    {
                        ImageBufferPtr = Marshal.AllocHGlobal(this.CameraCapability.sResolutionRange.iWidthMax * this.CameraCapability.sResolutionRange.iHeightMax * 3 + 1024);
                        MvApi.CameraSetIspOutFormat(this.CameraHandle, (uint)MVSDK.emImageFormat.CAMERA_MEDIA_TYPE_MONO8);
                    }
                    MvApi.CameraPlay(this.CameraHandle);

                    //MvApi.CameraSetExtTrigSignalType(this.CameraHandle, 0);


                    //CAMERA_SNAP_PROC pCaptureCallOld = null;
                    //m_CaptureCallback = new CAMERA_SNAP_PROC(ImageCaptureCallback);
                    //MvApi.CameraSetCallbackFunction(this.CameraHandle, m_CaptureCallback, m_iCaptureCallbackCtx, ref pCaptureCallOld);



                    this.ExitCaptureThread = false;
                    this.CaptureThread = new Thread(new ThreadStart(this.CaptureThreadProc));
                    this.CaptureThread.Start();
                    this.IsPortCreated = true;
                    return true;
                }


                else
                {
                    MyMessageBox.ShowDialog($"相機初始化失敗{this.CameraName}");
                    return false;
                }
            }
            return false;

        }

        public void Snap()
        {
            this.Set_Trigger_Mode(Trigger_Mode.Soft);
            MvApi.CameraSoftTrigger(this.CameraHandle);
            // this.Set_Trigger_Mode(Trigger_Mode.External);
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
            long Frame_nuf = this.Frame;
            this.Set_Trigger_Mode(Trigger_Mode.Soft);
            MvApi.CameraSoftTrigger(this.CameraHandle);
            double StartTime = this.CurrentTime;

            while (true)
            {

                if (Frame_nuf != this.Frame)
                {
                    break;
                }
                if ((this.CurrentTime - StartTime) > TimeOut)
                {
                    if (MessageBoxShow) MessageBox.Show(string.Format("相機軟觸發逾時({0})", this.CameraName));
                    this.Set_Trigger_Mode(Trigger_Mode.External);
                    return -1;
                }
            }
            this.Set_Trigger_Mode(Trigger_Mode.External);
            return 0;
        }
        public void Set_Trigger_Mode(Trigger_Mode Trigger_Mode)
        {
            if (this.IsPortCreated)
            {
                MvApi.CameraSetTriggerMode(this.CameraHandle, (int)Trigger_Mode);
            }
        }
        #endregion

        private void timer_Tick(object sender, EventArgs e)
        {

        }
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ExitCaptureThread = true;
            if (this.CameraHandle > 0)
            {
                MvApi.CameraUnInit(this.CameraHandle);
                this.CameraHandle = 0;
            }
        }
    }
}
