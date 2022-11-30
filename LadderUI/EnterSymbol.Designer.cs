namespace LadderForm
{
    partial class EnterSymbol
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
            this.button_指令輸入完成 = new System.Windows.Forms.Button();
            this.button_離開指令輸入 = new System.Windows.Forms.Button();
            this.timer_程序執行 = new System.Windows.Forms.Timer(this.components);
            this.textBox_指令輸入 = new System.Windows.Forms.TextBox();
            this.label_Devicename = new System.Windows.Forms.Label();
            this.panel_devicename = new System.Windows.Forms.Panel();
            this.panel_devicename.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_指令輸入完成
            // 
            this.button_指令輸入完成.Location = new System.Drawing.Point(305, 7);
            this.button_指令輸入完成.Name = "button_指令輸入完成";
            this.button_指令輸入完成.Size = new System.Drawing.Size(47, 31);
            this.button_指令輸入完成.TabIndex = 0;
            this.button_指令輸入完成.Text = "OK";
            this.button_指令輸入完成.UseVisualStyleBackColor = true;
            this.button_指令輸入完成.Click += new System.EventHandler(this.button_指令輸入完成_Click);
            // 
            // button_離開指令輸入
            // 
            this.button_離開指令輸入.Location = new System.Drawing.Point(353, 7);
            this.button_離開指令輸入.Name = "button_離開指令輸入";
            this.button_離開指令輸入.Size = new System.Drawing.Size(47, 31);
            this.button_離開指令輸入.TabIndex = 1;
            this.button_離開指令輸入.Text = "Exit";
            this.button_離開指令輸入.UseVisualStyleBackColor = true;
            this.button_離開指令輸入.Click += new System.EventHandler(this.button_離開指令輸入_Click);
            // 
            // timer_程序執行
            // 
            this.timer_程序執行.Interval = 1;
            this.timer_程序執行.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBox_指令輸入
            // 
            this.textBox_指令輸入.Location = new System.Drawing.Point(8, 11);
            this.textBox_指令輸入.Name = "textBox_指令輸入";
            this.textBox_指令輸入.Size = new System.Drawing.Size(291, 22);
            this.textBox_指令輸入.TabIndex = 0;
            this.textBox_指令輸入.TextChanged += new System.EventHandler(this.textBox_指令輸入_TextChanged);
            this.textBox_指令輸入.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_指令輸入_KeyDown);
            this.textBox_指令輸入.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_指令輸入_KeyPress);
            this.textBox_指令輸入.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_指令輸入_KeyUp);
            // 
            // label_Devicename
            // 
            this.label_Devicename.AutoSize = true;
            this.label_Devicename.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label_Devicename.Font = new System.Drawing.Font("新細明體", 9F);
            this.label_Devicename.Location = new System.Drawing.Point(3, 4);
            this.label_Devicename.Name = "label_Devicename";
            this.label_Devicename.Size = new System.Drawing.Size(19, 12);
            this.label_Devicename.TabIndex = 2;
            this.label_Devicename.Text = "X0";
            // 
            // panel_devicename
            // 
            this.panel_devicename.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_devicename.Controls.Add(this.label_Devicename);
            this.panel_devicename.Location = new System.Drawing.Point(3, 10);
            this.panel_devicename.Name = "panel_devicename";
            this.panel_devicename.Size = new System.Drawing.Size(45, 24);
            this.panel_devicename.TabIndex = 3;
            this.panel_devicename.Visible = false;
            // 
            // EnterSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(405, 43);
            this.Controls.Add(this.panel_devicename);
            this.Controls.Add(this.textBox_指令輸入);
            this.Controls.Add(this.button_離開指令輸入);
            this.Controls.Add(this.button_指令輸入完成);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(421, 82);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(421, 82);
            this.Name = "EnterSymbol";
            this.ShowIcon = false;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EnterSymbol_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EnterSymbol_FormClosed);
            this.Load += new System.EventHandler(this.EnterSymbol_Load);
            this.panel_devicename.ResumeLayout(false);
            this.panel_devicename.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_指令輸入完成;
        private System.Windows.Forms.Button button_離開指令輸入;
        private System.Windows.Forms.Timer timer_程序執行;
        private System.Windows.Forms.TextBox textBox_指令輸入;
        private System.Windows.Forms.Label label_Devicename;
        private System.Windows.Forms.Panel panel_devicename;
    }
}