using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CallBackUI
{
    static public class datagrid
    {
        private delegate void 填入DataTable_CallBack(DataTable datatable, DataGridView ctl);
        static public void 填入DataTable(DataTable datatable, DataGridView ctl)
        {
            if (ctl.InvokeRequired)
            {
                填入DataTable_CallBack myUpdate = new 填入DataTable_CallBack(填入DataTable);
                if (ctl.IsHandleCreated) ctl.Invoke(myUpdate, datatable, ctl);
            }
            else
            {
                ctl.DataSource = datatable;
            }
        }

        private delegate void 新增項目_CallBack(Object[] obj, DataTable ctl, DataGridView ctl1);
        static public void 新增項目(Object[] obj, DataTable ctl, DataGridView ctl1)
        {
            if (ctl1.InvokeRequired)
            {
                新增項目_CallBack myUpdate = new 新增項目_CallBack(新增項目);
                if (ctl1.IsHandleCreated) ctl1.Invoke(myUpdate, obj, ctl, ctl1);
            }
            else
            {
                ctl.Rows.Add(obj);
            }
        }

        private delegate void 清除所有資料_CallBack(DataGridView ctl);
        static public void 清除所有資料(DataGridView ctl)
        {
            if (ctl.InvokeRequired)
            {
                清除所有資料_CallBack myUpdate = new 清除所有資料_CallBack(清除所有資料);
                ctl.Invoke(myUpdate, ctl);
            }
            else
            {
                ctl.Rows.Clear();
            }
        }
        private delegate void 改變指定列資料_CallBack(Object[] obj, int num, DataGridView ctl);
        static public void 改變指定列資料(Object[] obj, int num, DataGridView ctl)
        {
            if (ctl.InvokeRequired)
            {
                改變指定列資料_CallBack myUpdate = new 改變指定列資料_CallBack(改變指定列資料);
                ctl.Invoke(myUpdate, obj, num, ctl);
            }
            else
            {
                for (int i = 0; i < obj.Length; i++)
                {
                    try
                    { ctl.Rows[num].Cells[i].Value = obj[i]; }
                    catch
                    { }

                }
            }
        }
        private delegate void 改變指定位置可編輯_CallBack(int rows, int cell, bool 可編輯, DataGridView ctl);
        static public void 改變指定位置可編輯(int rows, int cell, bool 可編輯, DataGridView ctl)
        {
            if (ctl.InvokeRequired)
            {
                改變指定位置可編輯_CallBack myUpdate = new 改變指定位置可編輯_CallBack(改變指定位置可編輯);
                ctl.Invoke(myUpdate, rows, cell, 可編輯, ctl);
            }
            else
            {
                ctl.Rows[rows].Cells[cell].ReadOnly = !可編輯;
            }
        }
        private delegate void 改變指定位置顏色_CallBack(int rows, int cell, Color color, DataGridView ctl);
        static public void 改變指定位置顏色(int rows, int cell, Color color, DataGridView ctl)
        {
            if (ctl.InvokeRequired)
            {
                改變指定位置顏色_CallBack myUpdate = new 改變指定位置顏色_CallBack(改變指定位置顏色);
                ctl.Invoke(myUpdate, rows, cell, color, ctl);
            }
            else
            {
                ctl.Rows[rows].Cells[cell].Style.BackColor = color;
            }
        }
        private delegate void 是否隱藏_CallBack(bool 隱藏, DataGridView ctl);
        static public void 是否隱藏(bool 隱藏, DataGridView ctl)
        {
            if (ctl.InvokeRequired)
            {
                是否隱藏_CallBack myUpdate = new 是否隱藏_CallBack(是否隱藏);
                ctl.Invoke(myUpdate, 隱藏, ctl);
            }
            else
            {
                ctl.Visible = !隱藏;
            }
        }
    }
}
