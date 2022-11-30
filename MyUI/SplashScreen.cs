using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace MyUI
{
    public partial class SplashScreen : Form
    {
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern Int32 GetWindowLong(IntPtr hwnd, int nIndex);
        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern Int32 SetWindowLong(IntPtr hwnd, int nIndex, Int32 dwNewLong);
        [DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
        public static extern int SetLayeredWindowAttributes(IntPtr Handle, int crKey, byte bAlpha, int dwFlags);
        const int GWL_EXSTYLE = -20;
        const int WS_EX_TRANSPARENT = 0x20;
        const int WS_EX_LAYERED = 0x80000;
        const int LWA_ALPHA = 2;

        static SplashScreen instance;
        /// <summary>
        /// 顯示的圖片
        /// </summary>
        public static SplashScreen Instance
        {
            get
            {
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public SplashScreen()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
           // BackgroundImage = bitmap;
        }
        /*protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                if (bitmap != null)
                {
                    bitmap.Dispose();
                    bitmap = null;
                }
                components.Dispose();
            }
            base.Dispose(disposing);
        }*/
        public static void ShowSplashScreen(Image bakImage , String Title,Font font)
        {      
            instance = new SplashScreen();
            instance.panel_Logo.BackgroundImage = bakImage;
            instance.label_Title.Text = Title;
            instance.label_Title.Font = font;
           // SetWindowLong(instance.Handle, GWL_EXSTYLE, GetWindowLong(instance.Handle, GWL_EXSTYLE) | WS_EX_LAYERED);
            instance.Show();
            Effect(true);
        }
        public static void CloseSplashScreen()
        {
            if (SplashScreen.Instance != null)
            {
                SplashScreen.Instance.BeginInvoke(new MethodInvoker(SplashScreen.Instance.Dispose));
                SplashScreen.Instance = null;
            }
        }
        static private void Effect(bool show)
        {
            for (byte i = 0; i < 150; i++)
            {
                if (show) Instance.Opacity = 0.01 * i;
                else Instance.Opacity = 1.0 - 0.01 * i;
                //SetLayeredWindowAttributes(instance.Handle, 0, (byte)(show ? i : byte.MaxValue - i), LWA_ALPHA);
                System.Threading.Thread.Sleep(15);
                Application.DoEvents();
            }
        }

        private void SplashScreen_Shown(object sender, EventArgs e)
        {
           
        }

        private void SplashScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
