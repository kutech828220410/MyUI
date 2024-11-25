using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
namespace Basic
{
    public class Logger
    {
        public static string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static string logDirectory = $"{currentDirectory}/log/";
        public static void Log(string Message)
        {
            string LogFileName = $"{DateTime.Now:yyyyMMdd-HH}.txt";
            string LogFilePath = Path.Combine(logDirectory, LogFileName);
            Directory.CreateDirectory(logDirectory);
            using (StreamWriter sw = File.AppendText(LogFilePath))
            {
                sw.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} - {Message}");
                Console.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} - {Message}");
            }
        }
     
        public static void Log(string Title ,string Message)
        {
            string LogFileName = $"{DateTime.Now:yyyyMMdd-HH}.txt";
            Log(LogFileName, Title, Message);
        }
        public static void Log(string fileName, string Title, string Message)
        {
            string LogFileName = $"{fileName}.txt";
            string LogFilePath = Path.Combine($"{logDirectory}{Title}/", LogFileName);
            Directory.CreateDirectory($"{logDirectory}{Title}/");
            using (StreamWriter sw = File.AppendText(LogFilePath))
            {
                sw.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} - {Message}");
                Console.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} - {Message}");
            }
        }
        public static void LogAddLine()
        {
            string Message = "---------------------------------------------------------------------------";
            string LogFileName = $"{DateTime.Now:yyyyMMdd-HH}.txt";
            string LogFilePath = Path.Combine(logDirectory, LogFileName);
            Directory.CreateDirectory(logDirectory);
            using (StreamWriter sw = File.AppendText(LogFilePath))
            {
                sw.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} - {Message}");
                Console.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} - {Message}");
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
                Console.WriteLine($"{Message}");
            }
        }

    }
}
