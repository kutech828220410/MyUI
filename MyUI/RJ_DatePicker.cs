using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrawingClass;
using System.Runtime.InteropServices;
using Basic;
namespace MyUI
{
    public class RJ_DatePicker : DateTimePicker
    {
        private bool DefaultDate = false;
        private const int SWP_NOMOVE = 0x0002;
        private const int DTM_First = 0x1000;
        private const int DTM_GETMONTHCAL = DTM_First + 8;
        private const int MCM_GETMINREQRECT = DTM_First + 9;
        private DateTime dateTime_min = "1753-01-01".StringToDateTime();
        [DllImport("uxtheme.dll")]
        private static extern int SetWindowTheme(IntPtr hWnd, string appName, string idList);
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, ref RECT lParam);
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
        int X, int Y, int cx, int cy, int uFlags);
        [DllImport("User32.dll")]
        private static extern IntPtr GetParent(IntPtr hWnd);
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT { public int L, T, R, B; }
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);


        private Color skinColor = Color.MediumSlateBlue;
        private Color textColor = Color.White;
        private Color borderColor = Color.PaleVioletRed;
        private int borderSize = 0;

        private bool droppedDown = false;
        private Image calendarIcon = Properties.Resources.Calendar_White;
        private RectangleF iconButtonArea;
        private const int calendarIconWidth = 34;
        private const int arrowIconWidth = 17;
     
        public Color SkinColor
        {
            get
            {
                return skinColor;
            }
            set
            {
                skinColor = value;
                if (skinColor.GetBrightness() >= 0.8F)
                {
                    this.calendarIcon = Properties.Resources.Calendar_Dark;
                }
                else
                {
                    this.calendarIcon = Properties.Resources.Calendar_White;
                }
                this.Invalidate();
            }
        }
        public Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                textColor = value;
                this.Invalidate();
            }
        }
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }
        public int BorderSize
        {
            get
            {
                return borderSize;
            }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }
        public Font PickerFont
        {
            get
            {
                return this.CalendarFont;
            }
            set
            {
                this.CalendarFont = value;
            }
        }
        public Color PickerFore
        {
            get
            {
                return this.CalendarForeColor;
            }
            set
            {
                this.CalendarForeColor = value;
            }
        }
        public RJ_DatePicker()
        {     
            SetStyle(ControlStyles.UserPaint, true);
            this.MinimumSize = new Size(250, 35);
            this.Font = new Font(this.Font.Name, 15.75F);
            base.AllowDrop = false;
            this.MinDate = DateTime.MinValue;
            this.MaxDate = DateTime.MaxValue;
        }
        protected override void OnClick(EventArgs e)
        {
            DefaultDate = false;
            base.OnClick(e);
        }
        protected override void OnDropDown(EventArgs eventargs)
        {
            

            if (Application.RenderWithVisualStyles)
            {
                const int DTM_GETMONTHCAL = 0x1008;

                //Get handle of calendar control - disable theming
                IntPtr hCalendar = SendMessage(this.Handle, DTM_GETMONTHCAL, 0, 0);
                if (hCalendar != IntPtr.Zero)
                {
                    SetWindowTheme(hCalendar, "", "");

                    //Get handle of parent popup - resize appropriately
                    IntPtr hParent = GetParent(hCalendar);
                    var r = new RECT();
                    SendMessage(hCalendar, MCM_GETMINREQRECT, 0, ref r);
                    if (hParent != IntPtr.Zero)
                    {
                        //The size you specify here will depend on the CalendarFont size chosen
                        MoveWindow(hParent, 0, 0, r.R - r.L + 6, r.B - r.T + 6, true);
                    }
                }
            }

            base.OnDropDown(eventargs);
            this.droppedDown = true;
        }
        protected override void OnCloseUp(EventArgs eventargs)
        {
            DefaultDate = false;
            base.OnCloseUp(eventargs);
            this.droppedDown = false;
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            e.Handled = true;
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            using (Graphics graphics = this.CreateGraphics())
            using (Pen penBorder = new Pen(borderColor, borderSize))
            using (SolidBrush skinBrush = new SolidBrush(skinColor))
            using (SolidBrush openIconBrush = new SolidBrush(Color.FromArgb(50, 64, 64, 64)))
            using (SolidBrush textBrush = new SolidBrush(textColor))
            using (StringFormat textFormat = new StringFormat())
            {
                string text = this.Text;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                RectangleF clientArea = new RectangleF(0, 0, this.Width - 0.5F, this.Height - 0.5F);
                RectangleF IconArea = new RectangleF(clientArea.Width - calendarIconWidth, 0, calendarIconWidth, clientArea.Height);
                penBorder.Alignment = PenAlignment.Inset;

                SizeF textSize = graphics.MeasureString("   " + text, this.Font);            
                graphics.FillRectangle(skinBrush, clientArea);
                if(DefaultDate)
                {
                    text = "----/--/--";
                }
                graphics.DrawString("   " + text, this.Font, textBrush, 0, (this.Height - TextRenderer.MeasureText(text, this.Font).Height) / 2, textFormat);
                if (droppedDown == true) graphics.FillRectangle(openIconBrush, IconArea);

                if (borderSize >= 1) graphics.DrawRectangle(penBorder, clientArea.X, clientArea.Y, clientArea.Width, clientArea.Height);

                graphics.DrawImage(calendarIcon, this.Width - calendarIcon.Width - 9, (this.Height - calendarIcon.Height) / 2);
            }
        }
        public void SetDefaultDate()
        {
            DefaultDate = true;
        }
        public bool IsDefaultDate()
        {
            return DefaultDate;
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RJ_DatePicker
            // 
            this.CalendarFont = new System.Drawing.Font("新宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResumeLayout(false);

        }
    }
}
