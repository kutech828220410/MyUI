namespace LadderForm
{
    partial class ReplaceDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReplaceDevice));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Earlier_device = new System.Windows.Forms.TextBox();
            this.exButton_Close = new MyUI.ExButton();
            this.exButton_FindNext = new MyUI.ExButton();
            this.exButton_Replace = new MyUI.ExButton();
            this.exButton_Replace_all = new MyUI.ExButton();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_New_device = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nud_num_point = new System.Windows.Forms.NumericUpDown();
            this.checkBox_move_cooments = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nud_specified_range_upper = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nud_specified_range_lower = new System.Windows.Forms.NumericUpDown();
            this.radioButton_specified_range = new System.Windows.Forms.RadioButton();
            this.radioButton_from_cursor_to_buttom = new System.Windows.Forms.RadioButton();
            this.radioButton_from_top_to_buttom = new System.Windows.Forms.RadioButton();
            this.timer_程序執行 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.nud_num_point)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_specified_range_upper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_specified_range_lower)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Earlier device:";
            // 
            // textBox_Earlier_device
            // 
            this.textBox_Earlier_device.Location = new System.Drawing.Point(12, 37);
            this.textBox_Earlier_device.Name = "textBox_Earlier_device";
            this.textBox_Earlier_device.Size = new System.Drawing.Size(196, 22);
            this.textBox_Earlier_device.TabIndex = 0;
            this.textBox_Earlier_device.TextChanged += new System.EventHandler(this.textBox_Earlier_device_TextChanged);
            // 
            // exButton_Close
            // 
            this.exButton_Close.Location = new System.Drawing.Point(242, 114);
            this.exButton_Close.Name = "exButton_Close";
            this.exButton_Close.OFF_文字內容 = "Close";
            this.exButton_Close.OFF_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Close.ON_文字內容 = "Close";
            this.exButton_Close.ON_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Close.Size = new System.Drawing.Size(110, 28);
            this.exButton_Close.TabIndex = 6;
            this.exButton_Close.字型鎖住 = true;
            this.exButton_Close.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_Close.文字鎖住 = true;
            this.exButton_Close.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Close.狀態OFF圖片")));
            this.exButton_Close.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Close.狀態ON圖片")));
            this.exButton_Close.讀寫鎖住 = true;
            this.exButton_Close.btnClick += new System.EventHandler(this.exButton_Close_btnClick);
            // 
            // exButton_FindNext
            // 
            this.exButton_FindNext.Location = new System.Drawing.Point(242, 12);
            this.exButton_FindNext.Name = "exButton_FindNext";
            this.exButton_FindNext.OFF_文字內容 = "Find Next";
            this.exButton_FindNext.OFF_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_FindNext.ON_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_FindNext.Size = new System.Drawing.Size(110, 28);
            this.exButton_FindNext.TabIndex = 5;
            this.exButton_FindNext.字型鎖住 = true;
            this.exButton_FindNext.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_FindNext.文字鎖住 = true;
            this.exButton_FindNext.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_FindNext.狀態OFF圖片")));
            this.exButton_FindNext.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_FindNext.狀態ON圖片")));
            this.exButton_FindNext.讀寫鎖住 = false;
            this.exButton_FindNext.btnClick += new System.EventHandler(this.exButton_FindNext_btnClick);
            // 
            // exButton_Replace
            // 
            this.exButton_Replace.Location = new System.Drawing.Point(242, 46);
            this.exButton_Replace.Name = "exButton_Replace";
            this.exButton_Replace.OFF_文字內容 = "Replace";
            this.exButton_Replace.OFF_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Replace.ON_文字內容 = "Replace";
            this.exButton_Replace.ON_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Replace.Size = new System.Drawing.Size(110, 28);
            this.exButton_Replace.TabIndex = 8;
            this.exButton_Replace.字型鎖住 = true;
            this.exButton_Replace.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_Replace.文字鎖住 = true;
            this.exButton_Replace.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Replace.狀態OFF圖片")));
            this.exButton_Replace.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Replace.狀態ON圖片")));
            this.exButton_Replace.讀寫鎖住 = false;
            this.exButton_Replace.btnClick += new System.EventHandler(this.exButton_Replace_btnClick);
            // 
            // exButton_Replace_all
            // 
            this.exButton_Replace_all.Location = new System.Drawing.Point(242, 80);
            this.exButton_Replace_all.Name = "exButton_Replace_all";
            this.exButton_Replace_all.OFF_文字內容 = "Replace all";
            this.exButton_Replace_all.OFF_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Replace_all.ON_文字內容 = "Replace all";
            this.exButton_Replace_all.ON_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Replace_all.Size = new System.Drawing.Size(110, 28);
            this.exButton_Replace_all.TabIndex = 9;
            this.exButton_Replace_all.字型鎖住 = true;
            this.exButton_Replace_all.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_Replace_all.文字鎖住 = true;
            this.exButton_Replace_all.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Replace_all.狀態OFF圖片")));
            this.exButton_Replace_all.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Replace_all.狀態ON圖片")));
            this.exButton_Replace_all.讀寫鎖住 = false;
            this.exButton_Replace_all.btnClick += new System.EventHandler(this.exButton_Replace_all_btnClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F);
            this.label2.Location = new System.Drawing.Point(11, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "New device:";
            // 
            // textBox_New_device
            // 
            this.textBox_New_device.Location = new System.Drawing.Point(13, 97);
            this.textBox_New_device.Name = "textBox_New_device";
            this.textBox_New_device.Size = new System.Drawing.Size(196, 22);
            this.textBox_New_device.TabIndex = 1;
            this.textBox_New_device.TextChanged += new System.EventHandler(this.textBox_New_device_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F);
            this.label3.Location = new System.Drawing.Point(12, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "No. of substitute points";
            // 
            // nud_num_point
            // 
            this.nud_num_point.Location = new System.Drawing.Point(166, 137);
            this.nud_num_point.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_num_point.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_num_point.Name = "nud_num_point";
            this.nud_num_point.Size = new System.Drawing.Size(65, 22);
            this.nud_num_point.TabIndex = 2;
            this.nud_num_point.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBox_move_cooments
            // 
            this.checkBox_move_cooments.AutoSize = true;
            this.checkBox_move_cooments.Font = new System.Drawing.Font("新細明體", 12F);
            this.checkBox_move_cooments.Location = new System.Drawing.Point(15, 173);
            this.checkBox_move_cooments.Name = "checkBox_move_cooments";
            this.checkBox_move_cooments.Size = new System.Drawing.Size(207, 20);
            this.checkBox_move_cooments.TabIndex = 14;
            this.checkBox_move_cooments.Text = "Move commemts and aliases";
            this.checkBox_move_cooments.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nud_specified_range_upper);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nud_specified_range_lower);
            this.groupBox1.Controls.Add(this.radioButton_specified_range);
            this.groupBox1.Controls.Add(this.radioButton_from_cursor_to_buttom);
            this.groupBox1.Controls.Add(this.radioButton_from_top_to_buttom);
            this.groupBox1.Font = new System.Drawing.Font("新細明體", 12F);
            this.groupBox1.Location = new System.Drawing.Point(12, 201);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 139);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Find direction";
            // 
            // nud_specified_range_upper
            // 
            this.nud_specified_range_upper.Enabled = false;
            this.nud_specified_range_upper.Location = new System.Drawing.Point(195, 99);
            this.nud_specified_range_upper.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_specified_range_upper.Name = "nud_specified_range_upper";
            this.nud_specified_range_upper.Size = new System.Drawing.Size(78, 27);
            this.nud_specified_range_upper.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F);
            this.label4.Location = new System.Drawing.Point(151, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "--";
            // 
            // nud_specified_range_lower
            // 
            this.nud_specified_range_lower.Enabled = false;
            this.nud_specified_range_lower.Location = new System.Drawing.Point(48, 99);
            this.nud_specified_range_lower.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_specified_range_lower.Name = "nud_specified_range_lower";
            this.nud_specified_range_lower.Size = new System.Drawing.Size(78, 27);
            this.nud_specified_range_lower.TabIndex = 14;
            // 
            // radioButton_specified_range
            // 
            this.radioButton_specified_range.AutoSize = true;
            this.radioButton_specified_range.Location = new System.Drawing.Point(24, 74);
            this.radioButton_specified_range.Name = "radioButton_specified_range";
            this.radioButton_specified_range.Size = new System.Drawing.Size(123, 20);
            this.radioButton_specified_range.TabIndex = 2;
            this.radioButton_specified_range.Text = "Specified range";
            this.radioButton_specified_range.UseVisualStyleBackColor = true;
            this.radioButton_specified_range.CheckedChanged += new System.EventHandler(this.radioButton_specified_range_CheckedChanged);
            // 
            // radioButton_from_cursor_to_buttom
            // 
            this.radioButton_from_cursor_to_buttom.AutoSize = true;
            this.radioButton_from_cursor_to_buttom.Location = new System.Drawing.Point(24, 48);
            this.radioButton_from_cursor_to_buttom.Name = "radioButton_from_cursor_to_buttom";
            this.radioButton_from_cursor_to_buttom.Size = new System.Drawing.Size(166, 20);
            this.radioButton_from_cursor_to_buttom.TabIndex = 1;
            this.radioButton_from_cursor_to_buttom.Text = "From cursor to buttom";
            this.radioButton_from_cursor_to_buttom.UseVisualStyleBackColor = true;
            this.radioButton_from_cursor_to_buttom.CheckedChanged += new System.EventHandler(this.radioButton_from_cursor_to_buttom_CheckedChanged);
            // 
            // radioButton_from_top_to_buttom
            // 
            this.radioButton_from_top_to_buttom.AutoSize = true;
            this.radioButton_from_top_to_buttom.Checked = true;
            this.radioButton_from_top_to_buttom.Location = new System.Drawing.Point(24, 22);
            this.radioButton_from_top_to_buttom.Name = "radioButton_from_top_to_buttom";
            this.radioButton_from_top_to_buttom.Size = new System.Drawing.Size(147, 20);
            this.radioButton_from_top_to_buttom.TabIndex = 0;
            this.radioButton_from_top_to_buttom.TabStop = true;
            this.radioButton_from_top_to_buttom.Text = "From top to buttom";
            this.radioButton_from_top_to_buttom.UseVisualStyleBackColor = true;
            this.radioButton_from_top_to_buttom.CheckedChanged += new System.EventHandler(this.radioButton_from_top_to_buttom_CheckedChanged);
            // 
            // timer_程序執行
            // 
            this.timer_程序執行.Interval = 1;
            this.timer_程序執行.Tick += new System.EventHandler(this.timer_程序執行_Tick);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // ReplaceDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 352);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox_move_cooments);
            this.Controls.Add(this.nud_num_point);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_New_device);
            this.Controls.Add(this.exButton_Replace_all);
            this.Controls.Add(this.exButton_Replace);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_Earlier_device);
            this.Controls.Add(this.exButton_Close);
            this.Controls.Add(this.exButton_FindNext);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(377, 391);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(377, 391);
            this.Name = "ReplaceDevice";
            this.ShowIcon = false;
            this.Text = "ReplaceDevice";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReplaceDevice_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ReplaceDevice_FormClosed);
            this.Load += new System.EventHandler(this.ReplaceDevice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nud_num_point)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_specified_range_upper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_specified_range_lower)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Earlier_device;
        private MyUI.ExButton exButton_Close;
        private MyUI.ExButton exButton_FindNext;
        private MyUI.ExButton exButton_Replace;
        private MyUI.ExButton exButton_Replace_all;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_New_device;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nud_num_point;
        private System.Windows.Forms.CheckBox checkBox_move_cooments;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nud_specified_range_upper;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nud_specified_range_lower;
        private System.Windows.Forms.RadioButton radioButton_specified_range;
        private System.Windows.Forms.RadioButton radioButton_from_cursor_to_buttom;
        private System.Windows.Forms.RadioButton radioButton_from_top_to_buttom;
        private System.Windows.Forms.Timer timer_程序執行;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}