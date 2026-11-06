using System;
using System.IO;

namespace BusinessLogic
{
    /// <summary>
    /// Central error log. Writes to &lt;app folder&gt;/logs/errors.log.
    /// Never throws: logging must not be the reason the app fails.
    /// </summary>
    public static class Logger
    {
        private static readonly object Sync = new();

        private static readonly string LogPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "errors.log");

        public static string LogFilePath => LogPath;

        public static void Error(string message, Exception? ex = null)
        {
            try
            {
                var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
                if (ex != null)
                    line += $" | {ex.GetType().Name}: {ex.Message}{Environment.NewLine}{ex.StackTrace}";

                lock (Sync)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(LogPath)!);
                    File.AppendAllText(LogPath, line + Environment.NewLine);
                }
            }
            catch
            {
                // Last resort: if the log itself cannot be written there is nowhere left to report it.
            }
        }
    }
}
