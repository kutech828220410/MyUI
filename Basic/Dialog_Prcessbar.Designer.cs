namespace Basic
{
    partial class Dialog_Prcessbar
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
            this.rJ_ProgressBar = new MyUI.RJ_ProgressBar();
            this.rJ_Button_取消 = new MyUI.RJ_Button();
            this.label_State = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rJ_ProgressBar
            // 
            this.rJ_ProgressBar.ChannelColor = System.Drawing.Color.LightSteelBlue;
            this.rJ_ProgressBar.ChannelHeight = 50;
            this.rJ_ProgressBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.rJ_ProgressBar.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_ProgressBar.ForeBackColor = System.Drawing.Color.RoyalBlue;
            this.rJ_ProgressBar.ForeColor = System.Drawing.Color.White;
            this.rJ_ProgressBar.Location = new System.Drawing.Point(0, 0);
            this.rJ_ProgressBar.Name = "rJ_ProgressBar";
            this.rJ_ProgressBar.ShowMaximun = false;
            this.rJ_ProgressBar.ShowValue = MyUI.TextPosition.Right;
            this.rJ_ProgressBar.Size = new System.Drawing.Size(533, 85);
            this.rJ_ProgressBar.SliderColor = System.Drawing.Color.RoyalBlue;
            this.rJ_ProgressBar.SliderHeight = 50;
            this.rJ_ProgressBar.SymbolAfter = "";
            this.rJ_ProgressBar.SymbolBefore = "";
            this.rJ_ProgressBar.TabIndex = 0;
            // 
            // rJ_Button_取消
            // 
            this.rJ_Button_取消.AutoResetState = false;
            this.rJ_Button_取消.BackColor = System.Drawing.Color.Gray;
            this.rJ_Button_取消.BackgroundColor = System.Drawing.Color.Gray;
            this.rJ_Button_取消.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rJ_Button_取消.BorderRadius = 5;
            this.rJ_Button_取消.BorderSize = 0;
            this.rJ_Button_取消.buttonType = MyUI.RJ_Button.ButtonType.Push;
            this.rJ_Button_取消.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rJ_Button_取消.FlatAppearance.BorderSize = 0;
            this.rJ_Button_取消.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rJ_Button_取消.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rJ_Button_取消.ForeColor = System.Drawing.Color.White;
            this.rJ_Button_取消.GUID = "";
            this.rJ_Button_取消.Location = new System.Drawing.Point(533, 0);
            this.rJ_Button_取消.Name = "rJ_Button_取消";
            this.rJ_Button_取消.Size = new System.Drawing.Size(88, 85);
            this.rJ_Button_取消.State = false;
            this.rJ_Button_取消.TabIndex = 20;
            this.rJ_Button_取消.Text = "取消";
            this.rJ_Button_取消.TextColor = System.Drawing.Color.White;
            this.rJ_Button_取消.UseVisualStyleBackColor = false;
            // 
            // label_State
            // 
            this.label_State.AutoSize = true;
            this.label_State.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_State.Location = new System.Drawing.Point(3, 2);
            this.label_State.Name = "label_State";
            this.label_State.Size = new System.Drawing.Size(0, 20);
            this.label_State.TabIndex = 21;
            // 
            // Dialog_Prcessbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(621, 85);
            this.ControlBox = false;
            this.Controls.Add(this.label_State);
            this.Controls.Add(this.rJ_Button_取消);
            this.Controls.Add(this.rJ_ProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Dialog_Prcessbar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Dialog_Prcessbar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyUI.RJ_ProgressBar rJ_ProgressBar;
        private MyUI.RJ_Button rJ_Button_取消;
        private System.Windows.Forms.Label label_State;
    }
}