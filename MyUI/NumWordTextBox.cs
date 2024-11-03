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
    public partial class NumWordTextBox : TextBox
    {
        string str_value = "";
        private delegate void BaseTextDelegate(string str);
        void BaseText(string str)
        {
            base.Text = str;
        }
        public override string Text
        {
            get
            {
                return str_value;
            }
            set
            {
                //BaseTextDelegate baseTextDelegate = new BaseTextDelegate(BaseText);
                //Invoke(baseTextDelegate, value);
                base.Text = value;
                textBox1.Text = value;
                str_value = value;
            }
        }

        public NumWordTextBox()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            base.Text = textBox1.Text;
            this.str_value = textBox1.Text;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (((int)e.KeyChar <= 57 && (int)e.KeyChar >= 48) || ((int)e.KeyChar <= 90 && (int)e.KeyChar >= 65) || ((int)e.KeyChar <= 122 && (int)e.KeyChar >= 97) || (int)e.KeyChar == 8) // 8 > BackSpace
            {
                e.Handled = false;
            }
            else e.Handled = true;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
        }
    }
}
