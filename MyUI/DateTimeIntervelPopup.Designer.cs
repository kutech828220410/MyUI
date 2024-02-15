
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
            this.calendarEnd = new System.Windows.Forms.MonthCalendar();
            this.calendarStart = new System.Windows.Forms.MonthCalendar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timeStart = new MyUI.TimeSetting();
            this.timeEnd = new MyUI.TimeSetting();
            this.SuspendLayout();
            // 
            // btnSure
            // 
            this.btnSure.Location = new System.Drawing.Point(381, 261);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(75, 23);
            this.btnSure.TabIndex = 10;
            this.btnSure.Text = "确定";
            this.btnSure.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(327, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "結束";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(102, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "開始";
            // 
            // calendarEnd
            // 
            this.calendarEnd.BackColor = System.Drawing.SystemColors.Window;
            this.calendarEnd.Location = new System.Drawing.Point(236, 32);
            this.calendarEnd.Name = "calendarEnd";
            this.calendarEnd.TabIndex = 7;
            // 
            // calendarStart
            // 
            this.calendarStart.Location = new System.Drawing.Point(9, 32);
            this.calendarStart.Name = "calendarStart";
            this.calendarStart.TabIndex = 6;
            // 
            // timeStart
            // 
            this.timeStart.BackColor = System.Drawing.Color.White;
            this.timeStart.Location = new System.Drawing.Point(23, 220);
            this.timeStart.Name = "timeStart";
            this.timeStart.Size = new System.Drawing.Size(192, 24);
            this.timeStart.TabIndex = 11;
            // 
            // timeEnd
            // 
            this.timeEnd.BackColor = System.Drawing.Color.White;
            this.timeEnd.Location = new System.Drawing.Point(249, 220);
            this.timeEnd.Name = "timeEnd";
            this.timeEnd.Size = new System.Drawing.Size(192, 24);
            this.timeEnd.TabIndex = 12;
            // 
            // DateTimeIntervelPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.timeEnd);
            this.Controls.Add(this.timeStart);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.calendarEnd);
            this.Controls.Add(this.calendarStart);
            this.Name = "DateTimeIntervelPopup";
            this.Size = new System.Drawing.Size(463, 293);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MonthCalendar calendarEnd;
        private System.Windows.Forms.MonthCalendar calendarStart;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TimeSetting timeStart;
        private TimeSetting timeEnd;
    }
}
