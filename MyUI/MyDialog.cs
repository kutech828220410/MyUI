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
using System.Runtime.InteropServices;
using Basic;
namespace MyUI
{
    public partial class MyDialog : CCSkinMain
    {
        public delegate void LoadFinishedEventHandler(EventArgs e);
        public event LoadFinishedEventHandler LoadFinishedEvent;
        public delegate void ShowDialogEventHandler();
        public event ShowDialogEventHandler ShowDialogEvent;

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

        // 导入 Windows API 函数
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOW = 5;
        private const int SW_RESTORE = 9;
        // 显示指定标题的窗口到最前端
        public static void BringDialogToFront(string windowTitle)
        {
            IntPtr hWnd = FindWindow(null, windowTitle);
            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, SW_SHOW);
                ShowWindow(hWnd, SW_SHOWNORMAL);
                SetForegroundWindow(hWnd);
            }
        }

        private Color _backColor = Color.White;
        public new Color BackColor 
        { 
            get
            {
                return _backColor;
            }
            set
            {
                this._backColor = value;
            }
        }
        private Color _ControlBoxActive = Color.DarkGray;
        public new Color ControlBoxActive
        {
            get
            {
                return this._ControlBoxActive;
            }
            set
            {
                this._ControlBoxActive = value;
            }
        }
        private Color _ControlBoxDeactive = Color.DarkGray;
        public new Color ControlBoxDeactive
        {
            get
            {
                return this._ControlBoxDeactive;
            }
            set
            {
                this._ControlBoxDeactive = value;
            }
        }

        private int special_Time = 100;
        [Category("Skin")]
        [DefaultValue(true)]
        [Description("窗口淡出時間")]
        public int Special_Time { get { return this.special_Time; } set { this.special_Time = value; } }

        private bool close_flag = false;
        public static Form form;
        public DialogResult ShowDialog()
        {
            if (ShowDialogEvent != null) ShowDialogEvent();
            if (this.DialogResult == DialogResult.Cancel) return this.DialogResult;
            base.ControlBoxActive = _ControlBoxActive;
            base.ControlBoxDeactive = _ControlBoxDeactive;
            base.BackColor = _backColor;

            if (form == null)
            {
                this.Special = false;
          
                this.Opacity = 1;
                this.Location = new Point((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
                if (this.FormBorderStyle == FormBorderStyle.FixedDialog)
                {
                    this.TopMost = true;
                    this.TopLevel = true;
                    this.ShowIcon = false;
                    this.ShowInTaskbar = false;
                    this.FormBorderStyle = FormBorderStyle.FixedDialog;
                }

                this.SizeGripStyle = SizeGripStyle.Show;
                if (this.FormBorderStyle == FormBorderStyle.None)
                {
                    base.Show();
                }
                else base.ShowDialog();
            }
            else
            {
                form.Invoke(new Action(delegate
                {
                    this.Special = false;
               
                    this.Opacity = 1;
                    this.Location = new Point((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
                    if (this.FormBorderStyle == FormBorderStyle.FixedDialog)
                    {
                        this.TopMost = true;
                        this.TopLevel = true;
                        this.ShowIcon = false;
                        this.ShowInTaskbar = false;
                        this.FormBorderStyle = FormBorderStyle.FixedDialog;
                    }
                    //this.Visible = false;
                    if (this.FormBorderStyle == FormBorderStyle.None)
                    {
                        base.Show();
                    }
                    else base.ShowDialog();
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

        new public void Close()
        {
            if (this.IsHandleCreated)
            {
                this.Invoke(new Action(delegate
                {
                    base.Close();
                }));
            }

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Application.DoEvents();
            AnimateWindow(this.Handle, special_Time, AW_HIDE | AW_BLEND | AW_SLIDE);
            Application.DoEvents();


        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014) return;
            if (m.Msg == 6 && m.WParam.ToInt32() == 2097152)
            {
                Console.WriteLine($"m.Msg:{m.Msg},m.WParam:{m.WParam.ToInt32()}");
                return;
            }
            base.WndProc(ref m);
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
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
