using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace Cyber_Func
{
    public class NetworkManager : IDisposable
    {
        private Malicious malicious;
        private Log log;
        private Timer monitoringTimer;

        private NetworkManager()
        {
            this.malicious = Malicious.GetMaliciousInstance();
            this.log = Log.GetLogInstance();
        }

        // Singleton
        private static NetworkManager instance;
        public static NetworkManager Instance => instance ?? (instance = new NetworkManager());

        public void StartMonitoring(int intervalInSeconds = 5)
        {
            StopMonitoring(); // Stop any previous monitoring

            monitoringTimer = new Timer(intervalInSeconds * 1000); // Convert interval to milliseconds
            monitoringTimer.Elapsed += async (sender, e) => await MonitorNetworkPorts();
            monitoringTimer.AutoReset = true;
            monitoringTimer.Start();
        }

        public void StopMonitoring()
        {
            monitoringTimer?.Stop();
            monitoringTimer?.Dispose();
        }

        private async Task MonitorNetworkPorts()
        {
            await Task.Run(() =>
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "netstat";
                    startInfo.Arguments = "-ano -a"; // Add -a option to display all active connections
                    startInfo.RedirectStandardOutput = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;

                    using (Process process = new Process())
                    {
                        process.StartInfo = startInfo;
                        process.OutputDataReceived += (sender, e) =>
                        {
                            if (!string.IsNullOrEmpty(e.Data))
                            {
                                ProcessNetworkPortData(e.Data);
                            }
                        };

                        process.Start();
                        process.BeginOutputReadLine();
                        process.WaitForExit();
                    }
                }
                catch (Exception ex)
                {
                    string errorMessage = "Exception occurred while monitoring network ports: " + ex.Message;
                    //Console.WriteLine(errorMessage);
                    log.WriteAlert(errorMessage);
                }
            });
        }

        private void ProcessNetworkPortData(string data)
        {
            try
            {
                string[] columns = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (columns.Length >= 4 && columns[0] == "TCP")
                {
                    string localAddress = columns[1];
                    string foreignAddress = columns[2];
                    string state = columns[3];

                    int processId = int.Parse(columns[4]);

                    try
                    {
                        Process process = Process.GetProcessById(processId);
                        string md5 = MD5Convert(process.MainModule.FileName);
                        string name = process.ProcessName;
                        bool isMalicious = malicious.IsMalicious(md5, name, process.MainModule.FileName);

                        log.WriteInfo($"Checking network\nLocal: {localAddress} | Foreign: {foreignAddress} | State: {state}\n" +
                            $"Process Name: {process.ProcessName} | Process ID: {process.Id}\n" +
                            $"File Path: {process.MainModule.FileName}\nMD5: {md5}");

                        if (isMalicious)
                        {
                            process.Kill();
                            System.Windows.Forms.MessageBox.Show($"Process Name: {process.ProcessName} | Process ID: {process.Id}\n" +
                                $"File Path: {process.MainModule.FileName} is malicious and making contact to {foreignAddress}");
                            System.Windows.Forms.MessageBox.Show($"{name} is closed due to malicious action !");
                            log.WriteAlert(name + " is malicious and trying to make network contact!!!");
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // Access is denied, skip processing
                        return;
                    }
                    catch (Exception ex)
                    {
                        string errorMessage = "Exception occurred while retrieving process details: " + ex.Message;
                        //Console.WriteLine(errorMessage);
                        log.WriteAlert(errorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Exception occurred while processing network port data: " + ex.Message;
                Console.WriteLine(errorMessage);
                log.WriteAlert(errorMessage);
            }
        }

        private string MD5Convert(string filePath)
        {
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

        public void Dispose()
        {
            StopMonitoring();
        }
    }
}

