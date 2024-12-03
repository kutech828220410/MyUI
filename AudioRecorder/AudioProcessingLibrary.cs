using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NAudio.Wave;

namespace AudioProcessingLibrary
{  
    /// <summary>
   /// MicrophoneRecorder 類別
   /// 
   /// 功能清單：
   /// 1. 錄音功能：
   ///    - 支援錄音到檔案或內存。
   ///    - 支援設定錄音參數（採樣率、位元深度、通道數）。
   /// 
   /// 2. 播放功能：
   ///    - 支援播放錄製的檔案或內存中的音訊資料。
   /// 
   /// 3. TCP 音訊串流：
   ///    - 支援同時錄音並將音訊資料即時傳輸到指定的目標（IP 和 PORT）。
   ///    - 提供切換傳輸目標 IP 和 PORT 的功能。
   /// 
   /// 4. TCP 字串監聽：
   ///    - 支援監聽指定埠號接收字串資料。
   ///    - 接收到字串資料後觸發事件。
   /// 
   /// 5. 事件通知：
   ///    - OnSampleDataAvailable: 錄音時的原始音訊資料。
   ///    - OnDecodedSamples: 解碼後的音訊樣本。
   ///    - OnAudioStreamReceived: 接收到的音訊資料。
   ///    - OnTcpMessage: TCP 狀態變化或錯誤訊息。
   ///    - OnTcpMessageReceived: 接收到的 TCP 字串資料。
   /// </summary>
    public class MicrophoneRecorder
    {
        // 私有字段
        private WaveInEvent waveIn;
        private WaveFileWriter waveWriter;
        private string outputFilePath;
        private MemoryStream memoryStream;
        private WaveOutEvent waveOut;

        // TCP 私有字段（音訊串流）
        private TcpClient tcpClient;
        private NetworkStream tcpStream;
        private Thread tcpSenderThread;
        private bool isConnected = false;
        private string currentIp = "127.0.0.1";
        private int currentPort = 5000;

        // TCP 私有字段（字串監聽）
        private TcpListener tcpListener;
        private Thread tcpListenerThread;

        // 公共屬性
        public int SelectedDeviceIndex { get; private set; } = 0;
        public WaveFormat WaveFormat { get; private set; }

        // 事件
        public event Action<byte[], int> OnSampleDataAvailable;
        public event Action<float[]> OnDecodedSamples;
        public event Action<byte[]> OnAudioStreamReceived;
        public event Action<string> OnTcpMessage;
        public event Action<string> OnTcpMessageReceived;

        #region 音訊錄製與播放功能
        public void StartRecording(string outputFilePath)
        {
            if (waveIn != null) throw new InvalidOperationException("錄音已經在進行中");
            this.outputFilePath = outputFilePath;
            waveIn = new WaveInEvent
            {
                DeviceNumber = SelectedDeviceIndex,
                WaveFormat = WaveFormat
            };
            waveIn.DataAvailable += WaveIn_DataAvailable;
            waveIn.RecordingStopped += WaveIn_RecordingStopped;
            waveWriter = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
            waveIn.StartRecording();
        }

        public byte[] StopRecording()
        {
            if (waveIn == null) return null;
            waveIn.StopRecording();
            byte[] recordedBytes = memoryStream?.ToArray();
            waveWriter?.Dispose();
            waveWriter = null;
            return recordedBytes;
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            waveWriter?.Write(e.Buffer, 0, e.BytesRecorded);
            waveWriter?.Flush();
            OnSampleDataAvailable?.Invoke(e.Buffer, e.BytesRecorded);
        }

        private void WaveIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
            waveIn.Dispose();
            waveIn = null;
            waveWriter?.Dispose();
            waveWriter = null;
        }
        #endregion

        #region TCP 音訊串流功能
        public void StartAudioStreaming(string ip, int port)
        {
            StopAudioStreaming();
            currentIp = ip;
            currentPort = port;
            tcpSenderThread = new Thread(SendAudio)
            {
                IsBackground = true
            };
            tcpSenderThread.Start();
        }

        public void ChangeTarget(string newIp, int newPort)
        {
            Console.WriteLine($"切換目標到新的 IP 和 PORT: {newIp}:{newPort}");
            StartAudioStreaming(newIp, newPort);
        }

        public void StopAudioStreaming()
        {
            isConnected = false;
            tcpStream?.Close();
            tcpClient?.Close();
            tcpStream = null;
            tcpClient = null;
            if (tcpSenderThread != null && tcpSenderThread.IsAlive)
            {
                tcpSenderThread.Join();
            }
        }

        private void SendAudio()
        {
            while (true)
            {
                if (!isConnected)
                {
                    try
                    {
                        tcpClient = new TcpClient(currentIp, currentPort);
                        tcpStream = tcpClient.GetStream();
                        isConnected = true;
                        OnTcpMessage?.Invoke($"已連接到 {currentIp}:{currentPort}");
                    }
                    catch (Exception ex)
                    {
                        OnTcpMessage?.Invoke($"連線失敗: {ex.Message}");
                        Thread.Sleep(1000);
                        continue;
                    }
                }

                try
                {
                    waveIn = new WaveInEvent
                    {
                        WaveFormat = new WaveFormat(16000, 16, 1)
                    };
                    waveIn.DataAvailable += (s, e) =>
                    {
                        if (tcpStream != null)
                        {
                            tcpStream.Write(e.Buffer, 0, e.BytesRecorded);
                            tcpStream.Flush();
                        }
                    };
                    waveIn.StartRecording();
                    while (isConnected) { Thread.Sleep(100); }
                    waveIn.StopRecording();
                }
                catch (Exception ex)
                {
                    OnTcpMessage?.Invoke($"發送音訊時發生錯誤: {ex.Message}");
                    StopAudioStreaming();
                }
            }
        }
        #endregion

        #region TCP 字串監聽功能
        public void StartTextTcpListener(int port)
        {
            StopTextTcpListener();
            tcpListenerThread = new Thread(() => ListenForTextTCP(port))
            {
                IsBackground = true
            };
            tcpListenerThread.Start();
        }

        public void StopTextTcpListener()
        {
            if (tcpListener != null)
            {
                tcpListener.Stop();
                tcpListener = null;
            }
            if (tcpListenerThread != null && tcpListenerThread.IsAlive)
            {
                tcpListenerThread.Join();
            }
        }

        private void ListenForTextTCP(int port)
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, port);
                tcpListener.Start();
                OnTcpMessage?.Invoke($"正在監聽 TCP 埠 {port}，接收來自客戶端的字串資料...");
                while (tcpListener != null)
                {
                    using (TcpClient client = tcpListener.AcceptTcpClient())
                    using (NetworkStream stream = client.GetStream())
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string receivedText = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        OnTcpMessageReceived?.Invoke(receivedText);
                    }
                }
            }
            catch (Exception ex)
            {
                OnTcpMessage?.Invoke($"接收字串資料時發生錯誤: {ex.Message}");
            }
        }
        #endregion
    }
}
