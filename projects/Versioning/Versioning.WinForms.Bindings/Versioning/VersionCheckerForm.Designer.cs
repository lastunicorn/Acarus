// Acarus
// Copyright (C) 2015 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace DustInTheWind.Versioning.WinForms.Mvp.Versioning
{
    partial class VersionCheckerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionCheckerForm));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelLineBottom = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonCheckAgain = new System.Windows.Forms.Button();
            this.buttonOpenDownloadedFile = new System.Windows.Forms.Button();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelStatusText = new System.Windows.Forms.Label();
            this.labelLineTop = new System.Windows.Forms.Label();
            this.checkBoxCheckAtStartup = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.MarqueeAnimationSpeed = 50;
            this.progressBar1.Name = "progressBar1";
            // 
            // labelLineBottom
            // 
            resources.ApplyResources(this.labelLineBottom, "labelLineBottom");
            this.labelLineBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelLineBottom.Name = "labelLineBottom";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelLineBottom, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panelContent, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelStatusText, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelLineTop, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxCheckAtStartup, 0, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.buttonClose);
            this.flowLayoutPanel1.Controls.Add(this.buttonCheckAgain);
            this.flowLayoutPanel1.Controls.Add(this.buttonOpenDownloadedFile);
            this.flowLayoutPanel1.Controls.Add(this.buttonDownload);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonCheckAgain
            // 
            resources.ApplyResources(this.buttonCheckAgain, "buttonCheckAgain");
            this.buttonCheckAgain.Name = "buttonCheckAgain";
            this.buttonCheckAgain.UseVisualStyleBackColor = true;
            this.buttonCheckAgain.Click += new System.EventHandler(this.buttonCheckAgain_Click);
            // 
            // buttonOpenDownloadedFile
            // 
            resources.ApplyResources(this.buttonOpenDownloadedFile, "buttonOpenDownloadedFile");
            this.buttonOpenDownloadedFile.Name = "buttonOpenDownloadedFile";
            this.buttonOpenDownloadedFile.UseVisualStyleBackColor = true;
            this.buttonOpenDownloadedFile.Click += new System.EventHandler(this.buttonOpenDownloadedFile_Click);
            // 
            // buttonDownload
            // 
            resources.ApplyResources(this.buttonDownload, "buttonDownload");
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.tableLayoutPanel2);
            resources.ApplyResources(this.panelContent, "panelContent");
            this.panelContent.Name = "panelContent";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.labelInfo, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.progressBar1, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // labelInfo
            // 
            resources.ApplyResources(this.labelInfo, "labelInfo");
            this.labelInfo.Name = "labelInfo";
            // 
            // labelStatusText
            // 
            resources.ApplyResources(this.labelStatusText, "labelStatusText");
            this.labelStatusText.BackColor = System.Drawing.Color.White;
            this.labelStatusText.Name = "labelStatusText";
            // 
            // labelLineTop
            // 
            resources.ApplyResources(this.labelLineTop, "labelLineTop");
            this.labelLineTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelLineTop.Name = "labelLineTop";
            // 
            // checkBoxCheckAtStartup
            // 
            resources.ApplyResources(this.checkBoxCheckAtStartup, "checkBoxCheckAtStartup");
            this.checkBoxCheckAtStartup.Name = "checkBoxCheckAtStartup";
            this.checkBoxCheckAtStartup.UseVisualStyleBackColor = true;
            this.checkBoxCheckAtStartup.CheckedChanged += new System.EventHandler(this.checkBoxCheckAtStartup_CheckedChanged);
            // 
            // VersionCheckerForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VersionCheckerForm";
            this.ShowInTaskbar = false;
            this.Shown += new System.EventHandler(this.VersionCheckerForm_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelLineBottom;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelStatusText;
        private System.Windows.Forms.Label labelLineTop;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonCheckAgain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox checkBoxCheckAtStartup;
        private System.Windows.Forms.Button buttonOpenDownloadedFile;
    }
}