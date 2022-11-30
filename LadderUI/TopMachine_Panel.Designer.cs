namespace LadderUI
{
    partial class TopMachine_Panel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopMachine_Panel));
            this.panel_main = new System.Windows.Forms.Panel();
            this.udP_Cilent = new TCP.UDP_Cilent();
            this.exButton_Connection_Test = new MyUI.ExButton();
            this.groupBox_SerialProt = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox_COM = new System.Windows.Forms.ComboBox();
            this.numWordTextBox_Baudrate = new MyUI.NumWordTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TextBox_Data_Value_Write = new MyUI.NumTextBox();
            this.radioButton_Data_Single_Write = new System.Windows.Forms.RadioButton();
            this.radioButton_Data_Double_Write = new System.Windows.Forms.RadioButton();
            this.exButton_Data_Write = new MyUI.ExButton();
            this.TextBox_Data_Device_Write = new MyUI.NumWordTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.exButton_bool_Write = new MyUI.ExButton();
            this.radioButton_bool_OFF_Write = new System.Windows.Forms.RadioButton();
            this.radioButton_bool_ON_Write = new System.Windows.Forms.RadioButton();
            this.TextBox_bool_Device_Write = new MyUI.NumWordTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.TextBox_Data_Value_Read = new MyUI.NumTextBox();
            this.radioButton_Data_Single_Read = new System.Windows.Forms.RadioButton();
            this.radioButton_Data_Double_Read = new System.Windows.Forms.RadioButton();
            this.exButton_Data_Read = new MyUI.ExButton();
            this.TextBox_Data_Device_Read = new MyUI.NumWordTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.exButton_bool_Read = new MyUI.ExButton();
            this.radioButton_bool_OFF_Read = new System.Windows.Forms.RadioButton();
            this.radioButton_bool_ON_Read = new System.Windows.Forms.RadioButton();
            this.TextBox_bool_Device_Read = new MyUI.NumWordTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.radioButton_Enthernet = new System.Windows.Forms.RadioButton();
            this.radioButton_SerialPort = new System.Windows.Forms.RadioButton();
            this.exButton_OK = new MyUI.ExButton();
            this.exButton_Close = new MyUI.ExButton();
            this.timer_程序執行 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel_main.SuspendLayout();
            this.groupBox_SerialProt.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.udP_Cilent);
            this.panel_main.Controls.Add(this.exButton_Connection_Test);
            this.panel_main.Controls.Add(this.groupBox_SerialProt);
            this.panel_main.Controls.Add(this.groupBox5);
            this.panel_main.Controls.Add(this.radioButton_Enthernet);
            this.panel_main.Controls.Add(this.radioButton_SerialPort);
            this.panel_main.Controls.Add(this.exButton_OK);
            this.panel_main.Controls.Add(this.exButton_Close);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(0, 0);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(872, 573);
            this.panel_main.TabIndex = 0;
            // 
            // udP_Cilent
            // 
            this.udP_Cilent.Location = new System.Drawing.Point(12, 38);
            this.udP_Cilent.Name = "udP_Cilent";
            this.udP_Cilent.Size = new System.Drawing.Size(360, 528);
            this.udP_Cilent.TabIndex = 18;
            // 
            // exButton_Connection_Test
            // 
            this.exButton_Connection_Test.Font = new System.Drawing.Font("新細明體", 12F);
            this.exButton_Connection_Test.Location = new System.Drawing.Point(505, 527);
            this.exButton_Connection_Test.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.exButton_Connection_Test.Name = "exButton_Connection_Test";
            this.exButton_Connection_Test.OFF_文字內容 = "Connection Test";
            this.exButton_Connection_Test.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Connection_Test.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_Connection_Test.ON_文字內容 = "Connection Test";
            this.exButton_Connection_Test.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Connection_Test.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_Connection_Test.Size = new System.Drawing.Size(141, 41);
            this.exButton_Connection_Test.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_Connection_Test.TabIndex = 17;
            this.exButton_Connection_Test.字型鎖住 = false;
            this.exButton_Connection_Test.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_Connection_Test.文字鎖住 = false;
            this.exButton_Connection_Test.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Connection_Test.狀態OFF圖片")));
            this.exButton_Connection_Test.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Connection_Test.狀態ON圖片")));
            this.exButton_Connection_Test.讀寫鎖住 = true;
            this.exButton_Connection_Test.音效 = false;
            this.exButton_Connection_Test.顯示狀態 = false;
            this.exButton_Connection_Test.btnClick += new System.EventHandler(this.exButton_Connection_Test_btnClick);
            // 
            // groupBox_SerialProt
            // 
            this.groupBox_SerialProt.Controls.Add(this.panel1);
            this.groupBox_SerialProt.Enabled = false;
            this.groupBox_SerialProt.Font = new System.Drawing.Font("新細明體", 12F);
            this.groupBox_SerialProt.Location = new System.Drawing.Point(383, 35);
            this.groupBox_SerialProt.Name = "groupBox_SerialProt";
            this.groupBox_SerialProt.Size = new System.Drawing.Size(476, 90);
            this.groupBox_SerialProt.TabIndex = 14;
            this.groupBox_SerialProt.TabStop = false;
            this.groupBox_SerialProt.Text = "SerialPort";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.comboBox_COM);
            this.panel1.Controls.Add(this.numWordTextBox_Baudrate);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(8, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(456, 51);
            this.panel1.TabIndex = 8;
            // 
            // comboBox_COM
            // 
            this.comboBox_COM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COM.FormattingEnabled = true;
            this.comboBox_COM.Location = new System.Drawing.Point(59, 13);
            this.comboBox_COM.Name = "comboBox_COM";
            this.comboBox_COM.Size = new System.Drawing.Size(81, 24);
            this.comboBox_COM.TabIndex = 0;
            this.comboBox_COM.SelectedIndexChanged += new System.EventHandler(this.comboBox_COM_SelectedIndexChanged);
            // 
            // numWordTextBox_Baudrate
            // 
            this.numWordTextBox_Baudrate.Enabled = false;
            this.numWordTextBox_Baudrate.Location = new System.Drawing.Point(220, 12);
            this.numWordTextBox_Baudrate.Name = "numWordTextBox_Baudrate";
            this.numWordTextBox_Baudrate.Size = new System.Drawing.Size(80, 27);
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
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox1);
            this.groupBox5.Controls.Add(this.groupBox2);
            this.groupBox5.Controls.Add(this.groupBox4);
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Font = new System.Drawing.Font("新細明體", 12F);
            this.groupBox5.Location = new System.Drawing.Point(383, 131);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(476, 351);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Set Value";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextBox_Data_Value_Write);
            this.groupBox1.Controls.Add(this.radioButton_Data_Single_Write);
            this.groupBox1.Controls.Add(this.radioButton_Data_Double_Write);
            this.groupBox1.Controls.Add(this.exButton_Data_Write);
            this.groupBox1.Controls.Add(this.TextBox_Data_Device_Write);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("新細明體", 12F);
            this.groupBox1.Location = new System.Drawing.Point(6, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 60);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data";
            // 
            // TextBox_Data_Value_Write
            // 
            this.TextBox_Data_Value_Write.Location = new System.Drawing.Point(178, 21);
            this.TextBox_Data_Value_Write.Name = "TextBox_Data_Value_Write";
            this.TextBox_Data_Value_Write.Size = new System.Drawing.Size(130, 27);
            this.TextBox_Data_Value_Write.TabIndex = 11;
            this.TextBox_Data_Value_Write.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TextBox_Data_Value_Write.字元長度 = MyUI.NumTextBox.WordLengthEnum.單字元;
            this.TextBox_Data_Value_Write.小數點位置 = 0;
            this.TextBox_Data_Value_Write.音效 = false;
            this.TextBox_Data_Value_Write.顯示螢幕小鍵盤 = false;
            // 
            // radioButton_Data_Single_Write
            // 
            this.radioButton_Data_Single_Write.AutoSize = true;
            this.radioButton_Data_Single_Write.Checked = true;
            this.radioButton_Data_Single_Write.Font = new System.Drawing.Font("新細明體", 9F);
            this.radioButton_Data_Single_Write.Location = new System.Drawing.Point(315, 37);
            this.radioButton_Data_Single_Write.Name = "radioButton_Data_Single_Write";
            this.radioButton_Data_Single_Write.Size = new System.Drawing.Size(52, 16);
            this.radioButton_Data_Single_Write.TabIndex = 10;
            this.radioButton_Data_Single_Write.TabStop = true;
            this.radioButton_Data_Single_Write.Text = "Single";
            this.radioButton_Data_Single_Write.UseVisualStyleBackColor = true;
            // 
            // radioButton_Data_Double_Write
            // 
            this.radioButton_Data_Double_Write.AutoSize = true;
            this.radioButton_Data_Double_Write.Font = new System.Drawing.Font("新細明體", 9F);
            this.radioButton_Data_Double_Write.Location = new System.Drawing.Point(315, 17);
            this.radioButton_Data_Double_Write.Name = "radioButton_Data_Double_Write";
            this.radioButton_Data_Double_Write.Size = new System.Drawing.Size(57, 16);
            this.radioButton_Data_Double_Write.TabIndex = 9;
            this.radioButton_Data_Double_Write.Text = "Double";
            this.radioButton_Data_Double_Write.UseVisualStyleBackColor = true;
            // 
            // exButton_Data_Write
            // 
            this.exButton_Data_Write.Location = new System.Drawing.Point(377, 17);
            this.exButton_Data_Write.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.exButton_Data_Write.Name = "exButton_Data_Write";
            this.exButton_Data_Write.OFF_文字內容 = "Write";
            this.exButton_Data_Write.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Data_Write.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_Data_Write.ON_文字內容 = "Write";
            this.exButton_Data_Write.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Data_Write.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_Data_Write.Size = new System.Drawing.Size(78, 36);
            this.exButton_Data_Write.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_Data_Write.TabIndex = 5;
            this.exButton_Data_Write.字型鎖住 = false;
            this.exButton_Data_Write.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_Data_Write.文字鎖住 = false;
            this.exButton_Data_Write.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Data_Write.狀態OFF圖片")));
            this.exButton_Data_Write.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Data_Write.狀態ON圖片")));
            this.exButton_Data_Write.讀寫鎖住 = true;
            this.exButton_Data_Write.音效 = false;
            this.exButton_Data_Write.顯示狀態 = false;
            // 
            // TextBox_Data_Device_Write
            // 
            this.TextBox_Data_Device_Write.Location = new System.Drawing.Point(68, 21);
            this.TextBox_Data_Device_Write.Name = "TextBox_Data_Device_Write";
            this.TextBox_Data_Device_Write.Size = new System.Drawing.Size(58, 27);
            this.TextBox_Data_Device_Write.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F);
            this.label2.Location = new System.Drawing.Point(132, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Value:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(13, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Device:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.exButton_bool_Write);
            this.groupBox2.Controls.Add(this.radioButton_bool_OFF_Write);
            this.groupBox2.Controls.Add(this.radioButton_bool_ON_Write);
            this.groupBox2.Controls.Add(this.TextBox_bool_Device_Write);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("新細明體", 12F);
            this.groupBox2.Location = new System.Drawing.Point(6, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(464, 60);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bool";
            // 
            // exButton_bool_Write
            // 
            this.exButton_bool_Write.Location = new System.Drawing.Point(377, 16);
            this.exButton_bool_Write.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.exButton_bool_Write.Name = "exButton_bool_Write";
            this.exButton_bool_Write.OFF_文字內容 = "Write";
            this.exButton_bool_Write.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_bool_Write.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_bool_Write.ON_文字內容 = "Write";
            this.exButton_bool_Write.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_bool_Write.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_bool_Write.Size = new System.Drawing.Size(78, 36);
            this.exButton_bool_Write.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_bool_Write.TabIndex = 8;
            this.exButton_bool_Write.字型鎖住 = false;
            this.exButton_bool_Write.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_bool_Write.文字鎖住 = false;
            this.exButton_bool_Write.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_bool_Write.狀態OFF圖片")));
            this.exButton_bool_Write.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_bool_Write.狀態ON圖片")));
            this.exButton_bool_Write.讀寫鎖住 = true;
            this.exButton_bool_Write.音效 = false;
            this.exButton_bool_Write.顯示狀態 = false;
            // 
            // radioButton_bool_OFF_Write
            // 
            this.radioButton_bool_OFF_Write.AutoSize = true;
            this.radioButton_bool_OFF_Write.Checked = true;
            this.radioButton_bool_OFF_Write.Location = new System.Drawing.Point(203, 24);
            this.radioButton_bool_OFF_Write.Name = "radioButton_bool_OFF_Write";
            this.radioButton_bool_OFF_Write.Size = new System.Drawing.Size(53, 20);
            this.radioButton_bool_OFF_Write.TabIndex = 6;
            this.radioButton_bool_OFF_Write.TabStop = true;
            this.radioButton_bool_OFF_Write.Text = "OFF";
            this.radioButton_bool_OFF_Write.UseVisualStyleBackColor = true;
            // 
            // radioButton_bool_ON_Write
            // 
            this.radioButton_bool_ON_Write.AutoSize = true;
            this.radioButton_bool_ON_Write.Location = new System.Drawing.Point(145, 24);
            this.radioButton_bool_ON_Write.Name = "radioButton_bool_ON_Write";
            this.radioButton_bool_ON_Write.Size = new System.Drawing.Size(48, 20);
            this.radioButton_bool_ON_Write.TabIndex = 5;
            this.radioButton_bool_ON_Write.Text = "ON";
            this.radioButton_bool_ON_Write.UseVisualStyleBackColor = true;
            // 
            // TextBox_bool_Device_Write
            // 
            this.TextBox_bool_Device_Write.Location = new System.Drawing.Point(68, 21);
            this.TextBox_bool_Device_Write.Name = "TextBox_bool_Device_Write";
            this.TextBox_bool_Device_Write.Size = new System.Drawing.Size(58, 27);
            this.TextBox_bool_Device_Write.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F);
            this.label4.Location = new System.Drawing.Point(13, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Device:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.TextBox_Data_Value_Read);
            this.groupBox4.Controls.Add(this.radioButton_Data_Single_Read);
            this.groupBox4.Controls.Add(this.radioButton_Data_Double_Read);
            this.groupBox4.Controls.Add(this.exButton_Data_Read);
            this.groupBox4.Controls.Add(this.TextBox_Data_Device_Read);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Font = new System.Drawing.Font("新細明體", 12F);
            this.groupBox4.Location = new System.Drawing.Point(6, 160);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(464, 60);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Data";
            // 
            // TextBox_Data_Value_Read
            // 
            this.TextBox_Data_Value_Read.Enabled = false;
            this.TextBox_Data_Value_Read.Location = new System.Drawing.Point(178, 21);
            this.TextBox_Data_Value_Read.Name = "TextBox_Data_Value_Read";
            this.TextBox_Data_Value_Read.Size = new System.Drawing.Size(130, 27);
            this.TextBox_Data_Value_Read.TabIndex = 12;
            this.TextBox_Data_Value_Read.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TextBox_Data_Value_Read.字元長度 = MyUI.NumTextBox.WordLengthEnum.單字元;
            this.TextBox_Data_Value_Read.小數點位置 = 0;
            this.TextBox_Data_Value_Read.音效 = false;
            this.TextBox_Data_Value_Read.顯示螢幕小鍵盤 = false;
            // 
            // radioButton_Data_Single_Read
            // 
            this.radioButton_Data_Single_Read.AutoSize = true;
            this.radioButton_Data_Single_Read.Checked = true;
            this.radioButton_Data_Single_Read.Font = new System.Drawing.Font("新細明體", 9F);
            this.radioButton_Data_Single_Read.Location = new System.Drawing.Point(316, 37);
            this.radioButton_Data_Single_Read.Name = "radioButton_Data_Single_Read";
            this.radioButton_Data_Single_Read.Size = new System.Drawing.Size(52, 16);
            this.radioButton_Data_Single_Read.TabIndex = 10;
            this.radioButton_Data_Single_Read.TabStop = true;
            this.radioButton_Data_Single_Read.Text = "Single";
            this.radioButton_Data_Single_Read.UseVisualStyleBackColor = true;
            // 
            // radioButton_Data_Double_Read
            // 
            this.radioButton_Data_Double_Read.AutoSize = true;
            this.radioButton_Data_Double_Read.Font = new System.Drawing.Font("新細明體", 9F);
            this.radioButton_Data_Double_Read.Location = new System.Drawing.Point(316, 17);
            this.radioButton_Data_Double_Read.Name = "radioButton_Data_Double_Read";
            this.radioButton_Data_Double_Read.Size = new System.Drawing.Size(57, 16);
            this.radioButton_Data_Double_Read.TabIndex = 9;
            this.radioButton_Data_Double_Read.Text = "Double";
            this.radioButton_Data_Double_Read.UseVisualStyleBackColor = true;
            // 
            // exButton_Data_Read
            // 
            this.exButton_Data_Read.Location = new System.Drawing.Point(377, 17);
            this.exButton_Data_Read.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.exButton_Data_Read.Name = "exButton_Data_Read";
            this.exButton_Data_Read.OFF_文字內容 = "Read";
            this.exButton_Data_Read.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Data_Read.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_Data_Read.ON_文字內容 = "Read";
            this.exButton_Data_Read.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Data_Read.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_Data_Read.Size = new System.Drawing.Size(78, 36);
            this.exButton_Data_Read.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_Data_Read.TabIndex = 5;
            this.exButton_Data_Read.字型鎖住 = false;
            this.exButton_Data_Read.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_Data_Read.文字鎖住 = false;
            this.exButton_Data_Read.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Data_Read.狀態OFF圖片")));
            this.exButton_Data_Read.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Data_Read.狀態ON圖片")));
            this.exButton_Data_Read.讀寫鎖住 = true;
            this.exButton_Data_Read.音效 = false;
            this.exButton_Data_Read.顯示狀態 = false;
            // 
            // TextBox_Data_Device_Read
            // 
            this.TextBox_Data_Device_Read.Location = new System.Drawing.Point(68, 21);
            this.TextBox_Data_Device_Read.Name = "TextBox_Data_Device_Read";
            this.TextBox_Data_Device_Read.Size = new System.Drawing.Size(58, 27);
            this.TextBox_Data_Device_Read.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("新細明體", 12F);
            this.label7.Location = new System.Drawing.Point(132, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "Value:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("新細明體", 12F);
            this.label8.Location = new System.Drawing.Point(13, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "Device:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.exButton_bool_Read);
            this.groupBox3.Controls.Add(this.radioButton_bool_OFF_Read);
            this.groupBox3.Controls.Add(this.radioButton_bool_ON_Read);
            this.groupBox3.Controls.Add(this.TextBox_bool_Device_Read);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Font = new System.Drawing.Font("新細明體", 12F);
            this.groupBox3.Location = new System.Drawing.Point(6, 226);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(464, 60);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bool";
            // 
            // exButton_bool_Read
            // 
            this.exButton_bool_Read.Location = new System.Drawing.Point(377, 16);
            this.exButton_bool_Read.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.exButton_bool_Read.Name = "exButton_bool_Read";
            this.exButton_bool_Read.OFF_文字內容 = "Read";
            this.exButton_bool_Read.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_bool_Read.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_bool_Read.ON_文字內容 = "Read";
            this.exButton_bool_Read.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_bool_Read.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_bool_Read.Size = new System.Drawing.Size(78, 36);
            this.exButton_bool_Read.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_bool_Read.TabIndex = 8;
            this.exButton_bool_Read.字型鎖住 = false;
            this.exButton_bool_Read.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_bool_Read.文字鎖住 = false;
            this.exButton_bool_Read.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_bool_Read.狀態OFF圖片")));
            this.exButton_bool_Read.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_bool_Read.狀態ON圖片")));
            this.exButton_bool_Read.讀寫鎖住 = true;
            this.exButton_bool_Read.音效 = false;
            this.exButton_bool_Read.顯示狀態 = false;
            // 
            // radioButton_bool_OFF_Read
            // 
            this.radioButton_bool_OFF_Read.AutoSize = true;
            this.radioButton_bool_OFF_Read.Checked = true;
            this.radioButton_bool_OFF_Read.Enabled = false;
            this.radioButton_bool_OFF_Read.Location = new System.Drawing.Point(203, 24);
            this.radioButton_bool_OFF_Read.Name = "radioButton_bool_OFF_Read";
            this.radioButton_bool_OFF_Read.Size = new System.Drawing.Size(53, 20);
            this.radioButton_bool_OFF_Read.TabIndex = 6;
            this.radioButton_bool_OFF_Read.TabStop = true;
            this.radioButton_bool_OFF_Read.Text = "OFF";
            this.radioButton_bool_OFF_Read.UseVisualStyleBackColor = true;
            // 
            // radioButton_bool_ON_Read
            // 
            this.radioButton_bool_ON_Read.AutoSize = true;
            this.radioButton_bool_ON_Read.Enabled = false;
            this.radioButton_bool_ON_Read.Location = new System.Drawing.Point(145, 24);
            this.radioButton_bool_ON_Read.Name = "radioButton_bool_ON_Read";
            this.radioButton_bool_ON_Read.Size = new System.Drawing.Size(14, 13);
            this.radioButton_bool_ON_Read.TabIndex = 5;
            this.radioButton_bool_ON_Read.UseVisualStyleBackColor = true;
            // 
            // TextBox_bool_Device_Read
            // 
            this.TextBox_bool_Device_Read.Location = new System.Drawing.Point(68, 21);
            this.TextBox_bool_Device_Read.Name = "TextBox_bool_Device_Read";
            this.TextBox_bool_Device_Read.Size = new System.Drawing.Size(58, 27);
            this.TextBox_bool_Device_Read.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("新細明體", 12F);
            this.label6.Location = new System.Drawing.Point(13, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Device:";
            // 
            // radioButton_Enthernet
            // 
            this.radioButton_Enthernet.AutoSize = true;
            this.radioButton_Enthernet.Checked = true;
            this.radioButton_Enthernet.Font = new System.Drawing.Font("新細明體", 12F);
            this.radioButton_Enthernet.Location = new System.Drawing.Point(14, 12);
            this.radioButton_Enthernet.Name = "radioButton_Enthernet";
            this.radioButton_Enthernet.Size = new System.Drawing.Size(86, 20);
            this.radioButton_Enthernet.TabIndex = 12;
            this.radioButton_Enthernet.TabStop = true;
            this.radioButton_Enthernet.Text = "Enthernet";
            this.radioButton_Enthernet.UseVisualStyleBackColor = true;
            this.radioButton_Enthernet.CheckedChanged += new System.EventHandler(this.radioButton_Enthernet_CheckedChanged);
            // 
            // radioButton_SerialPort
            // 
            this.radioButton_SerialPort.AutoSize = true;
            this.radioButton_SerialPort.Font = new System.Drawing.Font("新細明體", 12F);
            this.radioButton_SerialPort.Location = new System.Drawing.Point(383, 12);
            this.radioButton_SerialPort.Name = "radioButton_SerialPort";
            this.radioButton_SerialPort.Size = new System.Drawing.Size(86, 20);
            this.radioButton_SerialPort.TabIndex = 11;
            this.radioButton_SerialPort.Text = "SerialPort";
            this.radioButton_SerialPort.UseVisualStyleBackColor = true;
            this.radioButton_SerialPort.CheckedChanged += new System.EventHandler(this.radioButton_SerialPort_CheckedChanged);
            // 
            // exButton_OK
            // 
            this.exButton_OK.Location = new System.Drawing.Point(670, 530);
            this.exButton_OK.Margin = new System.Windows.Forms.Padding(4);
            this.exButton_OK.Name = "exButton_OK";
            this.exButton_OK.OFF_文字內容 = "OK";
            this.exButton_OK.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_OK.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_OK.ON_文字內容 = "OK";
            this.exButton_OK.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_OK.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_OK.Size = new System.Drawing.Size(88, 36);
            this.exButton_OK.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_OK.TabIndex = 9;
            this.exButton_OK.字型鎖住 = false;
            this.exButton_OK.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_OK.文字鎖住 = false;
            this.exButton_OK.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_OK.狀態OFF圖片")));
            this.exButton_OK.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_OK.狀態ON圖片")));
            this.exButton_OK.讀寫鎖住 = false;
            this.exButton_OK.音效 = false;
            this.exButton_OK.顯示狀態 = false;
            this.exButton_OK.btnClick += new System.EventHandler(this.exButton_OK_btnClick);
            // 
            // exButton_Close
            // 
            this.exButton_Close.Location = new System.Drawing.Point(771, 530);
            this.exButton_Close.Margin = new System.Windows.Forms.Padding(4);
            this.exButton_Close.Name = "exButton_Close";
            this.exButton_Close.OFF_文字內容 = "Close";
            this.exButton_Close.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Close.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_Close.ON_文字內容 = "Close";
            this.exButton_Close.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Close.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_Close.Size = new System.Drawing.Size(88, 36);
            this.exButton_Close.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_Close.TabIndex = 7;
            this.exButton_Close.字型鎖住 = false;
            this.exButton_Close.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_Close.文字鎖住 = false;
            this.exButton_Close.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Close.狀態OFF圖片")));
            this.exButton_Close.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_Close.狀態ON圖片")));
            this.exButton_Close.讀寫鎖住 = false;
            this.exButton_Close.音效 = false;
            this.exButton_Close.顯示狀態 = false;
            this.exButton_Close.btnClick += new System.EventHandler(this.exButton_Close_btnClick);
            // 
            // timer_程序執行
            // 
            this.timer_程序執行.Enabled = true;
            this.timer_程序執行.Interval = 10;
            this.timer_程序執行.Tick += new System.EventHandler(this.timer_程序執行_Tick);
            // 
            // TopMachine_Panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_main);
            this.Name = "TopMachine_Panel";
            this.Size = new System.Drawing.Size(872, 573);
            this.Load += new System.EventHandler(this.TopMachine_Panel_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            this.groupBox_SerialProt.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_main;
        private MyUI.ExButton exButton_Close;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton_bool_OFF_Write;
        private System.Windows.Forms.RadioButton radioButton_bool_ON_Write;
        private MyUI.NumWordTextBox TextBox_bool_Device_Write;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private MyUI.NumWordTextBox TextBox_Data_Device_Write;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox_COM;
        private MyUI.ExButton exButton_Connection_Test;
        private MyUI.NumWordTextBox numWordTextBox_Baudrate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private MyUI.ExButton exButton_OK;
        private MyUI.ExButton exButton_Data_Write;
        private MyUI.ExButton exButton_bool_Write;
        private System.Windows.Forms.RadioButton radioButton_Data_Single_Write;
        private System.Windows.Forms.RadioButton radioButton_Data_Double_Write;
        private System.Windows.Forms.GroupBox groupBox3;
        private MyUI.ExButton exButton_bool_Read;
        private System.Windows.Forms.RadioButton radioButton_bool_OFF_Read;
        private System.Windows.Forms.RadioButton radioButton_bool_ON_Read;
        private MyUI.NumWordTextBox TextBox_bool_Device_Read;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButton_Data_Single_Read;
        private System.Windows.Forms.RadioButton radioButton_Data_Double_Read;
        private MyUI.ExButton exButton_Data_Read;
        private MyUI.NumWordTextBox TextBox_Data_Device_Read;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private MyUI.NumTextBox TextBox_Data_Value_Read;
        private MyUI.NumTextBox TextBox_Data_Value_Write;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox_SerialProt;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButton_Enthernet;
        private System.Windows.Forms.RadioButton radioButton_SerialPort;
        public System.Windows.Forms.Timer timer_程序執行;
        public TCP.UDP_Cilent udP_Cilent;
    }
}
