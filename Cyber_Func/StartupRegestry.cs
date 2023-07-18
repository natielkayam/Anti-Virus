using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Cyber_Func
{
    class StartupRegestry
    {
        public static StartupRegestry instance;
        private Malicious malicious;
        private Log log;

        private StartupRegestry()
        {
            this.malicious = Malicious.GetMaliciousInstance(); 
            this.log = Log.GetLogInstance();
        }
        public static StartupRegestry GetStartupInstance()
        {
            if (instance == null)
            {
                instance = new StartupRegestry();
            }
            return instance;
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
        
        public void CheckStartupRegistryEntries(string Sys = "console")
        {
            try
            {
                string registryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(registryKeyPath))
                {
                    if (registryKey != null)
                    {
                        bool foundMalicious = false;
                        string[] valueNames = registryKey.GetValueNames();
                        foreach (string valueName in valueNames)
                        {
                            string valueData = registryKey.GetValue(valueName)?.ToString();

                            if (!string.IsNullOrEmpty(valueData) && valueName != "Cyber_Func" && valueName != "OneDrive")
                            {
                                //Console.WriteLine(valueData.ToString());
                                //Console.WriteLine(valueName.ToString());
                                string md5Hash = MD5_convert(valueData);
                                bool isMalicious = malicious.IsMalicious(md5Hash, valueName, valueData);

                                if (isMalicious)
                                {
                                    foundMalicious = true;
                                    DeleteRegistryValueForFilePath(valueData);
                                    log.WriteAlert($"Malicious entry found in startup registry: {valueName} = {valueData} md5 = {md5Hash} ");
                                    log.WriteInfo($"Malicious entry has been deleted from startup registry: {valueName} = {valueData} md5 = {md5Hash} ");
                                    MessageBox.Show($"Malicious entry found in startup registry: {valueName} = {valueData} \n{valueName} has been deleted for your own safety");
                                }
                            }
                            //else
                            //{
                            //    MessageBox.Show($"{valueName} is not malicious in startup registry");
                            //}
                        }
                        //if activate from gui handle message to user that no malicious in startup
                        if (Sys == "gui" && !foundMalicious)
                        {
                            MessageBox.Show("No malicious in regestry");

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                // if sotware regestried in the regestry and after that it been deleted from the computer
                // it raised expection while checking if there is malicious software in regestry run
                //it happen cause it cant be checked with the hash of the file cause it do not exists 
                // solution : cath this expection and delete this spesific software from the regestry and then activate the cheking again
                HandleStartupRegestry(ex);
            }
        }
        private static void HandleStartupRegestry(Exception ex)
        {
            if (ex is FileNotFoundException || ex is FileLoadException)
            {
                // Extract the file path from the exception message
                int startIndex = ex.Message.IndexOf('\'') + 1;
                int endIndex = ex.Message.LastIndexOf('\'');
                string filePath = ex.Message.Substring(startIndex, endIndex - startIndex);

                // Delete the file from the registry and re-activate StartupRegestry
                if (DeleteRegistryValueForFilePath(filePath))
                {
                    Console.WriteLine($"deleted file from regestry that has been deleted from computer {filePath}");
                    // Successfully deleted the registry value, re-activate StartupRegestry
                    StartupRegestry startCheck = StartupRegestry.GetStartupInstance();
                    startCheck.CheckStartupRegistryEntries();
                }
                else
                {
                    // Failed to delete the registry value
                    Console.WriteLine("Failed to delete the registry value for the file: " + filePath);
                }
            }
            else
            {
                // Handle other exceptions
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private static bool DeleteRegistryValueForFilePath(string filePath)
        {
            bool success = false;

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                foreach (string valueName in key.GetValueNames())
                {
                    string valueData = key.GetValue(valueName)?.ToString();
                    if (!string.IsNullOrEmpty(valueData) && string.Equals(valueData, filePath, StringComparison.OrdinalIgnoreCase))
                    {
                        key.DeleteValue(valueName);
                        success = true;
                        break;
                    }
                }
            }
            return success;
        }

    }
}
