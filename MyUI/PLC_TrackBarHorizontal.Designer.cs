namespace MyUI
{
    partial class PLC_TrackBarHorizontal
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_Title = new System.Windows.Forms.Label();
            this.label_Value = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label_Title);
            this.panel2.Controls.Add(this.label_Value);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 36);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(278, 15);
            this.panel2.TabIndex = 5;
            // 
            // label_Title
            // 
            this.label_Title.AutoSize = true;
            this.label_Title.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_Title.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_Title.Location = new System.Drawing.Point(0, 0);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(33, 13);
            this.label_Title.TabIndex = 4;
            this.label_Title.Text = "Title";
            // 
            // label_Value
            // 
            this.label_Value.AutoSize = true;
            this.label_Value.Dock = System.Windows.Forms.DockStyle.Right;
            this.label_Value.Font = new System.Drawing.Font("新細明體", 10F);
            this.label_Value.Location = new System.Drawing.Point(233, 0);
            this.label_Value.Name = "label_Value";
            this.label_Value.Size = new System.Drawing.Size(45, 14);
            this.label_Value.TabIndex = 2;
            this.label_Value.Text = "0.0000";
            this.label_Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 36);
            this.panel1.TabIndex = 6;
            // 
            // trackBar1
            // 
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar1.Location = new System.Drawing.Point(0, 0);
            this.trackBar1.Minimum = 10;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(278, 36);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.Value = 10;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll_1);
            // 
            // PLC_TrackBarHorizontal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "PLC_TrackBarHorizontal";
            this.Size = new System.Drawing.Size(278, 51);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_Value;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label_Title;

    }
}
