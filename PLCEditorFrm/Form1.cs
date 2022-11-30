using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPP編譯軟體__鴻森整合機電有限公司
{

    public partial class Form_main : Form
    {
        public Form_main()
        {
            InitializeComponent();

        }
        private void Form_main_Load(object sender, EventArgs e)
        {
            laddeR_Panel.Run(this.FindForm());
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
