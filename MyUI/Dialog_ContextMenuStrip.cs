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
namespace MyUI
{
    public class Dialog_ContextMenuStrip : Form
    {
        private string value = "None";
        public string Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }
        private string titleText = "";
        public string TitleText
        {
            get
            {
                return titleText;
            }
            set
            {
                label_Title.Visible = (value != "");
                titleText = value;
                label_Title.Text = "  " + titleText;
            }
        }
        public Font TitleFont
        {
            get
            {
                return label_Title.Font;
            }
            set
            {
                label_Title.Font = value;
            }
        }
        private Color titleForeColor = Color.Black;
        public Color TitleForeColor
        {
            get
            {
                return titleForeColor;
            }
            set
            {
                titleForeColor = value;
            }
        }
        public Color TitleBackColor
        {
            get
            {
                return label_Title.BackColor;
            }
            set
            {
                label_Title.BackColor = value;
            }
        }
        private int controlsRadius = 5;
        public int ControlsRadius
        {
            get
            {
                return controlsRadius;
            }
            set
            {
                controlsRadius = value;
            }
        }
        private Font controlsFont = new Font("微軟正黑體", 14);
        public Font ControlsFont
        {
            get
            {
                return controlsFont;
            }
            set
            {
                controlsFont = value;
            }
        }
        private Color controlsForeColor = Color.White;
        public Color ControlsForeColor
        {
            get
            {
                return controlsForeColor;
            }
            set
            {
                controlsForeColor = value;
            }
        }
        private Color controlsBackColor = Color.RoyalBlue;
        public Color ControlsBackColor
        {
            get
            {
                return controlsBackColor;
            }
            set
            {
                controlsBackColor = value;
            }
        }
        public int ControlsHeight = 50;
        private ContentAlignment controlsTextAlign = ContentAlignment.MiddleCenter;
        public ContentAlignment ControlsTextAlign
        {
            get
            {
                return controlsTextAlign;
            }
            set
            {
                controlsTextAlign = value;
            }
        }

        private Label label_Title;
        private RJ_Button rJ_Button_Calcel;
        private List<Control> list_Controls = new List<Control>();
        private List<RJ_Button> rJ_Buttons = new List<RJ_Button>();
        private List<PLC_Device> pLC_Devices = new List<PLC_Device>();
        private Panel panel;
        private string[] texts;
        private int label_height
        {
            get
            {
                if (!label_Title.Visible) return 0;
                return label_Title.Height;
            }
        }
        private int rjbutton_height
        {
            get
            {
                int height = 0;
                for(int i = 0; i < rJ_Buttons.Count; i++)
                {
                    height += rJ_Buttons[i].Height;
                }
                return height;
            }
        }
        private int rjbutton_cancel_height
        {
            get
            {
                return rJ_Button_Calcel.Height;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
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
            this.StartPosition = FormStartPosition.Manual;
            this.ControlBox = false;
            this.BackColor = Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResizeRedraw = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.Controls.Clear();
            panel = new System.Windows.Forms.Panel();
            panel.Dock = DockStyle.Fill;
            panel.Padding = new Padding(2, 2, 2, 2);
            label_Title = new Label();

            label_Title.AutoSize = false;
            label_Title.Dock = DockStyle.Top;
            label_Title.TextAlign = ContentAlignment.MiddleLeft;
            label_Title.ForeColor = titleForeColor;   
            label_Title.Text = "  " + titleText;
            label_Title.Visible = (titleText != "");
            list_Controls.Add(label_Title);
            for (int i = 0; i < texts.Length; i++)
            {
                RJ_Button rJ_Button = new RJ_Button();
                rJ_Button.Click += RJ_Button_Click;
                list_Controls.Add(rJ_Button);           
                rJ_Buttons.Add(rJ_Button);
            }
            this.RJ_Button_Init();
            rJ_Button_Calcel = new RJ_Button();
            list_Controls.Add(rJ_Button_Calcel);
            rJ_Button_Calcel.Size = new Size(100, this.ControlsHeight);
            rJ_Button_Calcel.Dock = DockStyle.Top;
            rJ_Button_Calcel.Text = "取消";
            rJ_Button_Calcel.Font = controlsFont;
            rJ_Button_Calcel.BorderRadius = controlsRadius;
            rJ_Button_Calcel.BackColor = controlsBackColor;
            rJ_Button_Calcel.ForeColor = controlsForeColor;
            rJ_Button_Calcel.Click += RJ_Button_Calcel_Click;
            rJ_Button_Calcel.BackColor = Color.DarkGray;
            rJ_Button_Calcel.Margin = new Padding(2, 2, 2, 2);
            for (int i = list_Controls.Count - 1; i >= 0; i--)
            {
                panel.Controls.Add(list_Controls[i]);
            }
            this.Controls.Add(this.panel);
            this.Resize();
            panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        public Dialog_ContextMenuStrip(object Enum)
        {
            texts = Enum.GetEnumNames();
            string[] DescriptionTexts = Enum.GetEnumDescriptions();
            for (int i = 0; i < texts.Length; i++)
            {
                if(!DescriptionTexts[i].StringIsEmpty())
                {
                    pLC_Devices.Add(new PLC_Device(DescriptionTexts[i]));
                }
                else
                {
                    pLC_Devices.Add(null);
                }
            }
            InitializeComponent();
            this.Load += Dialog_ContextMenuStrip_Load;
        }
        public Dialog_ContextMenuStrip(params string[] values)
        {
            texts = values;
            for (int i = 0; i < texts.Length; i++)
            {
                pLC_Devices.Add(null);
            }
            InitializeComponent();
            this.Load += Dialog_ContextMenuStrip_Load;
        }

        private void Dialog_ContextMenuStrip_Load(object sender, EventArgs e)
        {
            RJ_Button_Init();
            Resize();
        }
        private void Resize()
        {
            this.Height = label_height + rjbutton_height + rjbutton_cancel_height;
            int ScreenWidth = this.FindForm().Width;
            int ScreenHeight = this.FindForm().Height;
            this.Location = new Point((ScreenWidth - this.Width) / 2 + this.FindForm().Location.X, (ScreenHeight - this.Height) / 2 + this.FindForm().Location.Y);
        }
        private void RJ_Button_Init()
        {
            for (int i = 0; i < rJ_Buttons.Count; i++)
            {
                rJ_Buttons[i].Size = new Size(100, this.ControlsHeight);
                rJ_Buttons[i].Dock = DockStyle.Top;
                if(i < texts.Length) rJ_Buttons[i].Text = texts[i];
                rJ_Buttons[i].Font = controlsFont;
                rJ_Buttons[i].BorderRadius = controlsRadius;
                rJ_Buttons[i].BackColor = controlsBackColor;
                rJ_Buttons[i].ForeColor = controlsForeColor;
                rJ_Buttons[i].TextAlign = controlsTextAlign;
                if (pLC_Devices[i] != null)
                {
                    if (!pLC_Devices[i].Bool)
                    {
                        rJ_Buttons[i].ForeColor = Color.White;
                    }
                    rJ_Buttons[i].Enabled = pLC_Devices[i].Bool;              
                }
                rJ_Buttons[i].Invalidate();
            }     
        }
        private void RJ_Button_Calcel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
        private void RJ_Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.value = ((Control)sender).Text;
            this.Close();
        }
    }
}
