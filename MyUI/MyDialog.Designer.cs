
namespace MyUI
{
    partial class MyDialog
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
            this.SuspendLayout();
            // 
            // MyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CanResize = false;
            this.CaptionFont = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Bold);
            this.ClientSize = new System.Drawing.Size(850, 572);
            this.CloseBoxSize = new System.Drawing.Size(32, 24);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaxSize = new System.Drawing.Size(32, 24);
            this.MinimizeBox = false;
            this.MiniSize = new System.Drawing.Size(32, 24);
            this.Name = "MyDialog";
            this.Radius = 10;
            this.ShadowWidth = 15;
            this.ShowDrawIcon = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TitleOffset = new System.Drawing.Point(20, 5);
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
    }
}