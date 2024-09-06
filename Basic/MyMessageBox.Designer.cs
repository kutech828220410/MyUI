namespace Basic
{
    partial class MyMessageBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.plC_Button_Cancel = new MyUI.PLC_Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.plC_Button_Confirm = new MyUI.PLC_Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_TopBox = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label_Content = new MyUI.RJ_Lable();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel_ImageBox = new System.Windows.Forms.Panel();
            this.panel_Image = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel_TopBox.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel_ImageBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(4, 201);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(716, 85);
            this.panel1.TabIndex = 37;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.plC_Button_Cancel);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.plC_Button_Confirm);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(525, 0);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3);
            this.panel5.Size = new System.Drawing.Size(191, 85);
            this.panel5.TabIndex = 40;
            // 
            // plC_Button_Cancel
            // 
            this.plC_Button_Cancel.Bool = false;
            this.plC_Button_Cancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plC_Button_Cancel.but_press = false;
            this.plC_Button_Cancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.plC_Button_Cancel.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_Cancel.Location = new System.Drawing.Point(16, 3);
            this.plC_Button_Cancel.Name = "plC_Button_Cancel";
            this.plC_Button_Cancel.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_Cancel.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_Cancel.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_Cancel.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_Cancel.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_Cancel.ON_背景顏色 = System.Drawing.Color.Yellow;
            this.plC_Button_Cancel.Size = new System.Drawing.Size(80, 79);
            this.plC_Button_Cancel.Style = MyUI.PLC_Button.StyleEnum.自定義;
            this.plC_Button_Cancel.TabIndex = 35;
            this.plC_Button_Cancel.TabStop = false;
            this.plC_Button_Cancel.事件驅動 = false;
            this.plC_Button_Cancel.字型鎖住 = false;
            this.plC_Button_Cancel.按鈕型態 = MyUI.PLC_Button.StatusEnum.交替型;
            this.plC_Button_Cancel.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_Cancel.提示文字 = "Login";
            this.plC_Button_Cancel.文字鎖住 = false;
            this.plC_Button_Cancel.狀態OFF圖片 = global::Basic.Properties.Resources.Cancel;
            this.plC_Button_Cancel.狀態ON圖片 = global::Basic.Properties.Resources.Cancel;
            this.plC_Button_Cancel.讀取位元反向 = false;
            this.plC_Button_Cancel.讀寫鎖住 = false;
            this.plC_Button_Cancel.起始狀態 = false;
            this.plC_Button_Cancel.音效 = false;
            this.plC_Button_Cancel.顯示 = false;
            this.plC_Button_Cancel.顯示狀態 = false;
            this.plC_Button_Cancel.btnClick += new System.EventHandler(this.plC_Button_Cancel_btnClick);
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(96, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(12, 79);
            this.panel6.TabIndex = 34;
            // 
            // plC_Button_Confirm
            // 
            this.plC_Button_Confirm.Bool = false;
            this.plC_Button_Confirm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plC_Button_Confirm.but_press = false;
            this.plC_Button_Confirm.Dock = System.Windows.Forms.DockStyle.Right;
            this.plC_Button_Confirm.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_Confirm.Location = new System.Drawing.Point(108, 3);
            this.plC_Button_Confirm.Name = "plC_Button_Confirm";
            this.plC_Button_Confirm.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_Confirm.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_Confirm.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_Confirm.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_Confirm.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_Confirm.ON_背景顏色 = System.Drawing.Color.Yellow;
            this.plC_Button_Confirm.Size = new System.Drawing.Size(80, 79);
            this.plC_Button_Confirm.Style = MyUI.PLC_Button.StyleEnum.自定義;
            this.plC_Button_Confirm.TabIndex = 32;
            this.plC_Button_Confirm.TabStop = false;
            this.plC_Button_Confirm.事件驅動 = false;
            this.plC_Button_Confirm.字型鎖住 = false;
            this.plC_Button_Confirm.按鈕型態 = MyUI.PLC_Button.StatusEnum.交替型;
            this.plC_Button_Confirm.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_Confirm.提示文字 = "Login";
            this.plC_Button_Confirm.文字鎖住 = false;
            this.plC_Button_Confirm.狀態OFF圖片 = global::Basic.Properties.Resources.Confirn;
            this.plC_Button_Confirm.狀態ON圖片 = global::Basic.Properties.Resources.Confirn;
            this.plC_Button_Confirm.讀取位元反向 = false;
            this.plC_Button_Confirm.讀寫鎖住 = false;
            this.plC_Button_Confirm.起始狀態 = false;
            this.plC_Button_Confirm.音效 = false;
            this.plC_Button_Confirm.顯示 = false;
            this.plC_Button_Confirm.顯示狀態 = false;
            this.plC_Button_Confirm.btnClick += new System.EventHandler(this.plC_Button_Confirm_btnClick);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(716, 11);
            this.panel2.TabIndex = 38;
            // 
            // panel_TopBox
            // 
            this.panel_TopBox.Controls.Add(this.panel4);
            this.panel_TopBox.Controls.Add(this.panel_ImageBox);
            this.panel_TopBox.Controls.Add(this.panel7);
            this.panel_TopBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_TopBox.Location = new System.Drawing.Point(4, 39);
            this.panel_TopBox.Name = "panel_TopBox";
            this.panel_TopBox.Size = new System.Drawing.Size(716, 162);
            this.panel_TopBox.TabIndex = 40;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label_Content);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(115, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(601, 162);
            this.panel4.TabIndex = 46;
            // 
            // label_Content
            // 
            this.label_Content.BackColor = System.Drawing.Color.White;
            this.label_Content.BackgroundColor = System.Drawing.Color.White;
            this.label_Content.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.label_Content.BorderRadius = 10;
            this.label_Content.BorderSize = 0;
            this.label_Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Content.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Content.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_Content.ForeColor = System.Drawing.Color.Transparent;
            this.label_Content.GUID = "";
            this.label_Content.Location = new System.Drawing.Point(23, 0);
            this.label_Content.Name = "label_Content";
            this.label_Content.ShadowColor = System.Drawing.Color.DimGray;
            this.label_Content.ShadowSize = 0;
            this.label_Content.Size = new System.Drawing.Size(578, 162);
            this.label_Content.TabIndex = 1;
            this.label_Content.Text = "rJ_Lable1";
            this.label_Content.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_Content.TextColor = System.Drawing.Color.Black;
            this.label_Content.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(23, 162);
            this.panel3.TabIndex = 0;
            // 
            // panel_ImageBox
            // 
            this.panel_ImageBox.Controls.Add(this.panel_Image);
            this.panel_ImageBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_ImageBox.Location = new System.Drawing.Point(17, 0);
            this.panel_ImageBox.Name = "panel_ImageBox";
            this.panel_ImageBox.Size = new System.Drawing.Size(98, 162);
            this.panel_ImageBox.TabIndex = 45;
            // 
            // panel_Image
            // 
            this.panel_Image.BackgroundImage = global::Basic.Properties.Resources.Warning;
            this.panel_Image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel_Image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Image.Location = new System.Drawing.Point(0, 0);
            this.panel_Image.Name = "panel_Image";
            this.panel_Image.Size = new System.Drawing.Size(98, 162);
            this.panel_Image.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(17, 162);
            this.panel7.TabIndex = 47;
            // 
            // MyMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(724, 290);
            this.Controls.Add(this.panel_TopBox);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MyMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Title";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyMessageBox_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel_TopBox.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel_ImageBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MyUI.PLC_Button plC_Button_Confirm;
        private System.Windows.Forms.Panel panel5;
        private MyUI.PLC_Button plC_Button_Cancel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel_TopBox;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel_ImageBox;
        private System.Windows.Forms.Panel panel_Image;
        private System.Windows.Forms.Panel panel7;
        private MyUI.RJ_Lable label_Content;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Timer timer;
    }
}