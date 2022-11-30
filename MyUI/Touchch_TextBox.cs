using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyUI
{
    public partial class Touchch_TextBox : TextBox
    {
        public Touchch_TextBox()
        {
            InitializeComponent();
        }

        private void Touchch_TextBox_Enter(object sender, EventArgs e)
        {
            Basic.Screen.ShowInputPanel();
        }

        private void Touchch_TextBox_Leave(object sender, EventArgs e)
        {
            Basic.Screen.HideInputPanel();
        }
    }
}
