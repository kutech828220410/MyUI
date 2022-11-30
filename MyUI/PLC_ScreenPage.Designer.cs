namespace MyUI
{
    partial class PLC_ScreenPage
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
            this.SuspendLayout();
            // 
            // PLC_ScreenPage
            // 
            this.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Size = new System.Drawing.Size(491, 410);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.PLC_ScreenPage_ControlAdded);
            this.ResumeLayout(false);

        }

        #endregion


    }
}
