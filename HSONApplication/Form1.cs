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
using HIS_DB_Lib;
namespace HSONApplication
{
    public partial class Form1 : Form
    {
        DeltaMotor485.Port DeltaMotor485_port = new DeltaMotor485.Port();
        MySerialPort mySerialPort_delta = new MySerialPort();
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

            mySerialPort_delta.Init("COM6", 38400, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One);
            plC_UI_Init1.Add_Method(sub_program);
            this.rJ_Button_UJOG.MouseDownEvent += RJ_Button_UJOG_MouseDownEvent; 
            this.rJ_Button_DJOG.MouseDownEvent += RJ_Button_DJOG_MouseDownEvent;
            this.rJ_Button_JOG_STOP.MouseDownEvent += RJ_Button_JOG_STOP_MouseDownEvent;
            this.rJ_Button_return_home.MouseDownEvent += RJ_Button_return_home_MouseDownEvent;
            this.rJ_Button_set_path_config.MouseDownEvent += RJ_Button_set_path_config_MouseDownEvent;

            this.rJ_Button_Init.MouseDownEvent += RJ_Button_Init_MouseDownEvent;

            this.rJ_Button_Servo_ON.MouseDownEvent += RJ_Button_Servo_ON_MouseDownEvent;
            this.rJ_Button_Servo_OFF.MouseDownEvent += RJ_Button_Servo_OFF_MouseDownEvent;
        }
        private void RJ_Button_Init_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port.Init(mySerialPort_delta, new byte[] {1 });
            DeltaMotor485_port[1].flag_Init = true;
        }

        private void sub_program()
        {
            if(DeltaMotor485_port[1] != null)
            {
                plC_NumBox_po.Value = DeltaMotor485_port[1].CommandPosition;
            }
     
        }
        private void RJ_Button_Servo_OFF_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port[1].Servo_on_off(false);
        }
        private void RJ_Button_Servo_ON_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port[1].Servo_on_off(true);
        }

    



        async private void RJ_Button_return_home_MouseDownEvent(MouseEventArgs mevent)
        {
            //bool flag = await DeltaMotor485.Communication.Home(mySerialPort_delta, 1, DeltaMotor485.enum_Direction.CW, true, 1000, 10, 50, 50, 10000, 500, 50);
            DeltaMotor485_port[1].Home(DeltaMotor485.enum_Direction.CW, true, 1000, 10, 50, 50, 10000, 500, 50);
        }

        private void RJ_Button_JOG_STOP_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port[1].Stop();
        }

        private void RJ_Button_DJOG_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port[1].JOG(-100);
        }

        private void RJ_Button_UJOG_MouseDownEvent(MouseEventArgs mevent)
        {
            DeltaMotor485_port[1].JOG(100);
        }
        async private void RJ_Button_set_path_config_MouseDownEvent(MouseEventArgs mevent)
        {
            await DeltaMotor485_port[1].DDRVA(500000, 1000, 200);
            await DeltaMotor485_port[1].DDRVA(0, 1000, 200);
        }


        private void PlC_UI_Init1_UI_Finished_Event()
        {
            PLC_UI_Init.Set_PLC_ScreenPage(panel_main, this.plC_ScreenPage_main);
            //string url = $"http://220.135.128.247:4433/api/ChemotherapyRxScheduling/init_udnoectc";
            //returnData returnData = new returnData();
            //returnData.ServerName = "cheom";
            //returnData.ServerType = "癌症備藥機";
            //string json_in = returnData.JsonSerializationt();
            //string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            //List<Table> tables = json.JsonDeserializet<List<Table>>();

            //this.sqL_DataGridView_備藥通知.Init(tables[0]);
            //this.sqL_DataGridView_備藥通知.Set_ColumnVisible(false, new enum_udnoectc().GetEnumNames());

            //this.sqL_DataGridView_備藥通知.Set_ColumnWidth(150, enum_udnoectc.病房);
            //this.sqL_DataGridView_備藥通知.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_udnoectc.病歷號);
            //this.sqL_DataGridView_備藥通知.Set_ColumnWidth(150, DataGridViewContentAlignment.MiddleLeft, enum_udnoectc.診別);
            //this.sqL_DataGridView_備藥通知.Set_ColumnWidth(500, DataGridViewContentAlignment.MiddleLeft, enum_udnoectc.RegimenName);
            //this.sqL_DataGridView_備藥通知.Set_ColumnSortMode(DataGridViewColumnSortMode.Automatic, enum_udnoectc.病歷號);
            //this.sqL_DataGridView_備藥通知.DataGridRefreshEvent += SqL_DataGridView_備藥通知_DataGridRefreshEvent;
            //Function_取得備藥通知();
        }

        private void SqL_DataGridView_備藥通知_DataGridRefreshEvent()
        {
            for (int i = 0; i < this.sqL_DataGridView_備藥通知.dataGridView.Rows.Count; i++)
            {
                this.sqL_DataGridView_備藥通知.dataGridView.Rows[i].Cells["病歷號"].Value = $"{i}S";
            }
        }

        private void Function_取得備藥通知()
        {
            string url = $"http://220.135.128.247:4433/api/ChemotherapyRxScheduling/get_udnoectc_by_ctdate_st_end";
            returnData returnData = new returnData();
            returnData.ServerName = "cheom";
            returnData.ServerType = "癌症備藥機";
            returnData.Value = "2023-11-23 00:00:00,2023-11-24 23:59:59";
            string json_in = returnData.JsonSerializationt();
            string json = Basic.Net.WEBApiPostJson($"{url}", json_in);
            returnData = json.JsonDeserializet<returnData>();

            List<udnoectc> udnoectcs = returnData.Data.ObjToListClass<udnoectc>();
            List<object[]> list_udnoectc = udnoectcs.ClassToSQL<udnoectc, enum_udnoectc>();
            this.sqL_DataGridView_備藥通知.RefreshGrid(list_udnoectc);

        }

        private void sqL_DataGridView_備藥通知_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
