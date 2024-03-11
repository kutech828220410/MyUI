using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
namespace MyUI
{
    public partial class MyDialog : CCSkinMain
    {
        public delegate void LoadFinishedEventHandler(EventArgs e);
        public event LoadFinishedEventHandler LoadFinishedEvent;

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

        private int special_Time = 100;
        [Category("Skin")]
        [DefaultValue(true)]
        [Description("窗口淡出時間")]
        public int Special_Time { get { return this.special_Time; } set { this.special_Time = value; } }
        public static Form form;
        public DialogResult ShowDialog()
        {
            if (form == null)
            {
                this.Special = false;
                this.TopMost = true;
                this.TopLevel = true;
                this.Opacity = 1;
                this.Location = new Point((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
                this.ShowIcon = false;
                this.ShowInTaskbar = false;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.SizeGripStyle = SizeGripStyle.Show;
                //this.Visible = false;
                base.ShowDialog();
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    this.Special = false;
                    this.TopMost = true;
                    this.TopLevel = true;
                    this.Opacity = 1;
                    this.Location = new Point((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
                    this.ShowIcon = false;
                    this.ShowInTaskbar = false;
                    this.FormBorderStyle = FormBorderStyle.FixedDialog;
                    this.SizeGripStyle = SizeGripStyle.Show;
                    //this.Visible = false;
                    base.ShowDialog();
                }));
            }

            return this.DialogResult;
        }

        public MyDialog()
        {
            InitializeComponent();
            this.TopMost = true;
            this.Opacity = 1;
            Basic.Reflection.MakeDoubleBuffered(this, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }

   
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Application.DoEvents();
            AnimateWindow(this.Handle, special_Time, AW_HIDE | AW_BLEND | AW_SLIDE);
            Application.DoEvents();
            //double temp = 1.0D / special_Time;
            //int cnt = 0;
            //Basic.MyTimerBasic myTimer = new Basic.MyTimerBasic(1);
            //myTimer.StartTickTime();
            //while (true)
            //{
            //    if (cnt >= special_Time) break;
            //    if (myTimer.IsTimeOut())
            //    {
            //        this.Opacity = 1 - temp * cnt;
            //        myTimer.TickStop();
            //        myTimer.StartTickTime(1);
            //        Application.DoEvents();
            //        cnt++;
            //    }
            //}

        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014) return;
            base.WndProc(ref m);
        }
        protected override void OnShown(EventArgs e)
        {
            //this.Visible = true;
           

            base.OnShown(e);
    
            //double temp = 1.0D / special_Time;
            //int cnt = 0;
            //Basic.MyTimerBasic myTimer = new Basic.MyTimerBasic(1);
            //myTimer.StartTickTime();
            //while (true)
            //{
            //    if (cnt >= special_Time) break;
            //    if (myTimer.IsTimeOut())
            //    {
            //        this.Opacity = 0 + temp * cnt;
            //        myTimer.TickStop();
            //        myTimer.StartTickTime(1);
            //        Application.DoEvents();
            //        cnt++;
            //    }
            //}
        }
        protected override void OnLoad(EventArgs e)
        {
      
            //this.Visible = false;
            //base.SuspendLayout();
            base.OnLoad(e); 
            //base.SuspendLayout();
            //this.Visible = false;

            if (special_Time < 0) special_Time = 100;
            Application.DoEvents();
            AnimateWindow(this.Handle, special_Time, AW_BLEND | AW_SLIDE);
            Application.DoEvents();
            if (LoadFinishedEvent != null) LoadFinishedEvent(e);
        }

    }
}
