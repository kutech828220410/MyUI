namespace MyUI
{
    partial class PLC_AlarmFlow
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
            this.label_Alarm = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Alarm
            // 
            this.label_Alarm.BackColor = System.Drawing.Color.Red;
            this.label_Alarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Alarm.Location = new System.Drawing.Point(0, 0);
            this.label_Alarm.Name = "label_Alarm";
            this.label_Alarm.Size = new System.Drawing.Size(515, 24);
            this.label_Alarm.TabIndex = 0;
            // 
            // PLC_AlarmFlow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_Alarm);
            this.Name = "PLC_AlarmFlow";
            this.Size = new System.Drawing.Size(515, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_Alarm;

    }
}
