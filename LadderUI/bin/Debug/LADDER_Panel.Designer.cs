namespace LadderUI
{
    partial class LADDER_Panel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LADDER_Panel));
            this.panel_main = new System.Windows.Forms.Panel();
            this.panel_程式分頁 = new System.Windows.Forms.Panel();
            this.treeView_程式分頁 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip_TreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.調整大小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_工具箱 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel_工具列_檔案讀寫 = new System.Windows.Forms.FlowLayoutPanel();
            this.exButton_開新專案 = new MyUI.ExButton();
            this.exButton_讀取檔案 = new MyUI.ExButton();
            this.exButton_儲存檔案 = new MyUI.ExButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.exButton_剪下 = new MyUI.ExButton();
            this.exButton_複製 = new MyUI.ExButton();
            this.exButton_貼上 = new MyUI.ExButton();
            this.exButton_上一步 = new MyUI.ExButton();
            this.exButton_復原回未修正 = new MyUI.ExButton();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.exButton_寫入A接點 = new MyUI.ExButton();
            this.exButton_寫入B接點 = new MyUI.ExButton();
            this.exButton_畫橫線 = new MyUI.ExButton();
            this.exButton_畫豎線 = new MyUI.ExButton();
            this.exButton_刪除橫線 = new MyUI.ExButton();
            this.exButton_刪除豎線 = new MyUI.ExButton();
            this.exButton_窗選模式切換 = new MyUI.ExButton();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.exButton_語法切換 = new MyUI.ExButton();
            this.exButton_程式_註解模式選擇 = new MyUI.ExButton();
            this.exButton15 = new MyUI.ExButton();
            this.exButton_Online = new MyUI.ExButton();
            this.panel_Datagrid_註解列表 = new System.Windows.Forms.Panel();
            this.dataGridView_註解列表 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip_註解列表_右鑑選單 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.D_剪下 = new System.Windows.Forms.ToolStripMenuItem();
            this.D_複製 = new System.Windows.Forms.ToolStripMenuItem();
            this.D_貼上 = new System.Windows.Forms.ToolStripMenuItem();
            this.D_刪除 = new System.Windows.Forms.ToolStripMenuItem();
            this.D_選取全部 = new System.Windows.Forms.ToolStripMenuItem();
            this.exButton_註解查詢 = new MyUI.ExButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_註解查詢_Device_name = new System.Windows.Forms.TextBox();
            this.panel_LADDER = new System.Windows.Forms.Panel();
            this.panel_Listbox_IL指令集 = new System.Windows.Forms.Panel();
            this.listBox_IL指令集 = new System.Windows.Forms.ListBox();
            this.vScrollBar_picture_滾動條 = new System.Windows.Forms.VScrollBar();
            this.pictureBox_LADDER = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip_Ladder_右鍵選單 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.編譯 = new System.Windows.Forms.ToolStripMenuItem();
            this.復原回未編譯 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.復原 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.剪下 = new System.Windows.Forms.ToolStripMenuItem();
            this.複製 = new System.Windows.Forms.ToolStripMenuItem();
            this.貼上 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.插入一列 = new System.Windows.Forms.ToolStripMenuItem();
            this.刪除一列 = new System.Windows.Forms.ToolStripMenuItem();
            this.插入一行 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.繪製橫線 = new System.Windows.Forms.ToolStripMenuItem();
            this.刪除橫線 = new System.Windows.Forms.ToolStripMenuItem();
            this.繪製豎線 = new System.Windows.Forms.ToolStripMenuItem();
            this.刪除豎線 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.專案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新專案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.讀取ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.儲存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.關閉ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.顯示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem顯示目錄 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.顯示註解列表toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.顯示註解列表toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.註解顯示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.註解顏色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.註解字體ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.註解格式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.字母數ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox_註解字母數 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem_註解字母數_確認 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.列數ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox_註解列數 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_註解列數_確認 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.oNLINE字體ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem視窗字體 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.顯示_預設值ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.通訊ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上傳ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下載ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.程式比較ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker_LADDER_主程式 = new System.ComponentModel.BackgroundWorker();
            this.timer_主執行序監控 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog_LOAD = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog_SAVE = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorker_畫面更新 = new System.ComponentModel.BackgroundWorker();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.backgroundWorker_Online讀取 = new System.ComponentModel.BackgroundWorker();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.backgroundWorker_計時器 = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkBox_Online_High_Speed = new System.Windows.Forms.CheckBox();
            this.panel_main.SuspendLayout();
            this.panel_程式分頁.SuspendLayout();
            this.contextMenuStrip_TreeView.SuspendLayout();
            this.panel_工具箱.SuspendLayout();
            this.flowLayoutPanel_工具列_檔案讀寫.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.panel_Datagrid_註解列表.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_註解列表)).BeginInit();
            this.contextMenuStrip_註解列表_右鑑選單.SuspendLayout();
            this.panel_LADDER.SuspendLayout();
            this.panel_Listbox_IL指令集.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_LADDER)).BeginInit();
            this.contextMenuStrip_Ladder_右鍵選單.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.AutoScroll = true;
            this.panel_main.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_main.Controls.Add(this.panel_程式分頁);
            this.panel_main.Controls.Add(this.panel_工具箱);
            this.panel_main.Controls.Add(this.panel_Datagrid_註解列表);
            this.panel_main.Controls.Add(this.panel_LADDER);
            this.panel_main.Controls.Add(this.menuStrip);
            this.panel_main.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(0, 0);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(1642, 1000);
            this.panel_main.TabIndex = 0;
            // 
            // panel_程式分頁
            // 
            this.panel_程式分頁.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_程式分頁.Controls.Add(this.treeView_程式分頁);
            this.panel_程式分頁.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_程式分頁.Location = new System.Drawing.Point(0, 68);
            this.panel_程式分頁.Name = "panel_程式分頁";
            this.panel_程式分頁.Size = new System.Drawing.Size(220, 928);
            this.panel_程式分頁.TabIndex = 17;
            // 
            // treeView_程式分頁
            // 
            this.treeView_程式分頁.ContextMenuStrip = this.contextMenuStrip_TreeView;
            this.treeView_程式分頁.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.treeView_程式分頁.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_程式分頁.Font = new System.Drawing.Font("新細明體", 10F);
            this.treeView_程式分頁.Location = new System.Drawing.Point(0, 0);
            this.treeView_程式分頁.Name = "treeView_程式分頁";
            this.treeView_程式分頁.Size = new System.Drawing.Size(216, 924);
            this.treeView_程式分頁.TabIndex = 0;
            this.treeView_程式分頁.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_程式分頁_NodeMouseDoubleClick);
            this.treeView_程式分頁.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_程式分頁_MouseDown);
            this.treeView_程式分頁.MouseLeave += new System.EventHandler(this.treeView_程式分頁_MouseLeave);
            this.treeView_程式分頁.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeView_程式分頁_MouseMove);
            this.treeView_程式分頁.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView_程式分頁_MouseUp);
            // 
            // contextMenuStrip_TreeView
            // 
            this.contextMenuStrip_TreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.調整大小ToolStripMenuItem,
            this.取消ToolStripMenuItem1});
            this.contextMenuStrip_TreeView.Name = "contextMenuStrip1";
            this.contextMenuStrip_TreeView.Size = new System.Drawing.Size(125, 48);
            // 
            // 調整大小ToolStripMenuItem
            // 
            this.調整大小ToolStripMenuItem.Name = "調整大小ToolStripMenuItem";
            this.調整大小ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.調整大小ToolStripMenuItem.Text = "調整大小";
            this.調整大小ToolStripMenuItem.Click += new System.EventHandler(this.調整大小ToolStripMenuItem_Click);
            // 
            // 取消ToolStripMenuItem1
            // 
            this.取消ToolStripMenuItem1.Name = "取消ToolStripMenuItem1";
            this.取消ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.取消ToolStripMenuItem1.Text = "取消";
            this.取消ToolStripMenuItem1.Click += new System.EventHandler(this.取消ToolStripMenuItem1_Click);
            // 
            // panel_工具箱
            // 
            this.panel_工具箱.Controls.Add(this.checkBox_Online_High_Speed);
            this.panel_工具箱.Controls.Add(this.flowLayoutPanel_工具列_檔案讀寫);
            this.panel_工具箱.Controls.Add(this.flowLayoutPanel1);
            this.panel_工具箱.Controls.Add(this.flowLayoutPanel2);
            this.panel_工具箱.Controls.Add(this.flowLayoutPanel3);
            this.panel_工具箱.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_工具箱.Location = new System.Drawing.Point(0, 24);
            this.panel_工具箱.Name = "panel_工具箱";
            this.panel_工具箱.Size = new System.Drawing.Size(1238, 44);
            this.panel_工具箱.TabIndex = 9;
            // 
            // flowLayoutPanel_工具列_檔案讀寫
            // 
            this.flowLayoutPanel_工具列_檔案讀寫.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel_工具列_檔案讀寫.Controls.Add(this.exButton_開新專案);
            this.flowLayoutPanel_工具列_檔案讀寫.Controls.Add(this.exButton_讀取檔案);
            this.flowLayoutPanel_工具列_檔案讀寫.Controls.Add(this.exButton_儲存檔案);
            this.flowLayoutPanel_工具列_檔案讀寫.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel_工具列_檔案讀寫.Name = "flowLayoutPanel_工具列_檔案讀寫";
            this.flowLayoutPanel_工具列_檔案讀寫.Size = new System.Drawing.Size(110, 40);
            this.flowLayoutPanel_工具列_檔案讀寫.TabIndex = 11;
            // 
            // exButton_開新專案
            // 
            this.exButton_開新專案.Location = new System.Drawing.Point(3, 3);
            this.exButton_開新專案.Name = "exButton_開新專案";
            this.exButton_開新專案.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_開新專案.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_開新專案.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_開新專案.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_開新專案.Size = new System.Drawing.Size(30, 30);
            this.exButton_開新專案.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_開新專案.TabIndex = 0;
            this.exButton_開新專案.字型鎖住 = false;
            this.exButton_開新專案.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_開新專案.文字鎖住 = false;
            this.exButton_開新專案.狀態OFF圖片 = global::LadderUI.Properties.Resources.開新檔案;
            this.exButton_開新專案.狀態ON圖片 = global::LadderUI.Properties.Resources.開新檔案1;
            this.exButton_開新專案.讀寫鎖住 = true;
            this.exButton_開新專案.音效 = false;
            this.exButton_開新專案.顯示狀態 = false;
            // 
            // exButton_讀取檔案
            // 
            this.exButton_讀取檔案.Location = new System.Drawing.Point(39, 3);
            this.exButton_讀取檔案.Name = "exButton_讀取檔案";
            this.exButton_讀取檔案.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_讀取檔案.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_讀取檔案.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_讀取檔案.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_讀取檔案.Size = new System.Drawing.Size(30, 30);
            this.exButton_讀取檔案.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_讀取檔案.TabIndex = 1;
            this.exButton_讀取檔案.字型鎖住 = false;
            this.exButton_讀取檔案.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_讀取檔案.文字鎖住 = false;
            this.exButton_讀取檔案.狀態OFF圖片 = global::LadderUI.Properties.Resources.開啟舊檔;
            this.exButton_讀取檔案.狀態ON圖片 = global::LadderUI.Properties.Resources.開啟舊檔1;
            this.exButton_讀取檔案.讀寫鎖住 = true;
            this.exButton_讀取檔案.音效 = false;
            this.exButton_讀取檔案.顯示狀態 = false;
            // 
            // exButton_儲存檔案
            // 
            this.exButton_儲存檔案.Location = new System.Drawing.Point(75, 3);
            this.exButton_儲存檔案.Name = "exButton_儲存檔案";
            this.exButton_儲存檔案.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_儲存檔案.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_儲存檔案.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_儲存檔案.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_儲存檔案.Size = new System.Drawing.Size(30, 30);
            this.exButton_儲存檔案.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_儲存檔案.TabIndex = 2;
            this.exButton_儲存檔案.字型鎖住 = false;
            this.exButton_儲存檔案.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_儲存檔案.文字鎖住 = false;
            this.exButton_儲存檔案.狀態OFF圖片 = global::LadderUI.Properties.Resources.儲存檔案;
            this.exButton_儲存檔案.狀態ON圖片 = global::LadderUI.Properties.Resources.儲存檔案1;
            this.exButton_儲存檔案.讀寫鎖住 = true;
            this.exButton_儲存檔案.音效 = false;
            this.exButton_儲存檔案.顯示狀態 = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.exButton_剪下);
            this.flowLayoutPanel1.Controls.Add(this.exButton_複製);
            this.flowLayoutPanel1.Controls.Add(this.exButton_貼上);
            this.flowLayoutPanel1.Controls.Add(this.exButton_上一步);
            this.flowLayoutPanel1.Controls.Add(this.exButton_復原回未修正);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(115, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(185, 40);
            this.flowLayoutPanel1.TabIndex = 12;
            // 
            // exButton_剪下
            // 
            this.exButton_剪下.Location = new System.Drawing.Point(3, 3);
            this.exButton_剪下.Name = "exButton_剪下";
            this.exButton_剪下.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_剪下.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_剪下.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_剪下.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_剪下.Size = new System.Drawing.Size(30, 30);
            this.exButton_剪下.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_剪下.TabIndex = 0;
            this.exButton_剪下.字型鎖住 = false;
            this.exButton_剪下.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_剪下.文字鎖住 = false;
            this.exButton_剪下.狀態OFF圖片 = global::LadderUI.Properties.Resources.剪下;
            this.exButton_剪下.狀態ON圖片 = global::LadderUI.Properties.Resources.剪下1;
            this.exButton_剪下.讀寫鎖住 = true;
            this.exButton_剪下.音效 = false;
            this.exButton_剪下.顯示狀態 = false;
            // 
            // exButton_複製
            // 
            this.exButton_複製.Location = new System.Drawing.Point(39, 3);
            this.exButton_複製.Name = "exButton_複製";
            this.exButton_複製.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_複製.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_複製.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_複製.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_複製.Size = new System.Drawing.Size(30, 30);
            this.exButton_複製.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_複製.TabIndex = 1;
            this.exButton_複製.字型鎖住 = false;
            this.exButton_複製.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_複製.文字鎖住 = false;
            this.exButton_複製.狀態OFF圖片 = global::LadderUI.Properties.Resources.複製;
            this.exButton_複製.狀態ON圖片 = global::LadderUI.Properties.Resources.複製1;
            this.exButton_複製.讀寫鎖住 = true;
            this.exButton_複製.音效 = false;
            this.exButton_複製.顯示狀態 = false;
            // 
            // exButton_貼上
            // 
            this.exButton_貼上.Location = new System.Drawing.Point(75, 3);
            this.exButton_貼上.Name = "exButton_貼上";
            this.exButton_貼上.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_貼上.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_貼上.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_貼上.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_貼上.Size = new System.Drawing.Size(30, 30);
            this.exButton_貼上.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_貼上.TabIndex = 2;
            this.exButton_貼上.字型鎖住 = false;
            this.exButton_貼上.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_貼上.文字鎖住 = false;
            this.exButton_貼上.狀態OFF圖片 = global::LadderUI.Properties.Resources.貼上;
            this.exButton_貼上.狀態ON圖片 = global::LadderUI.Properties.Resources.貼上1;
            this.exButton_貼上.讀寫鎖住 = true;
            this.exButton_貼上.音效 = false;
            this.exButton_貼上.顯示狀態 = false;
            // 
            // exButton_上一步
            // 
            this.exButton_上一步.Location = new System.Drawing.Point(111, 3);
            this.exButton_上一步.Name = "exButton_上一步";
            this.exButton_上一步.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_上一步.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_上一步.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_上一步.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_上一步.Size = new System.Drawing.Size(30, 30);
            this.exButton_上一步.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_上一步.TabIndex = 3;
            this.exButton_上一步.字型鎖住 = false;
            this.exButton_上一步.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_上一步.文字鎖住 = false;
            this.exButton_上一步.狀態OFF圖片 = global::LadderUI.Properties.Resources.復原;
            this.exButton_上一步.狀態ON圖片 = global::LadderUI.Properties.Resources.復原1;
            this.exButton_上一步.讀寫鎖住 = true;
            this.exButton_上一步.音效 = false;
            this.exButton_上一步.顯示狀態 = false;
            // 
            // exButton_復原回未修正
            // 
            this.exButton_復原回未修正.Location = new System.Drawing.Point(147, 3);
            this.exButton_復原回未修正.Name = "exButton_復原回未修正";
            this.exButton_復原回未修正.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_復原回未修正.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_復原回未修正.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_復原回未修正.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_復原回未修正.Size = new System.Drawing.Size(30, 30);
            this.exButton_復原回未修正.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_復原回未修正.TabIndex = 4;
            this.exButton_復原回未修正.字型鎖住 = false;
            this.exButton_復原回未修正.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_復原回未修正.文字鎖住 = false;
            this.exButton_復原回未修正.狀態OFF圖片 = global::LadderUI.Properties.Resources.復原_未編譯;
            this.exButton_復原回未修正.狀態ON圖片 = global::LadderUI.Properties.Resources.復原1_未編譯;
            this.exButton_復原回未修正.讀寫鎖住 = true;
            this.exButton_復原回未修正.音效 = false;
            this.exButton_復原回未修正.顯示狀態 = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Controls.Add(this.exButton_寫入A接點);
            this.flowLayoutPanel2.Controls.Add(this.exButton_寫入B接點);
            this.flowLayoutPanel2.Controls.Add(this.exButton_畫橫線);
            this.flowLayoutPanel2.Controls.Add(this.exButton_畫豎線);
            this.flowLayoutPanel2.Controls.Add(this.exButton_刪除橫線);
            this.flowLayoutPanel2.Controls.Add(this.exButton_刪除豎線);
            this.flowLayoutPanel2.Controls.Add(this.exButton_窗選模式切換);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(303, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(254, 40);
            this.flowLayoutPanel2.TabIndex = 13;
            // 
            // exButton_寫入A接點
            // 
            this.exButton_寫入A接點.Location = new System.Drawing.Point(3, 3);
            this.exButton_寫入A接點.Name = "exButton_寫入A接點";
            this.exButton_寫入A接點.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_寫入A接點.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_寫入A接點.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_寫入A接點.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_寫入A接點.Size = new System.Drawing.Size(30, 30);
            this.exButton_寫入A接點.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_寫入A接點.TabIndex = 0;
            this.exButton_寫入A接點.字型鎖住 = false;
            this.exButton_寫入A接點.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_寫入A接點.文字鎖住 = false;
            this.exButton_寫入A接點.狀態OFF圖片 = global::LadderUI.Properties.Resources.OPen_contact;
            this.exButton_寫入A接點.狀態ON圖片 = global::LadderUI.Properties.Resources.OPen_contact1;
            this.exButton_寫入A接點.讀寫鎖住 = true;
            this.exButton_寫入A接點.音效 = false;
            this.exButton_寫入A接點.顯示狀態 = false;
            // 
            // exButton_寫入B接點
            // 
            this.exButton_寫入B接點.Location = new System.Drawing.Point(39, 3);
            this.exButton_寫入B接點.Name = "exButton_寫入B接點";
            this.exButton_寫入B接點.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_寫入B接點.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_寫入B接點.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_寫入B接點.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_寫入B接點.Size = new System.Drawing.Size(30, 30);
            this.exButton_寫入B接點.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_寫入B接點.TabIndex = 1;
            this.exButton_寫入B接點.字型鎖住 = false;
            this.exButton_寫入B接點.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_寫入B接點.文字鎖住 = false;
            this.exButton_寫入B接點.狀態OFF圖片 = global::LadderUI.Properties.Resources.Close_contact;
            this.exButton_寫入B接點.狀態ON圖片 = global::LadderUI.Properties.Resources.Close_contact1;
            this.exButton_寫入B接點.讀寫鎖住 = true;
            this.exButton_寫入B接點.音效 = false;
            this.exButton_寫入B接點.顯示狀態 = false;
            // 
            // exButton_畫橫線
            // 
            this.exButton_畫橫線.Location = new System.Drawing.Point(75, 3);
            this.exButton_畫橫線.Name = "exButton_畫橫線";
            this.exButton_畫橫線.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_畫橫線.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_畫橫線.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_畫橫線.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_畫橫線.Size = new System.Drawing.Size(30, 30);
            this.exButton_畫橫線.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_畫橫線.TabIndex = 2;
            this.exButton_畫橫線.字型鎖住 = false;
            this.exButton_畫橫線.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_畫橫線.文字鎖住 = false;
            this.exButton_畫橫線.狀態OFF圖片 = global::LadderUI.Properties.Resources.Draw_horizontal_line;
            this.exButton_畫橫線.狀態ON圖片 = global::LadderUI.Properties.Resources.Draw_horizontal_line1;
            this.exButton_畫橫線.讀寫鎖住 = true;
            this.exButton_畫橫線.音效 = false;
            this.exButton_畫橫線.顯示狀態 = false;
            // 
            // exButton_畫豎線
            // 
            this.exButton_畫豎線.Location = new System.Drawing.Point(111, 3);
            this.exButton_畫豎線.Name = "exButton_畫豎線";
            this.exButton_畫豎線.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_畫豎線.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_畫豎線.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_畫豎線.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_畫豎線.Size = new System.Drawing.Size(30, 30);
            this.exButton_畫豎線.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_畫豎線.TabIndex = 3;
            this.exButton_畫豎線.字型鎖住 = false;
            this.exButton_畫豎線.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_畫豎線.文字鎖住 = false;
            this.exButton_畫豎線.狀態OFF圖片 = global::LadderUI.Properties.Resources.Draw_vartical_line;
            this.exButton_畫豎線.狀態ON圖片 = global::LadderUI.Properties.Resources.Draw_vartical_line1;
            this.exButton_畫豎線.讀寫鎖住 = true;
            this.exButton_畫豎線.音效 = false;
            this.exButton_畫豎線.顯示狀態 = false;
            // 
            // exButton_刪除橫線
            // 
            this.exButton_刪除橫線.Location = new System.Drawing.Point(147, 3);
            this.exButton_刪除橫線.Name = "exButton_刪除橫線";
            this.exButton_刪除橫線.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_刪除橫線.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_刪除橫線.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_刪除橫線.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_刪除橫線.Size = new System.Drawing.Size(30, 30);
            this.exButton_刪除橫線.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_刪除橫線.TabIndex = 4;
            this.exButton_刪除橫線.字型鎖住 = false;
            this.exButton_刪除橫線.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_刪除橫線.文字鎖住 = false;
            this.exButton_刪除橫線.狀態OFF圖片 = global::LadderUI.Properties.Resources.Delete_horizontal_line;
            this.exButton_刪除橫線.狀態ON圖片 = global::LadderUI.Properties.Resources.Delete_horizontal_line1;
            this.exButton_刪除橫線.讀寫鎖住 = true;
            this.exButton_刪除橫線.音效 = false;
            this.exButton_刪除橫線.顯示狀態 = false;
            // 
            // exButton_刪除豎線
            // 
            this.exButton_刪除豎線.Location = new System.Drawing.Point(183, 3);
            this.exButton_刪除豎線.Name = "exButton_刪除豎線";
            this.exButton_刪除豎線.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_刪除豎線.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_刪除豎線.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_刪除豎線.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_刪除豎線.Size = new System.Drawing.Size(30, 30);
            this.exButton_刪除豎線.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_刪除豎線.TabIndex = 5;
            this.exButton_刪除豎線.字型鎖住 = false;
            this.exButton_刪除豎線.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_刪除豎線.文字鎖住 = false;
            this.exButton_刪除豎線.狀態OFF圖片 = global::LadderUI.Properties.Resources.Delete_vartical_line;
            this.exButton_刪除豎線.狀態ON圖片 = global::LadderUI.Properties.Resources.Delete_vartical_line1;
            this.exButton_刪除豎線.讀寫鎖住 = true;
            this.exButton_刪除豎線.音效 = false;
            this.exButton_刪除豎線.顯示狀態 = false;
            // 
            // exButton_窗選模式切換
            // 
            this.exButton_窗選模式切換.Location = new System.Drawing.Point(219, 3);
            this.exButton_窗選模式切換.Name = "exButton_窗選模式切換";
            this.exButton_窗選模式切換.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_窗選模式切換.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_窗選模式切換.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_窗選模式切換.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_窗選模式切換.Size = new System.Drawing.Size(30, 30);
            this.exButton_窗選模式切換.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_窗選模式切換.TabIndex = 6;
            this.exButton_窗選模式切換.字型鎖住 = false;
            this.exButton_窗選模式切換.按鈕型態 = MyUI.ExButton.StatusEnum.交替型;
            this.exButton_窗選模式切換.文字鎖住 = false;
            this.exButton_窗選模式切換.狀態OFF圖片 = global::LadderUI.Properties.Resources.Free_draw_line;
            this.exButton_窗選模式切換.狀態ON圖片 = global::LadderUI.Properties.Resources.Free_draw_line1;
            this.exButton_窗選模式切換.讀寫鎖住 = true;
            this.exButton_窗選模式切換.音效 = false;
            this.exButton_窗選模式切換.顯示狀態 = false;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel3.Controls.Add(this.exButton_語法切換);
            this.flowLayoutPanel3.Controls.Add(this.exButton_程式_註解模式選擇);
            this.flowLayoutPanel3.Controls.Add(this.exButton15);
            this.flowLayoutPanel3.Controls.Add(this.exButton_Online);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(559, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(147, 40);
            this.flowLayoutPanel3.TabIndex = 14;
            // 
            // exButton_語法切換
            // 
            this.exButton_語法切換.Location = new System.Drawing.Point(3, 3);
            this.exButton_語法切換.Name = "exButton_語法切換";
            this.exButton_語法切換.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_語法切換.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_語法切換.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_語法切換.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_語法切換.Size = new System.Drawing.Size(30, 30);
            this.exButton_語法切換.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_語法切換.TabIndex = 1;
            this.exButton_語法切換.字型鎖住 = false;
            this.exButton_語法切換.按鈕型態 = MyUI.ExButton.StatusEnum.交替型;
            this.exButton_語法切換.文字鎖住 = false;
            this.exButton_語法切換.狀態OFF圖片 = global::LadderUI.Properties.Resources.IL_LD切換1;
            this.exButton_語法切換.狀態ON圖片 = global::LadderUI.Properties.Resources.IL_LD切換11;
            this.exButton_語法切換.讀寫鎖住 = true;
            this.exButton_語法切換.音效 = false;
            this.exButton_語法切換.顯示狀態 = false;
            // 
            // exButton_程式_註解模式選擇
            // 
            this.exButton_程式_註解模式選擇.Location = new System.Drawing.Point(39, 3);
            this.exButton_程式_註解模式選擇.Name = "exButton_程式_註解模式選擇";
            this.exButton_程式_註解模式選擇.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_程式_註解模式選擇.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_程式_註解模式選擇.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_程式_註解模式選擇.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_程式_註解模式選擇.Size = new System.Drawing.Size(30, 30);
            this.exButton_程式_註解模式選擇.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_程式_註解模式選擇.TabIndex = 0;
            this.exButton_程式_註解模式選擇.字型鎖住 = false;
            this.exButton_程式_註解模式選擇.按鈕型態 = MyUI.ExButton.StatusEnum.交替型;
            this.exButton_程式_註解模式選擇.文字鎖住 = false;
            this.exButton_程式_註解模式選擇.狀態OFF圖片 = global::LadderUI.Properties.Resources.comment;
            this.exButton_程式_註解模式選擇.狀態ON圖片 = global::LadderUI.Properties.Resources.comment1;
            this.exButton_程式_註解模式選擇.讀寫鎖住 = true;
            this.exButton_程式_註解模式選擇.音效 = false;
            this.exButton_程式_註解模式選擇.顯示狀態 = false;
            // 
            // exButton15
            // 
            this.exButton15.Location = new System.Drawing.Point(75, 3);
            this.exButton15.Name = "exButton15";
            this.exButton15.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton15.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton15.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton15.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton15.Size = new System.Drawing.Size(30, 30);
            this.exButton15.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton15.TabIndex = 2;
            this.exButton15.字型鎖住 = false;
            this.exButton15.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton15.文字鎖住 = false;
            this.exButton15.狀態OFF圖片 = global::LadderUI.Properties.Resources.Device_test;
            this.exButton15.狀態ON圖片 = global::LadderUI.Properties.Resources.Device_test1;
            this.exButton15.讀寫鎖住 = true;
            this.exButton15.音效 = false;
            this.exButton15.顯示狀態 = false;
            // 
            // exButton_Online
            // 
            this.exButton_Online.Location = new System.Drawing.Point(111, 3);
            this.exButton_Online.Name = "exButton_Online";
            this.exButton_Online.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Online.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_Online.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_Online.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_Online.Size = new System.Drawing.Size(30, 30);
            this.exButton_Online.Style = MyUI.ExButton.StyleEnum.自定義;
            this.exButton_Online.TabIndex = 3;
            this.exButton_Online.字型鎖住 = false;
            this.exButton_Online.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_Online.文字鎖住 = false;
            this.exButton_Online.狀態OFF圖片 = global::LadderUI.Properties.Resources.monitorwrite_mode;
            this.exButton_Online.狀態ON圖片 = global::LadderUI.Properties.Resources.monitorwrite_mode1;
            this.exButton_Online.讀寫鎖住 = false;
            this.exButton_Online.音效 = false;
            this.exButton_Online.顯示狀態 = false;
            this.exButton_Online.btnClick += new System.EventHandler(this.exButton_Online_btnClick);
            // 
            // panel_Datagrid_註解列表
            // 
            this.panel_Datagrid_註解列表.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Datagrid_註解列表.Controls.Add(this.dataGridView_註解列表);
            this.panel_Datagrid_註解列表.Controls.Add(this.exButton_註解查詢);
            this.panel_Datagrid_註解列表.Controls.Add(this.label1);
            this.panel_Datagrid_註解列表.Controls.Add(this.textBox_註解查詢_Device_name);
            this.panel_Datagrid_註解列表.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_Datagrid_註解列表.Location = new System.Drawing.Point(1238, 24);
            this.panel_Datagrid_註解列表.Name = "panel_Datagrid_註解列表";
            this.panel_Datagrid_註解列表.Size = new System.Drawing.Size(400, 972);
            this.panel_Datagrid_註解列表.TabIndex = 10;
            this.panel_Datagrid_註解列表.TabStop = true;
            this.panel_Datagrid_註解列表.Click += new System.EventHandler(this.panel_Datagrid_註解列表_Click);
            // 
            // dataGridView_註解列表
            // 
            this.dataGridView_註解列表.AllowUserToAddRows = false;
            this.dataGridView_註解列表.AllowUserToDeleteRows = false;
            this.dataGridView_註解列表.AllowUserToResizeColumns = false;
            this.dataGridView_註解列表.AllowUserToResizeRows = false;
            this.dataGridView_註解列表.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView_註解列表.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView_註解列表.ColumnHeadersHeight = 20;
            this.dataGridView_註解列表.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView_註解列表.ContextMenuStrip = this.contextMenuStrip_註解列表_右鑑選單;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_註解列表.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_註解列表.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridView_註解列表.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView_註解列表.Location = new System.Drawing.Point(3, 54);
            this.dataGridView_註解列表.Name = "dataGridView_註解列表";
            this.dataGridView_註解列表.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_註解列表.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_註解列表.RowHeadersVisible = false;
            this.dataGridView_註解列表.RowHeadersWidth = 20;
            this.dataGridView_註解列表.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridView_註解列表.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_註解列表.RowTemplate.Height = 20;
            this.dataGridView_註解列表.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_註解列表.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_註解列表.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_註解列表.ShowCellToolTips = false;
            this.dataGridView_註解列表.Size = new System.Drawing.Size(395, 914);
            this.dataGridView_註解列表.TabIndex = 4;
            this.dataGridView_註解列表.TabStop = false;
            this.dataGridView_註解列表.Enter += new System.EventHandler(this.dataGridView_註解列表_Enter);
            this.dataGridView_註解列表.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dataGridView_註解列表_PreviewKeyDown);
            // 
            // contextMenuStrip_註解列表_右鑑選單
            // 
            this.contextMenuStrip_註解列表_右鑑選單.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.D_剪下,
            this.D_複製,
            this.D_貼上,
            this.D_刪除,
            this.D_選取全部});
            this.contextMenuStrip_註解列表_右鑑選單.Name = "contextMenuStrip_右鍵選單";
            this.contextMenuStrip_註解列表_右鑑選單.Size = new System.Drawing.Size(198, 114);
            this.contextMenuStrip_註解列表_右鑑選單.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_註解列表_右鑑選單_ItemClicked);
            // 
            // D_剪下
            // 
            this.D_剪下.Name = "D_剪下";
            this.D_剪下.Size = new System.Drawing.Size(197, 22);
            this.D_剪下.Text = "剪下                    Ctrl+X";
            // 
            // D_複製
            // 
            this.D_複製.Name = "D_複製";
            this.D_複製.Size = new System.Drawing.Size(197, 22);
            this.D_複製.Text = "複製                    Ctrl+C";
            // 
            // D_貼上
            // 
            this.D_貼上.Name = "D_貼上";
            this.D_貼上.Size = new System.Drawing.Size(197, 22);
            this.D_貼上.Text = "貼上                    Ctrl+V";
            // 
            // D_刪除
            // 
            this.D_刪除.Name = "D_刪除";
            this.D_刪除.Size = new System.Drawing.Size(197, 22);
            this.D_刪除.Text = "刪除                    Delete";
            // 
            // D_選取全部
            // 
            this.D_選取全部.Name = "D_選取全部";
            this.D_選取全部.Size = new System.Drawing.Size(197, 22);
            this.D_選取全部.Text = "選取全部            Ctrl+A";
            // 
            // exButton_註解查詢
            // 
            this.exButton_註解查詢.Location = new System.Drawing.Point(200, 11);
            this.exButton_註解查詢.Name = "exButton_註解查詢";
            this.exButton_註解查詢.OFF_文字內容 = "Display";
            this.exButton_註解查詢.OFF_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_註解查詢.OFF_文字顏色 = System.Drawing.Color.Black;
            this.exButton_註解查詢.ON_文字內容 = "Display";
            this.exButton_註解查詢.ON_文字字體 = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exButton_註解查詢.ON_文字顏色 = System.Drawing.Color.White;
            this.exButton_註解查詢.Size = new System.Drawing.Size(84, 28);
            this.exButton_註解查詢.Style = MyUI.ExButton.StyleEnum.經典;
            this.exButton_註解查詢.TabIndex = 3;
            this.exButton_註解查詢.TabStop = false;
            this.exButton_註解查詢.字型鎖住 = true;
            this.exButton_註解查詢.按鈕型態 = MyUI.ExButton.StatusEnum.保持型;
            this.exButton_註解查詢.文字鎖住 = false;
            this.exButton_註解查詢.狀態OFF圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_註解查詢.狀態OFF圖片")));
            this.exButton_註解查詢.狀態ON圖片 = ((System.Drawing.Image)(resources.GetObject("exButton_註解查詢.狀態ON圖片")));
            this.exButton_註解查詢.讀寫鎖住 = true;
            this.exButton_註解查詢.音效 = false;
            this.exButton_註解查詢.顯示狀態 = false;
            this.exButton_註解查詢.btnClick += new System.EventHandler(this.exButton_註解查詢_btnClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F);
            this.label1.Location = new System.Drawing.Point(11, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Device name";
            // 
            // textBox_註解查詢_Device_name
            // 
            this.textBox_註解查詢_Device_name.Location = new System.Drawing.Point(105, 14);
            this.textBox_註解查詢_Device_name.Name = "textBox_註解查詢_Device_name";
            this.textBox_註解查詢_Device_name.Size = new System.Drawing.Size(89, 22);
            this.textBox_註解查詢_Device_name.TabIndex = 1;
            this.textBox_註解查詢_Device_name.TabStop = false;
            this.textBox_註解查詢_Device_name.Text = "X0";
            this.textBox_註解查詢_Device_name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_註解查詢_Device_name_KeyPress);
            // 
            // panel_LADDER
            // 
            this.panel_LADDER.AllowDrop = true;
            this.panel_LADDER.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel_LADDER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_LADDER.Controls.Add(this.panel_Listbox_IL指令集);
            this.panel_LADDER.Controls.Add(this.vScrollBar_picture_滾動條);
            this.panel_LADDER.Controls.Add(this.pictureBox_LADDER);
            this.panel_LADDER.Location = new System.Drawing.Point(217, 73);
            this.panel_LADDER.Name = "panel_LADDER";
            this.panel_LADDER.Size = new System.Drawing.Size(990, 916);
            this.panel_LADDER.TabIndex = 0;
            this.panel_LADDER.Leave += new System.EventHandler(this.panel_LADDER_Leave);
            // 
            // panel_Listbox_IL指令集
            // 
            this.panel_Listbox_IL指令集.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_Listbox_IL指令集.Controls.Add(this.listBox_IL指令集);
            this.panel_Listbox_IL指令集.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel_Listbox_IL指令集.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Listbox_IL指令集.Location = new System.Drawing.Point(0, 0);
            this.panel_Listbox_IL指令集.Name = "panel_Listbox_IL指令集";
            this.panel_Listbox_IL指令集.Size = new System.Drawing.Size(963, 914);
            this.panel_Listbox_IL指令集.TabIndex = 10;
            this.panel_Listbox_IL指令集.Visible = false;
            // 
            // listBox_IL指令集
            // 
            this.listBox_IL指令集.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_IL指令集.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_IL指令集.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_IL指令集.FormattingEnabled = true;
            this.listBox_IL指令集.ItemHeight = 18;
            this.listBox_IL指令集.Location = new System.Drawing.Point(0, 0);
            this.listBox_IL指令集.Name = "listBox_IL指令集";
            this.listBox_IL指令集.Size = new System.Drawing.Size(959, 910);
            this.listBox_IL指令集.TabIndex = 8;
            this.listBox_IL指令集.TabStop = false;
            // 
            // vScrollBar_picture_滾動條
            // 
            this.vScrollBar_picture_滾動條.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar_picture_滾動條.LargeChange = 1;
            this.vScrollBar_picture_滾動條.Location = new System.Drawing.Point(963, 0);
            this.vScrollBar_picture_滾動條.Name = "vScrollBar_picture_滾動條";
            this.vScrollBar_picture_滾動條.Size = new System.Drawing.Size(25, 914);
            this.vScrollBar_picture_滾動條.TabIndex = 7;
            this.vScrollBar_picture_滾動條.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_picture_滾動條_Scroll);
            this.vScrollBar_picture_滾動條.MouseEnter += new System.EventHandler(this.vScrollBar_picture_滾動條_MouseEnter);
            this.vScrollBar_picture_滾動條.MouseLeave += new System.EventHandler(this.vScrollBar_picture_滾動條_MouseLeave);
            // 
            // pictureBox_LADDER
            // 
            this.pictureBox_LADDER.BackColor = System.Drawing.Color.White;
            this.pictureBox_LADDER.ContextMenuStrip = this.contextMenuStrip_Ladder_右鍵選單;
            this.pictureBox_LADDER.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBox_LADDER.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_LADDER.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_LADDER.Name = "pictureBox_LADDER";
            this.pictureBox_LADDER.Size = new System.Drawing.Size(988, 914);
            this.pictureBox_LADDER.TabIndex = 0;
            this.pictureBox_LADDER.TabStop = false;
            this.pictureBox_LADDER.DoubleClick += new System.EventHandler(this.pictureBox_LADDER_DoubleClick);
            this.pictureBox_LADDER.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_LADDER_MouseDown);
            this.pictureBox_LADDER.MouseHover += new System.EventHandler(this.pictureBox_LADDER_MouseHover);
            this.pictureBox_LADDER.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_LADDER_MouseMove);
            this.pictureBox_LADDER.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_LADDER_MouseUp);
            // 
            // contextMenuStrip_Ladder_右鍵選單
            // 
            this.contextMenuStrip_Ladder_右鍵選單.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.編譯,
            this.復原回未編譯,
            this.toolStripSeparator3,
            this.復原,
            this.toolStripSeparator1,
            this.剪下,
            this.複製,
            this.貼上,
            this.toolStripSeparator2,
            this.插入一列,
            this.刪除一列,
            this.插入一行,
            this.toolStripSeparator4,
            this.繪製橫線,
            this.刪除橫線,
            this.繪製豎線,
            this.刪除豎線});
            this.contextMenuStrip_Ladder_右鍵選單.Name = "contextMenuStrip_右鍵選單";
            this.contextMenuStrip_Ladder_右鍵選單.Size = new System.Drawing.Size(215, 314);
            this.contextMenuStrip_Ladder_右鍵選單.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_右鍵選單_ItemClicked);
            // 
            // 編譯
            // 
            this.編譯.Name = "編譯";
            this.編譯.Size = new System.Drawing.Size(214, 22);
            this.編譯.Text = "編譯                    F4";
            // 
            // 復原回未編譯
            // 
            this.復原回未編譯.Name = "復原回未編譯";
            this.復原回未編譯.Size = new System.Drawing.Size(214, 22);
            this.復原回未編譯.Text = "復原回未編譯";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(211, 6);
            // 
            // 復原
            // 
            this.復原.Name = "復原";
            this.復原.Size = new System.Drawing.Size(214, 22);
            this.復原.Text = "復原                    Ctrl+Z";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(211, 6);
            // 
            // 剪下
            // 
            this.剪下.Name = "剪下";
            this.剪下.Size = new System.Drawing.Size(214, 22);
            this.剪下.Text = "剪下                    Ctrl+X";
            // 
            // 複製
            // 
            this.複製.Name = "複製";
            this.複製.Size = new System.Drawing.Size(214, 22);
            this.複製.Text = "複製                    Ctrl+C";
            // 
            // 貼上
            // 
            this.貼上.Name = "貼上";
            this.貼上.Size = new System.Drawing.Size(214, 22);
            this.貼上.Text = "貼上                    Ctrl+V";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(211, 6);
            // 
            // 插入一列
            // 
            this.插入一列.Name = "插入一列";
            this.插入一列.Size = new System.Drawing.Size(214, 22);
            this.插入一列.Text = "插入一列            Shift+Ins";
            // 
            // 刪除一列
            // 
            this.刪除一列.Name = "刪除一列";
            this.刪除一列.Size = new System.Drawing.Size(214, 22);
            this.刪除一列.Text = "刪除一列            Shift+Del";
            // 
            // 插入一行
            // 
            this.插入一行.Name = "插入一行";
            this.插入一行.Size = new System.Drawing.Size(214, 22);
            this.插入一行.Text = "插入一行            Ctrl+Ins";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(211, 6);
            // 
            // 繪製橫線
            // 
            this.繪製橫線.Name = "繪製橫線";
            this.繪製橫線.Size = new System.Drawing.Size(214, 22);
            this.繪製橫線.Text = "繪製橫線            F9";
            // 
            // 刪除橫線
            // 
            this.刪除橫線.Name = "刪除橫線";
            this.刪除橫線.Size = new System.Drawing.Size(214, 22);
            this.刪除橫線.Text = "刪除橫線            Ctrl+F9";
            // 
            // 繪製豎線
            // 
            this.繪製豎線.Name = "繪製豎線";
            this.繪製豎線.Size = new System.Drawing.Size(214, 22);
            this.繪製豎線.Text = "繪製豎線            Shift+F9";
            // 
            // 刪除豎線
            // 
            this.刪除豎線.Name = "刪除豎線";
            this.刪除豎線.Size = new System.Drawing.Size(214, 22);
            this.刪除豎線.Text = "刪除豎線            Shift+F10";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.專案ToolStripMenuItem,
            this.顯示ToolStripMenuItem,
            this.通訊ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1638, 24);
            this.menuStrip.TabIndex = 16;
            this.menuStrip.Text = "menuStrip";
            // 
            // 專案ToolStripMenuItem
            // 
            this.專案ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新專案ToolStripMenuItem,
            this.讀取ToolStripMenuItem,
            this.儲存ToolStripMenuItem,
            this.關閉ToolStripMenuItem});
            this.專案ToolStripMenuItem.Name = "專案ToolStripMenuItem";
            this.專案ToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.專案ToolStripMenuItem.Text = "專案";
            // 
            // 新專案ToolStripMenuItem
            // 
            this.新專案ToolStripMenuItem.Name = "新專案ToolStripMenuItem";
            this.新專案ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.新專案ToolStripMenuItem.Text = "新專案";
            this.新專案ToolStripMenuItem.Click += new System.EventHandler(this.新專案ToolStripMenuItem_Click);
            // 
            // 讀取ToolStripMenuItem
            // 
            this.讀取ToolStripMenuItem.Name = "讀取ToolStripMenuItem";
            this.讀取ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.讀取ToolStripMenuItem.Text = "讀取";
            this.讀取ToolStripMenuItem.Click += new System.EventHandler(this.讀取ToolStripMenuItem_Click);
            // 
            // 儲存ToolStripMenuItem
            // 
            this.儲存ToolStripMenuItem.Name = "儲存ToolStripMenuItem";
            this.儲存ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.儲存ToolStripMenuItem.Text = "儲存";
            this.儲存ToolStripMenuItem.Click += new System.EventHandler(this.儲存ToolStripMenuItem_Click);
            // 
            // 關閉ToolStripMenuItem
            // 
            this.關閉ToolStripMenuItem.Name = "關閉ToolStripMenuItem";
            this.關閉ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.關閉ToolStripMenuItem.Text = "關閉";
            this.關閉ToolStripMenuItem.Click += new System.EventHandler(this.關閉ToolStripMenuItem_Click);
            // 
            // 顯示ToolStripMenuItem
            // 
            this.顯示ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem顯示目錄,
            this.toolStripSeparator11,
            this.顯示註解列表toolStripMenuItem,
            this.顯示註解列表toolStripSeparator1,
            this.註解顯示ToolStripMenuItem,
            this.註解顏色ToolStripMenuItem,
            this.註解字體ToolStripMenuItem,
            this.註解格式ToolStripMenuItem,
            this.toolStripSeparator10,
            this.oNLINE字體ToolStripMenuItem,
            this.toolStripSeparator9,
            this.toolStripMenuItem視窗字體,
            this.toolStripSeparator12,
            this.顯示_預設值ToolStripMenuItem});
            this.顯示ToolStripMenuItem.Name = "顯示ToolStripMenuItem";
            this.顯示ToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.顯示ToolStripMenuItem.Text = "顯示";
            // 
            // toolStripMenuItem顯示目錄
            // 
            this.toolStripMenuItem顯示目錄.Checked = true;
            this.toolStripMenuItem顯示目錄.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem顯示目錄.Name = "toolStripMenuItem顯示目錄";
            this.toolStripMenuItem顯示目錄.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem顯示目錄.Text = "顯示目錄";
            this.toolStripMenuItem顯示目錄.Click += new System.EventHandler(this.toolStripMenuItem顯示目錄_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(145, 6);
            // 
            // 顯示註解列表toolStripMenuItem
            // 
            this.顯示註解列表toolStripMenuItem.Checked = true;
            this.顯示註解列表toolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.顯示註解列表toolStripMenuItem.Name = "顯示註解列表toolStripMenuItem";
            this.顯示註解列表toolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.顯示註解列表toolStripMenuItem.Text = "顯示註解列表";
            this.顯示註解列表toolStripMenuItem.Click += new System.EventHandler(this.顯示註解列表toolStripMenuItem_Click);
            // 
            // 顯示註解列表toolStripSeparator1
            // 
            this.顯示註解列表toolStripSeparator1.Name = "顯示註解列表toolStripSeparator1";
            this.顯示註解列表toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // 註解顯示ToolStripMenuItem
            // 
            this.註解顯示ToolStripMenuItem.Checked = true;
            this.註解顯示ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.註解顯示ToolStripMenuItem.Name = "註解顯示ToolStripMenuItem";
            this.註解顯示ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.註解顯示ToolStripMenuItem.Text = "顯示註解";
            this.註解顯示ToolStripMenuItem.Click += new System.EventHandler(this.commentToolStripMenuItem_Click);
            // 
            // 註解顏色ToolStripMenuItem
            // 
            this.註解顏色ToolStripMenuItem.Name = "註解顏色ToolStripMenuItem";
            this.註解顏色ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.註解顏色ToolStripMenuItem.Text = "註解顏色";
            this.註解顏色ToolStripMenuItem.Click += new System.EventHandler(this.註解顏色ToolStripMenuItem_Click);
            // 
            // 註解字體ToolStripMenuItem
            // 
            this.註解字體ToolStripMenuItem.Name = "註解字體ToolStripMenuItem";
            this.註解字體ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.註解字體ToolStripMenuItem.Text = "註解字體";
            this.註解字體ToolStripMenuItem.Click += new System.EventHandler(this.註解字體ToolStripMenuItem_Click);
            // 
            // 註解格式ToolStripMenuItem
            // 
            this.註解格式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.字母數ToolStripMenuItem,
            this.toolStripSeparator5,
            this.toolStripTextBox_註解字母數,
            this.toolStripMenuItem_註解字母數_確認,
            this.toolStripSeparator6,
            this.列數ToolStripMenuItem,
            this.toolStripSeparator7,
            this.toolStripTextBox_註解列數,
            this.toolStripSeparator8,
            this.toolStripMenuItem_註解列數_確認});
            this.註解格式ToolStripMenuItem.Name = "註解格式ToolStripMenuItem";
            this.註解格式ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.註解格式ToolStripMenuItem.Text = "註解格式";
            // 
            // 字母數ToolStripMenuItem
            // 
            this.字母數ToolStripMenuItem.Enabled = false;
            this.字母數ToolStripMenuItem.Name = "字母數ToolStripMenuItem";
            this.字母數ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.字母數ToolStripMenuItem.Text = "字母數";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(157, 6);
            // 
            // toolStripTextBox_註解字母數
            // 
            this.toolStripTextBox_註解字母數.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox_註解字母數.Name = "toolStripTextBox_註解字母數";
            this.toolStripTextBox_註解字母數.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox_註解字母數.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBox_註解字母數_KeyPress);
            // 
            // toolStripMenuItem_註解字母數_確認
            // 
            this.toolStripMenuItem_註解字母數_確認.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripMenuItem_註解字母數_確認.Name = "toolStripMenuItem_註解字母數_確認";
            this.toolStripMenuItem_註解字母數_確認.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem_註解字母數_確認.Text = "確認輸入";
            this.toolStripMenuItem_註解字母數_確認.Click += new System.EventHandler(this.toolStripMenuItem_註解字母數_確認_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(157, 6);
            // 
            // 列數ToolStripMenuItem
            // 
            this.列數ToolStripMenuItem.Enabled = false;
            this.列數ToolStripMenuItem.Name = "列數ToolStripMenuItem";
            this.列數ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.列數ToolStripMenuItem.Text = "列數";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(157, 6);
            // 
            // toolStripTextBox_註解列數
            // 
            this.toolStripTextBox_註解列數.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox_註解列數.Name = "toolStripTextBox_註解列數";
            this.toolStripTextBox_註解列數.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox_註解列數.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBox_註解列數_KeyPress);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(157, 6);
            // 
            // toolStripMenuItem_註解列數_確認
            // 
            this.toolStripMenuItem_註解列數_確認.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripMenuItem_註解列數_確認.Name = "toolStripMenuItem_註解列數_確認";
            this.toolStripMenuItem_註解列數_確認.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItem_註解列數_確認.Text = "確認輸入";
            this.toolStripMenuItem_註解列數_確認.Click += new System.EventHandler(this.toolStripMenuItem_註解列數_確認_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(145, 6);
            // 
            // oNLINE字體ToolStripMenuItem
            // 
            this.oNLINE字體ToolStripMenuItem.Name = "oNLINE字體ToolStripMenuItem";
            this.oNLINE字體ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.oNLINE字體ToolStripMenuItem.Text = "ONLINE字體";
            this.oNLINE字體ToolStripMenuItem.Click += new System.EventHandler(this.oNLINE字體ToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(145, 6);
            // 
            // toolStripMenuItem視窗字體
            // 
            this.toolStripMenuItem視窗字體.Name = "toolStripMenuItem視窗字體";
            this.toolStripMenuItem視窗字體.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem視窗字體.Text = "視窗字體";
            this.toolStripMenuItem視窗字體.Click += new System.EventHandler(this.toolStripMenuItem視窗字體_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(145, 6);
            // 
            // 顯示_預設值ToolStripMenuItem
            // 
            this.顯示_預設值ToolStripMenuItem.Name = "顯示_預設值ToolStripMenuItem";
            this.顯示_預設值ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.顯示_預設值ToolStripMenuItem.Text = "預設值";
            this.顯示_預設值ToolStripMenuItem.Click += new System.EventHandler(this.顯示_預設值ToolStripMenuItem_Click);
            // 
            // 通訊ToolStripMenuItem
            // 
            this.通訊ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設定ToolStripMenuItem,
            this.上傳ToolStripMenuItem,
            this.下載ToolStripMenuItem,
            this.程式比較ToolStripMenuItem});
            this.通訊ToolStripMenuItem.Name = "通訊ToolStripMenuItem";
            this.通訊ToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.通訊ToolStripMenuItem.Text = "通訊";
            // 
            // 設定ToolStripMenuItem
            // 
            this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
            this.設定ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.設定ToolStripMenuItem.Text = "設定";
            this.設定ToolStripMenuItem.Click += new System.EventHandler(this.設定ToolStripMenuItem_Click);
            // 
            // 上傳ToolStripMenuItem
            // 
            this.上傳ToolStripMenuItem.Name = "上傳ToolStripMenuItem";
            this.上傳ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.上傳ToolStripMenuItem.Text = "上傳...";
            this.上傳ToolStripMenuItem.Click += new System.EventHandler(this.上傳ToolStripMenuItem_Click);
            // 
            // 下載ToolStripMenuItem
            // 
            this.下載ToolStripMenuItem.Name = "下載ToolStripMenuItem";
            this.下載ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.下載ToolStripMenuItem.Text = "下載...";
            this.下載ToolStripMenuItem.Click += new System.EventHandler(this.下載ToolStripMenuItem_Click);
            // 
            // 程式比較ToolStripMenuItem
            // 
            this.程式比較ToolStripMenuItem.Name = "程式比較ToolStripMenuItem";
            this.程式比較ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.程式比較ToolStripMenuItem.Text = "比較...";
            this.程式比較ToolStripMenuItem.Click += new System.EventHandler(this.程式比較ToolStripMenuItem_Click);
            // 
            // backgroundWorker_LADDER_主程式
            // 
            this.backgroundWorker_LADDER_主程式.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_LADDER_主程式_DoWork);
            // 
            // timer_主執行序監控
            // 
            this.timer_主執行序監控.Interval = 10;
            this.timer_主執行序監控.Tick += new System.EventHandler(this.timer_主執行序監控_Tick);
            // 
            // openFileDialog_LOAD
            // 
            this.openFileDialog_LOAD.DefaultExt = "stp";
            this.openFileDialog_LOAD.Filter = "stp File (*stp)|*stp;";
            // 
            // saveFileDialog_SAVE
            // 
            this.saveFileDialog_SAVE.DefaultExt = "stp";
            this.saveFileDialog_SAVE.Filter = "stp File (*stp)|*stp;";
            // 
            // backgroundWorker_畫面更新
            // 
            this.backgroundWorker_畫面更新.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_畫面更新_DoWork);
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 115200;
            this.serialPort.Parity = System.IO.Ports.Parity.Even;
            // 
            // backgroundWorker_Online讀取
            // 
            // 
            // colorDialog
            // 
            this.colorDialog.FullOpen = true;
            // 
            // backgroundWorker_計時器
            // 
            this.backgroundWorker_計時器.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_計時器_DoWork);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // checkBox_Online_High_Speed
            // 
            this.checkBox_Online_High_Speed.AutoSize = true;
            this.checkBox_Online_High_Speed.Location = new System.Drawing.Point(712, 17);
            this.checkBox_Online_High_Speed.Name = "checkBox_Online_High_Speed";
            this.checkBox_Online_High_Speed.Size = new System.Drawing.Size(112, 16);
            this.checkBox_Online_High_Speed.TabIndex = 15;
            this.checkBox_Online_High_Speed.Text = "Online High Speed";
            this.checkBox_Online_High_Speed.UseVisualStyleBackColor = true;
            // 
            // LADDER_Panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_main);
            this.Name = "LADDER_Panel";
            this.Size = new System.Drawing.Size(1642, 1000);
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            this.panel_程式分頁.ResumeLayout(false);
            this.contextMenuStrip_TreeView.ResumeLayout(false);
            this.panel_工具箱.ResumeLayout(false);
            this.panel_工具箱.PerformLayout();
            this.flowLayoutPanel_工具列_檔案讀寫.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.panel_Datagrid_註解列表.ResumeLayout(false);
            this.panel_Datagrid_註解列表.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_註解列表)).EndInit();
            this.contextMenuStrip_註解列表_右鑑選單.ResumeLayout(false);
            this.panel_LADDER.ResumeLayout(false);
            this.panel_Listbox_IL指令集.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_LADDER)).EndInit();
            this.contextMenuStrip_Ladder_右鍵選單.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_main;
        private System.ComponentModel.BackgroundWorker backgroundWorker_LADDER_主程式;
        private System.Windows.Forms.Panel panel_LADDER;
        private System.Windows.Forms.PictureBox pictureBox_LADDER;
        private System.Windows.Forms.Timer timer_主執行序監控;
        private System.Windows.Forms.OpenFileDialog openFileDialog_LOAD;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_SAVE;
        private System.ComponentModel.BackgroundWorker backgroundWorker_畫面更新;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Ladder_右鍵選單;
        private System.Windows.Forms.ToolStripMenuItem 復原;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 剪下;
        private System.Windows.Forms.ToolStripMenuItem 複製;
        private System.Windows.Forms.ToolStripMenuItem 貼上;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 插入一列;
        private System.Windows.Forms.ToolStripMenuItem 刪除一列;
        private System.Windows.Forms.ToolStripMenuItem 插入一行;
        private System.Windows.Forms.ToolStripMenuItem 編譯;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem 繪製橫線;
        private System.Windows.Forms.ToolStripMenuItem 刪除橫線;
        private System.Windows.Forms.ToolStripMenuItem 繪製豎線;
        private System.Windows.Forms.ToolStripMenuItem 刪除豎線;
        private System.Windows.Forms.ToolStripMenuItem 復原回未編譯;
        private System.Windows.Forms.VScrollBar vScrollBar_picture_滾動條;
        private System.Windows.Forms.ListBox listBox_IL指令集;
        private System.Windows.Forms.Panel panel_Listbox_IL指令集;
        private System.Windows.Forms.Panel panel_Datagrid_註解列表;
        private MyUI.ExButton exButton_註解查詢;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_註解查詢_Device_name;
        private System.Windows.Forms.DataGridView dataGridView_註解列表;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_註解列表_右鑑選單;
        private System.Windows.Forms.ToolStripMenuItem D_剪下;
        private System.Windows.Forms.ToolStripMenuItem D_複製;
        private System.Windows.Forms.ToolStripMenuItem D_貼上;
        private System.Windows.Forms.ToolStripMenuItem D_刪除;
        private System.Windows.Forms.ToolStripMenuItem D_選取全部;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_工具列_檔案讀寫;
        private MyUI.ExButton exButton_開新專案;
        private MyUI.ExButton exButton_讀取檔案;
        private MyUI.ExButton exButton_儲存檔案;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyUI.ExButton exButton_剪下;
        private MyUI.ExButton exButton_複製;
        private MyUI.ExButton exButton_貼上;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private MyUI.ExButton exButton_寫入A接點;
        private MyUI.ExButton exButton_寫入B接點;
        private MyUI.ExButton exButton_畫橫線;
        private MyUI.ExButton exButton_畫豎線;
        private MyUI.ExButton exButton_刪除橫線;
        private MyUI.ExButton exButton_刪除豎線;
        private MyUI.ExButton exButton_窗選模式切換;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private MyUI.ExButton exButton_語法切換;
        private MyUI.ExButton exButton_程式_註解模式選擇;
        private MyUI.ExButton exButton15;
        private MyUI.ExButton exButton_Online;
        private MyUI.ExButton exButton_上一步;
        private MyUI.ExButton exButton_復原回未修正;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem 專案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 顯示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 通訊ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新專案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 讀取ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 儲存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 關閉ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上傳ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下載ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 程式比較ToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Online讀取;
        private System.Windows.Forms.Panel panel_工具箱;
        private System.Windows.Forms.ToolStripMenuItem 註解顯示ToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ToolStripMenuItem 註解顏色ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 註解字體ToolStripMenuItem;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.ToolStripMenuItem 註解格式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 字母數ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_註解字母數;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem 列數ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_註解列數;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_註解字母數_確認;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_註解列數_確認;
        private System.Windows.Forms.ToolStripMenuItem oNLINE字體ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem 顯示_預設值ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_TreeView;
        private System.Windows.Forms.ToolStripMenuItem 顯示註解列表toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator 顯示註解列表toolStripSeparator1;
        private System.ComponentModel.BackgroundWorker backgroundWorker_計時器;
        private System.Windows.Forms.Panel panel_程式分頁;
        private System.Windows.Forms.TreeView treeView_程式分頁;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem顯示目錄;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem視窗字體;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem 調整大小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消ToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.CheckBox checkBox_Online_High_Speed;
    }
}
