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
using System.IO;
using log4net;

namespace FlickrUpload
{
    public partial class FolderSync : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FolderSync));

        OAuthAccessToken temp;

        FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

        Timer appTimer = new Timer();

        
        public FolderSync()
        {
            InitializeComponent();
            rootFolderTextBox.Enabled = false;
        }

        private void FolderSync_Load(object sender, EventArgs e)
        {
            temp = Properties.Settings.Default.OAuthToken;
            Text = "FlickrUpload ( " + temp.Username + " )";
            rootFolderTextBox.Text = Properties.Settings.Default.userDefinedRootFolder;

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            
        }

        private void browse_folder_Click(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string sourceDirectory = Properties.Settings.Default.userDefinedRootFolder;
                string destinationDirectory = folderBrowser.SelectedPath;                
                //string newdes = destinationDirectory + @"\FlickrBox";

                Properties.Settings.Default.userDefinedRootFolder = folderBrowser.SelectedPath; //+@"\FlickrBox";
                Properties.Settings.Default.Save();
                rootFolderTextBox.Text = Properties.Settings.Default.userDefinedRootFolder;
                
            }
        }

        private void reset_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            this.Hide();
            FlickrUp frm = new FlickrUp();
            frm.ShowDialog();

        }

        private void sync_Click(object sender, EventArgs e)
        {
            appTimer.Interval = 3000;
            appTimer.Tick += new EventHandler(appTimer_tick);
            appTimer.Start();
        }

        private void syncCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            
            sync.Enabled = true;
            browse_folder.Enabled = true;
        }

        private void appTimer_tick(object sender, EventArgs e)
        {
            appTimer.Stop();

            sync.Enabled = false;
            browse_folder.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var f = FlickrManager.GetAuthInstance();
            dirSearch(rootFolderTextBox.Text, e);
                        
        }

        private void backgroundWorker1_RunWorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            appTimer.Start();
           
            if (e.Cancelled)
            {
                appTimer.Stop();
            }
            sync.Enabled = true;
            browse_folder.Enabled = true;
     
        }

        
        private void flickr_OnUploadProgress(object sender, FlickrNet.UploadProgressEventArgs e)
        {
            backgroundWorker1.ReportProgress(e.ProcessPercentage);
        }

        private void dirSearch(string searchPath, DoWorkEventArgs e)
        { 
            var dirconfigPath = searchPath + @"\config.ini";
            iniFile ini = new iniFile(dirconfigPath);
            var f = FlickrManager.GetAuthInstance();
            try
            {
                foreach (var photoPath in Directory.GetFiles(searchPath, "*.jpg"))
                {
                    photoUpload(photoPath, dirconfigPath, f, e);
                }
                foreach (var dirPath in Directory.GetDirectories(searchPath))
                {
                    dirSearch(dirPath, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Fatal(ex.Message, ex);
            }
        }

        private void photoUpload(string filePath, string configPath, Flickr f, DoWorkEventArgs e)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            var fileName = fileInfo.Name;
            var folderName = fileInfo.Directory.Name;
            iniFile ini = new iniFile(configPath);

            if (backgroundWorker1.CancellationPending == true)
            {
                e.Cancel = true;
            }

            if (!File.Exists(configPath))
            {
                var strPhotoId = f.UploadPicture(filePath, fileName, "sample", null, false, false, false);
                Photoset myset = f.PhotosetsCreate(folderName, strPhotoId);
                ini.IniWriteValue("info", "id", myset.PhotosetId);
            }
            else
            {
                try
                {
                    var albumId = ini.IniReadValue("info", "id");
                    var existingPhoto = GetPhotos(albumId, f).Where(p => p.Title.Contains(fileName)).SingleOrDefault();
                    if (existingPhoto == null)
                        f.PhotosetsAddPhoto(albumId, f.UploadPicture(filePath, fileName, "sample", null, false, false, false));
                }
                catch
                {
                    var strPhotoId = f.UploadPicture(filePath, fileName, "sample", null, false, false, false);
                    Photoset myset = f.PhotosetsCreate(folderName, strPhotoId);
                    ini.IniWriteValue("info", "id", myset.PhotosetId);
                }
            }            
        }

        private PhotosetPhotoCollection GetPhotos(string albumId, Flickr f)
        {
            var outputPhotoSet = f.PhotosetsGetPhotos(albumId);
            return outputPhotoSet;
        }

        
                       
    }
}
