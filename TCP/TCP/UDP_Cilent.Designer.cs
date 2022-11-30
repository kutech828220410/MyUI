namespace TCP
{
    partial class UDP_Cilent
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UDP_Cilent));
            this.groupBox58 = new System.Windows.Forms.GroupBox();
            this.richTextBox_狀態視窗 = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_LocalPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_IPList = new System.Windows.Forms.ComboBox();
            this.checkBox_UI更新 = new System.Windows.Forms.CheckBox();
            this.checkBox_自動連線 = new System.Windows.Forms.CheckBox();
            this.exButton_開始連結 = new MyUI.ExButton();
            this.textBox_DNS域名 = new System.Windows.Forms.TextBox();
            this.checkBox_使用DNS解析 = new System.Windows.Forms.CheckBox();
            this.label220 = new System.Windows.Forms.Label();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.label219 = new System.Windows.Forms.Label();
            this.textBox_RemotePort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
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
            this.groupBox58.Size = new System.Drawing.Size(360, 528);
            this.groupBox58.TabIndex = 20;
            this.groupBox58.TabStop = false;
            this.groupBox58.Text = "UDP-Cilent";
            // 
            // richTextBox_狀態視窗
            // 
            this.richTextBox_狀態視窗.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_狀態視窗.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_狀態視窗.Location = new System.Drawing.Point(3, 18);
            this.richTextBox_狀態視窗.Name = "richTextBox_狀態視窗";
            this.richTextBox_狀態視窗.Size = new System.Drawing.Size(354, 300);
            this.richTextBox_狀態視窗.TabIndex = 10;
            this.richTextBox_狀態視窗.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_LocalPort);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.comboBox_IPList);
            this.groupBox2.Controls.Add(this.checkBox_UI更新);
            this.groupBox2.Controls.Add(this.checkBox_自動連線);
            this.groupBox2.Controls.Add(this.exButton_開始連結);
            this.groupBox2.Controls.Add(this.textBox_DNS域名);
            this.groupBox2.Controls.Add(this.checkBox_使用DNS解析);
            this.groupBox2.Controls.Add(this.label220);
            this.groupBox2.Controls.Add(this.textBox_IP);
            this.groupBox2.Controls.Add(this.label219);
            this.groupBox2.Controls.Add(this.textBox_RemotePort);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Font = new System.Drawing.Font("新細明體", 12F);
            this.groupBox2.Location = new System.Drawing.Point(3, 318);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(354, 207);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Setting";
            // 
            // textBox_LocalPort
            // 
            this.textBox_LocalPort.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox_LocalPort.Location = new System.Drawing.Point(283, 69);
            this.textBox_LocalPort.Name = "textBox_LocalPort";
            this.textBox_LocalPort.Size = new System.Drawing.Size(65, 27);
            this.textBox_LocalPort.TabIndex = 21;
            this.textBox_LocalPort.Text = "8080";
            this.textBox_LocalPort.TextChanged += new System.EventHandler(this.textBox_LocalPort_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F);
            this.label3.Location = new System.Drawing.Point(222, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "PORT :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(14, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "本機IP :";
            // 
            // comboBox_IPList
            // 
            this.comboBox_IPList.FormattingEnabled = true;
            this.comboBox_IPList.Location = new System.Drawing.Point(81, 69);
            this.comboBox_IPList.Name = "comboBox_IPList";
            this.comboBox_IPList.Size = new System.Drawing.Size(123, 24);
            this.comboBox_IPList.TabIndex = 18;
            // 
            // checkBox_UI更新
            // 
            this.checkBox_UI更新.AutoSize = true;
            this.checkBox_UI更新.Location = new System.Drawing.Point(31, 169);
            this.checkBox_UI更新.Name = "checkBox_UI更新";
            this.checkBox_UI更新.Size = new System.Drawing.Size(75, 20);
            this.checkBox_UI更新.TabIndex = 17;
            this.checkBox_UI更新.Text = "UI更新";
            this.checkBox_UI更新.UseVisualStyleBackColor = true;
            this.checkBox_UI更新.CheckedChanged += new System.EventHandler(this.checkBox_UI更新_CheckedChanged);
            // 
            // checkBox_自動連線
            // 
            this.checkBox_自動連線.AutoSize = true;
            this.checkBox_自動連線.Location = new System.Drawing.Point(109, 169);
            this.checkBox_自動連線.Name = "checkBox_自動連線";
            this.checkBox_自動連線.Size = new System.Drawing.Size(91, 20);
            this.checkBox_自動連線.TabIndex = 16;
            this.checkBox_自動連線.Text = "自動連線";
            this.checkBox_自動連線.UseVisualStyleBackColor = true;
            this.checkBox_自動連線.CheckedChanged += new System.EventHandler(this.checkBox_自動連線_CheckedChanged);
            // 
            // exButton_開始連結
            // 
            this.exButton_開始連結.Location = new System.Drawing.Point(206, 151);
            this.exButton_開始連結.Name = "exButton_開始連結";
            this.exButton_開始連結.OFF_文字內容 = "開始連結";
            this.exButton_開始連結.OFF_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_開始連結.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_開始連結.ON_文字內容 = "開始連結";
            this.exButton_開始連結.ON_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_開始連結.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_開始連結.Size = new System.Drawing.Size(130, 50);
            this.exButton_開始連結.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_開始連結.TabIndex = 10;
            this.exButton_開始連結.字型鎖住 = false;
            this.exButton_開始連結.按鈕型態 = MyUI.ExButton.StatusEnum.交替型;
            this.exButton_開始連結.文字鎖住 = false;
            this.exButton_開始連結.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_開始連結.狀態OFF圖片")));
            this.exButton_開始連結.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_開始連結.狀態ON圖片")));
            this.exButton_開始連結.讀寫鎖住 = false;
            this.exButton_開始連結.音效 = false;
            this.exButton_開始連結.顯示狀態 = false;
            this.exButton_開始連結.btnClick += new System.EventHandler(this.exButton_開始連結_btnClick);
            // 
            // textBox_DNS域名
            // 
            this.textBox_DNS域名.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox_DNS域名.Location = new System.Drawing.Point(158, 118);
            this.textBox_DNS域名.Name = "textBox_DNS域名";
            this.textBox_DNS域名.Size = new System.Drawing.Size(178, 27);
            this.textBox_DNS域名.TabIndex = 9;
            this.textBox_DNS域名.TextChanged += new System.EventHandler(this.textBox_DNS域名_TextChanged);
            // 
            // checkBox_使用DNS解析
            // 
            this.checkBox_使用DNS解析.AutoSize = true;
            this.checkBox_使用DNS解析.Location = new System.Drawing.Point(17, 121);
            this.checkBox_使用DNS解析.Name = "checkBox_使用DNS解析";
            this.checkBox_使用DNS解析.Size = new System.Drawing.Size(89, 20);
            this.checkBox_使用DNS解析.TabIndex = 8;
            this.checkBox_使用DNS解析.Text = "DNS解析";
            this.checkBox_使用DNS解析.UseVisualStyleBackColor = true;
            this.checkBox_使用DNS解析.CheckedChanged += new System.EventHandler(this.checkBox_使用DNS解析_CheckedChanged);
            // 
            // label220
            // 
            this.label220.AutoSize = true;
            this.label220.Font = new System.Drawing.Font("新細明體", 12F);
            this.label220.Location = new System.Drawing.Point(112, 123);
            this.label220.Name = "label220";
            this.label220.Size = new System.Drawing.Size(40, 16);
            this.label220.TabIndex = 7;
            this.label220.Text = "域名";
            // 
            // textBox_IP
            // 
            this.textBox_IP.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox_IP.Location = new System.Drawing.Point(81, 26);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(123, 27);
            this.textBox_IP.TabIndex = 6;
            this.textBox_IP.Text = "192.168.100.1";
            this.textBox_IP.TextChanged += new System.EventHandler(this.textBox_IP_TextChanged);
            // 
            // label219
            // 
            this.label219.AutoSize = true;
            this.label219.Font = new System.Drawing.Font("新細明體", 12F);
            this.label219.Location = new System.Drawing.Point(14, 31);
            this.label219.Name = "label219";
            this.label219.Size = new System.Drawing.Size(61, 16);
            this.label219.TabIndex = 5;
            this.label219.Text = "連結IP :";
            // 
            // textBox_RemotePort
            // 
            this.textBox_RemotePort.Font = new System.Drawing.Font("新細明體", 12F);
            this.textBox_RemotePort.Location = new System.Drawing.Point(283, 26);
            this.textBox_RemotePort.Name = "textBox_RemotePort";
            this.textBox_RemotePort.Size = new System.Drawing.Size(65, 27);
            this.textBox_RemotePort.TabIndex = 3;
            this.textBox_RemotePort.Text = "8080";
            this.textBox_RemotePort.TextChanged += new System.EventHandler(this.textBox_PORT_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F);
            this.label2.Location = new System.Drawing.Point(222, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "PORT :";
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // UDP_Cilent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox58);
            this.Name = "UDP_Cilent";
            this.Size = new System.Drawing.Size(360, 528);
            this.Load += new System.EventHandler(this.UDP_Cilent_Load);
            this.groupBox58.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox58;
        private System.Windows.Forms.RichTextBox richTextBox_狀態視窗;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox_UI更新;
        private System.Windows.Forms.CheckBox checkBox_自動連線;
        private MyUI.ExButton exButton_開始連結;
        private System.Windows.Forms.TextBox textBox_DNS域名;
        private System.Windows.Forms.CheckBox checkBox_使用DNS解析;
        private System.Windows.Forms.Label label220;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.Label label219;
        private System.Windows.Forms.TextBox textBox_RemotePort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_IPList;
        private System.Windows.Forms.TextBox textBox_LocalPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer;
    }
}
