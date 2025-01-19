using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Linq;

namespace MyUI
{
    public class ConsoleTrayUI
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        private const int SW_RESTORE = 9;

        private NotifyIcon notifyIcon;
        private ToolStripMenuItem showConsoleMenuItem;
        private ToolStripMenuItem hideConsoleMenuItem;
        private System.Windows.Forms.Timer timer;
        private bool isConsoleVisible = false;
        private int cycleInterval = 5000;
        private string notifyText;
        private bool checkDuplicate;
        private Mutex mutex;

        public event Action OnShowConsole;
        public event Action OnHideConsole;
        public event Action OnCycleExecute;

        public ConsoleTrayUI(string notifyTitle = "Console App in System Tray", Icon icon = null, int interval = 5000, bool checkDuplicateExecution = false)
        {
            //Console.OutputEncoding = Encoding.UTF8;
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput(), Encoding.UTF8) { AutoFlush = true });
            Console.SetError(new StreamWriter(Console.OpenStandardError(), Encoding.UTF8) { AutoFlush = true });
            Console.SetOut(new SafeConsoleWriter(Console.Out));
            Console.SetError(new SafeConsoleWriter(Console.Error));

            notifyText = notifyTitle;
            cycleInterval = interval;
            checkDuplicate = checkDuplicateExecution;

            if (checkDuplicate)
            {
                mutex = new Mutex(true, notifyText, out bool createdNew);
                if (!createdNew)
                {
                    Environment.Exit(0);
                }
            }

            InitializeTray(icon ?? SystemIcons.Information);
            HideConsole();
            StartTimer();
        }

        private void InitializeTray(Icon icon)
        {
            notifyIcon = new NotifyIcon()
            {
                Icon = icon,
                Text = notifyText,
                Visible = true
            };

            ContextMenuStrip menu = new ContextMenuStrip();
            showConsoleMenuItem = new ToolStripMenuItem("顯示", null, ShowConsole) { Enabled = true };
            hideConsoleMenuItem = new ToolStripMenuItem("隱藏", null, HideConsole) { Enabled = false };
            menu.Items.Add(showConsoleMenuItem);
            menu.Items.Add(hideConsoleMenuItem);
            menu.Items.Add("退出", null, ExitApp);
            notifyIcon.ContextMenuStrip = menu;
            notifyIcon.DoubleClick += ToggleConsole;
        }

        public void ShowConsole(object sender = null, EventArgs e = null)
        {
            if (GetConsoleWindow() == IntPtr.Zero)
            {
                AllocConsole();
                AttachConsole(-1); // 附加到父 Console，確保輸出顯示

                Console.OutputEncoding = Encoding.UTF8;
                Console.SetOut(new StreamWriter(Console.OpenStandardOutput(), Encoding.UTF8) { AutoFlush = true });
                Console.SetError(new StreamWriter(Console.OpenStandardError(), Encoding.UTF8) { AutoFlush = true });
                Console.SetOut(new SafeConsoleWriter(Console.Out));
                Console.SetError(new SafeConsoleWriter(Console.Error));
                Console.WriteLine("Console 已重新開啟");
            }
            IntPtr handle = GetConsoleWindow();
            if (handle != IntPtr.Zero)
            {
                ShowWindowAsync(handle, SW_RESTORE);
                SetForegroundWindow(handle);
                isConsoleVisible = true;
                showConsoleMenuItem.Enabled = false;
                hideConsoleMenuItem.Enabled = true;
                OnShowConsole?.Invoke();
            }
        }

        public void HideConsole(object sender = null, EventArgs e = null)
        {
            IntPtr handle = GetConsoleWindow();
            if (handle != IntPtr.Zero)
            {
                ShowWindow(handle, SW_HIDE);
                FreeConsole();
                isConsoleVisible = false;
                showConsoleMenuItem.Enabled = true;
                hideConsoleMenuItem.Enabled = false;
                OnHideConsole?.Invoke();
            }
        }

        private void ToggleConsole(object sender, EventArgs e)
        {
            if (isConsoleVisible)
            {
                HideConsole();
            }
            else
            {
                ShowConsole();
            }
        }

        private void ExitApp(object sender, EventArgs e)
        {
            notifyIcon.Dispose();
            Environment.Exit(0);
        }

        private void StartTimer()
        {
            timer = new System.Windows.Forms.Timer { Interval = cycleInterval };
            timer.Tick += (sender, e) => OnCycleExecute?.Invoke();
            timer.Start();
        }

        public class SafeConsoleWriter : TextWriter
        {
            private TextWriter originalOut;

            public SafeConsoleWriter(TextWriter originalOut)
            {
                this.originalOut = originalOut;
            }

            public override Encoding Encoding => Encoding.UTF8;

            public override void WriteLine(string value)
            {
                if (GetConsoleWindow() != IntPtr.Zero)
                {
                    try { originalOut.WriteLine(value); }
                    catch (IOException) { Debug.WriteLine(value); }
                }
                else
                {
                    Debug.WriteLine(value);
                }
            }

            public override void Write(string value)
            {
                if (GetConsoleWindow() != IntPtr.Zero)
                {
                    try { originalOut.Write(value); }
                    catch (IOException) { Debug.Write(value); }
                }
                else
                {
                    Debug.Write(value);
                }
            }
        }

    }
}
