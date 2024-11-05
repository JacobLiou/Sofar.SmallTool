using NLog;
using System.IO;

namespace BomTool.Utils
{
    public class NLogManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Initialize()
        {
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            // 加载NLog配置
            LogManager.Setup().LoadConfigurationFromFile("nlog.config");
        }

        public static void Trace(string message) => Logger.Trace(message);

        public static void Debug(string message) => Logger.Debug(message);

        public static void Info(string message) => Logger.Info(message);

        public static void Warn(string message) => Logger.Warn(message);

        public static void Error(string message) => Logger.Error(message);

        public static void Error(Exception ex, string? message = null)
        {
            if (string.IsNullOrEmpty(message))
                Logger.Error(ex);
            else
                Logger.Error(ex, message);
        }

        public static void Fatal(string message) => Logger.Fatal(message);
    }
}