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
        private string gUID = "";
        public string GUID { get => gUID; set => gUID = value; }
        private int borderSize = 0;
        private int borderRadius = 40;
        private Color borderColor = Color.PaleVioletRed;
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
            this.BackColor = Color.MediumSlateBlue;
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

            if(this.borderRadius > 2)
            {
                using (GraphicsPath pathSurface = this.GetFigurePath(rectSurface, this.borderRadius))
                using (GraphicsPath pathBorder = this.GetFigurePath(rectBorder, this.borderRadius - 1F))
                using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    penBorder.Alignment = PenAlignment.Inset;
                    this.Region = new Region(pathSurface);
                    pevent.Graphics.DrawPath(penSurface, pathSurface);

                    if(borderSize >=1)
                    {
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                    }
                }
            }
            else
            {
                this.Region = new Region(rectSurface);
                if(this.borderRadius >= 1)
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
            //base.OnHandleCreated(e);
            this.Parent.BackColorChanged += Parent_BackColorChanged;
        }
        private void Parent_BackColorChanged(object sender, EventArgs e)
        {
            if(this.DesignMode)
            {
                this.Invalidate();
            }
        }

        private bool flag_MouseDownEventEx_done = false;
        public delegate void MouseDownEventExHandler(RJ_Button rJ_Button, MouseEventArgs mevent);
        public event MouseDownEventExHandler MouseDownEventEx;
       


        private bool flag_MouseDownEvent_done = false;
        public delegate void MouseDownEventHandler(MouseEventArgs mevent);
        public event MouseDownEventHandler MouseDownEvent;
        async protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (flag_MouseDownEvent_done)
            {
                return;
            }
            flag_MouseDownEvent_done = true;
            if (buttonType == ButtonType.Push)
            {
                EventArgs e = new EventArgs();
                base.OnMouseUp(mevent);
                base.OnMouseEnter(e);
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
                    flag_MouseDownEvent_done = false;

                }));
                await task;
                flag_MouseDownEvent_done = false;

            }
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



    }
}
