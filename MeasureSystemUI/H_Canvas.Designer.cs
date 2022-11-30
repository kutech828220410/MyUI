namespace MeasureSystemUI
{
    partial class H_Canvas
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_Canvas));
            this.panel_Control = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.plC_Button_複製影像 = new MyUI.PLC_Button();
            this.plC_Button_繪製影像 = new MyUI.PLC_Button();
            this.plC_Button_清除畫布 = new MyUI.PLC_Button();
            this.plC_Button_更新畫布 = new MyUI.PLC_Button();
            this.plC_Button_儲存圖片 = new MyUI.PLC_Button();
            this.plC_Button_讀取圖片 = new MyUI.PLC_Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.plC_CheckBox_鎖定長寬比 = new MyUI.PLC_CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.plC_NumBox_ZoomY = new MyUI.PLC_NumBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.plC_NumBox_ZoomX = new MyUI.PLC_NumBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_Canvas = new System.Windows.Forms.Panel();
            this.openFileDialog_Image = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog_Image = new System.Windows.Forms.SaveFileDialog();
            this.timer_init = new System.Windows.Forms.Timer(this.components);
            this.panel_Control.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Control
            // 
            this.panel_Control.Controls.Add(this.tableLayoutPanel1);
            this.panel_Control.Controls.Add(this.groupBox1);
            this.panel_Control.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Control.Location = new System.Drawing.Point(0, 362);
            this.panel_Control.Name = "panel_Control";
            this.panel_Control.Size = new System.Drawing.Size(482, 68);
            this.panel_Control.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.plC_Button_複製影像, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.plC_Button_繪製影像, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.plC_Button_清除畫布, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.plC_Button_更新畫布, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.plC_Button_儲存圖片, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.plC_Button_讀取圖片, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(198, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 68);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // plC_Button_複製影像
            // 
            this.plC_Button_複製影像.Bool = false;
            this.plC_Button_複製影像.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_Button_複製影像.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_複製影像.Location = new System.Drawing.Point(1, 1);
            this.plC_Button_複製影像.Margin = new System.Windows.Forms.Padding(1);
            this.plC_Button_複製影像.Name = "plC_Button_複製影像";
            this.plC_Button_複製影像.OFF_文字內容 = "複製影像";
            this.plC_Button_複製影像.OFF_文字字體 = new System.Drawing.Font("新細明體", 9F);
            this.plC_Button_複製影像.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_複製影像.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_複製影像.ON_文字內容 = "複製影像";
            this.plC_Button_複製影像.ON_文字字體 = new System.Drawing.Font("新細明體", 9F);
            this.plC_Button_複製影像.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_複製影像.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_複製影像.Size = new System.Drawing.Size(69, 32);
            this.plC_Button_複製影像.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_複製影像.TabIndex = 2;
            this.plC_Button_複製影像.字型鎖住 = false;
            this.plC_Button_複製影像.按鈕型態 = MyUI.PLC_Button.StatusEnum.交替型;
            this.plC_Button_複製影像.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_複製影像.文字鎖住 = false;
            this.plC_Button_複製影像.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_複製影像.狀態OFF圖片")));
            this.plC_Button_複製影像.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_複製影像.狀態ON圖片")));
            this.plC_Button_複製影像.讀取位元反向 = false;
            this.plC_Button_複製影像.讀寫鎖住 = false;
            this.plC_Button_複製影像.音效 = true;
            this.plC_Button_複製影像.顯示 = false;
            this.plC_Button_複製影像.顯示狀態 = false;
            // 
            // plC_Button_繪製影像
            // 
            this.plC_Button_繪製影像.Bool = false;
            this.plC_Button_繪製影像.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_Button_繪製影像.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_繪製影像.Location = new System.Drawing.Point(72, 1);
            this.plC_Button_繪製影像.Margin = new System.Windows.Forms.Padding(1);
            this.plC_Button_繪製影像.Name = "plC_Button_繪製影像";
            this.plC_Button_繪製影像.OFF_文字內容 = "繪製影像";
            this.plC_Button_繪製影像.OFF_文字字體 = new System.Drawing.Font("新細明體", 9F);
            this.plC_Button_繪製影像.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_繪製影像.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_繪製影像.ON_文字內容 = "繪製影像";
            this.plC_Button_繪製影像.ON_文字字體 = new System.Drawing.Font("新細明體", 9F);
            this.plC_Button_繪製影像.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_繪製影像.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_繪製影像.Size = new System.Drawing.Size(69, 32);
            this.plC_Button_繪製影像.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_繪製影像.TabIndex = 1;
            this.plC_Button_繪製影像.字型鎖住 = false;
            this.plC_Button_繪製影像.按鈕型態 = MyUI.PLC_Button.StatusEnum.交替型;
            this.plC_Button_繪製影像.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_繪製影像.文字鎖住 = false;
            this.plC_Button_繪製影像.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_繪製影像.狀態OFF圖片")));
            this.plC_Button_繪製影像.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_繪製影像.狀態ON圖片")));
            this.plC_Button_繪製影像.讀取位元反向 = false;
            this.plC_Button_繪製影像.讀寫鎖住 = false;
            this.plC_Button_繪製影像.音效 = true;
            this.plC_Button_繪製影像.顯示 = false;
            this.plC_Button_繪製影像.顯示狀態 = false;
            // 
            // plC_Button_清除畫布
            // 
            this.plC_Button_清除畫布.Bool = false;
            this.plC_Button_清除畫布.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_Button_清除畫布.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_清除畫布.Location = new System.Drawing.Point(143, 1);
            this.plC_Button_清除畫布.Margin = new System.Windows.Forms.Padding(1);
            this.plC_Button_清除畫布.Name = "plC_Button_清除畫布";
            this.plC_Button_清除畫布.OFF_文字內容 = "清除畫布";
            this.plC_Button_清除畫布.OFF_文字字體 = new System.Drawing.Font("新細明體", 9F);
            this.plC_Button_清除畫布.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_清除畫布.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_清除畫布.ON_文字內容 = "清除畫布";
            this.plC_Button_清除畫布.ON_文字字體 = new System.Drawing.Font("新細明體", 9F);
            this.plC_Button_清除畫布.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_清除畫布.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_清除畫布.Size = new System.Drawing.Size(69, 32);
            this.plC_Button_清除畫布.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_清除畫布.TabIndex = 7;
            this.plC_Button_清除畫布.字型鎖住 = false;
            this.plC_Button_清除畫布.按鈕型態 = MyUI.PLC_Button.StatusEnum.交替型;
            this.plC_Button_清除畫布.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_清除畫布.文字鎖住 = false;
            this.plC_Button_清除畫布.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_清除畫布.狀態OFF圖片")));
            this.plC_Button_清除畫布.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_清除畫布.狀態ON圖片")));
            this.plC_Button_清除畫布.讀取位元反向 = false;
            this.plC_Button_清除畫布.讀寫鎖住 = false;
            this.plC_Button_清除畫布.音效 = true;
            this.plC_Button_清除畫布.顯示 = false;
            this.plC_Button_清除畫布.顯示狀態 = false;
            // 
            // plC_Button_更新畫布
            // 
            this.plC_Button_更新畫布.Bool = false;
            this.plC_Button_更新畫布.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_Button_更新畫布.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_更新畫布.Location = new System.Drawing.Point(214, 1);
            this.plC_Button_更新畫布.Margin = new System.Windows.Forms.Padding(1);
            this.plC_Button_更新畫布.Name = "plC_Button_更新畫布";
            this.plC_Button_更新畫布.OFF_文字內容 = "更新畫布";
            this.plC_Button_更新畫布.OFF_文字字體 = new System.Drawing.Font("新細明體", 9F);
            this.plC_Button_更新畫布.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_更新畫布.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_更新畫布.ON_文字內容 = "更新畫布";
            this.plC_Button_更新畫布.ON_文字字體 = new System.Drawing.Font("新細明體", 9F);
            this.plC_Button_更新畫布.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_更新畫布.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_更新畫布.Size = new System.Drawing.Size(69, 32);
            this.plC_Button_更新畫布.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_更新畫布.TabIndex = 0;
            this.plC_Button_更新畫布.字型鎖住 = false;
            this.plC_Button_更新畫布.按鈕型態 = MyUI.PLC_Button.StatusEnum.交替型;
            this.plC_Button_更新畫布.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_更新畫布.文字鎖住 = false;
            this.plC_Button_更新畫布.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_更新畫布.狀態OFF圖片")));
            this.plC_Button_更新畫布.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_更新畫布.狀態ON圖片")));
            this.plC_Button_更新畫布.讀取位元反向 = false;
            this.plC_Button_更新畫布.讀寫鎖住 = false;
            this.plC_Button_更新畫布.音效 = true;
            this.plC_Button_更新畫布.顯示 = false;
            this.plC_Button_更新畫布.顯示狀態 = false;
            // 
            // plC_Button_儲存圖片
            // 
            this.plC_Button_儲存圖片.Bool = false;
            this.plC_Button_儲存圖片.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_Button_儲存圖片.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_儲存圖片.Location = new System.Drawing.Point(143, 35);
            this.plC_Button_儲存圖片.Margin = new System.Windows.Forms.Padding(1);
            this.plC_Button_儲存圖片.Name = "plC_Button_儲存圖片";
            this.plC_Button_儲存圖片.OFF_文字內容 = "儲存圖片";
            this.plC_Button_儲存圖片.OFF_文字字體 = new System.Drawing.Font("新細明體", 9F);
            this.plC_Button_儲存圖片.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_儲存圖片.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_儲存圖片.ON_文字內容 = "儲存圖片";
            this.plC_Button_儲存圖片.ON_文字字體 = new System.Drawing.Font("新細明體", 9F);
            this.plC_Button_儲存圖片.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_儲存圖片.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_儲存圖片.Size = new System.Drawing.Size(69, 32);
            this.plC_Button_儲存圖片.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_儲存圖片.TabIndex = 5;
            this.plC_Button_儲存圖片.字型鎖住 = false;
            this.plC_Button_儲存圖片.按鈕型態 = MyUI.PLC_Button.StatusEnum.保持型;
            this.plC_Button_儲存圖片.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_儲存圖片.文字鎖住 = false;
            this.plC_Button_儲存圖片.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_儲存圖片.狀態OFF圖片")));
            this.plC_Button_儲存圖片.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_儲存圖片.狀態ON圖片")));
            this.plC_Button_儲存圖片.讀取位元反向 = false;
            this.plC_Button_儲存圖片.讀寫鎖住 = false;
            this.plC_Button_儲存圖片.音效 = true;
            this.plC_Button_儲存圖片.顯示 = false;
            this.plC_Button_儲存圖片.顯示狀態 = false;
            this.plC_Button_儲存圖片.btnClick += new System.EventHandler(this.plC_Button_儲存圖片_btnClick);
            // 
            // plC_Button_讀取圖片
            // 
            this.plC_Button_讀取圖片.Bool = false;
            this.plC_Button_讀取圖片.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_Button_讀取圖片.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_讀取圖片.Location = new System.Drawing.Point(214, 35);
            this.plC_Button_讀取圖片.Margin = new System.Windows.Forms.Padding(1);
            this.plC_Button_讀取圖片.Name = "plC_Button_讀取圖片";
            this.plC_Button_讀取圖片.OFF_文字內容 = "讀取圖片";
            this.plC_Button_讀取圖片.OFF_文字字體 = new System.Drawing.Font("新細明體", 9F);
            this.plC_Button_讀取圖片.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_讀取圖片.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_讀取圖片.ON_文字內容 = "讀取圖片";
            this.plC_Button_讀取圖片.ON_文字字體 = new System.Drawing.Font("新細明體", 9F);
            this.plC_Button_讀取圖片.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_讀取圖片.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_讀取圖片.Size = new System.Drawing.Size(69, 32);
            this.plC_Button_讀取圖片.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_讀取圖片.TabIndex = 6;
            this.plC_Button_讀取圖片.字型鎖住 = false;
            this.plC_Button_讀取圖片.按鈕型態 = MyUI.PLC_Button.StatusEnum.保持型;
            this.plC_Button_讀取圖片.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_讀取圖片.文字鎖住 = false;
            this.plC_Button_讀取圖片.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_讀取圖片.狀態OFF圖片")));
            this.plC_Button_讀取圖片.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_讀取圖片.狀態ON圖片")));
            this.plC_Button_讀取圖片.讀取位元反向 = false;
            this.plC_Button_讀取圖片.讀寫鎖住 = false;
            this.plC_Button_讀取圖片.音效 = true;
            this.plC_Button_讀取圖片.顯示 = false;
            this.plC_Button_讀取圖片.顯示狀態 = false;
            this.plC_Button_讀取圖片.btnClick += new System.EventHandler(this.plC_Button_讀取圖片_btnClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.plC_CheckBox_鎖定長寬比);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.plC_NumBox_ZoomY);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.plC_NumBox_ZoomX);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(191, 68);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zoom";
            // 
            // plC_CheckBox_鎖定長寬比
            // 
            this.plC_CheckBox_鎖定長寬比.AutoSize = true;
            this.plC_CheckBox_鎖定長寬比.Bool = false;
            this.plC_CheckBox_鎖定長寬比.Font = new System.Drawing.Font("新細明體", 9F);
            this.plC_CheckBox_鎖定長寬比.ForeColor = System.Drawing.Color.Black;
            this.plC_CheckBox_鎖定長寬比.Location = new System.Drawing.Point(140, 43);
            this.plC_CheckBox_鎖定長寬比.Name = "plC_CheckBox_鎖定長寬比";
            this.plC_CheckBox_鎖定長寬比.Size = new System.Drawing.Size(48, 16);
            this.plC_CheckBox_鎖定長寬比.TabIndex = 6;
            this.plC_CheckBox_鎖定長寬比.Text = "Lock";
            this.plC_CheckBox_鎖定長寬比.UseVisualStyleBackColor = true;
            this.plC_CheckBox_鎖定長寬比.文字內容 = "Lock";
            this.plC_CheckBox_鎖定長寬比.文字字體 = new System.Drawing.Font("新細明體", 9F);
            this.plC_CheckBox_鎖定長寬比.文字顏色 = System.Drawing.Color.Black;
            this.plC_CheckBox_鎖定長寬比.讀寫鎖住 = false;
            this.plC_CheckBox_鎖定長寬比.音效 = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(116, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "(%)";
            // 
            // plC_NumBox_ZoomY
            // 
            this.plC_NumBox_ZoomY.Location = new System.Drawing.Point(43, 40);
            this.plC_NumBox_ZoomY.Name = "plC_NumBox_ZoomY";
            this.plC_NumBox_ZoomY.ReadOnly = false;
            this.plC_NumBox_ZoomY.Size = new System.Drawing.Size(67, 22);
            this.plC_NumBox_ZoomY.TabIndex = 4;
            this.plC_NumBox_ZoomY.Value = 0;
            this.plC_NumBox_ZoomY.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_ZoomY.密碼欄位 = false;
            this.plC_NumBox_ZoomY.小數點位置 = 0;
            this.plC_NumBox_ZoomY.微調數值 = 1;
            this.plC_NumBox_ZoomY.音效 = true;
            this.plC_NumBox_ZoomY.顯示微調按鈕 = false;
            this.plC_NumBox_ZoomY.顯示螢幕小鍵盤 = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "Y:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "(%)";
            // 
            // plC_NumBox_ZoomX
            // 
            this.plC_NumBox_ZoomX.Location = new System.Drawing.Point(43, 15);
            this.plC_NumBox_ZoomX.Name = "plC_NumBox_ZoomX";
            this.plC_NumBox_ZoomX.ReadOnly = false;
            this.plC_NumBox_ZoomX.Size = new System.Drawing.Size(67, 22);
            this.plC_NumBox_ZoomX.TabIndex = 1;
            this.plC_NumBox_ZoomX.Value = 0;
            this.plC_NumBox_ZoomX.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_ZoomX.密碼欄位 = false;
            this.plC_NumBox_ZoomX.小數點位置 = 0;
            this.plC_NumBox_ZoomX.微調數值 = 1;
            this.plC_NumBox_ZoomX.音效 = true;
            this.plC_NumBox_ZoomX.顯示微調按鈕 = false;
            this.plC_NumBox_ZoomX.顯示螢幕小鍵盤 = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // panel_Canvas
            // 
            this.panel_Canvas.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel_Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Canvas.Location = new System.Drawing.Point(0, 0);
            this.panel_Canvas.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Canvas.Name = "panel_Canvas";
            this.panel_Canvas.Size = new System.Drawing.Size(482, 362);
            this.panel_Canvas.TabIndex = 2;
            // 
            // openFileDialog_Image
            // 
            this.openFileDialog_Image.DefaultExt = "bmp";
            this.openFileDialog_Image.FileName = "*.bmp";
            this.openFileDialog_Image.Filter = "Image File (*bmp)|*bmp;";
            // 
            // saveFileDialog_Image
            // 
            this.saveFileDialog_Image.DefaultExt = "bmp";
            this.saveFileDialog_Image.FileName = "*.bmp";
            this.saveFileDialog_Image.Filter = "Image File (*bmp)|*bmp;";
            // 
            // timer_init
            // 
            this.timer_init.Enabled = true;
            this.timer_init.Interval = 10;
            this.timer_init.Tick += new System.EventHandler(this.timer_init_Tick);
            // 
            // H_Canvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel_Canvas);
            this.Controls.Add(this.panel_Control);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "H_Canvas";
            this.Size = new System.Drawing.Size(482, 430);
            this.Load += new System.EventHandler(this.H_Canvas_Load);
            this.panel_Control.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Control;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MyUI.PLC_Button plC_Button_清除畫布;
        private MyUI.PLC_Button plC_Button_讀取圖片;
        private MyUI.PLC_Button plC_Button_儲存圖片;
        private MyUI.PLC_Button plC_Button_更新畫布;
        private MyUI.PLC_Button plC_Button_繪製影像;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private MyUI.PLC_NumBox plC_NumBox_ZoomY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private MyUI.PLC_NumBox plC_NumBox_ZoomX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_Canvas;
        private System.Windows.Forms.OpenFileDialog openFileDialog_Image;
        private MyUI.PLC_Button plC_Button_複製影像;
        private MyUI.PLC_CheckBox plC_CheckBox_鎖定長寬比;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_Image;
        private System.Windows.Forms.Timer timer_init;
    }
}
