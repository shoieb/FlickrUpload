namespace FlickrUpload
{
    partial class authBox
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
            this.label1 = new System.Windows.Forms.Label();
            this.verifierTextBox = new System.Windows.Forms.TextBox();
            this.buttonAuth_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter Code";
            // 
            // verifierTextBox
            // 
            this.verifierTextBox.Location = new System.Drawing.Point(84, 43);
            this.verifierTextBox.Name = "verifierTextBox";
            this.verifierTextBox.Size = new System.Drawing.Size(177, 20);
            this.verifierTextBox.TabIndex = 1;
            // 
            // buttonAuth_ok
            // 
            this.buttonAuth_ok.Location = new System.Drawing.Point(170, 80);
            this.buttonAuth_ok.Name = "buttonAuth_ok";
            this.buttonAuth_ok.Size = new System.Drawing.Size(75, 23);
            this.buttonAuth_ok.TabIndex = 2;
            this.buttonAuth_ok.Text = "OK";
            this.buttonAuth_ok.UseVisualStyleBackColor = true;
            this.buttonAuth_ok.Click += new System.EventHandler(this.buttonAuth_ok_Click);
            // 
            // authBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 153);
            this.Controls.Add(this.buttonAuth_ok);
            this.Controls.Add(this.verifierTextBox);
            this.Controls.Add(this.label1);
            this.Name = "authBox";
            this.Text = "authBox";
            this.Load += new System.EventHandler(this.authBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox verifierTextBox;
        private System.Windows.Forms.Button buttonAuth_ok;
    }
}