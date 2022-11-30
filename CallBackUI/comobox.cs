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
    static public class comobox
    {
        private delegate void 寫入DataSoure_CallBack(string[] myStr, ComboBox ctl);
        static public void 寫入DataSoure(string[] myStr, ComboBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                寫入DataSoure_CallBack myUpdate = new 寫入DataSoure_CallBack(寫入DataSoure);
                ctl.Invoke(myUpdate, myStr, ctl);
            }
            else
            {
                ctl.DataSource = myStr;
            }
        }

        private delegate void 取得字串_CallBack(ref string str, ComboBox ctl);
        static public void 取得字串(ref string str, ComboBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                取得字串_CallBack myUpdate = new 取得字串_CallBack(取得字串);
                if (ctl.IsHandleCreated) ctl.Invoke(myUpdate, str, ctl);
            }
            else
            {
                str = ctl.Text;
            }
        }
        private delegate void 字串更換_CallBack(string myStr, ComboBox ctl);
        static public void 字串更換(string myStr, ComboBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                字串更換_CallBack myUpdate = new 字串更換_CallBack(字串更換);
                ctl.Invoke(myUpdate, myStr, ctl);
            }
            else
            {
                ctl.Text = myStr;
            }
        }
        private delegate void Select_CallBack(int index, ComboBox ctl);
        static public void Select(int index, ComboBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                Select_CallBack myUpdate = new Select_CallBack(Select);
                if (ctl.IsHandleCreated) ctl.Invoke(myUpdate, index, ctl);
            }
            else
            {
                if (ctl.Items.Count > 0)
                {
                    if (index > ctl.Items.Count - 1) index = ctl.Items.Count - 1;
                    if (index < 0) index = 0;
                    ctl.SelectedIndex = index;
                }        
            }
        }
        private delegate void Items_Clear_CallBack(ComboBox ctl);
        static public void Clear(ComboBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                Items_Clear_CallBack myUpdate = new Items_Clear_CallBack(Clear);
                ctl.Invoke(myUpdate, ctl);
            }
            else
            {
                ctl.Items.Clear();
            }
        }
        private delegate void Items_Add_CallBack(string Item, ComboBox ctl);
        static public void Add(string Item, ComboBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                Items_Add_CallBack myUpdate = new Items_Add_CallBack(Add);
                ctl.Invoke(myUpdate, Item, ctl);
            }
            else
            {
                ctl.Items.Add(Item);
            }
        }
        private delegate void Enable_CallBack(bool flag, ComboBox ctl);
        static public void Enable(bool flag, ComboBox ctl)
        {
            if (ctl.InvokeRequired)
            {
                Enable_CallBack myUpdate = new Enable_CallBack(Enable);
                ctl.Invoke(myUpdate, flag, ctl);
            }
            else
            {
                ctl.Enabled = flag;
            }
        }
    }
}
