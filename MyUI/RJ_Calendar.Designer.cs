
namespace MyUI
{
    partial class RJ_Calendar
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
            this.panel_起始時間_時分秒 = new System.Windows.Forms.Panel();
            this.rJ_TextBox_時 = new MyUI.RJ_TextBox();
            this.rJ_Lable4 = new MyUI.RJ_Lable();
            this.rJ_TextBox_分 = new MyUI.RJ_TextBox();
            this.rJ_Lable3 = new MyUI.RJ_Lable();
            this.rJ_TextBox_秒 = new MyUI.RJ_TextBox();
            this.calendar = new Sunny.UI.UICalendar();
            this.panel_起始時間_時分秒.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_起始時間_時分秒
            // 
            this.panel_起始時間_時分秒.Controls.Add(this.rJ_TextBox_時);
            this.panel_起始時間_時分秒.Controls.Add(this.rJ_Lable4);
            this.panel_起始時間_時分秒.Controls.Add(this.rJ_TextBox_分);
            this.panel_起始時間_時分秒.Controls.Add(this.rJ_Lable3);
            this.panel_起始時間_時分秒.Controls.Add(this.rJ_TextBox_秒);
            this.panel_起始時間_時分秒.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_起始時間_時分秒.Location = new System.Drawing.Point(0, 421);
            this.panel_起始時間_時分秒.Name = "panel_起始時間_時分秒";
            this.panel_起始時間_時分秒.Padding = new System.Windows.Forms.Padding(5);
            this.panel_起始時間_時分秒.Size = new System.Drawing.Size(514, 53);
            this.panel_起始時間_時分秒.TabIndex = 18;
            // 
            // rJ_TextBox_時
            // 
            this.rJ_TextBox_時.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_時.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_時.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_時.BorderRadius = 0;
            this.rJ_TextBox_時.BorderSize = 1;
            this.rJ_TextBox_時.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_TextBox_時.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_時.ForeColor = System.Drawing.Color.Black;
            this.rJ_TextBox_時.GUID = "";
            this.rJ_TextBox_時.Location = new System.Drawing.Point(232, 5);
            this.rJ_TextBox_時.Multiline = false;
            this.rJ_TextBox_時.Name = "rJ_TextBox_時";
            this.rJ_TextBox_時.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_時.PassWordChar = false;
            this.rJ_TextBox_時.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_時.PlaceholderText = "時";
            this.rJ_TextBox_時.ShowTouchPannel = false;
            this.rJ_TextBox_時.Size = new System.Drawing.Size(75, 42);
            this.rJ_TextBox_時.TabIndex = 4;
            this.rJ_TextBox_時.TextAlgin = System.Windows.Forms.HorizontalAlignment.Right;
            this.rJ_TextBox_時.Texts = "";
            this.rJ_TextBox_時.UnderlineStyle = false;
            // 
            // rJ_Lable4
            // 
            this.rJ_Lable4.BackColor = System.Drawing.Color.White;
            this.rJ_Lable4.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Lable4.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable4.BorderRadius = 10;
            this.rJ_Lable4.BorderSize = 0;
            this.rJ_Lable4.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Lable4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable4.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable4.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable4.GUID = "";
            this.rJ_Lable4.Location = new System.Drawing.Point(307, 5);
            this.rJ_Lable4.Name = "rJ_Lable4";
            this.rJ_Lable4.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable4.ShadowSize = 0;
            this.rJ_Lable4.Size = new System.Drawing.Size(26, 43);
            this.rJ_Lable4.TabIndex = 3;
            this.rJ_Lable4.Text = ":";
            this.rJ_Lable4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable4.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_TextBox_分
            // 
            this.rJ_TextBox_分.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_分.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_分.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_分.BorderRadius = 0;
            this.rJ_TextBox_分.BorderSize = 1;
            this.rJ_TextBox_分.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_TextBox_分.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_分.ForeColor = System.Drawing.Color.Black;
            this.rJ_TextBox_分.GUID = "";
            this.rJ_TextBox_分.Location = new System.Drawing.Point(333, 5);
            this.rJ_TextBox_分.Multiline = false;
            this.rJ_TextBox_分.Name = "rJ_TextBox_分";
            this.rJ_TextBox_分.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_分.PassWordChar = false;
            this.rJ_TextBox_分.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_分.PlaceholderText = "分";
            this.rJ_TextBox_分.ShowTouchPannel = false;
            this.rJ_TextBox_分.Size = new System.Drawing.Size(75, 42);
            this.rJ_TextBox_分.TabIndex = 2;
            this.rJ_TextBox_分.TextAlgin = System.Windows.Forms.HorizontalAlignment.Right;
            this.rJ_TextBox_分.Texts = "";
            this.rJ_TextBox_分.UnderlineStyle = false;
            // 
            // rJ_Lable3
            // 
            this.rJ_Lable3.BackColor = System.Drawing.Color.White;
            this.rJ_Lable3.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Lable3.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable3.BorderRadius = 10;
            this.rJ_Lable3.BorderSize = 0;
            this.rJ_Lable3.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Lable3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable3.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable3.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable3.GUID = "";
            this.rJ_Lable3.Location = new System.Drawing.Point(408, 5);
            this.rJ_Lable3.Name = "rJ_Lable3";
            this.rJ_Lable3.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable3.ShadowSize = 0;
            this.rJ_Lable3.Size = new System.Drawing.Size(26, 43);
            this.rJ_Lable3.TabIndex = 1;
            this.rJ_Lable3.Text = ":";
            this.rJ_Lable3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable3.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_TextBox_秒
            // 
            this.rJ_TextBox_秒.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_秒.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_秒.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_秒.BorderRadius = 0;
            this.rJ_TextBox_秒.BorderSize = 1;
            this.rJ_TextBox_秒.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_TextBox_秒.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_秒.ForeColor = System.Drawing.Color.Black;
            this.rJ_TextBox_秒.GUID = "";
            this.rJ_TextBox_秒.Location = new System.Drawing.Point(434, 5);
            this.rJ_TextBox_秒.Multiline = false;
            this.rJ_TextBox_秒.Name = "rJ_TextBox_秒";
            this.rJ_TextBox_秒.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_秒.PassWordChar = false;
            this.rJ_TextBox_秒.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_秒.PlaceholderText = "秒";
            this.rJ_TextBox_秒.ShowTouchPannel = false;
            this.rJ_TextBox_秒.Size = new System.Drawing.Size(75, 42);
            this.rJ_TextBox_秒.TabIndex = 0;
            this.rJ_TextBox_秒.TextAlgin = System.Windows.Forms.HorizontalAlignment.Right;
            this.rJ_TextBox_秒.Texts = "";
            this.rJ_TextBox_秒.UnderlineStyle = false;
            // 
            // calendar
            // 
            this.calendar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.calendar.Date = new System.DateTime(2024, 3, 18, 0, 0, 0, 0);
            this.calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calendar.FillColor = System.Drawing.Color.White;
            this.calendar.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.calendar.FillColorGradient = true;
            this.calendar.Font = new System.Drawing.Font("新宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calendar.ForeColor = System.Drawing.Color.Black;
            this.calendar.Location = new System.Drawing.Point(0, 0);
            this.calendar.MinimumSize = new System.Drawing.Size(240, 180);
            this.calendar.Name = "calendar";
            this.calendar.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.calendar.Radius = 10;
            this.calendar.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.calendar.RectSize = 2;
            this.calendar.Size = new System.Drawing.Size(514, 421);
            this.calendar.Style = Sunny.UI.UIStyle.Custom;
            this.calendar.TabIndex = 19;
            this.calendar.Text = "起始時間";
            this.calendar.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RJ_Calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.calendar);
            this.Controls.Add(this.panel_起始時間_時分秒);
            this.Name = "RJ_Calendar";
            this.Size = new System.Drawing.Size(514, 474);
            this.panel_起始時間_時分秒.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_起始時間_時分秒;
        private RJ_TextBox rJ_TextBox_時;
        private RJ_Lable rJ_Lable4;
        private RJ_TextBox rJ_TextBox_分;
        private RJ_Lable rJ_Lable3;
        private RJ_TextBox rJ_TextBox_秒;
        private Sunny.UI.UICalendar calendar;
    }
}
