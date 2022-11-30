
namespace Basic
{
    partial class Dialog_TextBox
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
            this.rJ_TextBox1 = new MyUI.RJ_TextBox();
            this.rJ_Button_Cancel = new MyUI.RJ_Button();
            this.rJ_Button_OK = new MyUI.RJ_Button();
            this.label_Title = new MyUI.RJ_Lable();
            this.SuspendLayout();
            // 
            // rJ_TextBox1
            // 
            this.rJ_TextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.rJ_TextBox1.BorderColor = System.Drawing.Color.SkyBlue;
            this.rJ_TextBox1.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rJ_TextBox1.BorderRadius = 0;
            this.rJ_TextBox1.BorderSize = 2;
            this.rJ_TextBox1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_TextBox1.ForeColor = System.Drawing.Color.DimGray;
            this.rJ_TextBox1.Location = new System.Drawing.Point(170, 13);
            this.rJ_TextBox1.Multiline = false;
            this.rJ_TextBox1.Name = "rJ_TextBox1";
            this.rJ_TextBox1.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rJ_TextBox1.PassWordChar = false;
            this.rJ_TextBox1.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rJ_TextBox1.PlaceholderText = "";
            this.rJ_TextBox1.ShowTouchPannel = false;
            this.rJ_TextBox1.Size = new System.Drawing.Size(250, 42);
            this.rJ_TextBox1.TabIndex = 0;
            this.rJ_TextBox1.TextAlgin = System.Windows.Forms.HorizontalAlignment.Left;
            this.rJ_TextBox1.Texts = "";
            this.rJ_TextBox1.UnderlineStyle = false;
            // 
            // rJ_Button_Cancel
            // 
            this.rJ_Button_Cancel.BackColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_Cancel.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_Cancel.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_Cancel.BorderRadius = 10;
            this.rJ_Button_Cancel.BorderSize = 0;
            this.rJ_Button_Cancel.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_Cancel.FlatAppearance.BorderSize = 0;
            this.rJ_Button_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_Cancel.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_Cancel.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_Cancel.Location = new System.Drawing.Point(511, 13);
            this.rJ_Button_Cancel.Name = "rJ_Button_Cancel";
            this.rJ_Button_Cancel.Size = new System.Drawing.Size(84, 42);
            this.rJ_Button_Cancel.State = false;
            this.rJ_Button_Cancel.TabIndex = 47;
            this.rJ_Button_Cancel.Text = "Cancel";
            this.rJ_Button_Cancel.TextColor = System.Drawing.Color.White;
            this.rJ_Button_Cancel.UseVisualStyleBackColor = false;
            this.rJ_Button_Cancel.Click += new System.EventHandler(this.rJ_Button_Cancel_Click);
            // 
            // rJ_Button_OK
            // 
            this.rJ_Button_OK.BackColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_OK.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.rJ_Button_OK.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_OK.BorderRadius = 10;
            this.rJ_Button_OK.BorderSize = 0;
            this.rJ_Button_OK.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_OK.FlatAppearance.BorderSize = 0;
            this.rJ_Button_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_OK.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_OK.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_OK.Location = new System.Drawing.Point(426, 13);
            this.rJ_Button_OK.Name = "rJ_Button_OK";
            this.rJ_Button_OK.Size = new System.Drawing.Size(84, 42);
            this.rJ_Button_OK.State = false;
            this.rJ_Button_OK.TabIndex = 46;
            this.rJ_Button_OK.Text = "OK";
            this.rJ_Button_OK.TextColor = System.Drawing.Color.White;
            this.rJ_Button_OK.UseVisualStyleBackColor = false;
            this.rJ_Button_OK.Click += new System.EventHandler(this.rJ_Button_OK_Click);
            // 
            // label_Title
            // 
            this.label_Title.BackColor = System.Drawing.Color.SkyBlue;
            this.label_Title.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.label_Title.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.label_Title.BorderRadius = 12;
            this.label_Title.BorderSize = 0;
            this.label_Title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Title.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.label_Title.ForeColor = System.Drawing.Color.White;
            this.label_Title.Location = new System.Drawing.Point(12, 14);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(150, 40);
            this.label_Title.TabIndex = 48;
            this.label_Title.Text = "Title";
            this.label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_Title.TextColor = System.Drawing.Color.White;
            // 
            // Dialog_TextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(604, 68);
            this.ControlBox = false;
            this.Controls.Add(this.label_Title);
            this.Controls.Add(this.rJ_Button_Cancel);
            this.Controls.Add(this.rJ_Button_OK);
            this.Controls.Add(this.rJ_TextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Dialog_TextBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        private MyUI.RJ_TextBox rJ_TextBox1;
        private MyUI.RJ_Button rJ_Button_Cancel;
        private MyUI.RJ_Button rJ_Button_OK;
        private MyUI.RJ_Lable label_Title;
    }
}