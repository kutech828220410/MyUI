using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic
{
    public partial class Dialog_Validity_period_Inventory_Setting : Form
    {
        private List<string> list_Validity_period = new List<string>();
        private List<string> list_Inventory = new List<string>();
        private List<string> list_Lot_number = new List<string>();

        public List<string> List_Validity_period { get => list_Validity_period; set => list_Validity_period = value; }
        public List<string> List_Inventory { get => list_Inventory; set => list_Inventory = value; }
        public List<string> List_Lot_number { get => list_Lot_number; set => list_Lot_number = value; }

        public void Set_Value(List<string> List_Validity_period , List<string> List_Lot_number , List<string> List_Inventory)
        {
            this.List_Validity_period = List_Validity_period;
            this.List_Inventory = List_Inventory;
            this.List_Lot_number = List_Lot_number;
            this.listBox_效期.DataSource = List_Validity_period;
        }
        public Dialog_Validity_period_Inventory_Setting()
        {
            InitializeComponent();
        }

        private void Dialog_Validity_period_Inventory_Setting_Load(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.TopLevel = true;
            this.TopMost = true;
        }       
        private void rJ_Button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
            this.Dispose();
        }
        private void rJ_Button_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
            this.Dispose();
        }

        private void listBox_效期_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox_效期.SelectedIndex;
            this.rJ_TextBox_批號.Text = this.List_Lot_number[index];
            this.rJ_TextBox_庫存.Text = this.List_Inventory[index];
        }

        private void rJ_TextBox_庫存_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar <= 57 && (int)e.KeyChar >= 48) || (int)e.KeyChar == 8) // 8 > BackSpace
            {

                e.Handled = false;
            }
            else e.Handled = true;
        }

        private void rJ_TextBox_批號__TextChanged(object sender, EventArgs e)
        {
            int index = listBox_效期.SelectedIndex;
            this.List_Lot_number[index] = this.rJ_TextBox_批號.Text;
        }
        private void rJ_TextBox_庫存__TextChanged(object sender, EventArgs e)
        {
            int index = listBox_效期.SelectedIndex;
            if (!this.rJ_TextBox_庫存.Text.StringIsEmpty()) this.rJ_TextBox_庫存.Text = this.rJ_TextBox_庫存.Text.StringToInt32().ToString();
            else this.rJ_TextBox_庫存.Text = "0";
            this.List_Inventory[index] = this.rJ_TextBox_庫存.Text;
        }
    }
}
