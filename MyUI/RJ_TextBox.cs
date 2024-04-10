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
    [DefaultEvent("_TextChanged")]
    public partial class RJ_TextBox : UserControl
    {
        private string gUID = "";
        public string GUID { get => gUID; set => gUID = value; }
        private Color borderColor = Color.MediumSlateBlue;
        private int borderSize = 2;
        private bool underlineStyle = false;
        private Color borderFocusColor = Color.HotPink;
        public bool IsFocused = false;
        private bool showTouchPannel = false;
        private int borderRadius = 0;
        private Color placeholderColor = Color.DarkGray;
        private string placeholderText = "";
        private bool isPlaceholder = false;
        private bool isPasswordChar = false;
        public RJ_Button KeypressEnterButton;
        public virtual string Text
        {
            get
            {
                if (isPlaceholder) return "";
                else return textBox1.Text;
            }
            set
            {
                this.Invoke(new Action(delegate 
                {
                    textBox1.Text = value;
                    SetPlcaeHolder();
                }));    
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
        public bool UnderlineStyle
        {
            get
            {
                return underlineStyle;
            }
            set
            {
                underlineStyle = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public bool PassWordChar
        {
            get
            {
                return isPasswordChar;
            }
            set
            {
                isPasswordChar = value;
                //if (value)
                //{
                //    textBox1.PasswordChar = '*';
                //}
                //else
                //{
                //    textBox1.PasswordChar = new char();
                //}
                //textBox1.UseSystemPasswordChar = value;
            }
        }
        [Category("RJ Code Advance")]
        public bool Multiline
        {
            get
            {
                return textBox1.Multiline;
            }
            set
            {
                textBox1.Multiline = value;
            }
        }
        [Category("RJ Code Advance")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                textBox1.BackColor = value;
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
                textBox1.ForeColor = value;
            }
        }
        [Category("RJ Code Advance")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                textBox1.Font = value;
                if(this.DesignMode)
                {
                    this.UpdateControlHeight();
                }
            }
        }
        [Category("RJ Code Advance")]
        public string Texts
        {
            get
            {
                if (isPlaceholder) return "";
                else return textBox1.Text;
            }
            set
            {
                if(this.IsHandleCreated)
                {
                    this.Invoke(new Action(delegate
                    {
                        if (isPlaceholder && value == "") return;
                        textBox1.Text = value;
                        RemovePlcaeHolder();
                        textBox1.Text = value;
                        RemovePlcaeHolder();
                    }));
                }
                        
            }
        }
        [Category("RJ Code Advance")]
        public Color BorderFocusColor
        {
            get
            {
                return borderFocusColor;
            }
            set
            {
                borderFocusColor = value;
            }
        }
        [Category("RJ Code Advance")]
        public bool ShowTouchPannel
        {
            get
            {
                return showTouchPannel;
            }
            set
            {
                showTouchPannel = value;
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
                if (value >= 0)
                {
                    borderRadius = value;
                    this.Invalidate();
                }            
            }
        }
        [Category("RJ Code Advance")]
        public Color PlaceholderColor
        {
            get
            {
                return placeholderColor;
            }
            set
            {
                placeholderColor = value;
                if(isPasswordChar)
                {
                    textBox1.ForeColor = value;
                }
            }
        }
        [Category("RJ Code Advance")]
        public string PlaceholderText
        {
            get
            {
                return placeholderText;
            }
            set
            {
                placeholderText = value;
                textBox1.Text = "";
                SetPlcaeHolder();
            }
        }
        [Category("RJ Code Advance")]
        public HorizontalAlignment TextAlgin
        {
            get
            {
                return this.textBox1.TextAlign;
            }
            set
            {
                this.textBox1.TextAlign = value;

            }
        }

        public event EventHandler _TextChanged;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(this._TextChanged != null)
            {
                this._TextChanged.Invoke(sender, e);
            }
        }

        public RJ_TextBox()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            SetPlcaeHolder();
        }

   

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;

            if(borderRadius > 1)
            {
                var rectBorderSmooth = this.ClientRectangle;
                var rectBorder = Rectangle.Inflate(rectBorderSmooth, -borderSize, -borderSize);
                int smoothSize = borderSize > 0 ? borderSize : 1;

                using (GraphicsPath pathBorderSmooth = this.GetFigurePath(rectBorderSmooth, borderRadius))
                using (GraphicsPath pathBorder = this.GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penBorderSmooth = new Pen(this.Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    this.Region = new Region(pathBorderSmooth);

                    if (borderRadius > 15) SetTextBoxRoudRegion();
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    penBorder.Alignment = PenAlignment.Center;
                    if (IsFocused) penBorder.Color = borderFocusColor;

                    if(underlineStyle)
                    {
                        graphics.DrawPath(penBorderSmooth, pathBorderSmooth);
                        graphics.SmoothingMode = SmoothingMode.None;
                        graphics.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                    }
                    else
                    {
                        graphics.DrawPath(penBorderSmooth, pathBorderSmooth);
                        graphics.DrawPath(penBorder, pathBorder);
                    }
                }

            }
            else
            {
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    this.Region = new Region(this.ClientRectangle);
                    penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    if (!IsFocused)
                    {
                        if (underlineStyle)
                        {                   
                            graphics.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 2);
                        }
                        else
                        {
                            graphics.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
                        }
                    }
                    else
                    {
                        penBorder.Color = borderFocusColor;
                        if (underlineStyle)
                        {
                            graphics.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 2);
                        }
                        else
                        {
                            graphics.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
                        }
                    }
                }
            }

          
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.UpdateControlHeight();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if(this.DesignMode)
            {
                this.UpdateControlHeight();
            }
        }

        private void SetPlcaeHolder()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) && placeholderText != "")
            {
                if (isPasswordChar)
                {
                    textBox1.PasswordChar = new char();
                    //textBox1.UseSystemPasswordChar = false;
                }
                isPlaceholder = true;
                textBox1.Text = placeholderText;
                textBox1.ForeColor = placeholderColor;
              
            }
        }
        private void RemovePlcaeHolder()
        {
            if (isPlaceholder && placeholderText != "")
            {
                isPlaceholder = false;
                textBox1.Text = "";
                textBox1.ForeColor = this.ForeColor;
                if (isPasswordChar)
                {
                    textBox1.PasswordChar = '*';
                    //textBox1.UseSystemPasswordChar = true;
                }
            }
        }
        private void SetTextBoxRoudRegion()
        {
            GraphicsPath pathTxt;
            if(Multiline)
            {
                pathTxt = GetFigurePath(textBox1.ClientRectangle, borderRadius - borderSize);
                textBox1.Region = new Region(pathTxt);
            }
            else
            {
                pathTxt = GetFigurePath(textBox1.ClientRectangle, borderSize * 2);
                textBox1.Region = new Region(pathTxt);
            }
        }
        private void UpdateControlHeight()
        {
            if(this.textBox1.Multiline == false)
            {
                int txtHeight = TextRenderer.MeasureText("Text", this.Font).Height + 1;
                textBox1.Multiline = true;
                textBox1.MinimumSize = new Size(0, txtHeight);
                textBox1.Multiline = false;

                this.Height = textBox1.Height + this.Padding.Top + this.Padding.Bottom;
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

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }
        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
            if(showTouchPannel)
            {
                Basic.Screen.ShowInputPanel();
            }
        }
        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
            if(showTouchPannel)
            {
                Basic.Screen.HideInputPanel();
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
            if (e.KeyChar == (char)Keys.Enter)
            {
                if(KeypressEnterButton != null)
                {
                    KeypressEnterButton.onClick();
                }
            }
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(!this.DesignMode)
            {
                this.IsFocused = true;
                this.Invalidate();
                this.RemovePlcaeHolder();
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.IsFocused = false;
                this.SetPlcaeHolder();
                this.Invalidate();
        
            }
        }
    }
}
