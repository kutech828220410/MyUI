namespace UpdateProgram
{
    partial class UpdatForm
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdatForm));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.but_瀏覽 = new System.Windows.Forms.Button();
            this.textBox_檔案路徑 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_State = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.exButton_發送檔案 = new MyUI.ExButton();
            this.uDP_Cilent = new TCP.UDP_Cilent();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // but_瀏覽
            // 
            this.but_瀏覽.Font = new System.Drawing.Font("新細明體", 12F);
            this.but_瀏覽.Location = new System.Drawing.Point(298, 55);
            this.but_瀏覽.Name = "but_瀏覽";
            this.but_瀏覽.Size = new System.Drawing.Size(98, 38);
            this.but_瀏覽.TabIndex = 1;
            this.but_瀏覽.Text = "瀏覽";
            this.but_瀏覽.UseVisualStyleBackColor = true;
            this.but_瀏覽.Click += new System.EventHandler(this.but_瀏覽_Click);
            // 
            // textBox_檔案路徑
            // 
            this.textBox_檔案路徑.Location = new System.Drawing.Point(92, 25);
            this.textBox_檔案路徑.Name = "textBox_檔案路徑";
            this.textBox_檔案路徑.Size = new System.Drawing.Size(304, 27);
            this.textBox_檔案路徑.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "檔案路徑 :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.but_瀏覽);
            this.groupBox1.Controls.Add(this.textBox_檔案路徑);
            this.groupBox1.Font = new System.Drawing.Font("新細明體", 12F);
            this.groupBox1.Location = new System.Drawing.Point(378, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 100);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.exButton_發送檔案);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Font = new System.Drawing.Font("新細明體", 12F);
            this.groupBox2.Location = new System.Drawing.Point(378, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(405, 198);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "發送";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label_State);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(9, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(387, 44);
            this.panel1.TabIndex = 0;
            // 
            // label_State
            // 
            this.label_State.AutoSize = true;
            this.label_State.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_State.Location = new System.Drawing.Point(52, 13);
            this.label_State.Name = "label_State";
            this.label_State.Size = new System.Drawing.Size(330, 18);
            this.label_State.TabIndex = 1;
            this.label_State.Text = "########################################";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "狀態 :";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // exButton_發送檔案
            // 
            this.exButton_發送檔案.Location = new System.Drawing.Point(268, 141);
            this.exButton_發送檔案.Margin = new System.Windows.Forms.Padding(4);
            this.exButton_發送檔案.Name = "exButton_發送檔案";
            this.exButton_發送檔案.OFF_文字內容 = "發送檔案";
            this.exButton_發送檔案.OFF_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_發送檔案.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_發送檔案.ON_文字內容 = "發送檔案";
            this.exButton_發送檔案.ON_文字字體 = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_發送檔案.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_發送檔案.Size = new System.Drawing.Size(130, 50);
            this.exButton_發送檔案.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_發送檔案.TabIndex = 1;
            this.exButton_發送檔案.字型鎖住 = false;
            this.exButton_發送檔案.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_發送檔案.文字鎖住 = false;
            this.exButton_發送檔案.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_發送檔案.狀態OFF圖片")));
            this.exButton_發送檔案.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_發送檔案.狀態ON圖片")));
            this.exButton_發送檔案.讀寫鎖住 = false;
            this.exButton_發送檔案.音效 = false;
            this.exButton_發送檔案.顯示狀態 = false;
            // 
            // uDP_Cilent
            // 
            this.uDP_Cilent.Location = new System.Drawing.Point(12, 12);
            this.uDP_Cilent.Name = "uDP_Cilent";
            this.uDP_Cilent.Size = new System.Drawing.Size(360, 528);
            this.uDP_Cilent.TabIndex = 0;
            // 
            // UpdatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 546);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.uDP_Cilent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdatForm";
            this.Text = "Update";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TCP.UDP_Cilent uDP_Cilent;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button but_瀏覽;
        private System.Windows.Forms.TextBox textBox_檔案路徑;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_State;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private MyUI.ExButton exButton_發送檔案;
    }
}

