namespace MyUI
{
    partial class PLC_ComboBox
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
            // PLC_ComboBox
            // 
            this.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Size = new System.Drawing.Size(149, 20);
            this.DropDown += new System.EventHandler(this.PLC_ComboBox_DropDown);
            this.SelectedIndexChanged += new System.EventHandler(this.PLC_ComboBox_SelectedIndexChanged);
            this.DropDownClosed += new System.EventHandler(this.PLC_ComboBox_DropDownClosed);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
