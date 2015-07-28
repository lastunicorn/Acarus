namespace ApplicationExit.Presentation.UI
{
    partial class TheDataView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelTheData = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelTheData
            // 
            this.labelTheData.BackColor = System.Drawing.Color.Silver;
            this.labelTheData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTheData.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTheData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelTheData.Location = new System.Drawing.Point(0, 0);
            this.labelTheData.Name = "labelTheData";
            this.labelTheData.Size = new System.Drawing.Size(150, 150);
            this.labelTheData.TabIndex = 0;
            this.labelTheData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TheDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelTheData);
            this.Name = "TheDataView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTheData;
    }
}
