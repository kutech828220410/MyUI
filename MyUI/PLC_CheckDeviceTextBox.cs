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
    public partial class PLC_CheckDeviceTextBox  : TextBox
    {
        #region 自訂屬性
        public enum 格式Enum : int
        {
            DATA = 0, BIT
        }
        格式Enum _格式Enum = new 格式Enum();
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public 格式Enum 格式
        {
            get
            {
                return _格式Enum;
            }
            set
            {
                _格式Enum = value;
            }
        }
        #endregion
        private delegate void BaseTextDelegate(string str);
        void BaseText(string str)
        {
            base.Text = str;
        }
        public override string Text
        {
            get
            {
                return textBox1.Text;
            }
            set
            {               
                //BaseTextDelegate baseTextDelegate = new BaseTextDelegate(BaseText);
                //Invoke(baseTextDelegate, value);
                base.Text = value;
                textBox1.Text = value;
            }
        }

        public PLC_CheckDeviceTextBox()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            base.Text = textBox1.Text;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (((int)e.KeyChar <= 57 && (int)e.KeyChar >= 48) || ((int)e.KeyChar <= 90 && (int)e.KeyChar >= 65) || ((int)e.KeyChar <= 122 && (int)e.KeyChar >= 97) || (int)e.KeyChar == 8 || (int)e.KeyChar == 13) // 8 > BackSpace
            {
                if ((int)e.KeyChar == 13)
                {
                    textBox1.Text = CheckText(textBox1.Text);
                }
                e.Handled = false;
            }
            else e.Handled = true;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.Text = CheckText(textBox1.Text);
            base.Text = textBox1.Text;
        }
        string CheckText(string text)
        {
            if(text.Length > 0)
            {
                string temp = "";
                if (!LadderProperty.DEVICE.TestDevice(text))
                {
                    text = "";
                    return text;
                }
                if (格式 == 格式Enum.BIT)
                {
                    temp = text.Remove(1);
                    if (!(temp == "S" || temp == "M"))
                    {
                        text = "";
                        return text;
                    }
                }
                else if (格式 == 格式Enum.DATA)
                {
                    temp = text.Remove(1);
                    if (!(temp == "D" || temp == "R" || temp == "F"))
                    {
                        text = "";
                        return text;
                    }
                }
            }

            return text;
        }
    }
}
