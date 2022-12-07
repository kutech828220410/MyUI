namespace HSONApplication
{
    partial class Form1
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog_LoadExcel = new System.Windows.Forms.OpenFileDialog();
            this.plC_ScreenPage1 = new MyUI.PLC_ScreenPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.plC_NumBox1 = new MyUI.PLC_NumBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.sqL_DataGridView_file = new SQLUI.SQL_DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.rJ_TrackBar1 = new MyUI.RJ_TrackBar();
            this.rJ_Button2 = new MyUI.RJ_Button();
            this.rJ_Button1 = new MyUI.RJ_Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.plC_RJ_ScreenButton1 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton2 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_UI_Init1 = new MyUI.PLC_UI_Init();
            this.lowerMachine_Panel1 = new LadderUI.LowerMachine_Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lScrollBar1 = new DemoControls.LScrollBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.plC_RJ_ScreenButton_Main = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton_System = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton3 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton4 = new MyUI.PLC_RJ_ScreenButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.plC_ScreenPage = new MyUI.PLC_ScreenPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.plC_AlarmFlow1 = new MyUI.PLC_AlarmFlow();
            this.plC_AlarmFlow2 = new MyUI.PLC_AlarmFlow();
            this.plC_Button1 = new MyUI.PLC_Button();
            this.plC_ScreenPage1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.plC_ScreenPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog_LoadExcel
            // 
            this.openFileDialog_LoadExcel.DefaultExt = "txt";
            this.openFileDialog_LoadExcel.Filter = "txt File (*.txt)|*.txt;";
            // 
            // plC_ScreenPage1
            // 
            this.plC_ScreenPage1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.plC_ScreenPage1.BackColor = System.Drawing.Color.White;
            this.plC_ScreenPage1.Controls.Add(this.tabPage1);
            this.plC_ScreenPage1.Controls.Add(this.tabPage2);
            this.plC_ScreenPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_ScreenPage1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.plC_ScreenPage1.ForekColor = System.Drawing.Color.Black;
            this.plC_ScreenPage1.ItemSize = new System.Drawing.Size(54, 21);
            this.plC_ScreenPage1.Location = new System.Drawing.Point(0, 0);
            this.plC_ScreenPage1.Margin = new System.Windows.Forms.Padding(0);
            this.plC_ScreenPage1.Name = "plC_ScreenPage1";
            this.plC_ScreenPage1.Padding = new System.Drawing.Point(0, 0);
            this.plC_ScreenPage1.SelectedIndex = 0;
            this.plC_ScreenPage1.ShowToolTips = true;
            this.plC_ScreenPage1.Size = new System.Drawing.Size(1658, 1016);
            this.plC_ScreenPage1.TabBackColor = System.Drawing.Color.White;
            this.plC_ScreenPage1.TabIndex = 1;
            this.plC_ScreenPage1.顯示標籤列 = MyUI.PLC_ScreenPage.TabVisibleEnum.顯示;
            this.plC_ScreenPage1.顯示頁面 = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackColor = System.Drawing.SystemColors.Window;
            this.tabPage1.Controls.Add(this.plC_Button1);
            this.tabPage1.Controls.Add(this.plC_AlarmFlow2);
            this.tabPage1.Controls.Add(this.plC_NumBox1);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.sqL_DataGridView_file);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.rJ_TrackBar1);
            this.tabPage1.Controls.Add(this.rJ_Button2);
            this.tabPage1.Controls.Add(this.rJ_Button1);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.plC_RJ_ScreenButton1);
            this.tabPage1.Controls.Add(this.plC_RJ_ScreenButton2);
            this.tabPage1.Controls.Add(this.plC_UI_Init1);
            this.tabPage1.Controls.Add(this.lowerMachine_Panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1650, 987);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "主畫面";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // plC_NumBox1
            // 
            this.plC_NumBox1.Location = new System.Drawing.Point(1099, 267);
            this.plC_NumBox1.mBackColor = System.Drawing.SystemColors.Window;
            this.plC_NumBox1.mForeColor = System.Drawing.SystemColors.WindowText;
            this.plC_NumBox1.Name = "plC_NumBox1";
            this.plC_NumBox1.ReadOnly = false;
            this.plC_NumBox1.Size = new System.Drawing.Size(112, 22);
            this.plC_NumBox1.TabIndex = 52;
            this.plC_NumBox1.Value = 0;
            this.plC_NumBox1.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox1.密碼欄位 = false;
            this.plC_NumBox1.小數點位置 = 0;
            this.plC_NumBox1.微調數值 = 1;
            this.plC_NumBox1.音效 = true;
            this.plC_NumBox1.顯示微調按鈕 = false;
            this.plC_NumBox1.顯示螢幕小鍵盤 = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1089, 190);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 51;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(984, 626);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 50;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // sqL_DataGridView_file
            // 
            this.sqL_DataGridView_file.AutoSelectToDeep = true;
            this.sqL_DataGridView_file.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_file.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_file.BorderRadius = 10;
            this.sqL_DataGridView_file.BorderSize = 2;
            this.sqL_DataGridView_file.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_file.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_file.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_file.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_file.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_file.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_file.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_file.columnHeadersHeight = 26;
            this.sqL_DataGridView_file.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_file.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_file.Columns"))));
            this.sqL_DataGridView_file.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_file.Columns1"))));
            this.sqL_DataGridView_file.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_file.Columns2"))));
            this.sqL_DataGridView_file.DataBaseName = "test";
            this.sqL_DataGridView_file.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_file.ImageBox = false;
            this.sqL_DataGridView_file.Location = new System.Drawing.Point(195, 6);
            this.sqL_DataGridView_file.Name = "sqL_DataGridView_file";
            this.sqL_DataGridView_file.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_file.Password = "user82822040";
            this.sqL_DataGridView_file.Port = ((uint)(3306u));
            this.sqL_DataGridView_file.rowHeaderBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_file.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_file.RowsColor = System.Drawing.SystemColors.Control;
            this.sqL_DataGridView_file.RowsHeight = 10;
            this.sqL_DataGridView_file.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_file.Server = "127.0.0.0";
            this.sqL_DataGridView_file.Size = new System.Drawing.Size(805, 585);
            this.sqL_DataGridView_file.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_file.TabIndex = 49;
            this.sqL_DataGridView_file.TableName = "file";
            this.sqL_DataGridView_file.UserName = "root";
            this.sqL_DataGridView_file.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_file.可選擇多列 = false;
            this.sqL_DataGridView_file.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_file.自動換行 = true;
            this.sqL_DataGridView_file.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_file.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_file.顯示CheckBox = false;
            this.sqL_DataGridView_file.顯示首列 = true;
            this.sqL_DataGridView_file.顯示首行 = true;
            this.sqL_DataGridView_file.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_file.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(984, 597);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 48;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rJ_TrackBar1
            // 
            this.rJ_TrackBar1.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.rJ_TrackBar1.BarFont = new System.Drawing.Font("微軟正黑體", 9F);
            this.rJ_TrackBar1.BarForeColor = System.Drawing.Color.DarkGray;
            this.rJ_TrackBar1.BarSize = 50;
            this.rJ_TrackBar1.BottomSliderColor = System.Drawing.Color.Red;
            this.rJ_TrackBar1.Location = new System.Drawing.Point(312, 278);
            this.rJ_TrackBar1.Maximum = 100;
            this.rJ_TrackBar1.MaxValue = 25;
            this.rJ_TrackBar1.Minimum = 20;
            this.rJ_TrackBar1.MinValue = 20;
            this.rJ_TrackBar1.Name = "rJ_TrackBar1";
            this.rJ_TrackBar1.Size = new System.Drawing.Size(1158, 90);
            this.rJ_TrackBar1.SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.rJ_TrackBar1.SliderSize = 20;
            this.rJ_TrackBar1.TabIndex = 47;
            this.rJ_TrackBar1.Text = "rJ_TrackBar1";
            this.rJ_TrackBar1.TopSliderColor = System.Drawing.Color.Red;
            // 
            // rJ_Button2
            // 
            this.rJ_Button2.AutoResetState = false;
            this.rJ_Button2.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_Button2.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_Button2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button2.BorderRadius = 40;
            this.rJ_Button2.BorderSize = 0;
            this.rJ_Button2.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button2.FlatAppearance.BorderSize = 0;
            this.rJ_Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button2.ForeColor = System.Drawing.Color.White;
            this.rJ_Button2.Location = new System.Drawing.Point(98, 479);
            this.rJ_Button2.Name = "rJ_Button2";
            this.rJ_Button2.Size = new System.Drawing.Size(150, 40);
            this.rJ_Button2.State = false;
            this.rJ_Button2.TabIndex = 46;
            this.rJ_Button2.Text = "rJ_Button2";
            this.rJ_Button2.TextColor = System.Drawing.Color.White;
            this.rJ_Button2.UseVisualStyleBackColor = false;
            this.rJ_Button2.Click += new System.EventHandler(this.rJ_Button2_Click);
            // 
            // rJ_Button1
            // 
            this.rJ_Button1.AutoResetState = false;
            this.rJ_Button1.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_Button1.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_Button1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button1.BorderRadius = 40;
            this.rJ_Button1.BorderSize = 0;
            this.rJ_Button1.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button1.FlatAppearance.BorderSize = 0;
            this.rJ_Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button1.ForeColor = System.Drawing.Color.White;
            this.rJ_Button1.Location = new System.Drawing.Point(98, 433);
            this.rJ_Button1.Name = "rJ_Button1";
            this.rJ_Button1.Size = new System.Drawing.Size(150, 40);
            this.rJ_Button1.State = false;
            this.rJ_Button1.TabIndex = 45;
            this.rJ_Button1.Text = "rJ_Button1";
            this.rJ_Button1.TextColor = System.Drawing.Color.White;
            this.rJ_Button1.UseVisualStyleBackColor = false;
            this.rJ_Button1.Click += new System.EventHandler(this.rJ_Button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(883, 33);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 31;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // plC_RJ_ScreenButton1
            // 
            this.plC_RJ_ScreenButton1.but_press = false;
            this.plC_RJ_ScreenButton1.IconChar = FontAwesome.Sharp.IconChar.Cubes;
            this.plC_RJ_ScreenButton1.IconSize = 32;
            this.plC_RJ_ScreenButton1.Location = new System.Drawing.Point(1452, 74);
            this.plC_RJ_ScreenButton1.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton1.Name = "plC_RJ_ScreenButton1";
            this.plC_RJ_ScreenButton1.OffBackColor = System.Drawing.Color.SkyBlue;
            this.plC_RJ_ScreenButton1.OffFont = new System.Drawing.Font("新細明體", 12F);
            this.plC_RJ_ScreenButton1.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton1.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton1.OffText = "手動";
            this.plC_RJ_ScreenButton1.OnBackColor = System.Drawing.Color.LightBlue;
            this.plC_RJ_ScreenButton1.OnFont = new System.Drawing.Font("新細明體", 12F);
            this.plC_RJ_ScreenButton1.OnForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton1.OnIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton1.OnText = "手動";
            this.plC_RJ_ScreenButton1.ShowIcon = true;
            this.plC_RJ_ScreenButton1.Size = new System.Drawing.Size(193, 68);
            this.plC_RJ_ScreenButton1.TabIndex = 30;
            this.plC_RJ_ScreenButton1.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton1.寫入位置註解 = "";
            this.plC_RJ_ScreenButton1.寫入元件位置 = "";
            this.plC_RJ_ScreenButton1.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton1.控制位址 = "D0";
            this.plC_RJ_ScreenButton1.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton1.致能讀取位置 = "";
            this.plC_RJ_ScreenButton1.讀取位元反向 = false;
            this.plC_RJ_ScreenButton1.讀取位置註解 = "";
            this.plC_RJ_ScreenButton1.讀取元件位置 = "";
            this.plC_RJ_ScreenButton1.音效 = true;
            this.plC_RJ_ScreenButton1.頁面名稱 = "手動";
            this.plC_RJ_ScreenButton1.頁面編號 = 0;
            this.plC_RJ_ScreenButton1.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton1.顯示狀態 = false;
            this.plC_RJ_ScreenButton1.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton2
            // 
            this.plC_RJ_ScreenButton2.but_press = false;
            this.plC_RJ_ScreenButton2.IconChar = FontAwesome.Sharp.IconChar.Cube;
            this.plC_RJ_ScreenButton2.IconSize = 32;
            this.plC_RJ_ScreenButton2.Location = new System.Drawing.Point(1452, 6);
            this.plC_RJ_ScreenButton2.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton2.Name = "plC_RJ_ScreenButton2";
            this.plC_RJ_ScreenButton2.OffBackColor = System.Drawing.Color.SkyBlue;
            this.plC_RJ_ScreenButton2.OffFont = new System.Drawing.Font("新細明體", 12F);
            this.plC_RJ_ScreenButton2.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton2.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton2.OffText = "主畫面";
            this.plC_RJ_ScreenButton2.OnBackColor = System.Drawing.Color.LightBlue;
            this.plC_RJ_ScreenButton2.OnFont = new System.Drawing.Font("新細明體", 12F);
            this.plC_RJ_ScreenButton2.OnForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton2.OnIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton2.OnText = "主畫面";
            this.plC_RJ_ScreenButton2.ShowIcon = true;
            this.plC_RJ_ScreenButton2.Size = new System.Drawing.Size(193, 68);
            this.plC_RJ_ScreenButton2.TabIndex = 29;
            this.plC_RJ_ScreenButton2.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton2.寫入位置註解 = "";
            this.plC_RJ_ScreenButton2.寫入元件位置 = "";
            this.plC_RJ_ScreenButton2.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton2.控制位址 = "D0";
            this.plC_RJ_ScreenButton2.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton2.致能讀取位置 = "";
            this.plC_RJ_ScreenButton2.讀取位元反向 = false;
            this.plC_RJ_ScreenButton2.讀取位置註解 = "";
            this.plC_RJ_ScreenButton2.讀取元件位置 = "";
            this.plC_RJ_ScreenButton2.音效 = true;
            this.plC_RJ_ScreenButton2.頁面名稱 = "主畫面";
            this.plC_RJ_ScreenButton2.頁面編號 = 0;
            this.plC_RJ_ScreenButton2.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton2.顯示狀態 = false;
            this.plC_RJ_ScreenButton2.顯示讀取位置 = "";
            // 
            // plC_UI_Init1
            // 
            this.plC_UI_Init1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.plC_UI_Init1.Location = new System.Drawing.Point(883, 16);
            this.plC_UI_Init1.Name = "plC_UI_Init1";
            this.plC_UI_Init1.Size = new System.Drawing.Size(72, 25);
            this.plC_UI_Init1.TabIndex = 20;
            this.plC_UI_Init1.光道視覺元件初始化 = true;
            this.plC_UI_Init1.全螢幕顯示 = false;
            this.plC_UI_Init1.掃描速度 = 1;
            this.plC_UI_Init1.起始畫面標題內容 = "鴻森整合機電有限公司";
            this.plC_UI_Init1.起始畫面標題字體 = new System.Drawing.Font("標楷體", 20F, System.Drawing.FontStyle.Bold);
            this.plC_UI_Init1.起始畫面背景 = ((System.Drawing.Image)(resources.GetObject("plC_UI_Init1.起始畫面背景")));
            this.plC_UI_Init1.起始畫面顯示 = false;
            this.plC_UI_Init1.邁得威視元件初始化 = false;
            this.plC_UI_Init1.開機延遲 = 0;
            this.plC_UI_Init1.音效 = false;
            // 
            // lowerMachine_Panel1
            // 
            this.lowerMachine_Panel1.Location = new System.Drawing.Point(8, 6);
            this.lowerMachine_Panel1.Name = "lowerMachine_Panel1";
            this.lowerMachine_Panel1.Size = new System.Drawing.Size(869, 572);
            this.lowerMachine_Panel1.TabIndex = 0;
            this.lowerMachine_Panel1.掃描速度 = 0;
            this.lowerMachine_Panel1.Load += new System.EventHandler(this.lowerMachine_Panel1_Load);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Window;
            this.tabPage2.Controls.Add(this.lScrollBar1);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.plC_RJ_ScreenButton3);
            this.tabPage2.Controls.Add(this.plC_RJ_ScreenButton4);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1650, 987);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "手動";
            // 
            // lScrollBar1
            // 
            this.lScrollBar1.L_BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lScrollBar1.L_BarSize = 20;
            this.lScrollBar1.L_DocSize = 100F;
            this.lScrollBar1.L_IsRound = true;
            this.lScrollBar1.L_Orientation = DemoControls.OrientationScrollBar.Vertical;
            this.lScrollBar1.L_PageSize = 10F;
            this.lScrollBar1.L_ScrollInterval = 10F;
            this.lScrollBar1.L_ShowPosition = -9F;
            this.lScrollBar1.L_SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lScrollBar1.L_SliderMiniSize = 20F;
            this.lScrollBar1.L_SliderMouseInColor = System.Drawing.Color.Green;
            this.lScrollBar1.Location = new System.Drawing.Point(1155, 141);
            this.lScrollBar1.Name = "lScrollBar1";
            this.lScrollBar1.Size = new System.Drawing.Size(20, 310);
            this.lScrollBar1.TabIndex = 34;
            this.lScrollBar1.Text = "lScrollBar1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.plC_RJ_ScreenButton_Main);
            this.panel2.Controls.Add(this.plC_RJ_ScreenButton_System);
            this.panel2.Location = new System.Drawing.Point(524, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(248, 144);
            this.panel2.TabIndex = 33;
            // 
            // plC_RJ_ScreenButton_Main
            // 
            this.plC_RJ_ScreenButton_Main.but_press = false;
            this.plC_RJ_ScreenButton_Main.IconChar = FontAwesome.Sharp.IconChar.Cube;
            this.plC_RJ_ScreenButton_Main.IconSize = 32;
            this.plC_RJ_ScreenButton_Main.Location = new System.Drawing.Point(0, 0);
            this.plC_RJ_ScreenButton_Main.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton_Main.Name = "plC_RJ_ScreenButton_Main";
            this.plC_RJ_ScreenButton_Main.OffBackColor = System.Drawing.Color.SkyBlue;
            this.plC_RJ_ScreenButton_Main.OffFont = new System.Drawing.Font("新細明體", 12F);
            this.plC_RJ_ScreenButton_Main.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton_Main.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton_Main.OffText = "Main";
            this.plC_RJ_ScreenButton_Main.OnBackColor = System.Drawing.Color.LightBlue;
            this.plC_RJ_ScreenButton_Main.OnFont = new System.Drawing.Font("新細明體", 12F);
            this.plC_RJ_ScreenButton_Main.OnForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton_Main.OnIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton_Main.OnText = "Main";
            this.plC_RJ_ScreenButton_Main.ShowIcon = true;
            this.plC_RJ_ScreenButton_Main.Size = new System.Drawing.Size(193, 68);
            this.plC_RJ_ScreenButton_Main.TabIndex = 27;
            this.plC_RJ_ScreenButton_Main.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton_Main.寫入位置註解 = "";
            this.plC_RJ_ScreenButton_Main.寫入元件位置 = "";
            this.plC_RJ_ScreenButton_Main.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton_Main.控制位址 = "D0";
            this.plC_RJ_ScreenButton_Main.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton_Main.致能讀取位置 = "";
            this.plC_RJ_ScreenButton_Main.讀取位元反向 = false;
            this.plC_RJ_ScreenButton_Main.讀取位置註解 = "";
            this.plC_RJ_ScreenButton_Main.讀取元件位置 = "";
            this.plC_RJ_ScreenButton_Main.音效 = true;
            this.plC_RJ_ScreenButton_Main.頁面名稱 = "Main";
            this.plC_RJ_ScreenButton_Main.頁面編號 = 0;
            this.plC_RJ_ScreenButton_Main.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton_Main.顯示狀態 = false;
            this.plC_RJ_ScreenButton_Main.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton_System
            // 
            this.plC_RJ_ScreenButton_System.but_press = false;
            this.plC_RJ_ScreenButton_System.IconChar = FontAwesome.Sharp.IconChar.Cubes;
            this.plC_RJ_ScreenButton_System.IconSize = 32;
            this.plC_RJ_ScreenButton_System.Location = new System.Drawing.Point(0, 76);
            this.plC_RJ_ScreenButton_System.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton_System.Name = "plC_RJ_ScreenButton_System";
            this.plC_RJ_ScreenButton_System.OffBackColor = System.Drawing.Color.SkyBlue;
            this.plC_RJ_ScreenButton_System.OffFont = new System.Drawing.Font("新細明體", 12F);
            this.plC_RJ_ScreenButton_System.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton_System.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton_System.OffText = "System";
            this.plC_RJ_ScreenButton_System.OnBackColor = System.Drawing.Color.LightBlue;
            this.plC_RJ_ScreenButton_System.OnFont = new System.Drawing.Font("新細明體", 12F);
            this.plC_RJ_ScreenButton_System.OnForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton_System.OnIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton_System.OnText = "System";
            this.plC_RJ_ScreenButton_System.ShowIcon = true;
            this.plC_RJ_ScreenButton_System.Size = new System.Drawing.Size(193, 68);
            this.plC_RJ_ScreenButton_System.TabIndex = 28;
            this.plC_RJ_ScreenButton_System.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton_System.寫入位置註解 = "";
            this.plC_RJ_ScreenButton_System.寫入元件位置 = "";
            this.plC_RJ_ScreenButton_System.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton_System.控制位址 = "D0";
            this.plC_RJ_ScreenButton_System.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton_System.致能讀取位置 = "";
            this.plC_RJ_ScreenButton_System.讀取位元反向 = false;
            this.plC_RJ_ScreenButton_System.讀取位置註解 = "";
            this.plC_RJ_ScreenButton_System.讀取元件位置 = "";
            this.plC_RJ_ScreenButton_System.音效 = true;
            this.plC_RJ_ScreenButton_System.頁面名稱 = "System";
            this.plC_RJ_ScreenButton_System.頁面編號 = 0;
            this.plC_RJ_ScreenButton_System.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton_System.顯示狀態 = false;
            this.plC_RJ_ScreenButton_System.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton3
            // 
            this.plC_RJ_ScreenButton3.but_press = false;
            this.plC_RJ_ScreenButton3.IconChar = FontAwesome.Sharp.IconChar.Cubes;
            this.plC_RJ_ScreenButton3.IconSize = 32;
            this.plC_RJ_ScreenButton3.Location = new System.Drawing.Point(852, 493);
            this.plC_RJ_ScreenButton3.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton3.Name = "plC_RJ_ScreenButton3";
            this.plC_RJ_ScreenButton3.OffBackColor = System.Drawing.Color.SkyBlue;
            this.plC_RJ_ScreenButton3.OffFont = new System.Drawing.Font("新細明體", 12F);
            this.plC_RJ_ScreenButton3.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton3.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton3.OffText = "手動";
            this.plC_RJ_ScreenButton3.OnBackColor = System.Drawing.Color.LightBlue;
            this.plC_RJ_ScreenButton3.OnFont = new System.Drawing.Font("新細明體", 12F);
            this.plC_RJ_ScreenButton3.OnForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton3.OnIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton3.OnText = "手動";
            this.plC_RJ_ScreenButton3.ShowIcon = true;
            this.plC_RJ_ScreenButton3.Size = new System.Drawing.Size(193, 68);
            this.plC_RJ_ScreenButton3.TabIndex = 32;
            this.plC_RJ_ScreenButton3.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton3.寫入位置註解 = "";
            this.plC_RJ_ScreenButton3.寫入元件位置 = "";
            this.plC_RJ_ScreenButton3.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton3.控制位址 = "D0";
            this.plC_RJ_ScreenButton3.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton3.致能讀取位置 = "";
            this.plC_RJ_ScreenButton3.讀取位元反向 = false;
            this.plC_RJ_ScreenButton3.讀取位置註解 = "";
            this.plC_RJ_ScreenButton3.讀取元件位置 = "";
            this.plC_RJ_ScreenButton3.音效 = true;
            this.plC_RJ_ScreenButton3.頁面名稱 = "手動";
            this.plC_RJ_ScreenButton3.頁面編號 = 0;
            this.plC_RJ_ScreenButton3.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton3.顯示狀態 = false;
            this.plC_RJ_ScreenButton3.顯示讀取位置 = "";
            // 
            // plC_RJ_ScreenButton4
            // 
            this.plC_RJ_ScreenButton4.but_press = false;
            this.plC_RJ_ScreenButton4.IconChar = FontAwesome.Sharp.IconChar.Cube;
            this.plC_RJ_ScreenButton4.IconSize = 32;
            this.plC_RJ_ScreenButton4.Location = new System.Drawing.Point(852, 425);
            this.plC_RJ_ScreenButton4.Margin = new System.Windows.Forms.Padding(0);
            this.plC_RJ_ScreenButton4.Name = "plC_RJ_ScreenButton4";
            this.plC_RJ_ScreenButton4.OffBackColor = System.Drawing.Color.SkyBlue;
            this.plC_RJ_ScreenButton4.OffFont = new System.Drawing.Font("新細明體", 12F);
            this.plC_RJ_ScreenButton4.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton4.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton4.OffText = "主畫面";
            this.plC_RJ_ScreenButton4.OnBackColor = System.Drawing.Color.LightBlue;
            this.plC_RJ_ScreenButton4.OnFont = new System.Drawing.Font("新細明體", 12F);
            this.plC_RJ_ScreenButton4.OnForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton4.OnIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButton4.OnText = "主畫面";
            this.plC_RJ_ScreenButton4.ShowIcon = true;
            this.plC_RJ_ScreenButton4.Size = new System.Drawing.Size(193, 68);
            this.plC_RJ_ScreenButton4.TabIndex = 31;
            this.plC_RJ_ScreenButton4.字元長度 = MyUI.PLC_RJ_ScreenButton.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButton4.寫入位置註解 = "";
            this.plC_RJ_ScreenButton4.寫入元件位置 = "";
            this.plC_RJ_ScreenButton4.按鈕型態 = MyUI.PLC_RJ_ScreenButton.StatusEnum.保持型;
            this.plC_RJ_ScreenButton4.控制位址 = "D0";
            this.plC_RJ_ScreenButton4.換頁選擇方式 = MyUI.PLC_RJ_ScreenButton.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButton4.致能讀取位置 = "";
            this.plC_RJ_ScreenButton4.讀取位元反向 = false;
            this.plC_RJ_ScreenButton4.讀取位置註解 = "";
            this.plC_RJ_ScreenButton4.讀取元件位置 = "";
            this.plC_RJ_ScreenButton4.音效 = true;
            this.plC_RJ_ScreenButton4.頁面名稱 = "主畫面";
            this.plC_RJ_ScreenButton4.頁面編號 = 0;
            this.plC_RJ_ScreenButton4.顯示方式 = MyUI.PLC_RJ_ScreenButton.StateEnum.顯示為OFF;
            this.plC_RJ_ScreenButton4.顯示狀態 = false;
            this.plC_RJ_ScreenButton4.顯示讀取位置 = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.plC_ScreenPage);
            this.panel1.Location = new System.Drawing.Point(5, 209);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(629, 368);
            this.panel1.TabIndex = 29;
            // 
            // plC_ScreenPage
            // 
            this.plC_ScreenPage.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.plC_ScreenPage.BackColor = System.Drawing.Color.White;
            this.plC_ScreenPage.Controls.Add(this.tabPage3);
            this.plC_ScreenPage.Controls.Add(this.tabPage4);
            this.plC_ScreenPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_ScreenPage.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.plC_ScreenPage.ForekColor = System.Drawing.Color.Black;
            this.plC_ScreenPage.ItemSize = new System.Drawing.Size(54, 21);
            this.plC_ScreenPage.Location = new System.Drawing.Point(0, 0);
            this.plC_ScreenPage.Name = "plC_ScreenPage";
            this.plC_ScreenPage.SelectedIndex = 0;
            this.plC_ScreenPage.ShowToolTips = true;
            this.plC_ScreenPage.Size = new System.Drawing.Size(629, 368);
            this.plC_ScreenPage.TabBackColor = System.Drawing.Color.DimGray;
            this.plC_ScreenPage.TabIndex = 21;
            this.plC_ScreenPage.顯示標籤列 = MyUI.PLC_ScreenPage.TabVisibleEnum.顯示;
            this.plC_ScreenPage.顯示頁面 = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Window;
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(621, 339);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Main";
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(621, 339);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "System";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // plC_AlarmFlow1
            // 
            this.plC_AlarmFlow1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plC_AlarmFlow1.Location = new System.Drawing.Point(0, 1016);
            this.plC_AlarmFlow1.Name = "plC_AlarmFlow1";
            this.plC_AlarmFlow1.Size = new System.Drawing.Size(1658, 26);
            this.plC_AlarmFlow1.TabIndex = 0;
            this.plC_AlarmFlow1.捲動速度 = 200;
            this.plC_AlarmFlow1.文字字體 = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_AlarmFlow1.文字顏色 = System.Drawing.Color.White;
            this.plC_AlarmFlow1.自動隱藏 = false;
            this.plC_AlarmFlow1.警報編輯 = ((System.Collections.Generic.List<string>)(resources.GetObject("plC_AlarmFlow1.警報編輯")));
            this.plC_AlarmFlow1.顯示警報編號 = true;
            // 
            // plC_AlarmFlow2
            // 
            this.plC_AlarmFlow2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plC_AlarmFlow2.Location = new System.Drawing.Point(0, 961);
            this.plC_AlarmFlow2.Name = "plC_AlarmFlow2";
            this.plC_AlarmFlow2.Size = new System.Drawing.Size(1650, 26);
            this.plC_AlarmFlow2.TabIndex = 54;
            this.plC_AlarmFlow2.捲動速度 = 200;
            this.plC_AlarmFlow2.文字字體 = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_AlarmFlow2.文字顏色 = System.Drawing.Color.White;
            this.plC_AlarmFlow2.自動隱藏 = false;
            this.plC_AlarmFlow2.警報編輯 = ((System.Collections.Generic.List<string>)(resources.GetObject("plC_AlarmFlow2.警報編輯")));
            this.plC_AlarmFlow2.顯示警報編號 = false;
            // 
            // plC_Button1
            // 
            this.plC_Button1.Bool = false;
            this.plC_Button1.but_press = false;
            this.plC_Button1.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button1.Location = new System.Drawing.Point(1244, 123);
            this.plC_Button1.Name = "plC_Button1";
            this.plC_Button1.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button1.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button1.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button1.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button1.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button1.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button1.Size = new System.Drawing.Size(141, 60);
            this.plC_Button1.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button1.TabIndex = 55;
            this.plC_Button1.事件驅動 = false;
            this.plC_Button1.字型鎖住 = false;
            this.plC_Button1.按鈕型態 = MyUI.PLC_Button.StatusEnum.交替型;
            this.plC_Button1.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button1.文字鎖住 = false;
            this.plC_Button1.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button1.狀態OFF圖片")));
            this.plC_Button1.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button1.狀態ON圖片")));
            this.plC_Button1.讀取位元反向 = false;
            this.plC_Button1.讀寫鎖住 = false;
            this.plC_Button1.音效 = true;
            this.plC_Button1.顯示 = false;
            this.plC_Button1.顯示狀態 = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1658, 1042);
            this.Controls.Add(this.plC_ScreenPage1);
            this.Controls.Add(this.plC_AlarmFlow1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.plC_ScreenPage1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.plC_ScreenPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyUI.PLC_ScreenPage plC_ScreenPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private MyUI.PLC_UI_Init plC_UI_Init1;
        private LadderUI.LowerMachine_Panel lowerMachine_Panel1;
        private MyUI.PLC_AlarmFlow plC_AlarmFlow1;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton1;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton2;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton3;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton4;
        private System.Windows.Forms.Panel panel1;
        private MyUI.PLC_ScreenPage plC_ScreenPage;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton_System;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton_Main;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.OpenFileDialog openFileDialog_LoadExcel;
        private DemoControls.LScrollBar lScrollBar1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewButtonColumn Column2;
        private MyUI.RJ_Button rJ_Button1;
        private MyUI.RJ_Button rJ_Button2;
        private MyUI.RJ_TrackBar rJ_TrackBar1;
        private System.Windows.Forms.Button button1;
        private SQLUI.SQL_DataGridView sqL_DataGridView_file;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private MyUI.PLC_NumBox plC_NumBox1;
        private MyUI.PLC_Button plC_Button1;
        private MyUI.PLC_AlarmFlow plC_AlarmFlow2;
    }
}

