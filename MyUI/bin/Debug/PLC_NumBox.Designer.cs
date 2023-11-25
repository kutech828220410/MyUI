namespace MyUI
{
    partial class PLC_NumBox
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_UP = new System.Windows.Forms.Button();
            this.button_DOWN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox1.Location = new System.Drawing.Point(17, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(78, 22);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            this.textBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseDown);
            // 
            // button_UP
            // 
            this.button_UP.BackgroundImage = global::MyUI.Resource1._568111;
            this.button_UP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_UP.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_UP.Location = new System.Drawing.Point(95, 0);
            this.button_UP.Name = "button_UP";
            this.button_UP.Size = new System.Drawing.Size(17, 22);
            this.button_UP.TabIndex = 5;
            this.button_UP.UseVisualStyleBackColor = true;
            this.button_UP.Visible = false;
            this.button_UP.Click += new System.EventHandler(this.button_UP_Click);
            // 
            // button_DOWN
            // 
            this.button_DOWN.BackgroundImage = global::MyUI.Resource1._568102;
            this.button_DOWN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_DOWN.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_DOWN.Location = new System.Drawing.Point(0, 0);
            this.button_DOWN.Name = "button_DOWN";
            this.button_DOWN.Size = new System.Drawing.Size(17, 22);
            this.button_DOWN.TabIndex = 3;
            this.button_DOWN.UseVisualStyleBackColor = true;
            this.button_DOWN.Visible = false;
            this.button_DOWN.Click += new System.EventHandler(this.button_DOWN_Click);
            // 
            // PLC_NumBox
            // 
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_UP);
            this.Controls.Add(this.button_DOWN);
            this.Name = "PLC_NumBox";
            this.Size = new System.Drawing.Size(112, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_DOWN;
        private System.Windows.Forms.Button button_UP;
        private System.Windows.Forms.TextBox textBox1;
    }
}
