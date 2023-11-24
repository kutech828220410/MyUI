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
            this.plC_RJ_ScreenButton2 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_RJ_ScreenButton1 = new MyUI.PLC_RJ_ScreenButton();
            this.plC_ScreenPage_main = new MyUI.PLC_ScreenPage();
            this.主畫面 = new System.Windows.Forms.TabPage();
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
            this.panel_main.Controls.Add(this.plC_RJ_ScreenButton2);
            this.panel_main.Controls.Add(this.plC_RJ_ScreenButton1);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_main.Location = new System.Drawing.Point(0, 0);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(1264, 62);
            this.panel_main.TabIndex = 2;
            // 
            // plC_RJ_ScreenButton2
            // 
            this.plC_RJ_ScreenButton2.but_press = false;
            this.plC_RJ_ScreenButton2.Dock = System.Windows.Forms.DockStyle.Left;
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
            this.plC_RJ_ScreenButton1.but_press = false;
            this.plC_RJ_ScreenButton1.Dock = System.Windows.Forms.DockStyle.Left;
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
            this.plC_ScreenPage_main.Size = new System.Drawing.Size(1264, 859);
            this.plC_ScreenPage_main.TabBackColor = System.Drawing.Color.White;
            this.plC_ScreenPage_main.TabIndex = 3;
            this.plC_ScreenPage_main.顯示標籤列 = MyUI.PLC_ScreenPage.TabVisibleEnum.顯示;
            this.plC_ScreenPage_main.顯示頁面 = 0;
            // 
            // 主畫面
            // 
            this.主畫面.Controls.Add(this.sqL_DataGridView_備藥通知);
            this.主畫面.Location = new System.Drawing.Point(4, 25);
            this.主畫面.Name = "主畫面";
            this.主畫面.Size = new System.Drawing.Size(1256, 830);
            this.主畫面.TabIndex = 2;
            this.主畫面.Text = "主畫面";
            this.主畫面.UseVisualStyleBackColor = true;
            // 
            // sqL_DataGridView_備藥通知
            // 
            this.sqL_DataGridView_備藥通知.AutoSelectToDeep = true;
            this.sqL_DataGridView_備藥通知.backColor = System.Drawing.Color.DimGray;
            this.sqL_DataGridView_備藥通知.BorderColor = System.Drawing.Color.Silver;
            this.sqL_DataGridView_備藥通知.BorderRadius = 0;
            this.sqL_DataGridView_備藥通知.BorderSize = 2;
            this.sqL_DataGridView_備藥通知.cellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_備藥通知.cellStylBackColor = System.Drawing.Color.PowderBlue;
            this.sqL_DataGridView_備藥通知.cellStyleFont = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_備藥通知.cellStylForeColor = System.Drawing.Color.Black;
            this.sqL_DataGridView_備藥通知.columnHeaderBackColor = System.Drawing.Color.DarkGray;
            this.sqL_DataGridView_備藥通知.columnHeaderFont = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_備藥通知.columnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Raised;
            this.sqL_DataGridView_備藥通知.columnHeadersHeight = 40;
            this.sqL_DataGridView_備藥通知.columnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.sqL_DataGridView_備藥通知.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.sqL_DataGridView_備藥通知.Server = "127.0.0.0";
            this.sqL_DataGridView_備藥通知.Size = new System.Drawing.Size(1256, 830);
            this.sqL_DataGridView_備藥通知.SSLMode = MySql.Data.MySqlClient.MySqlSslMode.None;
            this.sqL_DataGridView_備藥通知.TabIndex = 5;
            this.sqL_DataGridView_備藥通知.UserName = "root";
            this.sqL_DataGridView_備藥通知.可拖曳欄位寬度 = false;
            this.sqL_DataGridView_備藥通知.可選擇多列 = false;
            this.sqL_DataGridView_備藥通知.單格樣式 = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.sqL_DataGridView_備藥通知.自動換行 = true;
            this.sqL_DataGridView_備藥通知.表單字體 = new System.Drawing.Font("新細明體", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sqL_DataGridView_備藥通知.邊框樣式 = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqL_DataGridView_備藥通知.顯示CheckBox = false;
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
            this.手動.Size = new System.Drawing.Size(1256, 830);
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
            this.plC_AlarmFlow2.Size = new System.Drawing.Size(1256, 26);
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
            this.ClientSize = new System.Drawing.Size(1264, 921);
            this.Controls.Add(this.plC_ScreenPage_main);
            this.Controls.Add(this.panel_main);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel_main.ResumeLayout(false);
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
    }
}

