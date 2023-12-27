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
    [Designer(typeof(MyUserControlDesigner))]
    public class RJ_GroupBox :UserControl
    {
        private string gUID = "";
        public string GUID { get => gUID; set => gUID = value; }
        private RJ_Lable rJ_Lable_Title;
        private RJ_Pannel rJ_Pannel;

        [Category("RJ Code Advance")]
        public int TitleBorderSize
        {
            get
            {
                return rJ_Lable_Title.BorderSize;
            }
            set
            {
                rJ_Lable_Title.BorderSize = value;
            }
        }
        [Category("RJ Code Advance")]
        public int TitleBorderRadius
        {
            get
            {
                return rJ_Lable_Title.BorderRadius;
            }
            set
            {
                rJ_Lable_Title.BorderRadius = value;
            }
        }
        [Category("RJ Code Advance")]
        public int TitleHeight
        {
            get
            {
                return this.rJ_Lable_Title.Height;
            }
            set
            {
                this.rJ_Lable_Title.Height = value;
            }
        }

        [Category("RJ Code Advance")]
        public Color TitleBorderColor
        {
            get
            {
                return rJ_Lable_Title.BorderColor;
            }
            set
            {
                rJ_Lable_Title.BorderColor = value;
            }
        }
        [Category("RJ Code Advance")]
        public Color TitleForeColor
        {
            get
            {
                return rJ_Lable_Title.TextColor;
            }
            set
            {
                rJ_Lable_Title.TextColor = value;
            }
        }
        [Category("RJ Code Advance")]
        public Color TitleBackColor
        {
            get
            {
                return rJ_Lable_Title.BackgroundColor;
            }
            set
            {
                rJ_Lable_Title.BackColor = value;
                rJ_Lable_Title.BackgroundColor = value;
            }
        }
        [Category("RJ Code Advance")]
        public string TitleTexts
        {
            get
            {
                return rJ_Lable_Title.Text;
            }
            set
            {
                rJ_Lable_Title.Text = value;
            }
        }
        [Category("RJ Code Advance")]
        public Font TitleFont
        {
            get
            {
                return rJ_Lable_Title.Font;
            }
            set
            {
                rJ_Lable_Title.Font = value;
            }
        }
        [Category("RJ Code Advance")]
        public ContentAlignment TitleTextAlign
        {
            get
            {
                return rJ_Lable_Title.TextAlign;
            }
            set
            {
                rJ_Lable_Title.TextAlign = value;
            }
        }
        [Category("RJ Code Advance")]
        public int PannelBorderSize
        {
            get
            {
                return rJ_Pannel.BorderSize;
            }
            set
            {
                rJ_Pannel.BorderSize = value;
            }
        }
        [Category("RJ Code Advance")]
        public int PannelBorderRadius
        {
            get
            {
                return rJ_Pannel.BorderRadius;
            }
            set
            {
                rJ_Pannel.BorderRadius = value;
            }
        }
        [Category("RJ Code Advance")]
        public Color PannelBorderColor
        {
            get
            {
                return rJ_Pannel.BorderColor;
            }
            set
            {
                rJ_Pannel.BorderColor = value;
            }
        }
        [Category("RJ Code Advance")]
        public Color PannelBackColor
        {
            get
            {
                return rJ_Pannel.BackColor;
            }
            set
            {
                rJ_Pannel.BackColor = value;
            }
        }


        private void InitializeComponent()
        {
            this.rJ_Lable_Title = new MyUI.RJ_Lable();
            this.rJ_Pannel = new MyUI.RJ_Pannel();
            this.SuspendLayout();
            // 
            // rJ_Lable_Title
            // 
            this.rJ_Lable_Title.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.rJ_Lable_Title.BackgroundColor = System.Drawing.Color.DeepSkyBlue;
            this.rJ_Lable_Title.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_Title.BorderRadius = 5;
            this.rJ_Lable_Title.BorderSize = 0;
            this.rJ_Lable_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable_Title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_Title.Font = new System.Drawing.Font("新細明體", 12F);
            this.rJ_Lable_Title.ForeColor = System.Drawing.Color.White;
            this.rJ_Lable_Title.Location = new System.Drawing.Point(0, 0);
            this.rJ_Lable_Title.Name = "rJ_Lable_Title";
            this.rJ_Lable_Title.Size = new System.Drawing.Size(521, 37);
            this.rJ_Lable_Title.TabIndex = 0;
            this.rJ_Lable_Title.Text = "Title";
            this.rJ_Lable_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable_Title.TextColor = System.Drawing.Color.White;
            // 
            // rJ_Pannel
            // 
            this.rJ_Pannel.BackColor = System.Drawing.Color.White;
            this.rJ_Pannel.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_Pannel.BorderRadius = 5;
            this.rJ_Pannel.BorderSize = 0;
            this.rJ_Pannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_Pannel.ForeColor = System.Drawing.Color.White;
            this.rJ_Pannel.Location = new System.Drawing.Point(0, 37);
            this.rJ_Pannel.Name = "rJ_Pannel";
            this.rJ_Pannel.Size = new System.Drawing.Size(521, 113);
            this.rJ_Pannel.TabIndex = 2;
            // 
            // RJ_GroupBox
            // 
            this.Controls.Add(this.rJ_Pannel);
            this.Controls.Add(this.rJ_Lable_Title);
            this.Name = "RJ_GroupBox";
            this.Size = new System.Drawing.Size(521, 150);
            this.ResumeLayout(false);

        }
        public RJ_GroupBox()
        {
            InitializeComponent();
        }


        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
        }

        public class MyPanelDesigner : ParentControlDesigner
        {
            public override SelectionRules SelectionRules
            {
                get
                {
                    SelectionRules selectionRules = base.SelectionRules;
                    selectionRules &= ~SelectionRules.AllSizeable;
                    return selectionRules;
                }
            }
            protected override void PostFilterAttributes(IDictionary attributes)
            {
                base.PostFilterAttributes(attributes);
                attributes[typeof(DockingAttribute)] = new DockingAttribute(DockingBehavior.Never);
            }
            protected override void PostFilterProperties(IDictionary properties)
            {
                base.PostFilterProperties(properties);
                var propertiesToRemove = new string[] {
                "Dock", "Anchor",
                "Size", "Location", "Width", "Height",
                "MinimumSize", "MaximumSize",
                "AutoSize", "AutoSizeMode",
                "Visible", "Enabled",
            };
                foreach (var item in propertiesToRemove)
                {
                    if (properties.Contains(item))
                        properties[item] = TypeDescriptor.CreateProperty(this.Component.GetType(),
                            (PropertyDescriptor)properties[item],
                            new BrowsableAttribute(false));
                }
            }
        }

        public class MyUserControlDesigner : ParentControlDesigner
        {
            public override void Initialize(IComponent component)
            {
                base.Initialize(component);
                var contentsPanel = ((RJ_GroupBox)this.Control).ContentsPanel;
                this.EnableDesignMode(contentsPanel, "ContentsPanel");
            }
            public override bool CanParent(Control control)
            {
                return false;
            }
            protected override void OnDragOver(DragEventArgs de)
            {
                de.Effect = DragDropEffects.None;
            }
            protected override IComponent[] CreateToolCore(ToolboxItem tool, int x, int y, int width, int height, bool hasLocation, bool hasSize)
            {
                return null;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RJ_Pannel ContentsPanel
        {
            get { return rJ_Pannel; }
        }
    }
}
