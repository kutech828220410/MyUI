namespace MyUI
{
    partial class PLC_UI_Init
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_Cycletime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_Cycletime
            // 
            this.label_Cycletime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Cycletime.Location = new System.Drawing.Point(0, 0);
            this.label_Cycletime.Name = "label_Cycletime";
            this.label_Cycletime.Size = new System.Drawing.Size(72, 25);
            this.label_Cycletime.TabIndex = 0;
            this.label_Cycletime.Text = "0.000 ms";
            this.label_Cycletime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PLC_UI_Init
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.label_Cycletime);
            this.Name = "PLC_UI_Init";
            this.Size = new System.Drawing.Size(72, 25);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_Cycletime;
    }
}
