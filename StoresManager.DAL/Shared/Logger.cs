using System;
using System.IO;

namespace StoresManager.DAL.Shared
{
    public class Logger
    {
        private static string _LogFilePath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs",
                    "AppLog.txt");
            }
        }
        private enum _enLogBehavior
        {
            ERROR,
            INFO,
            WARNING
        }
        static Logger()
        {
            string Folder = Path.GetDirectoryName(_LogFilePath);
            if (!Directory.Exists(Folder))
            {
                Directory.CreateDirectory(Folder);
            }
        }
        private static _enLogBehavior _LogBehavior;
        private static void _Log(StreamWriter Writer, _enLogBehavior bahavior,
            string message)
        {
            Writer.WriteLine("_____" + bahavior.ToString() + "_____");
            Writer.WriteLine("Date & Time: {0};", DateTime.Now.ToString("g"));
            Writer.WriteLine("Message: {0};", message);
        }
        public static void LogError(string message, Exception ex)
        {
            _LogBehavior = _enLogBehavior.ERROR;
            try
            {
                using (StreamWriter Writer = new StreamWriter(_LogFilePath, true))
                {
                    _Log(Writer, _LogBehavior, message);
                    Writer.WriteLine("Exception: {0};", ex.Message);
                    Writer.WriteLine("Stack Trace: {0};", ex.StackTrace);
                }
            }
            catch
            {
                //If logging fails, we silently ignore to avoid crashing the application
            }
        }
        public static void LogInfo(string message)
        {
            _LogBehavior = _enLogBehavior.INFO;
            try
            {
                using (StreamWriter Writer = new StreamWriter(_LogFilePath, true))
                {
                    _Log(Writer, _LogBehavior, message);
                }
            }
            catch
            {
                //If logging fails, we silently ignore to avoid crashing the application
            }
        }
        public static void LogWarning(string message)
        {
            _LogBehavior = _enLogBehavior.WARNING;
            try
            {
                using (StreamWriter Writer = new StreamWriter(_LogFilePath, true))
                {
                    _Log(Writer, _LogBehavior, message);
                }
            }
            catch
            {
                //If logging fails, we silently ignore to avoid crashing the application
            }
        }
    }
}
