using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlickrNet;

namespace FlickrUpload
{
    public partial class FlickrUp : Form
    {
        OAuthAccessToken token = null;
        public FlickrUp()
        {
            InitializeComponent();
        }             
        
        private void FlickrUp_Load(object sender, EventArgs e)
        {            
            token = Properties.Settings.Default.OAuthToken;
            if (token!=null)
            {
                this.Hide();
                FolderSync folderSync = new FolderSync();
                folderSync.ShowDialog();
                this.Close();
             }            
        }

        private void buttonLoad_ok_Click(object sender, EventArgs e)
        {
            this.Hide();
            authBox authForm = new authBox();
            authForm.ShowDialog();
            this.Close();
            Application.Exit();
        }

    }
}
