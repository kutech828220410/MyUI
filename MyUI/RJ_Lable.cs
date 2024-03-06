using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyUI
{
    public class RJ_Lable :Label
    {
        private string gUID = "";
        public string GUID { get => gUID; set => gUID = value; }
        private Color textColor = Color.White;
        private Color backgroundColor = Color.RoyalBlue;
        private int borderSize = 0;
        private int borderRadius = 10;
        private Color borderColor = Color.PaleVioletRed;

        private int shadowSize = 0;
        private Color shadowColor = Color.DimGray;

        [Category("RJ Code Advance")]
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
        [Category("RJ Code Advance")]
        public int BorderRadius
        {
            get
            {
                return borderRadius;
            }
            set
            {
                if (value <= this.Height)
                {
                    this.borderRadius = value;
                }
                else
                {
                    this.borderRadius = this.Height;
                }
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
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
        [Category("RJ Code Advance")]
        public Color BackgroundColor
        {
            get
            {
                return this.backgroundColor;
            }
            set
            {
                this.backgroundColor = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                textColor = value;
            }
        }
        [Category("RJ Code Advance")]
        public int ShadowSize
        {
            get
            {
                return shadowSize;
            }
            set
            {
                if (value < 3 && value != 0) value = 3;
                shadowSize = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public Color ShadowColor
        {
            get
            {
                return shadowColor;
            }
            set
            {
                shadowColor = value;
                this.Invalidate();
            }
        }

        public RJ_Lable()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.AutoSize = false;
            this.FlatStyle = FlatStyle.Flat;          
            this.Size = new Size(150, 40);
            this.BackColor = Color.MediumSlateBlue;
            this.ForeColor = Color.Transparent;
            this.Resize += RJ_Label_Resize;         
            this.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void RJ_Label_Resize(object sender, EventArgs e)
        {
            if (this.borderRadius > this.Height)
            {
                this.BorderRadius = this.Height;
            }
        }
        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            return GetFigurePath(rect, radius, 0);
        }
        private GraphicsPath GetFigurePath(RectangleF rect, float radius ,int offset)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius <= 0) radius = 1;
            path.StartFigure();
            path.AddArc(rect.X + 0, rect.Y + 0, radius, radius, 180, 90);
            path.AddArc(rect.Width + offset - radius, rect.Y+ 0, radius, radius, 270, 90);
            path.AddArc(rect.Width + offset - radius, rect.Height+ offset - radius, radius, radius, 0, 90);
            path.AddArc(rect.X+ 0, rect.Height+ offset - radius, radius, radius, 90, 90);
            path.CloseFigure();

            return path;
        }
        public void DrawRoundShadow(Graphics g, RectangleF rect, float radius, int width)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int penWidth = 3;
            int index = 1;
            using (Pen pen = new Pen(ShadowColor, penWidth))
            {
                int color_temp_R = ShadowColor.R;
                int offset_color_R = (254 - ShadowColor.R) / (width + penWidth);

                int color_temp_G = ShadowColor.G;
                int offset_color_G = (254 - ShadowColor.G) / (width + penWidth);

                int color_temp_B = ShadowColor.B;
                int offset_color_B = (254 - ShadowColor.B) / (width + penWidth);

                for (int i = -penWidth; i < width; i++)
                {
                    color_temp_R += offset_color_R;
                    color_temp_G += offset_color_G;
                    color_temp_B += offset_color_B;
                    pen.Color = Color.FromArgb(255, color_temp_R, color_temp_G, color_temp_B);
                    using (GraphicsPath pathBorder = this.GetFigurePath(rect, radius, i))
                    {
                        g.DrawPath(pen, pathBorder);
                    }
                }
            }
        }

    
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            this.AutoSize = false;
         
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rectBorder = new RectangleF(0, 0, this.Width - (this.ShadowSize + this.BorderSize), this.Height - (this.ShadowSize + this.BorderSize));
            if (this.ShadowSize == 0) rectBorder = new RectangleF(0, 0, this.Width - (1), this.Height - (1));
            RectangleF rectShadow = new RectangleF(0, 0, this.Width - (this.ShadowSize), this.Height - (this.ShadowSize));
            RectangleF rectSurface = new RectangleF(0, 0, this.Width - 1, this.Height - 1);
            RectangleF rectBackGround = new RectangleF(0, 0, this.Width, this.Height);

            using (GraphicsPath pathSurface = this.GetFigurePath(rectSurface, this.borderRadius))
            using (GraphicsPath pathBackGround = this.GetFigurePath(rectBackGround, this.borderRadius))
            using (GraphicsPath pathShadow = this.GetFigurePath(rectShadow, this.borderRadius))
            using (GraphicsPath pathBorder = this.GetFigurePath(rectBorder, this.borderRadius))
            using (Brush brushBackgroung = new SolidBrush(this.BackgroundColor))
            using (Pen penSurface = new Pen(this.Parent.BackColor, this.ShadowSize + 1))
            using (Pen penBorder = new Pen(borderColor, borderSize))
            {
                penBorder.Alignment = PenAlignment.Inset;
                this.Region = new Region(rectBackGround);
                this.BackColor = this.Parent.BackColor;
                pevent.Graphics.FillPath(brushBackgroung, pathBackGround);
                if (this.ShadowSize >= 1) DrawRoundShadow(pevent.Graphics, rectShadow, this.borderRadius, this.ShadowSize);
                pevent.Graphics.DrawPath(penSurface, pathBackGround);
                if (this.BorderSize >= 1) pevent.Graphics.DrawPath(penBorder, pathBorder);

                Rectangle rectangle_text = new Rectangle((int)rectShadow.X, (int)rectShadow.Y, (int)rectShadow.Width, (int)rectShadow.Height);
                SizeF size_Text_temp = pevent.Graphics.MeasureString(this.Text, this.Font, new SizeF(rectangle_text.Width, rectangle_text.Height), StringFormat.GenericDefault);
                Size size_Text = new Size((int)size_Text_temp.Width, (int)size_Text_temp.Height);
                Point point = new Point(0, 0);
                if (TextAlign == ContentAlignment.TopLeft)
                {
                    point = new Point(0, 0);
                }
                else if (TextAlign == ContentAlignment.TopCenter)
                {
                    point = new Point((rectangle_text.Width - size_Text.Width) / 2, 0);
                }
                else if (TextAlign == ContentAlignment.TopRight)
                {
                    point = new Point((rectangle_text.Width - size_Text.Width), 0);
                }
                else if (TextAlign == ContentAlignment.MiddleLeft)
                {
                    point = new Point(0, (rectangle_text.Height - size_Text.Height) / 2);
                }
                else if (TextAlign == ContentAlignment.MiddleCenter)
                {
                    point = new Point((rectangle_text.Width - size_Text.Width) / 2, (rectangle_text.Height - size_Text.Height) / 2);
                }
                else if (TextAlign == ContentAlignment.MiddleRight)
                {
                    point = new Point((rectangle_text.Width - size_Text.Width), (rectangle_text.Height - size_Text.Height) / 2);
                }
                else if (TextAlign == ContentAlignment.BottomLeft)
                {
                    point = new Point(0, (rectangle_text.Height - size_Text.Height));
                }
                else if (TextAlign == ContentAlignment.BottomCenter)
                {
                    point = new Point((rectangle_text.Width - size_Text.Width) / 2, (rectangle_text.Height - size_Text.Height));
                }
                else if (TextAlign == ContentAlignment.BottomRight)
                {
                    point = new Point((rectangle_text.Width - size_Text.Width), (rectangle_text.Height - size_Text.Height));
                }

                pevent.Graphics.DrawString($"{this.Text}", this.Font, new SolidBrush(this.TextColor), point, StringFormat.GenericDefault);
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += Parent_BackColorChanged;
        }
        private void Parent_BackColorChanged(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                this.Invalidate();
            }
        }
    }
}
