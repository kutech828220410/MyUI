namespace LadderForm
{
    partial class Verify
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
            this.timer_程序執行 = new System.Windows.Forms.Timer(this.components);
            this.listBox_比較結果 = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.up_Down_load_Panel = new LadderUI.Up_Down_load_Panel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_程序執行
            // 
            this.timer_程序執行.Interval = 10;
            this.timer_程序執行.Tick += new System.EventHandler(this.timer_程序執行_Tick);
            // 
            // listBox_比較結果
            // 
            this.listBox_比較結果.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_比較結果.FormattingEnabled = true;
            this.listBox_比較結果.ItemHeight = 18;
            this.listBox_比較結果.Location = new System.Drawing.Point(6, 21);
            this.listBox_比較結果.Name = "listBox_比較結果";
            this.listBox_比較結果.Size = new System.Drawing.Size(328, 220);
            this.listBox_比較結果.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox_比較結果);
            this.groupBox1.Font = new System.Drawing.Font("新細明體", 12F);
            this.groupBox1.Location = new System.Drawing.Point(2, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 257);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Result";
            // 
            // up_Down_load_Panel
            // 
            this.up_Down_load_Panel.Location = new System.Drawing.Point(8, 5);
            this.up_Down_load_Panel.Name = "up_Down_load_Panel";
            this.up_Down_load_Panel.Size = new System.Drawing.Size(334, 86);
            this.up_Down_load_Panel.TabIndex = 0;
            // 
            // Verify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 345);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.up_Down_load_Panel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Verify";
            this.ShowIcon = false;
            this.Text = "Verify";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Verify_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Verify_FormClosed);
            this.Load += new System.EventHandler(this.Verify_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LadderUI.Up_Down_load_Panel up_Down_load_Panel;
        private System.Windows.Forms.Timer timer_程序執行;
        private System.Windows.Forms.ListBox listBox_比較結果;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}