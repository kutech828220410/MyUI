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
    public class RJ_Pannel : Panel
    {
        private int borderSize = 2;
        private int borderRadius = 10;
        private Color borderColor = Color.SkyBlue;

        [Category("RJ Code Advance")]
        public int BorderSize
        {
            get
            {
                return borderSize;
            }
            set
            {
                bool flag_refresh = (borderSize != value);
                borderSize = value;
                if (flag_refresh) this.Invalidate();
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
                bool flag_refresh = (borderRadius != value);
                borderRadius = value;
                if (flag_refresh) this.Invalidate();
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
                bool flag_refresh = (borderColor != value);
                borderColor = value;
                if (flag_refresh) this.Invalidate();
            }
        }

        public RJ_Pannel()
        {
            this.BorderStyle = BorderStyle.None;
            this.Size = new Size(400, 300);
            this.BackColor = Color.White;
            this.ForeColor = Color.White;
            this.Resize += RJ_Pannel_Resize;
        }


        private void RJ_Pannel_Resize(object sender, EventArgs e)
        {
            if (this.borderRadius > this.Height)
            {
                this.BorderRadius = this.Height;
            }
        }
        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius < 0) radius = 0;
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
            //this.Parent.BackColorChanged += Parent_BackColorChanged;
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
