namespace FlickrUpload
{
    partial class FlickrUp
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
            this.buttonLoad_ok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonLoad_ok
            // 
            this.buttonLoad_ok.Location = new System.Drawing.Point(117, 52);
            this.buttonLoad_ok.Name = "buttonLoad_ok";
            this.buttonLoad_ok.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad_ok.TabIndex = 0;
            this.buttonLoad_ok.Text = "OK";
            this.buttonLoad_ok.UseVisualStyleBackColor = true;
            this.buttonLoad_ok.Click += new System.EventHandler(this.buttonLoad_ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Press OK to authorize your Flickr acount";
            // 
            // FlickrUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 107);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonLoad_ok);
            this.Name = "FlickrUp";
            this.Text = "FlickrUpload";
            this.Load += new System.EventHandler(this.FlickrUp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLoad_ok;
        private System.Windows.Forms.Label label1;
    }
}

