using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;


namespace Cyber_Func
{
    public class FileManager : IDisposable
    {
        private static FileManager instance;
        private FileSystemWatcher systemFilesWatcher;
        //private FileSystemWatcher AppDataWatcher;
        //private FileSystemWatcher EdgeWatcher;
        private FileSystemWatcher UserWatcher;
        private static readonly Log log = Log.GetLogInstance();
        private static readonly Malicious malicious = Malicious.GetMaliciousInstance();


        private FileManager()
        {
            systemFilesWatcher = CreateFileSystemWatcher(@"C:\Windows\System32");
            //AppDataWatcher = CreateFileSystemWatcher(@"C:\Users\User\AppData");
            //EdgeWatcher = CreateFileSystemWatcher(@"C:\Users\User\AppData\Local\Microsoft\Edge\User Data");
            UserWatcher = CreateFileSystemWatcher(@"C:\Users\User\");

        }

        private FileSystemWatcher CreateFileSystemWatcher(string path)
        {
            var watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.Filter = "*.exe";
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = NotifyFilters.LastWrite
                                   | NotifyFilters.FileName
                                   | NotifyFilters.DirectoryName;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;
            return watcher;
        }


        //singelton
        public static FileManager GetFileMangerInstance()
        {
            if(instance == null)
            {
                instance = new FileManager();
            }
            return instance;
        }
        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                // Check if the file's directory is in the excluded directories list
                if (IsExcludedDirectory(Path.GetDirectoryName(e.FullPath)))
                {
                    return; // Skip processing files in excluded directories
                }

                log.WriteAlert("File Created in : " + e.FullPath);
                string md5Hash = MD5_convert(e.FullPath);
                //MessageBox.Show($"{e.Name} Created in {e.FullPath} !!");
                bool maliciousResponse = malicious.IsMalicious(md5Hash, e.Name, e.FullPath);

                if (maliciousResponse)
                {
                    log.WriteAlert(e.Name + " is malicious !!!");
                    log.WriteInfo($"MD5 hash: {md5Hash}");
                    MessageBox.Show($"{e.Name} Malicious Created in {e.FullPath} !!");
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Access is denied, skip processing this file event
                return;
            }
            catch (Exception)
            {
                // Handle other exceptions that may occur during processing
                //Console.WriteLine("Exception occurred while processing file event: " + ex.Message);
                return;
            }
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            try
            {
                // Check if the file's directory is in the excluded directories list
                if (IsExcludedDirectory(Path.GetDirectoryName(e.FullPath)))
                {
                    return; // Skip processing files in excluded directories
                }
                //MessageBox.Show($"{e.Name} Deleted in {e.FullPath} !!");
                string md5Hash = MD5_convert(e.FullPath);
                bool maliciousResponse = malicious.IsMalicious(md5Hash, e.Name, e.FullPath);
                log.WriteInfo("File Deleted in : " + e.FullPath);

                if (maliciousResponse)
                {
                    log.WriteAlert(e.Name + " is malicious !!!");
                    log.WriteInfo($"MD5 hash: {md5Hash}");
                    MessageBox.Show($"{e.Name} Malicious Deleted in : {e.FullPath} !!");
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Access is denied, skip processing this file event
                return;
            }
            catch (Exception)
            {
                // Handle other exceptions that may occur during processing
                //Console.WriteLine("Exception occurred while processing file event: " + ex.Message);
                return;
            }
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            try
            {
                // Check if the file's directory is in the excluded directories list
                if (IsExcludedDirectory(Path.GetDirectoryName(e.FullPath)))
                {
                    return; // Skip processing files in excluded directories
                }
                //MessageBox.Show($"File Renamed from {e.OldName} to {e.Name}");
                string md5Hash = MD5_convert(e.FullPath);
                bool maliciousResponse = malicious.IsMalicious(md5Hash, e.Name, e.FullPath);
                log.WriteInfo($"File Renamed from {e.OldName} to {e.Name}");
                if (maliciousResponse)
                {
                    log.WriteAlert(e.Name + " is malicious !!!");
                    log.WriteInfo($"MD5 hash: {md5Hash}");
                    MessageBox.Show($"File Renamed from {e.OldName} to {e.Name}");
                }

            }
            catch (UnauthorizedAccessException)
            {
                // Access is denied, skip processing this file event
                return;
            }
            catch (Exception)
            {
                // Handle other exceptions that may occur during processing
                //Console.WriteLine("Exception occurred while processing file event: " + ex.Message);
                return;

            }
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                // Check if the file's directory is in the excluded directories list
                if (IsExcludedDirectory(Path.GetDirectoryName(e.FullPath)))
                {
                    return; // Skip processing files in excluded directories
                }

                //MessageBox.Show($"File changed: {e.FullPath}");
                string md5Hash = MD5_convert(e.FullPath);
                bool maliciousResponse = malicious.IsMalicious(md5Hash, e.Name, e.FullPath);
                log.WriteInfo($"File changed: {e.FullPath}");
                if (maliciousResponse)
                {
                    log.WriteAlert(e.Name + " is malicious !!!");
                    log.WriteInfo($"MD5 hash: {md5Hash}");
                    MessageBox.Show($"File changed: {e.FullPath} and its malicious");
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Access is denied, skip processing this file event
                return;
            }
            catch (Exception)
            {
                // Handle other exceptions that may occur during processing
                //Console.WriteLine("Exception occurred while processing file event: " + ex.Message);
                return;
            }
        }
        private string MD5_convert(string filePath)
        {
            // Calculate the MD5 hash of the file
            string md5Hash = string.Empty;
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    md5Hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
            return md5Hash;
        }
        private bool IsExcludedDirectory(string path)
        {
            string[] excludedDirectories = {
                        @"C:\Users\User\Documents\project\Cyber_Func",
                        @"C:\Users\User\Documents\project\Cyber_GUI",
                        @"C:\Users\User\Documents\project\Log" 
                                        };

            foreach (string excludedDirectory in excludedDirectories)
            {
                if (path.StartsWith(excludedDirectory, StringComparison.OrdinalIgnoreCase))
                {
                    return true; // Path is in excluded directories
                }
            }

            return false; // Path is not in excluded directories
        }
        public void Dispose()
        {
            systemFilesWatcher.Dispose();
            //AppDataWatcher.Dispose();
            //EdgeWatcher.Dispose();
            UserWatcher.Dispose();
    }
    }
}