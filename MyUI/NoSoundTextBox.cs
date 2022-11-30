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
       [System.Drawing.ToolboxBitmap(typeof(TextBox))]
    public partial class NoSoundTextBox : TextBox
    {
        public NoSoundTextBox()
        {
            InitializeComponent();
        }
        protected override bool ProcessDialogKey(Keys KeyCode)
        {
            if (KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                return true;
            }
            return base.ProcessDialogKey(KeyCode);
        }
    }
}
