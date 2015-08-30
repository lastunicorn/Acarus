using DustInTheWind.CoolApp.Utils;

namespace DustInTheWind.CoolApp
{
    partial class CoolForm
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
            this.buttonCheckAzzul = new System.Windows.Forms.Button();
            this.textBoxAzzulVersion = new System.Windows.Forms.TextBox();
            this.checkBoxCheckAtStartUp = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelNewVersion = new BindableToolStripStatusLabel();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCheckAzzul
            // 
            this.buttonCheckAzzul.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonCheckAzzul.Location = new System.Drawing.Point(3, 3);
            this.buttonCheckAzzul.Name = "buttonCheckAzzul";
            this.buttonCheckAzzul.Size = new System.Drawing.Size(75, 23);
            this.buttonCheckAzzul.TabIndex = 0;
            this.buttonCheckAzzul.Text = "Check Azzul";
            this.buttonCheckAzzul.UseVisualStyleBackColor = true;
            this.buttonCheckAzzul.Click += new System.EventHandler(this.HandleButtonCheckAzzulClick);
            // 
            // textBoxAzzulVersion
            // 
            this.textBoxAzzulVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAzzulVersion.Location = new System.Drawing.Point(84, 4);
            this.textBoxAzzulVersion.Name = "textBoxAzzulVersion";
            this.textBoxAzzulVersion.Size = new System.Drawing.Size(237, 20);
            this.textBoxAzzulVersion.TabIndex = 1;
            this.textBoxAzzulVersion.Text = "1.0.0.0";
            // 
            // checkBoxCheckAtStartUp
            // 
            this.checkBoxCheckAtStartUp.AutoSize = true;
            this.checkBoxCheckAtStartUp.Location = new System.Drawing.Point(6, 19);
            this.checkBoxCheckAtStartUp.Name = "checkBoxCheckAtStartUp";
            this.checkBoxCheckAtStartUp.Size = new System.Drawing.Size(141, 17);
            this.checkBoxCheckAtStartUp.TabIndex = 2;
            this.checkBoxCheckAtStartUp.Text = "Check version at startup";
            this.checkBoxCheckAtStartUp.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.checkBoxCheckAtStartUp);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 187);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options Panel";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.buttonCheckAzzul, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxAzzulVersion, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(324, 222);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(8);
            this.panel1.Size = new System.Drawing.Size(340, 238);
            this.panel1.TabIndex = 5;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatus,
            this.toolStripStatusLabelNewVersion});
            this.statusStrip1.Location = new System.Drawing.Point(0, 238);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(340, 24);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelNewVersion
            // 
            this.toolStripStatusLabelNewVersion.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabelNewVersion.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.toolStripStatusLabelNewVersion.Name = "toolStripStatusLabelNewVersion";
            this.toolStripStatusLabelNewVersion.Size = new System.Drawing.Size(115, 19);
            this.toolStripStatusLabelNewVersion.Text = "New version: 0.0.0.0";
            // 
            // toolStripStatusLabelStatus
            // 
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(210, 19);
            this.toolStripStatusLabelStatus.Spring = true;
            this.toolStripStatusLabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CoolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 262);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "CoolForm";
            this.Text = "Cool Application";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCheckAzzul;
        private System.Windows.Forms.TextBox textBoxAzzulVersion;
        private System.Windows.Forms.CheckBox checkBoxCheckAtStartUp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private BindableToolStripStatusLabel toolStripStatusLabelNewVersion;
    }
}

