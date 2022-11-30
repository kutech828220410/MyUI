namespace GPP編譯軟體__鴻森整合機電有限公司
{
    partial class Form_main
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_main));
            this.laddeR_Panel = new LadderUI.LADDER_Panel();
            this.SuspendLayout();
            // 
            // laddeR_Panel
            // 
            this.laddeR_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.laddeR_Panel.Location = new System.Drawing.Point(0, 0);
            this.laddeR_Panel.Name = "laddeR_Panel";
            this.laddeR_Panel.Size = new System.Drawing.Size(1632, 1044);
            this.laddeR_Panel.TabIndex = 0;
            // 
            // Form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1632, 1044);
            this.Controls.Add(this.laddeR_Panel);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_main";
            this.Text = "PLCEditor";
            this.Load += new System.EventHandler(this.Form_main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private LadderUI.LADDER_Panel laddeR_Panel;
    }
}

