﻿namespace LeadShineUI
{
    partial class DMC3000
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DMC3000));
            this.panel_Open = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel67 = new System.Windows.Forms.Panel();
            this.label_CycleTime = new System.Windows.Forms.Label();
            this.label_單位 = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.numWordTextBox_StreamName = new MyUI.NumWordTextBox();
            this.plC_Button_Open = new MyUI.PLC_Button();
            this.panel_TAB = new System.Windows.Forms.Panel();
            this.panel_Open.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel67.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Open
            // 
            this.panel_Open.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_Open.Controls.Add(this.panel1);
            this.panel_Open.Controls.Add(this.plC_Button_Open);
            this.panel_Open.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Open.Location = new System.Drawing.Point(0, 0);
            this.panel_Open.Name = "panel_Open";
            this.panel_Open.Size = new System.Drawing.Size(812, 59);
            this.panel_Open.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel67);
            this.panel1.Controls.Add(this.groupBox14);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(576, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 55);
            this.panel1.TabIndex = 40;
            // 
            // panel67
            // 
            this.panel67.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel67.Controls.Add(this.label_CycleTime);
            this.panel67.Controls.Add(this.label_單位);
            this.panel67.Location = new System.Drawing.Point(3, 14);
            this.panel67.Name = "panel67";
            this.panel67.Size = new System.Drawing.Size(104, 31);
            this.panel67.TabIndex = 39;
            // 
            // label_CycleTime
            // 
            this.label_CycleTime.AutoSize = true;
            this.label_CycleTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label_CycleTime.Font = new System.Drawing.Font("新細明體", 12F);
            this.label_CycleTime.Location = new System.Drawing.Point(27, 6);
            this.label_CycleTime.Name = "label_CycleTime";
            this.label_CycleTime.Size = new System.Drawing.Size(32, 16);
            this.label_CycleTime.TabIndex = 16;
            this.label_CycleTime.Text = "000";
            // 
            // label_單位
            // 
            this.label_單位.AutoSize = true;
            this.label_單位.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label_單位.Font = new System.Drawing.Font("新細明體", 12F);
            this.label_單位.Location = new System.Drawing.Point(75, 6);
            this.label_單位.Name = "label_單位";
            this.label_單位.Size = new System.Drawing.Size(26, 16);
            this.label_單位.TabIndex = 15;
            this.label_單位.Text = "ms";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.numWordTextBox_StreamName);
            this.groupBox14.Location = new System.Drawing.Point(113, 4);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(116, 50);
            this.groupBox14.TabIndex = 8;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "檔案名稱";
            // 
            // numWordTextBox_StreamName
            // 
            this.numWordTextBox_StreamName.Enabled = false;
            this.numWordTextBox_StreamName.Location = new System.Drawing.Point(4, 19);
            this.numWordTextBox_StreamName.Name = "numWordTextBox_StreamName";
            this.numWordTextBox_StreamName.ReadOnly = true;
            this.numWordTextBox_StreamName.Size = new System.Drawing.Size(110, 22);
            this.numWordTextBox_StreamName.TabIndex = 5;
            this.numWordTextBox_StreamName.Text = "DMC3000-001";
            // 
            // plC_Button_Open
            // 
            this.plC_Button_Open.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_Open.Location = new System.Drawing.Point(5, 7);
            this.plC_Button_Open.Name = "plC_Button_Open";
            this.plC_Button_Open.OFF_文字內容 = "Open";
            this.plC_Button_Open.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_Open.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_Open.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_Open.ON_文字內容 = "Open";
            this.plC_Button_Open.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_Open.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_Open.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_Open.Size = new System.Drawing.Size(88, 41);
            this.plC_Button_Open.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_Open.TabIndex = 0;
            this.plC_Button_Open.字型鎖住 = false;
            this.plC_Button_Open.按鈕型態 = MyUI.PLC_Button.StatusEnum.保持型;
            this.plC_Button_Open.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_Open.文字鎖住 = false;
            this.plC_Button_Open.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_Open.狀態OFF圖片")));
            this.plC_Button_Open.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_Open.狀態ON圖片")));
            this.plC_Button_Open.讀寫鎖住 = false;
            this.plC_Button_Open.音效 = true;
            this.plC_Button_Open.顯示 = false;
            this.plC_Button_Open.顯示狀態 = false;
            this.plC_Button_Open.btnClick += new System.EventHandler(this.plC_Button_Open_btnClick);
            // 
            // panel_TAB
            // 
            this.panel_TAB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_TAB.Location = new System.Drawing.Point(0, 59);
            this.panel_TAB.Name = "panel_TAB";
            this.panel_TAB.Size = new System.Drawing.Size(812, 399);
            this.panel_TAB.TabIndex = 5;
            // 
            // DMC3000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel_TAB);
            this.Controls.Add(this.panel_Open);
            this.Name = "DMC3000";
            this.Size = new System.Drawing.Size(812, 458);
            this.panel_Open.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel67.ResumeLayout(false);
            this.panel67.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Open;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel67;
        private System.Windows.Forms.Label label_CycleTime;
        private System.Windows.Forms.Label label_單位;
        private System.Windows.Forms.GroupBox groupBox14;
        private MyUI.NumWordTextBox numWordTextBox_StreamName;
        private MyUI.PLC_Button plC_Button_Open;
        private System.Windows.Forms.Panel panel_TAB;
    }
}
