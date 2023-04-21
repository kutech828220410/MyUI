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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plC_RJ_Button_顯示 = new MyUI.PLC_RJ_Button();
            this.sqL_DataGridView_人員資料 = new SQLUI.SQL_DataGridView();
            this.plC_AlarmFlow2 = new MyUI.PLC_AlarmFlow();
            this.plC_RJ_ScreenButton1 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton2 = new MyUI.PLC_RJ_ScreenButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.plC_UI_Init1 = new MyUI.PLC_UI_Init();
            this.lowerMachine_Panel1 = new LadderUI.LowerMachine_Panel();
            this.plC_RJ_ScreenButton3 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton4 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_ScreenPage1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
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
            this.plC_ScreenPage1.Size = new System.Drawing.Size(1658, 1042);
            this.plC_ScreenPage1.TabBackColor = System.Drawing.Color.White;
            this.plC_ScreenPage1.TabIndex = 1;
            this.plC_ScreenPage1.顯示標籤列 = MyUI.PLC_ScreenPage.TabVisibleEnum.顯示;
            this.plC_ScreenPage1.顯示頁面 = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackColor = System.Drawing.SystemColors.Window;
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.plC_RJ_Button_顯示);
            this.tabPage1.Controls.Add(this.sqL_DataGridView_人員資料);
            this.tabPage1.Controls.Add(this.plC_AlarmFlow2);
            this.tabPage1.Controls.Add(this.plC_RJ_ScreenButton1);
            this.tabPage1.Controls.Add(this.plC_RJ_ScreenButton2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1650, 1013);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "主畫面";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(576, 286);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 57;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // plC_RJ_Button_顯示
            // 
            this.plC_RJ_Button_顯示.AutoResetState = false;
            this.plC_RJ_Button_顯示.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.plC_RJ_Button_顯示.Bool = false;
            this.plC_RJ_Button_顯示.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_Button_顯示.BorderRadius = 5;
            this.plC_RJ_Button_顯示.BorderSize = 0;
            this.plC_RJ_Button_顯示.but_press = false;
            this.plC_RJ_Button_顯示.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_顯示.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_顯示.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_顯示.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_RJ_Button_顯示.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_顯示.Location = new System.Drawing.Point(1407, 253);
            this.plC_RJ_Button_顯示.Name = "plC_RJ_Button_顯示";
            this.plC_RJ_Button_顯示.OFF_文字內容 = "顯示";
            this.plC_RJ_Button_顯示.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_RJ_Button_顯示.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_顯示.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_顯示.ON_BorderSize = 5;
            this.plC_RJ_Button_顯示.ON_文字內容 = "顯示";
            this.plC_RJ_Button_顯示.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_RJ_Button_顯示.ON_文字顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_顯示.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_RJ_Button_顯示.Size = new System.Drawing.Size(150, 75);
            this.plC_RJ_Button_顯示.State = false;
            this.plC_RJ_Button_顯示.TabIndex = 56;
            this.plC_RJ_Button_顯示.Text = "顯示";
            this.plC_RJ_Button_顯示.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_顯示.Texts = "顯示";
            this.plC_RJ_Button_顯示.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_顯示.字型鎖住 = false;
            this.plC_RJ_Button_顯示.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_顯示.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_顯示.文字鎖住 = false;
            this.plC_RJ_Button_顯示.讀取位元反向 = false;
            this.plC_RJ_Button_顯示.讀寫鎖住 = false;
            this.plC_RJ_Button_顯示.音效 = true;
            this.plC_RJ_Button_顯示.顯示 = false;
            this.plC_RJ_Button_顯示.顯示狀態 = false;
            // 
            // sqL_DataGridView_人員資料
            // 
            this.sqL_DataGridView_人員資料.AutoSelectToDeep = false;
            this.sqL_DataGridView_人員資料.backColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_人員資料.BorderColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_人員資料.BorderRadius = 10;
            this.sqL_DataGridView_人員資料.BorderSize = 2;
            this.sqL_DataGridView_人員資料.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_人員資料.cellStylBackColor = System.Drawing.Color.LightBlue;
            this.sqL_DataGridView_人員資料.cellStyleFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_人員資料.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_人員資料.columnHeaderBackColor = System.Drawing.Color.SkyBlue;
            this.sqL_DataGridView_人員資料.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.sqL_DataGridView_人員資料.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_人員資料.columnHeadersHeight = 23;
            this.sqL_DataGridView_人員資料.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns1"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns2"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns3"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns4"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns5"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns6"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns7"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns8"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns9"))));
            this.sqL_DataGridView_人員資料.Columns.Add(((SQLUI.SQL_DataGridView.ColumnElement)(resources.GetObject("sqL_DataGridView_人員資料.Columns10"))));
            this.sqL_DataGridView_人員資料.DataBaseName = "Dispensing_000";
            this.sqL_DataGridView_人員資料.Dock = System.Windows.Forms.DockStyle.Left;
            this.sqL_DataGridView_人員資料.Font = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_人員資料.ImageBox = false;
            this.sqL_DataGridView_人員資料.Location = new System.Drawing.Point(0, 0);
            this.sqL_DataGridView_人員資料.Name = "sqL_DataGridView_人員資料";
            this.sqL_DataGridView_人員資料.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_人員資料.Password = "user82822040";
            this.sqL_DataGridView_人員資料.Port = ((uint)(3306u));
            this.sqL_DataGridView_人員資料.rowHeaderBackColor = System.Drawing.Color.CornflowerBlue;
            this.sqL_DataGridView_人員資料.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_人員資料.RowsColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sqL_DataGridView_人員資料.RowsHeight = 50;
            this.sqL_DataGridView_人員資料.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_人員資料.Server = "localhost";
            this.sqL_DataGridView_人員資料.Size = new System.Drawing.Size(1390, 987);
            this.sqL_DataGridView_人員資料.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_人員資料.TabIndex = 55;
            this.sqL_DataGridView_人員資料.TableName = "person_page";
            this.sqL_DataGridView_人員資料.UserName = "root";
            this.sqL_DataGridView_人員資料.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_人員資料.可選擇多列 = true;
            this.sqL_DataGridView_人員資料.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.sqL_DataGridView_人員資料.自動換行 = true;
            this.sqL_DataGridView_人員資料.表單字體 = new System.Drawing.Font("新細明體", 9F);
            this.sqL_DataGridView_人員資料.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_人員資料.顯示CheckBox = true;
            this.sqL_DataGridView_人員資料.顯示首列 = true;
            this.sqL_DataGridView_人員資料.顯示首行 = true;
            this.sqL_DataGridView_人員資料.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sqL_DataGridView_人員資料.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            // 
            // plC_AlarmFlow2
            // 
            this.plC_AlarmFlow2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plC_AlarmFlow2.Location = new System.Drawing.Point(0, 987);
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
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Window;
            this.tabPage2.Controls.Add(this.plC_UI_Init1);
            this.tabPage2.Controls.Add(this.lowerMachine_Panel1);
            this.tabPage2.Controls.Add(this.plC_RJ_ScreenButton3);
            this.tabPage2.Controls.Add(this.plC_RJ_ScreenButton4);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1650, 1013);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "手動";
            // 
            // plC_UI_Init1
            // 
            this.plC_UI_Init1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.plC_UI_Init1.Location = new System.Drawing.Point(931, 41);
            this.plC_UI_Init1.Name = "plC_UI_Init1";
            this.plC_UI_Init1.Size = new System.Drawing.Size(72, 25);
            this.plC_UI_Init1.TabIndex = 34;
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
            this.lowerMachine_Panel1.Location = new System.Drawing.Point(18, 6);
            this.lowerMachine_Panel1.Name = "lowerMachine_Panel1";
            this.lowerMachine_Panel1.Size = new System.Drawing.Size(869, 572);
            this.lowerMachine_Panel1.TabIndex = 33;
            this.lowerMachine_Panel1.掃描速度 = 0;
            // 
            // plC_RJ_ScreenButton3
            // 
            this.plC_RJ_ScreenButton3.but_press = false;
            this.plC_RJ_ScreenButton3.IconChar = FontAwesome.Sharp.IconChar.Cubes;
            this.plC_RJ_ScreenButton3.IconSize = 32;
            this.plC_RJ_ScreenButton3.Location = new System.Drawing.Point(1254, 140);
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
            this.plC_RJ_ScreenButton4.Location = new System.Drawing.Point(1254, 72);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1658, 1042);
            this.Controls.Add(this.plC_ScreenPage1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.plC_ScreenPage1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyUI.PLC_ScreenPage plC_ScreenPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton1;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton2;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton3;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton4;
        private System.Windows.Forms.OpenFileDialog openFileDialog_LoadExcel;
        private MyUI.PLC_AlarmFlow plC_AlarmFlow2;
        private SQLUI.SQL_DataGridView sqL_DataGridView_人員資料;
        private MyUI.PLC_UI_Init plC_UI_Init1;
        private LadderUI.LowerMachine_Panel lowerMachine_Panel1;
        private MyUI.PLC_RJ_Button plC_RJ_Button_顯示;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}

