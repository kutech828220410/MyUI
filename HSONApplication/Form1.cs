using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Net;
using MyUI;
using Basic;
using System.IO;
using SQLUI;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
namespace HSONApplication
{
    public partial class Form1 : Form
    {
        public class class_acceptance_med_data
        {
            [JsonPropertyName("code")]
            public string 藥品碼 { get; set; }
            [JsonPropertyName("value")]
            public string 數量 { get; set; }
            [JsonPropertyName("validity_period")]
            public string 效期 { get; set; }
            [JsonPropertyName("lot_number")]
            public string 批號 { get; set; }
            [JsonPropertyName("acceptance_date")]
            public string 驗收時間 { get; set; }
            [JsonPropertyName("state")]
            public string 狀態 { get; set; }

        }

        #region DBConfigClass
        private const string DBConfigFileName = "DBConfig.txt";
        public DBConfigClass dBConfigClass = new DBConfigClass();
        public class DBConfigClass
        {
            private SQL_DataGridView.ConnentionClass dB_Basic = new SQL_DataGridView.ConnentionClass();
            private SQL_DataGridView.ConnentionClass dB_person_page = new SQL_DataGridView.ConnentionClass();
            private SQL_DataGridView.ConnentionClass dB_order_list = new SQL_DataGridView.ConnentionClass();


            public SQL_DataGridView.ConnentionClass DB_Basic { get => dB_Basic; set => dB_Basic = value; }
            public SQL_DataGridView.ConnentionClass DB_person_page { get => dB_person_page; set => dB_person_page = value; }
            public SQL_DataGridView.ConnentionClass DB_order_list { get => dB_order_list; set => dB_order_list = value; }
        }
        private void LoadDBConfig()
        {
            string jsonstr = MyFileStream.LoadFileAllText($".//{DBConfigFileName}");
            if (jsonstr.StringIsEmpty())
            {

                jsonstr = Basic.Net.JsonSerializationt<DBConfigClass>(new DBConfigClass());
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($".//{DBConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{DBConfigFileName}檔案失敗!");
                }
                MyMessageBox.ShowDialog($"未建立參數文件!請至子目錄設定{DBConfigFileName}");
                Application.Exit();
            }
            else
            {
                dBConfigClass = Basic.Net.JsonDeserializet<DBConfigClass>(jsonstr);

                jsonstr = Basic.Net.JsonSerializationt<DBConfigClass>(dBConfigClass);
                List<string> list_jsonstring = new List<string>();
                list_jsonstring.Add(jsonstr);
                if (!MyFileStream.SaveFile($".//{DBConfigFileName}", list_jsonstring))
                {
                    MyMessageBox.ShowDialog($"建立{DBConfigFileName}檔案失敗!");
                }

            }
        }
        #endregion
        Basic.MyConvert _MyConvert = new Basic.MyConvert();
        public Form1()
        {
            InitializeComponent();
        }
        enum file
        {
            GUID,
            filename,
            value,
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            rJ_Pannel1.MouseDown += RJ_Pannel1_MouseDown;
            plC_UI_Init1.Run(this.FindForm(), lowerMachine_Panel1);
            plC_UI_Init1.Set_CycleTime(1);
            plC_UI_Init1.UI_Finished_Event += PlC_UI_Init1_UI_Finished_Event;
        }

        private void RJ_Pannel1_MouseDown(object sender, MouseEventArgs e)
        {
          
        }

        private void PlC_UI_Init1_UI_Finished_Event()
        {
            this.LoadDBConfig();
            SQLUI.SQL_DataGridView.SQL_Set_Properties(dBConfigClass.DB_Basic.DataBaseName, dBConfigClass.DB_Basic.UserName, dBConfigClass.DB_Basic.Password, dBConfigClass.DB_Basic.IP, dBConfigClass.DB_Basic.Port, dBConfigClass.DB_Basic.MySqlSslMode, this.FindForm());
            sqL_DataGridView_file.Init();
            if (!sqL_DataGridView_file.SQL_IsTableCreat()) sqL_DataGridView_file.SQL_CreateTable();
        }

        private void SqL_DataGridView_人員資料_CheckedChangedEvent(List<object[]> RowsList)
        {
         
        }

        private void rJ_Button1_Click(object sender, EventArgs e)
        {
        }

        private void rJ_Button2_Click(object sender, EventArgs e)
        {
        
        }

        private void lowerMachine_Panel1_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(5000);
            byte[] bytes = FileIO.LoadFileStream(@"C:\Users\User\Desktop\Release.zip");
            Console.WriteLine($"讀取檔案,耗時 {myTimer.ToString()}");
            List<object[]> list_value = this.sqL_DataGridView_file.SQL_GetAllRows(false);
            list_value.GetRows((int)file.filename, "Release.zip");
            Console.WriteLine($"取得SQL資料,耗時 {myTimer.ToString()}");
            object[] value = new object[new file().GetLength()];
            if (list_value.Count == 0)
            {
                value[(int)file.GUID] = Guid.NewGuid().ToString();
                value[(int)file.filename] = "Release.zip";
                value[(int)file.value] = bytes;
                this.sqL_DataGridView_file.SQL_AddRow(value, false);
                Console.WriteLine($"新增SQL資料,耗時 {myTimer.ToString()}");
            }
            else
            {
                value = list_value[0];
                value[(int)file.filename] = "Release.zip";
                value[(int)file.value] = bytes;
                this.sqL_DataGridView_file.SQL_ReplaceExtra(value, false);
                Console.WriteLine($"修正SQL資料,耗時 {myTimer.ToString()}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyTimer myTimer = new MyTimer();
            myTimer.StartTickTime(5000);
            List<object[]> list_value = this.sqL_DataGridView_file.SQL_GetAllRows(false);
            Console.WriteLine($"取得SQL資料,耗時 {myTimer.ToString()}");
            for (int i = 0; i < list_value.Count; i++)
            {
                if(list_value[i][(int)file.value] is byte[])
                {
                    FileIO.SaveFileStream((byte[])list_value[i][(int)file.value], @"C:\Users\User\Desktop\Release11122.zip");
                }
            }
            Console.WriteLine($"寫入本地,耗時 {myTimer.ToString()}");
        }
    }
}
