using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CallBackUI
{
    static public class listbox
    {
  

        private delegate void 新增項目_CallBack(string myStr, ListBox ctl);
        static public void 新增項目(string myStr, ListBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                新增項目_CallBack myUpdate = new 新增項目_CallBack(新增項目);
                if (ctl.IsHandleCreated) ctl.Invoke(myUpdate, myStr, ctl);
            }
            else
            {
                ctl.Items.Add(myStr);
            }
        }

        private delegate void 清除所有資料_CallBack(ListBox ctl);
        static public void 清除所有資料(ListBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                清除所有資料_CallBack myUpdate = new 清除所有資料_CallBack(清除所有資料);
                if (ctl.IsHandleCreated) ctl.Invoke(myUpdate, ctl);
            }
            else
            {
                ctl.Items.Clear();
            }
        }
        private delegate void 刪除第一列_CallBack(ListBox ctl);
        static public void 刪除第一列(ListBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                刪除第一列_CallBack myUpdate = new 刪除第一列_CallBack(刪除第一列);
                if (ctl.IsHandleCreated) ctl.Invoke(myUpdate, ctl);
            }
            else
            {
                if (ctl.Items.Count > 0)
                {
                    ctl.Items.RemoveAt(0);
                }

            }
        }
        private delegate void 刪除最後一列_CallBack(ListBox ctl);
        static public void 刪除最後一列(ListBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                刪除最後一列_CallBack myUpdate = new 刪除最後一列_CallBack(刪除最後一列);
                if (ctl.IsHandleCreated) ctl.Invoke(myUpdate, ctl);
            }
            else
            {
                if (ctl.Items.Count>0)
                {
                    ctl.Items.RemoveAt(ctl.Items.Count - 1);
                }
           
            }
        }
        private delegate void 自動捲軸_CallBack(int 顯示項目數量, ListBox ctl);
        static public void 自動捲軸(int 顯示項目數量, ListBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                自動捲軸_CallBack myUpdate = new 自動捲軸_CallBack(自動捲軸);
                if (ctl.IsHandleCreated) ctl.Invoke(myUpdate, 顯示項目數量, ctl);
            }
            else
            {
                ctl.TopIndex = ctl.Items.Count - 顯示項目數量;
            }
        }
        private delegate void 是否隱藏_CallBack(bool 隱藏, ListBox ctl);
        static public void 是否隱藏(bool 隱藏, ListBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                是否隱藏_CallBack myUpdate = new 是否隱藏_CallBack(是否隱藏);
                if (ctl.IsHandleCreated) ctl.Invoke(myUpdate, 隱藏, ctl);
            }
            else
            {
                ctl.Visible = !隱藏;
            }
        }
    }
}
