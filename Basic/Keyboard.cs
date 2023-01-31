using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
namespace Basic
{
    public class Keyboard
    {
        public Keyboard()
        {
            Hook.KeyDown += new KeyAction(Hook_KeyDown);
            Hook.KeyUp += new KeyAction(Hook_KeyUp);
            Hook.Hook_Start();
        }


        public bool Key_Left;
        public bool Key_Right;
        public bool Key_Up;
        public bool Key_Down;
        public bool Key_Enter;
        public bool Key_ShiftKey;
        public bool Key_ControlKey;
        public bool Key_Insert;
        public bool Key_Delete;
        public bool Key_Esc;
        public bool Key_Back;
        public bool Key_PageUp;
        public bool Key_PageDown;
        public bool Key_Oemcomma;
        public bool Key_OemPeriod;
        
        public bool Key_Q;
        public bool Key_W;
        public bool Key_E;
        public bool Key_R;
        public bool Key_T;
        public bool Key_Y;
        public bool Key_U;
        public bool Key_I;
        public bool Key_O;
        public bool Key_P;
        public bool Key_A;
        public bool Key_S;
        public bool Key_D;
        public bool Key_F;
        public bool Key_G;
        public bool Key_H;
        public bool Key_J;
        public bool Key_K;
        public bool Key_L;
        public bool Key_Z;
        public bool Key_X;
        public bool Key_C;
        public bool Key_V;
        public bool Key_B;
        public bool Key_N;
        public bool Key_M;

        public bool Key_F1;
        public bool Key_F2;
        public bool Key_F3;
        public bool Key_F4;
        public bool Key_F5;
        public bool Key_F6;
        public bool Key_F7;
        public bool Key_F8;
        public bool Key_F9;
        public bool Key_F10;
        public bool Key_F11;
        public bool Key_F12;

        public bool MouseDown = false;
        public void KeyReset()
        {
            Key_Left = false;
            Key_Right = false;
            Key_Up = false;
            Key_Down = false;
            Key_Enter = false;
            Key_ShiftKey = false;
            Key_ControlKey = false;
            Key_Insert = false;
            Key_Delete = false;
            Key_Esc = false;
            Key_Back = false;
            Key_PageUp = false;
            Key_PageDown = false;
            Key_Oemcomma = false;
            Key_OemPeriod = false;

            Key_Q = false;
            Key_W = false;
            Key_E = false;
            Key_R = false;
            Key_T = false;
            Key_Y = false;
            Key_U = false;
            Key_I = false;
            Key_O = false;
            Key_P = false;
            Key_A = false;
            Key_S = false;
            Key_D = false;
            Key_F = false;
            Key_G = false;
            Key_H = false;
            Key_J = false;
            Key_K = false;
            Key_L = false;
            Key_Z = false;
            Key_X = false;
            Key_C = false;
            Key_V = false;
            Key_B = false;
            Key_N = false;
            Key_M = false;

            Key_F1 = false;
            Key_F2 = false;
            Key_F3 = false;
            Key_F4 = false;
            Key_F5 = false;
            Key_F6 = false;
            Key_F7 = false;
            Key_F8 = false;
            Key_F9 = false;
            Key_F10 = false;
            Key_F11 = false;
            Key_F12 = false;
        }
        public virtual void Hook_KeyDown(int nCode, IntPtr wParam, Keys Keys)
        {
            if (nCode >= 0)
            {
                string Keys_str = Keys.ToString();
                if (Keys == Keys.Left) Key_Left = true;
                if (Keys == Keys.Right) Key_Right = true;
                if (Keys == Keys.Up) Key_Up = true;
                if (Keys == Keys.Down) Key_Down = true;
                if (Keys == Keys.Enter) Key_Enter = true;
                if (Keys == Keys.LShiftKey) Key_ShiftKey = true;
                if (Keys == Keys.LControlKey) Key_ControlKey = true;
                if (Keys == Keys.Insert) Key_Insert = true;
                if (Keys == Keys.Delete) Key_Delete = true;
                if (Keys == Keys.Escape) Key_Esc = true;
                if (Keys == Keys.Back) Key_Back = true;
                if (Keys == Keys.PageUp) Key_PageUp = true;
                if (Keys == Keys.PageDown) Key_PageDown = true;
                if (Keys == Keys.Oemcomma) Key_Oemcomma = true;
                if (Keys == Keys.OemPeriod) Key_OemPeriod = true;

                if (Keys == Keys.Q) Key_Q = true;
                if (Keys == Keys.W) Key_W = true;
                if (Keys == Keys.E) Key_E = true;
                if (Keys == Keys.R) Key_R = true;
                if (Keys == Keys.T) Key_T = true;
                if (Keys == Keys.Y) Key_Y = true;
                if (Keys == Keys.U) Key_U = true;
                if (Keys == Keys.I) Key_I = true;
                if (Keys == Keys.O) Key_O = true;
                if (Keys == Keys.P) Key_P = true;
                if (Keys == Keys.A) Key_A = true;
                if (Keys == Keys.S) Key_S = true;
                if (Keys == Keys.D) Key_D = true;
                if (Keys == Keys.F) Key_F = true;
                if (Keys == Keys.G) Key_G = true;
                if (Keys == Keys.H) Key_H = true;
                if (Keys == Keys.J) Key_J = true;
                if (Keys == Keys.K) Key_K = true;
                if (Keys == Keys.L) Key_L = true;
                if (Keys == Keys.Z) Key_Z = true;
                if (Keys == Keys.X) Key_X = true;
                if (Keys == Keys.C) Key_C = true;
                if (Keys == Keys.V) Key_V = true;
                if (Keys == Keys.B) Key_B = true;
                if (Keys == Keys.N) Key_N = true;
                if (Keys == Keys.M) Key_M = true;

                if (Keys == Keys.F1) Key_F1 = true;
                if (Keys == Keys.F2) Key_F2 = true;
                if (Keys == Keys.F3) Key_F3 = true;
                if (Keys == Keys.F4) Key_F4 = true;
                if (Keys == Keys.F5) Key_F5 = true;
                if (Keys == Keys.F6) Key_F6 = true;
                if (Keys == Keys.F7) Key_F7 = true;
                if (Keys == Keys.F8) Key_F8 = true;
                if (Keys == Keys.F9) Key_F9 = true;
                if (Keys == Keys.F10) Key_F10 = true;
                if (Keys == Keys.F11) Key_F11 = true;
                if (Keys == Keys.F12) Key_F12 = true;
 
            }
        }
        public virtual void Hook_KeyUp(int nCode, IntPtr wParam, Keys Keys)
        {
            if (nCode >= 0)
            {
                if (Keys == Keys.Left) Key_Left = false;
                if (Keys == Keys.Right) Key_Right = false;
                if (Keys == Keys.Up) Key_Up = false;
                if (Keys == Keys.Down) Key_Down = false;
                if (Keys == Keys.Enter) Key_Enter = false;
                if (Keys == Keys.LShiftKey) Key_ShiftKey = false;
                if (Keys == Keys.LControlKey) Key_ControlKey = false;
                if (Keys == Keys.Insert) Key_Insert = false;
                if (Keys == Keys.Delete) Key_Delete = false;
                if (Keys == Keys.Escape) Key_Esc = false;
                if (Keys == Keys.Back) Key_Back = false;
                if (Keys == Keys.PageUp) Key_PageUp = false;
                if (Keys == Keys.PageDown) Key_PageDown = false;
                if (Keys == Keys.Oemcomma) Key_Oemcomma = false;
                if (Keys == Keys.OemPeriod) Key_OemPeriod = false;

                if (Keys == Keys.Q) Key_Q = false;
                if (Keys == Keys.W) Key_W = false;
                if (Keys == Keys.E) Key_E = false;
                if (Keys == Keys.R) Key_R = false;
                if (Keys == Keys.T) Key_T = false;
                if (Keys == Keys.Y) Key_Y = false;
                if (Keys == Keys.U) Key_U = false;
                if (Keys == Keys.I) Key_I = false;
                if (Keys == Keys.O) Key_O = false;
                if (Keys == Keys.P) Key_P = false;
                if (Keys == Keys.A) Key_A = false;
                if (Keys == Keys.S) Key_S = false;
                if (Keys == Keys.D) Key_D = false;
                if (Keys == Keys.F) Key_F = false;
                if (Keys == Keys.G) Key_G = false;
                if (Keys == Keys.H) Key_H = false;
                if (Keys == Keys.J) Key_J = false;
                if (Keys == Keys.K) Key_K = false;
                if (Keys == Keys.L) Key_L = false;
                if (Keys == Keys.Z) Key_Z = false;
                if (Keys == Keys.X) Key_X = false;
                if (Keys == Keys.C) Key_C = false;
                if (Keys == Keys.V) Key_V = false;
                if (Keys == Keys.B) Key_B = false;
                if (Keys == Keys.N) Key_N = false;
                if (Keys == Keys.M) Key_M = false;

                if (Keys == Keys.F1) Key_F1 = false;
                if (Keys == Keys.F2) Key_F2 = false;
                if (Keys == Keys.F3) Key_F3 = false;
                if (Keys == Keys.F4) Key_F4 = false;
                if (Keys == Keys.F5) Key_F5 = false;
                if (Keys == Keys.F6) Key_F6 = false;
                if (Keys == Keys.F7) Key_F7 = false;
                if (Keys == Keys.F8) Key_F8 = false;
                if (Keys == Keys.F9) Key_F9 = false;
                if (Keys == Keys.F10) Key_F10 = false;
                if (Keys == Keys.F11) Key_F11 = false;
                if (Keys == Keys.F12) Key_F12 = false;
            }
        }

        public delegate void KeyAction(int nCode, IntPtr wParam, Keys Keys);
        public delegate void MouseAction(int nCode, int mouse_x, int mouse_y);
        public class Hook
        {
            //挂钩
            [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
            private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
            //取消挂钩
            [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
            private static extern bool UnhookWindowsHookEx(int idHook);
            //调用下一个钩子
            [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
            private static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr LParam);
            //键盘消息结构
            [StructLayout(LayoutKind.Sequential)]
            private class KeyBoardHookStruct
            {
                public int vkCode;
                public int scanCode;
                public int flags;
                public int time;
                public int dwExtraInfo;
            }


            public enum MouseEventType : int
            {
                MouseMove = 512,
                MouseDown = 513,
                MouseUp = 514,
            }
            //滑鼠事件結構
            [StructLayout(LayoutKind.Sequential)]
            private struct POINT
            {
                public int x;
                public int y;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct MSLLHOOKSTRUCT
            {
                public POINT pt;
                public uint mouseData;
                public uint flags;
                public uint time;
                public IntPtr dwExtraInfo;
            }

            //私有委托
            private delegate int HookProc(int nCode, IntPtr wParam, IntPtr LParam);
            private static HookProc KeyBoardHookProcedure;
            private static HookProc MouseHookProcedure;

            //事件聲明
            public static event KeyAction KeyDown;
            public static event KeyAction KeyUp;
            public static event MouseAction MouseMove;
            public static event MouseAction MouseUp;
            public static event MouseAction MouseDown;

            //要用到的变量，标记是否成功设置钩子
            private static int hHook = 0;

            //LowLevel键盘截获，如果是WH_KEYBOARD＝2，并不能对系统键盘截取，Acrobat Reader会在你截取之前获得键盘。 
            public enum HookType : int
            {
                WH_JOURNALRECORD = 0,
                WH_JOURNALPLAYBACK = 1,
                WH_KEYBOARD = 2,
                WH_GETMESSAGE = 3,
                WH_CALLWNDPROC = 4,
                WH_CBT = 5,
                WH_SYSMSGFILTER = 6,
                WH_MOUSE = 7,
                WH_HARDWARE = 8,
                WH_DEBUG = 9,
                WH_SHELL = 10,
                WH_FOREGROUNDIDLE = 11,
                WH_CALLWNDPROCRET = 12,
                WH_KEYBOARD_LL = 13,
                WH_MOUSE_LL = 14
            }


            private static int KeyBoardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
            {
                KeyBoardHookStruct kbh = (KeyBoardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyBoardHookStruct));
                //使用前需要為KeyUp和KeyDown綁定对应的处理过程，就像使用控件的事件一樣。

                //鬆開按鍵
                
                if (wParam.ToInt32() == 257)
                {
                    if (KeyUp != null) KeyUp(nCode, wParam, (Keys)kbh.vkCode);
                }
                else
                {
                    if (KeyDown != null) KeyDown(nCode, wParam, (Keys)kbh.vkCode);
                }

                //將按鍵消息交給下一個鉤子
                
                return CallNextHookEx(hHook, nCode, wParam, lParam);
                //這句好像沒什麼用？即使這裡不調用，其他程序還是能收到鍵盤消息，有待研究。

            }
            private static int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
            {
                MSLLHOOKSTRUCT kbh = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                //使用前需要為KeyUp和KeyDown綁定对应的处理过程，就像使用控件的事件一樣。
                if (nCode >= 0)
                {
                    if ((int)wParam == (int)MouseEventType.MouseMove)
                    {
                        if (MouseMove != null) MouseMove(nCode, kbh.pt.x, kbh.pt.y);
                    }
                    else if ((int)wParam == (int)MouseEventType.MouseDown)
                    {
                        if (MouseDown != null) MouseDown(nCode, kbh.pt.x, kbh.pt.y);
                    }
                    else if ((int)wParam == (int)MouseEventType.MouseUp)
                    {
                        if (MouseUp != null) MouseUp(nCode, kbh.pt.x, kbh.pt.y);
                    }
                }               
                //將按鍵消息交給下一個鉤子
                return CallNextHookEx(hHook, nCode, wParam, lParam);

            }
            //取消钩子事件
            public static bool Hook_Clear()
            {
                if (hHook != 0)
                {
                    hHook = 0;
                    return UnhookWindowsHookEx(hHook);
                   
                }
                return true;
            }
            //开始钩子
            public static bool Hook_Start()
            {
                if (hHook == 0)
                {
                    KeyBoardHookProcedure = new HookProc(KeyBoardHookProc);
                    hHook = SetWindowsHookEx((int)(HookType.WH_KEYBOARD_LL), KeyBoardHookProcedure, Process.GetCurrentProcess().MainModule.BaseAddress, 0);
                    //如果设置钩子失败. 
                    if (hHook == 0)
                    {
                        Hook_Clear();
                        return false;                     
                    }

                    MouseHookProcedure = new HookProc(MouseHookProc);
                    hHook = SetWindowsHookEx((int)HookType.WH_MOUSE_LL, MouseHookProcedure, Process.GetCurrentProcess().MainModule.BaseAddress, 0);
                    //如果设置钩子失败. 
                    if (hHook == 0)
                    {
                        Hook_Clear();
                        return false;
                    }

                }
                return true;
            }
        }
    }
}
