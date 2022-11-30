using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using Basic;
namespace TCP_Server
{
    static public class ChatSetting
    {
        public static String serverIp = "192.168.100.1";
        public static int port = 8080;
        public static int UDP_remote_prot = 8080;
        public static int UDP_local_prot = 8080;
        public static TCPorUDP 網路協議方式 = new TCPorUDP();
    }
    
    public enum TCPorUDP:int
    {
        TCP =0,UDP
    }
 

    public delegate void StrHandler(String str);
    public class ChatServer
    {
        public bool Server已建立 = false;
        public IPHostEntry iphostentry;
        private String strHostName;
        private List<ChatSocket> clientList = new List<ChatSocket>();
        private List<String> ReciveData = new List<String>();
        private bool RefreshUI = false;

        private Socket ListenSocket;

        private ManualResetEvent ThreadDead_ListenProcess, ThreadTrigger_ListenProcess;
        private ManualResetEvent ThreadDead_CommStatus, ThreadTrigger_CommStatus;
        private BackgroundWorker BackgroundWorker_ListenProcess = new BackgroundWorker();
        private BackgroundWorker BackgroundWorker_CommStatus = new BackgroundWorker();

        private RichTextBox richTextBox;
        private delegate void RichTextBox_NewLine(String str);
        private void RichTextBox_NewLine_UI(String str)
        {
            if (richTextBox != null && RefreshUI)
            {
               
                str.Replace("\n", "");
                if (richTextBox.InvokeRequired)
                {

                    RichTextBox_NewLine myUpdate = new RichTextBox_NewLine(RichTextBox_NewLine_UI);
                    richTextBox.Invoke(myUpdate, str);
                }
                else
                {
                   // String Time_Short = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    if (richTextBox.Lines.Length >= 1000) richTextBox.Clear();
                    richTextBox.AppendText(str + "\n");
                    richTextBox.SelectionStart = richTextBox.Text.Length;
                    richTextBox.ScrollToCaret();
                }
            }
        }     
        public ChatServer()
        {

        }
        public ChatServer(RichTextBox richTextBox)
        {
            this.richTextBox = richTextBox;
        }

        public void SetRefreshUI(bool Enable)
        {
            RefreshUI = Enable;
        }
        public void Server開始執行()
        {
            ThreadTrigger_CommStatus = new ManualResetEvent(false);
            ThreadDead_CommStatus = new ManualResetEvent(false);
            ThreadTrigger_ListenProcess = new ManualResetEvent(false);
            ThreadDead_ListenProcess = new ManualResetEvent(false);

            strHostName = Dns.GetHostName();                                     //先讀取本機名稱
            iphostentry = Dns.GetHostByName(strHostName);                   //取得本機的 IpHostEntry 類別實體
            this.BackgroundWorker_ListenProcess.WorkerSupportsCancellation = true;
            this.BackgroundWorker_ListenProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DoWork_Listening);
            if (!BackgroundWorker_ListenProcess.IsBusy) { BackgroundWorker_ListenProcess.RunWorkerAsync(); RichTextBox_NewLine_UI("Server 開始監聽!"); }
            else RichTextBox_NewLine_UI("Server 程序已執行中!");

            this.BackgroundWorker_CommStatus.WorkerSupportsCancellation = true;
            this.BackgroundWorker_CommStatus.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DoWork_CommStatus);
            if (!BackgroundWorker_CommStatus.IsBusy) { BackgroundWorker_CommStatus.RunWorkerAsync(); }
            Server已建立 = true;
        }
        /* public void Server停止執行()
        {
            ThreadDead_ListenProcess.Set();
            ThreadTrigger_ListenProcess.Set();
            ThreadDead_CommStatus.Set();
            ThreadTrigger_CommStatus.Set();
            ListenSocket.Close();
            clientList.Clear();
            Server已建立 = false;
        }*/
        private void DoWork_Listening(object sender, DoWorkEventArgs e)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, ChatSetting.port);
            ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ListenSocket.Bind(ipep);
            ListenSocket.Listen(10);
            RichTextBox_NewLine_UI("Hostname : " + strHostName);
            foreach (IPAddress ip in iphostentry.AddressList)
            {
                RichTextBox_NewLine_UI("IPAddress : " + ip.ToString());
            }
            RichTextBox_NewLine_UI("Port : " + ChatSetting.port);

            while (!ThreadDead_ListenProcess.WaitOne(0))
            {
                ThreadTrigger_ListenProcess.Set();
                ThreadTrigger_ListenProcess.WaitOne();
                Socket socket = ListenSocket.Accept();
                ChatSocket client = new ChatSocket(socket);
                RichTextBox_NewLine_UI(client.GetEndPoint().ToString() + " 已連上!");
                try
                {
                    client.模式選擇 = ChatSocket.模式.主機模式;
                    clientList.Add(client);
                    client.newListener(processMsgComeIn);
                }
                catch
                {

                }
                ThreadTrigger_ListenProcess.Reset();
            }

        }
        private void DoWork_CommStatus(object sender, DoWorkEventArgs e)
        {
            while (!ThreadDead_CommStatus.WaitOne(0))
            {
                ThreadTrigger_CommStatus.Set();
                ThreadTrigger_CommStatus.WaitOne();
                foreach (ChatSocket client in clientList)
                {
                    if (!client.CommTest())
                    {
                        RichTextBox_NewLine_UI(client.GetEndPoint().ToString() + ":" + "通訊斷線!");
                        client.CilentDispose();
                        clientList.Remove(client);
                        break;                       
                    }
               
                }
                Thread.Sleep(100);
                ThreadTrigger_CommStatus.Reset();
            }
        }
        private void processMsgComeIn(string msg)
        {
            RichTextBox_NewLine_UI(msg);
            ReciveData.Add(msg);                    
           //broadCast(msg);
        }
        public void Writeline(string line)
        {
            foreach(ChatSocket chatSocket in clientList)
            {
                chatSocket.Writeline(line);
            }
        }
        public bool Readline(ref string line)
        {
            if (ReciveData.Count > 0)
            {
                line = ReciveData[0];
                ReciveData.RemoveAt(0);
                return true;
            }
            else return false;
        }
    }
    public class ChatSocket
    {
        const string CommTestStr = "!CommTest!";
        public String UserName = "";
        private string Recive_str = "";
        public List<String> ReciveData = new List<String>();
        public 模式 模式選擇 = new 模式();
        public bool isDead = false;
        private Encoding encoding = Encoding.UTF8;

        public IPHostEntry iphostentry;
        private bool RefreshUI = false;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        private RichTextBox richTextBox;
        private StrHandler richTextBox_UI;
        private StrHandler inHandler;
        private EndPoint remoteEndPoint;
        private IPEndPoint ipep0;
        private IPEndPoint ipep1;
        private Socket socket_TCP;
        private Socket socket_UDP_server;
        private Socket socket_UDP_cilent;
        private byte[] UDP_pushdata = new byte[16384];
        public byte[] UDP_getdata = new byte[16384];
        public int UDP_recvlen = 0;
        private ManualResetEvent ThreadDead_ListenProcess, ThreadTrigger_ListenProcess;
        private System.ComponentModel.BackgroundWorker BackgroundWorker_Listen = new BackgroundWorker();

        public enum 模式 { 連線者模式, 主機模式 };

        public ChatSocket(Socket serverCilent)
        {
            ChatSetting.網路協議方式 = TCPorUDP.TCP;
            this.socket_TCP = serverCilent;
            stream = new NetworkStream(serverCilent);
           
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.AutoFlush = true;
            remoteEndPoint = socket_TCP.RemoteEndPoint;
        }
        public ChatSocket()
        {
            if (模式選擇 == 模式.連線者模式) isDead = true;
        }
        public void SetRefreshUI(bool Enable)
        {
            RefreshUI = Enable;
        }
        public String GetIP()
        {
            String IPAdress = IPAddress.Parse(((IPEndPoint)remoteEndPoint).Address.ToString()).ToString();
            return ChatSetting.serverIp;
        }
        public String GetPort()
        {
            String Port = IPAddress.Parse(((IPEndPoint)remoteEndPoint).Port.ToString()).ToString();
            return ChatSetting.port.ToString();
        }
        public EndPoint GetEndPoint()
        {
            return this.remoteEndPoint;
        }
        Thread UDP_Thread; 
        private bool readline(ref String line)
        {
            if(UDP_Thread == null)UDP_Thread = Thread.CurrentThread;
            if (ChatSetting.網路協議方式 == TCPorUDP.TCP)
            {
                if (IsSockeALive() && !isDead)
                {
                    if (stream.DataAvailable)
                    {
                        line = reader.ReadLine();
                   
                        if (line != CommTestStr)
                        {
                            inHandler(line);
                        }
                    }
                    return true;
                }
            }
            else if (ChatSetting.網路協議方式 == TCPorUDP.UDP)
            {
                if (remoteEndPoint == null)
                {
                    IPEndPoint IPEnd = new IPEndPoint(IPAddress.Any, ChatSetting.UDP_local_prot);
                    remoteEndPoint = (EndPoint)IPEnd;
                }
                UDP_recvlen = socket_UDP_cilent.ReceiveFrom(UDP_getdata, ref remoteEndPoint); //把接收的封包放進getdata且傳回大小存入recv
                line = encoding.GetString(UDP_getdata, 0, UDP_recvlen); //把接收的byte資料轉回string型態
         
                inHandler(line);
                }
            return false;
        }
        public void SetEncoding(Encoding encoding)
        {
            this.encoding = encoding;
        }
        public bool Readline(ref String line)
        {
            if (ReciveData.Count > 0)
            {
                line = ReciveData[0];
                ReciveData.RemoveAt(0);
                return true;
            }
            else
            {
                line = "";
                return false;
            }
        }
        public void ClearReadBuf()
        {
            ReciveData.Clear();
        }
        public void Writeline(String line)
        {
            if (ChatSetting.網路協議方式 == TCPorUDP.TCP)
            {
                if (writer != null)
                {
                    writer.WriteLine(line);
                    writer.Flush();

                }
            }
            else if (ChatSetting.網路協議方式 == TCPorUDP.UDP)
            {
                UDP_pushdata = encoding.GetBytes(line); //把要送出的資料轉成byte型態
                socket_UDP_server.SendTo(UDP_pushdata, ipep0); //送出的資料跟目的
                socket_UDP_server.SendTo(UDP_pushdata, ipep1); //送出的資料跟目的
            }
        }
        public void WriteChar(char[] value)
        {
            if (ChatSetting.網路協議方式 == TCPorUDP.TCP)
            {
                writer.Write(value);
                writer.Flush();
            }
        }
        public void WriteByte(byte[] value)
        {
            if (ChatSetting.網路協議方式 == TCPorUDP.TCP)
            {
                writer.Write(value);
                writer.Flush();
            }
            else if (ChatSetting.網路協議方式 == TCPorUDP.UDP)
            {

                socket_UDP_server.SendTo(value, ipep0); //送出的資料跟目的
                socket_UDP_server.SendTo(value, ipep1); //送出的資料跟目的
            }
        }
        public bool IsSockeALive()
        {
            if (ChatSetting.網路協議方式 == TCPorUDP.TCP)
            {
                if (socket_TCP != null)
                {
                    return socket_TCP.Connected;
                }
            }
            else if (ChatSetting.網路協議方式 == TCPorUDP.UDP)
            {
                return true;
            }
            return false;
        }

        public void CreateCilent(string IP, string PORT, string userName)
        {
            ChatSetting.port = Convert.ToInt32(PORT);
            ChatSetting.serverIp = IP;
            ChatSetting.網路協議方式 = TCP_Server.TCPorUDP.TCP;
            this.UserName = userName;
            this.richTextBox_UI = this._RichTextBoxAddMsg;
            newListener(CilentProcessmsgComeIn);

            string strHostName = Dns.GetHostName();                         //先讀取本機名稱
            iphostentry = Dns.GetHostByName(strHostName);                   //取得本機的 IpHostEntry 類別實體

        }
        public void CreateCilent(string IP, string remote_prot,string local_prot, string userName)
        {
            ChatSetting.UDP_remote_prot = Convert.ToInt32(remote_prot);
            ChatSetting.UDP_local_prot = Convert.ToInt32(local_prot);
            ChatSetting.serverIp = IP;
            ChatSetting.網路協議方式 = TCPorUDP.UDP;
            this.UserName = userName;
            this.richTextBox_UI = this._RichTextBoxAddMsg;
            newListener(CilentProcessmsgComeIn);

            string strHostName = Dns.GetHostName();                         //先讀取本機名稱
            iphostentry = Dns.GetHostByName(strHostName);                   //取得本機的 IpHostEntry 類別實體

        }
        public void SetRichTextBox(RichTextBox richTextBox)
        {
            this.richTextBox = richTextBox;
            inHandler += new StrHandler(RichTextBoxAddMsg);
        }
        private void RichTextBoxAddMsg(String msg)
        {
            if (richTextBox.IsHandleCreated) if (richTextBox != null) richTextBox.Invoke(richTextBox_UI, new Object[] { msg });
        }
        private void _RichTextBoxAddMsg(String msg)
        {
            if (RefreshUI)
            {
                richTextBox.AppendText(msg + "\n");
                if (richTextBox.Lines.Length >= 1000) richTextBox.Clear();
                richTextBox.SelectionStart = richTextBox.Text.Length;
                richTextBox.ScrollToCaret();
            }

        }
        private void CilentProcessmsgComeIn(String line)
        {
            ReciveData.Add(line);
        }

        public bool ConnectToServer()
        {
            bool 連線成功 = false;
            DateTime Date = DateTime.Now;
            String Time_Short = Date.Year.ToString() + Date.Month.ToString() + Date.Day.ToString() + "-" + Date.Hour.ToString() + Date.Minute.ToString() + Date.Second.ToString();
            try
            {
               
                if (ChatSetting.網路協議方式 == TCPorUDP.TCP)
                {
                    ipep0 = new IPEndPoint(IPAddress.Parse(ChatSetting.serverIp), ChatSetting.port);
                    inHandler(Time_Short + " : 嘗試連線到...." + ChatSetting.serverIp + ":" + ChatSetting.port);         
                    socket_TCP = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket_TCP.Connect(ipep0);
                    stream = new NetworkStream(socket_TCP);
                    reader = new StreamReader(stream);
                    writer = new StreamWriter(stream);
                    remoteEndPoint = socket_TCP.RemoteEndPoint;
                    inHandler(Time_Short + " : 連線成功!");
                }
                else if (ChatSetting.網路協議方式 == TCPorUDP.UDP)
                {
                    ipep0 = new IPEndPoint(IPAddress.Parse(ChatSetting.serverIp), ChatSetting.UDP_remote_prot);
                    ipep1 = new IPEndPoint(IPAddress.Parse(ChatSetting.serverIp), ChatSetting.UDP_remote_prot + 1);
                    socket_UDP_server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); //定義發送的格式及有效區域
                    socket_UDP_server.EnableBroadcast = true;
                    socket_UDP_server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);

                    IPEndPoint IPEnd = new IPEndPoint(IPAddress.Any, ChatSetting.UDP_local_prot);
                    remoteEndPoint = (EndPoint)IPEnd;
                    socket_UDP_cilent = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    socket_UDP_cilent.Bind(IPEnd);
                
                }
                連線成功 = true;
            }
            catch { 連線成功 = false; }
            return 連線成功;
        }
        public void Close()
        {
            if (UDP_Thread != null) UDP_Thread.Abort();
            if (UDP_Thread != null) socket_UDP_cilent.Close();
        }
        public void CilentDispose()
        {
            ThreadDead_ListenProcess.Set();
            ThreadTrigger_ListenProcess.Set();
            if (socket_TCP != null) socket_TCP.Close();
            if (stream != null) stream.Close(); 
            socket_TCP = null;
            isDead = true;
        }
        public bool CommTest()
        {
            try
            {
                writer.WriteLine(CommTestStr);
                writer.Flush();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void newListener(StrHandler pHandler)
        {
            inHandler += pHandler;
            ThreadDead_ListenProcess = new ManualResetEvent(false);
            ThreadTrigger_ListenProcess = new ManualResetEvent(false);
            this.BackgroundWorker_Listen.WorkerSupportsCancellation = true;
            this.BackgroundWorker_Listen.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Listen);
            if (!BackgroundWorker_Listen.IsBusy) { BackgroundWorker_Listen.RunWorkerAsync(); }

        }
        private void Listen(object sender, DoWorkEventArgs e)
        {
            while (!ThreadDead_ListenProcess.WaitOne(0))
            {
                ThreadTrigger_ListenProcess.Set();
                ThreadTrigger_ListenProcess.WaitOne();

                if (isDead && 模式選擇 == 模式.連線者模式)
                {
                    isDead = !ConnectToServer();
                    Thread.Sleep(100);
                }
                if (ChatSetting.網路協議方式 == TCPorUDP.TCP)
                {
                    if (socket_TCP != null && !isDead)
                    {
                        if (!socket_TCP.Connected) isDead = true;
                        Recive_str = "";
                        readline(ref Recive_str);
                    }
                }
                else if (ChatSetting.網路協議方式 == TCPorUDP.UDP)
                {
                    Recive_str = "";
                    readline(ref Recive_str);
                }
                ThreadTrigger_ListenProcess.Reset();
            }             
        }

    }
    public class SoketFile
    {
        
        private const int BufferSize = 4096;

        private const string 命令空格 = "∰∰";
        private const string 清除封包 = "**CleanPacket";
        private const string 確認 = "**OK";
        private const string 完成 = "**Finish";
        private const string 開始發送封包 = "**StartSendPacket";
        private const string 確認長度 = "**CheckPaketLenth";
        private const string 等待封包發送致能 = "**PaketWaitON";
        private const string 等待封包發送不致能 = "**PaketWaitOFF";
        private const string 等待封包逾時 = "**PaketOverTimeOut";
        private const string 封包全部發送完成 = "**PaketOver";
        private const string 封包檢查 = "**PaketCheck";
        private const string 確認封包 = "**PaketConfirm";
        private const string 創建檔案 = "**CreatFile";
        private const string 更新命令 = "**UpdateCommand";
        private const string 錯誤 = "**ERROR";

        public class Sender
        {
            public string FileName = "temp.exe";
            public string StateText = "";

            private Basic.MyConvert MyConvert = new Basic.MyConvert();
            private string[] CtrrentReceiveData;
            private List<string> ReceiveDataList = new List<string>();
            private List<byte[]> SendPaketList = new List<byte[]>();
            private List<byte> ReceivePaket = new List<byte>();
            private byte[] FileData;
            private bool SenderTrigger = false;
            private ChatSocket Soket;
            private MyUI.ExButton ExbuttonSend;
            public bool PaketWait = false;
            public Sender(ChatSocket chatSocket, MyUI.ExButton ExbuttonSend)
            {
                this.ExbuttonSend = ExbuttonSend;
                this.Soket = chatSocket;
            }
            public void AddReceiveData(string Data)
            {
                ReceiveDataList.Add(Data);
            }
            private byte cnt_Sender = 255;
            private LadderProperty.DEVICE Timer = new LadderProperty.DEVICE(0, 0, 0, 0, 2, 0, 0, true);
            private bool FLAG_接收逾時 = false;
            private bool FLAG_封包接收逾時 = false;
            private int 重新發送次數現在值 = 0;
            private int 重新發送次數設定值 = 5;
            private int 封包組大小現在值 = 0;
            private int 封包組大小設定值 = 0;
            private int 逾時時間= 1000;
            private int 封包接收時間 = 1000;
            private int 略過開頭封包 = 0;

            public bool Run()
            {
                if (ExbuttonSend.Load_WriteState())
                {
                    if (!SenderTrigger)
                    {
                        cnt_Sender = 1;
                        SenderTrigger = true;
                    }
                }
                ExbuttonSend.Set_LoadState(cnt_Sender != 255);

                ReceiveDataProcess();
                if (cnt_Sender == 1) sub_Sender_00_初始化(ref cnt_Sender);
                if (cnt_Sender == 2) sub_Sender_00_FileData拆分(ref cnt_Sender);              
                if (cnt_Sender == 3) cnt_Sender = 10;

                if (cnt_Sender == 10) sub_Sender_10_清除封包_檢查重新發送次數(ref cnt_Sender);
                if (cnt_Sender == 11) sub_Sender_10_清除封包_發送(ref cnt_Sender);
                if (cnt_Sender == 12) sub_Sender_10_清除封包_等待接收(ref cnt_Sender);
                if (cnt_Sender == 13) cnt_Sender = 20;

                if (cnt_Sender == 20) sub_Sender_20_發送封包_檢查封包組發送完成(ref cnt_Sender);
                if (cnt_Sender == 21) cnt_Sender = 25;


                if (cnt_Sender == 25) sub_Sender_25_發送封包_檢查重新發送次數(ref cnt_Sender);
                if (cnt_Sender == 26) sub_Sender_25_發送封包_封包致能_發送(ref cnt_Sender);
                if (cnt_Sender == 27) sub_Sender_25_發送封包_封包致能_等待接收(ref cnt_Sender);
                if (cnt_Sender == 28) sub_Sender_25_發送封包_封包組_發送(ref cnt_Sender);
                if (cnt_Sender == 29) sub_Sender_25_發送封包_封包組_等待完成(ref cnt_Sender);
                if (cnt_Sender == 30) sub_Sender_25_發送封包_封包檢查_發送(ref cnt_Sender);
                if (cnt_Sender == 31) sub_Sender_25_發送封包_封包檢查_等待接收(ref cnt_Sender);
                if (cnt_Sender == 32) sub_Sender_25_發送封包_封包確認_發送(ref cnt_Sender);
                if (cnt_Sender == 33) sub_Sender_25_發送封包_封包確認_等待接收(ref cnt_Sender);
                if (cnt_Sender == 34) cnt_Sender = 20;

                if (cnt_Sender == 40) sub_Sender_40_創建檔案_檢查重新發送次數(ref cnt_Sender);
                if (cnt_Sender == 41) sub_Sender_40_創建檔案_發送(ref cnt_Sender);
                if (cnt_Sender == 42) sub_Sender_40_創建檔案_等待接收(ref cnt_Sender);
                if (cnt_Sender == 43) cnt_Sender = 50;

                if (cnt_Sender == 50) sub_Sender_50_更新命令_檢查重新發送次數(ref cnt_Sender);
                if (cnt_Sender == 51) sub_Sender_50_更新命令_發送(ref cnt_Sender);
                if (cnt_Sender == 52) sub_Sender_50_更新命令_等待接收(ref cnt_Sender);
                if (cnt_Sender == 53) cnt_Sender = 250;

                if (cnt_Sender == 200)
                {
                    StateText = "失敗!";
                    cnt_Sender = 255;
                }
                if (cnt_Sender == 250)
                {
                    StateText = "成功!";
                    cnt_Sender = 255;
                }             
                if (cnt_Sender == 255)
                {
                    if (!ExbuttonSend.Load_WriteState() && SenderTrigger)
                    {
                        SenderTrigger = false;
                        PaketWait = false;
                    }
                }

                if (cnt_Sender == 1 && cnt_Sender < 10) StateText = "讀取檔案...";
                else if (cnt_Sender == 10 && cnt_Sender < 20) StateText = "發送清除封包命令...";
                else if (cnt_Sender == 20 && cnt_Sender < 40)
                {
                    if (封包組大小現在值 < SendPaketList.Count)
                    {
                        StateText = "封包傳送 : " + "Size " + SendPaketList[封包組大小現在值].Length + "," + 封包組大小現在值 + "/" + 封包組大小設定值;
                    }
                }
                else if (cnt_Sender == 40 && cnt_Sender < 50) StateText = "發送創建檔案命令...";
                else if (cnt_Sender == 50 && cnt_Sender < 60) StateText = "發送更新命令...";
             
                if (cnt_Sender != 255) return true;
                else return false;
            }
            private void ReceiveDataProcess()
            {
                if (ReceiveDataList.Count > 0)
                {
                    CtrrentReceiveData = MyConvert.分解分隔號字串(ReceiveDataList[0], 命令空格);              
                    ReceiveDataList.RemoveAt(0);
                }
            }
            private void sub_設置逾時Timer()
            {
                Timer.Set_Device("T0", 逾時時間);
                FLAG_接收逾時 = false;
            }
            private void sub_取得T0狀態()
            {
                object obj_temp = new object();
                Timer.Set_Device("T0", true);
                Timer.Get_Device("T0", out obj_temp);
                FLAG_接收逾時 = (bool)obj_temp;
            }
            private void sub_設置接收封包逾時Timer()
            {
                Timer.Set_Device("T1", 封包接收時間);
                FLAG_封包接收逾時 = false;
            }
            private void sub_取得T1狀態()
            {
                object obj_temp = new object();
                Timer.Set_Device("T1", true);
                Timer.Get_Device("T1", out obj_temp);
                FLAG_封包接收逾時 = (bool)obj_temp;
            }
            private void sub_Sender_00_初始化(ref byte cnt)
            {
           
                if (FileName == "" || FileName == null)
                {
                    cnt = 200;
                }
                else
                {
                    SendPaketList.Clear();
                   
                    FileData = File.ReadAllBytes(FileName);
                    重新發送次數現在值 = 0;
                    cnt++;
                }      
            }
            private void sub_Sender_00_FileData拆分(ref byte cnt)
            {
                int div = FileData.Length / BufferSize;
                int lenth;
                List<byte> buffer_temp = new List<byte>();
                for (int i = 0; i <= div; i++)
                {              
                    for (int k = 0; k < BufferSize; k++)
                    {
                        lenth = i * BufferSize + k;
                        if (lenth < FileData.Length) buffer_temp.Add(FileData[lenth]);          
                    }
                    SendPaketList.Add(buffer_temp.ToArray());
                    buffer_temp.Clear();
                }
                cnt++;
            }

            private void sub_Sender_10_清除封包_檢查重新發送次數(ref byte cnt)
            {
                if (重新發送次數現在值 <= 重新發送次數設定值) cnt++;
                else cnt = 200;
            }
            private void sub_Sender_10_清除封包_發送(ref byte cnt)
            {
                Soket.Writeline(清除封包);
                sub_設置逾時Timer();
                cnt++;
            }
            private void sub_Sender_10_清除封包_等待接收(ref byte cnt)
            {
                sub_取得T0狀態();
                if (FLAG_接收逾時)
                {
                    重新發送次數現在值++;
                    cnt = 10;  
                    return;
                }
                if (CtrrentReceiveData != null)
                {
                    if (CtrrentReceiveData[0] == 完成)
                    {
                        CtrrentReceiveData = null;
                        封包組大小設定值 = SendPaketList.Count;
                        封包組大小現在值 = 0;
                        重新發送次數現在值 = 0;
                        cnt++;
                    }
                }           
            }

            private void sub_Sender_20_發送封包_檢查封包組發送完成(ref byte cnt)
            {
                if (封包組大小現在值 < 封包組大小設定值)
                {           
                    cnt++;
                }
                else cnt = 40;
                重新發送次數現在值 = 0;
            }

            private void sub_Sender_25_發送封包_檢查重新發送次數(ref byte cnt)
            {
                if (重新發送次數現在值 <= 重新發送次數設定值) cnt++;
                else cnt = 200;
            }
            private void sub_Sender_25_發送封包_封包致能_發送(ref byte cnt)
            {
                Soket.Writeline(等待封包發送致能 + 命令空格 + SendPaketList[封包組大小現在值].Length.ToString());
                sub_設置逾時Timer();
                cnt++;
            }
            private void sub_Sender_25_發送封包_封包致能_等待接收(ref byte cnt)
            {
                sub_取得T0狀態();
                if (FLAG_接收逾時)
                {
                    重新發送次數現在值++;
                    cnt = 25;
                    return;
                }
                if (CtrrentReceiveData != null)
                {
                    if (CtrrentReceiveData[0] == 完成)
                    {
                        CtrrentReceiveData = null;       
                        cnt++;
                    }
                }  
            }
            private void sub_Sender_25_發送封包_封包組_發送(ref byte cnt)
            {
                Soket.WriteByte(SendPaketList[封包組大小現在值].ToArray());
                sub_設置逾時Timer();
                cnt++;
            }
            private void sub_Sender_25_發送封包_封包組_等待完成(ref byte cnt)
            {
                sub_取得T0狀態();
                if (FLAG_接收逾時)
                {
                 
                    重新發送次數現在值++;
                    cnt = 25;
                    return;
                }
                if (CtrrentReceiveData != null)
                {
                    if (CtrrentReceiveData[0] == 完成)
                    {
                        CtrrentReceiveData = null;
                        cnt++;
                    }
                } 
            }
            private void sub_Sender_25_發送封包_封包檢查_發送(ref byte cnt)
            {
                cnt++;
                return;
                Soket.Writeline(封包檢查);
                ReceivePaket.Clear();
                略過開頭封包 = 0;
                sub_設置接收封包逾時Timer();               
                cnt++;
            }
            private void sub_Sender_25_發送封包_封包檢查_等待接收(ref byte cnt)
            {
                cnt++;
                return;
                sub_取得T1狀態();
                if (FLAG_封包接收逾時)
                {
                    重新發送次數現在值++;
                    cnt = 25;
                    return;
                }
                if (CtrrentReceiveData != null)
                {
                    if (SendPaketList[封包組大小現在值].Length > 0)
                    {
                        byte[] Paket_tmep = new byte[Soket.UDP_recvlen];
                        for (int i = 0; i < Soket.UDP_recvlen; i++)
                        {
                           Paket_tmep[i] = Soket.UDP_getdata[i];
                        }
                        for (int i = 0; i < Paket_tmep.Length; i++)
                        {
                            bool flag_NG = false;
                            if (i == 0 && Paket_tmep[i] != 63) flag_NG = true;
                            else if (i == 1 && Paket_tmep[i] != 63) flag_NG = true;
                            if (!flag_NG)
                            {
                                ReceivePaket.Add(Paket_tmep[i]);
                            }
                        }
                        if (ReceivePaket.Count >= SendPaketList[封包組大小現在值].Length)
                        {
                            bool flag_NG = false;
                            if (ReceivePaket.Count != SendPaketList[封包組大小現在值].Length) flag_NG = true;

                            if (!flag_NG)
                            {
                                for (int i = 0; i < ReceivePaket.Count; i++)
                                {
                                    if (ReceivePaket[i] != SendPaketList[封包組大小現在值][i])
                                    {
                                        flag_NG = true;
                                    }
                                }
                            }
                        
                            if (!flag_NG)
                            {
                                cnt++;
                            }
                            else
                            {
                                重新發送次數現在值++;
                                cnt = 25;
                            }                      
                        }
                    }
                    else
                    {
                        cnt++;
                    }
                } 
            }
            private void sub_Sender_25_發送封包_封包確認_發送(ref byte cnt)
            {
    
                Soket.Writeline(確認封包);
                sub_設置逾時Timer();
                cnt++;
            }
            private void sub_Sender_25_發送封包_封包確認_等待接收(ref byte cnt)
            {
                sub_取得T0狀態();
                if (FLAG_接收逾時)
                {
                    重新發送次數現在值++;
                    cnt = 25;
                    return;
                }
                if (CtrrentReceiveData != null )
                {
                    if (CtrrentReceiveData[0] == 完成 )
                    {
                        封包組大小現在值++;
                        重新發送次數現在值 = 0;
                        CtrrentReceiveData = null;
                        cnt++;
                    }
                } 
            }

            private void sub_Sender_40_創建檔案_檢查重新發送次數(ref byte cnt)
            {
                if (重新發送次數現在值 <= 重新發送次數設定值) cnt++;
                else cnt = 200;
            }
            private void sub_Sender_40_創建檔案_發送(ref byte cnt)
            {
                Soket.Writeline(創建檔案);
                sub_設置逾時Timer();
                cnt++;
            }
            private void sub_Sender_40_創建檔案_等待接收(ref byte cnt)
            {
                sub_取得T0狀態();
                if (FLAG_接收逾時)
                {
                    重新發送次數現在值++;
                    cnt = 40;
                    return;
                }
                if (CtrrentReceiveData != null)
                {
                    if (CtrrentReceiveData[0] == 完成)
                    {
                        重新發送次數現在值 = 0;
                        CtrrentReceiveData = null;
                        cnt++;
                    }
                }
            }

            private void sub_Sender_50_更新命令_檢查重新發送次數(ref byte cnt)
            {
                if (重新發送次數現在值 <= 重新發送次數設定值) cnt++;
                else cnt = 200;
            }
            private void sub_Sender_50_更新命令_發送(ref byte cnt)
            {
                Soket.Writeline(更新命令);
                sub_設置逾時Timer();
                cnt++;
            }
            private void sub_Sender_50_更新命令_等待接收(ref byte cnt)
            {
                sub_取得T0狀態();
                if (FLAG_接收逾時)
                {
                    重新發送次數現在值++;
                    cnt = 50;
                    return;
                }
                if (CtrrentReceiveData != null)
                {
                    if (CtrrentReceiveData[0] == 完成)
                    {
                        CtrrentReceiveData = null;
                        cnt++;
                    }
                }
            }

            private void sub_Sender_(ref byte cnt)
            {
                cnt++;
            }
        }
        public class Receiver
        {
            public string FileName = "temp.exe";
            private Basic.MyConvert MyConvert = new Basic.MyConvert();       
            private int NumOfCurrentPake = 0;

            private LadderProperty.DEVICE Timer = new LadderProperty.DEVICE(0, 0, 0, 0, 2, 0, 0, true);
            private string[] CtrrentReceiveData;
            private List<string> ReceiveDataList = new List<string>();
            private List<byte> CurrentPaket = new List<byte>();     
            private List<byte[]> ReceivePaketList = new List<byte[]>();
            private List<byte> AllPaket = new List<byte>();
            public Receiver(ChatSocket chatSocket)
            {
                this.Soket = chatSocket;
            }
            private ChatSocket Soket = new ChatSocket();
            public bool PaketWait = false;
            public void AddReceiveData(string Data)
            {
                ReceiveDataList.Add(Data);
            }
            private byte cnt_Receiver = 1;
            private bool FLAG_封包接收逾時 = false;
            private int 封包接收時間 = 1000;
            private void sub_設置接收封包逾時Timer()
            {
                Timer.Set_Device("T1", 封包接收時間);
                FLAG_封包接收逾時 = false;
            }
            private void sub_取得T1狀態()
            {
                object obj_temp = new object();
                Timer.Set_Device("T1", true);
                Timer.Get_Device("T1", out obj_temp);
                FLAG_封包接收逾時 = (bool)obj_temp;
            }
            private byte[] GetAllPaket()
            {
                AllPaket.Clear();
                foreach (byte[] byte_temp in ReceivePaketList)
                {
                    for(int i = 0 ; i < byte_temp.Length ; i++)
                    {
                        AllPaket.Add(byte_temp[i]);
                    }
                }
                return AllPaket.ToArray(); 
            }

            public void Run()
            {
                if (cnt_Receiver == 255)
                {                    
                    cnt_Receiver = 1;
                }
                if (cnt_Receiver == 1) sub_Receiver_00_檢查有無訊息(ref cnt_Receiver);
                if (cnt_Receiver == 2) cnt_Receiver = 10;

                if (cnt_Receiver == 10) sub_Receiver_10_解析收到的訊息(ref cnt_Receiver);
                if (cnt_Receiver == 11) cnt_Receiver = 200;

                if (cnt_Receiver == 15) sub_Receiver_15_清除封包(ref cnt_Receiver);
                if (cnt_Receiver == 16) sub_Receiver_16_等待封包發送致能(ref cnt_Receiver);
                if (cnt_Receiver == 17) sub_Receiver_17_封包檢查(ref cnt_Receiver);
                if (cnt_Receiver == 18) sub_Receiver_18_確認封包(ref cnt_Receiver);
                if (cnt_Receiver == 19) sub_Receiver_19_創建檔案(ref cnt_Receiver);
                if (cnt_Receiver == 20) sub_Receiver_20_更新命令(ref cnt_Receiver);

                if (cnt_Receiver == 200) cnt_Receiver = 255;
                if (cnt_Receiver == 250) cnt_Receiver = 255;
            }
            private void sub_Receiver_00_檢查有無訊息(ref byte cnt)
            {
                if (PaketWait)
                {
                    sub_取得T1狀態();
                    if (FLAG_封包接收逾時)
                    {
                        Soket.Writeline(等待封包逾時);
                        PaketWait = false;
                    }
                }
                if (ReceiveDataList.Count > 0)
                {
                    if (PaketWait)
                    {
                        byte[] Paket_tmep = new byte[Soket.UDP_recvlen];
                        for (int i = 0; i < Soket.UDP_recvlen; i++)
                        {
                            Paket_tmep[i] = Soket.UDP_getdata[i];
                        }

                        for (int i = 0; i < Paket_tmep.Length; i++ )
                        {
                            CurrentPaket.Add(Paket_tmep[i]);
                        }
                        if (CurrentPaket.Count >= NumOfCurrentPake)
                        {
                            Soket.Writeline(完成); 
                            PaketWait = false;
                        }
                        sub_設置接收封包逾時Timer();
                    }
                    else
                    {
                        CtrrentReceiveData = MyConvert.分解分隔號字串(ReceiveDataList[0], 命令空格);
                        if (ReceiveDataList[0].IndexOf("**") >= 0) cnt++;
                    }
                    ReceiveDataList.RemoveAt(0);             
                   
                }

  
                
            }
            private void sub_Receiver_10_解析收到的訊息(ref byte cnt)
            {
                if (CtrrentReceiveData[0] == 清除封包)
                {
                    cnt = 15;
                }
                else if (CtrrentReceiveData[0] == 等待封包發送致能)
                {
                    cnt = 16;
                }
                else if (CtrrentReceiveData[0] == 封包檢查)
                {
                    cnt = 17;
                }
                else if (CtrrentReceiveData[0] == 確認封包)
                {
                    cnt = 18;
                }
                else if (CtrrentReceiveData[0] == 創建檔案)
                {
                    cnt = 19;
                }
                else if (CtrrentReceiveData[0] == 更新命令)
                {
                    cnt = 20;
                }
                else cnt++;
            }
            private void sub_Receiver_15_清除封包(ref byte cnt)
            {
                ReceivePaketList.Clear();
                Soket.Writeline(完成);
                cnt = 200;
            }
            private void sub_Receiver_16_等待封包發送致能(ref byte cnt)
            {
                if (CtrrentReceiveData.Length == 2)
                {
                    if (int.TryParse(CtrrentReceiveData[1], out NumOfCurrentPake))
                    {
                        PaketWait = true;
                        CurrentPaket.Clear();
                        sub_設置接收封包逾時Timer();
                        Soket.Writeline(完成);       
                    }
                    else
                    {
                        Soket.Writeline(錯誤 + "#001");
                    }
                }
                else
                {
                    Soket.Writeline(錯誤 + "#002");
                }
                cnt = 200;
            }
            private void sub_Receiver_17_封包檢查(ref byte cnt)
            {
                if (CtrrentReceiveData.Length == 1)
                {
                    byte[] temp = Encoding.UTF8.GetBytes("**");
                    List<byte> CurrentPaket_temp = CurrentPaket.DeepClone();
                    CurrentPaket_temp.InsertRange(0, temp);
                    Soket.WriteByte(CurrentPaket_temp.ToArray());
                }
                else
                {
                    Soket.Writeline(錯誤 + "#003");
                }
                cnt = 200;
            }
            private void sub_Receiver_18_確認封包(ref byte cnt)
            {
                if (CtrrentReceiveData.Length == 1)
                {
                    ReceivePaketList.Add(CurrentPaket.ToArray());
                    Soket.Writeline(完成);
                }
                else
                {
                    Soket.Writeline(錯誤 + "#004");
                }
                cnt = 200;
            }
            private void sub_Receiver_19_創建檔案(ref byte cnt)
            {
                if (CtrrentReceiveData.Length == 1)
                {
                    FileStream fs = null;
                    try
                    {
                        fs = new FileStream(@".//" + FileName, FileMode.Create, FileAccess.Write);
                        byte[] AllPaket_Array = GetAllPaket();
                        fs.Write(AllPaket_Array, 0, AllPaket_Array.Length);//將快取中的資料寫入檔案中
                        fs.Flush();//清空快取資訊
                        fs.Close();
                    }
                    finally
                    {
                        if (fs != null) fs.Dispose();
                    }
                    Soket.Writeline(完成);
                }
                else
                {
                    Soket.Writeline(錯誤 + "#005");
                }
                cnt = 200;
            }
            private void sub_Receiver_20_更新命令(ref byte cnt)
            {
                if (CtrrentReceiveData.Length == 1)
                {
                    Soket.Writeline(完成);
                    Soket.Close();
                    UpdateProgram.SetFileName(Path.GetFileNameWithoutExtension(FileName), Path.GetExtension(FileName));
                    UpdateProgram.Update();                
                }
                else
                {
                    Soket.Writeline(錯誤 + "#006");
                }
                cnt = 200;
            }
            private void sub_Receiver_(ref byte cnt)
            {                          
                cnt++;
            }
        }
        static public class UpdateProgram
        {
            static private string sources_file = "";
            static private string process_file = "";
            static private string process_temp_file = "";
            static private string extension = "";
            internal enum MoveFileFlags
            {
                MOVEFILE_REPLACE_EXISTING = 1,
                MOVEFILE_COPY_ALLOWED = 2,
                MOVEFILE_DELAY_UNTIL_REBOOT = 4,
                MOVEFILE_WRITE_THROUGH = 8
            }
            [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            internal static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, MoveFileFlags dwFlags);

            static public void SetFileName(string sourcesfile, string Extension)
            {
                string ProcessName_temp = Process.GetCurrentProcess().ProcessName.Replace(".vshost", "");
                extension = Extension;
                sources_file = sourcesfile + extension;
                process_file = ProcessName_temp + extension;
                process_temp_file = ProcessName_temp + "_bak" + extension;    
            }
            static public void Update()
            {
                MoveFileEx(process_file, process_temp_file, MoveFileFlags.MOVEFILE_REPLACE_EXISTING);
                MoveFileEx(sources_file, process_file, MoveFileFlags.MOVEFILE_REPLACE_EXISTING);
                //將原始執行檔搬移,很奇妙的系統API.....可以在執行途中改變執行檔名稱與路徑


                //下載新版執行檔,到原始執行檔位置,或是也可以先搬別處之後再搬回 
                //更正式的做法會加上與server端的程式版本新就判斷才更新


                //清除垃圾舊檔
                Process P_sources = new Process();
                //設定一秒延遲,讓程式順利關閉才能刪除 
                P_sources.StartInfo = new ProcessStartInfo("cmd.exe", "/C choice /C Y /N /D Y /T 5 & Del " + "\"" + process_temp_file + "\"");
                P_sources.StartInfo.CreateNoWindow = true;
                P_sources.StartInfo.UseShellExecute = false;
                P_sources.Start();

                //執行新版程式自啟
                Process P_new = new Process();
                //設定一秒延遲,讓程式順利關閉才能刪除 
                P_new.StartInfo = new ProcessStartInfo("cmd.exe", "/C choice /C Y /N /D Y /T 1 & " + "\"" + process_file + "\"");
                P_new.StartInfo.CreateNoWindow = true;
                P_new.StartInfo.UseShellExecute = false;
                P_new.Start();

                //關閉程式
                Process.GetCurrentProcess().Close();
                Process.GetCurrentProcess().CloseMainWindow();
 
            }
        }
    }
}