namespace MyUI
{
    partial class DateTimeComList
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.rJ_ComboBox_Day = new MyUI.RJ_ComboBox();
            this.rJ_ComboBox_Month = new MyUI.RJ_ComboBox();
            this.rJ_ComboBox_Year = new MyUI.RJ_ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("標楷體", 24F);
            this.label1.Location = new System.Drawing.Point(129, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 55);
            this.label1.TabIndex = 45;
            this.label1.Text = "/";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rJ_ComboBox_Day
            // 
            this.rJ_ComboBox_Day.BackColor = System.Drawing.Color.SkyBlue;
            this.rJ_ComboBox_Day.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_ComboBox_Day.BorderSize = 2;
            this.rJ_ComboBox_Day.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_ComboBox_Day.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.rJ_ComboBox_Day.Font = new System.Drawing.Font("標楷體", 24F);
            this.rJ_ComboBox_Day.ForeColor = System.Drawing.Color.White;
            this.rJ_ComboBox_Day.IconColor = System.Drawing.Color.Black;
            this.rJ_ComboBox_Day.ListBackColor = System.Drawing.Color.SkyBlue;
            this.rJ_ComboBox_Day.ListTextColor = System.Drawing.Color.White;
            this.rJ_ComboBox_Day.Location = new System.Drawing.Point(299, 0);
            this.rJ_ComboBox_Day.MinimumSize = new System.Drawing.Size(50, 30);
            this.rJ_ComboBox_Day.Name = "rJ_ComboBox_Day";
            this.rJ_ComboBox_Day.Padding = new System.Windows.Forms.Padding(2);
            this.rJ_ComboBox_Day.Size = new System.Drawing.Size(98, 55);
            this.rJ_ComboBox_Day.TabIndex = 48;
            this.rJ_ComboBox_Day.Texts = "";
            this.rJ_ComboBox_Day.OnSelectedIndexChanged += new System.EventHandler(this.rJ_ComboBox_Day_OnSelectedIndexChanged);
            // 
            // rJ_ComboBox_Month
            // 
            this.rJ_ComboBox_Month.BackColor = System.Drawing.Color.SkyBlue;
            this.rJ_ComboBox_Month.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_ComboBox_Month.BorderSize = 2;
            this.rJ_ComboBox_Month.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_ComboBox_Month.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.rJ_ComboBox_Month.Font = new System.Drawing.Font("標楷體", 24F);
            this.rJ_ComboBox_Month.ForeColor = System.Drawing.Color.White;
            this.rJ_ComboBox_Month.IconColor = System.Drawing.Color.Black;
            this.rJ_ComboBox_Month.ListBackColor = System.Drawing.Color.SkyBlue;
            this.rJ_ComboBox_Month.ListTextColor = System.Drawing.Color.White;
            this.rJ_ComboBox_Month.Location = new System.Drawing.Point(165, 0);
            this.rJ_ComboBox_Month.MinimumSize = new System.Drawing.Size(50, 30);
            this.rJ_ComboBox_Month.Name = "rJ_ComboBox_Month";
            this.rJ_ComboBox_Month.Padding = new System.Windows.Forms.Padding(2);
            this.rJ_ComboBox_Month.Size = new System.Drawing.Size(98, 55);
            this.rJ_ComboBox_Month.TabIndex = 46;
            this.rJ_ComboBox_Month.Texts = "";
            this.rJ_ComboBox_Month.OnSelectedIndexChanged += new System.EventHandler(this.rJ_ComboBox_Month_OnSelectedIndexChanged);
            // 
            // rJ_ComboBox_Year
            // 
            this.rJ_ComboBox_Year.BackColor = System.Drawing.Color.SkyBlue;
            this.rJ_ComboBox_Year.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_ComboBox_Year.BorderSize = 2;
            this.rJ_ComboBox_Year.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_ComboBox_Year.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.rJ_ComboBox_Year.Font = new System.Drawing.Font("標楷體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_ComboBox_Year.ForeColor = System.Drawing.Color.White;
            this.rJ_ComboBox_Year.IconColor = System.Drawing.Color.Black;
            this.rJ_ComboBox_Year.ListBackColor = System.Drawing.Color.SkyBlue;
            this.rJ_ComboBox_Year.ListTextColor = System.Drawing.Color.White;
            this.rJ_ComboBox_Year.Location = new System.Drawing.Point(0, 0);
            this.rJ_ComboBox_Year.MinimumSize = new System.Drawing.Size(50, 30);
            this.rJ_ComboBox_Year.Name = "rJ_ComboBox_Year";
            this.rJ_ComboBox_Year.Padding = new System.Windows.Forms.Padding(2);
            this.rJ_ComboBox_Year.Size = new System.Drawing.Size(129, 55);
            this.rJ_ComboBox_Year.TabIndex = 44;
            this.rJ_ComboBox_Year.Texts = "";
            this.rJ_ComboBox_Year.OnSelectedIndexChanged += new System.EventHandler(this.rJ_ComboBox_Year_OnSelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("標楷體", 24F);
            this.label2.Location = new System.Drawing.Point(263, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 55);
            this.label2.TabIndex = 49;
            this.label2.Text = "/";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DateTimeComList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.rJ_ComboBox_Day);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rJ_ComboBox_Month);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rJ_ComboBox_Year);
            this.Name = "DateTimeComList";
            this.Size = new System.Drawing.Size(400, 55);
            this.Load += new System.EventHandler(this.DateTimeComList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private RJ_ComboBox rJ_ComboBox_Day;
        private RJ_ComboBox rJ_ComboBox_Month;
        private System.Windows.Forms.Label label1;
        private RJ_ComboBox rJ_ComboBox_Year;
        private System.Windows.Forms.Label label2;
    }
}
