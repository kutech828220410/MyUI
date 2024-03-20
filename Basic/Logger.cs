using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Basic
{
    public class Logger
    {
        private static string logDirectory = "log/";
        public static void Log(string Message)
        {
            string LogFileName = $"{DateTime.Now:yyyyMMdd-HH}.txt";
            string LogFilePath = Path.Combine(logDirectory, LogFileName);
            Directory.CreateDirectory(logDirectory);
            using (StreamWriter sw = File.AppendText(LogFilePath))
            {
                sw.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} - {Message}");
            }

        }

        public static void Log(string Title ,string Message)
        {
            string LogFileName = $"{DateTime.Now:yyyyMMdd-HH}.txt";
            string LogFilePath = Path.Combine($"{logDirectory}{Title}/", LogFileName);
            Directory.CreateDirectory($"{logDirectory}{Title}/");
            using (StreamWriter sw = File.AppendText(LogFilePath))
            {
                sw.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} - {Message}");
            }
        }
        public static void LogAddLine(string Title)
        {
            LogAddLine(Title, "---------------------------------------------------------------------------");
        }
        public static void LogAddLine(string Title, string Message)
        {
            string LogFileName = $"{DateTime.Now:yyyyMMdd-HH}.txt";
            string LogFilePath = Path.Combine($"{logDirectory}{Title}/", LogFileName);
            Directory.CreateDirectory($"{logDirectory}{Title}/");
            using (StreamWriter sw = File.AppendText(LogFilePath))
            {
                sw.WriteLine($"{Message}");
            }
        }

    }
}
