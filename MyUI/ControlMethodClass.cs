using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using LadderConnection;
using System.Media;
using System.Runtime.InteropServices;

namespace MyUI
{
    public static class ControlMethodClass
    {
        public static string GetComboBoxText(this ComboBox comboBox)
        {
            string text = "";
            comboBox.Invoke(new Action(delegate 
            {
                text = comboBox.Text;
            }));
            return text;
        }


    }
}
