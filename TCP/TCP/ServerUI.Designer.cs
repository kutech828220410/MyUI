namespace TCP
{
    partial class ServerUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerUI));
            this.groupBox58 = new System.Windows.Forms.GroupBox();
            this.richTextBox_狀態視窗 = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox_IPList = new System.Windows.Forms.ComboBox();
            this.checkBox_UI更新 = new System.Windows.Forms.CheckBox();
            this.exButton_開始監聽 = new MyUI.ExButton();
            this.checkBox_自動連線 = new System.Windows.Forms.CheckBox();
            this.textBox_PORT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox58.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox58
            // 
            this.groupBox58.Controls.Add(this.richTextBox_狀態視窗);
            this.groupBox58.Controls.Add(this.groupBox2);
            this.groupBox58.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox58.Location = new System.Drawing.Point(0, 0);
            this.groupBox58.Name = "groupBox58";
            this.groupBox58.Size = new System.Drawing.Size(357, 387);
            this.groupBox58.TabIndex = 18;
            this.groupBox58.TabStop = false;
            this.groupBox58.Text = "TCP-Server";
            // 
            // richTextBox_狀態視窗
            // 
            this.richTextBox_狀態視窗.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_狀態視窗.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_狀態視窗.Location = new System.Drawing.Point(3, 18);
            this.richTextBox_狀態視窗.Name = "richTextBox_狀態視窗";
            this.richTextBox_狀態視窗.Size = new System.Drawing.Size(351, 265);
            this.richTextBox_狀態視窗.TabIndex = 10;
            this.richTextBox_狀態視窗.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox_IPList);
            this.groupBox2.Controls.Add(this.checkBox_UI更新);
            this.groupBox2.Controls.Add(this.exButton_開始監聽);
            this.groupBox2.Controls.Add(this.checkBox_自動連線);
            this.groupBox2.Controls.Add(this.textBox_PORT);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Font = new System.Drawing.Font("新細明體", 12F);
            this.groupBox2.Location = new System.Drawing.Point(3, 283);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(351, 101);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Setting";
            // 
            // comboBox_IPList
            // 
            this.comboBox_IPList.FormattingEnabled = true;
            this.comboBox_IPList.Location = new System.Drawing.Point(6, 67);
            this.comboBox_IPList.Name = "comboBox_IPList";
            this.comboBox_IPList.Size = new System.Drawing.Size(148, 24);
            this.comboBox_IPList.TabIndex = 16;
            // 
            // checkBox_UI更新
            // 
            this.checkBox_UI更新.AutoSize = true;
            this.checkBox_UI更新.Font = new System.Drawing.Font("新細明體", 12F);
            this.checkBox_UI更新.Location = new System.Drawing.Point(173, 71);
            this.checkBox_UI更新.Name = "checkBox_UI更新";
            this.checkBox_UI更新.Size = new System.Drawing.Size(75, 20);
            this.checkBox_UI更新.TabIndex = 15;
            this.checkBox_UI更新.Text = "UI更新";
            this.checkBox_UI更新.UseVisualStyleBackColor = true;
            this.checkBox_UI更新.CheckedChanged += new System.EventHandler(this.checkBox_UI更新_CheckedChanged);
            // 
            // exButton_開始監聽
            // 
            this.exButton_開始監聽.Location = new System.Drawing.Point(206, 15);
            this.exButton_開始監聽.Name = "exButton_開始監聽";
            this.exButton_開始監聽.OFF_文字內容 = "開始監聽";
            this.exButton_開始監聽.OFF_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_開始監聽.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_開始監聽.ON_文字內容 = "開始監聽";
            this.exButton_開始監聽.ON_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_開始監聽.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_開始監聽.Size = new System.Drawing.Size(130, 50);
            this.exButton_開始監聽.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_開始監聽.TabIndex = 10;
            this.exButton_開始監聽.字型鎖住 = false;
            this.exButton_開始監聽.按鈕型態 = MyUI.ExButton.StatusEnum.交替型;
            this.exButton_開始監聽.文字鎖住 = false;
            this.exButton_開始監聽.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_開始監聽.狀態OFF圖片")));
            this.exButton_開始監聽.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_開始監聽.狀態ON圖片")));
            this.exButton_開始監聽.讀寫鎖住 = false;
            this.exButton_開始監聽.音效 = false;
            this.exButton_開始監聽.顯示狀態 = false;
            this.exButton_開始監聽.btnClick += new System.EventHandler(this.exButton_開始監聽_btnClick);
            // 
            // checkBox_自動連線
            // 
            this.checkBox_自動連線.AutoSize = true;
            this.checkBox_自動連線.Font = new System.Drawing.Font("新細明體", 12F);
            this.checkBox_自動連線.Location = new System.Drawing.Point(254, 71);
            this.checkBox_自動連線.Name = "checkBox_自動連線";
            this.checkBox_自動連線.Size = new System.Drawing.Size(91, 20);
            this.checkBox_自動連線.TabIndex = 14;
            this.checkBox_自動連線.Text = "自動連線";
            this.checkBox_自動連線.UseVisualStyleBackColor = true;
            this.checkBox_自動連線.CheckedChanged += new System.EventHandler(this.checkBox_自動連線_CheckedChanged);
            // 
            // textBox_PORT
            // 
            this.textBox_PORT.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox_PORT.Location = new System.Drawing.Point(83, 26);
            this.textBox_PORT.Name = "textBox_PORT";
            this.textBox_PORT.Size = new System.Drawing.Size(86, 27);
            this.textBox_PORT.TabIndex = 3;
            this.textBox_PORT.Text = "8080";
            this.textBox_PORT.TextChanged += new System.EventHandler(this.textBox_PORT_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F);
            this.label2.Location = new System.Drawing.Point(22, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "PORT";
            // 
            // ServerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox58);
            this.Name = "ServerUI";
            this.Size = new System.Drawing.Size(357, 387);
            this.groupBox58.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox58;
        private System.Windows.Forms.RichTextBox richTextBox_狀態視窗;
        private System.Windows.Forms.GroupBox groupBox2;
        private MyUI.ExButton exButton_開始監聽;
        private System.Windows.Forms.TextBox textBox_PORT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_UI更新;
        private System.Windows.Forms.CheckBox checkBox_自動連線;
        private System.Windows.Forms.ComboBox comboBox_IPList;
    }
}
