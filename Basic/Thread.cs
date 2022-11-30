using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
namespace Basic
{
    public class MyThread
    {
        public delegate void MethodDelegate();
        public bool IsDead = false;
        private bool FLAG_AutoRun = false;
        private bool FLAG_Stop = false;
        private bool FLAG_AutoStop = true;
        private String ThreadName = "";
        private List<MethodDelegate> Method = new List<MethodDelegate>();
        private ManualResetEvent ThreadDeadEvent, ThreadTriggerEvent;
        private System.Threading.Thread WorkerThread;
        private int SleepTime = 1;
        private double CycleTime;
        private double CycleTime_start;
        private Stopwatch stopwatch = new Stopwatch();
        private double RefreshTimeNow = 0;
        private bool isBusy = false;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            private set
            {
                this.isBusy = value;
            }
        }
        public bool IsBackGround
        {
            get
            {
                return WorkerThread.IsBackground;
            }
            set
            {
                WorkerThread.IsBackground = value;
            }
        }
        public MyThread(string ThreadName)
        {
            init(ThreadName);
        }
        public MyThread()
        {
            init("");
        }
        public MyThread(Form form)
        {
            form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
            init("");
        }
        public MyThread(string ThreadName, Form form)
        {
            form.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosing);
            init(ThreadName);
        }
        void init(string ThreadName)
        {
            // Create worker thread #1 releated ojects
            stopwatch.Start();
            this.ThreadName = ThreadName;
            ThreadDeadEvent = new ManualResetEvent(false);
            ThreadTriggerEvent = new ManualResetEvent(false);
            WorkerThread = new System.Threading.Thread(this.ThreadFunction);
            WorkerThread.IsBackground = true;
            WorkerThread.Start();
        }
        public void AutoRun(bool Enable)
        {
            FLAG_AutoRun = Enable;
        }
        public void Trigger()
        {
            ThreadTriggerEvent.Set();
        }
        public void Restart()
        {
            if (ThreadDeadEvent == null) ThreadDeadEvent = new ManualResetEvent(false);
            if (ThreadTriggerEvent == null) ThreadTriggerEvent = new ManualResetEvent(false);
            WorkerThread = new System.Threading.Thread(this.ThreadFunction);
            WorkerThread.Start();
        }
        public void Stop()
        {
            FLAG_AutoRun = false;
            FLAG_Stop = true;
            ThreadDeadEvent.Set();
            ThreadTriggerEvent.Set();
        }
        public void AutoStop(bool Enable)
        {
            this.FLAG_AutoStop = Enable;
        }
        public void Abort()
        {
            WorkerThread.Abort();
        }
        public void Add_Method(MethodDelegate method)
        {
            lock (this) Method.Add(method);
        }
        public void SetSleepTime(int Time)
        {
            this.SleepTime = Time;
        }
        public double GetCycleTime()
        {
            return Math.Round(CycleTime, 3);
        }
        public void GetCycleTime(double RefreshTime_ms, Label label)
        {
            if ((stopwatch.Elapsed.TotalMilliseconds - RefreshTimeNow) > RefreshTime_ms)
            {
                RefreshTimeNow = stopwatch.Elapsed.TotalMilliseconds;
                label.BeginInvoke(new Action(delegate
                {
                    label.Text = this.GetCycleTime().ToString();
                }));
            }
        }
        private void ThreadFunction()
        {
            MethodDelegate[] DelegateArrayUI;
            while (!ThreadDeadEvent.WaitOne(0))
            {
                this.IsBusy = true;
                if (FLAG_AutoRun) ThreadTriggerEvent.Set();
                if (FLAG_AutoRun) CycleTime_start = stopwatch.Elapsed.TotalMilliseconds;
                ThreadTriggerEvent.WaitOne();
                if (!FLAG_AutoRun) CycleTime_start = stopwatch.Elapsed.TotalMilliseconds;
                DelegateArrayUI = Method.ToArray();
                if (!FLAG_Stop)
                {
                    for (int i = 0; i < DelegateArrayUI.Length; i++)
                    {
                        if (DelegateArrayUI[i] != null) DelegateArrayUI[i]();
                    }
                }

                if (SleepTime > 0) System.Threading.Thread.Sleep(SleepTime);
                ThreadTriggerEvent.Reset();
                CycleTime = stopwatch.Elapsed.TotalMilliseconds - CycleTime_start;
                if (this.FLAG_Stop) this.IsDead = true;
                this.IsBusy = false;
            }

        }
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.FLAG_AutoStop) this.Stop();
        }

    }
    public static class ControlExtensions
    {
        public static void InvokeOnUiThreadIfRequired(this Control control, Action action)
        {
            try
            {
                if (!control.IsDisposed)
                {
                    if (control.InvokeRequired)
                    {
                        control.Invoke(action);
                    }
                    else
                    {
                        action.Invoke();
                    }
                }
                else
                    Thread.CurrentThread.Abort();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
