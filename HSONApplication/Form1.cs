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

        public enum ContextMenuStrip_藥品資料_藥檔資料
        {
            [Description("M8000")]
            匯出,
            [Description("M8000")]
            匯入,
            [Description("M8000")]
            顯示有儲位藥品,
            [Description("M8000")]
            下載未建置條碼藥品,
            [Description("M8000")]
            下載已建置條碼藥品,
            [Description("M8000")]
            上載條碼Excel表,
            [Description("M8000")]
            藥檔維護,
            [Description("S4077")]
            更新至線上藥檔,
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

        private void rJ_Button3_Click(object sender, EventArgs e)
        {
            Dialog_ContextMenuStrip dialog_ContextMenuStrip = new Dialog_ContextMenuStrip(new ContextMenuStrip_藥品資料_藥檔資料());
            if (dialog_ContextMenuStrip.ShowDialog() == DialogResult.Yes)
            {
               
            }
        }
    }
}
