namespace MyUI
{
    partial class PLC_SerialPort
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PLC_SerialPort));
            this.panel1 = new System.Windows.Forms.Panel();
            this.exButton_Open = new MyUI.ExButton();
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
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.exButton_Open);
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
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(428, 82);
            this.panel1.TabIndex = 9;
            // 
            // exButton_Open
            // 
            this.exButton_Open.Location = new System.Drawing.Point(309, 5);
            this.exButton_Open.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.exButton_Open.Name = "exButton_Open";
            this.exButton_Open.OFF_文字內容 = "Open";
            this.exButton_Open.OFF_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Open.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_Open.ON_文字內容 = "Open";
            this.exButton_Open.ON_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Open.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_Open.Size = new System.Drawing.Size(108, 33);
            this.exButton_Open.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_Open.TabIndex = 18;
            this.exButton_Open.字型鎖住 = false;
            this.exButton_Open.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_Open.文字鎖住 = false;
            this.exButton_Open.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Open.狀態OFF圖片")));
            this.exButton_Open.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Open.狀態ON圖片")));
            this.exButton_Open.讀寫鎖住 = false;
            this.exButton_Open.音效 = false;
            this.exButton_Open.顯示狀態 = false;
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
            this.comboBox_Baudrate.Location = new System.Drawing.Point(220, 14);
            this.comboBox_Baudrate.Name = "comboBox_Baudrate";
            this.comboBox_Baudrate.Size = new System.Drawing.Size(81, 20);
            this.comboBox_Baudrate.TabIndex = 25;
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
            this.comboBox_StopBits.Location = new System.Drawing.Point(356, 48);
            this.comboBox_StopBits.Name = "comboBox_StopBits";
            this.comboBox_StopBits.Size = new System.Drawing.Size(61, 20);
            this.comboBox_StopBits.TabIndex = 23;
            this.comboBox_StopBits.SelectedIndexChanged += new System.EventHandler(this.comboBox_StopBits_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F);
            this.label4.Location = new System.Drawing.Point(286, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 24;
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
            this.comboBox_Parity.Location = new System.Drawing.Point(209, 48);
            this.comboBox_Parity.Name = "comboBox_Parity";
            this.comboBox_Parity.Size = new System.Drawing.Size(61, 20);
            this.comboBox_Parity.TabIndex = 21;
            this.comboBox_Parity.SelectedIndexChanged += new System.EventHandler(this.comboBox_Parity_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F);
            this.label2.Location = new System.Drawing.Point(155, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 22;
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
            this.comboBox_DataBits.Location = new System.Drawing.Point(77, 48);
            this.comboBox_DataBits.Name = "comboBox_DataBits";
            this.comboBox_DataBits.Size = new System.Drawing.Size(61, 20);
            this.comboBox_DataBits.TabIndex = 19;
            this.comboBox_DataBits.SelectedIndexChanged += new System.EventHandler(this.comboBox_DataBits_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(6, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "DataBits:";
            // 
            // comboBox_COM
            // 
            this.comboBox_COM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COM.FormattingEnabled = true;
            this.comboBox_COM.Location = new System.Drawing.Point(59, 14);
            this.comboBox_COM.Name = "comboBox_COM";
            this.comboBox_COM.Size = new System.Drawing.Size(81, 20);
            this.comboBox_COM.TabIndex = 0;
            this.comboBox_COM.SelectedIndexChanged += new System.EventHandler(this.comboBox_COM_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新細明體", 12F);
            this.label5.Location = new System.Drawing.Point(146, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Baudrate:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F);
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "COM:";
            // 
            // serialPort
            // 
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // PLC_SerialPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PLC_SerialPort";
            this.Size = new System.Drawing.Size(428, 82);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox_COM;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private ExButton exButton_Open;
        private System.Windows.Forms.ComboBox comboBox_StopBits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_Parity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_DataBits;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_Baudrate;
        private System.IO.Ports.SerialPort serialPort;
    }
}
