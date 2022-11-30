using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
namespace LadderUI
{
    public partial class Up_Down_load_Panel : UserControl
    {
        public Up_Down_load_Panel()
        {
            InitializeComponent();
        }
        private void exButton_關閉_btnClick(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }
}
