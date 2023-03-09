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
            this.plC_RJ_Pannel1.Set_Enable(false);
            SQLUI.SQL_DataGridView.SQL_Set_Properties(dBConfigClass.DB_Basic.DataBaseName, dBConfigClass.DB_Basic.UserName, dBConfigClass.DB_Basic.Password, dBConfigClass.DB_Basic.IP, dBConfigClass.DB_Basic.Port, dBConfigClass.DB_Basic.MySqlSslMode, this.FindForm());
         
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
            this.plC_RJ_Pannel1.Set_Enable(!this.plC_RJ_Pannel1.Get_Enable());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Body><Drug_DATAResponse xmlns=\"http://tempuri.org/\"><Drug_DATAResult><xs:schema id=\"NewDataSet\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\"><xs:element name=\"NewDataSet\" msdata:IsDataSet=\"true\" msdata:MainDataTable=\"Temp1\" msdata:UseCurrentLocale=\"true\"><xs:complexType><xs:choice minOccurs=\"0\" maxOccurs=\"unbounded\"><xs:element name=\"Temp1\"><xs:complexType><xs:sequence><xs:element name=\"MCODE\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"FULLNAME\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"SHORTNAME\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"UDPOS\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"UDPOS11\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"UDPOS12\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"UDPOS21\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"UDPOS22\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"UDPOS221\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"UDPOS222\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"FQCNVOUT2\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"FQCNVOUT3\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"MFG_COMP\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"MFG_NAME\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"LICENSE\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"DRGLOTNO1\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"DRGDUEDT1\" type=\"xs:dateTime\" minOccurs=\"0\" /><xs:element name=\"DRGLOTNO2\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"DRGDUEDT2\" type=\"xs:dateTime\" minOccurs=\"0\" /><xs:element name=\"DRGLOTNO3\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"DRGDUEDT3\" type=\"xs:dateTime\" minOccurs=\"0\" /><xs:element name=\"DRGLOTNO4\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"DRGDUEDT4\" type=\"xs:dateTime\" minOccurs=\"0\" /><xs:element name=\"DRGLOTNO5\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"DRGDUEDT5\" type=\"xs:dateTime\" minOccurs=\"0\" /><xs:element name=\"COMPAR2\" type=\"xs:string\" minOccurs=\"0\" /><xs:element name=\"MLEVEL\" type=\"xs:string\" minOccurs=\"0\" /></xs:sequence></xs:complexType></xs:element></xs:choice></xs:complexType></xs:element></xs:schema><diffgr:diffgram xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\" xmlns:diffgr=\"urn:schemas-microsoft-com:xml-diffgram-v1\"><NewDataSet xmlns=\"\"><Temp1 diffgr:id=\"Temp11\" msdata:rowOrder=\"0\"><MCODE>21061</MCODE><FULLNAME>FUSIDIC ACID/FUCIDIN* 250</FULLNAME><SHORTNAME>FUSIDIC ACID/FUCIDIN* 250</SHORTNAME><UDPOS11>D5</UDPOS11><UDPOS12>D5</UDPOS12><UDPOS21>A4-11</UDPOS21><UDPOS22>A4-11</UDPOS22><UDPOS221>A4-11</UDPOS221><UDPOS222>A4-11</UDPOS222><FQCNVOUT2>成水O2,兒水K2,*C6-1,急E3,庫A5</FQCNVOUT2><MFG_COMP>禾利行股份有限公司 </MFG_COMP><MFG_NAME> LABORATOIRES LEO, S.A.</MFG_NAME><LICENSE>衛署藥輸字第019108號</LICENSE></Temp1></NewDataSet></diffgr:diffgram></Drug_DATAResult></Drug_DATAResponse></soap:Body></soap:Envelope>";

            string[] str_array = new string[] { "soap:Body" , "Drug_DATAResponse" , "Drug_DATAResult" , "diffgr:diffgram", "NewDataSet" , "Temp1" , "MCODE" };

            // XmlElement xmlElement = str.Xml_GetElement(str_array);
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(str);

            //XmlElement xmlElement = doc.DocumentElement;
            //for (int i = 0; i < str_array.Length; i++)
            //{
            //    xmlElement = xmlElement[str_array[i]];

            //}
            //XmlNodeList xmlNodeList = xmlElement.ChildNodes;
            // string MCODE = doc.DocumentElement["soap:Body"]["Drug_DATAResponse"]["Drug_DATAResult"]["diffgr:diffgram"]["NewDataSet"]["Temp1"]["MCODE"].InnerXml;


            string MCODE = str.Xml_GetInnerXml(str_array);
        }
    }
}
