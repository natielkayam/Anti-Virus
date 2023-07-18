using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Cyber_Func;


namespace Cyber_GUI
{
    public partial class Form1 : Form
    {
        bool IsExpert = false;
        public Form1()
        {
            InitializeComponent();
            // Subscribe to the DetectedListCountChanged event
            GuiHelper.DetectedListCountChanged += DetectedListCountChangedHandler;
        }

        // Event handler for DetectedListCountChanged event
        private void DetectedListCountChangedHandler(int count)
        {
            // Update the label with the new count value
            lblDisplayDetected.Text = $"Detected {count.ToString()} Virus\\Malware";
        }

        private void btnExpert_Click(object sender, EventArgs e)
        {
            GuiHelper.WriteUser("Click on 'Expert'");
            if (lblExpert.Text == "Regular Mode")
            {
                MessageBox.Show("Switch to EXPERT");
                lblExpert.Text = "Expert Mode";
                IsExpert = true;
            }
            else if (lblExpert.Text == "Expert Mode")
            {
                MessageBox.Show("Switch to Regular");
                lblExpert.Text = "Regular Mode";
                IsExpert = false;
            }
        }

        private void btnScanFile_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Scan scanForm = new Scan(this, "file",IsExpert);
            scanForm.Show(this);
            // write an information message to the log
            GuiHelper.WriteScan("Click on 'Scan File'");
        }
        private void btnScanDirectory_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Scan scanForm = new Scan(this, "folder", IsExpert);
            scanForm.Show();
            GuiHelper.WriteScan("Click on 'Scan Directory'");

        }

        private void btnLogFile_Click(object sender, EventArgs e)
        {
            GuiHelper.WriteUser("Click on 'Open Log'");
            GuiHelper.OpenLog();
            //shuld open the log in notpad.exe 
            //TODO: open it when the software keep the log (folder)
        }

        private void btnShowPastDetections_Click(object sender, EventArgs e)
        {
            GuiHelper.WriteUser("Click on 'Show Past Detections'");
            var detected = GuiHelper.GetDetected();

            // Create a new form to display past detections
            var pastDetectionsForm = new Form()
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterScreen
            };

            // Create a new DataGridView control and add it to the form
            var dataGridView = new DataGridView()
            {
                Dock = DockStyle.Fill
            };
            pastDetectionsForm.Controls.Add(dataGridView);

            // Set up the DataGridView columns
            dataGridView.Columns.Add("MD5", "MD5");
            dataGridView.Columns.Add("NAME", "NAME");
            dataGridView.Columns.Add("LOCATION", "LOCATION");
            dataGridView.Columns.Add("TIMESTAMP", "TIMESTAMP");
            dataGridView.Columns.Add("REASON", "REASON");

            // Add rows to the DataGridView control
            foreach (KeyValuePair<string, string[]> kvp in detected)
            {
                var row = new DataGridViewRow();
                row.Cells.Add(new DataGridViewTextBoxCell { Value = kvp.Key });
                foreach(var i in kvp.Value)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = i });
                }
                dataGridView.Rows.Add(row);
            }

            // Show the form as a pop-up dialog
            pastDetectionsForm.Show(this);
        }

        private void btnFileManger_Click(object sender, EventArgs e)
        {
            GuiHelper.WriteUser("Click on 'Start Monitoring'");
            MessageBox.Show("Started Monitoring");
            var f = GuiHelper.GetFileManager();
        }

        private void btnStartRegestry_Click(object sender, EventArgs e)
        {
            GuiHelper.WriteUser("Click on 'Check Startup Registry'");
            GuiHelper.CheckStartupRegistryEntries();
        }

        private void btnStopMonitor_Click(object sender, EventArgs e)
        {
            GuiHelper.WriteUser("Click on 'Stop Monitoring'");
            MessageBox.Show("Stoped Monitoring");
            GuiHelper.DisposeFileManager();
        }

        private void btnStartNetwork_Click(object sender, EventArgs e)
        {
            GuiHelper.WriteUser("Click on 'Start check Networks Ports'");
            MessageBox.Show("Started check Networks Ports");
            GuiHelper.StartNetworkCheck();
        }

        private void btnStopNetwork_Click(object sender, EventArgs e)
        {
            GuiHelper.WriteUser("Click on 'Stop check Networks Ports'");
            MessageBox.Show("Stoped check Networks Ports");
            GuiHelper.StopNetworkCheck();
        }
    }
}
