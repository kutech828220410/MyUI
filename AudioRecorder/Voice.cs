using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SoundTouch.Net.NAudioSupport;
using SoundTouch.Net;
using SoundTouch;
using System.Diagnostics;

namespace AudioProcessingLibrary
{
    public class SoundTouchProfile
    {
        /// <summary>
        /// 是否使用快速模式（省略更精細處理以提升效能）
        /// </summary>
        public bool UseQuickSeek { get; private set; }

        /// <summary>
        /// 是否啟用防 alias 濾波器
        /// </summary>
        public bool UseAntiAliasingFilter { get; private set; }

        public SoundTouchProfile(bool useQuickSeek, bool useAntiAliasingFilter)
        {
            this.UseQuickSeek = useQuickSeek;
            this.UseAntiAliasingFilter = useAntiAliasingFilter;
        }
    }

    public class VarispeedSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider source;
        private readonly SoundTouchProcessor soundTouch;
        private readonly int channels;
        private readonly int readSize;
        private readonly float[] sourceBuffer;
        private readonly float[] soundTouchBuffer;

        public float PlaybackRate
        {
            get => (float)soundTouch.Rate;
            set => soundTouch.Rate = value;
        }

        public WaveFormat WaveFormat => source.WaveFormat;

        public VarispeedSampleProvider(ISampleProvider source, int readSize, SoundTouchProfile profile)
        {
            this.source = source;
            this.readSize = readSize;
            this.channels = source.WaveFormat.Channels;
            this.soundTouch = new SoundTouchProcessor
            {
                SampleRate = source.WaveFormat.SampleRate,
                Channels = channels,
                Pitch = 1.0,
                Tempo = 1.0,
                Rate = 1.0
            };

            this.sourceBuffer = new float[readSize];
            this.soundTouchBuffer = new float[readSize * 2]; // 預留空間
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int totalSamplesSupplied = 0;

            while (totalSamplesSupplied < count)
            {
                int samplesAvailable = soundTouch.AvailableSamples;
                if (samplesAvailable == 0)
                {
                    int readFromSource = source.Read(sourceBuffer, 0, readSize);
                    if (readFromSource > 0)
                    {
                        var span = new ReadOnlySpan<float>(sourceBuffer, 0, readFromSource);
                        soundTouch.PutSamples(span, readFromSource / channels);
                    }
                    else
                    {
                        soundTouch.Flush();
                    }
                }

                var outSpan = new Span<float>(soundTouchBuffer, 0, Math.Min(count - totalSamplesSupplied, soundTouchBuffer.Length));
                int received = soundTouch.ReceiveSamples(outSpan, outSpan.Length / channels) * channels;
                if (received == 0) break;

                Array.Copy(soundTouchBuffer, 0, buffer, offset + totalSamplesSupplied, received);
                totalSamplesSupplied += received;
            }

            return totalSamplesSupplied;
        }
    }
    static public class Voice
    {
        public static string currentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        static public string PlayBase64Mp3WithFFmpegAndReturnMp3Base64(this string base64Data, float speed = 1.0f)
        {
            string tempInputPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.mp3");
            string tempOutputPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.mp3");
            string resultBase64 = null;

            try
            {
                // 將 Base64 存成 MP3 檔
                File.WriteAllBytes(tempInputPath, Convert.FromBase64String(base64Data));

                // 呼叫 FFmpeg 處理 atempo 並輸出 MP3
                Console.WriteLine("開始呼叫 FFmpeg 處理...");

                string ffmpegPath = $@"{currentDirectory}\ffmpeg.exe"; // ⚠ 請改成你的路徑
                Console.WriteLine(ffmpegPath);

                // 注意 atempo 只接受 0.5~2.0，若超過要串接多個 atempo
                string atempoFilter = GenerateAtempoFilter(speed);

                var psi = new ProcessStartInfo
                {
                    FileName = "ffmpeg.exe",
                    Arguments = $"-y -i \"{tempInputPath}\" -filter:a \"{atempoFilter}\" -codec:a libmp3lame -qscale:a 2 \"{tempOutputPath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(psi))
                {
                    process.OutputDataReceived += (s, e) => { if (e.Data != null) Console.WriteLine(e.Data); };
                    process.ErrorDataReceived += (s, e) => { if (e.Data != null) Console.WriteLine(e.Data); };
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();
                }

                Console.WriteLine("FFmpeg 處理完成");

                // 回傳處理後 MP3 的 Base64
                byte[] mp3Bytes = File.ReadAllBytes(tempOutputPath);
                resultBase64 = Convert.ToBase64String(mp3Bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"處理過程發生錯誤: {ex.Message}");
            }
            finally
            {
                if (File.Exists(tempInputPath)) File.Delete(tempInputPath);
                if (File.Exists(tempOutputPath)) File.Delete(tempOutputPath);
            }

            return resultBase64;
        }
        static public string PlayBase64Mp3WithFFmpegAndReturnMp3Base64_Docker(this string base64Data, float speed = 1.0f)
        {
            string tempInputPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.mp3");
            string tempOutputPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.mp3");
            string resultBase64 = null;

            try
            {
                File.WriteAllBytes(tempInputPath, Convert.FromBase64String(base64Data));

                Console.WriteLine("PlayBase64Mp3WithFFmpegAndReturnMp3Base64_Docker");
                Console.WriteLine("開始呼叫 FFmpeg 處理...");

                string atempoFilter = GenerateAtempoFilter(speed);

                var psi = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = $"-y -i \"{tempInputPath}\" -filter:a \"{atempoFilter}\" -codec:a libmp3lame -qscale:a 2 \"{tempOutputPath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = psi })
                {
                    process.OutputDataReceived += (s, e) => { if (e.Data != null) Console.WriteLine(e.Data); };
                    process.ErrorDataReceived += (s, e) => { if (e.Data != null) Console.WriteLine(e.Data); };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    if (!process.WaitForExit(60000))
                    {
                        process.Kill();
                        throw new TimeoutException("FFmpeg 處理超時");
                    }

                    if (process.ExitCode != 0)
                    {
                        throw new Exception($"FFmpeg 執行失敗，ExitCode: {process.ExitCode}");
                    }
                }

                byte[] mp3Bytes = File.ReadAllBytes(tempOutputPath);
                resultBase64 = Convert.ToBase64String(mp3Bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"處理過程發生錯誤: {ex.Message}");
            }
            finally
            {
                if (File.Exists(tempInputPath)) File.Delete(tempInputPath);
                if (File.Exists(tempOutputPath)) File.Delete(tempOutputPath);
            }

            return resultBase64;
        }

     
        static public void PlayBase64Mp3WithFFmpeg(this string base64Data, float volume = 1.0f, float speed = 1.0f)
        {
            try
            {
                string tempInputPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.mp3");
                string tempOutputPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.wav");

                // 將 Base64 存成 MP3 檔
                File.WriteAllBytes(tempInputPath, Convert.FromBase64String(base64Data));

                // 呼叫 FFmpeg 處理 atempo
                Console.WriteLine("開始呼叫 FFmpeg 處理...");
                Console.WriteLine($"{currentDirectory}\ffmpeg.exe");
                var psi = new ProcessStartInfo
                {
                    FileName = $@"{currentDirectory}\ffmpeg.exe",
                    Arguments = $"-y -i \"{tempInputPath}\" -filter:a \"atempo={speed}\" \"{tempOutputPath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(psi))
                {
                    process.OutputDataReceived += (s, e) => { if (e.Data != null) Console.WriteLine(e.Data); };
                    process.ErrorDataReceived += (s, e) => { if (e.Data != null) Console.WriteLine(e.Data); };
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();
                }

                Console.WriteLine("FFmpeg 處理完成，開始播放...");

                using (var reader = new AudioFileReader(tempOutputPath))
                using (var waveOut = new WaveOutEvent())
                {
                    var volumeProvider = new VolumeSampleProvider(reader) { Volume = volume };

                    waveOut.Init(volumeProvider);
                    waveOut.Play();

                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }

                Console.WriteLine("播放完成");

                // 清除臨時檔案
                File.Delete(tempInputPath);
                File.Delete(tempOutputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"播放過程發生錯誤: {ex.Message}");
            }
        }
        static public void PlayBase64Mp3(this string base64Data, float volume = 1.0f)
        {
            try
            {
                byte[] mp3Bytes = Convert.FromBase64String(base64Data);
                using (var ms = new MemoryStream(mp3Bytes))
                using (var mp3Reader = new Mp3FileReader(ms))
                using (var waveOut = new WaveOutEvent())
                {
                    var waveProvider = mp3Reader.ToSampleProvider();
                    var volumeProvider = new VolumeSampleProvider(waveProvider)
                    {
                        Volume = volume
                    };

                    waveOut.Init(volumeProvider);
                    waveOut.Play();

                    Console.WriteLine("開始播放 Base64 MP3...");

                    // 等待播放完成
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(1);
                    }

                    Console.WriteLine("Base64 MP3 播放完成");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"播放 Base64 MP3 發生錯誤: {ex.Message}");
            }
        }
        static public string GoogleSpeakerBase64(this string text, string language = "zh-tw")
        {
            string encodedText = Uri.EscapeDataString(text);
            string url = string.Format(
                "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen={0}&client=tw-ob&q={1}&tl={2}",
                text.Length, encodedText, language);
            Console.WriteLine($"Google TTS URL: {url}");

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    byte[] audioData = webClient.DownloadData(url);
                    Console.WriteLine($"下載完成，音檔大小：{audioData.Length} bytes");
                    return Convert.ToBase64String(audioData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("下載失敗: " + ex.Message);
                    return null;
                }
            }
        }
 

           // 自動產生 atempo 串接字串 (支援 >2 或 <0.5)
        private static string GenerateAtempoFilter(float speed)
        {
            if (speed < 0.5f)
            {
                var filters = new List<string>();
                float remaining = speed;
                while (remaining < 0.5f)
                {
                    filters.Add("atempo=0.5");
                    remaining /= 0.5f;
                }
                filters.Add($"atempo={remaining}");
                return string.Join(",", filters);
            }
            else if (speed > 2.0f)
            {
                var filters = new List<string>();
                float remaining = speed;
                while (remaining > 2.0f)
                {
                    filters.Add("atempo=2.0");
                    remaining /= 2.0f;
                }
                filters.Add($"atempo={remaining}");
                return string.Join(",", filters);
            }
            else
            {
                return $"atempo={speed}";
            }
        }
    }
}
