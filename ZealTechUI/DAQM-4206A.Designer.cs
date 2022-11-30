namespace ZealTechUI
{
    partial class DAQM_4206A
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DAQM_4206A));
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_Adress = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.plC_Button_通訊測試 = new MyUI.PLC_Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_Baudrate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_COM = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.plC_Button_通訊已建立 = new MyUI.PLC_Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button_參數設定_Adress = new System.Windows.Forms.Button();
            this.comboBox_參數設定_Adress = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button_參數設定_Baudrate = new System.Windows.Forms.Button();
            this.comboBox_參數設定_Baudrate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.plC_NumBox_CH00_Value = new MyUI.PLC_NumBox();
            this.plC_NumBox_CH04_Value = new MyUI.PLC_NumBox();
            this.plC_NumBox_CH05_Value = new MyUI.PLC_NumBox();
            this.plC_NumBox_CH06_Value = new MyUI.PLC_NumBox();
            this.plC_NumBox_CH07_Value = new MyUI.PLC_NumBox();
            this.plC_NumBox_CH01_Value = new MyUI.PLC_NumBox();
            this.plC_NumBox_CH02_Value = new MyUI.PLC_NumBox();
            this.plC_NumBox_CH03_Value = new MyUI.PLC_NumBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.plC_Button_更新數值完成 = new MyUI.PLC_Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort
            // 
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.comboBox_Adress);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.plC_Button_通訊測試);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.comboBox_COM);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(435, 52);
            this.panel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(105, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 12);
            this.label5.TabIndex = 38;
            this.label5.Text = "Adress:";
            // 
            // comboBox_Adress
            // 
            this.comboBox_Adress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Adress.FormattingEnabled = true;
            this.comboBox_Adress.Location = new System.Drawing.Point(144, 14);
            this.comboBox_Adress.Name = "comboBox_Adress";
            this.comboBox_Adress.Size = new System.Drawing.Size(54, 20);
            this.comboBox_Adress.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "COM:";
            // 
            // plC_Button_通訊測試
            // 
            this.plC_Button_通訊測試.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_通訊測試.Location = new System.Drawing.Point(319, 2);
            this.plC_Button_通訊測試.Name = "plC_Button_通訊測試";
            this.plC_Button_通訊測試.OFF_文字內容 = "通訊測試";
            this.plC_Button_通訊測試.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_通訊測試.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_通訊測試.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_通訊測試.ON_文字內容 = "通訊測試";
            this.plC_Button_通訊測試.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_通訊測試.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_通訊測試.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_通訊測試.Size = new System.Drawing.Size(110, 42);
            this.plC_Button_通訊測試.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_通訊測試.TabIndex = 1;
            this.plC_Button_通訊測試.字型鎖住 = false;
            this.plC_Button_通訊測試.按鈕型態 = MyUI.PLC_Button.StatusEnum.交替型;
            this.plC_Button_通訊測試.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_通訊測試.文字鎖住 = false;
            this.plC_Button_通訊測試.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_通訊測試.狀態OFF圖片")));
            this.plC_Button_通訊測試.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_通訊測試.狀態ON圖片")));
            this.plC_Button_通訊測試.讀寫鎖住 = false;
            this.plC_Button_通訊測試.音效 = true;
            this.plC_Button_通訊測試.顯示 = false;
            this.plC_Button_通訊測試.顯示狀態 = false;
            this.plC_Button_通訊測試.btnClick += new System.EventHandler(this.plC_Button_通訊測試_btnClick);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label_Baudrate);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(203, 11);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(110, 27);
            this.panel2.TabIndex = 2;
            // 
            // label_Baudrate
            // 
            this.label_Baudrate.AutoSize = true;
            this.label_Baudrate.Location = new System.Drawing.Point(62, 6);
            this.label_Baudrate.Name = "label_Baudrate";
            this.label_Baudrate.Size = new System.Drawing.Size(29, 12);
            this.label_Baudrate.TabIndex = 40;
            this.label_Baudrate.Text = "9600";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Baudrate:";
            // 
            // comboBox_COM
            // 
            this.comboBox_COM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_COM.FormattingEnabled = true;
            this.comboBox_COM.Location = new System.Drawing.Point(38, 14);
            this.comboBox_COM.Name = "comboBox_COM";
            this.comboBox_COM.Size = new System.Drawing.Size(65, 20);
            this.comboBox_COM.TabIndex = 37;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.plC_Button_更新數值完成);
            this.groupBox1.Controls.Add(this.plC_Button_通訊已建立);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 109);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "參數設定";
            // 
            // plC_Button_通訊已建立
            // 
            this.plC_Button_通訊已建立.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_通訊已建立.Location = new System.Drawing.Point(321, 10);
            this.plC_Button_通訊已建立.Name = "plC_Button_通訊已建立";
            this.plC_Button_通訊已建立.OFF_文字內容 = "通訊已建立";
            this.plC_Button_通訊已建立.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_通訊已建立.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_通訊已建立.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_通訊已建立.ON_文字內容 = "通訊已建立";
            this.plC_Button_通訊已建立.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_通訊已建立.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_通訊已建立.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_通訊已建立.Size = new System.Drawing.Size(110, 93);
            this.plC_Button_通訊已建立.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_通訊已建立.TabIndex = 4;
            this.plC_Button_通訊已建立.字型鎖住 = false;
            this.plC_Button_通訊已建立.按鈕型態 = MyUI.PLC_Button.StatusEnum.交替型;
            this.plC_Button_通訊已建立.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_通訊已建立.文字鎖住 = false;
            this.plC_Button_通訊已建立.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_通訊已建立.狀態OFF圖片")));
            this.plC_Button_通訊已建立.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_通訊已建立.狀態ON圖片")));
            this.plC_Button_通訊已建立.讀寫鎖住 = false;
            this.plC_Button_通訊已建立.音效 = true;
            this.plC_Button_通訊已建立.顯示 = false;
            this.plC_Button_通訊已建立.顯示狀態 = false;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.button_參數設定_Adress);
            this.panel4.Controls.Add(this.comboBox_參數設定_Adress);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(7, 63);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(227, 39);
            this.panel4.TabIndex = 3;
            // 
            // button_參數設定_Adress
            // 
            this.button_參數設定_Adress.Location = new System.Drawing.Point(161, 5);
            this.button_參數設定_Adress.Name = "button_參數設定_Adress";
            this.button_參數設定_Adress.Size = new System.Drawing.Size(55, 23);
            this.button_參數設定_Adress.TabIndex = 2;
            this.button_參數設定_Adress.Text = "設定";
            this.button_參數設定_Adress.UseVisualStyleBackColor = true;
            this.button_參數設定_Adress.Click += new System.EventHandler(this.button_參數設定_Adress_Click);
            // 
            // comboBox_參數設定_Adress
            // 
            this.comboBox_參數設定_Adress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_參數設定_Adress.FormattingEnabled = true;
            this.comboBox_參數設定_Adress.Location = new System.Drawing.Point(66, 7);
            this.comboBox_參數設定_Adress.Name = "comboBox_參數設定_Adress";
            this.comboBox_參數設定_Adress.Size = new System.Drawing.Size(56, 20);
            this.comboBox_參數設定_Adress.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Adress :";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.button_參數設定_Baudrate);
            this.panel3.Controls.Add(this.comboBox_參數設定_Baudrate);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(7, 21);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(227, 39);
            this.panel3.TabIndex = 2;
            // 
            // button_參數設定_Baudrate
            // 
            this.button_參數設定_Baudrate.Location = new System.Drawing.Point(161, 6);
            this.button_參數設定_Baudrate.Name = "button_參數設定_Baudrate";
            this.button_參數設定_Baudrate.Size = new System.Drawing.Size(55, 23);
            this.button_參數設定_Baudrate.TabIndex = 2;
            this.button_參數設定_Baudrate.Text = "設定";
            this.button_參數設定_Baudrate.UseVisualStyleBackColor = true;
            this.button_參數設定_Baudrate.Click += new System.EventHandler(this.button_參數設定_Baudrate_Click);
            // 
            // comboBox_參數設定_Baudrate
            // 
            this.comboBox_參數設定_Baudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_參數設定_Baudrate.FormattingEnabled = true;
            this.comboBox_參數設定_Baudrate.Location = new System.Drawing.Point(66, 7);
            this.comboBox_參數設定_Baudrate.Name = "comboBox_參數設定_Baudrate";
            this.comboBox_參數設定_Baudrate.Size = new System.Drawing.Size(94, 20);
            this.comboBox_參數設定_Baudrate.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Baudrate :";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.label13, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label11, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label9, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.plC_NumBox_CH03_Value, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.plC_NumBox_CH02_Value, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.plC_NumBox_CH01_Value, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.plC_NumBox_CH07_Value, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.plC_NumBox_CH06_Value, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.plC_NumBox_CH05_Value, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.plC_NumBox_CH04_Value, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.plC_NumBox_CH00_Value, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 161);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(435, 126);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Moccasin;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(4, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 30);
            this.label6.TabIndex = 3;
            this.label6.Text = "CH00:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plC_NumBox_CH00_Value
            // 
            this.plC_NumBox_CH00_Value.Location = new System.Drawing.Point(112, 4);
            this.plC_NumBox_CH00_Value.Name = "plC_NumBox_CH00_Value";
            this.plC_NumBox_CH00_Value.ReadOnly = false;
            this.plC_NumBox_CH00_Value.Size = new System.Drawing.Size(101, 22);
            this.plC_NumBox_CH00_Value.TabIndex = 4;
            this.plC_NumBox_CH00_Value.Text = "0";
            this.plC_NumBox_CH00_Value.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_CH00_Value.密碼欄位 = false;
            this.plC_NumBox_CH00_Value.小數點位置 = 0;
            this.plC_NumBox_CH00_Value.音效 = true;
            this.plC_NumBox_CH00_Value.顯示螢幕小鍵盤 = true;
            // 
            // plC_NumBox_CH04_Value
            // 
            this.plC_NumBox_CH04_Value.Location = new System.Drawing.Point(328, 4);
            this.plC_NumBox_CH04_Value.Name = "plC_NumBox_CH04_Value";
            this.plC_NumBox_CH04_Value.ReadOnly = false;
            this.plC_NumBox_CH04_Value.Size = new System.Drawing.Size(101, 22);
            this.plC_NumBox_CH04_Value.TabIndex = 5;
            this.plC_NumBox_CH04_Value.Text = "0";
            this.plC_NumBox_CH04_Value.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_CH04_Value.密碼欄位 = false;
            this.plC_NumBox_CH04_Value.小數點位置 = 0;
            this.plC_NumBox_CH04_Value.音效 = true;
            this.plC_NumBox_CH04_Value.顯示螢幕小鍵盤 = true;
            // 
            // plC_NumBox_CH05_Value
            // 
            this.plC_NumBox_CH05_Value.Location = new System.Drawing.Point(328, 35);
            this.plC_NumBox_CH05_Value.Name = "plC_NumBox_CH05_Value";
            this.plC_NumBox_CH05_Value.ReadOnly = false;
            this.plC_NumBox_CH05_Value.Size = new System.Drawing.Size(101, 22);
            this.plC_NumBox_CH05_Value.TabIndex = 5;
            this.plC_NumBox_CH05_Value.Text = "0";
            this.plC_NumBox_CH05_Value.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_CH05_Value.密碼欄位 = false;
            this.plC_NumBox_CH05_Value.小數點位置 = 0;
            this.plC_NumBox_CH05_Value.音效 = true;
            this.plC_NumBox_CH05_Value.顯示螢幕小鍵盤 = true;
            // 
            // plC_NumBox_CH06_Value
            // 
            this.plC_NumBox_CH06_Value.Location = new System.Drawing.Point(328, 66);
            this.plC_NumBox_CH06_Value.Name = "plC_NumBox_CH06_Value";
            this.plC_NumBox_CH06_Value.ReadOnly = false;
            this.plC_NumBox_CH06_Value.Size = new System.Drawing.Size(101, 22);
            this.plC_NumBox_CH06_Value.TabIndex = 5;
            this.plC_NumBox_CH06_Value.Text = "0";
            this.plC_NumBox_CH06_Value.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_CH06_Value.密碼欄位 = false;
            this.plC_NumBox_CH06_Value.小數點位置 = 0;
            this.plC_NumBox_CH06_Value.音效 = true;
            this.plC_NumBox_CH06_Value.顯示螢幕小鍵盤 = true;
            // 
            // plC_NumBox_CH07_Value
            // 
            this.plC_NumBox_CH07_Value.Location = new System.Drawing.Point(328, 97);
            this.plC_NumBox_CH07_Value.Name = "plC_NumBox_CH07_Value";
            this.plC_NumBox_CH07_Value.ReadOnly = false;
            this.plC_NumBox_CH07_Value.Size = new System.Drawing.Size(101, 22);
            this.plC_NumBox_CH07_Value.TabIndex = 5;
            this.plC_NumBox_CH07_Value.Text = "0";
            this.plC_NumBox_CH07_Value.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_CH07_Value.密碼欄位 = false;
            this.plC_NumBox_CH07_Value.小數點位置 = 0;
            this.plC_NumBox_CH07_Value.音效 = true;
            this.plC_NumBox_CH07_Value.顯示螢幕小鍵盤 = true;
            // 
            // plC_NumBox_CH01_Value
            // 
            this.plC_NumBox_CH01_Value.Location = new System.Drawing.Point(112, 35);
            this.plC_NumBox_CH01_Value.Name = "plC_NumBox_CH01_Value";
            this.plC_NumBox_CH01_Value.ReadOnly = false;
            this.plC_NumBox_CH01_Value.Size = new System.Drawing.Size(101, 22);
            this.plC_NumBox_CH01_Value.TabIndex = 5;
            this.plC_NumBox_CH01_Value.Text = "0";
            this.plC_NumBox_CH01_Value.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_CH01_Value.密碼欄位 = false;
            this.plC_NumBox_CH01_Value.小數點位置 = 0;
            this.plC_NumBox_CH01_Value.音效 = true;
            this.plC_NumBox_CH01_Value.顯示螢幕小鍵盤 = true;
            // 
            // plC_NumBox_CH02_Value
            // 
            this.plC_NumBox_CH02_Value.Location = new System.Drawing.Point(112, 66);
            this.plC_NumBox_CH02_Value.Name = "plC_NumBox_CH02_Value";
            this.plC_NumBox_CH02_Value.ReadOnly = false;
            this.plC_NumBox_CH02_Value.Size = new System.Drawing.Size(101, 22);
            this.plC_NumBox_CH02_Value.TabIndex = 5;
            this.plC_NumBox_CH02_Value.Text = "0";
            this.plC_NumBox_CH02_Value.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_CH02_Value.密碼欄位 = false;
            this.plC_NumBox_CH02_Value.小數點位置 = 0;
            this.plC_NumBox_CH02_Value.音效 = true;
            this.plC_NumBox_CH02_Value.顯示螢幕小鍵盤 = true;
            // 
            // plC_NumBox_CH03_Value
            // 
            this.plC_NumBox_CH03_Value.Location = new System.Drawing.Point(112, 97);
            this.plC_NumBox_CH03_Value.Name = "plC_NumBox_CH03_Value";
            this.plC_NumBox_CH03_Value.ReadOnly = false;
            this.plC_NumBox_CH03_Value.Size = new System.Drawing.Size(101, 22);
            this.plC_NumBox_CH03_Value.TabIndex = 5;
            this.plC_NumBox_CH03_Value.Text = "0";
            this.plC_NumBox_CH03_Value.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_CH03_Value.密碼欄位 = false;
            this.plC_NumBox_CH03_Value.小數點位置 = 0;
            this.plC_NumBox_CH03_Value.音效 = true;
            this.plC_NumBox_CH03_Value.顯示螢幕小鍵盤 = true;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Moccasin;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(220, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 30);
            this.label7.TabIndex = 6;
            this.label7.Text = "CH04:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Moccasin;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(4, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 30);
            this.label8.TabIndex = 7;
            this.label8.Text = "CH01:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Moccasin;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(220, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 30);
            this.label9.TabIndex = 8;
            this.label9.Text = "CH05:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Moccasin;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(4, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 30);
            this.label10.TabIndex = 9;
            this.label10.Text = "CH02:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Moccasin;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(220, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 30);
            this.label11.TabIndex = 10;
            this.label11.Text = "CH06:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Moccasin;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(4, 94);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 31);
            this.label12.TabIndex = 11;
            this.label12.Text = "CH03:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Moccasin;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(220, 94);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 31);
            this.label13.TabIndex = 12;
            this.label13.Text = "CH07:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plC_Button_更新數值完成
            // 
            this.plC_Button_更新數值完成.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_更新數值完成.Location = new System.Drawing.Point(240, 13);
            this.plC_Button_更新數值完成.Name = "plC_Button_更新數值完成";
            this.plC_Button_更新數值完成.OFF_文字內容 = "更新數值完成";
            this.plC_Button_更新數值完成.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_更新數值完成.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_更新數值完成.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_更新數值完成.ON_文字內容 = "更新數值完成";
            this.plC_Button_更新數值完成.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_更新數值完成.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_更新數值完成.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_更新數值完成.Size = new System.Drawing.Size(75, 90);
            this.plC_Button_更新數值完成.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_更新數值完成.TabIndex = 5;
            this.plC_Button_更新數值完成.字型鎖住 = false;
            this.plC_Button_更新數值完成.按鈕型態 = MyUI.PLC_Button.StatusEnum.交替型;
            this.plC_Button_更新數值完成.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_更新數值完成.文字鎖住 = false;
            this.plC_Button_更新數值完成.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_更新數值完成.狀態OFF圖片")));
            this.plC_Button_更新數值完成.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_更新數值完成.狀態ON圖片")));
            this.plC_Button_更新數值完成.讀寫鎖住 = false;
            this.plC_Button_更新數值完成.音效 = true;
            this.plC_Button_更新數值完成.顯示 = false;
            this.plC_Button_更新數值完成.顯示狀態 = false;
            // 
            // DAQM_4206A
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "DAQM_4206A";
            this.Size = new System.Drawing.Size(435, 287);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox_COM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_Baudrate;
        private System.Windows.Forms.Panel panel2;
        private MyUI.PLC_Button plC_Button_通訊測試;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button_參數設定_Baudrate;
        private System.Windows.Forms.ComboBox comboBox_參數設定_Baudrate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button_參數設定_Adress;
        private System.Windows.Forms.ComboBox comboBox_參數設定_Adress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_Adress;
        private MyUI.PLC_Button plC_Button_通訊已建立;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private MyUI.PLC_NumBox plC_NumBox_CH03_Value;
        private MyUI.PLC_NumBox plC_NumBox_CH02_Value;
        private MyUI.PLC_NumBox plC_NumBox_CH01_Value;
        private MyUI.PLC_NumBox plC_NumBox_CH07_Value;
        private MyUI.PLC_NumBox plC_NumBox_CH06_Value;
        private MyUI.PLC_NumBox plC_NumBox_CH05_Value;
        private MyUI.PLC_NumBox plC_NumBox_CH04_Value;
        private System.Windows.Forms.Label label6;
        private MyUI.PLC_NumBox plC_NumBox_CH00_Value;
        private MyUI.PLC_Button plC_Button_更新數值完成;
    }
}
