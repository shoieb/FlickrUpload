﻿namespace FlickrUpload
{
    partial class FolderSync
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.rootFolderTextBox = new System.Windows.Forms.TextBox();
            this.browse_folder = new System.Windows.Forms.Button();
            this.sync = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.reset = new System.Windows.Forms.Button();
            this.syncCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Root Floder";
            // 
            // rootFolderTextBox
            // 
            this.rootFolderTextBox.Location = new System.Drawing.Point(92, 49);
            this.rootFolderTextBox.Name = "rootFolderTextBox";
            this.rootFolderTextBox.Size = new System.Drawing.Size(223, 20);
            this.rootFolderTextBox.TabIndex = 1;
            // 
            // browse_folder
            // 
            this.browse_folder.Location = new System.Drawing.Point(321, 49);
            this.browse_folder.Name = "browse_folder";
            this.browse_folder.Size = new System.Drawing.Size(33, 23);
            this.browse_folder.TabIndex = 2;
            this.browse_folder.Text = "...";
            this.browse_folder.UseVisualStyleBackColor = true;
            this.browse_folder.Click += new System.EventHandler(this.browse_folder_Click);
            // 
            // sync
            // 
            this.sync.Location = new System.Drawing.Point(94, 75);
            this.sync.Name = "sync";
            this.sync.Size = new System.Drawing.Size(120, 23);
            this.sync.TabIndex = 3;
            this.sync.Text = "Sync";
            this.sync.UseVisualStyleBackColor = true;
            this.sync.Click += new System.EventHandler(this.sync_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "A folder named \'FlickrUpload\' will be created inside the folder you select.";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerComplete);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.appTimer_tick);
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(160, 126);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(75, 23);
            this.reset.TabIndex = 4;
            this.reset.Text = "Logout";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // syncCancel
            // 
            this.syncCancel.Location = new System.Drawing.Point(240, 75);
            this.syncCancel.Name = "syncCancel";
            this.syncCancel.Size = new System.Drawing.Size(75, 23);
            this.syncCancel.TabIndex = 5;
            this.syncCancel.Text = "Cancel";
            this.syncCancel.UseVisualStyleBackColor = true;
            this.syncCancel.Click += new System.EventHandler(this.syncCancel_Click);
            // 
            // FolderSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(415, 190);
            this.Controls.Add(this.syncCancel);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.sync);
            this.Controls.Add(this.browse_folder);
            this.Controls.Add(this.rootFolderTextBox);
            this.Controls.Add(this.label1);
            this.Name = "FolderSync";
            this.Text = "FlickrUpload";
            this.Load += new System.EventHandler(this.FolderSync_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox rootFolderTextBox;
        private System.Windows.Forms.Button browse_folder;
        private System.Windows.Forms.Button sync;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.Button syncCancel;
    }
}