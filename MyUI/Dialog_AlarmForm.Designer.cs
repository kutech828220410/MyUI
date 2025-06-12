
namespace MyUI
{
    partial class Dialog_AlarmForm
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
            this.rJ_Lable_text = new MyUI.RJ_Lable();
            this.SuspendLayout();
            // 
            // rJ_Lable_text
            // 
            this.rJ_Lable_text.BackColor = System.Drawing.Color.White;
            this.rJ_Lable_text.BackgroundColor = System.Drawing.Color.DarkRed;
            this.rJ_Lable_text.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Lable_text.BorderRadius = 10;
            this.rJ_Lable_text.BorderSize = 0;
            this.rJ_Lable_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_Lable_text.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Lable_text.Font = new System.Drawing.Font("微軟正黑體", 36F, System.Drawing.FontStyle.Bold);
            this.rJ_Lable_text.ForeColor = System.Drawing.Color.Transparent;
            this.rJ_Lable_text.GUID = "";
            this.rJ_Lable_text.Location = new System.Drawing.Point(14, 24);
            this.rJ_Lable_text.Name = "rJ_Lable_text";
            this.rJ_Lable_text.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Lable_text.ShadowSize = 0;
            this.rJ_Lable_text.Size = new System.Drawing.Size(745, 119);
            this.rJ_Lable_text.TabIndex = 0;
            this.rJ_Lable_text.Text = "錯誤提示";
            this.rJ_Lable_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rJ_Lable_text.TextColor = System.Drawing.Color.White;
            this.rJ_Lable_text.UseMnemonic = false;
            // 
            // Dialog_AlarmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CaptionHeight = 10;
            this.ClientSize = new System.Drawing.Size(773, 157);
            this.Controls.Add(this.rJ_Lable_text);
            this.MaximizeBox = false;
            this.Name = "Dialog_AlarmForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ResumeLayout(false);

        }

        #endregion

        private RJ_Lable rJ_Lable_text;
    }
}