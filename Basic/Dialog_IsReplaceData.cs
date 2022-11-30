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
    public partial class Dialog_IsReplaceData : Form
    {
        public enum_Result Result;
        private List<string> List_SourceData = new List<string>();
        private List<string> List_TargetData = new List<string>();

        public enum enum_Result
        {
            None = 0, Yes = 1, No = 2, All_Yes = 3, All_No = 4
        }
        private bool _Is_ShowAllYes_Button = true;
        public bool Is_ShowAllYes_Button
        {
            get
            {
                return _Is_ShowAllYes_Button;
            }
            set
            {
                _Is_ShowAllYes_Button = value;
            }
        }
        private bool _Is_ShowAllNo_Button = true;
        public bool Is_ShowAllNo_Button
        {
            get
            {
                return _Is_ShowAllNo_Button;
            }
            set
            {
                _Is_ShowAllNo_Button = value;
            }
        }

        public Dialog_IsReplaceData()
        {
            InitializeComponent();
        }

        #region Function
        public void Add_SourceData(string str)
        {
            this.List_SourceData.Add(str);
        }
        public void Set_SourceDataList(object[] value ,object Enum)
        {
            string[] enum_names = Enum.GetEnumNames();
            List<string> str_List = new List<string>();
            for (int i = 0; i < value.Length; i++)
            {
                str_List.Add(((i + 1).ToString("00") + ". " + enum_names[i]).StringLength(20) + value[i].ObjectToString());
            }
            this.Set_SourceDataList(str_List);
        }
        public void Set_SourceDataList(object[] value)
        {
            List<string> str_List = new List<string>();
            foreach (object temp in value)
            {
                str_List.Add((string)temp);
            }
            this.Set_SourceDataList(str_List);
        }
        public void Set_SourceDataList(List<string> List)
        {
            this.List_SourceData = List;
        }
        public void Add_TargetData(string str)
        {
            this.List_TargetData.Add(str);
        }
        public void Set_TargetDataList(object[] value, object Enum)
        {
            string[] enum_names = Enum.GetEnumNames();
            List<string> str_List = new List<string>();
            for (int i = 0; i < value.Length; i++)
            {
                str_List.Add(((i + 1).ToString("00") + "." + enum_names[i]).StringLength(20) + ":" + value[i].ObjectToString());
            }
            this.Set_TargetDataList(str_List);
        }
        public void Set_TargetDataList(object[] value)
        {
            List<string> str_List = new List<string>();
            foreach (object temp in value)
            {
                str_List.Add((string)temp);
            }
            this.Set_TargetDataList(str_List);
        }
        public void Set_TargetDataList(List<string> List)
        {
            this.List_TargetData = List;
        }
        #endregion
        #region Event
        private void Dialog_IsReplaceData_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.Result = enum_Result.None;
            this.button_全部皆是.Visible = this.Is_ShowAllYes_Button;
            this.button_全部皆否.Visible = this.Is_ShowAllNo_Button;

            foreach (string temp in List_SourceData)
            {
                this.listBox_SourceData.Items.Add(temp);
            }
            foreach (string temp in List_TargetData)
            {
                this.listBox_TargetData.Items.Add(temp);
            }
        }

        private void button_是_Click(object sender, EventArgs e)
        {
            this.Result = enum_Result.Yes;
            this.Close();
        }

        private void button_否_Click(object sender, EventArgs e)
        {
            this.Result = enum_Result.No;
            this.Close();
        }

        private void button_全部皆是_Click(object sender, EventArgs e)
        {
            this.Result = enum_Result.All_Yes;
            this.Close();
        }

        private void button_全部皆否_Click(object sender, EventArgs e)
        {
            this.Result = enum_Result.All_No;
            this.Close();
        }
        #endregion
    }
}
