using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LadderConnection;
using Basic;
namespace MyUI
{
    public partial class ParentCheckBox : CheckBox
    {
        private List<CheckBox> checkBoxes = new List<CheckBox>();

        public ParentCheckBox()
        {
            InitializeComponent();
            this.CheckedChanged += ParentCheckBox_CheckedChanged;
        }

        private void ParentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkBoxes.Count; i++)
            {
                checkBoxes[i].CheckedChanged -= ChildCheckBox_CheckedChanged;
                checkBoxes[i].Checked = this.Checked;
                checkBoxes[i].CheckedChanged += ChildCheckBox_CheckedChanged;
            }

        }

        public void AddChildCheckBox(CheckBox checkBox)
        {
            checkBoxes.Add(checkBox);
            checkBox.CheckedChanged += ChildCheckBox_CheckedChanged;
        }

        private void ChildCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.CheckedChanged -= ParentCheckBox_CheckedChanged;
            try
            {
                for (int i = 0; i < checkBoxes.Count; i++)
                {
                    if (checkBoxes[i].Checked == false)
                    {
                        this.Checked = false;
                        return;
                    }
                }
                this.Checked = true;
            }
            catch
            {

            }
            finally
            {
                this.CheckedChanged += ParentCheckBox_CheckedChanged;
            }

        }
    }
}
