using System;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Reflection;
using System.Security.Principal;

namespace Cyber_Func
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Start check if the software run as Administrator");
                if (IsRunningAsAdministrator())
                {
                    RegisterKeyRun();
                }
                else
                {
                    Console.WriteLine("The software not ran as Administrator and no need to regestry key");
                }
                int pidFunc = 0;
                Process[] processes = Process.GetProcessesByName("Cyber_Func");
                if (processes.Length > 0)
                {
                    pidFunc = processes[0].Id;
                }
                //Console.WriteLine($"the PID of the software is {pidFunc}");

                Console.WriteLine("Intial Malicious class");
                //Malicious list to detect 
                Malicious malicious = Malicious.GetMaliciousInstance();
                Console.WriteLine("Malicious class intailzed succsesfully");

                Console.WriteLine("Intial FileManger class");
                //monitor changes in files
                FileManager watcher = FileManager.GetFileMangerInstance();
                Console.WriteLine("FileManger class intailzed succsesfully");

                Console.WriteLine("Intial StartupRegestry class");
                // Check startup registry
                StartupRegestry startCheck = StartupRegestry.GetStartupInstance();
                startCheck.CheckStartupRegistryEntries();
                Console.WriteLine("StartupRegestry class intailzed succsesfully");

                Console.WriteLine("Intial Log class");
                //Intial Log
                Log log = Log.GetLogInstance();
                Console.WriteLine("Log class intailzed succsesfully");

                Console.WriteLine("Intial NetworkManager class");
                // Create an instance of NetworkManager
                NetworkManager networkManager = NetworkManager.Instance;
                // Start monitoring the network
                networkManager.StartMonitoring();

                // Keep the application running
                Console.WriteLine("Network monitoring started. Press any key to stop.");
                Console.ReadKey();


                AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
                {
                    // Check if the current process is the console application
                    if (Process.GetCurrentProcess().Id == pidFunc)
                    {
                        log.DeleteLogFile();
                        // Stop monitoring and dispose of the NetworkManager
                        networkManager.StopMonitoring();
                        networkManager.Dispose();
                    }
                };
            }
            catch (Exception ex)
            {
                string exceptionMessage = $"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}";

            //Save the exception details to a file
                //string filePath = "C:\\Users\\NE185029\\Documents\\exception.txt";
                string filePath = "C:\\Users\\User\\Documents\\project\\Cyber_Func\\exception.txt";
                File.WriteAllText(filePath, exceptionMessage);
                Log log = Log.GetLogInstance();
                log.WriteError(ex.ToString());
                Console.WriteLine("Exception details saved to file: " + filePath);
            }

        }
        private static bool IsRunningAsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static void RegisterKeyRun()
        {
            const string keyName = @"Software\Microsoft\Windows\CurrentVersion\Run";
            const string valueName = "Cyber_Func";
            const string valueData = "C:\\Users\\User\\Documents\\project\\Cyber_Func\\Cyber_Func.exe";

            // Check if the registry key already exists
            using (var key = Registry.CurrentUser.OpenSubKey(keyName, true))
            {
                if (key != null && key.GetValue(valueName) != null)
                {
                    // Key already exists, so exit
                    Console.WriteLine("Key already exists in Registry.");
                    return;
                }
            }

            // Create the registry key with the value data
            using (var key = Registry.CurrentUser.CreateSubKey(keyName))
            {
                key.SetValue(valueName, valueData);
            }

            Console.WriteLine("Registry entry created successfully.");
        }

    }
}



