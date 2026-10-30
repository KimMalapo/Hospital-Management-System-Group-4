using System;
using System.IO;

namespace BusinessLogic
{
    public static class Logger
    {
        private static readonly string LogPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "errors.log");

        public static void Error(string message, Exception? ex = null)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LogPath)!);
                var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
                if (ex != null)
                    line += $" | {ex.GetType().Name}: {ex.Message}";
                File.AppendAllText(LogPath, line + Environment.NewLine);
            }
            catch { }
        }
    }
}