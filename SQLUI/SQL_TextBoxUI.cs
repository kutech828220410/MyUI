using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;
using Basic;
namespace SQLUI
{
    public partial class SQL_TextBoxUI : UserControl
    {
        [ReadOnly(false), Browsable(false), Category(""), Description(""), DefaultValue("")]
        public string Value
        {
            get
            {
                if(this.rJ_CheckBox.Checked || !this.rJ_CheckBox.Visible)
                {
                    return TextBoxTexts;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                TextBoxTexts = value;
            }
        }

        private int titleWidth = 100;
        [Category("Title label")]
        public int TitleWidth
        {
            get
            {
                return titleWidth;
            }
            set
            {
                titleWidth = value;
                rJ_Lable_Title.Width = value;
                this.Invalidate();
            }
        }
        private int titleBorderSize = 0;
        [Category("Title label")]
        public int TitleBorderSize
        {
            get
            {
                return titleBorderSize;
            }
            set
            {
                titleBorderSize = value;
                rJ_Lable_Title.BorderSize = value;
                this.Invalidate();
            }
        }
        private int titleBorderRadius = 5;
        [Category("Title label")]
        public int TitleBorderRadius
        {
            get
            {
                return titleBorderRadius;
            }
            set
            {
                titleBorderRadius = value;
                rJ_Lable_Title.BorderRadius = value;
                this.Invalidate();
            }
        }
        private string titleText = "Title";
        [Category("Title label")]
        public string TitleText
        {
            get
            {
                return titleText;
            }
            set
            {
                titleText = value;
                if (titleTextAlign == ContentAlignment.MiddleLeft) rJ_Lable_Title.Text = "  " + titleText;
                else rJ_Lable_Title.Text = titleText;           
                this.Invalidate();
            }
        }      
        private Font titleFont = new Font("微軟正黑體", 12);
        [Category("Title label")]
        public Font TitleFont
        {
            get
            {
                return titleFont;
            }
            set
            {
                titleFont = value;
                rJ_Lable_Title.Font = value;
                this.Invalidate();
            }
        }
        private Color titleForeColor = Color.Black;
        [Category("Title label")]
        public Color TitleForeColor
        {
            get
            {
                return titleForeColor;
            }
            set
            {
                titleForeColor = value;
                rJ_Lable_Title.ForeColor = value;
                this.Invalidate();
            }
        }
        private Color titleBorderColor = Color.DeepSkyBlue;
        [Category("Title label")]
        public Color TitleBorderColor
        {
            get
            {
                return titleBorderColor;
            }
            set
            {
                titleBorderColor = value;
                rJ_Lable_Title.BorderColor = value;
                this.Invalidate();
            }
        }
        private Color titleBackColor = Color.DeepSkyBlue;
        [Category("Title label")]
        public Color TitleBackColor
        {
            get
            {
                return titleBackColor;
            }
            set
            {
                titleBackColor = value;
                rJ_Lable_Title.BackColor = value;
                this.Invalidate();
            }
        }
        private ContentAlignment titleTextAlign = ContentAlignment.MiddleCenter;
        [Category("Title label")]
        public ContentAlignment TitleTextAlign
        {
            get
            {
                return titleTextAlign;
            }
            set
            {
                titleTextAlign = value;
                rJ_Lable_Title.TextAlign = value;
                if (titleTextAlign == ContentAlignment.MiddleLeft) rJ_Lable_Title.Text = "  " + titleText;
                this.Invalidate();
            }
        }


        private string textBoxTexts = "";
        [Category("TextBox")]
        public string TextBoxTexts
        {
            get
            {
                return textBoxTexts;
            }
            set
            {
                textBoxTexts = value;
                rJ_Lable_Title.Text = value;
                this.Invalidate();
            }
        }     
        private string textBoxPlaceholderText = "";
        [Category("TextBox")]
        public string TextBoxPlaceholderText
        {
            get
            {
                return textBoxPlaceholderText;
            }
            set
            {
                textBoxPlaceholderText = value;
                rJ_Lable_Title.Text = value;
                this.Invalidate();
            }
        }
        private int txtBorderSize = 2;
        [Category("TextBox")]
        public int TextBorderSize
        {
            get
            {
                return txtBorderSize;
            }
            set
            {
                txtBorderSize = value;
                rJ_TextBox.BorderSize = value;
                this.Invalidate();
            }
        }     
        private int txtBorderRadius = 3;
        [Category("TextBox")]
        public int TextBorderRadius
        {
            get
            {
                return txtBorderRadius;
            }
            set
            {
                txtBorderRadius = value;
                rJ_TextBox.BorderRadius = value;
                this.Invalidate();
            }
        } 
        private Color textBorderColor = System.Drawing.Color.DeepSkyBlue;
        [Category("TextBox")]
        public Color TextBorderColor
        {
            get
            {
                return textBorderColor;
            }
            set
            {
                textBorderColor = value;
                rJ_TextBox.BorderColor = value;
                this.Invalidate();
            }
        }
        private Color textBackColor = System.Drawing.SystemColors.Window;
        [Category("TextBox")]
        public Color TextBackColor
        {
            get
            {
                return textBackColor;
            }
            set
            {
                textBackColor = value;
                rJ_TextBox.BackColor = value;
                this.Invalidate();
            }
        }      
        private Color textBorderFocusColor = System.Drawing.Color.HotPink;
        [Category("TextBox")]
        public Color TextBorderFocusColor
        {
            get
            {
                return textBorderFocusColor;
            }
            set
            {
                textBorderFocusColor = value;
                rJ_TextBox.BorderFocusColor = value;
                this.Invalidate();
            }
        }      
        private Color textForeColor = System.Drawing.Color.DimGray;
        [Category("TextBox")]
        public Color TextForeColor
        {
            get
            {
                return textForeColor;
            }
            set
            {
                textForeColor = value;
                rJ_TextBox.ForeColor = value;
                this.Invalidate();
            }
        }    
        private Color textPlaceholderColor = System.Drawing.Color.DarkGray;
        [Category("TextBox")]
        public Color TextPlaceholderColor
        {
            get
            {
                return textPlaceholderColor;
            }
            set
            {
                textPlaceholderColor = value;
                rJ_TextBox.PlaceholderColor = value;
                this.Invalidate();
            }
        } 
        private System.Windows.Forms.HorizontalAlignment textBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Left;
        [Category("TextBox")]
        public System.Windows.Forms.HorizontalAlignment TextBoxTextAlign
        {
            get
            {
                return textBoxTextAlign;
            }
            set
            {
                textBoxTextAlign = value;
                rJ_TextBox.TextAlgin = value;
                this.Invalidate();
            }
        } 
        private Font textFont = new System.Drawing.Font("新細明體", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
        [Category("TextBox")]
        public Font TextFont
        {
            get
            {
                return textFont;
            }
            set
            {
                textFont = value;
                rJ_TextBox.Font = value;
                this.Invalidate();
            }
        }

        private int checkboxWidth = 40;
        [Category("Checkbox")]
        public int CheckboxWidth
        {
            get
            {
                return checkboxWidth;
            }
            set
            {
                checkboxWidth = value;
                rJ_CheckBox.Width = value;
                this.Invalidate();
            }
        }
        private Color checkboxOffBackColor = System.Drawing.Color.Gray;
        [Category("Checkbox")]
        public Color CheckboxOffBackColor
        {
            get
            {
                return checkboxOffBackColor;
            }
            set
            {
                checkboxOffBackColor = value;
                rJ_CheckBox.OffBackColor = value;
                this.Invalidate();
            }
        }
        private Color checkboxOnBackColor = System.Drawing.Color.MediumSlateBlue;
        [Category("Checkbox")]
        public Color CheckboxOnBackColor
        {
            get
            {
                return checkboxOnBackColor;
            }
            set
            {
                checkboxOnBackColor = value;
                rJ_CheckBox.OnBackColor = value;
                this.Invalidate();
            }
        }
        private Color checkboxOffToggleColor = System.Drawing.Color.Gainsboro;
        [Category("Checkbox")]
        public Color CheckboxOffToggleColor
        {
            get
            {
                return checkboxOffToggleColor;
            }
            set
            {
                checkboxOffToggleColor = value;
                rJ_CheckBox.OffToggleColor = value;
                this.Invalidate();
            }
        }
        private Color checkboxOnToggleColor = System.Drawing.Color.WhiteSmoke;
        [Category("Checkbox")]
        public Color CheckboxOnToggleColor
        {
            get
            {
                return checkboxOnToggleColor;
            }
            set
            {
                checkboxOnToggleColor = value;
                rJ_CheckBox.OnToggleColor = value;
                this.Invalidate();
            }
        }
        private bool checkboxSolidStyle = true;
        [Category("Checkbox")]
        public bool CheckboxSolidStyle
        {
            get
            {
                return checkboxSolidStyle;
            }
            set
            {
                checkboxSolidStyle = value;
                rJ_CheckBox.SolidStyle = value;
                this.Invalidate();
            }
        }
        private bool checkboxVisible = true;
        [Category("Checkbox")]
        public bool CheckboxVisible
        {
            get
            {
                return checkboxVisible;
            }
            set
            {
                checkboxVisible = value;
                rJ_CheckBox.Visible = value;
                this.Invalidate();
            }
        }








        private RJ_Lable rJ_Lable_Title = new RJ_Lable();
        private RJ_TextBox rJ_TextBox = new RJ_TextBox();
        private RJ_CheckBox rJ_CheckBox = new RJ_CheckBox();
        private Panel panel = new Panel();
        private Panel panel_space_Title = new Panel();
        private Panel panel_space_CheckBox = new Panel();
        private List<Control> list_Controls = new List<Control>();
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            panel.Dock = DockStyle.Fill;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.Size = new System.Drawing.Size(250, 30);
            this.Padding = new Padding(0);
            this.Margin = new Padding(0);
            panel.Padding = new Padding(0);
            panel.Margin = new Padding(0);
            rJ_Lable_Title.AutoSize = false;
            rJ_Lable_Title.Width = titleWidth;
            rJ_Lable_Title.Dock = DockStyle.Left;
            rJ_Lable_Title.TextAlign = titleTextAlign;
            rJ_Lable_Title.Font = titleFont;
            rJ_Lable_Title.ForeColor = titleForeColor;
            rJ_Lable_Title.BackColor = titleBackColor;
            if (titleTextAlign == ContentAlignment.MiddleLeft) rJ_Lable_Title.Text = "  " + titleText;
            else rJ_Lable_Title.Text = titleText;
            rJ_Lable_Title.Padding = new Padding(0);
            rJ_Lable_Title.Margin = new Padding(0);
            rJ_Lable_Title.BorderRadius = titleBorderRadius;
            rJ_Lable_Title.BorderSize = titleBorderSize;
            rJ_Lable_Title.BorderColor = titleBorderColor;

            rJ_Lable_Title.TabIndex = list_Controls.Count;
            list_Controls.Add(rJ_Lable_Title);

            panel_space_Title.Dock = DockStyle.Left;
            panel_space_Title.Width = 10;
            panel_space_Title.TabIndex = list_Controls.Count;
            list_Controls.Add(panel_space_Title);

            rJ_CheckBox.AutoSize = false;
            rJ_CheckBox.MinimumSize = new System.Drawing.Size(10, 10);
            rJ_CheckBox.OffBackColor = checkboxOffBackColor;
            rJ_CheckBox.OffToggleColor = checkboxOffToggleColor;
            rJ_CheckBox.OnBackColor = checkboxOnBackColor;
            rJ_CheckBox.OnToggleColor = checkboxOnToggleColor;
            rJ_CheckBox.Dock = DockStyle.Right;
            rJ_CheckBox.Size = new System.Drawing.Size(checkboxWidth, 24);
            rJ_CheckBox.SolidStyle = true;
            rJ_CheckBox.Padding = new Padding(10);
            rJ_CheckBox.Margin = new Padding(10);
            rJ_CheckBox.UseVisualStyleBackColor = checkboxSolidStyle;
            rJ_CheckBox.TabIndex = list_Controls.Count;
            list_Controls.Add(rJ_CheckBox);

            panel_space_CheckBox.Dock = DockStyle.Right;
            panel_space_CheckBox.Width = 10;
            panel_space_CheckBox.TabIndex = list_Controls.Count;
            list_Controls.Add(panel_space_CheckBox);

            rJ_TextBox.AutoSize = false;
            rJ_TextBox.Dock = DockStyle.Fill;
            rJ_TextBox.BackColor = textBackColor;
            rJ_TextBox.BorderColor = textBorderColor;
            rJ_TextBox.BorderFocusColor = textBorderFocusColor;
            rJ_TextBox.BorderRadius = txtBorderRadius;
            rJ_TextBox.BorderSize = txtBorderSize;
            rJ_TextBox.Font = textFont;
            rJ_TextBox.ForeColor = textForeColor;
            rJ_TextBox.Multiline = false;
            rJ_TextBox.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            rJ_TextBox.PlaceholderColor = textPlaceholderColor;
            rJ_TextBox.PlaceholderText = textBoxPlaceholderText;
            rJ_TextBox.ShowTouchPannel = false;
            rJ_TextBox.TabIndex = list_Controls.Count;
            rJ_TextBox.TextAlgin = textBoxTextAlign;
            rJ_TextBox.Texts = textBoxTexts;
            rJ_TextBox.UnderlineStyle = false;

            list_Controls.Add(rJ_TextBox);
            for (int i = list_Controls.Count - 1; i >= 0; i--)
            {
                panel.Controls.Add(list_Controls[i]);
            }
            this.Controls.Add(this.panel);
            panel.ResumeLayout(false);
            this.ResumeLayout(false);       
        }
        public SQL_TextBoxUI()
        {
            InitializeComponent();
       
        }
    }
}
