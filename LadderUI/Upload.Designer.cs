namespace LadderForm
{
    partial class Upload
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.up_Down_load_Panel = new LadderUI.Up_Down_load_Panel();
            this.timer_程序執行 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // up_Down_load_Panel
            // 
            this.up_Down_load_Panel.Location = new System.Drawing.Point(8, 5);
            this.up_Down_load_Panel.Name = "up_Down_load_Panel";
            this.up_Down_load_Panel.Size = new System.Drawing.Size(334, 86);
            this.up_Down_load_Panel.TabIndex = 0;
            // 
            // timer_程序執行
            // 
            this.timer_程序執行.Interval = 10;
            this.timer_程序執行.Tick += new System.EventHandler(this.timer_程序執行_Tick);
            // 
            // Upload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 95);
            this.Controls.Add(this.up_Down_load_Panel);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(360, 134);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(360, 134);
            this.Name = "Upload";
            this.ShowIcon = false;
            this.Text = "Upload";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Upload_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Upload_FormClosed);
            this.Load += new System.EventHandler(this.Upload_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private LadderUI.Up_Down_load_Panel up_Down_load_Panel;
        private System.Windows.Forms.Timer timer_程序執行;
    }
}