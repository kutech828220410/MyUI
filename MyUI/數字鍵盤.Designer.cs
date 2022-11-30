namespace MyUI
{
    partial class 數字鍵盤
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(數字鍵盤));
            this.textBox_Value = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.exButton_Enter = new MyUI.ExButton();
            this.exButton_Dot = new MyUI.ExButton();
            this.exButton_0 = new MyUI.ExButton();
            this.exButton_正負 = new MyUI.ExButton();
            this.exButton_3 = new MyUI.ExButton();
            this.exButton_2 = new MyUI.ExButton();
            this.exButton_1 = new MyUI.ExButton();
            this.exButton_CE = new MyUI.ExButton();
            this.exButton_6 = new MyUI.ExButton();
            this.exButton_5 = new MyUI.ExButton();
            this.exButton_4 = new MyUI.ExButton();
            this.exButton_Backspace = new MyUI.ExButton();
            this.exButton_9 = new MyUI.ExButton();
            this.exButton_8 = new MyUI.ExButton();
            this.exButton_7 = new MyUI.ExButton();
            this.SuspendLayout();
            // 
            // textBox_Value
            // 
            this.textBox_Value.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox_Value.Font = new System.Drawing.Font("新細明體", 20F);
            this.textBox_Value.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_Value.Location = new System.Drawing.Point(99, 12);
            this.textBox_Value.Name = "textBox_Value";
            this.textBox_Value.ReadOnly = true;
            this.textBox_Value.Size = new System.Drawing.Size(234, 39);
            this.textBox_Value.TabIndex = 16;
            this.textBox_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Value_KeyDown);
            this.textBox_Value.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_Value_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("新細明體", 20F);
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 29);
            this.label1.TabIndex = 17;
            this.label1.Text = "Value";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // exButton_Enter
            // 
            this.exButton_Enter.Location = new System.Drawing.Point(253, 294);
            this.exButton_Enter.Name = "exButton_Enter";
            this.exButton_Enter.OFF_文字內容 = "↵ ";
            this.exButton_Enter.OFF_文字字體 = new System.Drawing.Font("新細明體", 60F);
            this.exButton_Enter.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_Enter.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_Enter.ON_文字內容 = "↵ ";
            this.exButton_Enter.ON_文字字體 = new System.Drawing.Font("新細明體", 60F);
            this.exButton_Enter.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_Enter.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_Enter.Size = new System.Drawing.Size(80, 80);
            this.exButton_Enter.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_Enter.TabIndex = 32;
            this.exButton_Enter.字型鎖住 = true;
            this.exButton_Enter.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_Enter.文字鎖住 = true;
            this.exButton_Enter.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Enter.狀態OFF圖片")));
            this.exButton_Enter.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Enter.狀態ON圖片")));
            this.exButton_Enter.讀寫鎖住 = true;
            this.exButton_Enter.音效 = false;
            this.exButton_Enter.顯示狀態 = false;
            // 
            // exButton_Dot
            // 
            this.exButton_Dot.Location = new System.Drawing.Point(173, 294);
            this.exButton_Dot.Name = "exButton_Dot";
            this.exButton_Dot.OFF_文字內容 = ".";
            this.exButton_Dot.OFF_文字字體 = new System.Drawing.Font("新細明體", 60F);
            this.exButton_Dot.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_Dot.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_Dot.ON_文字內容 = ".";
            this.exButton_Dot.ON_文字字體 = new System.Drawing.Font("新細明體", 60F);
            this.exButton_Dot.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_Dot.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_Dot.Size = new System.Drawing.Size(80, 80);
            this.exButton_Dot.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_Dot.TabIndex = 31;
            this.exButton_Dot.字型鎖住 = true;
            this.exButton_Dot.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_Dot.文字鎖住 = true;
            this.exButton_Dot.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Dot.狀態OFF圖片")));
            this.exButton_Dot.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Dot.狀態ON圖片")));
            this.exButton_Dot.讀寫鎖住 = true;
            this.exButton_Dot.音效 = false;
            this.exButton_Dot.顯示狀態 = false;
            // 
            // exButton_0
            // 
            this.exButton_0.Location = new System.Drawing.Point(93, 294);
            this.exButton_0.Name = "exButton_0";
            this.exButton_0.OFF_文字內容 = "0";
            this.exButton_0.OFF_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_0.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_0.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_0.ON_文字內容 = "0";
            this.exButton_0.ON_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_0.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_0.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_0.Size = new System.Drawing.Size(80, 80);
            this.exButton_0.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_0.TabIndex = 30;
            this.exButton_0.字型鎖住 = true;
            this.exButton_0.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_0.文字鎖住 = true;
            this.exButton_0.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_0.狀態OFF圖片")));
            this.exButton_0.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_0.狀態ON圖片")));
            this.exButton_0.讀寫鎖住 = true;
            this.exButton_0.音效 = false;
            this.exButton_0.顯示狀態 = false;
            // 
            // exButton_正負
            // 
            this.exButton_正負.Location = new System.Drawing.Point(13, 294);
            this.exButton_正負.Name = "exButton_正負";
            this.exButton_正負.OFF_文字內容 = "±";
            this.exButton_正負.OFF_文字字體 = new System.Drawing.Font("新細明體", 30F);
            this.exButton_正負.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_正負.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_正負.ON_文字內容 = "±";
            this.exButton_正負.ON_文字字體 = new System.Drawing.Font("新細明體", 30F);
            this.exButton_正負.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_正負.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_正負.Size = new System.Drawing.Size(80, 80);
            this.exButton_正負.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_正負.TabIndex = 29;
            this.exButton_正負.字型鎖住 = true;
            this.exButton_正負.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_正負.文字鎖住 = true;
            this.exButton_正負.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_正負.狀態OFF圖片")));
            this.exButton_正負.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_正負.狀態ON圖片")));
            this.exButton_正負.讀寫鎖住 = true;
            this.exButton_正負.音效 = false;
            this.exButton_正負.顯示狀態 = false;
            // 
            // exButton_3
            // 
            this.exButton_3.Location = new System.Drawing.Point(173, 215);
            this.exButton_3.Name = "exButton_3";
            this.exButton_3.OFF_文字內容 = "3";
            this.exButton_3.OFF_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_3.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_3.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_3.ON_文字內容 = "3";
            this.exButton_3.ON_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_3.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_3.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_3.Size = new System.Drawing.Size(80, 80);
            this.exButton_3.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_3.TabIndex = 27;
            this.exButton_3.字型鎖住 = true;
            this.exButton_3.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_3.文字鎖住 = true;
            this.exButton_3.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_3.狀態OFF圖片")));
            this.exButton_3.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_3.狀態ON圖片")));
            this.exButton_3.讀寫鎖住 = true;
            this.exButton_3.音效 = false;
            this.exButton_3.顯示狀態 = false;
            // 
            // exButton_2
            // 
            this.exButton_2.Location = new System.Drawing.Point(93, 215);
            this.exButton_2.Name = "exButton_2";
            this.exButton_2.OFF_文字內容 = "2";
            this.exButton_2.OFF_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_2.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_2.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_2.ON_文字內容 = "2";
            this.exButton_2.ON_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_2.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_2.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_2.Size = new System.Drawing.Size(80, 80);
            this.exButton_2.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_2.TabIndex = 26;
            this.exButton_2.字型鎖住 = true;
            this.exButton_2.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_2.文字鎖住 = true;
            this.exButton_2.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_2.狀態OFF圖片")));
            this.exButton_2.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_2.狀態ON圖片")));
            this.exButton_2.讀寫鎖住 = true;
            this.exButton_2.音效 = false;
            this.exButton_2.顯示狀態 = false;
            // 
            // exButton_1
            // 
            this.exButton_1.Location = new System.Drawing.Point(13, 215);
            this.exButton_1.Name = "exButton_1";
            this.exButton_1.OFF_文字內容 = "1";
            this.exButton_1.OFF_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_1.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_1.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_1.ON_文字內容 = "1";
            this.exButton_1.ON_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_1.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_1.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_1.Size = new System.Drawing.Size(80, 80);
            this.exButton_1.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_1.TabIndex = 25;
            this.exButton_1.字型鎖住 = true;
            this.exButton_1.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_1.文字鎖住 = true;
            this.exButton_1.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_1.狀態OFF圖片")));
            this.exButton_1.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_1.狀態ON圖片")));
            this.exButton_1.讀寫鎖住 = true;
            this.exButton_1.音效 = false;
            this.exButton_1.顯示狀態 = false;
            // 
            // exButton_CE
            // 
            this.exButton_CE.Location = new System.Drawing.Point(253, 136);
            this.exButton_CE.Name = "exButton_CE";
            this.exButton_CE.OFF_文字內容 = "CE";
            this.exButton_CE.OFF_文字字體 = new System.Drawing.Font("新細明體", 30F);
            this.exButton_CE.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_CE.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_CE.ON_文字內容 = "CE";
            this.exButton_CE.ON_文字字體 = new System.Drawing.Font("新細明體", 30F);
            this.exButton_CE.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_CE.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_CE.Size = new System.Drawing.Size(80, 80);
            this.exButton_CE.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_CE.TabIndex = 24;
            this.exButton_CE.字型鎖住 = true;
            this.exButton_CE.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_CE.文字鎖住 = true;
            this.exButton_CE.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_CE.狀態OFF圖片")));
            this.exButton_CE.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_CE.狀態ON圖片")));
            this.exButton_CE.讀寫鎖住 = true;
            this.exButton_CE.音效 = false;
            this.exButton_CE.顯示狀態 = false;
            // 
            // exButton_6
            // 
            this.exButton_6.Location = new System.Drawing.Point(173, 136);
            this.exButton_6.Name = "exButton_6";
            this.exButton_6.OFF_文字內容 = "6";
            this.exButton_6.OFF_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_6.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_6.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_6.ON_文字內容 = "6";
            this.exButton_6.ON_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_6.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_6.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_6.Size = new System.Drawing.Size(80, 80);
            this.exButton_6.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_6.TabIndex = 23;
            this.exButton_6.字型鎖住 = true;
            this.exButton_6.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_6.文字鎖住 = true;
            this.exButton_6.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_6.狀態OFF圖片")));
            this.exButton_6.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_6.狀態ON圖片")));
            this.exButton_6.讀寫鎖住 = true;
            this.exButton_6.音效 = false;
            this.exButton_6.顯示狀態 = false;
            // 
            // exButton_5
            // 
            this.exButton_5.Location = new System.Drawing.Point(93, 136);
            this.exButton_5.Name = "exButton_5";
            this.exButton_5.OFF_文字內容 = "5";
            this.exButton_5.OFF_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_5.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_5.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_5.ON_文字內容 = "5";
            this.exButton_5.ON_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_5.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_5.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_5.Size = new System.Drawing.Size(80, 80);
            this.exButton_5.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_5.TabIndex = 22;
            this.exButton_5.字型鎖住 = true;
            this.exButton_5.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_5.文字鎖住 = true;
            this.exButton_5.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_5.狀態OFF圖片")));
            this.exButton_5.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_5.狀態ON圖片")));
            this.exButton_5.讀寫鎖住 = true;
            this.exButton_5.音效 = false;
            this.exButton_5.顯示狀態 = false;
            // 
            // exButton_4
            // 
            this.exButton_4.Location = new System.Drawing.Point(13, 136);
            this.exButton_4.Name = "exButton_4";
            this.exButton_4.OFF_文字內容 = "4";
            this.exButton_4.OFF_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_4.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_4.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_4.ON_文字內容 = "4";
            this.exButton_4.ON_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_4.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_4.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_4.Size = new System.Drawing.Size(80, 80);
            this.exButton_4.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_4.TabIndex = 21;
            this.exButton_4.字型鎖住 = true;
            this.exButton_4.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_4.文字鎖住 = true;
            this.exButton_4.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_4.狀態OFF圖片")));
            this.exButton_4.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_4.狀態ON圖片")));
            this.exButton_4.讀寫鎖住 = true;
            this.exButton_4.音效 = false;
            this.exButton_4.顯示狀態 = false;
            // 
            // exButton_Backspace
            // 
            this.exButton_Backspace.Location = new System.Drawing.Point(253, 57);
            this.exButton_Backspace.Name = "exButton_Backspace";
            this.exButton_Backspace.OFF_文字內容 = "←";
            this.exButton_Backspace.OFF_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_Backspace.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_Backspace.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_Backspace.ON_文字內容 = "←";
            this.exButton_Backspace.ON_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_Backspace.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_Backspace.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_Backspace.Size = new System.Drawing.Size(80, 80);
            this.exButton_Backspace.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_Backspace.TabIndex = 20;
            this.exButton_Backspace.字型鎖住 = true;
            this.exButton_Backspace.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_Backspace.文字鎖住 = true;
            this.exButton_Backspace.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Backspace.狀態OFF圖片")));
            this.exButton_Backspace.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Backspace.狀態ON圖片")));
            this.exButton_Backspace.讀寫鎖住 = true;
            this.exButton_Backspace.音效 = false;
            this.exButton_Backspace.顯示狀態 = false;
            // 
            // exButton_9
            // 
            this.exButton_9.Location = new System.Drawing.Point(173, 57);
            this.exButton_9.Name = "exButton_9";
            this.exButton_9.OFF_文字內容 = "9";
            this.exButton_9.OFF_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_9.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_9.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_9.ON_文字內容 = "9";
            this.exButton_9.ON_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_9.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_9.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_9.Size = new System.Drawing.Size(80, 80);
            this.exButton_9.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_9.TabIndex = 19;
            this.exButton_9.字型鎖住 = true;
            this.exButton_9.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_9.文字鎖住 = true;
            this.exButton_9.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_9.狀態OFF圖片")));
            this.exButton_9.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_9.狀態ON圖片")));
            this.exButton_9.讀寫鎖住 = true;
            this.exButton_9.音效 = false;
            this.exButton_9.顯示狀態 = false;
            // 
            // exButton_8
            // 
            this.exButton_8.Location = new System.Drawing.Point(93, 57);
            this.exButton_8.Name = "exButton_8";
            this.exButton_8.OFF_文字內容 = "8";
            this.exButton_8.OFF_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_8.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_8.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_8.ON_文字內容 = "8";
            this.exButton_8.ON_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_8.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_8.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_8.Size = new System.Drawing.Size(80, 80);
            this.exButton_8.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_8.TabIndex = 18;
            this.exButton_8.字型鎖住 = true;
            this.exButton_8.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_8.文字鎖住 = true;
            this.exButton_8.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_8.狀態OFF圖片")));
            this.exButton_8.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_8.狀態ON圖片")));
            this.exButton_8.讀寫鎖住 = true;
            this.exButton_8.音效 = false;
            this.exButton_8.顯示狀態 = false;
            // 
            // exButton_7
            // 
            this.exButton_7.Location = new System.Drawing.Point(13, 57);
            this.exButton_7.Name = "exButton_7";
            this.exButton_7.OFF_文字內容 = "7";
            this.exButton_7.OFF_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_7.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_7.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_7.ON_文字內容 = "7";
            this.exButton_7.ON_文字字體 = new System.Drawing.Font("新細明體", 40F);
            this.exButton_7.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_7.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_7.Size = new System.Drawing.Size(80, 80);
            this.exButton_7.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_7.TabIndex = 0;
            this.exButton_7.字型鎖住 = true;
            this.exButton_7.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_7.文字鎖住 = true;
            this.exButton_7.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_7.狀態OFF圖片")));
            this.exButton_7.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_7.狀態ON圖片")));
            this.exButton_7.讀寫鎖住 = true;
            this.exButton_7.音效 = false;
            this.exButton_7.顯示狀態 = false;
            // 
            // 數字鍵盤
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 384);
            this.Controls.Add(this.exButton_Enter);
            this.Controls.Add(this.exButton_Dot);
            this.Controls.Add(this.exButton_0);
            this.Controls.Add(this.exButton_正負);
            this.Controls.Add(this.exButton_3);
            this.Controls.Add(this.exButton_2);
            this.Controls.Add(this.exButton_1);
            this.Controls.Add(this.exButton_CE);
            this.Controls.Add(this.exButton_6);
            this.Controls.Add(this.exButton_5);
            this.Controls.Add(this.exButton_4);
            this.Controls.Add(this.exButton_Backspace);
            this.Controls.Add(this.exButton_9);
            this.Controls.Add(this.exButton_8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_Value);
            this.Controls.Add(this.exButton_7);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "數字鍵盤";
            this.ShowIcon = false;
            this.Text = "數字鍵盤";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.數字鍵盤_FormClosing);
            this.Load += new System.EventHandler(this.數字鍵盤_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.數字鍵盤_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.數字鍵盤_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.數字鍵盤_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExButton exButton_7;
        private System.Windows.Forms.TextBox textBox_Value;
        private System.Windows.Forms.Label label1;
        private ExButton exButton_8;
        private ExButton exButton_9;
        private ExButton exButton_Backspace;
        private ExButton exButton_CE;
        private ExButton exButton_6;
        private ExButton exButton_5;
        private ExButton exButton_4;
        private ExButton exButton_3;
        private ExButton exButton_2;
        private ExButton exButton_1;
        private ExButton exButton_Enter;
        private ExButton exButton_Dot;
        private ExButton exButton_0;
        private ExButton exButton_正負;
        private System.Windows.Forms.Timer timer;
    }
}