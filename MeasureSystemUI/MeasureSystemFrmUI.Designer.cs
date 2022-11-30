namespace MeasureSystemUI
{
    partial class MeasureSystemFrmUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeasureSystemFrmUI));
            this.menuStrip_上方工作列 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.開啟專案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.儲存專案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.關閉ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.系統模組ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.檔案路徑瀏覽ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.影像路徑瀏覽ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.影像宣告模組ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._8位深度影像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._24位深度彩色影像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.開始執行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.逐步執行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止跳出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.變量編輯ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView_模组列表 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip_模组列表 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.編輯ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刪除模組ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刪除相關所有模組ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lowerMachine_Panel1 = new LadderUI.LowerMachine_Panel();
            this.plC_UI_Init1 = new MyUI.PLC_UI_Init();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.編輯註解toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_上方工作列.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_模组列表)).BeginInit();
            this.contextMenuStrip_模组列表.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip_上方工作列
            // 
            this.menuStrip_上方工作列.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem4,
            this.toolStripMenuItem3});
            this.menuStrip_上方工作列.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_上方工作列.Name = "menuStrip_上方工作列";
            this.menuStrip_上方工作列.Size = new System.Drawing.Size(1642, 24);
            this.menuStrip_上方工作列.TabIndex = 0;
            this.menuStrip_上方工作列.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開啟專案ToolStripMenuItem,
            this.儲存專案ToolStripMenuItem,
            this.關閉ToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(43, 20);
            this.toolStripMenuItem1.Text = "檔案";
            // 
            // 開啟專案ToolStripMenuItem
            // 
            this.開啟專案ToolStripMenuItem.Name = "開啟專案ToolStripMenuItem";
            this.開啟專案ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.開啟專案ToolStripMenuItem.Text = "開啟專案";
            this.開啟專案ToolStripMenuItem.Click += new System.EventHandler(this.開啟專案ToolStripMenuItem_Click);
            // 
            // 儲存專案ToolStripMenuItem
            // 
            this.儲存專案ToolStripMenuItem.Name = "儲存專案ToolStripMenuItem";
            this.儲存專案ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.儲存專案ToolStripMenuItem.Text = "儲存專案";
            this.儲存專案ToolStripMenuItem.Click += new System.EventHandler(this.儲存專案ToolStripMenuItem_Click);
            // 
            // 關閉ToolStripMenuItem
            // 
            this.關閉ToolStripMenuItem.Name = "關閉ToolStripMenuItem";
            this.關閉ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.關閉ToolStripMenuItem.Text = "關閉";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系統模組ToolStripMenuItem,
            this.影像宣告模組ToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(55, 20);
            this.toolStripMenuItem2.Text = "工具箱";
            // 
            // 系統模組ToolStripMenuItem
            // 
            this.系統模組ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.檔案路徑瀏覽ToolStripMenuItem,
            this.影像路徑瀏覽ToolStripMenuItem});
            this.系統模組ToolStripMenuItem.Name = "系統模組ToolStripMenuItem";
            this.系統模組ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.系統模組ToolStripMenuItem.Text = "A-系統模組";
            // 
            // 檔案路徑瀏覽ToolStripMenuItem
            // 
            this.檔案路徑瀏覽ToolStripMenuItem.Name = "檔案路徑瀏覽ToolStripMenuItem";
            this.檔案路徑瀏覽ToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.檔案路徑瀏覽ToolStripMenuItem.Text = "00-檔案路徑瀏覽";
            this.檔案路徑瀏覽ToolStripMenuItem.Click += new System.EventHandler(this.檔案路徑瀏覽ToolStripMenuItem_Click);
            // 
            // 影像路徑瀏覽ToolStripMenuItem
            // 
            this.影像路徑瀏覽ToolStripMenuItem.Name = "影像路徑瀏覽ToolStripMenuItem";
            this.影像路徑瀏覽ToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.影像路徑瀏覽ToolStripMenuItem.Text = "01-影像路徑瀏覽";
            this.影像路徑瀏覽ToolStripMenuItem.Click += new System.EventHandler(this.影像路徑瀏覽ToolStripMenuItem_Click);
            // 
            // 影像宣告模組ToolStripMenuItem
            // 
            this.影像宣告模組ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._8位深度影像ToolStripMenuItem,
            this._24位深度彩色影像ToolStripMenuItem});
            this.影像宣告模組ToolStripMenuItem.Name = "影像宣告模組ToolStripMenuItem";
            this.影像宣告模組ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.影像宣告模組ToolStripMenuItem.Text = "B-影像宣告模組";
            // 
            // _8位深度影像ToolStripMenuItem
            // 
            this._8位深度影像ToolStripMenuItem.Name = "_8位深度影像ToolStripMenuItem";
            this._8位深度影像ToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this._8位深度影像ToolStripMenuItem.Text = "00-8位深度灰階影像";
            this._8位深度影像ToolStripMenuItem.Click += new System.EventHandler(this._8位深度影像ToolStripMenuItem_Click);
            // 
            // _24位深度彩色影像ToolStripMenuItem
            // 
            this._24位深度彩色影像ToolStripMenuItem.Name = "_24位深度彩色影像ToolStripMenuItem";
            this._24位深度彩色影像ToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this._24位深度彩色影像ToolStripMenuItem.Text = "01-24位深度彩色影像";
            this._24位深度彩色影像ToolStripMenuItem.Click += new System.EventHandler(this._24位深度彩色影像ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開始執行ToolStripMenuItem,
            this.逐步執行ToolStripMenuItem,
            this.停止跳出ToolStripMenuItem});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(43, 20);
            this.toolStripMenuItem4.Text = "偵錯";
            // 
            // 開始執行ToolStripMenuItem
            // 
            this.開始執行ToolStripMenuItem.Name = "開始執行ToolStripMenuItem";
            this.開始執行ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.開始執行ToolStripMenuItem.Text = "開始執行";
            // 
            // 逐步執行ToolStripMenuItem
            // 
            this.逐步執行ToolStripMenuItem.Name = "逐步執行ToolStripMenuItem";
            this.逐步執行ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.逐步執行ToolStripMenuItem.Text = "逐步執行";
            // 
            // 停止跳出ToolStripMenuItem
            // 
            this.停止跳出ToolStripMenuItem.Name = "停止跳出ToolStripMenuItem";
            this.停止跳出ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.停止跳出ToolStripMenuItem.Text = "停止跳出";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.變量編輯ToolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(43, 20);
            this.toolStripMenuItem3.Text = "其他";
            // 
            // 變量編輯ToolStripMenuItem
            // 
            this.變量編輯ToolStripMenuItem.Name = "變量編輯ToolStripMenuItem";
            this.變量編輯ToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.變量編輯ToolStripMenuItem.Text = "變量編輯...";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView_模组列表);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 24);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(509, 976);
            this.panel3.TabIndex = 3;
            // 
            // dataGridView_模组列表
            // 
            this.dataGridView_模组列表.AllowUserToAddRows = false;
            this.dataGridView_模组列表.AllowUserToDeleteRows = false;
            this.dataGridView_模组列表.AllowUserToResizeColumns = false;
            this.dataGridView_模组列表.AllowUserToResizeRows = false;
            this.dataGridView_模组列表.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView_模组列表.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_模组列表.ContextMenuStrip = this.contextMenuStrip_模组列表;
            this.dataGridView_模组列表.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_模组列表.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_模组列表.Name = "dataGridView_模组列表";
            this.dataGridView_模组列表.ReadOnly = true;
            this.dataGridView_模组列表.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView_模组列表.RowTemplate.Height = 24;
            this.dataGridView_模组列表.Size = new System.Drawing.Size(509, 976);
            this.dataGridView_模组列表.TabIndex = 0;
            this.dataGridView_模组列表.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_模组列表_CellMouseDown);
            // 
            // contextMenuStrip_模组列表
            // 
            this.contextMenuStrip_模组列表.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.編輯ToolStripMenuItem,
            this.編輯註解toolStripMenuItem,
            this.刪除模組ToolStripMenuItem,
            this.刪除相關所有模組ToolStripMenuItem});
            this.contextMenuStrip_模组列表.Name = "contextMenuStrip_模组列表";
            this.contextMenuStrip_模组列表.Size = new System.Drawing.Size(171, 92);
            // 
            // 編輯ToolStripMenuItem
            // 
            this.編輯ToolStripMenuItem.Name = "編輯ToolStripMenuItem";
            this.編輯ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.編輯ToolStripMenuItem.Text = "編輯...";
            this.編輯ToolStripMenuItem.Click += new System.EventHandler(this.編輯ToolStripMenuItem_Click);
            // 
            // 刪除模組ToolStripMenuItem
            // 
            this.刪除模組ToolStripMenuItem.Name = "刪除模組ToolStripMenuItem";
            this.刪除模組ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.刪除模組ToolStripMenuItem.Text = "刪除模組";
            this.刪除模組ToolStripMenuItem.Click += new System.EventHandler(this.刪除模組ToolStripMenuItem_Click);
            // 
            // 刪除相關所有模組ToolStripMenuItem
            // 
            this.刪除相關所有模組ToolStripMenuItem.Name = "刪除相關所有模組ToolStripMenuItem";
            this.刪除相關所有模組ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.刪除相關所有模組ToolStripMenuItem.Text = "刪除相關所有模組";
            this.刪除相關所有模組ToolStripMenuItem.Click += new System.EventHandler(this.刪除相關所有模組ToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(509, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1133, 780);
            this.panel2.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1133, 780);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(1);
            this.tabPage1.Size = new System.Drawing.Size(1125, 754);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "輸出畫面";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
    
 
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lowerMachine_Panel1);
            this.tabPage2.Controls.Add(this.plC_UI_Init1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1125, 754);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "系統資訊";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lowerMachine_Panel1
            // 
            this.lowerMachine_Panel1.Location = new System.Drawing.Point(6, 6);
            this.lowerMachine_Panel1.Name = "lowerMachine_Panel1";
            this.lowerMachine_Panel1.Size = new System.Drawing.Size(869, 565);
            this.lowerMachine_Panel1.TabIndex = 2;
            // 
            // plC_UI_Init1
            // 
            this.plC_UI_Init1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.plC_UI_Init1.Location = new System.Drawing.Point(6, 723);
            this.plC_UI_Init1.Name = "plC_UI_Init1";
            this.plC_UI_Init1.Size = new System.Drawing.Size(72, 25);
            this.plC_UI_Init1.TabIndex = 1;
            this.plC_UI_Init1.Visible = false;
            this.plC_UI_Init1.全螢幕顯示 = false;
            this.plC_UI_Init1.起始畫面標題內容 = "鴻森整合機電有限公司";
            this.plC_UI_Init1.起始畫面標題字體 = new System.Drawing.Font("標楷體", 20F, System.Drawing.FontStyle.Bold);
            this.plC_UI_Init1.起始畫面背景 = ((System.Drawing.Image)(resources.GetObject("plC_UI_Init1.起始畫面背景")));
            this.plC_UI_Init1.起始畫面顯示 = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(509, 804);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1133, 196);
            this.panel1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(966, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "poj";
            this.openFileDialog.FileName = "*.poj";
            this.openFileDialog.Filter = "Project File (*poj)|*poj;";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "poj";
            this.saveFileDialog.Filter = "Project File (*poj)|*poj;";
            // 
            // 編輯註解toolStripMenuItem
            // 
            this.編輯註解toolStripMenuItem.Name = "編輯註解toolStripMenuItem";
            this.編輯註解toolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.編輯註解toolStripMenuItem.Text = "編輯註解...";
            // 
            // MeasureSystemFrmUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.menuStrip_上方工作列);
            this.Name = "MeasureSystemFrmUI";
            this.Size = new System.Drawing.Size(1642, 1000);
            this.Load += new System.EventHandler(this.MeasureSystemFrmUI_Load);
            this.menuStrip_上方工作列.ResumeLayout(false);
            this.menuStrip_上方工作列.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_模组列表)).EndInit();
            this.contextMenuStrip_模组列表.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip_上方工作列;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 系統模組ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 檔案路徑瀏覽ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 影像宣告模組ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _8位深度影像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _24位深度彩色影像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem 開始執行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 逐步執行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止跳出ToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView_模组列表;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private MyUI.PLC_UI_Init plC_UI_Init1;
        private LadderUI.LowerMachine_Panel lowerMachine_Panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_模组列表;
        private System.Windows.Forms.ToolStripMenuItem 刪除模組ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刪除相關所有模組ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 影像路徑瀏覽ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 開啟專案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 儲存專案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 關閉ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem 編輯ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 變量編輯ToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem 編輯註解toolStripMenuItem;
    }
}
