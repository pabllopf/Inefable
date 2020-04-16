//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="AutoUpdate.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace Utils.Updater
{
    using System.Collections.Generic;
    using System.Net;
    using UnityEngine;
    using Utils.Data.Cloud;
    using Utils.Data.Local;

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
        private static bool HaveConnection => (Application.internetReachability == NetworkReachability.NotReachable) ? false : true;

        /// <summary>Gets the last version.</summary>
        /// <value>The last version.</value>
        private static Version LastVersion => JsonUtility.FromJson<Version>(new WebClient().DownloadString(url));

        /// <summary>Gets the current version.</summary>
        /// <value>The current version.</value>
        private static Version CurrentVersion => LocalData.Exits("version", Application.persistentDataPath + "/resources") ? LocalData.Load<Version>("version", Application.persistentDataPath + "/resources") : new Version(double.Parse(Application.version.Replace(".", ",")), url);

        /// <summary>Update now the config data of game.</summary>
        public static void Now()
        {
            if (HaveConnection)
            {
                if (LastVersion.Id > CurrentVersion.Id)
                {
                    CloudData.LoadOfDropboxAFolder("/resources", Application.persistentDataPath, new User(), new List<string>(new string[] { ".json", ".csv" }));
                }
            }
        }
    }
}
