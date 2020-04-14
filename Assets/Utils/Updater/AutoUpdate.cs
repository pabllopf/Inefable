//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="AutoUpdate.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace Utils.Updater
{
    using System;
    using System.ComponentModel;
    using System.Net;
    using UnityEngine;
    using Util.ZIP;
    using Utils.Data.Local;

    /// <summary>Auto update necessary data.</summary>
    public static class AutoUpdate
    {
        /// <summary>The default URL</summary>
        private static string url = "https://pabllopf.github.io/Game-Inefable/version.html";

        /// <summary>The temporary file</summary>
        private static string tempFile = Application.persistentDataPath + "/Resources/Inefable-Config.zip";

        /// <summary>The path to update data</summary>
        private static string pathToUpdateData = Application.persistentDataPath + "/Resources";

        /// <summary>The download progress</summary>
        private static float downloadProgress = 0;

        /// <summary>The updating</summary>
        private static bool updating = false;

        /// <summary>Gets or sets the url.</summary>
        /// <value>The url.</value>
        public static string Url { get => url; set => url = value; }

        /// <summary>Gets or sets the temporary file.</summary>
        /// <value>The temporary file.</value>
        public static string TempFile { get => tempFile; set => tempFile = value; }

        /// <summary>Gets or sets the path to update data.</summary>
        /// <value>The path to update data.</value>
        public static string PathToUpdateData { get => pathToUpdateData; set => pathToUpdateData = value; }

        /// <summary>Gets or sets the download progress.</summary>
        /// <value>The download progress.</value>
        public static float DownloadProgress { get => downloadProgress; set => downloadProgress = value; }

        /// <summary>Gets or sets a value indicating whether this <see cref="AutoUpdate"/> is updating.</summary>
        /// <value>
        /// <c>true</c> if updating; otherwise, <c>false</c>.</value>
        public static bool Updating { get => updating; set => updating = value; }

        /// <summary>Gets a value indicating whether [have connection].</summary>
        /// <value>
        /// <c>true</c> if [have connection]; otherwise, <c>false</c>.</value>
        private static bool HaveConnection => (Application.internetReachability == NetworkReachability.NotReachable) ? false : true;

        /// <summary>Gets the last version.</summary>
        /// <value>The last version.</value>
        private static Version LastVersion => JsonUtility.FromJson<Version>(new WebClient().DownloadString(url));

        /// <summary>Gets the current version.</summary>
        /// <value>The current version.</value>
        private static Version CurrentVersion => LocalData.Exits("Version", Application.persistentDataPath + "/Resources") ? LocalData.Load<Version>("Version", Application.persistentDataPath + "/Resources") : new Version(double.Parse(Application.version.Replace(".",",")), url);

        /// <summary>Update now the config data of game.</summary>
        public static void Now()
        {
            Debug.Log("Updater Active");
            if (HaveConnection)
            {
                Debug.Log("Is connected");
                if (LastVersion.Id > CurrentVersion.Id)
                {
                    Debug.Log("Need Update");
                    StartUpdate();
                }
            }
        }

        /// <summary>Starts the update.</summary>
        private static void StartUpdate() 
        {
            updating = true;

            WebClient webClient = new WebClient();
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(WebClientDownloadProgressChanged);
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(WebClientDownloadFileCompleted);

            try 
            {
                webClient.DownloadFileAsync(new Uri(LastVersion.Url), tempFile);
            } catch(Exception e)
            {
                Debug.Log(e);
            }
        }

        /// <summary>Webs the client download progress changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DownloadProgressChangedEventArgs"/> instance containing the event data.</param>
        private static void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Debug.Log("Updating: " + e.ProgressPercentage / 100 + "%");
        }


        /// <summary>Webs the client download file completed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="AsyncCompletedEventArgs"/> instance containing the event data.</param>
        private static void WebClientDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            FinishUpdate();
        }

        /// <summary>Finishes the update.</summary>
        private static void FinishUpdate() 
        {
            ZipUtil.Unzip(tempFile, pathToUpdateData);
            updating = false;
            Debug.Log("Updated.");
        }
    }
}
