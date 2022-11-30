namespace MyUI
{
    partial class PLC_SaveDeviceButtom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PLC_SaveDeviceButtom));
            this.SuspendLayout();
            // 
            // PLC_SaveDeviceButtom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "PLC_SaveDeviceButtom";
            this.OFF_文字內容 = "SAVE";
            this.ON_文字內容 = "SAVE";
            this.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("$this.狀態OFF圖片")));
            this.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("$this.狀態ON圖片")));
            this.ResumeLayout(false);

        }

        #endregion
    }
}
