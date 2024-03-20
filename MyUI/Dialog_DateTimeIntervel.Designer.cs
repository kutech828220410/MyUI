
namespace MyUI
{
    partial class Dialog_DateTimeIntervel
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
            this.rJ_Lable1 = new MyUI.RJ_Lable();
            this.rJ_Pannel1 = new MyUI.RJ_Pannel();
            this.calendarStart = new Sunny.UI.UICalendar();
            this.panel_起始時間_時分秒 = new System.Windows.Forms.Panel();
            this.rJ_TextBox_起始時間_時 = new MyUI.RJ_TextBox();
            this.rJ_Lable4 = new MyUI.RJ_Lable();
            this.rJ_TextBox_起始時間_分 = new MyUI.RJ_TextBox();
            this.rJ_Lable3 = new MyUI.RJ_Lable();
            this.rJ_TextBox_起始時間_秒 = new MyUI.RJ_TextBox();
            this.rJ_Lable2 = new MyUI.RJ_Lable();
            this.rJ_Pannel2 = new MyUI.RJ_Pannel();
            this.calendarEnd = new Sunny.UI.UICalendar();
            this.panel_結束時間_時分秒 = new System.Windows.Forms.Panel();
            this.rJ_TextBox_結束時間_時 = new MyUI.RJ_TextBox();
            this.rJ_Lable5 = new MyUI.RJ_Lable();
            this.rJ_TextBox_結束時間_分 = new MyUI.RJ_TextBox();
            this.rJ_Lable6 = new MyUI.RJ_Lable();
            this.rJ_TextBox_結束時間_秒 = new MyUI.RJ_TextBox();
            this.rJ_Lable7 = new MyUI.RJ_Lable();
            this.panel3 = new System.Windows.Forms.Panel();
            this.plC_RJ_Button_返回 = new MyUI.PLC_RJ_Button();
            this.plC_RJ_Button_確認 = new MyUI.PLC_RJ_Button();
            this.rJ_Pannel1.SuspendLayout();
            this.panel_起始時間_時分秒.SuspendLayout();
            this.rJ_Pannel2.SuspendLayout();
            this.panel_結束時間_時分秒.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rJ_Lable1
            // 
            this.rJ_Lable1.BackColor = System.Drawing.Color.White;
            this.rJ_Lable1.BackgroundColor = System.Drawing.Color.DarkGray;
            this.rJ_Lable1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable1.BorderRadius = 10;
            this.rJ_Lable1.BorderSize = 0;
            this.rJ_Lable1.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable1.Font = new System.Drawing.Font("微軟正黑體", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable1.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable1.GUID = "";
            this.rJ_Lable1.Location = new System.Drawing.Point(4, 28);
            this.rJ_Lable1.Name = "rJ_Lable1";
            this.rJ_Lable1.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable1.ShadowSize = 3;
            this.rJ_Lable1.Size = new System.Drawing.Size(1050, 108);
            this.rJ_Lable1.TabIndex = 2;
            this.rJ_Lable1.Text = "日  期  選  擇";
            this.rJ_Lable1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable1.TextColor = System.Drawing.Color.White;
            // 
            // rJ_Pannel1
            // 
            this.rJ_Pannel1.BackColor = System.Drawing.Color.White;
            this.rJ_Pannel1.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Pannel1.BorderColor = System.Drawing.Color.White;
            this.rJ_Pannel1.BorderRadius = 10;
            this.rJ_Pannel1.BorderSize = 0;
            this.rJ_Pannel1.Controls.Add(this.calendarStart);
            this.rJ_Pannel1.Controls.Add(this.panel_起始時間_時分秒);
            this.rJ_Pannel1.Controls.Add(this.rJ_Lable2);
            this.rJ_Pannel1.ForeColor = System.Drawing.Color.Black;
            this.rJ_Pannel1.IsSelected = false;
            this.rJ_Pannel1.Location = new System.Drawing.Point(13, 152);
            this.rJ_Pannel1.Name = "rJ_Pannel1";
            this.rJ_Pannel1.Padding = new System.Windows.Forms.Padding(2, 2, 10, 10);
            this.rJ_Pannel1.ShadowColor = System.Drawing.Color.Black;
            this.rJ_Pannel1.ShadowSize = 3;
            this.rJ_Pannel1.Size = new System.Drawing.Size(512, 465);
            this.rJ_Pannel1.TabIndex = 16;
            // 
            // calendarStart
            // 
            this.calendarStart.Date = new System.DateTime(2024, 3, 18, 0, 0, 0, 0);
            this.calendarStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calendarStart.FillColor = System.Drawing.Color.Gray;
            this.calendarStart.FillColorGradient = true;
            this.calendarStart.Font = new System.Drawing.Font("新宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calendarStart.Location = new System.Drawing.Point(2, 74);
            this.calendarStart.MinimumSize = new System.Drawing.Size(240, 180);
            this.calendarStart.Name = "calendarStart";
            this.calendarStart.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.calendarStart.Radius = 10;
            this.calendarStart.RectColor = System.Drawing.Color.Silver;
            this.calendarStart.RectSize = 2;
            this.calendarStart.Size = new System.Drawing.Size(500, 328);
            this.calendarStart.Style = Sunny.UI.UIStyle.Custom;
            this.calendarStart.TabIndex = 18;
            this.calendarStart.Text = "起始時間";
            this.calendarStart.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_起始時間_時分秒
            // 
            this.panel_起始時間_時分秒.Controls.Add(this.rJ_TextBox_起始時間_時);
            this.panel_起始時間_時分秒.Controls.Add(this.rJ_Lable4);
            this.panel_起始時間_時分秒.Controls.Add(this.rJ_TextBox_起始時間_分);
            this.panel_起始時間_時分秒.Controls.Add(this.rJ_Lable3);
            this.panel_起始時間_時分秒.Controls.Add(this.rJ_TextBox_起始時間_秒);
            this.panel_起始時間_時分秒.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_起始時間_時分秒.Location = new System.Drawing.Point(2, 402);
            this.panel_起始時間_時分秒.Name = "panel_起始時間_時分秒";
            this.panel_起始時間_時分秒.Padding = new System.Windows.Forms.Padding(5);
            this.panel_起始時間_時分秒.Size = new System.Drawing.Size(500, 53);
            this.panel_起始時間_時分秒.TabIndex = 17;
            // 
            // rJ_TextBox_起始時間_時
            // 
            this.rJ_TextBox_起始時間_時.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_起始時間_時.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_起始時間_時.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_起始時間_時.BorderRadius = 0;
            this.rJ_TextBox_起始時間_時.BorderSize = 1;
            this.rJ_TextBox_起始時間_時.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_TextBox_起始時間_時.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_起始時間_時.ForeColor = System.Drawing.Color.Black;
            this.rJ_TextBox_起始時間_時.GUID = "";
            this.rJ_TextBox_起始時間_時.Location = new System.Drawing.Point(218, 5);
            this.rJ_TextBox_起始時間_時.Multiline = false;
            this.rJ_TextBox_起始時間_時.Name = "rJ_TextBox_起始時間_時";
            this.rJ_TextBox_起始時間_時.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_起始時間_時.PassWordChar = false;
            this.rJ_TextBox_起始時間_時.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_起始時間_時.PlaceholderText = "時";
            this.rJ_TextBox_起始時間_時.ShowTouchPannel = false;
            this.rJ_TextBox_起始時間_時.Size = new System.Drawing.Size(75, 42);
            this.rJ_TextBox_起始時間_時.TabIndex = 4;
            this.rJ_TextBox_起始時間_時.TextAlgin = System.Windows.Forms.HorizontalAlignment.Right;
            this.rJ_TextBox_起始時間_時.Texts = "";
            this.rJ_TextBox_起始時間_時.UnderlineStyle = false;
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
            this.rJ_Lable4.Location = new System.Drawing.Point(293, 5);
            this.rJ_Lable4.Name = "rJ_Lable4";
            this.rJ_Lable4.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable4.ShadowSize = 0;
            this.rJ_Lable4.Size = new System.Drawing.Size(26, 43);
            this.rJ_Lable4.TabIndex = 3;
            this.rJ_Lable4.Text = ":";
            this.rJ_Lable4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable4.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_TextBox_起始時間_分
            // 
            this.rJ_TextBox_起始時間_分.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_起始時間_分.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_起始時間_分.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_起始時間_分.BorderRadius = 0;
            this.rJ_TextBox_起始時間_分.BorderSize = 1;
            this.rJ_TextBox_起始時間_分.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_TextBox_起始時間_分.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_起始時間_分.ForeColor = System.Drawing.Color.Black;
            this.rJ_TextBox_起始時間_分.GUID = "";
            this.rJ_TextBox_起始時間_分.Location = new System.Drawing.Point(319, 5);
            this.rJ_TextBox_起始時間_分.Multiline = false;
            this.rJ_TextBox_起始時間_分.Name = "rJ_TextBox_起始時間_分";
            this.rJ_TextBox_起始時間_分.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_起始時間_分.PassWordChar = false;
            this.rJ_TextBox_起始時間_分.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_起始時間_分.PlaceholderText = "分";
            this.rJ_TextBox_起始時間_分.ShowTouchPannel = false;
            this.rJ_TextBox_起始時間_分.Size = new System.Drawing.Size(75, 42);
            this.rJ_TextBox_起始時間_分.TabIndex = 2;
            this.rJ_TextBox_起始時間_分.TextAlgin = System.Windows.Forms.HorizontalAlignment.Right;
            this.rJ_TextBox_起始時間_分.Texts = "";
            this.rJ_TextBox_起始時間_分.UnderlineStyle = false;
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
            this.rJ_Lable3.Location = new System.Drawing.Point(394, 5);
            this.rJ_Lable3.Name = "rJ_Lable3";
            this.rJ_Lable3.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable3.ShadowSize = 0;
            this.rJ_Lable3.Size = new System.Drawing.Size(26, 43);
            this.rJ_Lable3.TabIndex = 1;
            this.rJ_Lable3.Text = ":";
            this.rJ_Lable3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable3.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_TextBox_起始時間_秒
            // 
            this.rJ_TextBox_起始時間_秒.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_起始時間_秒.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_起始時間_秒.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_起始時間_秒.BorderRadius = 0;
            this.rJ_TextBox_起始時間_秒.BorderSize = 1;
            this.rJ_TextBox_起始時間_秒.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_TextBox_起始時間_秒.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_起始時間_秒.ForeColor = System.Drawing.Color.Black;
            this.rJ_TextBox_起始時間_秒.GUID = "";
            this.rJ_TextBox_起始時間_秒.Location = new System.Drawing.Point(420, 5);
            this.rJ_TextBox_起始時間_秒.Multiline = false;
            this.rJ_TextBox_起始時間_秒.Name = "rJ_TextBox_起始時間_秒";
            this.rJ_TextBox_起始時間_秒.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_起始時間_秒.PassWordChar = false;
            this.rJ_TextBox_起始時間_秒.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_起始時間_秒.PlaceholderText = "秒";
            this.rJ_TextBox_起始時間_秒.ShowTouchPannel = false;
            this.rJ_TextBox_起始時間_秒.Size = new System.Drawing.Size(75, 42);
            this.rJ_TextBox_起始時間_秒.TabIndex = 0;
            this.rJ_TextBox_起始時間_秒.TextAlgin = System.Windows.Forms.HorizontalAlignment.Right;
            this.rJ_TextBox_起始時間_秒.Texts = "";
            this.rJ_TextBox_起始時間_秒.UnderlineStyle = false;
            // 
            // rJ_Lable2
            // 
            this.rJ_Lable2.BackColor = System.Drawing.Color.White;
            this.rJ_Lable2.BackgroundColor = System.Drawing.Color.Silver;
            this.rJ_Lable2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable2.BorderRadius = 10;
            this.rJ_Lable2.BorderSize = 0;
            this.rJ_Lable2.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable2.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable2.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable2.GUID = "";
            this.rJ_Lable2.Location = new System.Drawing.Point(2, 2);
            this.rJ_Lable2.Name = "rJ_Lable2";
            this.rJ_Lable2.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable2.ShadowSize = 3;
            this.rJ_Lable2.Size = new System.Drawing.Size(500, 72);
            this.rJ_Lable2.TabIndex = 16;
            this.rJ_Lable2.Text = "起始時間";
            this.rJ_Lable2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable2.TextColor = System.Drawing.Color.White;
            // 
            // rJ_Pannel2
            // 
            this.rJ_Pannel2.BackColor = System.Drawing.Color.White;
            this.rJ_Pannel2.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Pannel2.BorderColor = System.Drawing.Color.White;
            this.rJ_Pannel2.BorderRadius = 10;
            this.rJ_Pannel2.BorderSize = 0;
            this.rJ_Pannel2.Controls.Add(this.calendarEnd);
            this.rJ_Pannel2.Controls.Add(this.panel_結束時間_時分秒);
            this.rJ_Pannel2.Controls.Add(this.rJ_Lable7);
            this.rJ_Pannel2.ForeColor = System.Drawing.Color.Black;
            this.rJ_Pannel2.IsSelected = false;
            this.rJ_Pannel2.Location = new System.Drawing.Point(531, 152);
            this.rJ_Pannel2.Name = "rJ_Pannel2";
            this.rJ_Pannel2.Padding = new System.Windows.Forms.Padding(2, 2, 10, 10);
            this.rJ_Pannel2.ShadowColor = System.Drawing.Color.Black;
            this.rJ_Pannel2.ShadowSize = 3;
            this.rJ_Pannel2.Size = new System.Drawing.Size(512, 465);
            this.rJ_Pannel2.TabIndex = 17;
            // 
            // calendarEnd
            // 
            this.calendarEnd.Date = new System.DateTime(2024, 3, 18, 0, 0, 0, 0);
            this.calendarEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calendarEnd.FillColor = System.Drawing.Color.Gray;
            this.calendarEnd.FillColorGradient = true;
            this.calendarEnd.Font = new System.Drawing.Font("新宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calendarEnd.Location = new System.Drawing.Point(2, 74);
            this.calendarEnd.MinimumSize = new System.Drawing.Size(240, 180);
            this.calendarEnd.Name = "calendarEnd";
            this.calendarEnd.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.calendarEnd.Radius = 10;
            this.calendarEnd.RectColor = System.Drawing.Color.Silver;
            this.calendarEnd.RectSize = 2;
            this.calendarEnd.Size = new System.Drawing.Size(500, 328);
            this.calendarEnd.Style = Sunny.UI.UIStyle.Custom;
            this.calendarEnd.TabIndex = 18;
            this.calendarEnd.Text = "起始時間";
            this.calendarEnd.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_結束時間_時分秒
            // 
            this.panel_結束時間_時分秒.Controls.Add(this.rJ_TextBox_結束時間_時);
            this.panel_結束時間_時分秒.Controls.Add(this.rJ_Lable5);
            this.panel_結束時間_時分秒.Controls.Add(this.rJ_TextBox_結束時間_分);
            this.panel_結束時間_時分秒.Controls.Add(this.rJ_Lable6);
            this.panel_結束時間_時分秒.Controls.Add(this.rJ_TextBox_結束時間_秒);
            this.panel_結束時間_時分秒.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_結束時間_時分秒.Location = new System.Drawing.Point(2, 402);
            this.panel_結束時間_時分秒.Name = "panel_結束時間_時分秒";
            this.panel_結束時間_時分秒.Padding = new System.Windows.Forms.Padding(5);
            this.panel_結束時間_時分秒.Size = new System.Drawing.Size(500, 53);
            this.panel_結束時間_時分秒.TabIndex = 17;
            // 
            // rJ_TextBox_結束時間_時
            // 
            this.rJ_TextBox_結束時間_時.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_結束時間_時.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_結束時間_時.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_結束時間_時.BorderRadius = 0;
            this.rJ_TextBox_結束時間_時.BorderSize = 1;
            this.rJ_TextBox_結束時間_時.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_TextBox_結束時間_時.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_結束時間_時.ForeColor = System.Drawing.Color.Black;
            this.rJ_TextBox_結束時間_時.GUID = "";
            this.rJ_TextBox_結束時間_時.Location = new System.Drawing.Point(218, 5);
            this.rJ_TextBox_結束時間_時.Multiline = false;
            this.rJ_TextBox_結束時間_時.Name = "rJ_TextBox_結束時間_時";
            this.rJ_TextBox_結束時間_時.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_結束時間_時.PassWordChar = false;
            this.rJ_TextBox_結束時間_時.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_結束時間_時.PlaceholderText = "時";
            this.rJ_TextBox_結束時間_時.ShowTouchPannel = false;
            this.rJ_TextBox_結束時間_時.Size = new System.Drawing.Size(75, 42);
            this.rJ_TextBox_結束時間_時.TabIndex = 4;
            this.rJ_TextBox_結束時間_時.TextAlgin = System.Windows.Forms.HorizontalAlignment.Right;
            this.rJ_TextBox_結束時間_時.Texts = "";
            this.rJ_TextBox_結束時間_時.UnderlineStyle = false;
            // 
            // rJ_Lable5
            // 
            this.rJ_Lable5.BackColor = System.Drawing.Color.White;
            this.rJ_Lable5.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Lable5.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable5.BorderRadius = 10;
            this.rJ_Lable5.BorderSize = 0;
            this.rJ_Lable5.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Lable5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable5.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable5.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable5.GUID = "";
            this.rJ_Lable5.Location = new System.Drawing.Point(293, 5);
            this.rJ_Lable5.Name = "rJ_Lable5";
            this.rJ_Lable5.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable5.ShadowSize = 0;
            this.rJ_Lable5.Size = new System.Drawing.Size(26, 43);
            this.rJ_Lable5.TabIndex = 3;
            this.rJ_Lable5.Text = ":";
            this.rJ_Lable5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable5.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_TextBox_結束時間_分
            // 
            this.rJ_TextBox_結束時間_分.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_結束時間_分.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_結束時間_分.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_結束時間_分.BorderRadius = 0;
            this.rJ_TextBox_結束時間_分.BorderSize = 1;
            this.rJ_TextBox_結束時間_分.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_TextBox_結束時間_分.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_結束時間_分.ForeColor = System.Drawing.Color.Black;
            this.rJ_TextBox_結束時間_分.GUID = "";
            this.rJ_TextBox_結束時間_分.Location = new System.Drawing.Point(319, 5);
            this.rJ_TextBox_結束時間_分.Multiline = false;
            this.rJ_TextBox_結束時間_分.Name = "rJ_TextBox_結束時間_分";
            this.rJ_TextBox_結束時間_分.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_結束時間_分.PassWordChar = false;
            this.rJ_TextBox_結束時間_分.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_結束時間_分.PlaceholderText = "分";
            this.rJ_TextBox_結束時間_分.ShowTouchPannel = false;
            this.rJ_TextBox_結束時間_分.Size = new System.Drawing.Size(75, 42);
            this.rJ_TextBox_結束時間_分.TabIndex = 2;
            this.rJ_TextBox_結束時間_分.TextAlgin = System.Windows.Forms.HorizontalAlignment.Right;
            this.rJ_TextBox_結束時間_分.Texts = "";
            this.rJ_TextBox_結束時間_分.UnderlineStyle = false;
            // 
            // rJ_Lable6
            // 
            this.rJ_Lable6.BackColor = System.Drawing.Color.White;
            this.rJ_Lable6.BackgroundColor = System.Drawing.Color.White;
            this.rJ_Lable6.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable6.BorderRadius = 10;
            this.rJ_Lable6.BorderSize = 0;
            this.rJ_Lable6.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_Lable6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable6.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable6.ForeColor = System.Drawing.Color.Black;
            this.rJ_Lable6.GUID = "";
            this.rJ_Lable6.Location = new System.Drawing.Point(394, 5);
            this.rJ_Lable6.Name = "rJ_Lable6";
            this.rJ_Lable6.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable6.ShadowSize = 0;
            this.rJ_Lable6.Size = new System.Drawing.Size(26, 43);
            this.rJ_Lable6.TabIndex = 1;
            this.rJ_Lable6.Text = ":";
            this.rJ_Lable6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable6.TextColor = System.Drawing.Color.Black;
            // 
            // rJ_TextBox_結束時間_秒
            // 
            this.rJ_TextBox_結束時間_秒.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox_結束時間_秒.BorderColor = System.Drawing.Color.Black;
            this.rJ_TextBox_結束時間_秒.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox_結束時間_秒.BorderRadius = 0;
            this.rJ_TextBox_結束時間_秒.BorderSize = 1;
            this.rJ_TextBox_結束時間_秒.Dock = System.Windows.Forms.DockStyle.Right;
            this.rJ_TextBox_結束時間_秒.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox_結束時間_秒.ForeColor = System.Drawing.Color.Black;
            this.rJ_TextBox_結束時間_秒.GUID = "";
            this.rJ_TextBox_結束時間_秒.Location = new System.Drawing.Point(420, 5);
            this.rJ_TextBox_結束時間_秒.Multiline = false;
            this.rJ_TextBox_結束時間_秒.Name = "rJ_TextBox_結束時間_秒";
            this.rJ_TextBox_結束時間_秒.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox_結束時間_秒.PassWordChar = false;
            this.rJ_TextBox_結束時間_秒.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox_結束時間_秒.PlaceholderText = "秒";
            this.rJ_TextBox_結束時間_秒.ShowTouchPannel = false;
            this.rJ_TextBox_結束時間_秒.Size = new System.Drawing.Size(75, 42);
            this.rJ_TextBox_結束時間_秒.TabIndex = 0;
            this.rJ_TextBox_結束時間_秒.TextAlgin = System.Windows.Forms.HorizontalAlignment.Right;
            this.rJ_TextBox_結束時間_秒.Texts = "";
            this.rJ_TextBox_結束時間_秒.UnderlineStyle = false;
            // 
            // rJ_Lable7
            // 
            this.rJ_Lable7.BackColor = System.Drawing.Color.White;
            this.rJ_Lable7.BackgroundColor = System.Drawing.Color.Silver;
            this.rJ_Lable7.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable7.BorderRadius = 10;
            this.rJ_Lable7.BorderSize = 0;
            this.rJ_Lable7.Dock = System.Windows.Forms.DockStyle.Top;
            this.rJ_Lable7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable7.Font = new System.Drawing.Font("微軟正黑體", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Lable7.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable7.GUID = "";
            this.rJ_Lable7.Location = new System.Drawing.Point(2, 2);
            this.rJ_Lable7.Name = "rJ_Lable7";
            this.rJ_Lable7.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable7.ShadowSize = 3;
            this.rJ_Lable7.Size = new System.Drawing.Size(500, 72);
            this.rJ_Lable7.TabIndex = 16;
            this.rJ_Lable7.Text = "結束時間";
            this.rJ_Lable7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable7.TextColor = System.Drawing.Color.White;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.plC_RJ_Button_返回);
            this.panel3.Controls.Add(this.plC_RJ_Button_確認);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(4, 628);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1050, 88);
            this.panel3.TabIndex = 18;
            // 
            // plC_RJ_Button_返回
            // 
            this.plC_RJ_Button_返回.AutoResetState = false;
            this.plC_RJ_Button_返回.BackgroundColor = System.Drawing.Color.Gray;
            this.plC_RJ_Button_返回.Bool = false;
            this.plC_RJ_Button_返回.BorderColor = System.Drawing.Color.Thistle;
            this.plC_RJ_Button_返回.BorderRadius = 20;
            this.plC_RJ_Button_返回.BorderSize = 0;
            this.plC_RJ_Button_返回.but_press = false;
            this.plC_RJ_Button_返回.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_返回.Dock = System.Windows.Forms.DockStyle.Right;
            this.plC_RJ_Button_返回.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_返回.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_返回.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_返回.GUID = "";
            this.plC_RJ_Button_返回.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_返回.Image_padding = new System.Windows.Forms.Padding(0);
            this.plC_RJ_Button_返回.Location = new System.Drawing.Point(728, 0);
            this.plC_RJ_Button_返回.Name = "plC_RJ_Button_返回";
            this.plC_RJ_Button_返回.OFF_文字內容 = "返回";
            this.plC_RJ_Button_返回.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_返回.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_返回.OFF_背景顏色 = System.Drawing.Color.Gray;
            this.plC_RJ_Button_返回.ON_BorderSize = 5;
            this.plC_RJ_Button_返回.ON_文字內容 = "返回";
            this.plC_RJ_Button_返回.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_返回.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_返回.ON_背景顏色 = System.Drawing.Color.Gray;
            this.plC_RJ_Button_返回.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_返回.ShadowSize = 3;
            this.plC_RJ_Button_返回.ShowLoadingForm = false;
            this.plC_RJ_Button_返回.Size = new System.Drawing.Size(161, 88);
            this.plC_RJ_Button_返回.State = false;
            this.plC_RJ_Button_返回.TabIndex = 12;
            this.plC_RJ_Button_返回.Text = "返回";
            this.plC_RJ_Button_返回.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_返回.TextHeight = 0;
            this.plC_RJ_Button_返回.Texts = "返回";
            this.plC_RJ_Button_返回.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_返回.字型鎖住 = false;
            this.plC_RJ_Button_返回.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_返回.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_返回.文字鎖住 = false;
            this.plC_RJ_Button_返回.背景圖片 = null;
            this.plC_RJ_Button_返回.讀取位元反向 = false;
            this.plC_RJ_Button_返回.讀寫鎖住 = false;
            this.plC_RJ_Button_返回.音效 = false;
            this.plC_RJ_Button_返回.顯示 = false;
            this.plC_RJ_Button_返回.顯示狀態 = false;
            // 
            // plC_RJ_Button_確認
            // 
            this.plC_RJ_Button_確認.AutoResetState = false;
            this.plC_RJ_Button_確認.BackgroundColor = System.Drawing.Color.Black;
            this.plC_RJ_Button_確認.Bool = false;
            this.plC_RJ_Button_確認.BorderColor = System.Drawing.Color.Thistle;
            this.plC_RJ_Button_確認.BorderRadius = 20;
            this.plC_RJ_Button_確認.BorderSize = 0;
            this.plC_RJ_Button_確認.but_press = false;
            this.plC_RJ_Button_確認.buttonType = MyUI.RJ_Button.ButtonType.Toggle;
            this.plC_RJ_Button_確認.Dock = System.Windows.Forms.DockStyle.Right;
            this.plC_RJ_Button_確認.FlatAppearance.BorderSize = 0;
            this.plC_RJ_Button_確認.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plC_RJ_Button_確認.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_確認.GUID = "";
            this.plC_RJ_Button_確認.Icon = System.Windows.Forms.MessageBoxIcon.Warning;
            this.plC_RJ_Button_確認.Image_padding = new System.Windows.Forms.Padding(0);
            this.plC_RJ_Button_確認.Location = new System.Drawing.Point(889, 0);
            this.plC_RJ_Button_確認.Name = "plC_RJ_Button_確認";
            this.plC_RJ_Button_確認.OFF_文字內容 = "確認";
            this.plC_RJ_Button_確認.OFF_文字字體 = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.plC_RJ_Button_確認.OFF_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_確認.OFF_背景顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_確認.ON_BorderSize = 5;
            this.plC_RJ_Button_確認.ON_文字內容 = "確認";
            this.plC_RJ_Button_確認.ON_文字字體 = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold);
            this.plC_RJ_Button_確認.ON_文字顏色 = System.Drawing.Color.White;
            this.plC_RJ_Button_確認.ON_背景顏色 = System.Drawing.Color.Black;
            this.plC_RJ_Button_確認.ShadowColor = System.Drawing.Color.DimGray;
            this.plC_RJ_Button_確認.ShadowSize = 3;
            this.plC_RJ_Button_確認.ShowLoadingForm = false;
            this.plC_RJ_Button_確認.Size = new System.Drawing.Size(161, 88);
            this.plC_RJ_Button_確認.State = false;
            this.plC_RJ_Button_確認.TabIndex = 11;
            this.plC_RJ_Button_確認.Text = "確認";
            this.plC_RJ_Button_確認.TextColor = System.Drawing.Color.White;
            this.plC_RJ_Button_確認.TextHeight = 0;
            this.plC_RJ_Button_確認.Texts = "確認";
            this.plC_RJ_Button_確認.UseVisualStyleBackColor = false;
            this.plC_RJ_Button_確認.字型鎖住 = false;
            this.plC_RJ_Button_確認.按鈕型態 = MyUI.PLC_RJ_Button.StatusEnum.保持型;
            this.plC_RJ_Button_確認.按鍵方式 = MyUI.PLC_RJ_Button.PressEnum.Mouse_左鍵;
            this.plC_RJ_Button_確認.文字鎖住 = false;
            this.plC_RJ_Button_確認.背景圖片 = null;
            this.plC_RJ_Button_確認.讀取位元反向 = false;
            this.plC_RJ_Button_確認.讀寫鎖住 = false;
            this.plC_RJ_Button_確認.音效 = false;
            this.plC_RJ_Button_確認.顯示 = false;
            this.plC_RJ_Button_確認.顯示狀態 = false;
            // 
            // Dialog_DateTimeIntervel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1058, 720);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.rJ_Pannel2);
            this.Controls.Add(this.rJ_Pannel1);
            this.Controls.Add(this.rJ_Lable1);
            this.Name = "Dialog_DateTimeIntervel";
            this.rJ_Pannel1.ResumeLayout(false);
            this.panel_起始時間_時分秒.ResumeLayout(false);
            this.rJ_Pannel2.ResumeLayout(false);
            this.panel_結束時間_時分秒.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RJ_Lable rJ_Lable1;
        private RJ_Pannel rJ_Pannel1;
        private Sunny.UI.UICalendar calendarStart;
        private System.Windows.Forms.Panel panel_起始時間_時分秒;
        private RJ_TextBox rJ_TextBox_起始時間_時;
        private RJ_Lable rJ_Lable4;
        private RJ_TextBox rJ_TextBox_起始時間_分;
        private RJ_Lable rJ_Lable3;
        private RJ_TextBox rJ_TextBox_起始時間_秒;
        private RJ_Lable rJ_Lable2;
        private RJ_Pannel rJ_Pannel2;
        private Sunny.UI.UICalendar calendarEnd;
        private System.Windows.Forms.Panel panel_結束時間_時分秒;
        private RJ_TextBox rJ_TextBox_結束時間_時;
        private RJ_Lable rJ_Lable5;
        private RJ_TextBox rJ_TextBox_結束時間_分;
        private RJ_Lable rJ_Lable6;
        private RJ_TextBox rJ_TextBox_結束時間_秒;
        private RJ_Lable rJ_Lable7;
        private System.Windows.Forms.Panel panel3;
        private PLC_RJ_Button plC_RJ_Button_返回;
        private PLC_RJ_Button plC_RJ_Button_確認;
    }
}