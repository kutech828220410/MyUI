namespace MeasureSystemUI
{
    partial class H_ImageCopier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_ImageCopier));
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_UIName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.plC_NumBox_DstImageHandle = new MyUI.PLC_NumBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.plC_NumBox_SrcImageHandle = new MyUI.PLC_NumBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.plC_NumBox_VagaHandle = new MyUI.PLC_NumBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.plC_Button_Copy = new MyUI.PLC_Button();
            this.panel13.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel16.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.panel15);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel13.Location = new System.Drawing.Point(0, 0);
            this.panel13.Margin = new System.Windows.Forms.Padding(0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(188, 20);
            this.panel13.TabIndex = 3;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.label7);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(188, 20);
            this.panel15.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(188, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "ImageCopier";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_UIName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 20);
            this.panel1.TabIndex = 4;
            // 
            // label_UIName
            // 
            this.label_UIName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_UIName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_UIName.Location = new System.Drawing.Point(0, 0);
            this.label_UIName.Margin = new System.Windows.Forms.Padding(0);
            this.label_UIName.Name = "label_UIName";
            this.label_UIName.Size = new System.Drawing.Size(188, 20);
            this.label_UIName.TabIndex = 5;
            this.label_UIName.Text = "UI Name";
            this.label_UIName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 101);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Properties";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel16, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(182, 80);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel7.Controls.Add(this.label3);
            this.panel7.Location = new System.Drawing.Point(4, 56);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(83, 20);
            this.panel7.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "DstImageHandle ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            this.panel6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel6.Controls.Add(this.label2);
            this.panel6.Location = new System.Drawing.Point(4, 30);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(83, 19);
            this.panel6.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "SrcImageHandle";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel4.Controls.Add(this.plC_NumBox_DstImageHandle);
            this.panel4.Location = new System.Drawing.Point(94, 56);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(84, 20);
            this.panel4.TabIndex = 1;
            // 
            // plC_NumBox_DstImageHandle
            // 
            this.plC_NumBox_DstImageHandle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_NumBox_DstImageHandle.Location = new System.Drawing.Point(0, 0);
            this.plC_NumBox_DstImageHandle.Name = "plC_NumBox_DstImageHandle";
            this.plC_NumBox_DstImageHandle.ReadOnly = false;
            this.plC_NumBox_DstImageHandle.Size = new System.Drawing.Size(84, 22);
            this.plC_NumBox_DstImageHandle.TabIndex = 0;
            this.plC_NumBox_DstImageHandle.Text = "0";
            this.plC_NumBox_DstImageHandle.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_DstImageHandle.密碼欄位 = false;
            this.plC_NumBox_DstImageHandle.小數點位置 = 0;
            this.plC_NumBox_DstImageHandle.音效 = true;
            this.plC_NumBox_DstImageHandle.顯示螢幕小鍵盤 = true;
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel3.Controls.Add(this.plC_NumBox_SrcImageHandle);
            this.panel3.Location = new System.Drawing.Point(94, 30);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(84, 19);
            this.panel3.TabIndex = 1;
            // 
            // plC_NumBox_SrcImageHandle
            // 
            this.plC_NumBox_SrcImageHandle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_NumBox_SrcImageHandle.Location = new System.Drawing.Point(0, 0);
            this.plC_NumBox_SrcImageHandle.Name = "plC_NumBox_SrcImageHandle";
            this.plC_NumBox_SrcImageHandle.ReadOnly = false;
            this.plC_NumBox_SrcImageHandle.Size = new System.Drawing.Size(84, 22);
            this.plC_NumBox_SrcImageHandle.TabIndex = 0;
            this.plC_NumBox_SrcImageHandle.Text = "0";
            this.plC_NumBox_SrcImageHandle.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_SrcImageHandle.密碼欄位 = false;
            this.plC_NumBox_SrcImageHandle.小數點位置 = 0;
            this.plC_NumBox_SrcImageHandle.音效 = true;
            this.plC_NumBox_SrcImageHandle.顯示螢幕小鍵盤 = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(83, 19);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "VagaHandle";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel16
            // 
            this.panel16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel16.Controls.Add(this.plC_NumBox_VagaHandle);
            this.panel16.Location = new System.Drawing.Point(94, 4);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(84, 19);
            this.panel16.TabIndex = 0;
            // 
            // plC_NumBox_VagaHandle
            // 
            this.plC_NumBox_VagaHandle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_NumBox_VagaHandle.Location = new System.Drawing.Point(0, 0);
            this.plC_NumBox_VagaHandle.Name = "plC_NumBox_VagaHandle";
            this.plC_NumBox_VagaHandle.ReadOnly = true;
            this.plC_NumBox_VagaHandle.Size = new System.Drawing.Size(84, 22);
            this.plC_NumBox_VagaHandle.TabIndex = 0;
            this.plC_NumBox_VagaHandle.Text = "0";
            this.plC_NumBox_VagaHandle.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_VagaHandle.密碼欄位 = false;
            this.plC_NumBox_VagaHandle.小數點位置 = 0;
            this.plC_NumBox_VagaHandle.音效 = true;
            this.plC_NumBox_VagaHandle.顯示螢幕小鍵盤 = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.plC_Button_Copy, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 141);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(188, 33);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // plC_Button_Copy
            // 
            this.plC_Button_Copy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_Button_Copy.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_Copy.Location = new System.Drawing.Point(95, 1);
            this.plC_Button_Copy.Margin = new System.Windows.Forms.Padding(1);
            this.plC_Button_Copy.Name = "plC_Button_Copy";
            this.plC_Button_Copy.OFF_文字內容 = "Copy";
            this.plC_Button_Copy.OFF_文字字體 = new System.Drawing.Font("新細明體", 9F);
            this.plC_Button_Copy.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_Copy.ON_文字內容 = "Copy";
            this.plC_Button_Copy.ON_文字字體 = new System.Drawing.Font("新細明體", 9F);
            this.plC_Button_Copy.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_Copy.Size = new System.Drawing.Size(92, 31);
            this.plC_Button_Copy.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_Copy.TabIndex = 2;
            this.plC_Button_Copy.字型鎖住 = false;
            this.plC_Button_Copy.按鈕型態 = MyUI.PLC_Button.StatusEnum.保持型;
            this.plC_Button_Copy.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_Copy.文字鎖住 = false;
            this.plC_Button_Copy.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_Copy.狀態OFF圖片")));
            this.plC_Button_Copy.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_Copy.狀態ON圖片")));
            this.plC_Button_Copy.讀寫鎖住 = false;
            this.plC_Button_Copy.音效 = true;
            this.plC_Button_Copy.顯示 = false;
            this.plC_Button_Copy.顯示狀態 = false;
            // 
            // H_ImageCopier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel13);
            this.Name = "H_ImageCopier";
            this.Size = new System.Drawing.Size(188, 174);
            this.panel13.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_UIName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private MyUI.PLC_NumBox plC_NumBox_DstImageHandle;
        private System.Windows.Forms.Panel panel3;
        private MyUI.PLC_NumBox plC_NumBox_SrcImageHandle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel16;
        private MyUI.PLC_NumBox plC_NumBox_VagaHandle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private MyUI.PLC_Button plC_Button_Copy;
    }
}
