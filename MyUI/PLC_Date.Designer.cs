namespace MyUI
{
    partial class PLC_Date
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
            this.label_Time = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Time
            // 
            this.label_Time.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Time.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_Time.Location = new System.Drawing.Point(0, 0);
            this.label_Time.Name = "label_Time";
            this.label_Time.Size = new System.Drawing.Size(197, 35);
            this.label_Time.TabIndex = 0;
            this.label_Time.Text = "0000/00/00 00:00:00";
            this.label_Time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PLC_Date
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.label_Time);
            this.Name = "PLC_Date";
            this.Size = new System.Drawing.Size(197, 35);
            this.Load += new System.EventHandler(this.PLC_Date_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_Time;
    }
}
