//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="AutoUpdate.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace Utils.Updater
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using UnityEngine;
    using Utils.Data.Cloud;
    using Utils.Data.Local;
    
    /// <summary>Auto update necessary data.</summary>
    public class AutoUpdate
    {
        /// <summary>The default URL</summary>
        private string url = "https://pabllopf.github.io/Game-Inefable/version.html";

        /// <summary>The name folder</summary>
        private string nameFolder = string.Empty;

        /// <summary>The path folder</summary>
        private string pathFolder = string.Empty;

        /// <summary>Initializes a new instance of the <see cref="AutoUpdate"/> class.</summary>
        /// <param name="nameFolder">The name folder.</param>
        /// <param name="pathFolder">The path folder.</param>
        /// <param name="url">The URL.</param>
        public AutoUpdate(string nameFolder, string pathFolder, string url)
        {
            this.url = url;
            this.nameFolder = nameFolder;
            this.pathFolder = pathFolder;
        }

        /// <summary>Initializes a new instance of the <see cref="AutoUpdate"/> class.</summary>
        /// <param name="nameFolder">The name folder.</param>
        /// <param name="pathFolder">The path folder.</param>
        public AutoUpdate(string nameFolder, string pathFolder)
        {
            this.nameFolder = nameFolder;
            this.pathFolder = pathFolder;
        }

        /// <summary>Gets or sets the url.</summary>
        /// <value>The url.</value>
        public string Url { get => url; set => url = value; }

        /// <summary>Gets or sets the name folder.</summary>
        /// <value>The name folder.</value>
        public string NameFolder { get => nameFolder; set => nameFolder = value; }

        /// <summary>Gets or sets the path folder.</summary>
        /// <value>The path folder.</value>
        public string PathFolder { get => pathFolder; set => pathFolder = value; }

        /// <summary>Gets a value indicating whether [need update].</summary>
        /// <value>
        /// <c>true</c> if [need update]; otherwise, <c>false</c>.</value>
        public bool NeedUpdate => (LastVersion().Id > CurrentVersion().Id) ? true : false;

        /// <summary>Update now the config data of game.</summary>
        public void Now()
        {
            if (HaveInternetConnection())
            {
                if (NeedUpdate)
                {
                    LoadDataOfCloud();
                }
            }
        }

        /// <summary>Haves the internet connection.</summary>
        /// <returns>Return true if it is connected</returns>
        public bool HaveInternetConnection()
        {
            try
            {
                using (new WebClient().OpenRead("http://google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


        /// <summary>Numbers the of files.</summary>
        /// <param name="pathCloud">The path cloud.</param>
        /// <returns></returns>
        public int NumOfFiles(string pathCloud) 
        {
            return CloudData.NumOfFilesInFolderOfDropbox(pathCloud, new User(), new List<string>(new string[] { ".json", ".csv" }));
        }

        /// <summary>Lasts the version.</summary>
        /// <returns>Return the last version.</returns>
        public Version LastVersion()
        {
            return JsonUtility.FromJson<Version>(new WebClient().DownloadString(url));
        }

        /// <summary>Currents the version.</summary>
        /// <returns>Return the current version.</returns>
        public Version CurrentVersion()
        {
            if (LocalData.Exits("version", pathFolder))
            {
                return LocalData.Load<Version>("version", pathFolder);
            }
            else
            {
                Version result = new Version(0.01, string.Empty);
                LocalData.Save<Version>(result, "version", pathFolder);
                return result;
            }
        }

        /// <summary>Loads the data of cloud.</summary>
        private void LoadDataOfCloud()
        {
            if (Directory.Exists(pathFolder + "/" + nameFolder))
            {
                Directory.Delete(pathFolder + "/" + nameFolder, true);
            }

            CloudData.LoadOfDropboxAFolder(nameFolder, pathFolder, new User(), new List<string>(new string[] { ".json", ".csv" }));
            LocalData.Save<Version>(LastVersion(), "version", pathFolder);
        }
    }
}