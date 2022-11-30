namespace LadderForm
{
    partial class TransferSetup
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
            this.topMachine_Panel = new LadderUI.TopMachine_Panel();
            this.SuspendLayout();
            // 
            // topMachine_Panel
            // 
            this.topMachine_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topMachine_Panel.Location = new System.Drawing.Point(0, 0);
            this.topMachine_Panel.Name = "topMachine_Panel";
            this.topMachine_Panel.Size = new System.Drawing.Size(872, 580);
            this.topMachine_Panel.TabIndex = 0;
            // 
            // TransferSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 580);
            this.Controls.Add(this.topMachine_Panel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransferSetup";
            this.ShowIcon = false;
            this.Text = "TranferSetup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TransferSetup_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TransferSetup_FormClosed);
            this.Load += new System.EventHandler(this.TransferSetup_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public LadderUI.TopMachine_Panel topMachine_Panel;
    }
}