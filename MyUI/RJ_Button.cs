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
    public class RJ_Button : Button
    {
        private bool flag_MouseMove = false;
        private bool flag_MouseDown = false;
        private MyTimer myTimer = new MyTimer();
        private string gUID = "";
        public string GUID { get => gUID; set => gUID = value; }
        private int borderSize = 0;
        private int borderRadius = 10;
        private Color borderColor = Color.PaleVioletRed;

        private int shadowSize = 0;
        private Color shadowColor = Color.DimGray;

        private ButtonType _buttonType = ButtonType.Push;
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
                if(flag_refresh) this.Invalidate();
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
                bool flag_refresh = false;
                if (value <= this.Height)
                {
                    flag_refresh = (borderRadius != value);
                    this.borderRadius = value;
                }
                else
                {
                    flag_refresh = (borderRadius != this.Height);
                    this.borderRadius = this.Height;
                }
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

        private Color _backgroundColor_buf = Color.Transparent;
        private Color _backgroundColor = Color.Coral;
        private Color backgroundColor = Color.RoyalBlue;
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
                return this.ForeColor;
            }
            set
            {
                this.ForeColor = value;
            }
        }
        [Category("RJ Code Advance")]
        public ButtonType buttonType
        {
            get
            {
                return this._buttonType;
            }
            set
            {
                this._buttonType = value;
            }
        }

        private bool autoResetState = false;
        [Category("RJ Code Advance")]
        public bool AutoResetState
        {
            get
            {
                return autoResetState;
            }
            set
            {
                autoResetState = value;
            }
        }

        private bool showLoadingForm = false;
        [Category("RJ Code Advance")]
        public bool ShowLoadingForm
        {
            get
            {
                return showLoadingForm;
            }
            set
            {
                showLoadingForm = value;
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
                if (value < 3) value = 3;
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

        public enum ButtonType
        {
            Push,
            Toggle,
        }

        public bool state = false;
        private bool state_buf = false;
        [ReadOnly(false), Browsable(false), Category(""), Description(""), DefaultValue("")]
        public bool State
        {
            get
            {
                if(this.state_buf != this.state)
                {
                    this.SetState(this.state);
                    this.state_buf = this.state;
                    if (StateChangeEvent != null) StateChangeEvent(this, this.state);
                }
                return this.state;
            }
            set
            {
                this.state = value;
                if (this.state_buf != this.state)
                {
                    this.SetState(this.state);
                    this.state_buf = this.state;
                    if (StateChangeEvent != null) StateChangeEvent(this, this.state);
                }
            }
        }

        public RJ_Button()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.BackColor = Color.RoyalBlue;
            this.ForeColor = Color.White;
            this.Resize += RJ_Button_Resize;
        }

 

        public void ResetState()
        {
            this.Invoke(new Action(delegate 
            {
                this.Enabled = false;
                this.Enabled = true;
            }));
        }
        private void SetState(bool state)
        {
            if(state)
            {
                EventArgs e = new EventArgs();
                MouseButtons mouseButtons = MouseButtons.Left;
                MouseEventArgs mouseEventArgs = new MouseEventArgs(mouseButtons, 1, 0, 0, 0);
                base.OnMouseUp(mouseEventArgs);
                base.OnMouseEnter(e);
            }
            else
            {
                EventArgs e = new EventArgs();
                MouseButtons mouseButtons = MouseButtons.Left;
                MouseEventArgs mouseEventArgs = new MouseEventArgs(mouseButtons, 1, 0, 0, 0);
                base.OnMouseUp(mouseEventArgs);
                base.OnMouseLeave(e);
            }
        }

        private void RJ_Button_Resize(object sender, EventArgs e)
        {
            if(this.borderRadius > this.Height)
            {
                this.BorderRadius = this.Height;
            }
        }

        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            return GetFigurePath(rect, radius, 0);
        }
        private GraphicsPath GetFigurePath(RectangleF rect, float radius, int offset)
        {
            GraphicsPath path = new GraphicsPath();

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
            RectangleF rectShadow = new RectangleF(0, 0, this.Width - (this.ShadowSize), this.Height - (this.ShadowSize));
            RectangleF rectSurface = new RectangleF(0, 0, this.Width - 1, this.Height - 1);
            RectangleF rectBackGround = new RectangleF(0, 0, this.Width, this.Height);
            Color bkgroundcolor = this.backgroundColor;
            if(this.Enabled == false)
            {
                bkgroundcolor = Color.Gray;
            }
            else if (flag_MouseDown)
            {
                int R = bkgroundcolor.R + (int)(bkgroundcolor.R * 0.3);
                int G = bkgroundcolor.G + (int)(bkgroundcolor.G * 0.3);
                int B = bkgroundcolor.B + (int)(bkgroundcolor.B * 0.3);
                if (R > 255) R = 255;
                if (G > 255) G = 255;
                if (B > 255) B = 255;
                bkgroundcolor = Color.FromArgb(R, G, B);
            }
            else if (flag_MouseMove)
            {
                int R = bkgroundcolor.R - (int)(bkgroundcolor.R * 0.1);
                int G = bkgroundcolor.G - (int)(bkgroundcolor.G * 0.1);
                int B = bkgroundcolor.B - (int)(bkgroundcolor.B * 0.1);
                if (R > 255) R = 255;
                if (G > 255) G = 255;
                if (B > 255) B = 255;
                bkgroundcolor = Color.FromArgb(R, G, B);
            }

            using (GraphicsPath pathSurface = this.GetFigurePath(rectSurface, this.borderRadius))
            using (GraphicsPath pathBackGround = this.GetFigurePath(rectBackGround, this.borderRadius))
            using (GraphicsPath pathShadow = this.GetFigurePath(rectShadow, this.borderRadius))
            using (GraphicsPath pathBorder = this.GetFigurePath(rectBorder, this.borderRadius))
            using (Brush brushBackgroung = new SolidBrush(bkgroundcolor))
            using (Pen penSurface = new Pen(this.Parent.BackColor, this.ShadowSize + 1))
            using (Pen penBorder = new Pen(borderColor, borderSize))
            {
                penBorder.Alignment = PenAlignment.Inset;
                this.Region = new Region(rectBackGround);
                //this.BackColor = this.Parent.BackColor;
               
                pevent.Graphics.FillPath(brushBackgroung, pathBackGround);
                if (this.ShadowSize >= 1) DrawRoundShadow(pevent.Graphics, rectShadow, this.borderRadius, this.ShadowSize);
                pevent.Graphics.DrawPath(penSurface, pathBackGround);

                if (this.Enabled == true)
                {
                    if (this.BorderSize >= 1) pevent.Graphics.DrawPath(penBorder, pathBorder);
                }

                Rectangle rectangle_text = new Rectangle((int)rectShadow.X, (int)rectShadow.Y, (int)rectShadow.Width, (int)rectShadow.Height);
                SizeF size_Text_temp = pevent.Graphics.MeasureString(this.Text, this.Font, new SizeF(rectangle_text.Width, rectangle_text.Height), StringFormat.GenericDefault);
                Size size_Text = new Size((int)size_Text_temp.Width, (int)size_Text_temp.Height);
                Point point = new Point(0, 0);
                if(TextAlign == ContentAlignment.TopLeft)
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
                Color foreColor = this.ForeColor;
                if (this.Enabled == false) foreColor = Color.LightGray;
                pevent.Graphics.DrawString($"{this.Text}", this.Font, new SolidBrush(foreColor), point, StringFormat.GenericDefault);
               
                if(this.Enabled == false)DrawProhibitionSymbol(pevent.Graphics, new Point((int)((rectShadow.X + rectShadow.Width) / 2), (int)((rectShadow.Y + rectShadow.Height) / 2)), 30, 4, 1);

            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);

         
        }

        private bool flag_MouseDownEventEx_done = false;
        public delegate void MouseDownEventExHandler(RJ_Button rJ_Button, MouseEventArgs mevent);
        public event MouseDownEventExHandler MouseDownEventEx;
       


        private bool flag_MouseDownEvent_done = false;
        public delegate void MouseDownEventHandler(MouseEventArgs mevent);
        public event MouseDownEventHandler MouseDownEvent;
        async protected override void OnMouseDown(MouseEventArgs mevent)
        {
            flag_MouseDown = true;
            if (flag_MouseDownEvent_done)
            {
                return;
            }
            myTimer.TickStop();
            myTimer.StartTickTime(999999);
            flag_MouseDownEvent_done = true;
            if(showLoadingForm)
            {
                if(this.MouseDownEvent != null || this.MouseDownEventEx != null)
                {
                    LoadingForm loadingForm = LoadingForm.getLoading();
                    Task.Run(new Action(delegate
                    {
                        loadingForm.ShowDialog();
                    }));
              
                }
            }
            if (buttonType == ButtonType.Push)
            {
                EventArgs e = new EventArgs();
                base.OnMouseUp(mevent);
                base.OnMouseEnter(e);
                Task task = Task.Factory.StartNew(new Action(delegate
                {
                    if (this.MouseDownEvent != null)
                    {
                        this.MouseDownEvent(mevent);
                    }
                    if (this.MouseDownEventEx != null)
                    {
                        this.MouseDownEventEx(this, mevent);
                    }
                    if (AutoResetState)
                    {
                        this.Invoke(new Action(delegate
                        {
                            if (this.CanSelect)
                            {
                                this.ResetState();
                            }
                        }));
                    }
                    if (myTimer.GetTickTime() < 2000 && showLoadingForm)
                    {
                        System.Threading.Thread.Sleep(2000 - (int)myTimer.GetTickTime());
                    }

                }));
             
                await task;
                flag_MouseDownEvent_done = false;
            }
            else
            {
                EventArgs e = new EventArgs();

                base.OnMouseEnter(e);
                base.OnMouseUp(mevent);
                Task task = Task.Factory.StartNew(new Action(delegate
                {
                    if (this.MouseDownEvent != null) this.MouseDownEvent(mevent);
                    if (this.MouseDownEventEx != null) this.MouseDownEventEx(this, mevent);
                    if (AutoResetState)
                    {
                        this.Invoke(new Action(delegate
                        {
                            if (this.CanSelect)
                            {
                                this.ResetState();
                            }
                        }));
                    }
                    if (myTimer.GetTickTime() < 2000 && showLoadingForm)
                    {
                        System.Threading.Thread.Sleep(2000 - (int)myTimer.GetTickTime());
                    }
                    flag_MouseDownEvent_done = false;

                }));
                await task;
                flag_MouseDownEvent_done = false;

            }
           
            LoadingForm.getLoading().CloseLoadingForm();
            this.OnMouseClick(mevent);
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (buttonType == ButtonType.Push)
            {
                base.OnMouseEnter(e);
            }
            else
            {
               
            }
        }
 
        protected override void OnMouseEnter(EventArgs e)
        {
            if (buttonType == ButtonType.Push)
            {
                MouseButtons mouseButtons = MouseButtons.Left;
                MouseEventArgs mouseEventArgs = new MouseEventArgs(mouseButtons, 1, 0, 0, 0);
                base.OnMouseDown(mouseEventArgs);
            }
            else
            {

            }
        }

        public delegate void MouseLeaveEventHandler(EventArgs e);
        public event MouseLeaveEventHandler MouseLeaveEvent;
        protected override void OnMouseLeave(EventArgs e)
        {
            flag_MouseMove = false;
            if (buttonType == ButtonType.Push)
            {
                MouseButtons mouseButtons = MouseButtons.Left;
                MouseEventArgs mouseEventArgs = new MouseEventArgs(mouseButtons, 1, 0, 0, 0);
                base.OnMouseUp(mouseEventArgs);
                base.OnMouseLeave(e);
            }
            else
            {
                if (this.MouseLeaveEvent != null) this.MouseLeaveEvent(e);

                this.OnMouseUp(null);
            }
        }

        public delegate void MouseMoveEventHandler(MouseEventArgs mevent);
        public event MouseMoveEventHandler MouseMoveEvent;
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            flag_MouseMove = true;
            if (buttonType == ButtonType.Push)
            {
            }
            else
            {
                if (this.MouseMoveEvent != null) this.MouseMoveEvent(mevent);
            }
        }

        public delegate void MouseUpEventHandler(MouseEventArgs mevent);
        public event MouseUpEventHandler MouseUpEvent;
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            flag_MouseDown = false;
            if (buttonType == ButtonType.Push)
            {
                EventArgs e = new EventArgs();
                MouseButtons mouseButtons = MouseButtons.Left;
                MouseEventArgs mouseEventArgs = new MouseEventArgs(mouseButtons, 1, 0, 0, 0);
                base.OnMouseUp(mouseEventArgs);
                base.OnMouseLeave(e);
            }
            else
            {
                if (this.MouseUpEvent != null) this.MouseUpEvent(mevent);
            }
        }
        public void OnMouseUpEvent(MouseEventArgs mevent)
        {
            this.OnMouseUp(mevent);
        }
        public delegate void MouseClickEventHandler(MouseEventArgs mevent);
        public event MouseClickEventHandler MouseClickEvent;
        protected override void OnMouseClick(MouseEventArgs mevent)
        {
            if (buttonType == ButtonType.Push)
            {
                base.OnMouseEnter(mevent);
            }
            else
            {
                if (MouseClickEvent != null) this.MouseClickEvent(mevent);
            }
        }

        public delegate void StateChangeEventHandler(RJ_Button rJ_Button, bool state);
        public event StateChangeEventHandler StateChangeEvent;

        private void DrawProhibitionSymbol(Graphics g, Point center, int size, int lineWidth, int borderLineWidth)
        {
            // 计算符号绘制的起始坐标
            int startX = center.X - size / 2;
            int startY = center.Y - size / 2;


            // 画红色圆圈和红色斜线
            using (Pen redPen = new Pen(Color.Red, lineWidth))
            {
                // 绘制红色圆圈
                g.DrawEllipse(redPen, startX, startY, size, size);

                // 绘制红色斜线
                g.DrawLine(redPen, startX + size * 0.2f, startY + size * 0.8f, startX + size * 0.8f, startY + size * 0.2f);
            }
        }
    }
}
