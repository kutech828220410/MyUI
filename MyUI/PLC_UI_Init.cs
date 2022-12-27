using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SLDUI;
using LeadShineUI;
using PortControl;
using System.Runtime.InteropServices;
using MeasureSystemUI;
using LightControlUI;
using ZealTechUI;
using HCGUI;
namespace MyUI
{
     [System.Drawing.ToolboxBitmap(typeof(PLC_UI_Init), "PLC_UI_Init.bmp")]
    public partial class PLC_UI_Init : UserControl
    {
        public delegate void UI_Finished_EventHandler();
        public event UI_Finished_EventHandler UI_Finished_Event;

        public bool Init_Finish = false;
        Basic.MyThread Thread_DevicePoolling;
        Basic.MyThread Thread_ButtonUI;
        Basic.MyThread Thread_NumBoxUI;
        private PLC_ScreenPage pLC_ScreenPage;
        #region 自訂屬性
        private int _掃描速度 = 1;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int 掃描速度
        {
            get { return _掃描速度; }
            set
            {
                _掃描速度 = value;
            }
        }
        private int _開機延遲 = 0;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int 開機延遲
        {
            get { return _開機延遲; }
            set
            {
                _開機延遲 = value;
            }
        }
        private bool _光道視覺元件初始化 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 光道視覺元件初始化
        {
            get { return _光道視覺元件初始化; }
            set
            {
                _光道視覺元件初始化 = value;
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
        private bool _邁得威視元件初始化 = false;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 邁得威視元件初始化
        {
            get { return _邁得威視元件初始化; }
            set
            {
                _邁得威視元件初始化 = value;
            }
        }


        private bool _全螢幕顯示 = true;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 全螢幕顯示
        {
            get { return _全螢幕顯示; }
            set
            {
                _全螢幕顯示 = value;
            }
        }
        private bool _起始畫面顯示 = true;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public bool 起始畫面顯示
        {
            get { return _起始畫面顯示; }
            set
            {
                _起始畫面顯示 = value;
            }
        }
        private Image _起始畫面背景 = global::MyUI.Resource1.logo_1;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue(1)]
        public Image 起始畫面背景
        {
            get { return _起始畫面背景; }
            set
            {
                _起始畫面背景 = value;

            }
        }
        private string _起始畫面標題內容 = "鴻森整合機電有限公司";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 起始畫面標題內容
        {
            get { return _起始畫面標題內容; }
            set
            {
                _起始畫面標題內容 = value;
            }
        }
        private Font _起始畫面標題字體 = new System.Drawing.Font("標楷體", 20F, System.Drawing.FontStyle.Bold);
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Font 起始畫面標題字體
        {
            get { return _起始畫面標題字體; }
            set
            {
                _起始畫面標題字體 = value;

            }
        }
        #endregion
        Form form;
        LadderUI.LowerMachine_Panel lowerMachine_Panel;
        LadderConnection.LowerMachine PLC;
        public PLC_UI_Init()
        {
            InitializeComponent();
        }

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int screensize);
        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, int hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);
        private const int HWND_TOP = 0; //{在前面}
        private const int HWND_BOTTOM = 1; //{在后面}
        //uFlags 参数可选值:
        const uint SWP_NOMOVE = 0X2;
        const uint SWP_NOSIZE = 1;
        const uint SWP_NOZORDER = 0X4;
        const uint SWP_SHOWWINDOW = 0x0040;
        int hWnd = FindWindow("Shell_TrayWnd", "");




        public void Run(Form form, LadderUI.LowerMachine_Panel lowerMachine_Panel)
        {
            // 啟動

            form.Hide();
            if (起始畫面顯示) SplashScreen.ShowSplashScreen(起始畫面背景, 起始畫面標題內容, 起始畫面標題字體);
            lowerMachine_Panel.Run();
            this.lowerMachine_Panel = lowerMachine_Panel;
            this.form = form;
            form.Opacity = 0;
            form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
            form.Shown += new System.EventHandler(this.Form_Shown);
            this.Thread_DevicePoolling = new Basic.MyThread("Thread_DevicePoolling", form);
            this.Thread_ButtonUI = new Basic.MyThread("Thread_ButtonUI", form);
            this.Thread_NumBoxUI = new Basic.MyThread("Thread_NumBoxUI", form);
            while (true)
            {
                if (_開機延遲 == 0) break;
                if ((System.Environment.TickCount & Int32.MaxValue) >= _開機延遲 * 1000) break;
                System.Threading.Thread.Sleep(1);
            }
        }
        private void Form_Shown(object sender, EventArgs e)
        {
            List<H_Canvas> List_H_Canvas = new List<H_Canvas>();
            H_Canvas.FindCanvas(this.FindForm(), ref List_H_Canvas);
            for (int i = 0; i < List_H_Canvas.Count; i++)
            {
                List_H_Canvas[i].Canvas_Init();
            } //  form.Hide();
        }
        public void Add_Method(Basic.MyThread.MethodDelegate Method)
        {
            Thread_DevicePoolling.Add_Method(Method);
        }
        public void Set_CycleTime(int CycleTime)
        {
            _掃描速度 = CycleTime;
            Thread_DevicePoolling.SetSleepTime(_掃描速度);
        }
        public double Get_CycleTime()
        {
            return this.Thread_DevicePoolling.GetCycleTime();
        }
        public void Get_CycleTime(double RefreshTime_ms, Label label)
        {
            this.Thread_DevicePoolling.GetCycleTime(RefreshTime_ms, label);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (form != null && lowerMachine_Panel != null && !this.Init_Finish)
            {
                PLC = lowerMachine_Panel.GetlowerMachine();
                if (PLC != null)
                {
                    PLC_Device.InitPLC(PLC);                      
                    this.FindControl();
                    this.FindOvkControl();
                    this.FindMindVisionControl();

                   // this.pLC_ScreenPage.顯示標籤列 = PLC_ScreenPage.TabVisibleEnum.隱藏;

                    this.Thread_ButtonUI.AutoRun(true);
                    this.Thread_ButtonUI.AutoStop(true);
                    this.Thread_ButtonUI.SetSleepTime(100);
                    this.Thread_ButtonUI.Trigger();

                    this.Thread_NumBoxUI.AutoRun(true);
                    this.Thread_NumBoxUI.AutoStop(true);
                    this.Thread_NumBoxUI.SetSleepTime(100);
                    this.Thread_NumBoxUI.Trigger();

                    PLC.FLAG_UI_init = true;
                    if (起始畫面顯示) SplashScreen.CloseSplashScreen();

                    if (全螢幕顯示)
                    {
                        Basic.Screen.FullScreen(form, 0, true);
                    }
                    form.Opacity = 1;

                    this.Thread_DevicePoolling.AutoRun(true);
                    this.Thread_DevicePoolling.AutoStop(true);
                    this.Thread_DevicePoolling.SetSleepTime(_掃描速度);
                    this.Thread_DevicePoolling.Trigger();

                    GC.Collect();

                    Init_Finish = true;
                    if (this.UI_Finished_Event != null) this.UI_Finished_Event();
                }
            }
            if(this.Init_Finish)
            {
                this.Get_CycleTime(10, this.label_Cycletime);
            }
        }

 

        private void FindMindVisionControl()
        {
            if (this.邁得威視元件初始化)
            {
                foreach (Control ctl in form.Controls)
                {
                    FindSubMindVisionControl(ctl);
                }  
            }
        }
        private void FindSubMindVisionControl(Control ctl)
        {
            if (ctl is PLC_ScreenPage)
            {
                PLC_ScreenPage ctl_temp = (PLC_ScreenPage)ctl;
                foreach (Control temp in ctl_temp.Controls)
                {
                    FindSubMindVisionControl(temp);
                }
            }
            else if (ctl.Controls.Count > 0)
            {
                foreach (Control sub_ctl in ctl.Controls)
                {
                    FindSubMindVisionControl(sub_ctl);
                }
            }
            if (ctl is MVSDKUI.PLC_MindVision_Camera_UI)
            {
                MVSDKUI.PLC_MindVision_Camera_UI ctl_temp = (MVSDKUI.PLC_MindVision_Camera_UI)ctl;
                ctl_temp.Run(form, PLC);
            }
        }

        private void FindOvkControl()
        {
            List<H_Thread> List_H_Thread = new List<H_Thread>();
            H_Thread.FindThread(this.FindForm(), ref List_H_Thread);

            if (this.光道視覺元件初始化)
            {
                foreach (Control ctl in form.Controls)
                {
                    FindSubOvkControl(ctl);
                }
                for (int i = 0; i < List_H_Thread.Count; i++)
                {
                    List_H_Thread[i].Thread_Init();
                }
            }
        }
        private void FindSubOvkControl(Control ctl)
        {
            if (ctl is PLC_ScreenPage)
            {
                PLC_ScreenPage ctl_temp = (PLC_ScreenPage)ctl;
                foreach (Control temp in ctl_temp.Controls)
                {
                    FindSubOvkControl(temp);
                }
            }
            else if (ctl.Controls.Count > 0)
            {
                foreach (Control sub_ctl in ctl.Controls)
                {
                    FindSubOvkControl(sub_ctl);
                }
            }
            #region CCD元件
            if (ctl is H_AltairUDrv)
            {
                H_AltairUDrv ctl_temp = (H_AltairUDrv)ctl;
                ctl_temp.Run(form, PLC);
            }
            else if (ctl is H_Canvas)
            {
                H_Canvas ctl_temp = (H_Canvas)ctl;
                ctl_temp.Run(form, PLC);
            }
            else if (ctl is H_ROIBW8)
            {
                H_ROIBW8 ctl_temp = (H_ROIBW8)ctl;
                ctl_temp.Run(form, PLC, this);
            }
            else if (ctl is H_CircleROIBW8)
            {
                H_CircleROIBW8 ctl_temp = (H_CircleROIBW8)ctl;
                ctl_temp.Run(form, PLC, this);
            }
            else if (ctl is H_ImageBW8)
            {
                H_ImageBW8 ctl_temp = (H_ImageBW8)ctl;
                ctl_temp.Run(form, PLC, this);
            }
            else if (ctl is H_ImageC24)
            {
                H_ImageC24 ctl_temp = (H_ImageC24)ctl;
                ctl_temp.Run(form, PLC, this);
            }
            else if (ctl is H_ImageCopier)
            {
                H_ImageCopier ctl_temp = (H_ImageCopier)ctl;
                ctl_temp.Run(form, PLC, this);
            }
            else if (ctl is H_PointMsr)
            {
                H_PointMsr ctl_temp = (H_PointMsr)ctl;
                ctl_temp.Run(form, PLC, this);
            }

            #endregion
        }

        private void FindControl()
        {
            foreach (Control ctl in form.Controls)
            {
                FindSubControl(ctl);
            }
        }
        private void sub_FindScreenPage(ref Control ctl)
        {
            if (ctl.Parent is Form)
            {
                ctl = ctl.Parent;
                return;
            }
            else if (ctl.Parent is PLC_ScreenPage)
            {
                ctl = ctl.Parent;
                return;
            }
            else
            {
                ctl = ctl.Parent;
                sub_FindScreenPage(ref ctl);
            }
        }
        private void FindSubControl(Control ctl)
        {
            #region PLC元件
            if (ctl is PLC_ScreenPage)
            {
                PLC_ScreenPage ctl_temp = (PLC_ScreenPage)ctl;
                foreach (Control temp in ctl_temp.Controls)
                {
                    FindSubControl(temp);
                }
                ctl_temp.顯示標籤列 = PLC_ScreenPage.TabVisibleEnum.隱藏;
                ctl_temp.Run(PLC, form);
               
            }
            else if (ctl.Controls.Count > 0)
            {
                foreach (Control sub_ctl in ctl.Controls)
                {
                    FindSubControl(sub_ctl);
                }
            }
            if (ctl is TabControl)
            {
                Basic.Reflection.MakeDoubleBuffered((TabControl)ctl, true);
            }
            if (ctl is PLC_WordBox)
            {
                PLC_WordBox ctl_temp = (PLC_WordBox)ctl;
                ctl_temp.音效 = this.音效;
                ctl_temp.Run(PLC);
            }
            else if (ctl is PLC_NumBox)
            {
                PLC_NumBox ctl_temp = (PLC_NumBox)ctl;
                ctl_temp.音效 = this.音效;
                ctl_temp.Run(PLC, this.Thread_NumBoxUI);
                //ctl_temp.Run(PLC);
            }
            else if (ctl is PLC_ScreenButton)
            {
                PLC_ScreenButton ctl_temp = (PLC_ScreenButton)ctl;
                ctl_temp.音效 = this.音效;
                ctl_temp.Run(PLC);
                sub_FindScreenPage(ref ctl);
                if (ctl is PLC_ScreenPage)
                {
                    ctl_temp.Set_PLC_ScreenPage((PLC_ScreenPage)ctl);
                }

            }
            else if (ctl is PLC_RJ_ScreenButton)
            {
                PLC_RJ_ScreenButton ctl_temp = (PLC_RJ_ScreenButton)ctl;
                ctl_temp.音效 = this.音效;
                ctl_temp.Run(PLC);
                sub_FindScreenPage(ref ctl);
                if (ctl is PLC_ScreenPage)
                {
                    ctl_temp.Set_PLC_ScreenPage((PLC_ScreenPage)ctl);
                }

            }
            else if (ctl is PLC_Button)
            {
                PLC_Button ctl_temp = (PLC_Button)ctl;
                ctl_temp.音效 = this.音效;
                ctl_temp.Run(PLC, this.Thread_ButtonUI);
            }
               
            else if (ctl is PLC_AlarmFlow)
            {
                PLC_AlarmFlow ctl_temp = (PLC_AlarmFlow)ctl;
                ctl_temp.Run(form , PLC);
            }
            else if (ctl is PLC_CheckBox)
            {
                PLC_CheckBox ctl_temp = (PLC_CheckBox)ctl;
                ctl_temp.音效 = this.音效;
                ctl_temp.Run(PLC);
            }
            else if (ctl is PLC_ComboBox)
            {
                PLC_ComboBox ctl_temp = (PLC_ComboBox)ctl;
                ctl_temp.音效 = this.音效;
                ctl_temp.Run(PLC);
            }
            else if (ctl is PLC_MessageBox)
            {
                PLC_MessageBox ctl_temp = (PLC_MessageBox)ctl;
                ctl_temp.Run(PLC);
            }
            else if (ctl is PLC_TrackBarHorizontal)
            {
                PLC_TrackBarHorizontal ctl_temp = (PLC_TrackBarHorizontal)ctl;
                ctl_temp.Run(PLC);
            }
            else if (ctl is PLC_TrackBarVertical)
            {
                PLC_TrackBarVertical ctl_temp = (PLC_TrackBarVertical)ctl;
                ctl_temp.Run(PLC);
            }
            else if (ctl is PLC_SaveDeviceButtom)
            {
                PLC_SaveDeviceButtom ctl_temp = (PLC_SaveDeviceButtom)ctl;
                ctl_temp.Run(PLC);
            }
            else if (ctl is PLC_LoadDeviceButtom)
            {
                PLC_LoadDeviceButtom ctl_temp = (PLC_LoadDeviceButtom)ctl;
                ctl_temp.Run(PLC);
            }
            else if (ctl is PLC_MultiStateDisplay)
            {
                PLC_MultiStateDisplay ctl_temp = (PLC_MultiStateDisplay)ctl;
                ctl_temp.Run(PLC);
            }
            else if (ctl is PLC_SerialPort)
            {
                PLC_SerialPort ctl_temp = (PLC_SerialPort)ctl;
                ctl_temp.Run(form, PLC);
            }

            else if (ctl is PLC_Date)
            {
                PLC_Date ctl_temp = (PLC_Date)ctl;
      
                ctl_temp.Run(PLC);
            }
            else if (ctl is PLC_HighSpeedCounter)
            {
                PLC_HighSpeedCounter ctl_temp = (PLC_HighSpeedCounter)ctl;
                ctl_temp.Run(form, PLC);
            }
            else if (ctl is PLC_RJ_ChechBox)
            {
                PLC_RJ_ChechBox ctl_temp = (PLC_RJ_ChechBox)ctl;
                ctl_temp.音效 = this.音效;
                ctl_temp.Run(PLC);
            }
            else if (ctl is PLC_RJ_Button)
            {
                PLC_RJ_Button ctl_temp = (PLC_RJ_Button)ctl;
                ctl_temp.音效 = this.音效;
                ctl_temp.Run(PLC);
            }
            else if (ctl is PLC_RJ_ComboBox)
            {
                PLC_RJ_ComboBox ctl_temp = (PLC_RJ_ComboBox)ctl;
                ctl_temp.音效 = this.音效;
                ctl_temp.Run(PLC);
            }
            else if (ctl is PLC_RJ_WordBox)
            {
                PLC_RJ_WordBox ctl_temp = (PLC_RJ_WordBox)ctl;
                ctl_temp.音效 = this.音效;
                ctl_temp.Run(PLC);
            }
            else if (ctl is PLC_RJ_GroupBox)
            {
                PLC_RJ_GroupBox ctl_temp = (PLC_RJ_GroupBox)ctl;
                ctl_temp.Run(PLC);
            }
            else if (ctl is PLC_RJ_Pannel)
            {
                PLC_RJ_Pannel ctl_temp = (PLC_RJ_Pannel)ctl;
                ctl_temp.Run(PLC);
            }
            #endregion
            #region 外部設備
            else if (ctl is C9016)
            {
                C9016 ctl_temp = (C9016)ctl;
                ctl_temp.Run(form, PLC);
            }
            else if (ctl is E400)
            {
                E400 ctl_temp = (E400)ctl;
                ctl_temp.Run(form, PLC);
            }
            else if (ctl is E6232)
            {
                E6232 ctl_temp = (E6232)ctl;
                ctl_temp.Run(form, PLC);
            }
            else if (ctl is C1230)
            {
                C1230 ctl_temp = (C1230)ctl;
                ctl_temp.Run(form, PLC);
            }
            else if (ctl is DMC1000B)
            {
                DMC1000B ctl_temp = (DMC1000B)ctl;
                ctl_temp.Run(form, PLC);
            }
            else if (ctl is IOC1280)
            {
                IOC1280 ctl_temp = (IOC1280)ctl;
                ctl_temp.Run(form, PLC);
            }
            else if (ctl is DMC2410)
            {
                DMC2410 ctl_temp = (DMC2410)ctl;
                ctl_temp.Run(form, PLC);
            }
            else if (ctl is DMC3000)
            {
                DMC3000 ctl_temp = (DMC3000)ctl;
                ctl_temp.Run(form, PLC);
            }
            else if (ctl is ParallelPortControlUI)
            {
                ParallelPortControlUI ctl_temp = (ParallelPortControlUI)ctl;
                ctl_temp.Run(form, PLC);
            }
            else if (ctl is SerialPortControlUI)
            {
                SerialPortControlUI ctl_temp = (SerialPortControlUI)ctl;
                ctl_temp.Run(form, PLC);
            }
            else if (ctl is LD_NP24DV_4T)
            {
                LD_NP24DV_4T ctl_temp = (LD_NP24DV_4T)ctl;
                ctl_temp.Run(form, PLC, this);
            }
            else if (ctl is DAQM_4206A)
            {
                DAQM_4206A ctl_temp = (DAQM_4206A)ctl;
                ctl_temp.Run(form, PLC, this);
            }
            else if (ctl is HCG_485_IO)
            {
                HCG_485_IO ctl_temp = (HCG_485_IO)ctl;
                ctl_temp.Run(form, PLC);
            }
            #endregion
        }

        public static List<Control> Find_Control(Form form, Type type)
        {
            List<Control> list_ctl = new List<Control>();
            foreach (Control ctl in form.Controls)
            {
                FindSubControl(ctl, type, list_ctl);
            }
            return list_ctl;
        }
        private static void FindSubControl(Control ctl, Type type, List<Control> list_ctl)
        {
            if (ctl is PLC_ScreenPage)
            {
                PLC_ScreenPage ctl_temp = (PLC_ScreenPage)ctl;
                foreach (Control sub_ctl in ctl_temp.Controls)
                {
                    FindSubControl(sub_ctl, type, list_ctl);
                }
            }
            else if (ctl.Controls.Count > 0)
            {
                foreach (Control sub_ctl in ctl.Controls)
                {
                    FindSubControl(sub_ctl, type, list_ctl);
                }
            }
            if (ctl.GetType() == type)
            {
                list_ctl.Add(ctl);
            }
            
        }

        public static void Set_PLC_ScreenPage(Control control , PLC_ScreenPage pLC_ScreenPage)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                if(control.Controls[i] is PLC_ScreenButton)
                {
                    ((PLC_ScreenButton)control.Controls[i]).Set_PLC_ScreenPage(pLC_ScreenPage);
                }
                else if (control.Controls[i] is PLC_RJ_ScreenButton)
                {
                    ((PLC_RJ_ScreenButton)control.Controls[i]).Set_PLC_ScreenPage(pLC_ScreenPage);
                }
            }
        }

        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            //Basic.Screen.FullScreen(form, 0, false);
            //this.timer1.Dispose();
        }
    }
}
