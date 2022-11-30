namespace Basic
{
    partial class Dialog_Loading
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
            this.button_Cancel = new System.Windows.Forms.Button();
            this.rJ_ProgressBar = new MyUI.RJ_ProgressBar();
            this.SuspendLayout();
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(332, 14);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 37);
            this.button_Cancel.TabIndex = 4;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // rJ_ProgressBar
            // 
            this.rJ_ProgressBar.ChannelColor = System.Drawing.Color.LightSkyBlue;
            this.rJ_ProgressBar.ChannelHeight = 20;
            this.rJ_ProgressBar.ForeBackColor = System.Drawing.Color.RoyalBlue;
            this.rJ_ProgressBar.ForeColor = System.Drawing.Color.White;
            this.rJ_ProgressBar.Location = new System.Drawing.Point(12, 12);
            this.rJ_ProgressBar.Name = "rJ_ProgressBar";
            this.rJ_ProgressBar.ShowMaximun = true;
            this.rJ_ProgressBar.ShowValue = MyUI.TextPosition.Left;
            this.rJ_ProgressBar.Size = new System.Drawing.Size(314, 39);
            this.rJ_ProgressBar.SliderColor = System.Drawing.Color.RoyalBlue;
            this.rJ_ProgressBar.SliderHeight = 10;
            this.rJ_ProgressBar.SymbolAfter = "";
            this.rJ_ProgressBar.SymbolBefore = "";
            this.rJ_ProgressBar.TabIndex = 5;
            // 
            // Dialog_Loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(415, 63);
            this.ControlBox = false;
            this.Controls.Add(this.rJ_ProgressBar);
            this.Controls.Add(this.button_Cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Dialog_Loading";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Dialog_Loading_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_Cancel;
        private MyUI.RJ_ProgressBar rJ_ProgressBar;
    }
}