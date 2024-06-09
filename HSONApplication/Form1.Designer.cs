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
            this.panel_main = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rJ_Lable1 = new MyUI.RJ_Lable();
            this.plC_RJ_ScreenButton2 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton1 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_ScreenPage_main = new MyUI.PLC_ScreenPage();
            this.主畫面 = new System.Windows.Forms.TabPage();
            this.rJ_DatePicker1 = new MyUI.RJ_DatePicker();
            this.plC_RJ_ScreenButtonEx1 = new MyUI.PLC_RJ_ScreenButtonEx();
            this.rJ_Button1 = new MyUI.RJ_Button();
            this.button_next = new System.Windows.Forms.Button();
            this.button_pre = new System.Windows.Forms.Button();
            this.stepViewer1 = new MyUI.StepViewer();
            this.dateTimeIntervelPicker1 = new MyUI.DateTimeIntervelPicker();
            this.plC_NumBox_po = new MyUI.PLC_NumBox();
            this.rJ_Button_Servo_OFF = new MyUI.RJ_Button();
            this.rJ_Button_Servo_ON = new MyUI.RJ_Button();
            this.rJ_Button_set_path_config = new MyUI.RJ_Button();
            this.rJ_Button_Init = new MyUI.RJ_Button();
            this.rJ_Button_return_home = new MyUI.RJ_Button();
            this.rJ_Button_JOG_STOP = new MyUI.RJ_Button();
            this.rJ_Button_DJOG = new MyUI.RJ_Button();
            this.rJ_Button_UJOG = new MyUI.RJ_Button();
            this.sqL_DataGridView_備藥通知 = new SQLUI.SQL_DataGridView();
            this.手動 = new System.Windows.Forms.TabPage();
            this.plC_UI_Init1 = new MyUI.PLC_UI_Init();
            this.lowerMachine_Panel1 = new LadderUI.LowerMachine_Panel();
            this.plC_AlarmFlow2 = new MyUI.PLC_AlarmFlow();
            this.panel_main.SuspendLayout();
            this.plC_ScreenPage_main.SuspendLayout();
            this.主畫面.SuspendLayout();
            this.手動.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog_LoadExcel
            // 
            this.openFileDialog_LoadExcel.DefaultExt = "txt";
            this.openFileDialog_LoadExcel.Filter = "txt File (*.txt)|*.txt;";
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.label1);
            this.panel_main.Controls.Add(this.rJ_Lable1);
            this.panel_main.Controls.Add(this.plC_RJ_ScreenButton2);
            this.panel_main.Controls.Add(this.plC_RJ_ScreenButton1);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_main.Location = new System.Drawing.Point(0, 0);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(1084, 62);
            this.panel_main.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(776, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "label1";
            // 
            // rJ_Lable1
            // 
            this.rJ_Lable1.BackColor = System.Drawing.SystemColors.Control;
            this.rJ_Lable1.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Lable1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable1.BorderRadius = 15;
            this.rJ_Lable1.BorderSize = 0;
            this.rJ_Lable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable1.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable1.GUID = "";
            this.rJ_Lable1.Location = new System.Drawing.Point(508, 9);
            this.rJ_Lable1.Name = "rJ_Lable1";
            this.rJ_Lable1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable1.ShadowSize = 3;
            this.rJ_Lable1.Size = new System.Drawing.Size(135, 53);
            this.rJ_Lable1.TabIndex = 32;
            this.rJ_Lable1.Text = "rJ_Lable1";
            this.rJ_Lable1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable1.TextColor = System.Drawing.Color.White;
            // 
            // plC_RJ_ScreenButton2
            // 
            this.plC_RJ_ScreenButton2.but_press = false;
            this.plC_RJ_ScreenButton2.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton2.ForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton2.IconChar = FontAwesome.Sharp.IconChar.Cube;
            this.plC_RJ_ScreenButton2.IconSize = 32;
            this.plC_RJ_ScreenButton2.Location = new System.Drawing.Point(193, 0);
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
            this.plC_RJ_ScreenButton2.Size = new System.Drawing.Size(193, 62);
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
            // plC_RJ_ScreenButton1
            // 
            this.plC_RJ_ScreenButton1.BackColor = System.Drawing.Color.Transparent;
            this.plC_RJ_ScreenButton1.but_press = false;
            this.plC_RJ_ScreenButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.plC_RJ_ScreenButton1.ForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButton1.IconChar = FontAwesome.Sharp.IconChar.Cubes;
            this.plC_RJ_ScreenButton1.IconSize = 32;
            this.plC_RJ_ScreenButton1.Location = new System.Drawing.Point(0, 0);
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
            this.plC_RJ_ScreenButton1.Size = new System.Drawing.Size(193, 62);
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
            // plC_ScreenPage_main
            // 
            this.plC_ScreenPage_main.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.plC_ScreenPage_main.BackColor = System.Drawing.Color.White;
            this.plC_ScreenPage_main.Controls.Add(this.主畫面);
            this.plC_ScreenPage_main.Controls.Add(this.手動);
            this.plC_ScreenPage_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plC_ScreenPage_main.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.plC_ScreenPage_main.ForekColor = System.Drawing.Color.Black;
            this.plC_ScreenPage_main.ItemSize = new System.Drawing.Size(54, 21);
            this.plC_ScreenPage_main.Location = new System.Drawing.Point(0, 62);
            this.plC_ScreenPage_main.Margin = new System.Windows.Forms.Padding(0);
            this.plC_ScreenPage_main.Name = "plC_ScreenPage_main";
            this.plC_ScreenPage_main.Padding = new System.Drawing.Point(0, 0);
            this.plC_ScreenPage_main.SelectedIndex = 0;
            this.plC_ScreenPage_main.ShowToolTips = true;
            this.plC_ScreenPage_main.Size = new System.Drawing.Size(1084, 859);
            this.plC_ScreenPage_main.TabBackColor = System.Drawing.Color.White;
            this.plC_ScreenPage_main.TabIndex = 3;
            this.plC_ScreenPage_main.顯示標籤列 = MyUI.PLC_ScreenPage.TabVisibleEnum.顯示;
            this.plC_ScreenPage_main.顯示頁面 = 0;
            // 
            // 主畫面
            // 
            this.主畫面.Controls.Add(this.rJ_DatePicker1);
            this.主畫面.Controls.Add(this.plC_RJ_ScreenButtonEx1);
            this.主畫面.Controls.Add(this.rJ_Button1);
            this.主畫面.Controls.Add(this.button_next);
            this.主畫面.Controls.Add(this.button_pre);
            this.主畫面.Controls.Add(this.stepViewer1);
            this.主畫面.Controls.Add(this.dateTimeIntervelPicker1);
            this.主畫面.Controls.Add(this.plC_NumBox_po);
            this.主畫面.Controls.Add(this.rJ_Button_Servo_OFF);
            this.主畫面.Controls.Add(this.rJ_Button_Servo_ON);
            this.主畫面.Controls.Add(this.rJ_Button_set_path_config);
            this.主畫面.Controls.Add(this.rJ_Button_Init);
            this.主畫面.Controls.Add(this.rJ_Button_return_home);
            this.主畫面.Controls.Add(this.rJ_Button_JOG_STOP);
            this.主畫面.Controls.Add(this.rJ_Button_DJOG);
            this.主畫面.Controls.Add(this.rJ_Button_UJOG);
            this.主畫面.Controls.Add(this.sqL_DataGridView_備藥通知);
            this.主畫面.Location = new System.Drawing.Point(4, 25);
            this.主畫面.Name = "主畫面";
            this.主畫面.Size = new System.Drawing.Size(1076, 830);
            this.主畫面.TabIndex = 2;
            this.主畫面.Text = "主畫面";
            this.主畫面.UseVisualStyleBackColor = true;
            // 
            // rJ_DatePicker1
            // 
            this.rJ_DatePicker1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_DatePicker1.BorderSize = 0;
            this.rJ_DatePicker1.CalendarFont = new System.Drawing.Font("標楷體", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_DatePicker1.CalendarTitleBackColor = System.Drawing.SystemColors.ControlLight;
            this.rJ_DatePicker1.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText;
            this.rJ_DatePicker1.CalendarTrailingForeColor = System.Drawing.SystemColors.ControlLight;
            this.rJ_DatePicker1.Font = new System.Drawing.Font("新細明體", 15.75F);
            this.rJ_DatePicker1.Location = new System.Drawing.Point(250, 400);
            this.rJ_DatePicker1.MinimumSize = new System.Drawing.Size(250, 35);
            this.rJ_DatePicker1.Name = "rJ_DatePicker1";
            this.rJ_DatePicker1.PickerFont = new System.Drawing.Font("標楷體", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_DatePicker1.PickerFore = System.Drawing.SystemColors.ControlText;
            this.rJ_DatePicker1.Size = new System.Drawing.Size(250, 35);
            this.rJ_DatePicker1.SkinColor = System.Drawing.Color.MediumSlateBlue;
            this.rJ_DatePicker1.TabIndex = 31;
            this.rJ_DatePicker1.TextColor = System.Drawing.Color.White;
            // 
            // plC_RJ_ScreenButtonEx1
            // 
            this.plC_RJ_ScreenButtonEx1.AutoResetState = false;
            this.plC_RJ_ScreenButtonEx1.BackColor = System.Drawing.Color.Transparent;
            this.plC_RJ_ScreenButtonEx1.BackgroundColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButtonEx1.BackgroundImage = global::HSONApplication.Properties.Resources.人員資料_removebg_preview;
            this.plC_RJ_ScreenButtonEx1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.plC_RJ_ScreenButtonEx1.BorderRadius = 10;
            this.plC_RJ_ScreenButtonEx1.BorderSize = 0;
            this.plC_RJ_ScreenButtonEx1.but_press = false;
            this.plC_RJ_ScreenButtonEx1.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.plC_RJ_ScreenButtonEx1.DisenableColor = System.Drawing.Color.Gray;
            this.plC_RJ_ScreenButtonEx1.FlatAppearance.BorderSize = 0;
            this.plC_RJ_ScreenButtonEx1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_ScreenButtonEx1.ForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButtonEx1.GUID = "";
            this.plC_RJ_ScreenButtonEx1.Image_padding = new System.Windows.Forms.Padding(10);
            this.plC_RJ_ScreenButtonEx1.Location = new System.Drawing.Point(93, 640);
            this.plC_RJ_ScreenButtonEx1.Name = "plC_RJ_ScreenButtonEx1";
            this.plC_RJ_ScreenButtonEx1.OffFont = new System.Drawing.Font("新細明體", 12F);
            this.plC_RJ_ScreenButtonEx1.OffForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButtonEx1.OffIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButtonEx1.OffText = "";
            this.plC_RJ_ScreenButtonEx1.OnFont = new System.Drawing.Font("新細明體", 12F);
            this.plC_RJ_ScreenButtonEx1.OnForeColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButtonEx1.OnIconColor = System.Drawing.Color.Black;
            this.plC_RJ_ScreenButtonEx1.OnText = "iConText";
            this.plC_RJ_ScreenButtonEx1.ProhibitionBorderLineWidth = 1;
            this.plC_RJ_ScreenButtonEx1.ProhibitionLineWidth = 4;
            this.plC_RJ_ScreenButtonEx1.ProhibitionSymbolSize = 30;
            this.plC_RJ_ScreenButtonEx1.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_ScreenButtonEx1.ShadowSize = 0;
            this.plC_RJ_ScreenButtonEx1.ShowLoadingForm = false;
            this.plC_RJ_ScreenButtonEx1.Size = new System.Drawing.Size(122, 108);
            this.plC_RJ_ScreenButtonEx1.State = false;
            this.plC_RJ_ScreenButtonEx1.TabIndex = 30;
            this.plC_RJ_ScreenButtonEx1.Text = "20";
            this.plC_RJ_ScreenButtonEx1.TextColor = System.Drawing.Color.White;
            this.plC_RJ_ScreenButtonEx1.TextHeight = 20;
            this.plC_RJ_ScreenButtonEx1.UseVisualStyleBackColor = false;
            this.plC_RJ_ScreenButtonEx1.字元長度 = MyUI.PLC_RJ_ScreenButtonEx.WordLengthEnum.單字元;
            this.plC_RJ_ScreenButtonEx1.寫入位置註解 = "";
            this.plC_RJ_ScreenButtonEx1.寫入元件位置 = "";
            this.plC_RJ_ScreenButtonEx1.按鈕型態 = MyUI.PLC_RJ_ScreenButtonEx.StatusEnum.保持型;
            this.plC_RJ_ScreenButtonEx1.控制位址 = "D0";
            this.plC_RJ_ScreenButtonEx1.換頁選擇方式 = MyUI.PLC_RJ_ScreenButtonEx.換頁選擇方式Enum.名稱;
            this.plC_RJ_ScreenButtonEx1.致能讀取位置 = "";
            this.plC_RJ_ScreenButtonEx1.讀取位元反向 = false;
            this.plC_RJ_ScreenButtonEx1.讀取位置註解 = "";
            this.plC_RJ_ScreenButtonEx1.讀取元件位置 = "";
            this.plC_RJ_ScreenButtonEx1.音效 = true;
            this.plC_RJ_ScreenButtonEx1.頁面名稱 = "手動";
            this.plC_RJ_ScreenButtonEx1.頁面編號 = 0;
            this.plC_RJ_ScreenButtonEx1.顯示方式 = MyUI.PLC_RJ_ScreenButtonEx.StateEnum.正常顯示;
            this.plC_RJ_ScreenButtonEx1.顯示狀態 = false;
            this.plC_RJ_ScreenButtonEx1.顯示讀取位置 = "";
            // 
            // rJ_Button1
            // 
            this.rJ_Button1.AutoResetState = false;
            this.rJ_Button1.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button1.BackgroundColor = System.Drawing.Color.Black;
            this.rJ_Button1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button1.BorderRadius = 10;
            this.rJ_Button1.BorderSize = 0;
            this.rJ_Button1.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button1.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button1.FlatAppearance.BorderSize = 0;
            this.rJ_Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button1.ForeColor = System.Drawing.Color.White;
            this.rJ_Button1.GUID = "";
            this.rJ_Button1.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button1.Location = new System.Drawing.Point(362, 591);
            this.rJ_Button1.Name = "rJ_Button1";
            this.rJ_Button1.ProhibitionBorderLineWidth = 1;
            this.rJ_Button1.ProhibitionLineWidth = 4;
            this.rJ_Button1.ProhibitionSymbolSize = 30;
            this.rJ_Button1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button1.ShadowSize = 3;
            this.rJ_Button1.ShowLoadingForm = false;
            this.rJ_Button1.Size = new System.Drawing.Size(162, 80);
            this.rJ_Button1.State = false;
            this.rJ_Button1.TabIndex = 29;
            this.rJ_Button1.Text = "rJ_Button1";
            this.rJ_Button1.TextColor = System.Drawing.Color.White;
            this.rJ_Button1.TextHeight = 0;
            this.rJ_Button1.UseVisualStyleBackColor = false;
            // 
            // button_next
            // 
            this.button_next.Location = new System.Drawing.Point(313, 346);
            this.button_next.Name = "button_next";
            this.button_next.Size = new System.Drawing.Size(75, 23);
            this.button_next.TabIndex = 27;
            this.button_next.Text = "next";
            this.button_next.UseVisualStyleBackColor = true;
            // 
            // button_pre
            // 
            this.button_pre.Location = new System.Drawing.Point(232, 346);
            this.button_pre.Name = "button_pre";
            this.button_pre.Size = new System.Drawing.Size(75, 23);
            this.button_pre.TabIndex = 26;
            this.button_pre.Text = "pre";
            this.button_pre.UseVisualStyleBackColor = true;
            // 
            // stepViewer1
            // 
            this.stepViewer1.BackColor = System.Drawing.Color.White;
            this.stepViewer1.CurrentStep = 0;
            this.stepViewer1.LineWidth = 120;
            this.stepViewer1.ListDataSource = null;
            this.stepViewer1.Location = new System.Drawing.Point(31, 460);
            this.stepViewer1.Name = "stepViewer1";
            this.stepViewer1.Size = new System.Drawing.Size(891, 80);
            this.stepViewer1.TabIndex = 25;
            // 
            // dateTimeIntervelPicker1
            // 
            this.dateTimeIntervelPicker1.BackColor = System.Drawing.Color.Transparent;
            this.dateTimeIntervelPicker1.EndTime = new System.DateTime(2024, 5, 15, 23, 59, 59, 0);
            this.dateTimeIntervelPicker1.Location = new System.Drawing.Point(506, 400);
            this.dateTimeIntervelPicker1.Name = "dateTimeIntervelPicker1";
            this.dateTimeIntervelPicker1.Size = new System.Drawing.Size(535, 32);
            this.dateTimeIntervelPicker1.StartTime = new System.DateTime(2024, 5, 15, 0, 0, 0, 0);
            this.dateTimeIntervelPicker1.TabIndex = 24;
            // 
            // plC_NumBox_po
            // 
            this.plC_NumBox_po.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_NumBox_po.Location = new System.Drawing.Point(810, 628);
            this.plC_NumBox_po.mBackColor = System.Drawing.SystemColors.Window;
            this.plC_NumBox_po.mForeColor = System.Drawing.SystemColors.WindowText;
            this.plC_NumBox_po.Name = "plC_NumBox_po";
            this.plC_NumBox_po.ReadOnly = false;
            this.plC_NumBox_po.Size = new System.Drawing.Size(156, 24);
            this.plC_NumBox_po.TabIndex = 23;
            this.plC_NumBox_po.Value = 0;
            this.plC_NumBox_po.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_po.密碼欄位 = false;
            this.plC_NumBox_po.小數點位置 = 0;
            this.plC_NumBox_po.微調數值 = 1;
            this.plC_NumBox_po.音效 = true;
            this.plC_NumBox_po.顯示微調按鈕 = false;
            this.plC_NumBox_po.顯示螢幕小鍵盤 = true;
            // 
            // rJ_Button_Servo_OFF
            // 
            this.rJ_Button_Servo_OFF.AutoResetState = false;
            this.rJ_Button_Servo_OFF.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_Servo_OFF.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_Servo_OFF.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_Servo_OFF.BorderRadius = 10;
            this.rJ_Button_Servo_OFF.BorderSize = 0;
            this.rJ_Button_Servo_OFF.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_Servo_OFF.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_Servo_OFF.FlatAppearance.BorderSize = 0;
            this.rJ_Button_Servo_OFF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_Servo_OFF.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_Servo_OFF.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_Servo_OFF.GUID = "";
            this.rJ_Button_Servo_OFF.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_Servo_OFF.Location = new System.Drawing.Point(494, 738);
            this.rJ_Button_Servo_OFF.Name = "rJ_Button_Servo_OFF";
            this.rJ_Button_Servo_OFF.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_Servo_OFF.ProhibitionLineWidth = 4;
            this.rJ_Button_Servo_OFF.ProhibitionSymbolSize = 30;
            this.rJ_Button_Servo_OFF.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_Servo_OFF.ShadowSize = 0;
            this.rJ_Button_Servo_OFF.ShowLoadingForm = false;
            this.rJ_Button_Servo_OFF.Size = new System.Drawing.Size(222, 49);
            this.rJ_Button_Servo_OFF.State = false;
            this.rJ_Button_Servo_OFF.TabIndex = 22;
            this.rJ_Button_Servo_OFF.Text = "Servo OFF";
            this.rJ_Button_Servo_OFF.TextColor = System.Drawing.Color.White;
            this.rJ_Button_Servo_OFF.TextHeight = 0;
            this.rJ_Button_Servo_OFF.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_Servo_ON
            // 
            this.rJ_Button_Servo_ON.AutoResetState = false;
            this.rJ_Button_Servo_ON.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_Servo_ON.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_Servo_ON.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_Servo_ON.BorderRadius = 10;
            this.rJ_Button_Servo_ON.BorderSize = 0;
            this.rJ_Button_Servo_ON.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_Servo_ON.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_Servo_ON.FlatAppearance.BorderSize = 0;
            this.rJ_Button_Servo_ON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_Servo_ON.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_Servo_ON.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_Servo_ON.GUID = "";
            this.rJ_Button_Servo_ON.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_Servo_ON.Location = new System.Drawing.Point(266, 738);
            this.rJ_Button_Servo_ON.Name = "rJ_Button_Servo_ON";
            this.rJ_Button_Servo_ON.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_Servo_ON.ProhibitionLineWidth = 4;
            this.rJ_Button_Servo_ON.ProhibitionSymbolSize = 30;
            this.rJ_Button_Servo_ON.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_Servo_ON.ShadowSize = 0;
            this.rJ_Button_Servo_ON.ShowLoadingForm = false;
            this.rJ_Button_Servo_ON.Size = new System.Drawing.Size(222, 49);
            this.rJ_Button_Servo_ON.State = false;
            this.rJ_Button_Servo_ON.TabIndex = 21;
            this.rJ_Button_Servo_ON.Text = "Servo ON";
            this.rJ_Button_Servo_ON.TextColor = System.Drawing.Color.White;
            this.rJ_Button_Servo_ON.TextHeight = 0;
            this.rJ_Button_Servo_ON.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_set_path_config
            // 
            this.rJ_Button_set_path_config.AutoResetState = false;
            this.rJ_Button_set_path_config.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_set_path_config.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_set_path_config.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_set_path_config.BorderRadius = 10;
            this.rJ_Button_set_path_config.BorderSize = 0;
            this.rJ_Button_set_path_config.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_set_path_config.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_set_path_config.FlatAppearance.BorderSize = 0;
            this.rJ_Button_set_path_config.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_set_path_config.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_set_path_config.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_set_path_config.GUID = "";
            this.rJ_Button_set_path_config.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_set_path_config.Location = new System.Drawing.Point(559, 561);
            this.rJ_Button_set_path_config.Name = "rJ_Button_set_path_config";
            this.rJ_Button_set_path_config.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_set_path_config.ProhibitionLineWidth = 4;
            this.rJ_Button_set_path_config.ProhibitionSymbolSize = 30;
            this.rJ_Button_set_path_config.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_set_path_config.ShadowSize = 0;
            this.rJ_Button_set_path_config.ShowLoadingForm = false;
            this.rJ_Button_set_path_config.Size = new System.Drawing.Size(222, 49);
            this.rJ_Button_set_path_config.State = false;
            this.rJ_Button_set_path_config.TabIndex = 19;
            this.rJ_Button_set_path_config.Text = "set_path_config";
            this.rJ_Button_set_path_config.TextColor = System.Drawing.Color.White;
            this.rJ_Button_set_path_config.TextHeight = 0;
            this.rJ_Button_set_path_config.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_Init
            // 
            this.rJ_Button_Init.AutoResetState = false;
            this.rJ_Button_Init.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_Init.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_Init.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_Init.BorderRadius = 10;
            this.rJ_Button_Init.BorderSize = 0;
            this.rJ_Button_Init.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_Init.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_Init.FlatAppearance.BorderSize = 0;
            this.rJ_Button_Init.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_Init.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_Init.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_Init.GUID = "";
            this.rJ_Button_Init.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_Init.Location = new System.Drawing.Point(18, 561);
            this.rJ_Button_Init.Name = "rJ_Button_Init";
            this.rJ_Button_Init.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_Init.ProhibitionLineWidth = 4;
            this.rJ_Button_Init.ProhibitionSymbolSize = 30;
            this.rJ_Button_Init.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_Init.ShadowSize = 0;
            this.rJ_Button_Init.ShowLoadingForm = false;
            this.rJ_Button_Init.Size = new System.Drawing.Size(222, 49);
            this.rJ_Button_Init.State = false;
            this.rJ_Button_Init.TabIndex = 18;
            this.rJ_Button_Init.Text = "Init";
            this.rJ_Button_Init.TextColor = System.Drawing.Color.White;
            this.rJ_Button_Init.TextHeight = 0;
            this.rJ_Button_Init.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_return_home
            // 
            this.rJ_Button_return_home.AutoResetState = false;
            this.rJ_Button_return_home.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_return_home.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_return_home.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_return_home.BorderRadius = 10;
            this.rJ_Button_return_home.BorderSize = 0;
            this.rJ_Button_return_home.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_return_home.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_return_home.FlatAppearance.BorderSize = 0;
            this.rJ_Button_return_home.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_return_home.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_return_home.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_return_home.GUID = "";
            this.rJ_Button_return_home.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_return_home.Location = new System.Drawing.Point(787, 561);
            this.rJ_Button_return_home.Name = "rJ_Button_return_home";
            this.rJ_Button_return_home.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_return_home.ProhibitionLineWidth = 4;
            this.rJ_Button_return_home.ProhibitionSymbolSize = 30;
            this.rJ_Button_return_home.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_return_home.ShadowSize = 0;
            this.rJ_Button_return_home.ShowLoadingForm = false;
            this.rJ_Button_return_home.Size = new System.Drawing.Size(222, 49);
            this.rJ_Button_return_home.State = false;
            this.rJ_Button_return_home.TabIndex = 15;
            this.rJ_Button_return_home.Text = "return_home";
            this.rJ_Button_return_home.TextColor = System.Drawing.Color.White;
            this.rJ_Button_return_home.TextHeight = 0;
            this.rJ_Button_return_home.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_JOG_STOP
            // 
            this.rJ_Button_JOG_STOP.AutoResetState = false;
            this.rJ_Button_JOG_STOP.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_JOG_STOP.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_JOG_STOP.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_JOG_STOP.BorderRadius = 10;
            this.rJ_Button_JOG_STOP.BorderSize = 0;
            this.rJ_Button_JOG_STOP.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_JOG_STOP.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_JOG_STOP.FlatAppearance.BorderSize = 0;
            this.rJ_Button_JOG_STOP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_JOG_STOP.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_JOG_STOP.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_JOG_STOP.GUID = "";
            this.rJ_Button_JOG_STOP.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_JOG_STOP.Location = new System.Drawing.Point(919, 738);
            this.rJ_Button_JOG_STOP.Name = "rJ_Button_JOG_STOP";
            this.rJ_Button_JOG_STOP.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_JOG_STOP.ProhibitionLineWidth = 4;
            this.rJ_Button_JOG_STOP.ProhibitionSymbolSize = 30;
            this.rJ_Button_JOG_STOP.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_JOG_STOP.ShadowSize = 0;
            this.rJ_Button_JOG_STOP.ShowLoadingForm = false;
            this.rJ_Button_JOG_STOP.Size = new System.Drawing.Size(150, 49);
            this.rJ_Button_JOG_STOP.State = false;
            this.rJ_Button_JOG_STOP.TabIndex = 8;
            this.rJ_Button_JOG_STOP.Text = "JOG STOP";
            this.rJ_Button_JOG_STOP.TextColor = System.Drawing.Color.White;
            this.rJ_Button_JOG_STOP.TextHeight = 0;
            this.rJ_Button_JOG_STOP.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_DJOG
            // 
            this.rJ_Button_DJOG.AutoResetState = false;
            this.rJ_Button_DJOG.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_DJOG.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_DJOG.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_DJOG.BorderRadius = 10;
            this.rJ_Button_DJOG.BorderSize = 0;
            this.rJ_Button_DJOG.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_DJOG.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_DJOG.FlatAppearance.BorderSize = 0;
            this.rJ_Button_DJOG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_DJOG.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_DJOG.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_DJOG.GUID = "";
            this.rJ_Button_DJOG.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_DJOG.Location = new System.Drawing.Point(1075, 738);
            this.rJ_Button_DJOG.Name = "rJ_Button_DJOG";
            this.rJ_Button_DJOG.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_DJOG.ProhibitionLineWidth = 4;
            this.rJ_Button_DJOG.ProhibitionSymbolSize = 30;
            this.rJ_Button_DJOG.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_DJOG.ShadowSize = 0;
            this.rJ_Button_DJOG.ShowLoadingForm = false;
            this.rJ_Button_DJOG.Size = new System.Drawing.Size(150, 49);
            this.rJ_Button_DJOG.State = false;
            this.rJ_Button_DJOG.TabIndex = 7;
            this.rJ_Button_DJOG.Text = "DJOG";
            this.rJ_Button_DJOG.TextColor = System.Drawing.Color.White;
            this.rJ_Button_DJOG.TextHeight = 0;
            this.rJ_Button_DJOG.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_UJOG
            // 
            this.rJ_Button_UJOG.AutoResetState = false;
            this.rJ_Button_UJOG.BackColor = System.Drawing.Color.Transparent;
            this.rJ_Button_UJOG.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_UJOG.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_UJOG.BorderRadius = 10;
            this.rJ_Button_UJOG.BorderSize = 0;
            this.rJ_Button_UJOG.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_UJOG.DisenableColor = System.Drawing.Color.Gray;
            this.rJ_Button_UJOG.FlatAppearance.BorderSize = 0;
            this.rJ_Button_UJOG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_UJOG.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_UJOG.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_UJOG.GUID = "";
            this.rJ_Button_UJOG.Image_padding = new System.Windows.Forms.Padding(0);
            this.rJ_Button_UJOG.Location = new System.Drawing.Point(763, 738);
            this.rJ_Button_UJOG.Name = "rJ_Button_UJOG";
            this.rJ_Button_UJOG.ProhibitionBorderLineWidth = 1;
            this.rJ_Button_UJOG.ProhibitionLineWidth = 4;
            this.rJ_Button_UJOG.ProhibitionSymbolSize = 30;
            this.rJ_Button_UJOG.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_UJOG.ShadowSize = 0;
            this.rJ_Button_UJOG.ShowLoadingForm = false;
            this.rJ_Button_UJOG.Size = new System.Drawing.Size(150, 49);
            this.rJ_Button_UJOG.State = false;
            this.rJ_Button_UJOG.TabIndex = 6;
            this.rJ_Button_UJOG.Text = "UJOG";
            this.rJ_Button_UJOG.TextColor = System.Drawing.Color.White;
            this.rJ_Button_UJOG.TextHeight = 0;
            this.rJ_Button_UJOG.UseVisualStyleBackColor = false;
            // 
            // sqL_DataGridView_備藥通知
            // 
            this.sqL_DataGridView_備藥通知.AutoSelectToDeep = true;
            this.sqL_DataGridView_備藥通知.backColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_備藥通知.BorderColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_備藥通知.BorderRadius = 0;
            this.sqL_DataGridView_備藥通知.BorderSize = 2;
            this.sqL_DataGridView_備藥通知.CellBorderColor = System.Drawing.Color.White;
            this.sqL_DataGridView_備藥通知.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_備藥通知.cellStylBackColor = System.Drawing.Color.Gainsboro;
            this.sqL_DataGridView_備藥通知.cellStyleFont = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_備藥通知.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_備藥通知.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_備藥通知.columnHeaderBorderColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_備藥通知.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_備藥通知.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_備藥通知.columnHeadersHeight = 40;
            this.sqL_DataGridView_備藥通知.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.sqL_DataGridView_備藥通知.DataGridViewAutoSizeColumnMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sqL_DataGridView_備藥通知.Dock = System.Windows.Forms.DockStyle.Top;
            this.sqL_DataGridView_備藥通知.Font = new System.Drawing.Font("新細明體", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_備藥通知.ImageBox = false;
            this.sqL_DataGridView_備藥通知.Location = new System.Drawing.Point(0, 0);
            this.sqL_DataGridView_備藥通知.Margin = new System.Windows.Forms.Padding(0);
            this.sqL_DataGridView_備藥通知.Name = "sqL_DataGridView_備藥通知";
            this.sqL_DataGridView_備藥通知.OnlineState = SQLUI.SQL_DataGridView.OnlineEnum.Online;
            this.sqL_DataGridView_備藥通知.Password = "user82822040";
            this.sqL_DataGridView_備藥通知.Port = ((uint)(3306u));
            this.sqL_DataGridView_備藥通知.rowHeaderBackColor = System.Drawing.Color.Gray;
            this.sqL_DataGridView_備藥通知.rowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_備藥通知.RowsColor = System.Drawing.SystemColors.Control;
            this.sqL_DataGridView_備藥通知.RowsHeight = 80;
            this.sqL_DataGridView_備藥通知.SaveFileName = "SQL_DataGridView";
            this.sqL_DataGridView_備藥通知.selectedBorderSize = 0;
            this.sqL_DataGridView_備藥通知.selectedRowBackColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_備藥通知.selectedRowBorderColor = System.Drawing.Color.Blue;
            this.sqL_DataGridView_備藥通知.selectedRowForeColor = System.Drawing.Color.White;
            this.sqL_DataGridView_備藥通知.Server = "127.0.0.0";
            this.sqL_DataGridView_備藥通知.Size = new System.Drawing.Size(1076, 384);
            this.sqL_DataGridView_備藥通知.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_備藥通知.TabIndex = 5;
            this.sqL_DataGridView_備藥通知.UserName = "root";
            this.sqL_DataGridView_備藥通知.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_備藥通知.可選擇多列 = false;
            this.sqL_DataGridView_備藥通知.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_備藥通知.自動換行 = true;
            this.sqL_DataGridView_備藥通知.表單字體 = new System.Drawing.Font("新細明體", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_備藥通知.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_備藥通知.顯示CheckBox = true;
            this.sqL_DataGridView_備藥通知.顯示首列 = true;
            this.sqL_DataGridView_備藥通知.顯示首行 = true;
            this.sqL_DataGridView_備藥通知.首列樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_備藥通知.首行樣式 = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_備藥通知.MouseDown += new System.Windows.Forms.MouseEventHandler(this.sqL_DataGridView_備藥通知_MouseDown);
            // 
            // 手動
            // 
            this.手動.AutoScroll = true;
            this.手動.BackColor = System.Drawing.SystemColors.Window;
            this.手動.Controls.Add(this.plC_UI_Init1);
            this.手動.Controls.Add(this.lowerMachine_Panel1);
            this.手動.Controls.Add(this.plC_AlarmFlow2);
            this.手動.Location = new System.Drawing.Point(4, 25);
            this.手動.Margin = new System.Windows.Forms.Padding(0);
            this.手動.Name = "手動";
            this.手動.Size = new System.Drawing.Size(1076, 830);
            this.手動.TabIndex = 0;
            this.手動.Text = "手動";
            // 
            // plC_UI_Init1
            // 
            this.plC_UI_Init1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.plC_UI_Init1.Location = new System.Drawing.Point(883, 574);
            this.plC_UI_Init1.Name = "plC_UI_Init1";
            this.plC_UI_Init1.Size = new System.Drawing.Size(72, 25);
            this.plC_UI_Init1.TabIndex = 57;
            this.plC_UI_Init1.光道視覺元件初始化 = false;
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
            this.lowerMachine_Panel1.Location = new System.Drawing.Point(8, 27);
            this.lowerMachine_Panel1.Name = "lowerMachine_Panel1";
            this.lowerMachine_Panel1.Size = new System.Drawing.Size(869, 572);
            this.lowerMachine_Panel1.TabIndex = 56;
            this.lowerMachine_Panel1.掃描速度 = 0;
            // 
            // plC_AlarmFlow2
            // 
            this.plC_AlarmFlow2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plC_AlarmFlow2.Location = new System.Drawing.Point(0, 804);
            this.plC_AlarmFlow2.Name = "plC_AlarmFlow2";
            this.plC_AlarmFlow2.Size = new System.Drawing.Size(1076, 26);
            this.plC_AlarmFlow2.TabIndex = 54;
            this.plC_AlarmFlow2.捲動速度 = 200;
            this.plC_AlarmFlow2.文字字體 = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_AlarmFlow2.文字顏色 = System.Drawing.Color.White;
            this.plC_AlarmFlow2.自動隱藏 = false;
            this.plC_AlarmFlow2.警報編輯 = ((System.Collections.Generic.List<string>)(resources.GetObject("plC_AlarmFlow2.警報編輯")));
            this.plC_AlarmFlow2.顯示警報編號 = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1084, 921);
            this.Controls.Add(this.plC_ScreenPage_main);
            this.Controls.Add(this.panel_main);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            this.plC_ScreenPage_main.ResumeLayout(false);
            this.主畫面.ResumeLayout(false);
            this.手動.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog_LoadExcel;
        private System.Windows.Forms.Panel panel_main;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton2;
        private MyUI.PLC_RJ_ScreenButton plC_RJ_ScreenButton1;
        private MyUI.PLC_ScreenPage plC_ScreenPage_main;
        private System.Windows.Forms.TabPage 手動;
        private MyUI.PLC_UI_Init plC_UI_Init1;
        private LadderUI.LowerMachine_Panel lowerMachine_Panel1;
        private MyUI.PLC_AlarmFlow plC_AlarmFlow2;
        private System.Windows.Forms.TabPage 主畫面;
        private SQLUI.SQL_DataGridView sqL_DataGridView_備藥通知;
        private MyUI.RJ_Button rJ_Button_UJOG;
        private MyUI.RJ_Button rJ_Button_JOG_STOP;
        private MyUI.RJ_Button rJ_Button_DJOG;
        private MyUI.RJ_Button rJ_Button_return_home;
        private MyUI.RJ_Button rJ_Button_Init;
        private MyUI.RJ_Button rJ_Button_set_path_config;
        private MyUI.RJ_Button rJ_Button_Servo_OFF;
        private MyUI.RJ_Button rJ_Button_Servo_ON;
        private MyUI.PLC_NumBox plC_NumBox_po;
        private MyUI.DateTimeIntervelPicker dateTimeIntervelPicker1;
        private System.Windows.Forms.Button button_pre;
        private MyUI.StepViewer stepViewer1;
        private System.Windows.Forms.Button button_next;
        private MyUI.RJ_Button rJ_Button1;
        private MyUI.PLC_RJ_ScreenButtonEx plC_RJ_ScreenButtonEx1;
        private MyUI.RJ_DatePicker rJ_DatePicker1;
        private System.Windows.Forms.Label label1;
        private MyUI.RJ_Lable rJ_Lable1;
    }
}

