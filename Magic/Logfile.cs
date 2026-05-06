using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;

namespace Magic
{
  

    public static class LogFile
    {
        private static readonly string _filePathLogfile = @"magicLogfile\Logfile.txt";

        public static void WriteLog(
            string message,
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0
        )
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message} (File: {Path.GetFileName(filePath)}, Line: {lineNumber})";
            File.AppendAllText(_filePathLogfile, logEntry + Environment.NewLine);
        }

       
    }

}