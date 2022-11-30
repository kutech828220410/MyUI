namespace LadderForm
{
    partial class FindDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindDevice));
            this.exButton_FindNext = new MyUI.ExButton();
            this.exButton_Close = new MyUI.ExButton();
            this.textBox_DeviceName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer_程序執行 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // exButton_FindNext
            // 
            this.exButton_FindNext.Location = new System.Drawing.Point(235, 6);
            this.exButton_FindNext.Name = "exButton_FindNext";
            this.exButton_FindNext.OFF_文字內容 = "Find Next";
            this.exButton_FindNext.OFF_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_FindNext.ON_文字內容 = "Find Next";
            this.exButton_FindNext.ON_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_FindNext.Size = new System.Drawing.Size(110, 28);
            this.exButton_FindNext.TabIndex = 0;
            this.exButton_FindNext.字型鎖住 = true;
            this.exButton_FindNext.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_FindNext.文字鎖住 = true;
            this.exButton_FindNext.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_FindNext.狀態OFF圖片")));
            this.exButton_FindNext.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_FindNext.狀態ON圖片")));
            this.exButton_FindNext.讀寫鎖住 = false;
            this.exButton_FindNext.btnClick += new System.EventHandler(this.exButton_FindNext_btnClick);
            // 
            // exButton_Close
            // 
            this.exButton_Close.Location = new System.Drawing.Point(235, 40);
            this.exButton_Close.Name = "exButton_Close";
            this.exButton_Close.OFF_文字內容 = "Close";
            this.exButton_Close.OFF_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Close.ON_文字內容 = "Close";
            this.exButton_Close.ON_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Close.Size = new System.Drawing.Size(110, 28);
            this.exButton_Close.TabIndex = 1;
            this.exButton_Close.字型鎖住 = true;
            this.exButton_Close.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_Close.文字鎖住 = true;
            this.exButton_Close.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Close.狀態OFF圖片")));
            this.exButton_Close.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Close.狀態ON圖片")));
            this.exButton_Close.讀寫鎖住 = true;
            this.exButton_Close.btnClick += new System.EventHandler(this.exButton_Close_btnClick);
            // 
            // textBox_DeviceName
            // 
            this.textBox_DeviceName.Location = new System.Drawing.Point(20, 43);
            this.textBox_DeviceName.Name = "textBox_DeviceName";
            this.textBox_DeviceName.Size = new System.Drawing.Size(196, 22);
            this.textBox_DeviceName.TabIndex = 0;
            this.textBox_DeviceName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_DeviceName_KeyDown);
            this.textBox_DeviceName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_DeviceName_KeyPress);
            this.textBox_DeviceName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_DeviceName_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(18, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Device:";
            // 
            // timer_程序執行
            // 
            this.timer_程序執行.Interval = 10;
            this.timer_程序執行.Tick += new System.EventHandler(this.timer_程序執行_Tick);
            // 
            // FindDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 75);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_DeviceName);
            this.Controls.Add(this.exButton_Close);
            this.Controls.Add(this.exButton_FindNext);
            this.DoubleBuffered = true;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(372, 114);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(372, 114);
            this.Name = "FindDevice";
            this.ShowIcon = false;
            this.Text = "FindDevice";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindDevice_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FindDevice_FormClosed);
            this.Load += new System.EventHandler(this.FindDevice_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyUI.ExButton exButton_FindNext;
        private MyUI.ExButton exButton_Close;
        private System.Windows.Forms.TextBox textBox_DeviceName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer_程序執行;
    }
}