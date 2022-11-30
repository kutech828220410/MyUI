using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Design;
namespace MyUI
{
    [DefaultEvent("OnSelectedIndexChanged")]
    public class RJ_ComboBox : UserControl
    {
        private Color backColor = Color.WhiteSmoke;
        private Color iconColor = Color.RoyalBlue;
        private Color listBackColor = Color.FromArgb(230, 228, 245);
        private Color listTextColor = Color.DimGray;
        private Color borderColor = Color.MediumSlateBlue;
        private int borderSize = 1;

        private ComboBox cmbList;
        private Label lblText;
        private Button btnIcon;

        public string Text
        {
            get
            {
                return this.Texts;
            }
            set
            {
                this.Invoke(new Action(delegate 
                {
                    this.Texts = value;
                    this.cmbList.SelectedItem = value;
                }));
       
            }
        }
        [Category("RJ Code - Appearance")]
        public new Color BackColor
        {
            get
            {
                return backColor;
            }
            set
            {
                backColor = value;
                lblText.BackColor = backColor;
                btnIcon.BackColor = backColor;
            }
        }
        [Category("RJ Code - Appearance")]
        public Color IconColor
        {
            get
            {
                return iconColor;
            }
            set
            {
                iconColor = value;
                btnIcon.Invalidate();
            }
        }
        [Category("RJ Code - Appearance")]
        public Color ListBackColor
        {
            get
            {
                return listBackColor;
            }
            set
            {
                listBackColor = value;
                cmbList.BackColor = listBackColor;
            }
        }
        [Category("RJ Code - Appearance")]
        public Color ListTextColor
        {
            get
            {
                return listTextColor;
            }
            set
            {
                listTextColor = value;
                cmbList.ForeColor = listTextColor;
            }
        }
        [Category("RJ Code - Appearance")]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                base.BackColor = borderColor;
            }
        }
        [Category("RJ Code - Appearance")]
        public int BorderSize
        {
            get
            {
                return borderSize;
            }
            set
            {
                borderSize = value;
                this.Padding = new Padding(borderSize);
                this.AdjustComboBoxDimensions();
            }
        }
        [Category("RJ Code - Appearance")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                lblText.ForeColor = value;
            }
        }
        [Category("RJ Code - Appearance")]
        private Font font = new Font("標楷體", 10F);
        public override Font Font
        {
            get
            {
                return this.font;
            }
            set
            {
                base.Font = value;
                this.font = value;
                lblText.Font = value;
                cmbList.Font = value;

                this.AdjustComboBoxDimensions();
            }
        }
        [Category("RJ Code - Appearance")]
        public string Texts
        {
            get
            {
                return lblText.Text;
            }
            set
            {
                lblText.Text = value;
            }
        }
        [Category("RJ Code - Appearance")]
        public ComboBoxStyle DropDownStyle
        {
            get
            {
                return cmbList.DropDownStyle;
            }
            set
            {
                if(cmbList.DropDownStyle != ComboBoxStyle.Simple)
                {
                    cmbList.DropDownStyle = value;
                }
            }
        }

        [Category("RJ Code - Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [MergableProperty(false)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public ComboBox.ObjectCollection Items
        {
            get
            {
                return cmbList.Items;
            }
        }
        [Category("RJ Code - Data")]
        [AttributeProvider(typeof(IListSource))]
        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public object DataSource
        {
            get
            {
                return cmbList.DataSource;
            }
            set
            {
                cmbList.DataSource = value;
            }
        }
        [Category("RJ Code - Data")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get
            {
                return cmbList.AutoCompleteCustomSource;
            }
            set
            {
                cmbList.AutoCompleteCustomSource = value;
            }
        }
        [Category("RJ Code - Data")]
        [Browsable(true)]
        [DefaultValue(AutoCompleteSource.None)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteSource AutoCompleteSource
        {
            get
            {
                return cmbList.AutoCompleteSource;
            }
            set
            {
                cmbList.AutoCompleteSource = value;
            }
        }
        [Category("RJ Code - Data")]
        [Browsable(true)]
        [DefaultValue(AutoCompleteMode.None)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteMode AutoCompleteMode
        {
            get
            {
                return cmbList.AutoCompleteMode;
            }
            set
            {
                cmbList.AutoCompleteMode = value;
            }
        }
        [Category("RJ Code - Data")]
        [Bindable(true)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem
        {
            get
            {
                object value = new object();
                this.Invoke(new Action(delegate
                {
                    value = cmbList.SelectedItem;
                }));

                return value;
            }
            set
            {
                cmbList.SelectedItem = value;
            }
        }
        [Category("RJ Code - Data")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get
            {
                int value = -1;
                this.Invoke(new Action(delegate
                {
                    value = cmbList.SelectedIndex;
                }));
                return value;
            }
            set
            {
                this.Invoke(new Action(delegate
                {
                    cmbList.SelectedIndex = value;
                }));
          
            }
        }

        public event EventHandler OnSelectedIndexChanged;
        public event EventHandler OnDropDown;
        public event EventHandler OnDropDownClosed;

        public RJ_ComboBox()
        {
            cmbList = new ComboBox();
            lblText = new Label();
            btnIcon = new Button();

            cmbList.BackColor = listBackColor;
            cmbList.Font = this.Font;
            cmbList.ForeColor = listTextColor;
            cmbList.SelectedIndexChanged += CmbList_SelectedIndexChanged;
            cmbList.DropDown += CmbList_DropDown;
            cmbList.DropDownClosed += CmbList_DropDownClosed;
            cmbList.TextChanged += CmbList_TextChanged;
            cmbList.MouseWheel += CmbList_MouseWheel;

            btnIcon.Dock = DockStyle.Right;
            btnIcon.FlatStyle = FlatStyle.Flat;
            btnIcon.FlatAppearance.BorderSize = 0;
            btnIcon.Size = new Size(30, 30);
            btnIcon.Cursor = Cursors.Hand;
            btnIcon.Click += BtnIcon_Click;
            btnIcon.Paint += BtnIcon_Paint;

            lblText.Dock = DockStyle.Fill;
            lblText.AutoSize = false;
            lblText.BackColor = backColor;
            lblText.TextAlign = ContentAlignment.MiddleLeft;
            lblText.Padding = new Padding(0, 0, 0, 0);
            lblText.Font = this.Font;
            lblText.Click += Surface_Click;
            lblText.MouseEnter += Surface_MouseEnter;
            lblText.MouseLeave += Surface_MouseLeave;
            lblText.MouseWheel += Surface_MouseWheel;
            this.Controls.Add(lblText);
            this.Controls.Add(btnIcon);
            this.Controls.Add(cmbList);
            this.MinimumSize = new Size(50, 30); 
            this.Size = new Size(50, 30);
            this.ForeColor = Color.DimGray;
            this.Padding = new Padding(borderSize);
            base.BackColor = borderColor;
            this.ResumeLayout();
            AdjustComboBoxDimensions();

        }

        public void SetDataSource(object value)
        {
            int index = this.SelectedIndex;
            this.DataSource = value;
            if (index == -1)
            {
                if (this.Items.Count > 0)
                {
                    this.SelectedIndex = 0;
                    return;
                }
            }
            if (index < this.Items.Count)
            {
                this.SelectedIndex = index;
                return;
            }
        }

        public void SetTextSize(int TextWidth , int iconWidth)
        {
            this.Size = new Size(TextWidth + iconWidth ,this.Size.Height);
            lblText.Size = new Size(TextWidth, this.Height);
            btnIcon.Size = new Size(iconWidth, this.Height);
 
        }
        private void CmbList_MouseWheel(object sender, MouseEventArgs e)
        {
          
        }
        private void Surface_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (cmbList.SelectedIndex == -1)
                {
                    cmbList.SelectedIndex = 0;
                }
                else if (cmbList.SelectedIndex < cmbList.Items.Count - 1)
                {
                    cmbList.SelectedIndex++;
                }
            }
            else
            {
                if (cmbList.SelectedIndex == -1)
                {
                    cmbList.SelectedIndex = 0;
                }
                else if (cmbList.SelectedIndex > 0)
                {
                    cmbList.SelectedIndex--;
                }
            }
        }
        private void Surface_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }
        private void Surface_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
        }
        private void Surface_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
            cmbList.Select();
            if(cmbList.DropDownStyle == ComboBoxStyle.DropDownList)
            {
                cmbList.DroppedDown = true;
            }
        }
        private void BtnIcon_Paint(object sender, PaintEventArgs e)
        {
            int iconWidth = 14;
            int iconHeight = 6;
            var rectIcon = new Rectangle((btnIcon.Width - iconWidth) / 2, (btnIcon.Height - iconHeight) / 2, iconWidth, iconHeight);
            Graphics graphics = e.Graphics;

            using (GraphicsPath path = new GraphicsPath())
            using (Pen pen = new Pen(iconColor, 2))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                path.AddLine(rectIcon.X, rectIcon.Y, rectIcon.X + (iconWidth / 2), rectIcon.Bottom);
                path.AddLine(rectIcon.X + (iconWidth / 2), rectIcon.Bottom, rectIcon.Right, rectIcon.Y);
                graphics.DrawPath(pen, path);
            }
            this.AdjustComboBoxDimensions();
        }
        private void BtnIcon_Click(object sender, EventArgs e)
        {
            cmbList.Select();
            cmbList.DroppedDown = true;
        }
        private void CmbList_TextChanged(object sender, EventArgs e)
        {
            lblText.Text = cmbList.Text;
        }
        private void CmbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.OnSelectedIndexChanged != null)
            {
                this.OnSelectedIndexChanged.Invoke(sender, e);
            }
            lblText.Text = cmbList.Text;
        }
        private void CmbList_DropDownClosed(object sender, EventArgs e)
        {
            if (this.OnDropDownClosed != null)
            {
                this.OnDropDownClosed.Invoke(sender, e);
            }
        }
        private void CmbList_DropDown(object sender, EventArgs e)
        {
            if (this.OnDropDown != null)
            {
                this.OnDropDown.Invoke(sender, e);
            }
        }

        private void AdjustComboBoxDimensions()
        {
            cmbList.Width = this.Width - 12;
           
            cmbList.Location = new Point()
            {
                //X = this.Width - this.Padding.Right - cmbList.Width,
                X = 10,
                Y = lblText.Bottom - cmbList.Height
            };
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.AdjustComboBoxDimensions();
        }
    }
}
