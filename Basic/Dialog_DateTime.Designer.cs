namespace Basic
{
    partial class Dialog_DateTime
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
            this.dateTimeComList = new MyUI.DateTimeComList();
            this.rJ_Button_Cancel = new MyUI.RJ_Button();
            this.rJ_Button_OK = new MyUI.RJ_Button();
            this.SuspendLayout();
            // 
            // dateTimeComList
            // 
            this.dateTimeComList.BackColor = System.Drawing.SystemColors.Window;
            this.dateTimeComList.Day = 1;
            this.dateTimeComList.End_Year = 2030;
            this.dateTimeComList.Location = new System.Drawing.Point(12, 17);
            this.dateTimeComList.mFont = new System.Drawing.Font("標楷體", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dateTimeComList.Month = 1;
            this.dateTimeComList.Name = "dateTimeComList";
            this.dateTimeComList.Size = new System.Drawing.Size(348, 51);
            this.dateTimeComList.Start_Year = 2022;
            this.dateTimeComList.TabIndex = 46;
            this.dateTimeComList.Value = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            this.dateTimeComList.Year = 2022;
            // 
            // rJ_Button_Cancel
            // 
            this.rJ_Button_Cancel.AutoResetState = false;
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
            this.rJ_Button_Cancel.GUID = "";
            this.rJ_Button_Cancel.Location = new System.Drawing.Point(470, 8);
            this.rJ_Button_Cancel.Name = "rJ_Button_Cancel";
            this.rJ_Button_Cancel.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_Cancel.ShadowSize = 0;
            this.rJ_Button_Cancel.ShowLoadingForm = false;
            this.rJ_Button_Cancel.Size = new System.Drawing.Size(79, 65);
            this.rJ_Button_Cancel.State = false;
            this.rJ_Button_Cancel.TabIndex = 45;
            this.rJ_Button_Cancel.Text = "Cancel";
            this.rJ_Button_Cancel.TextColor = System.Drawing.Color.White;
            this.rJ_Button_Cancel.UseVisualStyleBackColor = false;
            // 
            // rJ_Button_OK
            // 
            this.rJ_Button_OK.AutoResetState = false;
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
            this.rJ_Button_OK.GUID = "";
            this.rJ_Button_OK.Location = new System.Drawing.Point(385, 8);
            this.rJ_Button_OK.Name = "rJ_Button_OK";
            this.rJ_Button_OK.ShadowColor = System.Drawing.Color.DimGray;
            this.rJ_Button_OK.ShadowSize = 0;
            this.rJ_Button_OK.ShowLoadingForm = false;
            this.rJ_Button_OK.Size = new System.Drawing.Size(79, 65);
            this.rJ_Button_OK.State = false;
            this.rJ_Button_OK.TabIndex = 44;
            this.rJ_Button_OK.Text = "OK";
            this.rJ_Button_OK.TextColor = System.Drawing.Color.White;
            this.rJ_Button_OK.UseVisualStyleBackColor = false;
            // 
            // Dialog_DateTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(566, 80);
            this.ControlBox = false;
            this.Controls.Add(this.dateTimeComList);
            this.Controls.Add(this.rJ_Button_Cancel);
            this.Controls.Add(this.rJ_Button_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Dialog_DateTime";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
        private MyUI.RJ_Button rJ_Button_OK;
        private MyUI.RJ_Button rJ_Button_Cancel;
        private MyUI.DateTimeComList dateTimeComList;
    }
}