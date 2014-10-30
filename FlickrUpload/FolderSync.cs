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

namespace FlickrUpload
{
    public partial class FolderSync : Form
    {
        OAuthAccessToken temp = Properties.Settings.Default.OAuthToken;
        
        List<PhotosetPhotoCollection> PhotoSets;

        FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

        public FolderSync()
        {
            InitializeComponent();
        }

        private void FolderSync_Load(object sender, EventArgs e)
        {
            rootFolderTextBox.Text = Properties.Settings.Default.userDefinedRootFolder;
        }

        private void browse_folder_Click(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string sourceDirectory = Properties.Settings.Default.userDefinedRootFolder;
                string destinationDirectory = folderBrowser.SelectedPath;
                //DirectoryInfo dirInfo = new DirectoryInfo(sourceDirectory);
                //string root = dirInfo.Name;                
                //string newdes = Path.Combine(destinationDirectory, root);
                string newdes = destinationDirectory + @"\FlickrBox";

                Properties.Settings.Default.userDefinedRootFolder = folderBrowser.SelectedPath + @"\FlickrBox";
                Properties.Settings.Default.Save();
                rootFolderTextBox.Text = Properties.Settings.Default.userDefinedRootFolder;
                try
                {
                    CopyFolder(sourceDirectory, newdes);
                    Directory.Delete(sourceDirectory,true);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }  

        private void sync_Click(object sender, EventArgs e)
        {
            dirSearch(rootFolderTextBox.Text);
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
            if (PhotoSets == null)
                PhotoSets = new List<PhotosetPhotoCollection>();

            var outputPhotoSet = PhotoSets.Where(set => set.PhotosetId == albumId).SingleOrDefault();

            if (outputPhotoSet == null)
            {
                outputPhotoSet = f.PhotosetsGetPhotos(albumId);
                PhotoSets.Add(outputPhotoSet);
            }
            return outputPhotoSet;
        }

        private void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }
    }
}
