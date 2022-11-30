namespace MyUI
{
    partial class SplashScreen
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
            this.panel_Logo = new System.Windows.Forms.Panel();
            this.label_Title = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_Logo.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Logo
            // 
            this.panel_Logo.BackgroundImage = global::MyUI.Resource1.logo_1;
            this.panel_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_Logo.Controls.Add(this.panel1);
            this.panel_Logo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Logo.Location = new System.Drawing.Point(0, 0);
            this.panel_Logo.Name = "panel_Logo";
            this.panel_Logo.Size = new System.Drawing.Size(460, 444);
            this.panel_Logo.TabIndex = 0;
            // 
            // label_Title
            // 
            this.label_Title.BackColor = System.Drawing.Color.Transparent;
            this.label_Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Title.Font = new System.Drawing.Font("標楷體", 20F, System.Drawing.FontStyle.Bold);
            this.label_Title.Location = new System.Drawing.Point(0, 0);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(460, 75);
            this.label_Title.TabIndex = 1;
            this.label_Title.Text = "鴻森整合機電有限公司";
            this.label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 444);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(460, 38);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(226, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "正在努力的載入程式，請稍後...";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label_Title);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 369);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 75);
            this.panel1.TabIndex = 2;
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(460, 482);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel_Logo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "SplashScreen";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SplashScreen_FormClosing);
            this.Shown += new System.EventHandler(this.SplashScreen_Shown);
            this.panel_Logo.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Logo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Panel panel1;
    }
}