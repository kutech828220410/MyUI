namespace LadderUI
{
    partial class LowerMachine_Panel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LowerMachine_Panel));
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox_COM = new System.Windows.Forms.ComboBox();
            this.exButton_Open = new MyUI.ExButton();
            this.numWordTextBox_Baudrate = new MyUI.NumWordTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel_main = new System.Windows.Forms.Panel();
            this.udP_Cilent = new TCP.UDP_Cilent();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_Enthernet = new System.Windows.Forms.RadioButton();
            this.radioButton_SerialPort = new System.Windows.Forms.RadioButton();
            this.checkBox_SerialPort_自動連線 = new System.Windows.Forms.CheckBox();
            this.groupBox_Readlist = new System.Windows.Forms.GroupBox();
            this.checkBox_ReadList_Refesh = new System.Windows.Forms.CheckBox();
            this.listBox_Read = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_CycleTime = new System.Windows.Forms.Label();
            this.label_單位 = new System.Windows.Forms.Label();
            this.groupBox_Sendlist = new System.Windows.Forms.GroupBox();
            this.checkBox_SendList_Refesh = new System.Windows.Forms.CheckBox();
            this.listBox_Send = new System.Windows.Forms.ListBox();
            this.backgroundWorker_主程序 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.panel_main.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox_Readlist.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox_Sendlist.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox_COM);
            this.panel1.Controls.Add(this.exButton_Open);
            this.panel1.Controls.Add(this.numWordTextBox_Baudrate);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(6, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(456, 51);
            this.panel1.TabIndex = 9;
            // 
            // comboBox_COM
            // 
            this.comboBox_COM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COM.FormattingEnabled = true;
            this.comboBox_COM.Location = new System.Drawing.Point(59, 13);
            this.comboBox_COM.Name = "comboBox_COM";
            this.comboBox_COM.Size = new System.Drawing.Size(81, 20);
            this.comboBox_COM.TabIndex = 18;
            // 
            // exButton_Open
            // 
            this.exButton_Open.Location = new System.Drawing.Point(362, 5);
            this.exButton_Open.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.exButton_Open.Name = "exButton_Open";
            this.exButton_Open.OFF_文字內容 = "Open";
            this.exButton_Open.OFF_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Open.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_Open.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_Open.ON_文字內容 = "Open";
            this.exButton_Open.ON_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Open.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_Open.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.exButton_Open.Size = new System.Drawing.Size(88, 41);
            this.exButton_Open.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_Open.TabIndex = 17;
            this.exButton_Open.字型鎖住 = false;
            this.exButton_Open.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_Open.文字鎖住 = false;
            this.exButton_Open.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Open.狀態OFF圖片")));
            this.exButton_Open.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Open.狀態ON圖片")));
            this.exButton_Open.讀寫鎖住 = false;
            this.exButton_Open.音效 = false;
            this.exButton_Open.顯示狀態 = false;
            this.exButton_Open.btnClick += new System.EventHandler(this.exButton_Open_btnClick);
            // 
            // numWordTextBox_Baudrate
            // 
            this.numWordTextBox_Baudrate.Enabled = false;
            this.numWordTextBox_Baudrate.Location = new System.Drawing.Point(220, 12);
            this.numWordTextBox_Baudrate.Name = "numWordTextBox_Baudrate";
            this.numWordTextBox_Baudrate.Size = new System.Drawing.Size(80, 22);
            this.numWordTextBox_Baudrate.TabIndex = 16;
            this.numWordTextBox_Baudrate.Text = "115200";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新細明體", 12F);
            this.label5.Location = new System.Drawing.Point(146, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Baudrate:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F);
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "COM:";
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.udP_Cilent);
            this.panel_main.Controls.Add(this.groupBox1);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(0, 0);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(869, 565);
            this.panel_main.TabIndex = 10;
            // 
            // udP_Cilent
            // 
            this.udP_Cilent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udP_Cilent.Location = new System.Drawing.Point(470, 0);
            this.udP_Cilent.Name = "udP_Cilent";
            this.udP_Cilent.Size = new System.Drawing.Size(399, 565);
            this.udP_Cilent.TabIndex = 9;
            this.udP_Cilent.Load += new System.EventHandler(this.udP_Cilent_Load);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_Enthernet);
            this.groupBox1.Controls.Add(this.radioButton_SerialPort);
            this.groupBox1.Controls.Add(this.checkBox_SerialPort_自動連線);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.groupBox_Readlist);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.groupBox_Sendlist);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 565);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SerialProt";
            // 
            // radioButton_Enthernet
            // 
            this.radioButton_Enthernet.AutoSize = true;
            this.radioButton_Enthernet.Checked = true;
            this.radioButton_Enthernet.Font = new System.Drawing.Font("新細明體", 12F);
            this.radioButton_Enthernet.Location = new System.Drawing.Point(15, 529);
            this.radioButton_Enthernet.Name = "radioButton_Enthernet";
            this.radioButton_Enthernet.Size = new System.Drawing.Size(86, 20);
            this.radioButton_Enthernet.TabIndex = 18;
            this.radioButton_Enthernet.TabStop = true;
            this.radioButton_Enthernet.Text = "Enthernet";
            this.radioButton_Enthernet.UseVisualStyleBackColor = true;
            this.radioButton_Enthernet.CheckedChanged += new System.EventHandler(this.radioButton_Enthernet_CheckedChanged);
            // 
            // radioButton_SerialPort
            // 
            this.radioButton_SerialPort.AutoSize = true;
            this.radioButton_SerialPort.Font = new System.Drawing.Font("新細明體", 12F);
            this.radioButton_SerialPort.Location = new System.Drawing.Point(107, 529);
            this.radioButton_SerialPort.Name = "radioButton_SerialPort";
            this.radioButton_SerialPort.Size = new System.Drawing.Size(86, 20);
            this.radioButton_SerialPort.TabIndex = 17;
            this.radioButton_SerialPort.Text = "SerialPort";
            this.radioButton_SerialPort.UseVisualStyleBackColor = true;
            this.radioButton_SerialPort.CheckedChanged += new System.EventHandler(this.radioButton_SerialPort_CheckedChanged);
            // 
            // checkBox_SerialPort_自動連線
            // 
            this.checkBox_SerialPort_自動連線.AutoSize = true;
            this.checkBox_SerialPort_自動連線.Location = new System.Drawing.Point(274, 406);
            this.checkBox_SerialPort_自動連線.Name = "checkBox_SerialPort_自動連線";
            this.checkBox_SerialPort_自動連線.Size = new System.Drawing.Size(72, 16);
            this.checkBox_SerialPort_自動連線.TabIndex = 15;
            this.checkBox_SerialPort_自動連線.Text = "自動連線";
            this.checkBox_SerialPort_自動連線.UseVisualStyleBackColor = true;
            this.checkBox_SerialPort_自動連線.CheckedChanged += new System.EventHandler(this.checkBox_SerialPort_自動連線_CheckedChanged);
            // 
            // groupBox_Readlist
            // 
            this.groupBox_Readlist.Controls.Add(this.checkBox_ReadList_Refesh);
            this.groupBox_Readlist.Controls.Add(this.listBox_Read);
            this.groupBox_Readlist.Location = new System.Drawing.Point(6, 78);
            this.groupBox_Readlist.Name = "groupBox_Readlist";
            this.groupBox_Readlist.Size = new System.Drawing.Size(230, 306);
            this.groupBox_Readlist.TabIndex = 11;
            this.groupBox_Readlist.TabStop = false;
            this.groupBox_Readlist.Text = "Read List";
            // 
            // checkBox_ReadList_Refesh
            // 
            this.checkBox_ReadList_Refesh.AutoSize = true;
            this.checkBox_ReadList_Refesh.Location = new System.Drawing.Point(176, 14);
            this.checkBox_ReadList_Refesh.Name = "checkBox_ReadList_Refesh";
            this.checkBox_ReadList_Refesh.Size = new System.Drawing.Size(48, 16);
            this.checkBox_ReadList_Refesh.TabIndex = 14;
            this.checkBox_ReadList_Refesh.Text = "更新";
            this.checkBox_ReadList_Refesh.UseVisualStyleBackColor = true;
            // 
            // listBox_Read
            // 
            this.listBox_Read.FormattingEnabled = true;
            this.listBox_Read.ItemHeight = 12;
            this.listBox_Read.Location = new System.Drawing.Point(6, 36);
            this.listBox_Read.Name = "listBox_Read";
            this.listBox_Read.Size = new System.Drawing.Size(218, 256);
            this.listBox_Read.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label_CycleTime);
            this.panel2.Controls.Add(this.label_單位);
            this.panel2.Location = new System.Drawing.Point(352, 396);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(104, 31);
            this.panel2.TabIndex = 16;
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
            // groupBox_Sendlist
            // 
            this.groupBox_Sendlist.Controls.Add(this.checkBox_SendList_Refesh);
            this.groupBox_Sendlist.Controls.Add(this.listBox_Send);
            this.groupBox_Sendlist.Location = new System.Drawing.Point(236, 78);
            this.groupBox_Sendlist.Name = "groupBox_Sendlist";
            this.groupBox_Sendlist.Size = new System.Drawing.Size(226, 306);
            this.groupBox_Sendlist.TabIndex = 12;
            this.groupBox_Sendlist.TabStop = false;
            this.groupBox_Sendlist.Text = "Send List";
            // 
            // checkBox_SendList_Refesh
            // 
            this.checkBox_SendList_Refesh.AutoSize = true;
            this.checkBox_SendList_Refesh.Location = new System.Drawing.Point(172, 14);
            this.checkBox_SendList_Refesh.Name = "checkBox_SendList_Refesh";
            this.checkBox_SendList_Refesh.Size = new System.Drawing.Size(48, 16);
            this.checkBox_SendList_Refesh.TabIndex = 13;
            this.checkBox_SendList_Refesh.Text = "更新";
            this.checkBox_SendList_Refesh.UseVisualStyleBackColor = true;
            // 
            // listBox_Send
            // 
            this.listBox_Send.FormattingEnabled = true;
            this.listBox_Send.ItemHeight = 12;
            this.listBox_Send.Location = new System.Drawing.Point(6, 36);
            this.listBox_Send.Name = "listBox_Send";
            this.listBox_Send.Size = new System.Drawing.Size(214, 256);
            this.listBox_Send.TabIndex = 10;
            // 
            // backgroundWorker_主程序
            // 
            this.backgroundWorker_主程序.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_主程序_DoWork);
            // 
            // LowerMachine_Panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_main);
            this.Name = "LowerMachine_Panel";
            this.Size = new System.Drawing.Size(869, 565);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_main.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox_Readlist.ResumeLayout(false);
            this.groupBox_Readlist.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox_Sendlist.ResumeLayout(false);
            this.groupBox_Sendlist.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox_COM;
        private MyUI.ExButton exButton_Open;
        private MyUI.NumWordTextBox numWordTextBox_Baudrate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel_main;
        private System.ComponentModel.BackgroundWorker backgroundWorker_主程序;
        private System.Windows.Forms.ListBox listBox_Read;
        private System.Windows.Forms.GroupBox groupBox_Sendlist;
        private System.Windows.Forms.ListBox listBox_Send;
        private System.Windows.Forms.GroupBox groupBox_Readlist;
        private System.Windows.Forms.CheckBox checkBox_SendList_Refesh;
        private System.Windows.Forms.CheckBox checkBox_ReadList_Refesh;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_CycleTime;
        private System.Windows.Forms.Label label_單位;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_SerialPort_自動連線;
        private TCP.UDP_Cilent udP_Cilent;
        private System.Windows.Forms.RadioButton radioButton_Enthernet;
        private System.Windows.Forms.RadioButton radioButton_SerialPort;
    }
}
