using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Collections;
using System.Windows.Forms.Design;

namespace MyUI
{
    public class RJ_DataGridView : DataGridView
    {
        private int borderSize = 2;
        private int borderRadius = 10;
        private Color borderColor = Color.LightBlue;

        [Category("RJ Code - Appearence")]
        public Font columnHeaderFont
        {
            get
            {
                return this.ColumnHeadersDefaultCellStyle.Font;
            }
            set
            {
                this.ColumnHeadersDefaultCellStyle.Font = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public Color columnHeaderBackColor
        {
            get
            {
                return this.ColumnHeadersDefaultCellStyle.BackColor;
            }
            set
            {
                this.ColumnHeadersDefaultCellStyle.BackColor = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public DataGridViewHeaderBorderStyle columnHeadersBorderStyle
        {
            get
            {
                return this.ColumnHeadersBorderStyle;
            }
            set
            {
                this.ColumnHeadersBorderStyle = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public DataGridViewColumnHeadersHeightSizeMode columnHeadersHeightSizeMode
        {
            get
            {
                return this.ColumnHeadersHeightSizeMode;
            }
            set
            {
                this.ColumnHeadersHeightSizeMode = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code - Appearence")]
        public int columnHeadersHeight
        {
            get
            {
                return this.ColumnHeadersHeight;
            }
            set
            {
                this.ColumnHeadersHeight = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public Color rowHeaderBackColor
        {
            get
            {
                return this.RowHeadersDefaultCellStyle.BackColor;
            }
            set
            {
                this.RowHeadersDefaultCellStyle.BackColor = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public DataGridViewHeaderBorderStyle rowHeadersBorderStyle
        {
            get
            {
                return this.RowHeadersBorderStyle;
            }
            set
            {
                this.RowHeadersBorderStyle = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public Font cellStyleFont
        {
            get
            {
                return this.DefaultCellStyle.Font;
            }
            set
            {
                this.DefaultCellStyle.Font = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public Color cellStylForeColor
        {
            get
            {
                return this.DefaultCellStyle.ForeColor;
            }
            set
            {
                this.DefaultCellStyle.ForeColor = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public Color cellStylBackColor
        {
            get
            {
                return this.DefaultCellStyle.BackColor;
            }
            set
            {
                this.DefaultCellStyle.BackColor = value;
            }
        }
        [Category("RJ Code - Appearence")]
        public DataGridViewCellBorderStyle cellBorderStyle
        {
            get
            {
                return this.CellBorderStyle;
            }
            set
            {
                this.CellBorderStyle = value;
            }
        }
        [Category("RJ Code - Appearence")]
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
        [Category("RJ Code - Appearence")]
        public int BorderRadius
        {
            get
            {
                return borderRadius;
            }
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code - Appearence")]
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
     

        public RJ_DataGridView()
        {
            this.columnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.BackgroundColor = Color.SkyBlue;
            this.EnableHeadersVisualStyles = false;

            this.rowHeaderBackColor = Color.CornflowerBlue;
            this.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.RowHeadersVisible = true;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.columnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.columnHeadersHeight = 40;
            this.columnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.columnHeaderFont = new Font("微軟正黑體", 12 , FontStyle.Bold);
            this.columnHeaderBackColor = Color.SkyBlue;
            this.columnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            this.cellStyleFont = new Font("微軟正黑體", 12, FontStyle.Bold);
            this.cellStylForeColor = Color.Black;
            this.cellStylBackColor = Color.LightBlue;
            this.cellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;

           
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
