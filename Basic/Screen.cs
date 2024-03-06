using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
namespace Basic
{
    public class Screen
    {
        private static int SW_HIDE = 0;
        private static int SW_SHOW = 1;
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int screensize);
        private static int SM_CXSCREEN = 0;
        private static int SM_CYSCREEN = 1;
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);
        public static int HWND_TOP = 0; //{在前面}
        public static int HWND_BOTTOM = 1; //{在后面}
        //uFlags 参数可选值:
        static uint SWP_NOMOVE = 0X02;
        static uint SWP_NOSIZE = 0x01;
        static uint SWP_NOZORDER = 0X04;
        static uint SWP_SHOWWINDOW = 0x0040;
        static uint SWP_FRAMECHANGED = 0x20;
        static int hWnd = FindWindow("Shell_TrayWnd", "");

        static public void FullScreen(Form form , int ScreenIndex ,bool enable)
        {
            System.Windows.Forms.Screen[] screen = System.Windows.Forms.Screen.AllScreens;
            if(ScreenIndex > screen.Length)
            {
                MessageBox.Show(string.Format("指定 < {0} > 號螢幕不存在!"), ScreenIndex.ToString());
                return;
            }
            if (enable)
            {                           
                form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                form.TopMost = false;
                form.StartPosition = FormStartPosition.CenterScreen;
              //  ShowWindow(hWnd, SW_HIDE);
                SetWindowPos(form.Handle, HWND_TOP, screen[ScreenIndex].Bounds.X, screen[ScreenIndex].Bounds.Y, screen[ScreenIndex].Bounds.Width, screen[ScreenIndex].Bounds.Height, SWP_SHOWWINDOW);
            }
            else
            {
                form.FormBorderStyle = FormBorderStyle.Sizable;
                //SetWindowPos(form.Handle, HWND_TOP, screen[ScreenIndex].Bounds.X, screen[ScreenIndex].Bounds.Y, screen[ScreenIndex].Bounds.Width, screen[ScreenIndex].Bounds.Height, SWP_SHOWWINDOW);
                form.WindowState = FormWindowState.Maximized;
                ShowWindow(hWnd, SW_SHOW);
            }
           
        }


        private const Int32 WM_SYSCOMMAND = 274;
        private const UInt32 SC_CLOSE = 61536;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int RegisterWindowMessage(string lpString);

        public static int ShowInputPanel()
        {
            try
            {
                string ProcessName = "TabTip";//換成想要結束的進程名字
                System.Diagnostics.Process[] MyProcess = System.Diagnostics.Process.GetProcessesByName(ProcessName);
                for (int i = 0; i < MyProcess.Length; i++)
                {
                    MyProcess[i].Kill();
                }
                dynamic file = "C:\\Program Files\\Common Files\\microsoft shared\\ink\\TabTip.exe";
                if (!System.IO.File.Exists(file))
                    return -1;
                Process.Start(file);
                //return SetUnDock(); //不知SetUnDock()是什麽，所以直接註釋返回1
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static void HideInputPanel()
        {
            IntPtr TouchhWnd = new IntPtr(0);
            TouchhWnd = (IntPtr)FindWindow("IPTip_Main_Window", null);
            if (TouchhWnd == IntPtr.Zero)
                return;
            PostMessage(TouchhWnd, WM_SYSCOMMAND, SC_CLOSE, 0);
        }

        private const UInt32 StdOutputHandle = 0xFFFFFFF5;
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetStdHandle(UInt32 nStdHandle);
        [DllImport("kernel32.dll")]
        private static extern void SetStdHandle(UInt32 nStdHandle, IntPtr handle);
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        static extern bool AllocConsole();
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        public static extern void FreeConsole();
        public static void ShowConsole()
        {
            AllocConsole();

            ConsoleColor oriColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("* Don't close this console window or the application will also close.");
            Console.WriteLine();
            Console.ForegroundColor = oriColor;

        }
        public static void CloseConsole()
        {
            FreeConsole();
        }
    }
}
