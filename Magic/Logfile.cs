using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;

namespace Magic
{
  

    public static class LogFile
    {
        public static void WriteLog(
            string message,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0
        )
        {
            string logFilePath = ConfigReader.Current.GetMagicLogFilePath();
            string? directoryName = Path.GetDirectoryName(logFilePath);
            if (!string.IsNullOrWhiteSpace(directoryName))
                Directory.CreateDirectory(directoryName);

            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message} (File: {Path.GetFileName(filePath)}, Line: {lineNumber})";
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
        }

       
    }

}
