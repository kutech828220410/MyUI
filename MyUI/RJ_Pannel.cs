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

        private int shadowSize = 0;
        private Color shadowColor = Color.DimGray;

        private Color backgroundColor = Color.Transparent;

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
            return GetFigurePath(rect, radius, 0);
        }
        private GraphicsPath GetFigurePath(RectangleF rect, float radius, int offset)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius == 0) radius = 1;
            path.StartFigure();
            path.AddArc(rect.X + 0, rect.Y + 0, radius, radius, 180, 90);
            path.AddArc(rect.Width + offset - radius, rect.Y + 0, radius, radius, 270, 90);
            path.AddArc(rect.Width + offset - radius, rect.Height + offset - radius, radius, radius, 0, 90);
            path.AddArc(rect.X + 0, rect.Height + offset - radius, radius, radius, 90, 90);
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
