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
    public enum TextPosition
    {
        Left,
        Right,
        Center,
        Sliding,
        None,
    }
    public class RJ_ProgressBar : ProgressBar
    {
        private Color channelColor = Color.LightSteelBlue;
        private Color sliderColor = Color.RoyalBlue;
        private Color foreBackColor = Color.RoyalBlue;
        private int channelHeight = 6;
        private int sliderHeight = 6;
        private TextPosition showValue = TextPosition.Right;

        private bool paintedBack = false;
        private bool stopPainting = false;
        private string symbolBefore = "";
        private string symbolAfter = "";
        private bool showMaximun = false;

        public RJ_ProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.ForeColor = Color.White;
            Basic.Reflection.MakeDoubleBuffered(this, true);
        }

        public int Value
        {
            set
            {
                this.Invoke(new Action(delegate
                {
                    base.Value = value;
                }));
            }
            get
            {
                return base.Value;
            }
        }
        public int Maximum
        {
            set
            {
                this.Invoke(new Action(delegate
                {
                    base.Maximum = value;
                }));
            }
            get
            {
                return base.Maximum;
            }
        }
        public int Minimum
        {
            set
            {
                this.Invoke(new Action(delegate
                {
                    base.Minimum = value;
                }));
            }
            get
            {
                return base.Minimum;
            }
        }
        [Category("RJ Code Advance")]
        public Color ChannelColor
        {
            get
            {
                return channelColor;
            }
            set
            {
                channelColor = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public Color SliderColor
        {
            get
            {
                return sliderColor;
            }
            set
            {
                sliderColor = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public Color ForeBackColor
        {
            get
            {
                return foreBackColor;
            }
            set
            {
                foreBackColor = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public int ChannelHeight
        {
            get
            {
                return channelHeight;
            }
            set
            {
                channelHeight = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public int SliderHeight
        {
            get
            {
                return sliderHeight;
            }
            set
            {
                sliderHeight = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public TextPosition ShowValue
        {
            get
            {
                return showValue;
            }
            set
            {
                showValue = value;
                this.Invalidate();
            }
        }
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }
        [Category("RJ Code Advance")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }
        [Category("RJ Code Advance")]
        public bool ShowMaximun
        {
            get
            {
                return showMaximun;
            }
            set
            {
                showMaximun = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public string SymbolBefore
        {
            get
            {
                return symbolBefore;
            }
            set
            {
                symbolBefore = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public string SymbolAfter
        {
            get
            {
                return symbolAfter;
            }
            set
            {
                symbolAfter = value;
                this.Invalidate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if(stopPainting == false)
            {
                paintedBack = false;
                if (paintedBack == false)
                {
                    Graphics graphics = pevent.Graphics;
                    Rectangle rectChnnel = new Rectangle(0, 0, this.Width, channelHeight);
                    using (var brushChannel = new SolidBrush(channelColor))
                    {
                        if(channelHeight >= sliderHeight)
                        {
                            rectChnnel.Y = this.Height - channelHeight;                      
                        }
                        else
                        {
                            rectChnnel.Y = this.Height - ((channelHeight + sliderHeight) / 2);
                        }
                        graphics.Clear(this.Parent.BackColor);
                        graphics.FillRectangle(brushChannel, rectChnnel); 
                        if(this.DesignMode == false)
                        {
                            paintedBack = true;
                        }
                    }
                }

                if(this.Value == this.Maximum || this.Value == this.Minimum)
                {
                    paintedBack = false;
                }
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (stopPainting == false)
            {
                Graphics graphics = e.Graphics;
                double scaleFactor = (((double)this.Value - this.Minimum) / ((double)this.Maximum - this.Minimum));
                int sliderWidth = (int)(this.Width * scaleFactor);
                Rectangle rectSlider = new Rectangle(0, 0, sliderWidth, sliderHeight);
                using (var brushSlider = new SolidBrush(sliderColor))
                {
                    if(sliderHeight >= channelHeight)
                    {
                        rectSlider.Y = this.Height - sliderHeight;
                    }
                    else
                    {
                        rectSlider.Y = this.Height - ((sliderHeight + channelHeight) / 2);
                    }
                    if(sliderWidth > 1)
                    {
                        graphics.FillRectangle(brushSlider, rectSlider);
                    }
                    if(showValue != TextPosition.None)
                    {
                        DrawValueText(graphics, sliderWidth, rectSlider);
                    }
                }
            }
            if (this.Value == this.Maximum) stopPainting = true;
            else stopPainting = false;
        }

        private void DrawValueText(Graphics graphics, int sliderWidth, Rectangle rectSlider)
        {
            string text =(((double)this.Value / (double)this.Maximum) * 100).ToString("0.000") + "%";
            if (showMaximun)
            {
                text = this.Value.ToString();
                if (showMaximun) text = text + "/" + symbolBefore + this.Maximum.ToString() + symbolAfter;
            }
            var textSize = TextRenderer.MeasureText(text, this.Font);
            var rectText = new Rectangle(0, 0, textSize.Width, textSize.Height + 2);
            using (var brushText = new SolidBrush(this.ForeColor))
            using (var brushTextBack = new SolidBrush(foreBackColor))
            using (var textFormat = new StringFormat())
            {
                switch(ShowValue)
                {
                    case TextPosition.Left:
                        rectText.X = 0;
                        textFormat.Alignment = StringAlignment.Near;
                        break;
                    case TextPosition.Right:
                        rectText.X = this.Width - textSize.Width;
                        textFormat.Alignment = StringAlignment.Far;
                        break;
                    case TextPosition.Center:
                        rectText.X = (this.Width - textSize.Width) / 2;
                        textFormat.Alignment = StringAlignment.Near;
                        break;
                    case TextPosition.Sliding:
                        rectText.X = sliderWidth - textSize.Width;
                        textFormat.Alignment = StringAlignment.Center;

                        using (var brushClear = new SolidBrush(this.Parent.BackColor))
                        {
                            var rect = rectSlider;
                            rect.Y = rectText.Y;
                            rect.Height = rectText.Height;
                            graphics.FillRectangle(brushClear, rect);
                        }
                        break;                       
                }
                graphics.FillRectangle(brushTextBack, rectText);
                graphics.DrawString(text, this.Font, brushText, rectText, textFormat);
            }

        }
    }
}
