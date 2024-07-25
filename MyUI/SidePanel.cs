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
    public enum ExpandDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    [Designer(typeof(MyUserControlDesigner))]
    public partial class SidePanel : UserControl
    {
        private Timer animationTimer;
        private bool isPanelExpanded = true;
        private int panelSize;
        private Button toggleButton;
        private Form parentForm;

        [Category("自訂義"), Description("The size of the panel when expanded.")]
        public int ExpandedSize { get; set; } = 200;

        private int collapsedSize = 30;
        [Category("自訂義"), Description("The size of the panel when collapsed.")]
        public int CollapsedSize { get => collapsedSize; set => collapsedSize = value; }

        [Category("自訂義"), Description("The step size for the animation.")]
        public int AnimationStep { get; set; } = 30;

   
        private ExpandDirection expandDirection = ExpandDirection.Left;
        [Category("自訂義"), Description("The direction in which the panel expands and collapses.")]
        public ExpandDirection ExpandDirection 
        { 
            get => expandDirection; 
            set
            {
                expandDirection = value;
                SetToggleButtonPosition();
                this.Invalidate();
            }
        }



        public SidePanel()
        {
            InitializeComponent();
            this.Load += SidePanel_Load;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void InitializeComponent()
        {


            this.components = new System.ComponentModel.Container();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.toggleButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // animationTimer
            // 
            this.animationTimer.Interval = 20;
            this.animationTimer.Tick += new System.EventHandler(this.AnimatePanel);

            // toggleButton
            this.toggleButton.Text = "<";
            this.toggleButton.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toggleButton.Size = new System.Drawing.Size(30, 30);
            this.toggleButton.Click += new EventHandler(this.TogglePanel);
            this.toggleButton.BackColor = Color.White;
            this.toggleButton.FlatStyle = FlatStyle.Flat;
            this.toggleButton.FlatAppearance.BorderSize = 0;

            // 
            // SidePanel
            // 
            this.rJ_Pannel = new RJ_Pannel();
            this.rJ_Pannel.BackColor = System.Drawing.Color.White;
            this.rJ_Pannel.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_Pannel.BorderRadius = 5;
            this.rJ_Pannel.BorderSize = 0;
            this.rJ_Pannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_Pannel.ForeColor = System.Drawing.Color.White;
            this.rJ_Pannel.Location = new System.Drawing.Point(0, 0);
            this.rJ_Pannel.Name = "rJ_Pannel";
            this.rJ_Pannel.Size = new System.Drawing.Size(521, 113);
            this.rJ_Pannel.TabIndex = 2;
            this.Controls.Add(this.rJ_Pannel);
            this.Controls.Add(this.toggleButton);
            this.Name = "SidePanel";
            this.Size = new System.Drawing.Size(200, 595);
            this.ResumeLayout(false);
        }

        private void SidePanel_Load(object sender, EventArgs e)
        {
            this.parentForm = this.FindForm();
            SetToggleButtonPosition();
        }

        private void SetToggleButtonPosition()
        {
            switch (ExpandDirection)
            {
                case ExpandDirection.Left:
                    this.toggleButton.Dock = DockStyle.Right;
                    this.toggleButton.Text = isPanelExpanded ? "<" : ">";
                    break;
                case ExpandDirection.Right:
                    this.toggleButton.Dock = DockStyle.Left;
                    this.toggleButton.Text = isPanelExpanded ? ">" : "<";
                    break;
                case ExpandDirection.Up:
                    this.toggleButton.Dock = DockStyle.Bottom;
                    this.toggleButton.Text = isPanelExpanded ? "∧" : "∨";
                    break;
                case ExpandDirection.Down:
                    this.toggleButton.Dock = DockStyle.Top;
                    this.toggleButton.Text = isPanelExpanded ? "∨" : "∧";
                    break;
            }
        }

        private void TogglePanel(object sender, EventArgs e)
        {
            this.isPanelExpanded = !this.isPanelExpanded;
            StartAnimation();
        }

        public void Expand()
        {
            if (!this.isPanelExpanded)
            {
                this.isPanelExpanded = true;
                StartAnimation();
            }
        }

        public void Collapse()
        {
            if (this.isPanelExpanded)
            {
                this.isPanelExpanded = false;
                StartAnimation();
            }
        }

        private void StartAnimation()
        {
            this.panelSize = ExpandDirection == ExpandDirection.Left || ExpandDirection == ExpandDirection.Right ? this.Width : this.Height;

            // 暫停母表單的繪製
            if (parentForm != null)
            {
                parentForm.SuspendLayout();
            }

            this.Invoke(new Action(delegate
            {
                SetToggleButtonPosition();
                this.animationTimer.Start();
            }));
        }

        private void AnimatePanel(object sender, EventArgs e)
        {
            if (this.isPanelExpanded)
            {
                if (ExpandDirection == ExpandDirection.Left || ExpandDirection == ExpandDirection.Right)
                {
                    if (this.Width <= this.ExpandedSize)
                    {
                        this.Width += this.AnimationStep;
                        if (this.Width >= this.ExpandedSize)
                        {
                            this.Width = this.ExpandedSize;
                            this.animationTimer.Stop();
                            ResumeParentFormLayout();
                        }
                    }
                }
                else
                {
                    if (this.Height <= this.ExpandedSize)
                    {
                        this.Height += this.AnimationStep;
                        if (this.Height >= this.ExpandedSize)
                        {
                            this.Height = this.ExpandedSize;
                            this.animationTimer.Stop();
                            ResumeParentFormLayout();
                        }
                    }
                }
            }
            else
            {
                if (ExpandDirection == ExpandDirection.Left || ExpandDirection == ExpandDirection.Right)
                {
                    if (this.Width >= this.CollapsedSize)
                    {
                        this.Width -= this.AnimationStep;
                        if (this.Width <= this.CollapsedSize)
                        {
                            this.Width = this.CollapsedSize;
                            this.animationTimer.Stop();
                            ResumeParentFormLayout();
                        }
                    }
                }
                else
                {
                    if (this.Height >= this.CollapsedSize)
                    {
                        this.Height -= this.AnimationStep;
                        if (this.Height <= this.CollapsedSize)
                        {
                            this.Height = this.CollapsedSize;
                            this.animationTimer.Stop();
                            ResumeParentFormLayout();
                        }
                    }
                }
            }
        }

        private void ResumeParentFormLayout()
        {
            if (parentForm != null)
            {
                parentForm.ResumeLayout();
                parentForm.Refresh();
            }
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
                var contentsPanel = ((SidePanel)this.Control).ContentsPanel;
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
