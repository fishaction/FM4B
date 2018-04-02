namespace FileManager4Broadcasting
{
    partial class DupFilesForm
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.allProgres = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 24);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(440, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(12, 9);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(50, 12);
            this.fileNameLabel.TabIndex = 1;
            this.fileNameLabel.Text = "fileName";
            // 
            // allProgres
            // 
            this.allProgres.AutoSize = true;
            this.allProgres.Location = new System.Drawing.Point(12, 50);
            this.allProgres.Name = "allProgres";
            this.allProgres.Size = new System.Drawing.Size(75, 12);
            this.allProgres.TabIndex = 2;
            this.allProgres.Text = "全体の進捗度";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(12, 65);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(440, 23);
            this.progressBar2.TabIndex = 3;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(364, 94);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(88, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "キャンセル(&C)";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // DupFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 129);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.allProgres);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DupFilesForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DupFilesForm_FormClosing);
            this.Load += new System.EventHandler(this.DupFilesForm_Load);
            this.Shown += new System.EventHandler(this.DupFilesForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Label allProgres;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Button cancelButton;
    }
}