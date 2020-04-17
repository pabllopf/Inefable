//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="CloudData.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace Utils.Data.Cloud
{
    using Dropbox.Api;
    using Dropbox.Api.Files;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UnityEngine;
    using Utils.Debug;

    /// <summary>Manage the cloud data.</summary>
    public class CloudData
    {
        /// <summary>Save data in the cloud.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">Data to save</param>
        /// <param name="nameFile">Name file (default .JSON extension)</param>
        /// <param name="pathFile">Path file</param>
        /// <param name="user">The user.</param>
        public static void SaveInDropbox<T>(T data, string nameFile, string pathFile, User user)
        {
            new DropboxClient(user.AccessToken).Files.UploadAsync(pathFile + "/" + nameFile + ".json", WriteMode.Overwrite.Instance, body: new MemoryStream(Encoding.UTF8.GetBytes(JsonUtility.ToJson(data))));
        }

        /// <summary>Load data of cloud.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="nameFile">Name file (default .JSON extension)</param>
        /// <param name="pathFile">Path file</param>
        /// <param name="user">The user.</param>
        /// <returns>Return the data in the correct format</returns>
        public static T LoadOfDropbox<T>(string nameFile, string pathFile, User user)
        {
            return JsonUtility.FromJson<T>(new DropboxClient(user.AccessToken).Files.DownloadAsync(pathFile + "/" + nameFile + ".json").Result.GetContentAsStringAsync().Result);
        }

        /// <summary>Saves the in dropbox a folder.</summary>
        /// <param name="pathOfCloud">The path of cloud.</param>
        /// <param name="user">The user.</param>
        /// <param name="extensions">The extensions.</param>
        public static void SaveInDropboxAFolderAsync(string pathOfCloud, string pathToCopy, User user, List<string> extensions)
        {
            DropboxClient client = new DropboxClient(user.AccessToken);
            string[] entries = Directory.GetFileSystemEntries(pathToCopy, "*.*", SearchOption.AllDirectories);
            List<string> listFiles = entries.ToList().FindAll(i => extensions.Any(j => new FileInfo(i).Extension.Contains(j)));

            for (int i = 0; i < listFiles.Count; i++)
            {
                var task = Task.Run(() => Upload(client, pathOfCloud, new FileInfo(listFiles[i]).Name, listFiles[i]));
                task.Wait();
                Console.Print("File uploaded: " + pathOfCloud + "/" + new FileInfo(listFiles[i]).Name);
            }

            Console.Print("Finish to upload " + listFiles.Count + " files.");
        }

        /// <summary>Uploads the specified DBX.</summary>
        /// <param name="dbx">The DBX.</param>
        /// <param name="folder">The folder.</param>
        /// <param name="file">The file.</param>
        /// <param name="content">The content.</param>
        public static async Task Upload(DropboxClient dbx, string folder, string file, string content)
        {
            using (var mem = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                var updated = await dbx.Files.UploadAsync(
                    folder + "/" + file,
                    WriteMode.Overwrite.Instance,
                    body: mem);
            }
        }

        /// <summary>Loads the of dropbox a folder.</summary>
        /// <param name="pathOfCloud">The path of cloud.</param>
        /// <param name="pathToDownload">The path to download.</param>
        /// <param name="user">The user.</param>
        /// <param name="extensions">The extensions.</param>
        public static void LoadOfDropboxAFolder(string pathOfCloud, string pathToDownload, User user, List<string> extensions) 
        {
            DropboxClient client = new DropboxClient(user.AccessToken);
            ListFolderResult cloudList = client.Files.ListFolderAsync(pathOfCloud, true).Result;
            List<Metadata> listFiles = cloudList.Entries.ToList().FindAll(i => i.IsFile).FindAll(i => extensions.Any(j => i.Name.Contains(j)));

            for (int i = 0; i < listFiles.Count; i++) 
            {
                string path = pathToDownload + Path.GetDirectoryName(listFiles[i].PathLower);
                string pathWithFile = pathToDownload + listFiles[i].PathLower;
                if (!Directory.Exists(path)) 
                {
                    Directory.CreateDirectory(path);
                }

                if (File.Exists(pathWithFile)) 
                {
                    File.Delete(pathWithFile);
                }

                Stream stream = client.Files.DownloadAsync(listFiles[i].PathLower).Result.GetContentAsStreamAsync().Result;
                FileStream file = File.Create(pathWithFile);
                stream.CopyTo(file);
                file.Close();
            }
        }

        /// <summary>Numbers the of files in folder of dropbox.</summary>
        /// <param name="path">The path.</param>
        /// <returns>Return num of files in folder</returns>
        public static int NumOfFilesInFolderOfDropbox(string pathOfCloud, User user, List<string> extensions) 
        {
            DropboxClient client = new DropboxClient(user.AccessToken);
            ListFolderResult cloudList = client.Files.ListFolderAsync(pathOfCloud, true).Result;
            List<Metadata> listFiles = cloudList.Entries.ToList().FindAll(i => i.IsFile).FindAll(i => extensions.Any(j => i.Name.Contains(j)));
            return listFiles.Count;
        }
    }
}