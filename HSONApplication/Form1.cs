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
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Schema;
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
            public string 盤點時間 { get; set; }
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
      
        private void Form1_Load(object sender, EventArgs e)
        {
            plC_UI_Init1.Run(this.FindForm(), lowerMachine_Panel1);
            plC_UI_Init1.Set_CycleTime(1);
            plC_UI_Init1.UI_Finished_Event += PlC_UI_Init1_UI_Finished_Event;

            this.plC_RJ_Button_顯示.MouseDownEvent += PlC_RJ_Button_顯示_MouseDownEvent;

        }

        private void PlC_RJ_Button_顯示_MouseDownEvent(MouseEventArgs mevent)
        {
            this.Invoke(new Action(delegate 
            {
                rJ_Button1.Enabled = !rJ_Button1.Enabled;
            }));
        }

        private void PlC_UI_Init1_UI_Finished_Event()
        {
            this.LoadDBConfig();
            SQLUI.SQL_DataGridView.SQL_Set_Properties(dBConfigClass.DB_Basic.DataBaseName, dBConfigClass.DB_Basic.UserName, dBConfigClass.DB_Basic.Password, dBConfigClass.DB_Basic.IP, dBConfigClass.DB_Basic.Port, dBConfigClass.DB_Basic.MySqlSslMode, this.FindForm());
      
        }

        private void SqL_DataGridView_人員資料_CellValidatingEvent(object[] RowValue, int rowIndex, int colIndex, string value, DataGridViewCellValidatingEventArgs e)
        {
           if(value.StringIsInt32() == false)
            {
                e.Cancel = true;
            }
        }

        private void SqL_DataGridView_人員資料_RowEndEditEvent(object[] RowValue, int rowIndex, int colIndex, string value)
        {
            this.sqL_DataGridView_人員資料.SQL_ReplaceExtra(RowValue, false);
          //  this.sqL_DataGridView_人員資料.ReplaceExtra(RowValue, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string table_json = "{\r\n  \"tableName\": \"medicine_page_cloud\",\r\n  \"columnList\": [\r\n    {\r\n      \"name\": \"GUID\",\r\n      \"typeName\": \" VARCHAR(50)\",\r\n      \"indexType\": 2\r\n    },\r\n    {\r\n      \"name\": \"藥品碼\",\r\n      \"typeName\": \" VARCHAR(20)\",\r\n      \"indexType\": 1\r\n    },\r\n    {\r\n      \"name\": \"料號\",\r\n      \"typeName\": \" VARCHAR(20)\",\r\n      \"indexType\": 1\r\n    },\r\n    {\r\n      \"name\": \"中文名稱\",\r\n      \"typeName\": \" VARCHAR(300)\",\r\n      \"indexType\": 0\r\n    },\r\n    {\r\n      \"name\": \"藥品名稱\",\r\n      \"typeName\": \" VARCHAR(300)\",\r\n      \"indexType\": 0\r\n    },\r\n    {\r\n      \"name\": \"藥品學名\",\r\n      \"typeName\": \" VARCHAR(300)\",\r\n      \"indexType\": 0\r\n    },\r\n    {\r\n      \"name\": \"健保碼\",\r\n      \"typeName\": \" VARCHAR(50)\",\r\n      \"indexType\": 0\r\n    },\r\n    {\r\n      \"name\": \"包裝單位\",\r\n      \"typeName\": \" VARCHAR(10)\",\r\n      \"indexType\": 0\r\n    },\r\n    {\r\n      \"name\": \"包裝數量\",\r\n      \"typeName\": \" VARCHAR(10)\",\r\n      \"indexType\": 0\r\n    },\r\n    {\r\n      \"name\": \"最小包裝單位\",\r\n      \"typeName\": \" VARCHAR(10)\",\r\n      \"indexType\": 0\r\n    },\r\n    {\r\n      \"name\": \"最小包裝數量\",\r\n      \"typeName\": \" VARCHAR(10)\",\r\n      \"indexType\": 0\r\n    },\r\n    {\r\n      \"name\": \"藥品條碼1\",\r\n      \"typeName\": \" VARCHAR(200)\",\r\n      \"indexType\": 0\r\n    },\r\n    {\r\n      \"name\": \"藥品條碼2\",\r\n      \"typeName\": \" TEXT\",\r\n      \"indexType\": 0\r\n    },\r\n    {\r\n      \"name\": \"警訊藥品\",\r\n      \"typeName\": \" VARCHAR(10)\",\r\n      \"indexType\": 0\r\n    },\r\n    {\r\n      \"name\": \"管制級別\",\r\n      \"typeName\": \" VARCHAR(10)\",\r\n      \"indexType\": 0\r\n    },\r\n    {\r\n      \"name\": \"類別\",\r\n      \"typeName\": \" VARCHAR(500)\",\r\n      \"indexType\": 0\r\n    }\r\n  ]\r\n}";
            Console.WriteLine(table_json);
            Table table = table_json.JsonDeserializet<Table>();
            this.sqL_DataGridView_人員資料.Init(table);
        }
    }
}
