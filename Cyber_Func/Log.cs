using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Cyber_Func
{
    class Log
    {
        private static Log instance;
        private readonly string logFilePath;
        private readonly object lockObject = new object();

        private Log()
        {
            //this.logFilePath = @"C:\Users\NE185029\Documents\LOG.txt";
            this.logFilePath = @"C:\Users\User\Documents\project\Log\LOG.txt";
        }

        public static Log GetLogInstance()
        {
            if (instance == null)
            {
                instance = new Log();
            }
            return instance;
        }

        internal void WriteInfo(string message)
        {
            lock (lockObject)
            {
                // write the message to the log file
                using (var writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now}] [INFO] {message}");
                }
            }
        }
        internal void WriteUser(string message)
        {
            lock (lockObject)
            {
                // write the message to the log file
                using (var writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now}] [USER] {message}");
                }
            }
        }
        internal void WriteScan(string message)
        {
            lock (lockObject)
            {
                // write the message to the log file
                using (var writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now}] [SCAN] {message}");
                }
            }
        }
        internal void WriteAlert(string message)
        {
            lock (lockObject)
            {
                // write the message to the log file
                using (var writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now}] [ALERT] {message}");
                }
            }
        }
        internal void WriteError(string message)
        {
            lock (lockObject)
            {
                // write the message to the log file
                using (var writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now}] [ERROR] {message}");
                }
            }
        }
        internal void ShowCopy(string destination = @"C:\Users\User\Documents\project\Log\log_copy.txt")
        {
            lock (lockObject)
            {                                                                 
                // Copy the original log file to the copy file
                File.Copy(this.logFilePath, destination, true);
                // Open the copy file
                ProcessStartInfo startInfo = new ProcessStartInfo(destination);
                startInfo.UseShellExecute = true;
                var p = Process.Start(startInfo);
                // Wait for the user to close the file
                p.WaitForExit();
                File.Delete(destination);
            }
        }
        internal void DeleteLogFile()
        {
            lock (lockObject)
            {
                if (File.Exists(logFilePath))
                {
                    File.Delete(logFilePath);
                }
            }
        }

    }
}
