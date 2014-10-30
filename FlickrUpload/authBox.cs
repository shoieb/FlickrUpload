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
    public partial class authBox : Form
    {
        FolderSync folderSync = new FolderSync();
        private OAuthRequestToken requestToken;
        public authBox()
        {
            InitializeComponent();
        }

        private void authBox_Load(object sender, EventArgs e)
        {
            Flickr f = FlickrManager.GetInstance();
            requestToken = f.OAuthGetRequestToken("oob");
            string url = f.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Delete);
            System.Diagnostics.Process.Start(url);
        }

        private void buttonAuth_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(verifierTextBox.Text))
            {
                MessageBox.Show("You must enter the verifier code");
                return;
            }
            Flickr f = FlickrManager.GetInstance();
            try
            {
                var accessToken = f.OAuthGetAccessToken(requestToken, verifierTextBox.Text);
                FlickrManager.OAuthToken = accessToken;
                MessageBox.Show("Successfully authenticated as " + accessToken.FullName);
                this.Hide();
                folderSync.ShowDialog();
                this.Close();
                Application.Exit();

            }
            catch (FlickrApiException ex)
            {
                MessageBox.Show("Failed to get access token. Error message: " + ex.Message);
            }
        }
    }
}
