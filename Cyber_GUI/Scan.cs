using System;
using System.Windows.Forms;
using System.IO;
using Cyber_Func;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace Cyber_GUI
{
    public partial class Scan : Form
    {
        
        private Form1 mainform;
        private string browse;
        private bool IsExpert;

        public Scan(Form1 mainform, string browse,bool IsExpert)
        {
            InitializeComponent();
            this.mainform = mainform;
            this.browse = browse;
            this.IsExpert = IsExpert;
            
        }

        private void Scan_Load(object sender, EventArgs e)
        {
            if (this.browse == "file")
            {
                // Show the file dialog and wait for the user to select a file
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog1.FileName;

                    // Extract the file name from the path and display it in the label
                    string fileName = Path.GetFileNameWithoutExtension(selectedFile);
                    string filePath = Path.GetFullPath(selectedFile);
                    //ScanFile(selectedFile);
                    lblNameFile.Text = "the file is : " + fileName;
                    lblNameFile.Visible = true;

                    string md5Hash = MD5_convert(filePath);
                    bool maliciousResponse = GuiHelper.IsMalicious(md5Hash,fileName,filePath);

                    if (maliciousResponse)
                    {
                        if (IsExpert)
                        {
                            GuiHelper.WriteAlert(fileName + "is malicious !!!");
                            GuiHelper.WriteInfo($"the MD5 is : {md5Hash}");
                            MessageBox.Show($"{fileName} Malicious !!/nthe MD5 is : {md5Hash}/nthe path is : {filePath}");
                        }
                        else
                        {
                            GuiHelper.WriteAlert(fileName + "is malicious !!!");
                            GuiHelper.WriteInfo($"the MD5 is : {md5Hash}");
                            MessageBox.Show($"{fileName} Malicious !!");
                        }
                        
                    }
                }
            }
            else if (this.browse == "folder")
            {
                // Show the folder dialog and wait for the user to select a folder
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    // Do something with the selected folder, such as iterating through its files
                    string selectedFolder = folderBrowserDialog1.SelectedPath;

                    // Extract the folder name from the path and display it in the label
                    string folderName = new DirectoryInfo(selectedFolder).Name;
                    // Iterate through the files in the folder
                    lblFolderName.Text = "Selected folder: " + folderName;
                    lblFolderName.Visible = true;
                    int countMalic = 0;
                    int countNonMalic = 0;
                    foreach (string file in Directory.GetFiles(selectedFolder))
                    {
                        // Extract the file name from the path and display it in the label
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        string filePath = Path.GetFullPath(file);

                        string md5Hash = MD5_convert(filePath);
                        bool maliciousResponse = GuiHelper.IsMalicious(md5Hash, fileName,filePath);

                        if (maliciousResponse)
                        {
                            countMalic++;
                            if (IsExpert)
                            {
                                GuiHelper.WriteAlert(fileName + " in " + selectedFolder + " is malicious !!!");
                                GuiHelper.WriteInfo($"The hash of {fileName} is {md5Hash}");
                                GuiHelper.WriteInfo($"The path of {fileName} is {filePath}");
                                MessageBox.Show($"{fileName} in {selectedFolder} Malicious !!/nthe MD5 is : {md5Hash}/nthe path is : {filePath}");
                            }
                            else
                            {
                                GuiHelper.WriteAlert(fileName + " in " + selectedFolder + " is malicious !!!");
                                MessageBox.Show($"{fileName} in {selectedFolder} Malicious !!");
                            }
                        }
                    }
                    GuiHelper.WriteInfo($"Found {countMalic} Malicious files and {countNonMalic} NOT Malicious files in Directory {selectedFolder}");
                }
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            mainform.Show();
            mainform.Enabled = true;
            this.Close();
            GuiHelper.WriteUser("Click on Back to main window");
        }
    }
}
