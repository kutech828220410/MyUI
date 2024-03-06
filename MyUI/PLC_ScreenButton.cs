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
using System.Diagnostics;
namespace MyUI
{
      [System.Drawing.ToolboxBitmap(typeof(Button))]
    public partial class PLC_ScreenButton : PLC_Button
    {
        private PLC_ScreenPage pLC_ScreenPage;
        private Basic.MyThread MyThread_ExitApp;
        private LowerMachine PLC;
        private bool flag_state = false;
        #region 隱藏屬性
        [ReadOnly(false), Browsable(false), Category("自訂屬性"), Description(""), DefaultValue("")]
        public override StatusEnum 按鈕型態
        {
            get
            {
                return base.按鈕型態;
            }
            set
            {
                base.按鈕型態 = value;
            }
        }
        [ReadOnly(false), Browsable(false), Category("自訂屬性"), Description(""), DefaultValue("")]
        public override string 寫入元件位置
        {
            get
            {
                return base.寫入元件位置;
            }
            set
            {
                base.寫入元件位置 = value;
            }
        }
        [ReadOnly(false), Browsable(false), Category("自訂屬性"), Description(""), DefaultValue("")]
        public override string 讀取元件位置
        {
            get
            {
                return base.讀取元件位置;
            }
            set
            {
                base.讀取元件位置 = string.Empty;
            }
        }
        [ReadOnly(false), Browsable(false), Category("自訂屬性"), Description(""), DefaultValue("")]
        public override bool 讀寫鎖住
        {
            get
            {
                return base.讀寫鎖住;
            }
            set
            {
                base.讀寫鎖住 = value;
            }
        }        
        #endregion
        #region 顯示屬性
        public enum 換頁選擇方式Enum : int
        {
            名稱 = 0,索引, 退出程式
        }
        換頁選擇方式Enum _換頁選擇方式 = new 換頁選擇方式Enum();
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public 換頁選擇方式Enum 換頁選擇方式
        {
            get
            {
                return _換頁選擇方式;
            }
            set
            {
                _換頁選擇方式 = value;
            }
        }
        private string _頁面名稱 = "";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 頁面名稱
        {
            get
            {
                return _頁面名稱;
            }
            set
            {
                _頁面名稱 = value;
            }
        }
        private int _頁面編號 = 0;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int 頁面編號
        {
            get
            {
                return _頁面編號;
            }
            set
            {
                _頁面編號 = value;
            }
        }
        private string _控制位址 = "D0";
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public string 控制位址
        {
            get { return _控制位址; }
            set
            {
                value = value.ToUpper();
                bool divice_OK = false;
                if (LadderProperty.DEVICE.TestDevice(value))
                {
                    string temp = value.Remove(1);
                    if (temp == "D" || temp == "R") divice_OK = true;
                }

                if (divice_OK) _控制位址 = value;
                else _控制位址 = "";
            }
        }
        public enum WordLengthEnum : int
        {
            單字元, 雙字元
        }
        [ReadOnly(true), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public WordLengthEnum 字元長度 { get; set; }
        public enum StateEnum : int
        {
            正常顯示 = 0, 顯示為ON, 顯示為OFF
        }
        StateEnum _顯示方式 = new StateEnum();
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public StateEnum 顯示方式
        {
            get
            {
                return _顯示方式;
            }
            set
            {
                if (value == StateEnum.顯示為ON) 顯示狀態 = true;
                else 顯示狀態 = false;
                _顯示方式 = value;
            }
        }
        #endregion
        public PLC_ScreenButton()
        {
            InitializeComponent();
            
        }
        private void PLC_ScreenButton_Load(object sender, EventArgs e)
        {
            this.MouseDown += PLC_ScreenButton_MouseDown;
        }

        private void PLC_ScreenButton_MouseDown(object sender, MouseEventArgs e)
        {
            if(this.PLC == null)
            {
                pLC_ScreenPage.SelecteTabText(this._頁面名稱);
            }
        
        }

        public override void Run(LowerMachine pLC)
        {
            if (顯示方式 == StateEnum.顯示為ON) base.sub_按鈕狀態設為ON();
            else if (顯示方式 == StateEnum.顯示為OFF) base.sub_按鈕狀態設為OFF();
            this.PLC = pLC;
            base.Run(pLC);
        }
        public void Set_PLC_ScreenPage(PLC_ScreenPage pLC_ScreenPage)
        {
            this.pLC_ScreenPage = pLC_ScreenPage;
        }
       // bool but_press_buf = false;
        delegate void strHandles(string str);
        CaptureDelegate captureDelegate;
        public override void Run()
        {
            if(captureDelegate == null)captureDelegate = new CaptureDelegate(LabelCapture);
            //Basic.ControlExtensions.InvokeOnUiThreadIfRequired(this.label1, LabelCapture);
            //Invoke(captureDelegate);

            if (pLC_ScreenPage != null)
            {
                if(this.pLC_ScreenPage.PageText == this._頁面名稱)
                {
                    if(!this.flag_state)
                    {
                        base.sub_按鈕狀態設為ON();
                    }
                    this.flag_state = true;
                }
                else
                {
                    if (this.flag_state)
                    {
                        base.sub_按鈕狀態設為OFF();
                    }
                    this.flag_state = false;
                }
            }

            base.EnableCheck();
            if (but_press != but_press_buf)
            {
                if (but_press && 換頁選擇方式 == 換頁選擇方式Enum.索引)
                {
                    if (_控制位址 != "" && _控制位址 != null && PLC != null)
                    {
                        int value = _頁面編號;
                        if (字元長度 == WordLengthEnum.單字元)
                        {
                            PLC.properties.Device.Set_Device(_控制位址, value);
                        }
                        else if (字元長度 == WordLengthEnum.雙字元)
                        {
                            PLC.properties.Device.Set_DoubleWord(_控制位址, Convert.ToInt64(value));
                        }
                    }
                    if (顯示方式 == StateEnum.正常顯示) base.sub_按鈕狀態設為ON();
                }
                if (but_press && 換頁選擇方式 == 換頁選擇方式Enum.名稱)
                {
                    if(pLC_ScreenPage != null)
                    {
                        this.Invoke(new Action(delegate
                        {
                            this.pLC_ScreenPage.SelecteTabText(_頁面名稱);
                        }));
        
                    }
                }
                else
                {
                    if (顯示方式 == StateEnum.正常顯示) base.sub_按鈕狀態設為OFF();
                }
                if (but_press && 換頁選擇方式 == 換頁選擇方式Enum.退出程式)
                {
                    DialogResult Result = MessageBox.Show("是否退出?", "Warring", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    if (Result == DialogResult.Yes)
                    {
                        MyThread_ExitApp = new Basic.MyThread();
                        MyThread_ExitApp.Add_Method(ExitApp);
                        MyThread_ExitApp.AutoRun(true);
                        MyThread_ExitApp.IsBackGround = true;
                        MyThread_ExitApp.Trigger();
                    }
                    else if (Result == DialogResult.No)
                    {
                       
                    }
                    else if (Result == DialogResult.Cancel)
                    {
                   
                    }
                }
   
                but_press_buf = but_press;
            }
         

        }
        void ExitApp()
        {
            if (this.InvokeRequired) this.Invoke(new Action(delegate { this.FindForm().Close(); }));         
          //  Process.GetCurrentProcess().CloseMainWindow();
        }

  
    }
}
