using System;
using System.Collections.Generic;
using System.IO;
using NAudio.Wave;

namespace AudioProcessingLibrary
{
    public class MicrophoneRecorder
    {
        // 私有字段
        private WaveInEvent waveIn;
        private WaveFileWriter waveWriter;
        private string outputFilePath;
        private MemoryStream memoryStream; // 用於存儲 bytes
        private WaveOutEvent waveOut; // 用於播放

        // 公共屬性
        public int SelectedDeviceIndex { get; private set; } = 0; // 默認設備索引
        public WaveFormat WaveFormat { get; private set; } // 動態設置格式

        // 事件
        public event Action<byte[], int> OnSampleDataAvailable;
        public event Action<float[]> OnDecodedSamples; // 解碼後的樣本數據事件

        /// <summary>
        /// 查詢所有可用的錄音設備。
        /// </summary>
        public List<string> GetRecordingDevices()
        {
            var devices = new List<string>();
            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                var deviceInfo = WaveIn.GetCapabilities(i);
                devices.Add($"{i}: {deviceInfo.ProductName} ({deviceInfo.Channels} channels)");
            }
            return devices;
        }

        /// <summary>
        /// 選擇錄音設備。
        /// </summary>
        public void SelectDevice(int deviceIndex)
        {
            if (deviceIndex < 0 || deviceIndex >= WaveIn.DeviceCount)
                throw new ArgumentOutOfRangeException(nameof(deviceIndex), "無效的設備索引");
            SelectedDeviceIndex = deviceIndex;
        }

        /// <summary>
        /// 設定音訊採樣參數。
        /// </summary>
        public void SetSampleRate(int sampleRate, int bitsPerSample, int channels)
        {
            if (bitsPerSample != 8 && bitsPerSample != 16 && bitsPerSample != 24 && bitsPerSample != 32)
                throw new ArgumentException("位元深度必須為 8, 16, 24 或 32。", nameof(bitsPerSample));
            if (channels < 1)
                throw new ArgumentException("通道數必須至少為 1。", nameof(channels));

            WaveFormat = new WaveFormat(sampleRate, bitsPerSample, channels);
        }

        /// <summary>
        /// 開始錄音並保存為 WAV 文件。
        /// </summary>
        /// <param name="outputFilePath">錄音結果的輸出文件路徑（.wav 格式）。</param>
        public void StartRecording(string outputFilePath)
        {
            if (waveIn != null) throw new InvalidOperationException("錄音已經在進行中");
            if (string.IsNullOrEmpty(outputFilePath) || Path.GetExtension(outputFilePath)?.ToLower() != ".wav")
                throw new ArgumentException("必須提供有效的 .wav 文件路徑。", nameof(outputFilePath));

            this.outputFilePath = outputFilePath;
            memoryStream = null;

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

        /// <summary>
        /// 開始錄音並將結果輸出為 byte[]。
        /// </summary>
        public void StartRecording()
        {
            if (waveIn != null) throw new InvalidOperationException("錄音已經在進行中");

            memoryStream = new MemoryStream();

            waveIn = new WaveInEvent
            {
                DeviceNumber = SelectedDeviceIndex,
                WaveFormat = WaveFormat
            };

            waveIn.DataAvailable += WaveIn_DataAvailable;
            waveIn.RecordingStopped += WaveIn_RecordingStopped;

            waveWriter = new WaveFileWriter(memoryStream, waveIn.WaveFormat);

            waveIn.StartRecording();
        }

        /// <summary>
        /// 停止錄音並保存結果。
        /// </summary>
        public byte[] StopRecording()
        {
            if (waveIn == null) return null;

            waveIn.StopRecording();

            byte[] recordedBytes = null;

            if (memoryStream != null)
            {
                waveWriter?.Dispose();
                recordedBytes = memoryStream.ToArray();
                memoryStream.Dispose();
            }

            return recordedBytes;
        }

        /// <summary>
        /// 播放錄音文件。
        /// </summary>
        /// <param name="filePath">錄音文件路徑。</param>
        public void Play(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("指定的文件不存在", filePath);

            StopPlayback(); // 停止當前播放（如果有）

            waveOut = new WaveOutEvent();
            var audioFileReader = new AudioFileReader(filePath);

            waveOut.Init(audioFileReader);
            waveOut.Play();
        }

        /// <summary>
        /// 播放內存中的錄音數據。
        /// </summary>
        public void Play(byte[] audioData)
        {
            if (audioData == null || audioData.Length == 0)
                throw new ArgumentException("音訊數據為空", nameof(audioData));

            StopPlayback(); // 停止當前播放（如果有）

            memoryStream = new MemoryStream(audioData);
            memoryStream.Position = 0;

            waveOut = new WaveOutEvent();
            var waveProvider = new RawSourceWaveStream(memoryStream, WaveFormat);

            waveOut.Init(waveProvider);
            waveOut.Play();
        }

        /// <summary>
        /// 停止播放。
        /// </summary>
        public void StopPlayback()
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
        }

        /// <summary>
        /// 處理錄音數據的事件，將數據寫入文件或內存並解碼。
        /// </summary>
        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            // 將數據寫入文件或內存
            waveWriter?.Write(e.Buffer, 0, e.BytesRecorded);
            waveWriter?.Flush();

            // 解碼數據並觸發事件
            var samples = DecodeAudioData(e.Buffer, e.BytesRecorded);
            OnDecodedSamples?.Invoke(samples);

            // 觸發原始數據事件
            OnSampleDataAvailable?.Invoke(e.Buffer, e.BytesRecorded);
        }

        /// <summary>
        /// 解碼音訊數據為浮點樣本數據。
        /// </summary>
        private float[] DecodeAudioData(byte[] buffer, int bytesRecorded)
        {
            int bytesPerSample = WaveFormat.BitsPerSample / 8;
            int sampleCount = bytesRecorded / bytesPerSample / WaveFormat.Channels;
            float[] samples = new float[sampleCount * WaveFormat.Channels];

            for (int i = 0, sampleIndex = 0; i < bytesRecorded; i += bytesPerSample)
            {
                float sample = 0;

                // 根據位元深度解碼
                switch (WaveFormat.BitsPerSample)
                {
                    case 8: // 8-bit 音訊
                        sample = (buffer[i] - 128) / 128f;
                        break;
                    case 16: // 16-bit 音訊
                        sample = BitConverter.ToInt16(buffer, i) / 32768f;
                        break;
                    case 24: // 24-bit 音訊
                        sample = ((buffer[i + 2] << 16) | (buffer[i + 1] << 8) | buffer[i]) / 8388608f;
                        break;
                    case 32: // 32-bit 音訊
                        sample = BitConverter.ToInt32(buffer, i) / (float)int.MaxValue;
                        break;
                    default:
                        throw new NotSupportedException($"不支持的位元深度: {WaveFormat.BitsPerSample}");
                }

                // 加入解碼數據
                samples[sampleIndex++] = sample;
            }

            return samples;
        }

        /// <summary>
        /// 當錄音停止時釋放資源。
        /// </summary>
        private void WaveIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
            waveIn.Dispose();
            waveIn = null;

            waveWriter?.Dispose();
            waveWriter = null;

            if (e.Exception != null)
            {
                Console.WriteLine($"錄音中發生錯誤: {e.Exception.Message}");
            }
        }
    }
}
