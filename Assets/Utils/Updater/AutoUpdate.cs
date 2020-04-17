//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="AutoUpdate.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace Utils.Updater
{
    using System.Collections.Generic;
    using System.Net;
    using Utils.Data.Cloud;
    using Utils.Data.Local;
    using Newtonsoft.Json;
    using Utils.Debug;
    using System.Net.NetworkInformation;
    using System.IO;

    /// <summary>Auto update necessary data.</summary>
    public static class AutoUpdate
    {
        /// <summary>The default URL</summary>
        private static string url = "https://pabllopf.github.io/Game-Inefable/version.html";

        /// <summary>Gets or sets the url.</summary>
        /// <value>The url.</value>
        public static string Url { get => url; set => url = value; }
        
        /// <summary>Gets a value indicating whether [have connection].</summary>
        /// <value>
        /// <c>true</c> if [have connection]; otherwise, <c>false</c>.</value>
        public static bool HaveConnection => (new Ping().Send("www.google.com.mx").Status == IPStatus.Success) ? true : false;

        /// <summary>Gets the last version.</summary>
        /// <value>The last version.</value>
        private static Version LastVersion => JsonConvert.DeserializeObject<Version>(new WebClient().DownloadString(url));

        /// <summary>Update now the config data of game.</summary>
        public static void Now(string nameFolder, string pathFolder)
        {
            Console.Print("Auto Updater Active.");
            if (HaveConnection)
            {
                Console.Print("Have Connection.");
                if (LastVersion.Id > CurrentVersion(nameFolder, pathFolder).Id)
                {
                    Console.Print("Loading Files Of Cloud.");
                    LoadDataOfCloud(nameFolder, pathFolder);
                }
            }
        }

        /// <summary>Checks the update.</summary>
        /// <param name="nameFolder">The name folder.</param>
        /// <param name="pathFolder">The path folder.</param>
        /// <returns>Return is has update.</returns>
        public static bool CheckUpdate(string nameFolder, string pathFolder)
        {
            if (HaveConnection)
            {
                if (LastVersion.Id > CurrentVersion(nameFolder, pathFolder).Id)
                {
                    return false;
                }
                else 
                {
                    return true;
                }
            }
            return true;
        }

        /// <summary>Numbers the of files.</summary>
        /// <returns></returns>
        public static int NumOfFiles(string pathCloudFolder) 
        {
            return CloudData.NumOfFilesInFolderOfDropbox(pathCloudFolder, new User(), new List<string>(new string[] { ".json", ".csv" }));
        }

        /// <summary>Currents the version.</summary>
        /// <returns>Return the current version</returns>
        private static Version CurrentVersion(string nameFolder, string pathFolder) 
        {
            Version result;
            string path = pathFolder;

            if (LocalData.Exits("version", path))
            {
                result = LocalData.Load<Version>("version", path);
            }
            else 
            {
                result = new Version(0.01, "");
                LocalData.Save<Version>(result, "version", path);
            }

            return result;
        }

        /// <summary>Loads the data of cloud.</summary>
        private static void LoadDataOfCloud(string nameFolder, string pathFolder) 
        {
            if (Directory.Exists(pathFolder + "/" + nameFolder)) 
            {
                Directory.Delete(pathFolder + "/" + nameFolder, true);
            }

            CloudData.LoadOfDropboxAFolder(nameFolder, pathFolder, new User(), new List<string>(new string[] { ".json", ".csv" }));
            LocalData.Save<Version>(LastVersion, "version", pathFolder);
        }
    }
}
