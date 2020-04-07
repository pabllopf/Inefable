//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Updater.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace AutoUpdater
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Net;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    /// <summary>Auto update game.</summary>
    public class Updater : MonoBehaviour
    {
        /// <summary>The message</summary>
        [SerializeField]
        private Text message = null;

        /// <summary>The process</summary>
        [SerializeField]
        private Text process = null;

        /// <summary>The process bar</summary>
        [SerializeField]
        private Scrollbar processBar = null;

        /// <summary>The URL of server to get version</summary>
        [SerializeField]
        private string urlOfServerToGetVersion = "https://pabllopf.github.io/Game-Inefable/version.html";

        /// <summary>The temporary files directory</summary>
        private string temporaryFilesDirectory = string.Empty;

        /// <summary>The scene to load after check version</summary>
        private string sceneToLoadAfterCheckVersion = "MainMenu";

        /// <summary>The current version</summary>
        private Version currentVersion;

        /// <summary>The last version</summary>
        private Version lastVersion;

        #region Encapsulate Fields

        /// <summary>Gets or sets the current version.</summary>
        /// <value>The current version.</value>
        public Version CurrentVersion { get => currentVersion; set => currentVersion = value; }
        
        /// <summary>Gets or sets the last version.</summary>
        /// <value>The last version.</value>
        public Version LastVersion { get => lastVersion; set => lastVersion = value; }
        
        /// <summary>Gets or sets the URL of server to get version.</summary>
        /// <value>The URL of server to get version.</value>
        public string UrlOfServerToGetVersion { get => urlOfServerToGetVersion; set => urlOfServerToGetVersion = value; }

        /// <summary>Gets or sets the scene to load after check version.</summary>
        /// <value>The scene to load after check version.</value>
        public string SceneToLoadAfterCheckVersion { get => sceneToLoadAfterCheckVersion; set => sceneToLoadAfterCheckVersion = value; }

        /// <summary>Gets or sets the temporary files directory.</summary>
        /// <value>The temporary files directory.</value>
        public string TemporaryFilesDirectory { get => temporaryFilesDirectory; set => temporaryFilesDirectory = value; }

        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        public Text Message { get => message; set => message = value; }
        
        /// <summary>Gets or sets the process.</summary>
        /// <value>The process.</value>
        public Text Process { get => process; set => process = value; }

        /// <summary>Gets or sets the process bar.</summary>
        /// <value>The process bar.</value>
        public Scrollbar ProcessBar { get => processBar; set => processBar = value; }

        #endregion

        #region Properties 

        /// <summary>Gets a value indicating whether [have connection].</summary>
        /// <value>
        /// <c>true</c> if [have connection]; otherwise, <c>false</c>.</value>
        public bool HaveConnection => (Application.internetReachability == NetworkReachability.NotReachable) ? false : true;

        #endregion

        /// <summary>Starts this instance.</summary>
        private void Start()
        {
            InitComponents();

            currentVersion = GetTheCurrentVersion();

            if (GetArg("-outputDir") != null) 
            {
                StartCoroutine(FinishInstallation());
                return;
            }

            if (File.Exists(GetInstallationDirectory() + "/Inefable-Download.zip")) 
            {
                File.Delete(GetInstallationDirectory() + "/Inefable-Download.zip");
            }


            if (HaveConnection)
            {
                lastVersion = GetTheLatestVersion();

                if (Directory.Exists((GetInstallationDirectory() + "/Inefable-" + lastVersion.Id.ToString().Replace(',', '.'))))
                {
                    Directory.Delete(GetInstallationDirectory() + "/Inefable-" + lastVersion.Id.ToString().Replace(',', '.'), true);
                }

                if (currentVersion.Id < lastVersion.Id)
                {
                    UpdateTheGame();
                }
                else 
                {
                    SceneManager.LoadScene(sceneToLoadAfterCheckVersion);
                }
            }
            else 
            {
                SceneManager.LoadScene(sceneToLoadAfterCheckVersion);
            }
        }

        /// <summary>Initializes the components.</summary>
        private void InitComponents() 
        {
            message = GameObject.FindWithTag("message").GetComponent<Text>();
            process = GameObject.FindWithTag("process").GetComponent<Text>();
            processBar = GameObject.FindWithTag("processBar").GetComponent<Scrollbar>();
        }

        /// <summary>Gets the installation directory.</summary>
        /// <returns>Installation directory</returns>
        private string GetInstallationDirectory() 
        {
            string path = string.Empty;
            string[] dataPath = Application.dataPath.Split('/');
            for (int i = 0; i < dataPath.Length - 2; i++)
            {
                path += (i == dataPath.Length - 3) ? dataPath[i] : dataPath[i] + "/";
            }

            return path;
        }

        /// <summary>Gets the installation directory1.</summary>
        /// <returns>Installation directory</returns>
        private string GetInstallationDirectory1()
        {
            string path = string.Empty;
            string[] dataPath = Application.dataPath.Split('/');
            for (int i = 0; i < dataPath.Length - 1; i++)
            {
                path += (i == dataPath.Length - 2) ? dataPath[i] : dataPath[i] + "/";
            }

            return path;
        }

        /// <summary>Gets the latest version.</summary>
        /// <returns>Return the last version.</returns>
        private Version GetTheLatestVersion() 
        {
            try
            {
                return JsonUtility.FromJson<Version>(new WebClient().DownloadString(urlOfServerToGetVersion));
            }
            catch (WebException e)
            {
                Debug.LogError(e.ToString());
                return new Version(0, string.Empty);
            }
        }

        /// <summary>Gets the current version.</summary>
        /// <returns>Return the current version.</returns>
        private Version GetTheCurrentVersion() 
        {
            return new Version(double.Parse(Application.version.Replace('.', ',')), string.Empty);
        }

        /// <summary>Updates the game.</summary>
        private void UpdateTheGame() 
        {
            message.text = "Downloading update";

            temporaryFilesDirectory = GetInstallationDirectory() + "/Inefable-Download.zip";

            WebClient webClient = new WebClient();
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(WebClientDownloadProgressChanged);
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(WebClientDownloadFileCompleted);

            try
            {
                webClient.DownloadFileAsync(new Uri(lastVersion.Url), temporaryFilesDirectory);
            }
            catch (WebException e)
            {
                Debug.LogError("Error: " + e);
            }
        }

        /// <summary>Webs the client download progress changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DownloadProgressChangedEventArgs"/> instance containing the event data.</param>
        private void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            process.text = e.ProgressPercentage + "%";
            
            float size = e.ProgressPercentage;
            processBar.size = size / 100;
        }

        /// <summary>Webs the client download file completed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="AsyncCompletedEventArgs"/> instance containing the event data.</param>
        private void WebClientDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Debug.LogError("Error: " + e.Error);
                return;
            }
            
            if (e.Cancelled)
            {
                Debug.LogError("Cancel: " + e.Cancelled);
                return;
            }

            StartCoroutine(FinishUpdate());
        }

        /// <summary>Finishes the update.</summary>
        /// <returns>Return none</returns>
        private IEnumerator FinishUpdate() 
        {
            message.text = "Verifying download...";
            yield return new WaitForSeconds(0.2f);

            string exportToDir = GetInstallationDirectory() + "/Inefable-" + lastVersion.Id.ToString().Replace(',', '.');

            ZipUtil.Unzip(temporaryFilesDirectory, exportToDir);

            yield return new WaitForSeconds(0.2f);
            message.text = "Download Completed";

            
            float duration = 3f;                
            float totalTime = 0;
            while (totalTime <= duration)
            {
                totalTime += Time.deltaTime;
                int integer = (int)totalTime;
                int timeToShow = (int)duration - integer;
                message.text = "Restart application in " + timeToShow;
                yield return null;
            }

            Debug.Log(exportToDir + "/Inefable.exe -outputDir " + GetInstallationDirectory1());

            System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized,
                Arguments = "-outputDir " + GetInstallationDirectory1(),
                FileName = exportToDir + "/Inefable.exe" 
            };
            System.Diagnostics.Process.Start(start);

            yield return null;

            Application.Quit();
        }

        private IEnumerator FinishInstallation() 
        {
            message.text = "Finishing installation...";
            temporaryFilesDirectory = GetInstallationDirectory() + "/Inefable-Download.zip";

            ZipUtil.Unzip(temporaryFilesDirectory, GetArg("-outputDir"));

            processBar.size = 0.25f;
            process.text = "25%";

            yield return new WaitForSeconds(2f);
            message.text = "Deletting Temp Files";
            processBar.size = 0.50f;
            process.text = "50%";

            yield return new WaitForSeconds(2f);

            processBar.size = 1f;
            process.text = "100%";

            float duration = 3f;
            float totalTime = 0;
            while (totalTime <= duration)
            {
                totalTime += Time.deltaTime;
                int integer = (int)totalTime;
                int timeToShow = (int)duration - integer;
                message.text = "Restart application in " + timeToShow;
                yield return null;
            }

            System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized,
                FileName = GetArg("-outputDir") + "/Inefable.exe"
            };
            System.Diagnostics.Process.Start(start);

            yield return null;

            Application.Quit();

        }

        /// <summary>Gets the argument.</summary>
        /// <param name="name">The name.</param>
        /// <returns>Return the argument.</returns>
        private static string GetArg(string name)
        {
            var args = System.Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == name && args.Length > i + 1)
                {
                    return args[i + 1];
                }
            }
            return null;
        }

    }
}