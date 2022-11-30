using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MeasureSystemFunction;
namespace MeasureSystemUI
{
    public partial class MeasureSystemFrmUI : UserControl
    {
        MoudleListCtl moudleListCtl = new MoudleListCtl();
        VariableListCtl variableListCtl = new VariableListCtl();
        MoudleDataGridView moudleDataGridView;
        public void Run()
        {
            plC_UI_Init1.Run(this.FindForm(), lowerMachine_Panel1);
            variableListCtl = VariableFrm.VariableListCtl;
            moudleDataGridView = new MoudleDataGridView(moudleListCtl, variableListCtl, dataGridView_模组列表);
        }
        public MeasureSystemFrmUI()
        {
            InitializeComponent();
        }
        private void MeasureSystemFrmUI_Load(object sender, EventArgs e)
        {

        }
  
        
        #region EvevtFunction-上方下拉式選單
        private void 檔案路徑瀏覽ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moudleListCtl.AddMoudle(ModleFactory.CreateModle(ModleType.系統模組.A00_檔案路徑瀏覽));
            moudleListCtl.GetCurrentMoudle().GetForm(moudleListCtl );
        }
        private void 影像路徑瀏覽ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           moudleListCtl.AddMoudle(ModleFactory.CreateModle(ModleType.系統模組.A01_影像路徑瀏覽));
           moudleListCtl.GetCurrentMoudle().GetForm(moudleListCtl);
        }
        private void _8位深度影像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moudleListCtl.AddMoudle(ModleFactory.CreateModle(ModleType.影像宣告模組.B00_8位深度影像));
            moudleListCtl.GetCurrentMoudle().GetForm(moudleListCtl);
        }
        private void _24位深度彩色影像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moudleListCtl.AddMoudle(new ModleClass(ModleType.影像宣告模組.B01_24位深度影像));
            moudleListCtl.GetCurrentMoudle().GetForm(moudleListCtl);
        }
        #endregion
        #region EvevtFunction-DataGridView_模组列表
        private void dataGridView_模组列表_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            for (int i = 0; i < dataGridView_模组列表.Rows.Count; i++)
            {
                dataGridView_模组列表.Rows[i].Selected = false;
            }
            if (e.RowIndex >= 0) dataGridView_模组列表.Rows[e.RowIndex].Selected = true;
        }

        private void 刪除模組ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string SN = "";
            string ID = "";
            for (int i = 0; i < dataGridView_模组列表.Rows.Count; i++)
            {
                if (dataGridView_模组列表.Rows[i].Selected)
                {
                    SN = (string)dataGridView_模组列表[0, i].Value;
                    ID = (string)dataGridView_模组列表[1, i].Value;
                }
            }
            moudleListCtl.DeleteMoudle(SN, ID);
        }
        private void 編輯ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string SN = "";
            string ID = "";
            for (int i = 0; i < dataGridView_模组列表.Rows.Count; i++)
            {
                if (dataGridView_模组列表.Rows[i].Selected)
                {
                    SN = (string)dataGridView_模组列表[0, i].Value;
                    ID = (string)dataGridView_模组列表[1, i].Value;
                }
            }
            moudleListCtl.GetMoudle(SN, ID).GetForm(moudleListCtl);
        }
        private void 刪除相關所有模組ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string SN = "";
            string ID = "";
            for (int i = 0; i < dataGridView_模组列表.Rows.Count; i++)
            {
                if (dataGridView_模组列表.Rows[i].Selected)
                {
                    SN = (string)dataGridView_模组列表[0, i].Value;
                    ID = (string)dataGridView_模组列表[1, i].Value;
                }
            }
            moudleListCtl.DeleteMoudle(SN);
        }
        #endregion

        private void 開啟專案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                moudleListCtl.LoadMoudles(openFileDialog.FileName);
            }
        }
        private void 儲存專案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                moudleListCtl.SaveMoudles(saveFileDialog.FileName);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            VariableFrm.GetForm().ShowDialog();
        }

  
    }
}
