using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace MyUI
{
    public partial class LoadingForm : Form
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        //正面_水平方向
        const int AW_HOR_POSITIVE = 0x0001;
        //负面_水平方向
        const int AW_HOR_NEGATIVE = 0x0002;
        //正面_垂直方向
        const int AW_VER_POSITIVE = 0x0004;
        //负面_垂直方向
        const int AW_VER_NEGATIVE = 0x0008;
        //由中间四周展开或由四周向中间缩小
        const int AW_CENTER = 0x0010;
        //隐藏对象
        const int AW_HIDE = 0x10000;
        //显示对象
        const int AW_ACTIVATE = 0x20000;
        //拉幕滑动效果
        const int AW_SLIDE = 0x40000;
        //淡入淡出渐变效果
        const int AW_BLEND = 0x80000;

        public static  Form form;
        public delegate void MethodEventHandler();
        public event MethodEventHandler MethodEvent;
        static public bool IsShown = false;
        static private  LoadingForm pLoading = new LoadingForm();
        delegate void SetTextCallback(string title, string caption, string description);
        delegate void CloseFormCallback();

        public LoadingForm()
        {
            InitializeComponent();
            Thread t = new Thread(new ThreadStart(delegateEventMethod));
            t.IsBackground = true;
            t.Start();
     
            this.Load += LoadingForm_Load;
            this.FormClosing += LoadingForm_FormClosing;
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014) return;
            base.WndProc(ref m);
        }
        public static void ShowLoadingFormInvoke()
        {
            form.Invoke(new Action(delegate 
            {
                ShowLoadingForm();
            }));
        }
        public static void ShowLoadingForm()
        {
            LoadingForm loadingForm = LoadingForm.getLoading();
            if (form != null)
            {
                loadingForm.Location = new Point((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - loadingForm.Width) / 2 , (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - loadingForm.Height) / 2);
            }
            Task.Run(new Action(delegate
            {
                if(IsShown == false)
                {
                    IsShown = true;
                    loadingForm.ShowDialog();
                }
   
            }));
            while(true)
            {
                if (IsShown) break;
                System.Threading.Thread.Sleep(20);
            }
        }
        public static void Set_Description(string description)
        {
            form.Invoke(new Action(delegate
            {
                pLoading.lbl_description.Text = $"{description}";
                pLoading.Refresh();
            }));
        }
        public static void CloseLoadingFormInvoke()
        {
            form.Invoke(new Action(delegate
            {
                CloseLoadingForm();
            }));
        }
        public static void CloseLoadingForm()
        {
            LoadingForm.getLoading().mCloseLoadingForm();
        }
        public static LoadingForm getLoading()
        {
            if (pLoading.IsDisposed)
            {
                pLoading = new LoadingForm();         
                return pLoading;
            }
            else
            {             
                return pLoading;
            }
        }
        protected override void OnShown(EventArgs e)
        {
            pLoading.BringToFront();
            pLoading.TopLevel = true;
            pLoading.TopMost = true;
            int cnt = 0;
            Basic.MyTimerBasic myTimer = new Basic.MyTimerBasic(2);
            myTimer.StartTickTime(1);
      
            //while (true)
            //{
            //    try
            //    {
            //        if (cnt >= 200) break;
            //        if (myTimer.IsTimeOut())
            //        {
            //            pLoading.Opacity = 0 + 0.04 * cnt;
            //            myTimer.TickStop();
            //            myTimer.StartTickTime(1);
            //            Application.DoEvents();
            //            cnt++;
            //        }
            //    }
            //    catch
            //    {
            //        break;
            //    }
            //}

            base.OnShown(e);
          

        }
        private static void PLoading_Shown(object sender, EventArgs e)
        {
           
        }

        public void SetCaptionAndDescription(string title, string caption, string description)
        {
            if (this.InvokeRequired && pLoading.lbl_caption.InvokeRequired && pLoading.lbl_description.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetCaptionAndDescription);
                this.Invoke(d, new object[] { title, caption, description });
            }
            else
            {
                if (!title.Equals(""))
                {
                    this.Text = title;
                }
                if (!caption.Equals(""))
                {
                    pLoading.lbl_caption.Text = caption;
                }
                if (!description.Equals(""))
                {
                    pLoading.lbl_description.Text = description;
                }
            }
        }
        public void SetExecuteMethod(MethodEventHandler method)
        {
            this.MethodEvent += method;
        }
        private void delegateEventMethod()
        {
            if (MethodEvent != null) MethodEvent();
        }
        public void mCloseLoadingForm()
        {
            if (IsShown == false) return;
            if (this.InvokeRequired)
            {
                CloseFormCallback d = new CloseFormCallback(mCloseLoadingForm);
                this.Invoke(d, new object[] { });
            }
            else
            {
                pLoading.Close();
                IsShown = false;

            }
        }
        private void LoadingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            int cnt = 0;
            Basic.MyTimerBasic myTimer = new Basic.MyTimerBasic(1);
            myTimer.StartTickTime();
            while (true)
            {
                if (cnt >= 200) break;
                if (myTimer.IsTimeOut())
                {
                    pLoading.Opacity = 0.8 - 0.004 * cnt;
                    myTimer.TickStop();
                    myTimer.StartTickTime(1);
                    Application.DoEvents();
                    cnt++;
                }
            }

            if (!this.IsDisposed)
            {
                this.Dispose(true);
            }
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
         
        }
    
    }
}
