using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlickrUpload
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string dirName = @"\FlickrBox";
            string syncPath = Properties.Settings.Default.userDefinedRootFolder;
            if (string.IsNullOrEmpty(syncPath))
            {
                Properties.Settings.Default.userDefinedRootFolder = path + dirName ;
                Properties.Settings.Default.Save();

                if (!Directory.Exists(path + dirName))
                {
                    Directory.CreateDirectory(path + dirName);
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FlickrUp());
        }
    }
}
