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

        public static void ShowLoadingForm()
        {
            LoadingForm loadingForm = LoadingForm.getLoading();
            Task.Run(new Action(delegate
            {
                if(IsShown == false)
                {
                    IsShown = true;
                    loadingForm.ShowDialog();
                }
   
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
                pLoading.Shown += PLoading_Shown;
                pLoading.TopMost = true;
                return pLoading;
            }
            else
            {
                return pLoading;
            }
        }

        private static void PLoading_Shown(object sender, EventArgs e)
        {
            //for (byte i = 0; i < 200; i++)
            //{
            //    pLoading.Opacity = 0.004 * i;
            //    System.Threading.Thread.Sleep(1);
            //    Application.DoEvents();
            //}


            int cnt = 0;
            Basic.MyTimerBasic myTimer = new Basic.MyTimerBasic(1);
            myTimer.StartTickTime();
            while (true)
            {
                try
                {
                    if (cnt >= 200) break;
                    if (myTimer.IsTimeOut())
                    {
                        pLoading.Opacity = 0 + 0.04 * cnt;
                        myTimer.TickStop();
                        myTimer.StartTickTime(1);
                        Application.DoEvents();
                        cnt++;
                    }
                }
                catch
                {
                    break;
                }
             
            }

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

            //for (byte i = 0; i < 200; i++)
            //{
            //    pLoading.Opacity = 0.8 - 0.004 * i;
            //    System.Threading.Thread.Sleep(1);
            //    Application.DoEvents();
            //}
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
