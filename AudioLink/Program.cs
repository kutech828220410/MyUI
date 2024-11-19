using System;
using System.Collections.Generic; // 支援 List<T>、集合操作
using System.IO; // 支援檔案操作（File.ReadAllBytes 等）
using System.Net; // 支援網路相關操作（IPAddress, IPEndPoint 等）
using System.Net.Sockets; // 支援 UDP 操作（UdpClient）
using System.Threading; // 支援多執行緒（Thread）
using NAudio.Wave;

class Program
{
    private static bool isRunning = true;
    private const string SettingsFile = "settings.txt"; // 設定檔路徑

    static void Main(string[] args)
    {
        int udpPort = 5005;

        // 加載預設 IP、Port 和採樣頻率
        (string defaultIP, int defaultPort, int defaultSampleRate) = LoadDefaultSettings();

        // 啟動 UDP 監聽執行緒
        Thread udpListenerThread = new Thread(() => StartUdpListener(udpPort));
        udpListenerThread.IsBackground = true;
        udpListenerThread.Start();

        string savedFilePath = "recordedAudio.wav";

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"==== 音訊錄製與播放 (監聽 UDP Port: {udpPort}) ====");
            Console.WriteLine("1. 錄製並存檔音訊");
            Console.WriteLine("2. 播放存檔的音訊");
            Console.WriteLine("3. 發送音訊資料流到指定 IP 和 Port (UDP)");
            Console.WriteLine("4. 使用儲存的 IP 和 Port 自動發送音訊");
            Console.WriteLine($"5. 設定錄音採樣頻率 (當前: {defaultSampleRate} Hz)");
            Console.WriteLine("6. 離開");
            Console.Write("請選擇操作：");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RecordAndSaveAudio(savedFilePath, defaultSampleRate);
                    break;

                case "2":
                    PlayAudio(savedFilePath);
                    break;

                case "3":
                    SendAudio(savedFilePath, ref defaultIP, ref defaultPort, defaultSampleRate);
                    break;

                case "4":
                    AutoSendAudio(savedFilePath, defaultIP, defaultPort);
                    break;

                case "5":
                    defaultSampleRate = SetSampleRate();
                    SaveDefaultSettings(defaultIP, defaultPort, defaultSampleRate);
                    break;

                case "6":
                    isRunning = false;
                    Console.WriteLine("程序結束，再見！");
                    return;

                default:
                    Console.WriteLine("無效選項，請重新選擇！");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }


    /// <summary>
    /// 錄製並保存音訊
    /// </summary>
    /// <param name="filePath">保存的檔案路徑</param>
    /// <param name="sampleRate">採樣頻率</param>
    static void RecordAndSaveAudio(string filePath, int sampleRate)
    {
        Console.WriteLine($"錄製音訊 (採樣頻率: {sampleRate} Hz)...按任意鍵停止錄製。");

        using (var waveIn = new WaveInEvent())
        {
            waveIn.WaveFormat = new WaveFormat(sampleRate, 16, 1); // 單聲道, 16位元
            using (var writer = new WaveFileWriter(filePath, waveIn.WaveFormat))
            {
                waveIn.DataAvailable += (s, e) =>
                {
                    writer.Write(e.Buffer, 0, e.BytesRecorded);
                };

                waveIn.StartRecording();
                Console.ReadKey();
                waveIn.StopRecording();
            }
        }

        Console.WriteLine($"錄製完成，已保存至 {filePath}");
        Thread.Sleep(1000);
    }

    /// <summary>
    /// 播放音訊
    /// </summary>
    static void PlayAudio(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"找不到音訊檔案：{filePath}");
            Thread.Sleep(1000);
            return;
        }

        Console.WriteLine($"正在播放 {filePath}...");
        using (WaveOutEvent waveOut = new WaveOutEvent())
        using (AudioFileReader audioFile = new AudioFileReader(filePath))
        {
            waveOut.Init(audioFile);
            waveOut.Play();

            // 等待播放完成
            while (waveOut.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(100); // 每 100ms 檢查播放狀態
            }
        }

        Console.WriteLine("音訊播放完畢...");
        Thread.Sleep(1000);
    }

    static void SendAudio(string filePath, ref string defaultIP, ref int defaultPort, int sampleRate)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("無錄製的音訊可發送！");
            Thread.Sleep(1000);
            return;
        }

        Console.Write($"請輸入目標 IP (預設 {defaultIP})：");
        string targetIP = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(targetIP)) targetIP = defaultIP;

        Console.Write($"請輸入目標 Port (預設 {defaultPort})：");
        string portInput = Console.ReadLine();
        int targetPort = string.IsNullOrWhiteSpace(portInput) ? defaultPort : int.Parse(portInput);

        byte[] audioData = File.ReadAllBytes(filePath);
        const int MaxPacketSize = 65000; // 單個 UDP 封包的最大大小

        Console.WriteLine($"開始發送音訊到 {targetIP}:{targetPort}...");

        using (UdpClient udpClient = new UdpClient())
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(targetIP), targetPort);

            // 發送封包
            SendPackets(udpClient, remoteEndPoint, audioData, MaxPacketSize);
        }

        // 更新預設值
        defaultIP = targetIP;
        defaultPort = targetPort;
        SaveDefaultSettings(defaultIP, defaultPort, sampleRate);
        Console.WriteLine($"目標 IP 和 Port 已保存為預設值：{defaultIP}:{defaultPort}，採樣頻率：{sampleRate} Hz");
    }



    /// <summary>
    /// 自動發送音訊資料流，支援分割封包和總封包，直到按任意鍵停止
    /// </summary>
    static void AutoSendAudio(string filePath, string targetIP, int targetPort)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("無錄製的音訊可發送！");
            Thread.Sleep(1000);
            return;
        }

        Console.Write("請輸入自動發送間隔時間（毫秒，預設 1000）：");
        string intervalInput = Console.ReadLine();
        int interval = string.IsNullOrWhiteSpace(intervalInput) ? 1000 : int.Parse(intervalInput);

        byte[] audioData = File.ReadAllBytes(filePath);
        const int MaxPacketSize = 65000; // 單個 UDP 封包的最大大小

        Console.WriteLine($"開始自動發送音訊到 {targetIP}:{targetPort}，按任意鍵停止...");

        Thread autoSendThread = new Thread(() =>
        {
            using (UdpClient udpClient = new UdpClient())
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(targetIP), targetPort);

                try
                {
                    while (!Console.KeyAvailable)
                    {
                        // 發送封包
                        SendPackets(udpClient, remoteEndPoint, audioData, MaxPacketSize);

                        Thread.Sleep(interval);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"發送失敗：{ex.Message}");
                }
            }
        });

        autoSendThread.Start();
        Console.ReadKey(); // 等待用戶按下任意鍵停止
        autoSendThread.Abort();
        Console.WriteLine("自動發送已停止。");
    }

    /// <summary>
    /// 設定錄音採樣頻率
    /// </summary>
    /// <returns>選擇的採樣頻率</returns>
    static int SetSampleRate()
    {
        Console.WriteLine("可選擇的採樣頻率 (Hz):");
        Console.WriteLine("1. 8000");
        Console.WriteLine("2. 16000");
        Console.WriteLine("3. 44100");
        Console.WriteLine("4. 48000");
        Console.WriteLine("5. 96000");
        Console.Write("請輸入選項 (1-5): ");
        string choice = Console.ReadLine();

        int sampleRate;

        switch (choice)
        {
            case "1":
                sampleRate = 8000;
                break;
            case "2":
                sampleRate = 16000;
                break;
            case "3":
                sampleRate = 44100;
                break;
            case "4":
                sampleRate = 48000;
                break;
            case "5":
                sampleRate = 96000;
                break;
            default:
                sampleRate = 44100; // 預設為 44100 Hz
                break;
        }

        return sampleRate;
    }

    /// <summary>
    /// 加載預設設定，包括 IP、Port 和採樣頻率
    /// </summary>
    /// <returns>預設 IP、Port 和採樣頻率</returns>
    static (string ip, int port, int sampleRate) LoadDefaultSettings()
    {
        if (File.Exists(SettingsFile))
        {
            try
            {
                string[] settings = File.ReadAllLines(SettingsFile);
                if (settings.Length == 3)
                {
                    string ip = settings[0];
                    int port = int.Parse(settings[1]);
                    int sampleRate = int.Parse(settings[2]);
                    return (ip, port, sampleRate);
                }
            }
            catch
            {
                Console.WriteLine("加載設定檔失敗，使用默認值。");
            }
        }

        return ("127.0.0.1", 5000, 44100);
    }


    /// <summary>
    /// 保存預設設定，包括 IP、Port 和採樣頻率
    /// </summary>
    /// <param name="ip">目標 IP 地址</param>
    /// <param name="port">目標 Port</param>
    /// <param name="sampleRate">錄音採樣頻率</param>
    static void SaveDefaultSettings(string ip, int port, int sampleRate)
    {
        try
        {
            File.WriteAllLines(SettingsFile, new[] { ip, port.ToString(), sampleRate.ToString() });
            Console.WriteLine($"已保存預設 IP、Port 和採樣頻率：{ip}:{port}，{sampleRate} Hz");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"保存設定檔失敗：{ex.Message}");
        }
    }



    /// <summary>
    /// 啟動 UDP 監聽
    /// </summary>
    static void StartUdpListener(int port)
    {
        using (UdpClient udpListener = new UdpClient(port))
        {
            Console.WriteLine($"正在監聽 UDP 數據 (Port: {port})...");
            while (isRunning)
            {
                try
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedBytes = udpListener.Receive(ref remoteEndPoint);

                    Console.WriteLine($"[UDP 接收] 來源 IP: {remoteEndPoint.Address}, 時間: {DateTime.Now}, 數據長度: {receivedBytes.Length} 字節");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"UDP 監聽錯誤: {ex.Message}");
                }
            }
        }
    }
    /// <summary>
    /// 發送封包資料，包括分割封包和總封包
    /// </summary>
    /// <param name="udpClient">UDP 客戶端</param>
    /// <param name="remoteEndPoint">目標終端點</param>
    /// <param name="audioData">完整音訊資料</param>
    /// <param name="maxPacketSize">單個封包的最大大小</param>
    static void SendPackets(UdpClient udpClient, IPEndPoint remoteEndPoint, byte[] audioData, int maxPacketSize)
    {
        const int HeaderSize = 4;       // 序號或長度的大小
        const int EndByteSize = 1;      // 總封包結尾字節大小

        // 計算分割封包數量
        int totalPackets = (int)Math.Ceiling((double)audioData.Length / (maxPacketSize - HeaderSize));

        // 發送分割封包
        for (int i = 0; i < totalPackets; i++)
        {
            int offset = i * (maxPacketSize - HeaderSize);
            int contentSize = Math.Min(maxPacketSize - HeaderSize, audioData.Length - offset);

            // 使用函式生成分割封包
            byte[] packet = CreateSegmentPacket(audioData, offset, contentSize, i + 1);
            udpClient.Send(packet, packet.Length, remoteEndPoint);
            Console.WriteLine($"已發送第 {i + 1}/{totalPackets} 個分割封包，大小：{packet.Length} 字節");
        }

        // 使用函式生成分割的總封包並逐個發送
        List<byte[]> totalPacketsList = CreateTotalPackets(audioData);
        foreach (var totalPacket in totalPacketsList)
        {
            udpClient.Send(totalPacket, totalPacket.Length, remoteEndPoint);
            Console.WriteLine($"已發送總封包片段，大小：{totalPacket.Length} 字節");
        }
    }

    /// <summary>
    /// 生成總封包，分割總封包以適應限制
    /// </summary>
    /// <param name="data">完整音訊資料</param>
    /// <returns>多個總封包</returns>
    static List<byte[]> CreateTotalPackets(byte[] data)
    {
        const int MaxPacketSize = 65000; // 單個 UDP 封包的最大大小
        const int HeaderSize = 4;       // 封包長度大小
        const int EndByteSize = 1;      // 結尾字節大小
        int totalPacketLength = HeaderSize + data.Length + EndByteSize;

        // 如果總封包大小小於限制，直接返回
        if (totalPacketLength <= MaxPacketSize)
        {
            byte[] totalPacket = new byte[totalPacketLength];
            byte[] totalPacketHeader = BitConverter.GetBytes(totalPacketLength);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(totalPacketHeader);

            Array.Copy(totalPacketHeader, 0, totalPacket, 0, HeaderSize);
            Array.Copy(data, 0, totalPacket, HeaderSize, data.Length);
            totalPacket[totalPacket.Length - 1] = 3;

            return new List<byte[]> { totalPacket };
        }

        // 分割總封包
        List<byte[]> packets = new List<byte[]>();
        int offset = 0;
        while (offset < data.Length)
        {
            int contentSize = Math.Min(MaxPacketSize - HeaderSize - EndByteSize, data.Length - offset);
            byte[] packet = new byte[HeaderSize + contentSize + EndByteSize];
            byte[] packetHeader = BitConverter.GetBytes(HeaderSize + contentSize + EndByteSize);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(packetHeader);

            Array.Copy(packetHeader, 0, packet, 0, HeaderSize);
            Array.Copy(data, offset, packet, HeaderSize, contentSize);
            packet[packet.Length - 1] = 3;

            packets.Add(packet);
            offset += contentSize;
        }

        return packets;
    }

    /// <summary>
    /// 生成單個分割封包
    /// </summary>
    /// <param name="data">完整音訊資料</param>
    /// <param name="offset">資料偏移量</param>
    /// <param name="packetSize">當前封包內容大小</param>
    /// <param name="sequenceNumber">封包序號</param>
    /// <returns>完整的分割封包</returns>
    static byte[] CreateSegmentPacket(byte[] data, int offset, int packetSize, int sequenceNumber)
    {
        const int HeaderSize = 4; // 封包序號大小
        byte[] packet = new byte[HeaderSize + packetSize];
        byte[] packetNumber = BitConverter.GetBytes(sequenceNumber);

        // 確保序號為大端序
        if (BitConverter.IsLittleEndian)
            Array.Reverse(packetNumber);

        // 加入序號與內容
        Array.Copy(packetNumber, 0, packet, 0, HeaderSize);
        Array.Copy(data, offset, packet, HeaderSize, packetSize);

        return packet;
    }

    /// <summary>
    /// 生成總封包
    /// </summary>
    /// <param name="data">完整音訊資料</param>
    /// <returns>完整的總封包</returns>
    static byte[] CreateTotalPacket(byte[] data)
    {
        const int HeaderSize = 4; // 封包長度大小
        const int EndByteSize = 1; // 結尾字節大小
        int totalPacketLength = HeaderSize + data.Length + EndByteSize;

        byte[] totalPacket = new byte[totalPacketLength];
        byte[] totalPacketHeader = BitConverter.GetBytes(totalPacketLength);

        // 確保封包長度為大端序
        if (BitConverter.IsLittleEndian)
            Array.Reverse(totalPacketHeader);

        // 加入封包長度、內容與結尾字節
        Array.Copy(totalPacketHeader, 0, totalPacket, 0, HeaderSize);
        Array.Copy(data, 0, totalPacket, HeaderSize, data.Length);
        totalPacket[totalPacket.Length - 1] = 3; // 結尾字節

        return totalPacket;
    }

}
