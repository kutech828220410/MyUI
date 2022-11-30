namespace HCGUI
{
    partial class HCG_485_IO
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HCG_485_IO));
            this.panel_Open = new System.Windows.Forms.Panel();
            this.plC_Button_Open = new MyUI.PLC_Button();
            this.panel67 = new System.Windows.Forms.Panel();
            this.label_CycleTime = new System.Windows.Forms.Label();
            this.label_單位 = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.numWordTextBox_StreamName = new MyUI.NumWordTextBox();
            this.comboBox_Baudrate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_COM = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.panel_TAB = new System.Windows.Forms.Panel();
            this.panel_Open.SuspendLayout();
            this.panel67.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Open
            // 
            this.panel_Open.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_Open.Controls.Add(this.plC_Button_Open);
            this.panel_Open.Controls.Add(this.panel67);
            this.panel_Open.Controls.Add(this.groupBox14);
            this.panel_Open.Controls.Add(this.comboBox_Baudrate);
            this.panel_Open.Controls.Add(this.label1);
            this.panel_Open.Controls.Add(this.textBox_COM);
            this.panel_Open.Controls.Add(this.label26);
            this.panel_Open.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Open.Location = new System.Drawing.Point(0, 0);
            this.panel_Open.Name = "panel_Open";
            this.panel_Open.Size = new System.Drawing.Size(691, 64);
            this.panel_Open.TabIndex = 6;
            // 
            // plC_Button_Open
            // 
            this.plC_Button_Open.Bool = false;
            this.plC_Button_Open.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_Open.Location = new System.Drawing.Point(499, 9);
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
            this.plC_Button_Open.TabIndex = 45;
            this.plC_Button_Open.字型鎖住 = false;
            this.plC_Button_Open.按鈕型態 = MyUI.PLC_Button.StatusEnum.保持型;
            this.plC_Button_Open.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_Open.文字鎖住 = false;
            this.plC_Button_Open.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_Open.狀態OFF圖片")));
            this.plC_Button_Open.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_Open.狀態ON圖片")));
            this.plC_Button_Open.讀取位元反向 = false;
            this.plC_Button_Open.讀寫鎖住 = false;
            this.plC_Button_Open.音效 = true;
            this.plC_Button_Open.顯示 = false;
            this.plC_Button_Open.顯示狀態 = false;
            this.plC_Button_Open.btnClick += new System.EventHandler(this.plC_Button_Open_btnClick);
            // 
            // panel67
            // 
            this.panel67.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel67.Controls.Add(this.label_CycleTime);
            this.panel67.Controls.Add(this.label_單位);
            this.panel67.Location = new System.Drawing.Point(266, 13);
            this.panel67.Name = "panel67";
            this.panel67.Size = new System.Drawing.Size(104, 31);
            this.panel67.TabIndex = 44;
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
            this.groupBox14.Location = new System.Drawing.Point(376, 3);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(116, 50);
            this.groupBox14.TabIndex = 43;
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
            this.numWordTextBox_StreamName.Text = "HCG_485_IO-001";
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
            this.comboBox_Baudrate.Location = new System.Drawing.Point(179, 19);
            this.comboBox_Baudrate.Name = "comboBox_Baudrate";
            this.comboBox_Baudrate.Size = new System.Drawing.Size(81, 20);
            this.comboBox_Baudrate.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 9F);
            this.label1.Location = new System.Drawing.Point(123, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 12);
            this.label1.TabIndex = 41;
            this.label1.Text = "Baudrate:";
            // 
            // textBox_COM
            // 
            this.textBox_COM.Location = new System.Drawing.Point(45, 19);
            this.textBox_COM.Name = "textBox_COM";
            this.textBox_COM.Size = new System.Drawing.Size(69, 22);
            this.textBox_COM.TabIndex = 40;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("新細明體", 9F);
            this.label26.Location = new System.Drawing.Point(5, 25);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(34, 12);
            this.label26.TabIndex = 39;
            this.label26.Text = "COM:";
            // 
            // serialPort
            // 
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // panel_TAB
            // 
            this.panel_TAB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_TAB.Location = new System.Drawing.Point(0, 64);
            this.panel_TAB.Name = "panel_TAB";
            this.panel_TAB.Size = new System.Drawing.Size(691, 325);
            this.panel_TAB.TabIndex = 7;
            // 
            // HCG_485_IO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel_TAB);
            this.Controls.Add(this.panel_Open);
            this.Name = "HCG_485_IO";
            this.Size = new System.Drawing.Size(691, 389);
            this.panel_Open.ResumeLayout(false);
            this.panel_Open.PerformLayout();
            this.panel67.ResumeLayout(false);
            this.panel67.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Open;
        private System.Windows.Forms.ComboBox comboBox_Baudrate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_COM;
        private System.Windows.Forms.Label label26;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Panel panel67;
        private System.Windows.Forms.Label label_CycleTime;
        private System.Windows.Forms.Label label_單位;
        private System.Windows.Forms.GroupBox groupBox14;
        private MyUI.NumWordTextBox numWordTextBox_StreamName;
        private System.Windows.Forms.Panel panel_TAB;
        private MyUI.PLC_Button plC_Button_Open;
    }
}
