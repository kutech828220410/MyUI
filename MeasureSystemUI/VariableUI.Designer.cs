namespace MeasureSystemUI
{
    partial class VariableUI
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
            this.comboBox_VariableItem = new System.Windows.Forms.ComboBox();
            this.textBox_Value = new System.Windows.Forms.TextBox();
            this.textBox_Comment = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox_VariableItem
            // 
            this.comboBox_VariableItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_VariableItem.Font = new System.Drawing.Font("新細明體", 12F);
            this.comboBox_VariableItem.FormattingEnabled = true;
            this.comboBox_VariableItem.Location = new System.Drawing.Point(13, 15);
            this.comboBox_VariableItem.Name = "comboBox_VariableItem";
            this.comboBox_VariableItem.Size = new System.Drawing.Size(67, 24);
            this.comboBox_VariableItem.TabIndex = 3;
            this.comboBox_VariableItem.SelectedIndexChanged += new System.EventHandler(this.comboBox_VariableItem_SelectedIndexChanged);
            // 
            // textBox_Value
            // 
            this.textBox_Value.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox_Value.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox_Value.Location = new System.Drawing.Point(242, 14);
            this.textBox_Value.Name = "textBox_Value";
            this.textBox_Value.Size = new System.Drawing.Size(250, 27);
            this.textBox_Value.TabIndex = 5;
            this.textBox_Value.TextChanged += new System.EventHandler(this.textBox_Value_TextChanged);
            // 
            // textBox_Comment
            // 
            this.textBox_Comment.BackColor = System.Drawing.Color.Cornsilk;
            this.textBox_Comment.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox_Comment.Location = new System.Drawing.Point(86, 14);
            this.textBox_Comment.Name = "textBox_Comment";
            this.textBox_Comment.Size = new System.Drawing.Size(150, 27);
            this.textBox_Comment.TabIndex = 4;
            this.textBox_Comment.TextChanged += new System.EventHandler(this.textBox_Comment_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Type";
            // 
            // VariableUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_VariableItem);
            this.Controls.Add(this.textBox_Value);
            this.Controls.Add(this.textBox_Comment);
            this.Name = "VariableUI";
            this.Size = new System.Drawing.Size(505, 55);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_VariableItem;
        private System.Windows.Forms.TextBox textBox_Comment;
        public System.Windows.Forms.TextBox textBox_Value;
        private System.Windows.Forms.Label label1;

    }
}
