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
        
        //List<PhotosetPhotoCollection> PhotoSets;

        FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

        //Timer appTimer = new Timer();
        
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
                //try
                //{
                //    CopyFolder(sourceDirectory, destinationDirectory);
                //    Directory.Delete(sourceDirectory, true);
                //}
                //catch(Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //    log.Fatal(ex.Message, ex);
                //}
            }
        }

        private void reset_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            this.Hide();
            FlickrUp frm = new FlickrUp();
            frm.ShowDialog();
            //this.Close();
        }

        private void sync_Click(object sender, EventArgs e)
        {            
            sync.Enabled = false;
            browse_folder.Enabled = false;
            backgroundWorker1.RunWorkerAsync();            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var f = FlickrManager.GetAuthInstance();            
            dirSearch(rootFolderTextBox.Text);
        }

        private void backgroundWorker1_RunWorkerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (MessageBox.Show("--Done!--") == DialogResult.OK)
            {
                sync.Enabled = true;
                browse_folder.Enabled = true;
            }      
        }

        
        private void flickr_OnUploadProgress(object sender, FlickrNet.UploadProgressEventArgs e)
        {
            backgroundWorker1.ReportProgress(e.ProcessPercentage);
        }

        private void dirSearch(string searchPath)
        {
            var dirconfigPath = searchPath + @"\config.ini";
            iniFile ini = new iniFile(dirconfigPath);
            var f = FlickrManager.GetAuthInstance();
            try
            {
                foreach (var photoPath in Directory.GetFiles(searchPath, "*.jpg"))
                {
                    photoUpload(photoPath, dirconfigPath, f);
                }
                foreach (var dirPath in Directory.GetDirectories(searchPath))
                {
                    dirSearch(dirPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Fatal(ex.Message, ex);
            }
        }

        private void photoUpload(string filePath, string configPath, Flickr f)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            var fileName = fileInfo.Name;
            var folderName = fileInfo.Directory.Name;
            iniFile ini = new iniFile(configPath);

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
            //if (PhotoSets == null)
            //    PhotoSets = new List<PhotosetPhotoCollection>();

            //var outputPhotoSet = PhotoSets.Where(set => set.PhotosetId == albumId).SingleOrDefault();

            //if (outputPhotoSet == null)
            //{
            //    outputPhotoSet = f.PhotosetsGetPhotos(albumId);
            //    PhotoSets.Add(outputPhotoSet);
            //}
            var outputPhotoSet = f.PhotosetsGetPhotos(albumId);
            return outputPhotoSet;
        }

        //private void CopyFolder(string sourceFolder, string destFolder)
        //{
        //    if (!Directory.Exists(destFolder))
        //        Directory.CreateDirectory(destFolder);
        //    string[] files = Directory.GetFiles(sourceFolder);
        //    foreach (string file in files)
        //    {
        //        string name = Path.GetFileName(file);
        //        string dest = Path.Combine(destFolder, name);
        //        File.Copy(file, dest);
        //    }
        //    string[] folders = Directory.GetDirectories(sourceFolder);
        //    foreach (string folder in folders)
        //    {
        //        string name = Path.GetFileName(folder);
        //        string dest = Path.Combine(destFolder, name);
        //        CopyFolder(folder, dest);
        //    }
        //}
                
    }
}
