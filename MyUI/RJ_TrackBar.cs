using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyUI
{
    [DefaultEvent("ValueChanged")]
    public partial class RJ_TrackBar : Control
    {
        public enum enum_MouseDownType
        {
            None,
            LeftSlider,
            RightSlider,
            Inside,
        }
        public enum MouseStatus
        {

            Enter,
            Leave,
            Down,
            Up
        }
        public delegate void ValueChangedEventHandler(int MinValue, int MaxValue);
        public event ValueChangedEventHandler ValueChanged;

        private enum_MouseDownType MouseDownType;     
        private MouseStatus mouseStatus = MouseStatus.Leave;
        private PointF mousePoint = Point.Empty;
        private int minValueBuf = 0;
        private int maxValueBuf = 0;

        #region 自訂屬性
        private int sliderSize = 10;
        [Category("自訂屬性"), Description("背景条颜色")]
        public int SliderSize
        {
            get
            {
                return sliderSize;
            }
            set
            {
                sliderSize = value;
                if (sliderSize < 10) sliderSize = 10;
                 Size = new Size(Width + sliderSize * 2, barSize + sliderSize * 2);
                this.Invalidate();
            }
        }
        private Color barColor = Color.FromArgb(128, 255, 128);//浅绿
        [Category("自訂屬性"), Description("背景条颜色")]
        public Color BarColor
        {
            get { return barColor; }
            set
            {
                barColor = value;
                Invalidate();
            }
        }
        private Color sliderColor = Color.FromArgb(0, 192, 0);//绿
        [Category("自訂屬性"), Description("滑块颜色")]
        public Color SliderColor
        {
            get { return sliderColor; }
            set
            {
                sliderColor = value;
                Invalidate();
            }
        }
        private Color topSliderColor = Color.Red;
        [Category("自訂屬性"), Description("滑块颜色")]
        public Color TopSliderColor
        {
            get { return topSliderColor; }
            set
            {
                topSliderColor = value;
                Invalidate();
            }
        }
        private Color bottomSliderColor = Color.Red;
        [Category("自訂屬性"), Description("滑块颜色")]
        public Color BottomSliderColor
        {
            get { return bottomSliderColor; }
            set
            {
                bottomSliderColor = value;
                Invalidate();
            }
        }
        private int minimum = 0;
        [Category("自訂屬性"), Description("最小值<para>范围：大于等于0</para>")]
        public int Minimum
        {
            get { return minimum; }
            set
            {
                minimum = value;
                if (minimum >= maximum) minimum = maximum - 1;
                if (minimum < 0) minimum = 0;
                Invalidate();
            }
        }
        private int maximum = 100;
        [Category("自訂屬性"), Description("最大值")]
        public int Maximum
        {
            get { return maximum; }
            set
            {
                maximum = value;
                if (maximum <= minimum) maximum = minimum + 1;
                Invalidate();
            }
        }
        private int minValue = 0;
        [Category("自訂屬性"), Description("当前值")]
        public int MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                if (minValue > maxValue) minValue = maxValue;
                if (minValue < minimum) minValue = minimum;
                if (minValue > maximum) minValue = maximum;
                Invalidate();
                ValueChanged?.Invoke(minValue, maxValue);
            }
        }
        private int maxValue = 0;
        [Category("自訂屬性"), Description("当前值")]
        public int MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                if (maxValue < minValue) maxValue = minValue;
                if (maxValue < minimum) maxValue = minimum;
                if (maxValue > maximum) maxValue = maximum;
                Invalidate();
                ValueChanged?.Invoke(minValue, maxValue);
            }
        }
        private int barSize = 30;
        [Category("自訂屬性"), Description("滑条高度（水平）/宽度（垂直)")]
        public int BarSize
        {
            get { return barSize; }
            set
            {
                barSize = value;
                if (barSize < 1) barSize = 1;
                Size = new Size(Width + sliderSize * 2, barSize + sliderSize * 2);
            }
        }
        [Category("自訂屬性"), Description("滑條字體大小")]
        private Font barFont = new Font("微軟正黑體", 9);
        public Font BarFont
        {
            get { return barFont; }
            set
            {
                barFont = value;
                this.Invalidate();
            }
        }
        [Category("自訂屬性"), Description("滑條字體顏色")]
        private Color barForeColor = Color.DarkGray;
        public Color BarForeColor
        {
            get { return barForeColor; }
            set
            {
                barForeColor = value;
                this.Invalidate();
            }
        }
        #endregion

        public RJ_TrackBar()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
        public void SetValue(int minValue , int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            if (this.maxValue > maximum) this.maxValue = maximum;
            if (this.minValue < minimum) this.minValue = minimum;
            this.Invalidate();
        }
        public void SetMaxMinmun(int minimum , int maximum)
        {
            if (minimum > maximum) minimum = maximum;
            this.minimum = minimum;
            this.maximum = maximum;
            if (this.maxValue > maximum) this.maxValue = maximum;
            if (this.minValue < minimum) this.minValue = minimum;
            this.Invalidate();
        }

        private enum_MouseDownType InMouseDown(PointF mousePoint)
        {
            float fRatio = Convert.ToSingle(maximum - minimum) / (Width - sliderSize * 2);
            int Value = Convert.ToInt32(fRatio * ( mousePoint.X ));
            float offset = fRatio * (sliderSize);
            int barOffset = (this.Height - barSize) / 2;
            if (mousePoint.Y >= barOffset && mousePoint.Y <= this.Height - barOffset)
            {
                if ((Value) <= maxValue + offset + 0 - minimum && (Value) >= minValue + offset - 0 - minimum) return enum_MouseDownType.Inside;
            }
            else
            {
                if ((Value) <= maxValue + offset + 2 - minimum && (Value) >= maxValue + offset - 1 - minimum) return enum_MouseDownType.RightSlider;
                else if ((Value) <= minValue + offset + 1 - minimum && (Value) >= minValue + offset - 2 - minimum) return enum_MouseDownType.LeftSlider;
            }
                      
            return enum_MouseDownType.None;
        }
        private int PointXToValue(float mousePointX)
        {
            int value = 0;
            float fRatio = Convert.ToSingle(maximum - minimum) / (Width - sliderSize * 2);
            value = Convert.ToInt32(fRatio * (mousePointX - sliderSize));
            return value;
        }
        private float ValueToPointX(int value)
        {
            float PointX = 0;
            float fRatio = Convert.ToSingle(maximum - minimum) / (Width - sliderSize * 2);
            PointX = Convert.ToInt32(value / fRatio);
            return PointX;
        }
        private float GetPaintPointValue(float p0)
        {
            float fPointValue = 0;
            float fCapHalfWidth = 0;
            fPointValue = p0;
            if (fPointValue < fCapHalfWidth) fPointValue = fCapHalfWidth;
            if (fPointValue > Width - fCapHalfWidth) fPointValue = Width - fCapHalfWidth;
            return fPointValue;
        }

        private void DrawTriangleDown(Graphics g , Point p0 , int height)
        {

            /*        p0
             *      /     \       |
             *     /       \      |  
             *    /         \     | height
             *   /__ __ __ __\    |
            */
            Point point1 = new Point(p0.X, p0.Y);
            Point point2 = new Point(p0.X - height / 2,  p0.Y + height);
            Point point3 = new Point(p0.X + height / 2,  p0.Y + height);
            Point[] pntArr = { point1, point2, point3 };
            Brush brush = new SolidBrush(bottomSliderColor);
            Pen pen = new Pen(brush);
            g.DrawPolygon(pen, pntArr);
            g.FillPolygon(brush, pntArr);
        }

        private void DrawTriangleTop(Graphics g, Point p0, int height)
        {

            /*    __ __ __ __
             *    \         /         |
             *     \       /          |  
             *      \     /           | height
             *        p0              |
            */
            Point point1 = new Point(p0.X, p0.Y);
            Point point2 = new Point(p0.X - height / 2, p0.Y - height);
            Point point3 = new Point(p0.X + height / 2, p0.Y - height);
            Point[] pntArr = { point1, point2, point3 };
            Brush brush = new SolidBrush(topSliderColor);
            Pen pen = new Pen(brush);
            g.DrawPolygon(pen, pntArr);
            g.FillPolygon(brush, pntArr);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (maxValue > maximum) maxValue = maximum;
            if (minValue < minimum) minValue = minimum;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            Pen penBarBack = new Pen(barColor, barSize);
            Pen penBarFore = new Pen(sliderColor, barSize);
            float PointXMin= ValueToPointX(MinValue - minimum);
            float PointXMax= ValueToPointX(MaxValue);
            float minimum_po = ValueToPointX(minimum);
            e.Graphics.DrawLine(penBarBack, sliderSize, Height / 2f, Width - sliderSize, Height / 2f);
            e.Graphics.DrawLine(penBarFore, PointXMin + sliderSize , Height / 2f, PointXMax + sliderSize - minimum_po, Height / 2f);

            DrawTriangleDown(e.Graphics, new Point((int)(PointXMin + sliderSize), (int)((Height + barSize) / 2f)), sliderSize);
            DrawTriangleTop(e.Graphics, new Point((int)(PointXMax + sliderSize - minimum_po), (int)((Height - barSize) / 2f)), sliderSize);

            SizeF text_sizeF = TextRenderer.MeasureText(MinValue.ToString(), barFont);
            Brush brush = new SolidBrush(barForeColor);
            e.Graphics.DrawString(MinValue.ToString(), barFont, brush, new PointF(sliderSize, (int)((Height - text_sizeF.Height) / 2f)));
            e.Graphics.DrawString(MaxValue.ToString(), barFont, brush, new PointF(Width - text_sizeF.Width - sliderSize, (int)((Height - text_sizeF.Height) / 2f)));
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mouseStatus = MouseStatus.Down;
            MouseDownType = InMouseDown(e.Location);
            mousePoint = e.Location;
            minValueBuf = MinValue;
            maxValueBuf = MaxValue;
            Invalidate();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mouseStatus == MouseStatus.Down)
            {
                if (MouseDownType == enum_MouseDownType.LeftSlider)
                {
                    MinValue = PointXToValue(e.Location.X) + minimum;
                }
                else if (MouseDownType == enum_MouseDownType.RightSlider)
                {
                    MaxValue = PointXToValue(e.Location.X) + minimum;
                }
                else if(MouseDownType == enum_MouseDownType.Inside)
                {
                    minValue = minValueBuf + PointXToValue(e.Location.X - mousePoint.X);
                    maxValue = maxValueBuf + PointXToValue(e.Location.X - mousePoint.X);
                    if (minValue > maxValue) minValue = maxValue;
                    if (minValue < minimum) minValue = minimum;
                    if (minValue > maximum) minValue = maximum;
                    if (maxValue > maximum) maxValue = maximum;
                    if (maxValue < minValue) maxValue = minValue;
                    ValueChanged?.Invoke(minValue, maxValue);
                }
                Invalidate();
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseStatus = MouseStatus.Up;
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            mouseStatus = MouseStatus.Enter;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            mouseStatus = MouseStatus.Leave;
        }
    }
}
