using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LadderConnection;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Media;

namespace MyUI
{
    public class RJ_CheckBox : CheckBox
    {
        private string gUID = "";
        public string GUID { get => gUID; set => gUID = value; }
        private Color onBackColor = Color.MediumSlateBlue;
        private Color onToggleColor = Color.WhiteSmoke;
        private Color offBackColor = Color.Gray;
        private Color offToggleColor = Color.Gainsboro;
        private bool solidStyle = true;

        [Category("RJ Data Advance")]
        public Color OnBackColor
        {
            get
            {
               return onBackColor;
            }
            set
            {
                onBackColor = value;
                this.Invalidate();
            }
        }
        [Category("RJ Data Advance")]
        public Color OnToggleColor
        {
            get
            {
               return onToggleColor;
            }
            set
            {
                onToggleColor = value;
                this.Invalidate();
            }
        }
        [Category("RJ Data Advance")]
        public Color OffBackColor
        {
            get
            {
                return offBackColor;
            }
            set
            {
                offBackColor = value;
                this.Invalidate();
            }
        }
        [Category("RJ Data Advance")]
        public Color OffToggleColor
        {
            get
            {
                return offToggleColor;
            }
            set
            {
                offToggleColor = value;
                this.Invalidate();
            }
        }
        [Category("RJ Data Advance")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
            }
        }
        [Category("RJ Data Advance")]
        public bool SolidStyle
        {
            get
            {
               return solidStyle;
            }

            set
            {
                solidStyle = value;
                this.Invalidate();
            }
        }

        public RJ_CheckBox()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.MinimumSize = new Size(45, 22);
        }
        private GraphicsPath GetFigurePath()
        {
            int acrSize = this.Height - 1;
            Rectangle leftArc = new Rectangle(0, 0, acrSize, acrSize);
            Rectangle rightArc = new Rectangle(this.Width - acrSize - 2, 0, acrSize, acrSize);
            GraphicsPath Path = new GraphicsPath();
            Path.StartFigure();
            Path.AddArc(leftArc, 90, 180);
            Path.AddArc(rightArc, 270, 180);
            Path.CloseFigure();
            return Path;

        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            int toggleSize = this.Height - 5;
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.Clear(this.Parent.BackColor);

            if (this.Checked)
            {
                if(this.solidStyle)
                {
                    pevent.Graphics.FillPath(new SolidBrush(OnBackColor), this.GetFigurePath());
                }
                else
                {
                    pevent.Graphics.DrawPath(new Pen(OnBackColor, 2), this.GetFigurePath());
                }
                pevent.Graphics.FillEllipse(new SolidBrush(OnToggleColor), new Rectangle(this.Width - this.Height + 1, 2, toggleSize, toggleSize));
            }
            else
            {
                if (this.solidStyle)
                {
                    pevent.Graphics.FillPath(new SolidBrush(OffBackColor), this.GetFigurePath());
                }
                else
                {
                    pevent.Graphics.DrawPath(new Pen(offBackColor, 2), this.GetFigurePath());
                }      
                pevent.Graphics.FillEllipse(new SolidBrush(OffToggleColor), new Rectangle(2, 2, toggleSize, toggleSize));
            }
        }
    }

}
