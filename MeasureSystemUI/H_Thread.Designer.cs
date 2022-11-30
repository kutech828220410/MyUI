namespace MeasureSystemUI
{
    partial class H_Thread
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
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_ThreadName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.textBox_CycleTime = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.timer_Refresh = new System.Windows.Forms.Timer(this.components);
            this.panel13.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.panel15);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel13.Location = new System.Drawing.Point(0, 0);
            this.panel13.Margin = new System.Windows.Forms.Padding(0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(135, 20);
            this.panel13.TabIndex = 3;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.label7);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(135, 20);
            this.panel15.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Thread";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_ThreadName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(135, 20);
            this.panel1.TabIndex = 4;
            // 
            // label_ThreadName
            // 
            this.label_ThreadName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_ThreadName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_ThreadName.Location = new System.Drawing.Point(0, 0);
            this.label_ThreadName.Margin = new System.Windows.Forms.Padding(0);
            this.label_ThreadName.Name = "label_ThreadName";
            this.label_ThreadName.Size = new System.Drawing.Size(135, 20);
            this.label_ThreadName.TabIndex = 5;
            this.label_ThreadName.Text = "ThreadName001";
            this.label_ThreadName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(135, 29);
            this.panel2.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(65, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(70, 29);
            this.panel4.TabIndex = 7;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.textBox_CycleTime);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(70, 29);
            this.panel5.TabIndex = 7;
            // 
            // textBox_CycleTime
            // 
            this.textBox_CycleTime.Location = new System.Drawing.Point(6, 4);
            this.textBox_CycleTime.Name = "textBox_CycleTime";
            this.textBox_CycleTime.ReadOnly = true;
            this.textBox_CycleTime.Size = new System.Drawing.Size(61, 22);
            this.textBox_CycleTime.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(65, 29);
            this.panel3.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 29);
            this.label1.TabIndex = 6;
            this.label1.Text = "CycleTime";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer_Refresh
            // 
            this.timer_Refresh.Tick += new System.EventHandler(this.timer_Refresh_Tick);
            // 
            // H_Thread
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel13);
            this.Name = "H_Thread";
            this.Size = new System.Drawing.Size(135, 69);
            this.panel13.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_ThreadName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox textBox_CycleTime;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer_Refresh;
    }
}
