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
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxAzzulVersion = new System.Windows.Forms.TextBox();
            this.checkBoxCheckAtStartUp = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Check Azzul";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxAzzulVersion
            // 
            this.textBoxAzzulVersion.Location = new System.Drawing.Point(93, 14);
            this.textBoxAzzulVersion.Name = "textBoxAzzulVersion";
            this.textBoxAzzulVersion.Size = new System.Drawing.Size(179, 20);
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
            this.groupBox1.Controls.Add(this.checkBoxCheckAtStartUp);
            this.groupBox1.Location = new System.Drawing.Point(12, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options Panel";
            // 
            // CoolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxAzzulVersion);
            this.Controls.Add(this.button1);
            this.Name = "CoolForm";
            this.Text = "Cool Application";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxAzzulVersion;
        private System.Windows.Forms.CheckBox checkBoxCheckAtStartUp;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

