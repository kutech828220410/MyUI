using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MeasureSystemFunction;
namespace MeasureSystemUI
{
    public partial class VariableUI : UserControl
    {
        #region 隱藏屬性
        [Browsable(false)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
        [Browsable(false)]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }
        [Browsable(false)]
        public override ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
            set
            {
                base.BackgroundImageLayout = value;
            }
        }
        [Browsable(false)]
        public override Cursor Cursor
        {
            get
            {
                return base.Cursor;
            }
            set
            {
                base.Cursor = value;
            }
        }
        [Browsable(false)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }
        [Browsable(false)]
        public override System.Drawing.Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }
        [Browsable(false)]
        public override RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }
            set
            {
                base.RightToLeft = value;
            }
        }

        #endregion
        #region 自訂屬性
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public VarTypeEnum _字元種類 = VarTypeEnum.String;
        public VarTypeEnum 字元種類 
        {
            get
            {
                return this._字元種類;
            }
            set
            {
                _字元種類 = value;

            }
        }
        int _註解欄寬度 = 150;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int 註解欄寬度
        {
            get
            {
                return _註解欄寬度;
            }
            set
            {
                _註解欄寬度 = value;
                textBox_Comment.Size = new Size(value, textBox_Comment.Size.Height);
            }
        }
        private Font _註解字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Font 註解字體
        {
            get { return _註解字體; }
            set
            {
                _註解字體 = value;
                textBox_Comment.Font = value;
            }
        }
        int _數值欄寬度 = 250;
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public int 數值欄寬度
        {
            get
            {
                return _數值欄寬度;
            }
            set
            {
                _數值欄寬度 = value;
                textBox_Value.Size = new Size(value, textBox_Value.Size.Height);
            }
        }
        private Font _數值字體 = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        [ReadOnly(false), Browsable(true), Category("自訂屬性"), Description(""), DefaultValue("")]
        public Font 數值字體
        {
            get { return _數值字體; }
            set
            {
                _數值字體 = value;
                textBox_Value.Font = value;
            }
        }
        #endregion
        public override System.Windows.Forms.Layout.LayoutEngine LayoutEngine
        {
            get
            {
                this.Size = new System.Drawing.Size(10 + comboBox_VariableItem.Size.Width + 10 + textBox_Comment.Size.Width + 10 + textBox_Value.Size.Width + 10, this.Size.Height);
                Point p0 = new Point(10, this.Size.Height / 2 - comboBox_VariableItem.Size.Height / 2);
                comboBox_VariableItem.Location = p0;
                p0 = new Point(p0.X + comboBox_VariableItem.Size.Width + 10, this.Size.Height / 2  - textBox_Comment.Size.Height /2);
                textBox_Comment.Location = p0;
                p0 = new Point(p0.X + textBox_Comment.Size.Width + 10, this.Size.Height / 2 - textBox_Value.Size.Height / 2);
                textBox_Value.Location = p0;
                return base.LayoutEngine;
            }
        }
        private VariableListCtl VariableListCtl;
        private VariableClass VariableClass;
        public VariableUI()
        {
            InitializeComponent();
        }
        public void Init(VariableListCtl VariableListCtl)
        {
            this.VariableListCtl = VariableListCtl;
            ComboBoxItemRefresh();
        }
        public void Init(VariableListCtl VariableListCtl , string ID)
        {
            this.VariableListCtl = VariableListCtl;
            ComboBoxItemRefresh();
            comboBox_VariableItem.Text = ID;
        }

        public void ComboBoxItemRefresh()
        {
            if (this.VariableListCtl != null)
            {
                comboBox_VariableItem.DataSource = this.VariableListCtl.GetAllVariableIDFromType(字元種類);
                label1.Text = 字元種類.ToString();
            }
        }
        private void comboBox_VariableItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.VariableClass = VariableListCtl.GetVariableFromID(comboBox_VariableItem.Text);
            if (this.VariableClass != null)
            {
                textBox_Comment.Text = this.VariableClass.GetComment().ToString();
                textBox_Value.Text = this.VariableClass.GetValue().ToString();
            }
        }
        public string GetVariableID()
        {
            return comboBox_VariableItem.Text;
        }
        public void SetValue(object value)
        {
            VariableClass.SetValue(value);
        }
        private void textBox_Comment_TextChanged(object sender, EventArgs e)
        {
            VariableClass.SetComment(textBox_Comment.Text);
        }
        private void textBox_Value_TextChanged(object sender, EventArgs e)
        {
            bool Value_test_OK = false;
            string str_temp = textBox_Value.Text;
            object value = new object();
            switch (VariableClass.GetEnumType())
            {
                case VarTypeEnum.Double:
                    double double_temp;
                    Value_test_OK = double.TryParse(str_temp, out double_temp);
                    value = double_temp;
                    break;
                case VarTypeEnum.Float:
                    float float_temp;
                    Value_test_OK = float.TryParse(str_temp, out float_temp);
                    value = float_temp;
                    break;
                case VarTypeEnum.Int32:
                    Int32 Int32_temp;
                    Value_test_OK = Int32.TryParse(str_temp, out Int32_temp);
                    value = Int32_temp;
                    break;
                case VarTypeEnum.Int64:
                    Int64 Int64_temp;
                    Value_test_OK = Int64.TryParse(str_temp, out Int64_temp);
                    value = Int64_temp;
                    break;
                case VarTypeEnum.Boolean:
                    Boolean Boolean_temp;
                    if (str_temp == "False" || str_temp == "True" || str_temp == "T" || str_temp == "F")
                    {
                        Value_test_OK = true;
                        if( str_temp == "T")
                        {
                            str_temp = "True";
                        }
                        else if (str_temp == "F")
                        {
                            str_temp = "False";
                        }
                    }
                    if (str_temp == "True")
                    {
                        Boolean_temp = true;
                    }
                    else if (str_temp == "False")
                    {
                        Boolean_temp = false;
                    }
                    else Boolean_temp = false;
                    value = Boolean_temp;
                    break;
                case VarTypeEnum.String:
                    value = str_temp;
                    Value_test_OK = true;
                    break;

            }

            if (Value_test_OK)
            {
              //  VariableClass.SetValue(value);
            }
            else
            {
                MessageBox.Show("請輸入正確數值!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox_Value.Text = this.VariableClass.GetValue().ToString();
            }

        }
    }
}
