using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyUI;

namespace CallBackUI
{
    static public class treeview
    {
        private delegate void 清空節點_CallBack(TreeView ctl);
        static public void 清空節點(TreeView ctl)
        {
            if (ctl.InvokeRequired)
            {
                清空節點_CallBack myUpdate = new 清空節點_CallBack(清空節點);
                ctl.Invoke(myUpdate, ctl);
            }
            else
            {
                ctl.Nodes.Clear();
            }
        }
        private delegate void 新增節點_CallBack(string str ,TreeView ctl);
        static public void 新增節點(string str, TreeView ctl)
        {
            if (ctl.InvokeRequired)
            {
                新增節點_CallBack myUpdate = new 新增節點_CallBack(新增節點);
                ctl.Invoke(myUpdate,str, ctl);
            }
            else
            {
                ctl.Nodes.Add(str);
            }
        }
    }
}
