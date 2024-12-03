using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NAudio.Wave;

class AudioStreamClient
{
    private static volatile bool isConnected = false; // 確保多執行緒狀態一致
    private static volatile bool isRunning = true;   // 控制整體運行狀態
    private static TcpClient client = null;
    private static NetworkStream stream = null;

    static void Main(string[] args)
    {
        string serverIP = "192.168.5.210"; // 伺服器 IP
        int serverPort = 3300;            // 伺服器端口
        int localPort = 3301;             // 本地監聽端口

        // 啟動音訊接收執行緒
        UdpClient udpListener = new UdpClient(localPort);
        Thread receiveThread = new Thread(() => ListenForTextTCP(3301));
        receiveThread.IsBackground = true;
        receiveThread.Start();

        // 啟動連線執行緒
        Thread connectThread = new Thread(() => ConnectToServer(serverIP, serverPort));
        connectThread.IsBackground = true;
        connectThread.Start();

        // 啟動音訊發送執行緒
        Thread sendAudioThread = new Thread(SendAudio);
        sendAudioThread.IsBackground = true;
        sendAudioThread.Start();

        Console.WriteLine("按任意鍵停止程式...");
        Console.ReadKey();

        // 停止程式
        isRunning = false;
        client?.Close();
    }

    private static void ConnectToServer(string serverIP, int serverPort)
    {
        while (isRunning)
        {
            if (!isConnected)
            {
                try
                {
                    Console.WriteLine($"嘗試連接到伺服器 {serverIP}:{serverPort}...");
                    client = new TcpClient(serverIP, serverPort);
                    stream = client.GetStream();
                    isConnected = true;
                    Console.WriteLine($"成功連接到伺服器 {serverIP}:{serverPort}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"無法連接到伺服器: {ex.Message}");
                    Console.WriteLine("2 秒後重試...");
                    Thread.Sleep(2000);
                }
            }
            else
            {
                Thread.Sleep(100); // 如果已連接，稍作休眠以節省資源
            }
        }
    }

    private static void SendAudio()
    {
        while (isRunning)
        {
            if (isConnected)
            {
                try
                {
                    using (WaveInEvent waveIn = new WaveInEvent())
                    {
                        waveIn.WaveFormat = new WaveFormat(16000, 16, 1); // 16kHz 單聲道 16-bit
                        waveIn.DataAvailable += (s, e) =>
                        {
                            try
                            {
                                if (isConnected && stream != null)
                                {
                                    int offset = 0;
                                    while (offset < e.BytesRecorded)
                                    {
                                        // 分塊發送，最大 1024 字節
                                        int chunkSize = Math.Min(1024, e.BytesRecorded - offset);
                                        stream.Write(e.Buffer, offset, chunkSize);
                                        offset += chunkSize;
                                    }
                                    stream.Flush();
                                    Console.WriteLine($"{DateTime.Now.ToLocalTime()} - 已發送 {e.BytesRecorded} 字節音訊資料 (分多次傳輸)。");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"發送音訊時發生錯誤: {ex.Message}");
                                isConnected = false; // 標記連線斷開
                            }
                        };

                        waveIn.StartRecording();
                        Console.WriteLine("正在錄音並傳輸音訊...");
                        while (isConnected) { Thread.Sleep(100); } // 保持發送執行緒運行
                        waveIn.StopRecording();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"發送音訊時發生意外錯誤: {ex.Message}");
                    isConnected = false; // 重置連線狀態
                }
            }
            else
            {
                Thread.Sleep(1000); // 未連線時稍作休眠
            }
        }
    }
    private static void ListenForTextTCP(int port)
    {
        try
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();
            Console.WriteLine($"正在監聽 TCP 埠 {port}，接收來自伺服器的字串資料...");

            while (true)
            {
                using (TcpClient client = tcpListener.AcceptTcpClient())
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string receivedText = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    Console.WriteLine($"接收到來自客戶端的字串資料: {receivedText}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"接收字串資料時發生錯誤: {ex.Message}");
        }
    }


}
