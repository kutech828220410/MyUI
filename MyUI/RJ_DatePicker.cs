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
namespace MyUI
{
    public class RJ_DatePicker : DateTimePicker
    {
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
            //Application.SetCompatibleTextRenderingDefault(false);
           
            SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.MinimumSize = new Size(250, 35);
            this.Font = new Font(this.Font.Name, 15.75F);
        }
        protected override void OnDropDown(EventArgs eventargs)
        {
            base.OnDropDown(eventargs);
            this.droppedDown = true;
        }
        protected override void OnCloseUp(EventArgs eventargs)
        {
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
             
                RectangleF clientArea = new RectangleF(0, 0, this.Width - 0.5F, this.Height - 0.5F);
                RectangleF IconArea = new RectangleF(clientArea.Width - calendarIconWidth, 0, calendarIconWidth, clientArea.Height);
                penBorder.Alignment = PenAlignment.Inset;

                SizeF textSize = graphics.MeasureString("   " + this.Text, this.Font);            
                graphics.FillRectangle(skinBrush, clientArea);

                graphics.DrawString("   " + this.Text, this.Font, textBrush, 0, (this.Height - TextRenderer.MeasureText(this.Text, this.Font).Height) / 2, textFormat);
                if (droppedDown == true) graphics.FillRectangle(openIconBrush, IconArea);

                if (borderSize >= 1) graphics.DrawRectangle(penBorder, clientArea.X, clientArea.Y, clientArea.Width, clientArea.Height);

                graphics.DrawImage(calendarIcon, this.Width - calendarIcon.Width - 9, (this.Height - calendarIcon.Height) / 2);
            }
        }
    }
}
