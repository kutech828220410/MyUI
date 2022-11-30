namespace LightControlUI
{
    partial class LD_NP24DV_4T
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LD_NP24DV_4T));
            this.panel1 = new System.Windows.Forms.Panel();
            this.plC_Button_Open = new MyUI.PLC_Button();
            this.comboBox_Baudrate = new System.Windows.Forms.ComboBox();
            this.comboBox_StopBits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_Parity = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_DataBits = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_COM = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.plC_Button_CH01_更新完成 = new MyUI.PLC_Button();
            this.plC_TrackBarHorizontal_CH01_Lightness = new MyUI.PLC_TrackBarHorizontal();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.plC_Button_CH02_更新完成 = new MyUI.PLC_Button();
            this.plC_TrackBarHorizontal_CH02_Lightness = new MyUI.PLC_TrackBarHorizontal();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.plC_Button_CH03_更新完成 = new MyUI.PLC_Button();
            this.plC_TrackBarHorizontal_CH03_Lightness = new MyUI.PLC_TrackBarHorizontal();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.plC_Button_CH04_更新完成 = new MyUI.PLC_Button();
            this.plC_TrackBarHorizontal_CH04_Lightness = new MyUI.PLC_TrackBarHorizontal();
            this.label_CycleTime = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.plC_Button_Open);
            this.panel1.Controls.Add(this.comboBox_Baudrate);
            this.panel1.Controls.Add(this.comboBox_StopBits);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.comboBox_Parity);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboBox_DataBits);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox_COM);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 76);
            this.panel1.TabIndex = 0;
            // 
            // plC_Button_Open
            // 
            this.plC_Button_Open.Bool = false;
            this.plC_Button_Open.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_Open.Location = new System.Drawing.Point(326, 4);
            this.plC_Button_Open.Name = "plC_Button_Open";
            this.plC_Button_Open.OFF_文字內容 = "Open";
            this.plC_Button_Open.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_Open.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_Open.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_Open.ON_文字內容 = "Open";
            this.plC_Button_Open.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_Open.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_Open.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_Open.Size = new System.Drawing.Size(100, 36);
            this.plC_Button_Open.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_Open.TabIndex = 36;
            this.plC_Button_Open.字型鎖住 = false;
            this.plC_Button_Open.按鈕型態 = MyUI.PLC_Button.StatusEnum.交替型;
            this.plC_Button_Open.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_Open.文字鎖住 = false;
            this.plC_Button_Open.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_Open.狀態OFF圖片")));
            this.plC_Button_Open.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_Open.狀態ON圖片")));
            this.plC_Button_Open.讀取位元反向 = false;
            this.plC_Button_Open.讀寫鎖住 = false;
            this.plC_Button_Open.音效 = true;
            this.plC_Button_Open.顯示 = false;
            this.plC_Button_Open.顯示狀態 = false;
            // 
            // comboBox_Baudrate
            // 
            this.comboBox_Baudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Baudrate.FormattingEnabled = true;
            this.comboBox_Baudrate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboBox_Baudrate.Location = new System.Drawing.Point(229, 11);
            this.comboBox_Baudrate.Name = "comboBox_Baudrate";
            this.comboBox_Baudrate.Size = new System.Drawing.Size(81, 20);
            this.comboBox_Baudrate.TabIndex = 35;
            this.comboBox_Baudrate.SelectedIndexChanged += new System.EventHandler(this.comboBox_Baudrate_SelectedIndexChanged);
            // 
            // comboBox_StopBits
            // 
            this.comboBox_StopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_StopBits.FormattingEnabled = true;
            this.comboBox_StopBits.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.comboBox_StopBits.Location = new System.Drawing.Point(365, 45);
            this.comboBox_StopBits.Name = "comboBox_StopBits";
            this.comboBox_StopBits.Size = new System.Drawing.Size(61, 20);
            this.comboBox_StopBits.TabIndex = 33;
            this.comboBox_StopBits.SelectedIndexChanged += new System.EventHandler(this.comboBox_StopBits_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F);
            this.label4.Location = new System.Drawing.Point(295, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 34;
            this.label4.Text = "StopBits:";
            // 
            // comboBox_Parity
            // 
            this.comboBox_Parity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Parity.FormattingEnabled = true;
            this.comboBox_Parity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd"});
            this.comboBox_Parity.Location = new System.Drawing.Point(218, 45);
            this.comboBox_Parity.Name = "comboBox_Parity";
            this.comboBox_Parity.Size = new System.Drawing.Size(61, 20);
            this.comboBox_Parity.TabIndex = 31;
            this.comboBox_Parity.SelectedIndexChanged += new System.EventHandler(this.comboBox_Parity_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F);
            this.label2.Location = new System.Drawing.Point(164, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 32;
            this.label2.Text = "Parity:";
            // 
            // comboBox_DataBits
            // 
            this.comboBox_DataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DataBits.FormattingEnabled = true;
            this.comboBox_DataBits.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "5"});
            this.comboBox_DataBits.Location = new System.Drawing.Point(86, 45);
            this.comboBox_DataBits.Name = "comboBox_DataBits";
            this.comboBox_DataBits.Size = new System.Drawing.Size(61, 20);
            this.comboBox_DataBits.TabIndex = 29;
            this.comboBox_DataBits.SelectedIndexChanged += new System.EventHandler(this.comboBox_DataBits_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(15, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 16);
            this.label1.TabIndex = 30;
            this.label1.Text = "DataBits:";
            // 
            // comboBox_COM
            // 
            this.comboBox_COM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COM.FormattingEnabled = true;
            this.comboBox_COM.Location = new System.Drawing.Point(68, 11);
            this.comboBox_COM.Name = "comboBox_COM";
            this.comboBox_COM.Size = new System.Drawing.Size(81, 20);
            this.comboBox_COM.TabIndex = 26;
            this.comboBox_COM.SelectedIndexChanged += new System.EventHandler(this.comboBox_COM_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新細明體", 12F);
            this.label5.Location = new System.Drawing.Point(155, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 16);
            this.label5.TabIndex = 28;
            this.label5.Text = "Baudrate:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F);
            this.label3.Location = new System.Drawing.Point(15, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "COM:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.plC_Button_CH01_更新完成);
            this.groupBox1.Controls.Add(this.plC_TrackBarHorizontal_CH01_Lightness);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 73);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CH01";
            // 
            // plC_Button_CH01_更新完成
            // 
            this.plC_Button_CH01_更新完成.Bool = false;
            this.plC_Button_CH01_更新完成.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_CH01_更新完成.Location = new System.Drawing.Point(333, 15);
            this.plC_Button_CH01_更新完成.Name = "plC_Button_CH01_更新完成";
            this.plC_Button_CH01_更新完成.OFF_文字內容 = "更新完成";
            this.plC_Button_CH01_更新完成.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_CH01_更新完成.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_CH01_更新完成.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_CH01_更新完成.ON_文字內容 = "更新完成";
            this.plC_Button_CH01_更新完成.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_CH01_更新完成.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_CH01_更新完成.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_CH01_更新完成.Size = new System.Drawing.Size(93, 52);
            this.plC_Button_CH01_更新完成.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_CH01_更新完成.TabIndex = 1;
            this.plC_Button_CH01_更新完成.字型鎖住 = false;
            this.plC_Button_CH01_更新完成.按鈕型態 = MyUI.PLC_Button.StatusEnum.保持型;
            this.plC_Button_CH01_更新完成.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_CH01_更新完成.文字鎖住 = false;
            this.plC_Button_CH01_更新完成.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_CH01_更新完成.狀態OFF圖片")));
            this.plC_Button_CH01_更新完成.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_CH01_更新完成.狀態ON圖片")));
            this.plC_Button_CH01_更新完成.讀取位元反向 = false;
            this.plC_Button_CH01_更新完成.讀寫鎖住 = false;
            this.plC_Button_CH01_更新完成.音效 = true;
            this.plC_Button_CH01_更新完成.顯示 = false;
            this.plC_Button_CH01_更新完成.顯示狀態 = false;
            // 
            // plC_TrackBarHorizontal_CH01_Lightness
            // 
            this.plC_TrackBarHorizontal_CH01_Lightness.Location = new System.Drawing.Point(6, 16);
            this.plC_TrackBarHorizontal_CH01_Lightness.Name = "plC_TrackBarHorizontal_CH01_Lightness";
            this.plC_TrackBarHorizontal_CH01_Lightness.Size = new System.Drawing.Size(321, 51);
            this.plC_TrackBarHorizontal_CH01_Lightness.TabIndex = 0;
            this.plC_TrackBarHorizontal_CH01_Lightness.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.plC_TrackBarHorizontal_CH01_Lightness.刻度最大值 = 255;
            this.plC_TrackBarHorizontal_CH01_Lightness.刻度最小值 = 0;
            this.plC_TrackBarHorizontal_CH01_Lightness.刻度間隔 = 20;
            this.plC_TrackBarHorizontal_CH01_Lightness.小數點位置 = 0;
            this.plC_TrackBarHorizontal_CH01_Lightness.數值字體 = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_CH01_Lightness.標題內容 = "Lightness";
            this.plC_TrackBarHorizontal_CH01_Lightness.標題字體 = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_CH01_Lightness.顯示數值 = true;
            this.plC_TrackBarHorizontal_CH01_Lightness.顯示標題 = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.plC_Button_CH02_更新完成);
            this.groupBox2.Controls.Add(this.plC_TrackBarHorizontal_CH02_Lightness);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(440, 73);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CH02";
            // 
            // plC_Button_CH02_更新完成
            // 
            this.plC_Button_CH02_更新完成.Bool = false;
            this.plC_Button_CH02_更新完成.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_CH02_更新完成.Location = new System.Drawing.Point(333, 15);
            this.plC_Button_CH02_更新完成.Name = "plC_Button_CH02_更新完成";
            this.plC_Button_CH02_更新完成.OFF_文字內容 = "更新完成";
            this.plC_Button_CH02_更新完成.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_CH02_更新完成.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_CH02_更新完成.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_CH02_更新完成.ON_文字內容 = "更新完成";
            this.plC_Button_CH02_更新完成.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_CH02_更新完成.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_CH02_更新完成.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_CH02_更新完成.Size = new System.Drawing.Size(93, 52);
            this.plC_Button_CH02_更新完成.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_CH02_更新完成.TabIndex = 1;
            this.plC_Button_CH02_更新完成.字型鎖住 = false;
            this.plC_Button_CH02_更新完成.按鈕型態 = MyUI.PLC_Button.StatusEnum.保持型;
            this.plC_Button_CH02_更新完成.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_CH02_更新完成.文字鎖住 = false;
            this.plC_Button_CH02_更新完成.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_CH02_更新完成.狀態OFF圖片")));
            this.plC_Button_CH02_更新完成.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_CH02_更新完成.狀態ON圖片")));
            this.plC_Button_CH02_更新完成.讀取位元反向 = false;
            this.plC_Button_CH02_更新完成.讀寫鎖住 = false;
            this.plC_Button_CH02_更新完成.音效 = true;
            this.plC_Button_CH02_更新完成.顯示 = false;
            this.plC_Button_CH02_更新完成.顯示狀態 = false;
            // 
            // plC_TrackBarHorizontal_CH02_Lightness
            // 
            this.plC_TrackBarHorizontal_CH02_Lightness.Location = new System.Drawing.Point(6, 16);
            this.plC_TrackBarHorizontal_CH02_Lightness.Name = "plC_TrackBarHorizontal_CH02_Lightness";
            this.plC_TrackBarHorizontal_CH02_Lightness.Size = new System.Drawing.Size(321, 51);
            this.plC_TrackBarHorizontal_CH02_Lightness.TabIndex = 0;
            this.plC_TrackBarHorizontal_CH02_Lightness.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.plC_TrackBarHorizontal_CH02_Lightness.刻度最大值 = 255;
            this.plC_TrackBarHorizontal_CH02_Lightness.刻度最小值 = 0;
            this.plC_TrackBarHorizontal_CH02_Lightness.刻度間隔 = 20;
            this.plC_TrackBarHorizontal_CH02_Lightness.小數點位置 = 0;
            this.plC_TrackBarHorizontal_CH02_Lightness.數值字體 = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_CH02_Lightness.標題內容 = "Lightness";
            this.plC_TrackBarHorizontal_CH02_Lightness.標題字體 = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_CH02_Lightness.顯示數值 = true;
            this.plC_TrackBarHorizontal_CH02_Lightness.顯示標題 = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.plC_Button_CH03_更新完成);
            this.groupBox3.Controls.Add(this.plC_TrackBarHorizontal_CH03_Lightness);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 222);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(440, 73);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CH03";
            // 
            // plC_Button_CH03_更新完成
            // 
            this.plC_Button_CH03_更新完成.Bool = false;
            this.plC_Button_CH03_更新完成.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_CH03_更新完成.Location = new System.Drawing.Point(333, 15);
            this.plC_Button_CH03_更新完成.Name = "plC_Button_CH03_更新完成";
            this.plC_Button_CH03_更新完成.OFF_文字內容 = "更新完成";
            this.plC_Button_CH03_更新完成.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_CH03_更新完成.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_CH03_更新完成.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_CH03_更新完成.ON_文字內容 = "更新完成";
            this.plC_Button_CH03_更新完成.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_CH03_更新完成.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_CH03_更新完成.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_CH03_更新完成.Size = new System.Drawing.Size(93, 52);
            this.plC_Button_CH03_更新完成.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_CH03_更新完成.TabIndex = 1;
            this.plC_Button_CH03_更新完成.字型鎖住 = false;
            this.plC_Button_CH03_更新完成.按鈕型態 = MyUI.PLC_Button.StatusEnum.保持型;
            this.plC_Button_CH03_更新完成.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_CH03_更新完成.文字鎖住 = false;
            this.plC_Button_CH03_更新完成.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_CH03_更新完成.狀態OFF圖片")));
            this.plC_Button_CH03_更新完成.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_CH03_更新完成.狀態ON圖片")));
            this.plC_Button_CH03_更新完成.讀取位元反向 = false;
            this.plC_Button_CH03_更新完成.讀寫鎖住 = false;
            this.plC_Button_CH03_更新完成.音效 = true;
            this.plC_Button_CH03_更新完成.顯示 = false;
            this.plC_Button_CH03_更新完成.顯示狀態 = false;
            // 
            // plC_TrackBarHorizontal_CH03_Lightness
            // 
            this.plC_TrackBarHorizontal_CH03_Lightness.Location = new System.Drawing.Point(6, 16);
            this.plC_TrackBarHorizontal_CH03_Lightness.Name = "plC_TrackBarHorizontal_CH03_Lightness";
            this.plC_TrackBarHorizontal_CH03_Lightness.Size = new System.Drawing.Size(321, 51);
            this.plC_TrackBarHorizontal_CH03_Lightness.TabIndex = 0;
            this.plC_TrackBarHorizontal_CH03_Lightness.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.plC_TrackBarHorizontal_CH03_Lightness.刻度最大值 = 255;
            this.plC_TrackBarHorizontal_CH03_Lightness.刻度最小值 = 0;
            this.plC_TrackBarHorizontal_CH03_Lightness.刻度間隔 = 20;
            this.plC_TrackBarHorizontal_CH03_Lightness.小數點位置 = 0;
            this.plC_TrackBarHorizontal_CH03_Lightness.數值字體 = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_CH03_Lightness.標題內容 = "Lightness";
            this.plC_TrackBarHorizontal_CH03_Lightness.標題字體 = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_CH03_Lightness.顯示數值 = true;
            this.plC_TrackBarHorizontal_CH03_Lightness.顯示標題 = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.plC_Button_CH04_更新完成);
            this.groupBox4.Controls.Add(this.plC_TrackBarHorizontal_CH04_Lightness);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 295);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(440, 73);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "CH04";
            // 
            // plC_Button_CH04_更新完成
            // 
            this.plC_Button_CH04_更新完成.Bool = false;
            this.plC_Button_CH04_更新完成.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_CH04_更新完成.Location = new System.Drawing.Point(333, 15);
            this.plC_Button_CH04_更新完成.Name = "plC_Button_CH04_更新完成";
            this.plC_Button_CH04_更新完成.OFF_文字內容 = "更新完成";
            this.plC_Button_CH04_更新完成.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_CH04_更新完成.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_CH04_更新完成.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_CH04_更新完成.ON_文字內容 = "更新完成";
            this.plC_Button_CH04_更新完成.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_CH04_更新完成.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_CH04_更新完成.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_CH04_更新完成.Size = new System.Drawing.Size(93, 52);
            this.plC_Button_CH04_更新完成.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_CH04_更新完成.TabIndex = 1;
            this.plC_Button_CH04_更新完成.字型鎖住 = false;
            this.plC_Button_CH04_更新完成.按鈕型態 = MyUI.PLC_Button.StatusEnum.保持型;
            this.plC_Button_CH04_更新完成.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_CH04_更新完成.文字鎖住 = false;
            this.plC_Button_CH04_更新完成.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_CH04_更新完成.狀態OFF圖片")));
            this.plC_Button_CH04_更新完成.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_CH04_更新完成.狀態ON圖片")));
            this.plC_Button_CH04_更新完成.讀取位元反向 = false;
            this.plC_Button_CH04_更新完成.讀寫鎖住 = false;
            this.plC_Button_CH04_更新完成.音效 = true;
            this.plC_Button_CH04_更新完成.顯示 = false;
            this.plC_Button_CH04_更新完成.顯示狀態 = false;
            // 
            // plC_TrackBarHorizontal_CH04_Lightness
            // 
            this.plC_TrackBarHorizontal_CH04_Lightness.Location = new System.Drawing.Point(6, 16);
            this.plC_TrackBarHorizontal_CH04_Lightness.Name = "plC_TrackBarHorizontal_CH04_Lightness";
            this.plC_TrackBarHorizontal_CH04_Lightness.Size = new System.Drawing.Size(321, 51);
            this.plC_TrackBarHorizontal_CH04_Lightness.TabIndex = 0;
            this.plC_TrackBarHorizontal_CH04_Lightness.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.plC_TrackBarHorizontal_CH04_Lightness.刻度最大值 = 255;
            this.plC_TrackBarHorizontal_CH04_Lightness.刻度最小值 = 0;
            this.plC_TrackBarHorizontal_CH04_Lightness.刻度間隔 = 20;
            this.plC_TrackBarHorizontal_CH04_Lightness.小數點位置 = 0;
            this.plC_TrackBarHorizontal_CH04_Lightness.數值字體 = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_CH04_Lightness.標題內容 = "Lightness";
            this.plC_TrackBarHorizontal_CH04_Lightness.標題字體 = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_CH04_Lightness.顯示數值 = true;
            this.plC_TrackBarHorizontal_CH04_Lightness.顯示標題 = true;
            // 
            // label_CycleTime
            // 
            this.label_CycleTime.AutoSize = true;
            this.label_CycleTime.Location = new System.Drawing.Point(393, 377);
            this.label_CycleTime.Name = "label_CycleTime";
            this.label_CycleTime.Size = new System.Drawing.Size(32, 12);
            this.label_CycleTime.TabIndex = 28;
            this.label_CycleTime.Text = "0.000";
            // 
            // LD_NP24DV_4T
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label_CycleTime);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "LD_NP24DV_4T";
            this.Size = new System.Drawing.Size(440, 397);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox_Baudrate;
        private System.Windows.Forms.ComboBox comboBox_StopBits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_Parity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_DataBits;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_COM;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private MyUI.PLC_TrackBarHorizontal plC_TrackBarHorizontal_CH01_Lightness;
        private MyUI.PLC_Button plC_Button_CH01_更新完成;
        private MyUI.PLC_Button plC_Button_Open;
        private System.Windows.Forms.GroupBox groupBox2;
        private MyUI.PLC_Button plC_Button_CH02_更新完成;
        private MyUI.PLC_TrackBarHorizontal plC_TrackBarHorizontal_CH02_Lightness;
        private System.Windows.Forms.GroupBox groupBox3;
        private MyUI.PLC_Button plC_Button_CH03_更新完成;
        private MyUI.PLC_TrackBarHorizontal plC_TrackBarHorizontal_CH03_Lightness;
        private System.Windows.Forms.GroupBox groupBox4;
        private MyUI.PLC_Button plC_Button_CH04_更新完成;
        private MyUI.PLC_TrackBarHorizontal plC_TrackBarHorizontal_CH04_Lightness;
        private System.Windows.Forms.Label label_CycleTime;
    }
}
