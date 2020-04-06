namespace TheUpdater
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using UnityEngine.Networking;
    using System.ComponentModel;
    using System;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using System.Diagnostics;
    using System.Linq;

    public class Updater : MonoBehaviour
    {
        [SerializeField]
        private string urlOfLastVersion = "https://www.dropbox.com/s/ur7sc3uc1erm6v5/Inefable-002.zip?dl=1";

        private WebClient webClient;

        private BackgroundWorker bgWorker;

        private string exportLocation = "C:/Users/wwwam/Documents/Inefable";

        private string tempFilePath = "C:/Users/wwwam/Documents/Inefable" + "downloadAndUnzipTest.zip";

        [SerializeField]
        private Text textLog = null;

        private string currentVersion;

        private string lastVersion;

        private string gameCurrentVersionPath = string.Empty;
        private string gameLastVersionPath = string.Empty;

        private void Start()
        {
            currentVersion = Application.version;
            lastVersion = GetLastVersion();

            if (lastVersion.Equals("0.00")) 
            {
                SceneManager.LoadScene("MainMenu");
                return;
            }

            if (lastVersion != currentVersion)
            {
                UnityEngine.Debug.Log("Current version: " + currentVersion);
                UnityEngine.Debug.Log("Last Version: " + lastVersion);
                UnityEngine.Debug.Log("You have outdated");
                GameUpdate(urlOfLastVersion);
            }
            else 
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        private void GameUpdate(string urlOfLastVersion) 
        {
            Uri location = new Uri(urlOfLastVersion);

            webClient = new WebClient();
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(WebClientProgress);
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(WebClientFileCompleted);

            try
            {
                UnityEngine.Debug.LogError("Start to downloading...");
                textLog.text = "Start to downloading...";
                webClient.DownloadFileAsync(location, tempFilePath);
            }
            catch
            {
                UnityEngine.Debug.LogError("Error of connection.");
                textLog.text = "Error of connection.";
            }
        }


        public void WebClientProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            UnityEngine.Debug.Log("Process: " + e.ProgressPercentage);
            textLog.text = "Process: " + e.ProgressPercentage;
        }

        public void WebClientFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                UnityEngine.Debug.LogError("Error:: " + e.Error);
            }
            else if (e.Cancelled)
            {
                UnityEngine.Debug.LogError("Cancelled download:: " + e.Cancelled);
            }
            else
            {
                StartCoroutine(FinishDownload());
            }
        }

        private IEnumerator FinishDownload() 
        {
            UnityEngine.Debug.Log("Verifying download...");
            textLog.text = "Verifying download...";
            yield return new WaitForSeconds(0.5f);

            exportLocation = "C:/Users/wwwam/Documents/Inefable-" + lastVersion;

            if (!Directory.Exists(exportLocation)) 
            {
                Directory.CreateDirectory(exportLocation);
            }

            ZipUtil.Unzip(tempFilePath, exportLocation);
            textLog.text = "Download Complete";
            UnityEngine.Debug.Log("Download Complete");


            ProcessStartInfo start = new ProcessStartInfo();
            start.WindowStyle = ProcessWindowStyle.Maximized;
            start.FileName = exportLocation + "/Inefable-0.02-Setup.exe";
            Process.Start(start);

            Application.Quit();
        }

        private string GetLastVersion()
        {
            try
            {
                WebClient client = new WebClient();
                return client.DownloadString("https://pabllopf.github.io/Game-Inefable/version.html");

            }
            catch (WebException webEx)
            {
                UnityEngine.Debug.LogError(webEx.ToString());

                if (webEx.Status == WebExceptionStatus.ConnectFailure)
                {
                    UnityEngine.Debug.LogError("You havent got connection.");
                }

                return "0.00";
            }
        }
    }
}