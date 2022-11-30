namespace LadderUI
{
    partial class Up_Down_load_Panel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Up_Down_load_Panel));
            this.progressBar_處理進度條 = new System.Windows.Forms.ProgressBar();
            this.exButton_上傳_下載 = new MyUI.ExButton();
            this.exButton_關閉 = new MyUI.ExButton();
            this.checkBox_Comment = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_進度 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar_處理進度條
            // 
            this.progressBar_處理進度條.Location = new System.Drawing.Point(5, 23);
            this.progressBar_處理進度條.MarqueeAnimationSpeed = 10;
            this.progressBar_處理進度條.Name = "progressBar_處理進度條";
            this.progressBar_處理進度條.Size = new System.Drawing.Size(229, 23);
            this.progressBar_處理進度條.TabIndex = 0;
            // 
            // exButton_上傳_下載
            // 
            this.exButton_上傳_下載.Location = new System.Drawing.Point(243, 9);
            this.exButton_上傳_下載.Name = "exButton_上傳_下載";
            this.exButton_上傳_下載.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_上傳_下載.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_上傳_下載.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_上傳_下載.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_上傳_下載.Size = new System.Drawing.Size(83, 31);
            this.exButton_上傳_下載.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_上傳_下載.TabIndex = 1;
            this.exButton_上傳_下載.字型鎖住 = false;
            this.exButton_上傳_下載.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_上傳_下載.文字鎖住 = false;
            this.exButton_上傳_下載.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_上傳_下載.狀態OFF圖片")));
            this.exButton_上傳_下載.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_上傳_下載.狀態ON圖片")));
            this.exButton_上傳_下載.讀寫鎖住 = true;
            this.exButton_上傳_下載.音效 = false;
            this.exButton_上傳_下載.顯示狀態 = false;
            // 
            // exButton_關閉
            // 
            this.exButton_關閉.Location = new System.Drawing.Point(243, 46);
            this.exButton_關閉.Name = "exButton_關閉";
            this.exButton_關閉.OFF_文字內容 = "關閉";
            this.exButton_關閉.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_關閉.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_關閉.ON_文字內容 = "關閉";
            this.exButton_關閉.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_關閉.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_關閉.Size = new System.Drawing.Size(83, 31);
            this.exButton_關閉.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_關閉.TabIndex = 2;
            this.exButton_關閉.字型鎖住 = false;
            this.exButton_關閉.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_關閉.文字鎖住 = false;
            this.exButton_關閉.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_關閉.狀態OFF圖片")));
            this.exButton_關閉.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_關閉.狀態ON圖片")));
            this.exButton_關閉.讀寫鎖住 = true;
            this.exButton_關閉.音效 = false;
            this.exButton_關閉.顯示狀態 = false;
            this.exButton_關閉.btnClick += new System.EventHandler(this.exButton_關閉_btnClick);
            // 
            // checkBox_Comment
            // 
            this.checkBox_Comment.AutoSize = true;
            this.checkBox_Comment.Checked = true;
            this.checkBox_Comment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Comment.Font = new System.Drawing.Font("新細明體", 12F);
            this.checkBox_Comment.Location = new System.Drawing.Point(5, 52);
            this.checkBox_Comment.Name = "checkBox_Comment";
            this.checkBox_Comment.Size = new System.Drawing.Size(88, 20);
            this.checkBox_Comment.TabIndex = 3;
            this.checkBox_Comment.Text = "Comment";
            this.checkBox_Comment.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "%";
            // 
            // label_進度
            // 
            this.label_進度.Location = new System.Drawing.Point(5, 6);
            this.label_進度.Name = "label_進度";
            this.label_進度.Size = new System.Drawing.Size(25, 12);
            this.label_進度.TabIndex = 5;
            this.label_進度.Text = "0";
            this.label_進度.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Up_Down_load_Panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_進度);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_Comment);
            this.Controls.Add(this.exButton_關閉);
            this.Controls.Add(this.exButton_上傳_下載);
            this.Controls.Add(this.progressBar_處理進度條);
            this.Name = "Up_Down_load_Panel";
            this.Size = new System.Drawing.Size(334, 86);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyUI.ExButton exButton_關閉;
        public System.Windows.Forms.ProgressBar progressBar_處理進度條;
        public MyUI.ExButton exButton_上傳_下載;
        public System.Windows.Forms.CheckBox checkBox_Comment;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label_進度;
    }
}
