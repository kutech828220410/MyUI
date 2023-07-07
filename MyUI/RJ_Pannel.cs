using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using Basic;
namespace MyUI
{
    public class RJ_Pannel : Panel
    {
        [Serializable]
        public class JaonstringClass
        {

            private int borderSize = 2;
            private int borderRadius = 10;

            [JsonIgnore]
            public Color BorderColor = Color.SkyBlue;
            [Browsable(false)]
            public string BorderColor_Serialize
            {
                get { return Basic.Net.ColorSerializationHelper.ToString(BorderColor); }
                set { BorderColor = Basic.Net.ColorSerializationHelper.FromString(value); }
            }

            private Point location;
            private Size size;
            private string gUID = "";
            public string GUID { get => gUID; set => gUID = value; }

            public int BorderSize { get => borderSize; set => borderSize = value; }
            public int BorderRadius { get => borderRadius; set => borderRadius = value; }
            public Point Location { get => location; set => location = value; }
            public Size Size { get => size; set => size = value; }
       

            public static string GetJaonstring(RJ_Pannel control)
            {
                JaonstringClass jaonstringClass = new JaonstringClass();
                jaonstringClass.BorderSize = control.BorderSize;
                jaonstringClass.BorderRadius = control.BorderRadius;
                jaonstringClass.BorderColor = control.BorderColor;
                jaonstringClass.Location = control.Location;
                jaonstringClass.Size = control.Size;

                return jaonstringClass.JsonSerializationt();
            }
            public static RJ_Pannel SetJaonstring(string jsonstring)
            {
                JaonstringClass jaonstringClass = jsonstring.JsonDeserializet<JaonstringClass>();
                RJ_Pannel rJ_Pannel = new RJ_Pannel();
                rJ_Pannel.BorderSize = jaonstringClass.BorderSize;
                rJ_Pannel.BorderRadius = jaonstringClass.BorderRadius;
                rJ_Pannel.BorderColor = jaonstringClass.BorderColor;
                rJ_Pannel.Location = jaonstringClass.Location;
                rJ_Pannel.Size = jaonstringClass.Size;

                return rJ_Pannel;
            }
        }
        
        private bool isSelected = false;
        [Browsable(false)]
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                this.isSelected = value;
                if (this.isSelected)
                {
                    this.BorderColor = this.borderColor;
                    this.Invalidate();
                }
                else
                {
                    this.BorderColor = this.borderColor;
                    this.Invalidate();
                }
            }
        }
        public string GUID = "";
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
            this.GUID = Guid.NewGuid().ToString();
         
        }


        private void RJ_Pannel_Resize(object sender, EventArgs e)
        {
            if (this.borderRadius > this.Height)
            {
                this.BorderRadius = this.Height;
            }
            this.Invalidate();
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
            Color mborderColor = borderColor;
            if (this.isSelected) mborderColor = Color.Blue;
            if (this.borderRadius > 2)
            {
                using (GraphicsPath pathSurface = this.GetFigurePath(rectSurface, this.borderRadius))
                using (GraphicsPath pathBorder = this.GetFigurePath(rectBorder, this.borderRadius - 1F))
                using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
                using (Pen penBorder = new Pen(mborderColor, borderSize))
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
                    using (Pen penBorder = new Pen(mborderColor, borderSize))
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
