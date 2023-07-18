
namespace Cyber_GUI
{
    partial class Scan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNameFile = new System.Windows.Forms.Label();
            this.lblFolderName = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // lblNameFile
            // 
            this.lblNameFile.AutoSize = true;
            this.lblNameFile.Location = new System.Drawing.Point(42, 51);
            this.lblNameFile.Name = "lblNameFile";
            this.lblNameFile.Size = new System.Drawing.Size(71, 20);
            this.lblNameFile.TabIndex = 0;
            this.lblNameFile.Text = "The File : ";
            this.lblNameFile.Visible = false;
            // 
            // lblFolderName
            // 
            this.lblFolderName.AutoSize = true;
            this.lblFolderName.Location = new System.Drawing.Point(42, 84);
            this.lblFolderName.Name = "lblFolderName";
            this.lblFolderName.Size = new System.Drawing.Size(86, 20);
            this.lblFolderName.TabIndex = 1;
            this.lblFolderName.Text = "The Folder :";
            this.lblFolderName.Visible = false;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(333, 357);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(209, 65);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Back to Main Window";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Scan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblFolderName);
            this.Controls.Add(this.lblNameFile);
            this.Name = "Scan";
            this.Text = "Scan";
            this.Load += new System.EventHandler(this.Scan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNameFile;
        private System.Windows.Forms.Label lblFolderName;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}