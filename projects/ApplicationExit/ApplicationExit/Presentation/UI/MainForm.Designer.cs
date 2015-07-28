using ApplicationExit.Presentation.Controls;

namespace ApplicationExit.Presentation.UI
{
    partial class MainForm
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
            this.buttonSave = new CustomButton();
            this.buttonChange = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.theDataView = new TheDataView();
            this.customButtonExit = new CustomButton();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(12, 156);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(134, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.ViewModel = null;
            // 
            // buttonChange
            // 
            this.buttonChange.Location = new System.Drawing.Point(152, 20);
            this.buttonChange.Name = "buttonChange";
            this.buttonChange.Size = new System.Drawing.Size(75, 23);
            this.buttonChange.TabIndex = 2;
            this.buttonChange.Text = "Change";
            this.buttonChange.UseVisualStyleBackColor = true;
            this.buttonChange.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(152, 98);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // theDataView
            // 
            this.theDataView.BackColor = System.Drawing.Color.Gainsboro;
            this.theDataView.Location = new System.Drawing.Point(12, 20);
            this.theDataView.Name = "theDataView";
            this.theDataView.Size = new System.Drawing.Size(134, 130);
            this.theDataView.TabIndex = 5;
            this.theDataView.ViewModel = null;
            // 
            // customButtonExit
            // 
            this.customButtonExit.Location = new System.Drawing.Point(152, 127);
            this.customButtonExit.Name = "customButtonExit";
            this.customButtonExit.Size = new System.Drawing.Size(75, 23);
            this.customButtonExit.TabIndex = 4;
            this.customButtonExit.Text = "Exit 2";
            this.customButtonExit.UseVisualStyleBackColor = true;
            this.customButtonExit.ViewModel = null;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 241);
            this.Controls.Add(this.theDataView);
            this.Controls.Add(this.customButtonExit);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonChange);
            this.Controls.Add(this.buttonSave);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomButton buttonSave;
        private System.Windows.Forms.Button buttonChange;
        private System.Windows.Forms.Button buttonExit;
        private CustomButton customButtonExit;
        private TheDataView theDataView;
    }
}

