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
        private int borderSize = 0;
        private int borderRadius = 40;
        private Color borderColor = Color.PaleVioletRed;

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
                return this.BackColor;
            }
            set
            {
                this.BackColor = value;
            }
        }
        [Category("RJ Code Advance")]
        public Color TextColor
        {
            get
            {
                return this.ForeColor;
            }
            set
            {
                this.ForeColor = value;
            }
        }


        public RJ_Lable()
        {
            this.AutoSize = false;
            this.FlatStyle = FlatStyle.Flat;          
            this.Size = new Size(150, 40);
            this.BackColor = Color.MediumSlateBlue;
            this.ForeColor = Color.White;
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
            GraphicsPath path = new GraphicsPath();

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            return path;
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            this.AutoSize = false;
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF rectBorder = new RectangleF(1, 1, this.Width - 0.8F, this.Height - 1);

            if (this.borderRadius > 2)
            {
                using (GraphicsPath pathSurface = this.GetFigurePath(rectSurface, this.borderRadius))
                using (GraphicsPath pathBorder = this.GetFigurePath(rectBorder, this.borderRadius - 1F))
                using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    penBorder.Alignment = PenAlignment.Inset;
                    this.Region = new Region(pathSurface);
                    pevent.Graphics.DrawPath(penSurface, pathSurface);

                    if (borderSize >= 1)
                    {
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                    }
                }
            }
            else
            {
                this.Region = new Region(rectSurface);
                if (this.borderRadius >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
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
