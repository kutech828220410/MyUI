namespace MVSDKUI
{
    partial class PLC_MindVision_Camera_UI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PLC_MindVision_Camera_UI));
            this.plC_TrackBarHorizontal_VGain = new MyUI.PLC_TrackBarHorizontal();
            this.plC_TrackBarHorizontal_Sharpness = new MyUI.PLC_TrackBarHorizontal();
            this.plC_TrackBarHorizontal_EISutter = new MyUI.PLC_TrackBarHorizontal();
            this.Camera = new MVSDKUI.MindVision_Camera_UI();
            this.label4 = new System.Windows.Forms.Label();
            this.plC_NumBox_取像時間 = new MyUI.PLC_NumBox();
            this.plC_Button_READY = new MyUI.PLC_Button();
            this.plC_Button_TRIGGER = new MyUI.PLC_Button();
            this.plC_Button_IsConneted = new MyUI.PLC_Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.plC_NumBox_ActiveSurfaceHandle = new MyUI.PLC_NumBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_LIVE = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_Control = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel_Control.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // plC_TrackBarHorizontal_VGain
            // 
            this.plC_TrackBarHorizontal_VGain.Location = new System.Drawing.Point(5, 60);
            this.plC_TrackBarHorizontal_VGain.Name = "plC_TrackBarHorizontal_VGain";
            this.plC_TrackBarHorizontal_VGain.Size = new System.Drawing.Size(278, 51);
            this.plC_TrackBarHorizontal_VGain.TabIndex = 3;
            this.plC_TrackBarHorizontal_VGain.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.plC_TrackBarHorizontal_VGain.Value = 0;
            this.plC_TrackBarHorizontal_VGain.刻度最大值 = 100;
            this.plC_TrackBarHorizontal_VGain.刻度最小值 = -100;
            this.plC_TrackBarHorizontal_VGain.刻度間隔 = 10;
            this.plC_TrackBarHorizontal_VGain.小數點位置 = 0;
            this.plC_TrackBarHorizontal_VGain.數值字體 = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_VGain.標題內容 = "VGain";
            this.plC_TrackBarHorizontal_VGain.標題字體 = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_VGain.顯示數值 = true;
            this.plC_TrackBarHorizontal_VGain.顯示標題 = true;
            // 
            // plC_TrackBarHorizontal_Sharpness
            // 
            this.plC_TrackBarHorizontal_Sharpness.Location = new System.Drawing.Point(5, 117);
            this.plC_TrackBarHorizontal_Sharpness.Name = "plC_TrackBarHorizontal_Sharpness";
            this.plC_TrackBarHorizontal_Sharpness.Size = new System.Drawing.Size(278, 51);
            this.plC_TrackBarHorizontal_Sharpness.TabIndex = 2;
            this.plC_TrackBarHorizontal_Sharpness.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.plC_TrackBarHorizontal_Sharpness.Value = 0;
            this.plC_TrackBarHorizontal_Sharpness.刻度最大值 = 100;
            this.plC_TrackBarHorizontal_Sharpness.刻度最小值 = 0;
            this.plC_TrackBarHorizontal_Sharpness.刻度間隔 = 10;
            this.plC_TrackBarHorizontal_Sharpness.小數點位置 = 0;
            this.plC_TrackBarHorizontal_Sharpness.數值字體 = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_Sharpness.標題內容 = "Sharpness";
            this.plC_TrackBarHorizontal_Sharpness.標題字體 = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_Sharpness.顯示數值 = true;
            this.plC_TrackBarHorizontal_Sharpness.顯示標題 = true;
            // 
            // plC_TrackBarHorizontal_EISutter
            // 
            this.plC_TrackBarHorizontal_EISutter.Location = new System.Drawing.Point(5, 3);
            this.plC_TrackBarHorizontal_EISutter.Name = "plC_TrackBarHorizontal_EISutter";
            this.plC_TrackBarHorizontal_EISutter.Size = new System.Drawing.Size(278, 51);
            this.plC_TrackBarHorizontal_EISutter.TabIndex = 1;
            this.plC_TrackBarHorizontal_EISutter.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.plC_TrackBarHorizontal_EISutter.Value = 0;
            this.plC_TrackBarHorizontal_EISutter.刻度最大值 = 1024000;
            this.plC_TrackBarHorizontal_EISutter.刻度最小值 = 0;
            this.plC_TrackBarHorizontal_EISutter.刻度間隔 = 1000;
            this.plC_TrackBarHorizontal_EISutter.小數點位置 = 3;
            this.plC_TrackBarHorizontal_EISutter.數值字體 = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_EISutter.標題內容 = "EIShuter";
            this.plC_TrackBarHorizontal_EISutter.標題字體 = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_TrackBarHorizontal_EISutter.顯示數值 = true;
            this.plC_TrackBarHorizontal_EISutter.顯示標題 = true;
            // 
            // Camera
            // 
            this.Camera.CameraName = "CCD00";
            this.Camera.Dock = System.Windows.Forms.DockStyle.Left;
            this.Camera.ImageDepth = MVSDKUI.MindVision_Camera_UI.enum_ImageDepth._8Bit;
            this.Camera.Location = new System.Drawing.Point(0, 0);
            this.Camera.Name = "Camera";
            this.Camera.Size = new System.Drawing.Size(607, 507);
            this.Camera.StreamIsSuspend = false;
            this.Camera.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F);
            this.label4.Location = new System.Drawing.Point(96, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "ms";
            // 
            // plC_NumBox_取像時間
            // 
            this.plC_NumBox_取像時間.Font = new System.Drawing.Font("新細明體", 12F);
            this.plC_NumBox_取像時間.Location = new System.Drawing.Point(18, 92);
            this.plC_NumBox_取像時間.mBackColor = System.Drawing.SystemColors.Control;
            this.plC_NumBox_取像時間.mForeColor = System.Drawing.SystemColors.WindowText;
            this.plC_NumBox_取像時間.Name = "plC_NumBox_取像時間";
            this.plC_NumBox_取像時間.ReadOnly = true;
            this.plC_NumBox_取像時間.Size = new System.Drawing.Size(72, 27);
            this.plC_NumBox_取像時間.TabIndex = 1;
            this.plC_NumBox_取像時間.Value = 0;
            this.plC_NumBox_取像時間.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.單字元;
            this.plC_NumBox_取像時間.密碼欄位 = false;
            this.plC_NumBox_取像時間.小數點位置 = 0;
            this.plC_NumBox_取像時間.微調數值 = 1;
            this.plC_NumBox_取像時間.音效 = true;
            this.plC_NumBox_取像時間.顯示微調按鈕 = false;
            this.plC_NumBox_取像時間.顯示螢幕小鍵盤 = false;
            // 
            // plC_Button_READY
            // 
            this.plC_Button_READY.Bool = false;
            this.plC_Button_READY.but_press = false;
            this.plC_Button_READY.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_READY.Location = new System.Drawing.Point(99, 3);
            this.plC_Button_READY.Name = "plC_Button_READY";
            this.plC_Button_READY.OFF_文字內容 = "READY";
            this.plC_Button_READY.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_READY.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_READY.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_READY.ON_文字內容 = "READY";
            this.plC_Button_READY.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_READY.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_READY.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_READY.Size = new System.Drawing.Size(78, 46);
            this.plC_Button_READY.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_READY.TabIndex = 12;
            this.plC_Button_READY.事件驅動 = false;
            this.plC_Button_READY.字型鎖住 = false;
            this.plC_Button_READY.按鈕型態 = MyUI.PLC_Button.StatusEnum.交替型;
            this.plC_Button_READY.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_READY.文字鎖住 = false;
            this.plC_Button_READY.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_READY.狀態OFF圖片")));
            this.plC_Button_READY.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_READY.狀態ON圖片")));
            this.plC_Button_READY.讀取位元反向 = false;
            this.plC_Button_READY.讀寫鎖住 = false;
            this.plC_Button_READY.起始狀態 = false;
            this.plC_Button_READY.音效 = true;
            this.plC_Button_READY.顯示 = false;
            this.plC_Button_READY.顯示狀態 = false;
            // 
            // plC_Button_TRIGGER
            // 
            this.plC_Button_TRIGGER.Bool = false;
            this.plC_Button_TRIGGER.but_press = false;
            this.plC_Button_TRIGGER.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_TRIGGER.Location = new System.Drawing.Point(3, 3);
            this.plC_Button_TRIGGER.Name = "plC_Button_TRIGGER";
            this.plC_Button_TRIGGER.OFF_文字內容 = "TRIGGER";
            this.plC_Button_TRIGGER.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_TRIGGER.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_TRIGGER.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_TRIGGER.ON_文字內容 = "TRIGGER";
            this.plC_Button_TRIGGER.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_TRIGGER.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_TRIGGER.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_TRIGGER.Size = new System.Drawing.Size(90, 46);
            this.plC_Button_TRIGGER.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_TRIGGER.TabIndex = 11;
            this.plC_Button_TRIGGER.事件驅動 = false;
            this.plC_Button_TRIGGER.字型鎖住 = false;
            this.plC_Button_TRIGGER.按鈕型態 = MyUI.PLC_Button.StatusEnum.交替型;
            this.plC_Button_TRIGGER.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_TRIGGER.文字鎖住 = false;
            this.plC_Button_TRIGGER.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_TRIGGER.狀態OFF圖片")));
            this.plC_Button_TRIGGER.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_TRIGGER.狀態ON圖片")));
            this.plC_Button_TRIGGER.讀取位元反向 = false;
            this.plC_Button_TRIGGER.讀寫鎖住 = false;
            this.plC_Button_TRIGGER.起始狀態 = false;
            this.plC_Button_TRIGGER.音效 = true;
            this.plC_Button_TRIGGER.顯示 = false;
            this.plC_Button_TRIGGER.顯示狀態 = false;
            // 
            // plC_Button_IsConneted
            // 
            this.plC_Button_IsConneted.Bool = false;
            this.plC_Button_IsConneted.but_press = false;
            this.plC_Button_IsConneted.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_Button_IsConneted.Location = new System.Drawing.Point(183, 3);
            this.plC_Button_IsConneted.Name = "plC_Button_IsConneted";
            this.plC_Button_IsConneted.OFF_文字內容 = "IsConneted";
            this.plC_Button_IsConneted.OFF_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_IsConneted.OFF_文字顏色 = System.Drawing.Color.Black;
            this.plC_Button_IsConneted.OFF_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_IsConneted.ON_文字內容 = "IsConneted";
            this.plC_Button_IsConneted.ON_文字字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.plC_Button_IsConneted.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_Button_IsConneted.ON_背景顏色 = System.Drawing.SystemColors.Control;
            this.plC_Button_IsConneted.Size = new System.Drawing.Size(98, 46);
            this.plC_Button_IsConneted.Style = MyUI.PLC_Button.StyleEnum.經典;
            this.plC_Button_IsConneted.TabIndex = 13;
            this.plC_Button_IsConneted.事件驅動 = false;
            this.plC_Button_IsConneted.字型鎖住 = false;
            this.plC_Button_IsConneted.按鈕型態 = MyUI.PLC_Button.StatusEnum.交替型;
            this.plC_Button_IsConneted.按鍵方式 = MyUI.PLC_Button.PressEnum.Mouse_左鍵;
            this.plC_Button_IsConneted.文字鎖住 = false;
            this.plC_Button_IsConneted.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_IsConneted.狀態OFF圖片")));
            this.plC_Button_IsConneted.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("plC_Button_IsConneted.狀態ON圖片")));
            this.plC_Button_IsConneted.讀取位元反向 = false;
            this.plC_Button_IsConneted.讀寫鎖住 = false;
            this.plC_Button_IsConneted.起始狀態 = false;
            this.plC_Button_IsConneted.音效 = true;
            this.plC_Button_IsConneted.顯示 = false;
            this.plC_Button_IsConneted.顯示狀態 = false;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.plC_NumBox_ActiveSurfaceHandle);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Location = new System.Drawing.Point(123, 55);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(158, 64);
            this.panel4.TabIndex = 14;
            // 
            // plC_NumBox_ActiveSurfaceHandle
            // 
            this.plC_NumBox_ActiveSurfaceHandle.Font = new System.Drawing.Font("新細明體", 12F);
            this.plC_NumBox_ActiveSurfaceHandle.Location = new System.Drawing.Point(10, 31);
            this.plC_NumBox_ActiveSurfaceHandle.mBackColor = System.Drawing.SystemColors.Control;
            this.plC_NumBox_ActiveSurfaceHandle.mForeColor = System.Drawing.SystemColors.WindowText;
            this.plC_NumBox_ActiveSurfaceHandle.Name = "plC_NumBox_ActiveSurfaceHandle";
            this.plC_NumBox_ActiveSurfaceHandle.ReadOnly = true;
            this.plC_NumBox_ActiveSurfaceHandle.Size = new System.Drawing.Size(138, 27);
            this.plC_NumBox_ActiveSurfaceHandle.TabIndex = 1;
            this.plC_NumBox_ActiveSurfaceHandle.Value = 0;
            this.plC_NumBox_ActiveSurfaceHandle.字元長度 = MyUI.PLC_NumBox.WordLengthEnum.雙字元;
            this.plC_NumBox_ActiveSurfaceHandle.密碼欄位 = false;
            this.plC_NumBox_ActiveSurfaceHandle.小數點位置 = 0;
            this.plC_NumBox_ActiveSurfaceHandle.微調數值 = 1;
            this.plC_NumBox_ActiveSurfaceHandle.音效 = true;
            this.plC_NumBox_ActiveSurfaceHandle.顯示微調按鈕 = false;
            this.plC_NumBox_ActiveSurfaceHandle.顯示螢幕小鍵盤 = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F);
            this.label2.Location = new System.Drawing.Point(7, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "ActiveSurfaceHandle";
            // 
            // checkBox_LIVE
            // 
            this.checkBox_LIVE.AutoSize = true;
            this.checkBox_LIVE.Location = new System.Drawing.Point(67, 64);
            this.checkBox_LIVE.Name = "checkBox_LIVE";
            this.checkBox_LIVE.Size = new System.Drawing.Size(50, 16);
            this.checkBox_LIVE.TabIndex = 15;
            this.checkBox_LIVE.Text = "LIVE";
            this.checkBox_LIVE.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.plC_TrackBarHorizontal_EISutter);
            this.panel1.Controls.Add(this.plC_TrackBarHorizontal_Sharpness);
            this.panel1.Controls.Add(this.plC_TrackBarHorizontal_VGain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 182);
            this.panel1.TabIndex = 16;
            // 
            // panel_Control
            // 
            this.panel_Control.Controls.Add(this.plC_Button_TRIGGER);
            this.panel_Control.Controls.Add(this.plC_Button_READY);
            this.panel_Control.Controls.Add(this.label4);
            this.panel_Control.Controls.Add(this.plC_Button_IsConneted);
            this.panel_Control.Controls.Add(this.checkBox_LIVE);
            this.panel_Control.Controls.Add(this.panel4);
            this.panel_Control.Controls.Add(this.plC_NumBox_取像時間);
            this.panel_Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Control.Location = new System.Drawing.Point(0, 182);
            this.panel_Control.Name = "panel_Control";
            this.panel_Control.Size = new System.Drawing.Size(292, 325);
            this.panel_Control.TabIndex = 17;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel_Control);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(607, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(292, 507);
            this.panel3.TabIndex = 18;
            // 
            // PLC_MindVision_Camera_UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.Camera);
            this.Name = "PLC_MindVision_Camera_UI";
            this.Size = new System.Drawing.Size(899, 507);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel_Control.ResumeLayout(false);
            this.panel_Control.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MyUI.PLC_TrackBarHorizontal plC_TrackBarHorizontal_EISutter;
        private MyUI.PLC_TrackBarHorizontal plC_TrackBarHorizontal_Sharpness;
        private MyUI.PLC_TrackBarHorizontal plC_TrackBarHorizontal_VGain;
        private System.Windows.Forms.Label label4;
        private MyUI.PLC_NumBox plC_NumBox_取像時間;
        private MyUI.PLC_Button plC_Button_READY;
        private MyUI.PLC_Button plC_Button_TRIGGER;
        private MyUI.PLC_Button plC_Button_IsConneted;
        private System.Windows.Forms.Panel panel4;
        private MyUI.PLC_NumBox plC_NumBox_ActiveSurfaceHandle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_LIVE;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_Control;
        private System.Windows.Forms.Panel panel3;
        public MindVision_Camera_UI Camera;
    }
}
