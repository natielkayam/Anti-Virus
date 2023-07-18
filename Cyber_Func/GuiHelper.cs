using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Cyber_Func
{
    public class GuiHelper
    {
        private static readonly Log log = Log.GetLogInstance();
        private static readonly Malicious malicious = Malicious.GetMaliciousInstance();
        private static readonly NetworkManager networkManager = NetworkManager.Instance;
        private static readonly StartupRegestry StartupRegestry = StartupRegestry.GetStartupInstance();



        public static event Action<int> DetectedListCountChanged
        {
            add { malicious.DetectedListCountChanged += value; }
            remove { malicious.DetectedListCountChanged -= value; }
        }

        public static void WriteUser(string message)
        {
            log.WriteUser("[GUI] " + message);
        }
        public static void WriteInfo(string message)
        {
            log.WriteInfo("[GUI] " + message);
        }
        public static void WriteScan(string message)
        {
            log.WriteScan("[GUI] " + message);
        }
        public static void WriteAlert(string message)
        {
            log.WriteAlert("[GUI] " + message);
        }
        public static void WriteError(string message)
        {
            log.WriteError("[GUI] " + message);
        }
        public static void OpenLog()
        {
            log.ShowCopy();
        }

        public static bool IsMalicious(string input, string filename, string location)
        {
            return malicious.IsMalicious(input, filename, location);
        }
        public static Dictionary<string, string[]> GetDetected()
        {
            return malicious.GetDetected();
        }

        public static FileManager GetFileManager()
        {
            return FileManager.GetFileMangerInstance();
        }
        public static void DisposeFileManager()
        {
            FileManager.GetFileMangerInstance().Dispose();
        }
        public static void CheckStartupRegistryEntries()
        {
            StartupRegestry.CheckStartupRegistryEntries("gui");
        }
        public static void StartNetworkCheck()
        {
            networkManager.StartMonitoring();
        }
        public static void StopNetworkCheck()
        {
            networkManager.StopMonitoring();
        }
    }
}
