using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;  //DllImport名空间
using System.Windows.Forms; //MouseEventHandler名空间
using System.Reflection; //Assembly名空间
using System.ComponentModel; //Win32Exception名空间
using System.Diagnostics;//PROCESS命名空间

namespace Basic
{
    public class Mouse
    {
        [StructLayout(LayoutKind.Sequential)]
        private class POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private class MouseLLHookStruct
        {
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public int dwExtraInfo;

        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);
        private delegate int HookProc(int nCode, int wParam, IntPtr lParam);
        [DllImport("user32")]
        private static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

        [DllImport("user32")]
        private static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetKeyState(int vKey);

        [DllImport("kernel32.dll", EntryPoint = "GetModuleHandleA", SetLastError = true, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
        public static extern IntPtr GetModuleHandleA(String lpModuleName);

        [DllImport("kernel32.dll", EntryPoint = "GetModuleHandleW", SetLastError = true, CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern IntPtr GetModuleHandleW(String lpModuleName);

        private const int WH_MOUSE_LL = 14;
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE = 7;
        private const int WH_KEYBOARD = 2;
        private const int WM_MOUSEMOVE = 0x200;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_RBUTTONDOWN = 0x204;
        private const int WM_MBUTTONDOWN = 0x207;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_RBUTTONUP = 0x205;
        private const int WM_MBUTTONUP = 0x208;
        private const int WM_LBUTTONDBLCLK = 0x203;
        private const int WM_RBUTTONDBLCLK = 0x206;
        private const int WM_MBUTTONDBLCLK = 0x209;
        private const int WM_MOUSEWHEEL = 0x020A;

        public Mouse()
        {
            Start();
        }
  
        ~Mouse()
        {
            Stop(true, false);
        }

        public event MouseEventHandler MouseEvent; //MouseEventHandler是委托，表示处理窗体、控件或其他组件的 MouseDown、MouseUp 或 MouseMove 事件的方法。
        private int hMouseHook = 0; //标记mouse hook是否安装
        private static HookProc MouseHookProcedure;


        public void Start()
        {
            IntPtr HM = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]);

            if (hMouseHook == 0)
            {
                MouseHookProcedure = new HookProc(MouseHookProc);//钩子的处理函数
                hMouseHook = SetWindowsHookEx(
                        WH_MOUSE_LL,
                        MouseHookProcedure,
                        GetModuleHandleW(Process.GetCurrentProcess().MainModule.ModuleName),//本进程模块句柄
                        0);
                if (hMouseHook == 0)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    Stop(false, false);
                    throw new Win32Exception(errorCode);
                }
            }

        }
        public void Stop(bool UninstallMouseHook, bool ThrowExceptions)
        {
            if (hMouseHook != 0 && UninstallMouseHook)
            {
                int retMouse = UnhookWindowsHookEx(hMouseHook);
                hMouseHook = 0;
                if (retMouse == 0 && ThrowExceptions)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    throw new Win32Exception(errorCode);
                }
            }

        }
        private int MouseHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if ((nCode >= 0) && (MouseEvent != null))
            {
                MouseLLHookStruct mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));

                MouseButtons button = MouseButtons.None;
                short mouseDelta = 0;
                int clickCount = 0;
                switch (wParam)
                {
                    case WM_LBUTTONDOWN://513出现了
                        button = MouseButtons.Left;
                        clickCount = 1;
                        break;
                    case WM_RBUTTONDOWN://516出现了
                        button = MouseButtons.Right;
                        clickCount = 1;
                        break;
                    case WM_LBUTTONDBLCLK://515  doubleclick没有出现过
                        button = MouseButtons.XButton1;
                        clickCount = 2;
                        break;
                    case WM_RBUTTONDBLCLK://518
                        button = MouseButtons.XButton1;
                        clickCount = 2;
                        break;
                    case WM_MOUSEMOVE://512 出现了
                        button = MouseButtons.XButton2;
                        clickCount = 0;
                        break;
                    case WM_MOUSEWHEEL://522 没试
                        mouseDelta = (short)((mouseHookStruct.mouseData >> 16) & 0xffff);
                        clickCount = 0;
                        break;
                }

                MouseEventArgs e = new MouseEventArgs(
                                                   button,
                                                   clickCount,
                                                   mouseHookStruct.pt.x,
                                                   mouseHookStruct.pt.y,
                                                   mouseDelta);
                MouseEvent(this, e);//转给委托函数
            }
            return CallNextHookEx(hMouseHook, nCode, wParam, lParam);
        }

    }
}
