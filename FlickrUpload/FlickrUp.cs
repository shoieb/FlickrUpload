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
        authBox authForm = new authBox();
        FolderSync folderSync = new FolderSync();
        public FlickrUp()
        {
            InitializeComponent();
        }             
        
        private void FlickrUp_Load(object sender, EventArgs e)
        {
            OAuthAccessToken token = null;
            try
            {
                token = Properties.Settings.Default.OAuthToken;
                if (token!=null)
                {
                    this.Hide();
                    folderSync.ShowDialog();
                    this.Close();
                    Application.Exit();
                }
            }
            catch (Exception)
            {

            }
        }
        private void buttonLoad_ok_Click(object sender, EventArgs e)
        {
            this.Hide();
            authForm.ShowDialog();
            this.Close();
            Application.Exit();
        }
    }
}
