
namespace Cyber_GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDisplay = new System.Windows.Forms.Label();
            this.lblDisplayStatus = new System.Windows.Forms.Label();
            this.lblDisplayDetected = new System.Windows.Forms.Label();
            this.btnExpert = new System.Windows.Forms.Button();
            this.lblExpert = new System.Windows.Forms.Label();
            this.btnScanFile = new System.Windows.Forms.Button();
            this.btnScanDirectory = new System.Windows.Forms.Button();
            this.btnShowPastDetections = new System.Windows.Forms.Button();
            this.btnLogFile = new System.Windows.Forms.Button();
            this.btnFileManger = new System.Windows.Forms.Button();
            this.btnStartRegestry = new System.Windows.Forms.Button();
            this.btnStopMonitor = new System.Windows.Forms.Button();
            this.btnStartNetwork = new System.Windows.Forms.Button();
            this.btnStopNetwork = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.Location = new System.Drawing.Point(39, 378);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(171, 15);
            this.lblDisplay.TabIndex = 0;
            this.lblDisplay.Text = "Display System Status :  Runing";
            // 
            // lblDisplayStatus
            // 
            this.lblDisplayStatus.AutoSize = true;
            this.lblDisplayStatus.Location = new System.Drawing.Point(210, 40);
            this.lblDisplayStatus.Name = "lblDisplayStatus";
            this.lblDisplayStatus.Size = new System.Drawing.Size(0, 15);
            this.lblDisplayStatus.TabIndex = 1;
            // 
            // lblDisplayDetected
            // 
            this.lblDisplayDetected.AutoSize = true;
            this.lblDisplayDetected.Location = new System.Drawing.Point(516, 383);
            this.lblDisplayDetected.Name = "lblDisplayDetected";
            this.lblDisplayDetected.Size = new System.Drawing.Size(142, 15);
            this.lblDisplayDetected.TabIndex = 2;
            this.lblDisplayDetected.Text = "Detected 0 Virus\\Malware";
            // 
            // btnExpert
            // 
            this.btnExpert.Location = new System.Drawing.Point(241, 372);
            this.btnExpert.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExpert.Name = "btnExpert";
            this.btnExpert.Size = new System.Drawing.Size(130, 26);
            this.btnExpert.TabIndex = 3;
            this.btnExpert.Text = "Expert ON/OFF";
            this.btnExpert.UseVisualStyleBackColor = true;
            this.btnExpert.Click += new System.EventHandler(this.btnExpert_Click);
            // 
            // lblExpert
            // 
            this.lblExpert.AutoSize = true;
            this.lblExpert.Location = new System.Drawing.Point(399, 383);
            this.lblExpert.Name = "lblExpert";
            this.lblExpert.Size = new System.Drawing.Size(81, 15);
            this.lblExpert.TabIndex = 4;
            this.lblExpert.Text = "Regular Mode";
            // 
            // btnScanFile
            // 
            this.btnScanFile.Location = new System.Drawing.Point(399, 49);
            this.btnScanFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnScanFile.Name = "btnScanFile";
            this.btnScanFile.Size = new System.Drawing.Size(259, 48);
            this.btnScanFile.TabIndex = 5;
            this.btnScanFile.Text = "Scan File";
            this.btnScanFile.UseVisualStyleBackColor = true;
            this.btnScanFile.Click += new System.EventHandler(this.btnScanFile_Click);
            // 
            // btnScanDirectory
            // 
            this.btnScanDirectory.Location = new System.Drawing.Point(399, 120);
            this.btnScanDirectory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnScanDirectory.Name = "btnScanDirectory";
            this.btnScanDirectory.Size = new System.Drawing.Size(259, 50);
            this.btnScanDirectory.TabIndex = 6;
            this.btnScanDirectory.Text = "Scan Directory";
            this.btnScanDirectory.UseVisualStyleBackColor = true;
            this.btnScanDirectory.Click += new System.EventHandler(this.btnScanDirectory_Click);
            // 
            // btnShowPastDetections
            // 
            this.btnShowPastDetections.Location = new System.Drawing.Point(242, 317);
            this.btnShowPastDetections.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnShowPastDetections.Name = "btnShowPastDetections";
            this.btnShowPastDetections.Size = new System.Drawing.Size(238, 34);
            this.btnShowPastDetections.TabIndex = 7;
            this.btnShowPastDetections.Text = "Show Past Detections";
            this.btnShowPastDetections.UseVisualStyleBackColor = true;
            this.btnShowPastDetections.Click += new System.EventHandler(this.btnShowPastDetections_Click);
            // 
            // btnLogFile
            // 
            this.btnLogFile.Location = new System.Drawing.Point(66, 49);
            this.btnLogFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogFile.Name = "btnLogFile";
            this.btnLogFile.Size = new System.Drawing.Size(265, 50);
            this.btnLogFile.TabIndex = 8;
            this.btnLogFile.Text = "Open Log File";
            this.btnLogFile.UseVisualStyleBackColor = true;
            this.btnLogFile.Click += new System.EventHandler(this.btnLogFile_Click);
            // 
            // btnFileManger
            // 
            this.btnFileManger.Location = new System.Drawing.Point(399, 185);
            this.btnFileManger.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFileManger.Name = "btnFileManger";
            this.btnFileManger.Size = new System.Drawing.Size(259, 46);
            this.btnFileManger.TabIndex = 9;
            this.btnFileManger.Text = "Monitor Files";
            this.btnFileManger.UseVisualStyleBackColor = true;
            this.btnFileManger.Click += new System.EventHandler(this.btnFileManger_Click);
            // 
            // btnStartRegestry
            // 
            this.btnStartRegestry.Location = new System.Drawing.Point(66, 120);
            this.btnStartRegestry.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStartRegestry.Name = "btnStartRegestry";
            this.btnStartRegestry.Size = new System.Drawing.Size(265, 50);
            this.btnStartRegestry.TabIndex = 10;
            this.btnStartRegestry.Text = "Check StartUp Regestry";
            this.btnStartRegestry.UseVisualStyleBackColor = true;
            this.btnStartRegestry.Click += new System.EventHandler(this.btnStartRegestry_Click);
            // 
            // btnStopMonitor
            // 
            this.btnStopMonitor.Location = new System.Drawing.Point(399, 251);
            this.btnStopMonitor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStopMonitor.Name = "btnStopMonitor";
            this.btnStopMonitor.Size = new System.Drawing.Size(259, 46);
            this.btnStopMonitor.TabIndex = 11;
            this.btnStopMonitor.Text = "Stop Monitor Files";
            this.btnStopMonitor.UseVisualStyleBackColor = true;
            this.btnStopMonitor.Click += new System.EventHandler(this.btnStopMonitor_Click);
            // 
            // btnStartNetwork
            // 
            this.btnStartNetwork.Location = new System.Drawing.Point(66, 185);
            this.btnStartNetwork.Name = "btnStartNetwork";
            this.btnStartNetwork.Size = new System.Drawing.Size(265, 46);
            this.btnStartNetwork.TabIndex = 12;
            this.btnStartNetwork.Text = "Start check Networks Ports";
            this.btnStartNetwork.UseVisualStyleBackColor = true;
            this.btnStartNetwork.Click += new System.EventHandler(this.btnStartNetwork_Click);
            // 
            // btnStopNetwork
            // 
            this.btnStopNetwork.Location = new System.Drawing.Point(66, 251);
            this.btnStopNetwork.Name = "btnStopNetwork";
            this.btnStopNetwork.Size = new System.Drawing.Size(265, 46);
            this.btnStopNetwork.TabIndex = 13;
            this.btnStopNetwork.Text = "Stop check Networks Ports";
            this.btnStopNetwork.UseVisualStyleBackColor = true;
            this.btnStopNetwork.Click += new System.EventHandler(this.btnStopNetwork_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 421);
            this.Controls.Add(this.btnStopNetwork);
            this.Controls.Add(this.btnStartNetwork);
            this.Controls.Add(this.btnStopMonitor);
            this.Controls.Add(this.btnStartRegestry);
            this.Controls.Add(this.btnFileManger);
            this.Controls.Add(this.btnLogFile);
            this.Controls.Add(this.btnShowPastDetections);
            this.Controls.Add(this.btnScanDirectory);
            this.Controls.Add(this.btnScanFile);
            this.Controls.Add(this.lblExpert);
            this.Controls.Add(this.btnExpert);
            this.Controls.Add(this.lblDisplayDetected);
            this.Controls.Add(this.lblDisplayStatus);
            this.Controls.Add(this.lblDisplay);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Ruppin Anti-Virus";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.Label lblDisplayStatus;
        private System.Windows.Forms.Label lblDisplayDetected;
        private System.Windows.Forms.Button btnExpert;
        private System.Windows.Forms.Label lblExpert;
        private System.Windows.Forms.Button btnScanFile;
        private System.Windows.Forms.Button btnScanDirectory;
        private System.Windows.Forms.Button btnShowPastDetections;
        private System.Windows.Forms.Button btnLogFile;
        private System.Windows.Forms.Button btnFileManger;
        private System.Windows.Forms.Button btnStartRegestry;
        private System.Windows.Forms.Button btnStopMonitor;
        private System.Windows.Forms.Button btnStartNetwork;
        private System.Windows.Forms.Button btnStopNetwork;
    }
}

