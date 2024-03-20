
namespace MyUI
{
    partial class DateTimeIntervelPopup
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
            this.btnSure = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timeEnd = new MyUI.TimeSetting();
            this.timeStart = new MyUI.TimeSetting();
            this.calendarEnd = new Sunny.UI.UICalendar();
            this.calendarStart = new Sunny.UI.UICalendar();
            this.SuspendLayout();
            // 
            // btnSure
            // 
            this.btnSure.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSure.Location = new System.Drawing.Point(795, 475);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(190, 65);
            this.btnSure.TabIndex = 10;
            this.btnSure.Text = "确定";
            this.btnSure.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(717, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 31);
            this.label2.TabIndex = 9;
            this.label2.Text = "結束";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(223, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 31);
            this.label1.TabIndex = 8;
            this.label1.Text = "開始";
            // 
            // timeEnd
            // 
            this.timeEnd.BackColor = System.Drawing.Color.White;
            this.timeEnd.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.timeEnd.Hour = "00";
            this.timeEnd.Location = new System.Drawing.Point(565, 425);
            this.timeEnd.Margin = new System.Windows.Forms.Padding(7);
            this.timeEnd.Min = "00";
            this.timeEnd.Name = "timeEnd";
            this.timeEnd.Second = "00";
            this.timeEnd.Size = new System.Drawing.Size(367, 40);
            this.timeEnd.TabIndex = 12;
            // 
            // timeStart
            // 
            this.timeStart.BackColor = System.Drawing.Color.White;
            this.timeStart.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.timeStart.Hour = "00";
            this.timeStart.Location = new System.Drawing.Point(71, 425);
            this.timeStart.Min = "00";
            this.timeStart.Name = "timeStart";
            this.timeStart.Second = "00";
            this.timeStart.Size = new System.Drawing.Size(367, 40);
            this.timeStart.TabIndex = 11;
            // 
            // calendarEnd
            // 
            this.calendarEnd.Date = new System.DateTime(2024, 3, 18, 0, 0, 0, 0);
            this.calendarEnd.FillColor = System.Drawing.Color.Gray;
            this.calendarEnd.FillColorGradient = true;
            this.calendarEnd.Font = new System.Drawing.Font("新宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calendarEnd.Location = new System.Drawing.Point(512, 58);
            this.calendarEnd.MinimumSize = new System.Drawing.Size(240, 180);
            this.calendarEnd.Name = "calendarEnd";
            this.calendarEnd.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.calendarEnd.Radius = 10;
            this.calendarEnd.RectColor = System.Drawing.Color.Silver;
            this.calendarEnd.RectSize = 2;
            this.calendarEnd.Size = new System.Drawing.Size(473, 355);
            this.calendarEnd.Style = Sunny.UI.UIStyle.Custom;
            this.calendarEnd.TabIndex = 13;
            this.calendarEnd.Text = "結束時間";
            this.calendarEnd.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // calendarStart
            // 
            this.calendarStart.Date = new System.DateTime(2024, 3, 18, 0, 0, 0, 0);
            this.calendarStart.FillColor = System.Drawing.Color.Gray;
            this.calendarStart.FillColorGradient = true;
            this.calendarStart.Font = new System.Drawing.Font("新宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calendarStart.Location = new System.Drawing.Point(18, 58);
            this.calendarStart.MinimumSize = new System.Drawing.Size(240, 180);
            this.calendarStart.Name = "calendarStart";
            this.calendarStart.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.calendarStart.Radius = 10;
            this.calendarStart.RectColor = System.Drawing.Color.Silver;
            this.calendarStart.RectSize = 2;
            this.calendarStart.Size = new System.Drawing.Size(473, 355);
            this.calendarStart.Style = Sunny.UI.UIStyle.Custom;
            this.calendarStart.TabIndex = 14;
            this.calendarStart.Text = "起始時間";
            this.calendarStart.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DateTimeIntervelPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.calendarStart);
            this.Controls.Add(this.calendarEnd);
            this.Controls.Add(this.timeEnd);
            this.Controls.Add(this.timeStart);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DateTimeIntervelPopup";
            this.Size = new System.Drawing.Size(1013, 552);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TimeSetting timeStart;
        private TimeSetting timeEnd;
        private Sunny.UI.UICalendar calendarEnd;
        private Sunny.UI.UICalendar calendarStart;
    }
}
