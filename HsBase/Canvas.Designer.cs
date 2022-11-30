namespace HsBase
{
    partial class Canvas
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
            this.pictureBox_basic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_basic)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_basic
            // 
            this.pictureBox_basic.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox_basic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_basic.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_basic.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_basic.Name = "pictureBox_basic";
            this.pictureBox_basic.Size = new System.Drawing.Size(300, 300);
            this.pictureBox_basic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_basic.TabIndex = 0;
            this.pictureBox_basic.TabStop = false;
            this.pictureBox_basic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_basic_MouseDown);
            this.pictureBox_basic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_basic_MouseMove);
            this.pictureBox_basic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_basic_MouseUp);
            // 
            // Canvas
            // 
            this.AutoScroll = true;
            this.Controls.Add(this.pictureBox_basic);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Canvas";
            this.Size = new System.Drawing.Size(300, 300);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_basic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_basic;
    }
}
