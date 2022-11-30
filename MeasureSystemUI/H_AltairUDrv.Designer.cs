namespace MeasureSystemUI
{
    partial class H_AltairUDrv
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_AltairUDrv));
            this.panel_CCD = new System.Windows.Forms.Panel();
            this.checkBox_水平翻轉 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox_亮度補償 = new System.Windows.Forms.CheckBox();
            this.checkBox_長曝光模式 = new System.Windows.Forms.CheckBox();
            this.checkBox_垂直翻轉 = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.plC_Button_相機初始化 = new MyUI.PLC_Button();
            this.plC_Button_相機已建立 = new MyUI.PLC_Button();
            this.plC_Button_READY = new MyUI.PLC_Button();
            this.plC_Button_TRIGGER = new MyUI.PLC_Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.plC_NumBox_序號 = new MyUI.PLC_NumBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.plC_NumBox_ActiveSurfaceHandle = new MyUI.PLC_NumBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.plC_NumBox_取像時間 = new MyUI.PLC_NumBox();
            this.label3 = new System.Windows.Forms.Label();
            this.plC_TrackBarHorizontal_視訊增益 = new MyUI.PLC_TrackBarHorizontal();
            this.plC_TrackBarHorizontal_電子快門 = new MyUI.PLC_TrackBarHorizontal();
            this.plC_TrackBarHorizontal_光源亮度 = new MyUI.PLC_TrackBarHorizontal();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_CCD
            // 
            this.panel_CCD.Location = new System.Drawing.Point(3, 3);
            this.panel_CCD.Name = "panel_CCD";
            this.panel_CCD.Size = new System.Drawing.Size(120, 120);
            this.panel_CCD.TabIndex = 0;
            // 
            // checkBox_水平翻轉
            // 
            this.checkBox_水平翻轉.AutoSize = true;
            this.checkBox_水平翻轉.Font = new System.Drawing.Font("新細明體", 12F);
            this.checkBox_水平翻轉.Location = new System.Drawing.Point(12, 15);
            this.checkBox_水平翻轉.Name = "checkBox_水平翻轉";
            this.checkBox_水平翻轉.Size = new System.Drawing.Size(91, 20);
            this.checkBox_水平翻轉.TabIndex = 1;
            this.checkBox_水平翻轉.Text = "水平翻轉";
            this.checkBox_水平翻轉.UseVisualStyleBackColor = true;
            this.checkBox_水平翻轉.CheckedChanged += new System.EventHandler(this.checkBox_水平翻轉_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.checkBox_亮度補償);
            this.panel1.Controls.Add(this.checkBox_長曝光模式);
            this.panel1.Controls.Add(this.checkBox_垂直翻轉);
            this.panel1.Controls.Add(this.checkBox_水平翻轉);
            this.panel1.Location = new System.Drawing.Point(3, 128);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 124);
            this.panel1.TabIndex = 2;
            // 
            // checkBox_亮度補償
            // 
            this.checkBox_亮度補償.AutoSize = true;
            this.checkBox_亮度補償.Font = new System.Drawing.Font("新細明體", 12F);
            this.checkBox_亮度補償.Location = new System.Drawing.Point(12, 93);
            this.checkBox_亮度補償.Name = "checkBox_亮度補償";
            this.checkBox_亮度補償.Size = new System.Drawing.Size(91, 20);
            this.checkBox_亮度補償.TabIndex = 4;
            this.checkBox_亮度補償.Text = "亮度補償";
            this.checkBox_亮度補償.UseVisualStyleBackColor = true;
            this.checkBox_亮度補償.CheckedChanged += new System.EventHandler(this.checkBox_亮度補償_CheckedChanged);
            // 
            // checkBox_長曝光模式
            // 
            this.checkBox_長曝光模式.AutoSize = true;
            this.checkBox_長曝光模式.Font = new System.Drawing.Font("新細明體", 12F);
            this.checkBox_長曝光模式.Location = new System.Drawing.Point(12, 67);
            this.checkBox_長曝光模式.Name = "checkBox_長曝光模式";
            this.checkBox_長曝光模式.Size = new System.Drawing.Size(107, 20);
            this.checkBox_長曝光模式.TabIndex = 3;
            this.checkBox_長曝光模式.Text = "長曝光模式";
            this.checkBox_長曝光模式.UseVisualStyleBackColor = true;
            this.checkBox_長曝光模式.CheckedChanged += new System.EventHandler(this.checkBox_長曝光模式_CheckedChanged);
            // 
            // checkBox_垂直翻轉
            // 
            this.checkBox_垂直翻轉.AutoSize = true;
            this.checkBox_垂直翻轉.Font = new System.Drawing.Font("新細明體", 12F);
            this.checkBox_垂直翻轉.Location = new System.Drawing.Point(12, 41);
            this.checkBox_垂直翻轉.Name = "checkBox_垂直翻轉";
            this.checkBox_垂直翻轉.Size = new System.Drawing.Size(91, 20);
            this.checkBox_垂直翻轉.TabIndex = 2;
            this.checkBox_垂直翻轉.Text = "垂直翻轉";
            this.checkBox_垂直翻轉.UseVisualStyleBackColor = true;
            this.checkBox_垂直翻轉.CheckedChanged += new System.EventHandler(this.checkBox_垂直翻轉_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.plC_Button_相機初始化);
            this.panel2.Controls.Add(this.plC_Button_相機已建立);
            this.panel2.Controls.Add(this.plC_Button_READY);
            this.panel2.Controls.Add(this.plC_Button_TRIGGER);
            this.panel2.Location = new System.Drawing.Point(129, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(209, 108);
            this.panel2.TabIndex = 3;
            // 
            // plC_Button_相機初始化
            // 
            this.plC_Button_相機初始化.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_相機初始化.Location = new System.Drawing.Point(3, 3);
            this.plC_Button_相機初始化.Name = "plC_Button_相機初始化";
            this.plC_Button_相機初始化.OFF_文字內容 = "相機初始化";
            this.plC_Button_相機初始化.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_相機初始化.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_相機初始化.ON_文字內容 = "相機初始化";
            this.plC_Button_相機初始化.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_相機初始化.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_相機初始化.Size = new System.Drawing.Size(97, 46);
            this.plC_Button_相機初始化.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_相機初始化.TabIndex = 3;
            this.plC_Button_相機初始化.字型鎖住 = false;
            this.plC_Button_相機初始化.按鈕型態 = MyUI.PLC_Button.StatusEnum.保持型;
            this.plC_Button_相機初始化.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_相機初始化.文字鎖住 = false;
            this.plC_Button_相機初始化.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_相機初始化.狀態OFF圖片")));
            this.plC_Button_相機初始化.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_相機初始化.狀態ON圖片")));
            this.plC_Button_相機初始化.讀寫鎖住 = false;
            this.plC_Button_相機初始化.音效 = true;
            this.plC_Button_相機初始化.顯示 = false;
            this.plC_Button_相機初始化.顯示狀態 = false;
            // 
            // plC_Button_相機已建立
            // 
            this.plC_Button_相機已建立.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_相機已建立.Location = new System.Drawing.Point(106, 3);
            this.plC_Button_相機已建立.Name = "plC_Button_相機已建立";
            this.plC_Button_相機已建立.OFF_文字內容 = "相機已建立";
            this.plC_Button_相機已建立.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_相機已建立.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_相機已建立.ON_文字內容 = "相機已建立";
            this.plC_Button_相機已建立.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_相機已建立.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_相機已建立.Size = new System.Drawing.Size(97, 46);
            this.plC_Button_相機已建立.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_相機已建立.TabIndex = 2;
            this.plC_Button_相機已建立.字型鎖住 = false;
            this.plC_Button_相機已建立.按鈕型態 = MyUI.PLC_Button.StatusEnum.保持型;
            this.plC_Button_相機已建立.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_相機已建立.文字鎖住 = false;
            this.plC_Button_相機已建立.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_相機已建立.狀態OFF圖片")));
            this.plC_Button_相機已建立.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_相機已建立.狀態ON圖片")));
            this.plC_Button_相機已建立.讀寫鎖住 = false;
            this.plC_Button_相機已建立.音效 = true;
            this.plC_Button_相機已建立.顯示 = false;
            this.plC_Button_相機已建立.顯示狀態 = false;
            // 
            // plC_Button_READY
            // 
            this.plC_Button_READY.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_READY.Location = new System.Drawing.Point(3, 55);
            this.plC_Button_READY.Name = "plC_Button_READY";
            this.plC_Button_READY.OFF_文字內容 = "READY";
            this.plC_Button_READY.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_READY.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_READY.ON_文字內容 = "READY";
            this.plC_Button_READY.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_READY.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_READY.Size = new System.Drawing.Size(97, 46);
            this.plC_Button_READY.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_READY.TabIndex = 1;
            this.plC_Button_READY.字型鎖住 = false;
            this.plC_Button_READY.按鈕型態 = MyUI.PLC_Button.StatusEnum.保持型;
            this.plC_Button_READY.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_READY.文字鎖住 = false;
            this.plC_Button_READY.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_READY.狀態OFF圖片")));
            this.plC_Button_READY.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_READY.狀態ON圖片")));
            this.plC_Button_READY.讀寫鎖住 = false;
            this.plC_Button_READY.音效 = true;
            this.plC_Button_READY.顯示 = false;
            this.plC_Button_READY.顯示狀態 = false;
            // 
            // plC_Button_TRIGGER
            // 
            this.plC_Button_TRIGGER.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_TRIGGER.Location = new System.Drawing.Point(106, 55);
            this.plC_Button_TRIGGER.Name = "plC_Button_TRIGGER";
            this.plC_Button_TRIGGER.OFF_文字內容 = "TRIGGER";
            this.plC_Button_TRIGGER.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_TRIGGER.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_TRIGGER.ON_文字內容 = "TRIGGER";
            this.plC_Button_TRIGGER.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_TRIGGER.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_TRIGGER.Size = new System.Drawing.Size(97, 46);
            this.plC_Button_TRIGGER.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_TRIGGER.TabIndex = 0;
            this.plC_Button_TRIGGER.字型鎖住 = false;
            this.plC_Button_TRIGGER.按鈕型態 = MyUI.PLC_Button.StatusEnum.保持型;
            this.plC_Button_TRIGGER.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_TRIGGER.文字鎖住 = false;
            this.plC_Button_TRIGGER.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_TRIGGER.狀態OFF圖片")));
            this.plC_Button_TRIGGER.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_TRIGGER.狀態ON圖片")));
            this.plC_Button_TRIGGER.讀寫鎖住 = false;
            this.plC_Button_TRIGGER.音效 = true;
            this.plC_Button_TRIGGER.顯示 = false;
            this.plC_Button_TRIGGER.顯示狀態 = false;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.plC_NumBox_序號);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(129, 143);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(209, 39);
            this.panel3.TabIndex = 4;
            // 
            // plC_NumBox_序號
            // 
            this.plC_NumBox_序號.Font = new System.Drawing.Font("新細明體", 12F);
            this.plC_NumBox_序號.Location = new System.Drawing.Point(53, 5);
            this.plC_NumBox_序號.Name = "plC_NumBox_序號";
            this.plC_NumBox_序號.ReadOnly = false;
            this.plC_NumBox_序號.Size = new System.Drawing.Size(148, 27);
            this.plC_NumBox_序號.TabIndex = 1;
            this.plC_NumBox_序號.Text = "0";
            this.plC_NumBox_序號.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_序號.密碼欄位 = false;
            this.plC_NumBox_序號.小數點位置 = 0;
            this.plC_NumBox_序號.音效 = true;
            this.plC_NumBox_序號.顯示螢幕小鍵盤 = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "序號";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.plC_NumBox_ActiveSurfaceHandle);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Location = new System.Drawing.Point(129, 188);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(209, 64);
            this.panel4.TabIndex = 5;
            // 
            // plC_NumBox_ActiveSurfaceHandle
            // 
            this.plC_NumBox_ActiveSurfaceHandle.Font = new System.Drawing.Font("新細明體", 12F);
            this.plC_NumBox_ActiveSurfaceHandle.Location = new System.Drawing.Point(10, 31);
            this.plC_NumBox_ActiveSurfaceHandle.Name = "plC_NumBox_ActiveSurfaceHandle";
            this.plC_NumBox_ActiveSurfaceHandle.ReadOnly = true;
            this.plC_NumBox_ActiveSurfaceHandle.Size = new System.Drawing.Size(191, 27);
            this.plC_NumBox_ActiveSurfaceHandle.TabIndex = 1;
            this.plC_NumBox_ActiveSurfaceHandle.Text = "0";
            this.plC_NumBox_ActiveSurfaceHandle.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.雙字元;
            this.plC_NumBox_ActiveSurfaceHandle.密碼欄位 = false;
            this.plC_NumBox_ActiveSurfaceHandle.小數點位置 = 0;
            this.plC_NumBox_ActiveSurfaceHandle.音效 = true;
            this.plC_NumBox_ActiveSurfaceHandle.顯示螢幕小鍵盤 = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F);
            this.label2.Location = new System.Drawing.Point(8, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "ActiveSurfaceHandle";
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.plC_NumBox_取像時間);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Location = new System.Drawing.Point(413, 213);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(209, 39);
            this.panel5.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F);
            this.label4.Location = new System.Drawing.Point(166, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "ms";
            // 
            // plC_NumBox_取像時間
            // 
            this.plC_NumBox_取像時間.Font = new System.Drawing.Font("新細明體", 12F);
            this.plC_NumBox_取像時間.Location = new System.Drawing.Point(88, 5);
            this.plC_NumBox_取像時間.Name = "plC_NumBox_取像時間";
            this.plC_NumBox_取像時間.ReadOnly = true;
            this.plC_NumBox_取像時間.Size = new System.Drawing.Size(70, 27);
            this.plC_NumBox_取像時間.TabIndex = 1;
            this.plC_NumBox_取像時間.Text = "0";
            this.plC_NumBox_取像時間.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_取像時間.密碼欄位 = false;
            this.plC_NumBox_取像時間.小數點位置 = 0;
            this.plC_NumBox_取像時間.音效 = true;
            this.plC_NumBox_取像時間.顯示螢幕小鍵盤 = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F);
            this.label3.Location = new System.Drawing.Point(7, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "取像時間";
            // 
            // plC_TrackBarHorizontal_視訊增益
            // 
            this.plC_TrackBarHorizontal_視訊增益.Location = new System.Drawing.Point(344, 123);
            this.plC_TrackBarHorizontal_視訊增益.Name = "plC_TrackBarHorizontal_視訊增益";
            this.plC_TrackBarHorizontal_視訊增益.Size = new System.Drawing.Size(278, 48);
            this.plC_TrackBarHorizontal_視訊增益.TabIndex = 8;
            this.plC_TrackBarHorizontal_視訊增益.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.plC_TrackBarHorizontal_視訊增益.刻度最大值 = 10;
            this.plC_TrackBarHorizontal_視訊增益.刻度最小值 = 0;
            this.plC_TrackBarHorizontal_視訊增益.刻度間隔 = 1;
            this.plC_TrackBarHorizontal_視訊增益.小數點位置 = 0;
            this.plC_TrackBarHorizontal_視訊增益.數值字體 = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_視訊增益.標題內容 = "視訊增益";
            this.plC_TrackBarHorizontal_視訊增益.標題字體 = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_視訊增益.顯示數值 = true;
            this.plC_TrackBarHorizontal_視訊增益.顯示標題 = true;
            // 
            // plC_TrackBarHorizontal_電子快門
            // 
            this.plC_TrackBarHorizontal_電子快門.Location = new System.Drawing.Point(344, 63);
            this.plC_TrackBarHorizontal_電子快門.Name = "plC_TrackBarHorizontal_電子快門";
            this.plC_TrackBarHorizontal_電子快門.Size = new System.Drawing.Size(278, 48);
            this.plC_TrackBarHorizontal_電子快門.TabIndex = 7;
            this.plC_TrackBarHorizontal_電子快門.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.plC_TrackBarHorizontal_電子快門.刻度最大值 = 2048;
            this.plC_TrackBarHorizontal_電子快門.刻度最小值 = 0;
            this.plC_TrackBarHorizontal_電子快門.刻度間隔 = 50;
            this.plC_TrackBarHorizontal_電子快門.小數點位置 = 0;
            this.plC_TrackBarHorizontal_電子快門.數值字體 = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_電子快門.標題內容 = "電子快門";
            this.plC_TrackBarHorizontal_電子快門.標題字體 = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_電子快門.顯示數值 = true;
            this.plC_TrackBarHorizontal_電子快門.顯示標題 = true;
            // 
            // plC_TrackBarHorizontal_光源亮度
            // 
            this.plC_TrackBarHorizontal_光源亮度.Location = new System.Drawing.Point(344, 5);
            this.plC_TrackBarHorizontal_光源亮度.Name = "plC_TrackBarHorizontal_光源亮度";
            this.plC_TrackBarHorizontal_光源亮度.Size = new System.Drawing.Size(278, 48);
            this.plC_TrackBarHorizontal_光源亮度.TabIndex = 6;
            this.plC_TrackBarHorizontal_光源亮度.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.plC_TrackBarHorizontal_光源亮度.刻度最大值 = 450;
            this.plC_TrackBarHorizontal_光源亮度.刻度最小值 = 0;
            this.plC_TrackBarHorizontal_光源亮度.刻度間隔 = 20;
            this.plC_TrackBarHorizontal_光源亮度.小數點位置 = 0;
            this.plC_TrackBarHorizontal_光源亮度.數值字體 = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_光源亮度.標題內容 = "光源亮度";
            this.plC_TrackBarHorizontal_光源亮度.標題字體 = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_光源亮度.顯示數值 = true;
            this.plC_TrackBarHorizontal_光源亮度.顯示標題 = true;
            // 
            // H_AltairUDrv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.plC_TrackBarHorizontal_視訊增益);
            this.Controls.Add(this.plC_TrackBarHorizontal_電子快門);
            this.Controls.Add(this.plC_TrackBarHorizontal_光源亮度);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_CCD);
            this.Name = "H_AltairUDrv";
            this.Size = new System.Drawing.Size(625, 256);
            this.Load += new System.EventHandler(this.H_AltairUDrv_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_CCD;
        private System.Windows.Forms.CheckBox checkBox_水平翻轉;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox_亮度補償;
        private System.Windows.Forms.CheckBox checkBox_長曝光模式;
        private System.Windows.Forms.CheckBox checkBox_垂直翻轉;
        private System.Windows.Forms.Panel panel2;
        private MyUI.PLC_Button plC_Button_相機初始化;
        private MyUI.PLC_Button plC_Button_相機已建立;
        private MyUI.PLC_Button plC_Button_READY;
        private MyUI.PLC_Button plC_Button_TRIGGER;
        private System.Windows.Forms.Panel panel3;
        private MyUI.PLC_NumBox plC_NumBox_序號;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private MyUI.PLC_NumBox plC_NumBox_ActiveSurfaceHandle;
        private System.Windows.Forms.Label label2;
        private MyUI.PLC_TrackBarHorizontal plC_TrackBarHorizontal_光源亮度;
        private MyUI.PLC_TrackBarHorizontal plC_TrackBarHorizontal_電子快門;
        private MyUI.PLC_TrackBarHorizontal plC_TrackBarHorizontal_視訊增益;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label4;
        private MyUI.PLC_NumBox plC_NumBox_取像時間;
        private System.Windows.Forms.Label label3;



    }
}
