using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
namespace MeasureSystemUI
{
    public partial class H_Thread : UserControl
    {
        Basic.MyThread MyThread_Loop;

        private bool flag_Init = false;
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
        private string _ThreadName = "ThreadName001";
        [ReadOnly(false), Browsable(true), Category("參數設定"), Description(""), DefaultValue("")]
        public string ThreadName
        {
            get
            {
                return _ThreadName;
            }
            set
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(delegate { this.label_ThreadName.Text = value; }));
                }
                _ThreadName = value;
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
        public H_Thread()
        {
            InitializeComponent();
        }

        public void Thread_Init()
        {
            if (!this.flag_Init)
            {
                this.MyThread_Loop = new Basic.MyThread(ThreadName, FindForm());
                this.MyThread_Loop.AutoRun(true);
                this.MyThread_Loop.SetSleepTime(this.CycleTime);
                this.MyThread_Loop.IsBackGround = true;
                this.MyThread_Loop.Trigger();
                this.timer_Refresh.Enabled = true;
                this.flag_Init = true;
            }     
        }
        public void AddMethod(Basic.MyThread.MethodDelegate method)
        {
            this.MyThread_Loop.Add_Method(method);
        }
        public static void FindThread( Form form, ref List<H_Thread> List_Thread)
        {
            foreach (Control ctl in form.Controls)
            {
                FindSubControl(ctl, "GetAll", ref List_Thread);
            }
        }
        public static void FindThread(string ThreadName, Form form, ref List<H_Thread> List_Thread)
        {
            foreach (Control ctl in form.Controls)
            {
                FindSubControl(ctl, ThreadName, ref List_Thread);
            }
        }
        private static void FindSubControl(Control ctl, string ThreadName, ref List<H_Thread> List_Thread)
        {
            if (ctl is PLC_ScreenPage)
            {
                PLC_ScreenPage ctl_temp = (PLC_ScreenPage)ctl;
                foreach (Control temp in ctl_temp.Controls)
                {
                    FindSubControl(temp, ThreadName, ref  List_Thread);
                }
            }
            else if (ctl.Controls.Count > 0)
            {
                foreach (Control sub_ctl in ctl.Controls)
                {
                    FindSubControl(sub_ctl, ThreadName, ref  List_Thread);
                }
            }

            if (ctl is H_Thread)
            {
                H_Thread ctl_temp = (H_Thread)ctl;
                if (ThreadName == ctl_temp.ThreadName)
                {
                    List_Thread.Add((H_Thread)ctl);
                }
                else if (ThreadName == "GetAll")
                {
                    List_Thread.Add((H_Thread)ctl);
                }
            }

        }
        private void timer_Refresh_Tick(object sender, EventArgs e)
        {
            if (this.MyThread_Loop != null)
            {
                this.textBox_CycleTime.Text = this.MyThread_Loop.GetCycleTime().ToString("0.00");
            }          
        }
    }
}
