//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="CloudData.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace Utils.Data.Cloud
{
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Text;
    using Dropbox.Api;
    using Dropbox.Api.Files;
    using Newtonsoft.Json;
    using Utils.ZIP;

    /// <summary>Manage the cloud data.</summary>
    public class CloudData
    {
        /// <summary>Save data in the cloud.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">Data to save</param>
        /// <param name="nameFile">Name file (default .JSON extension)</param>
        /// <param name="pathFile">Path file</param>
        /// <param name="user">The user.</param>
        public static void SaveInDropbox<T>(ref T data, string nameFile, string pathFile, User user)
        {
            new DropboxClient(user.AccessToken).Files.UploadAsync(pathFile + "/" + nameFile + ".json", WriteMode.Overwrite.Instance, body: new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data))));
        }

        /// <summary>Saves the zip in the cloud.</summary>
        /// <param name="pathToSave">The path to save.</param>
        /// <param name="nameFile">Name file (default .zip extension)</param>
        /// <param name="pathFile">The path file.</param>
        /// <param name="user">The user.</param>
        public static void SaveZipInDropbox(string pathToSave, string nameFile, string pathFile, User user) 
        {
            new DropboxClient(user.AccessToken).Files.UploadAsync(pathFile + "/" + nameFile + ".zip", WriteMode.Overwrite.Instance, body: Zip.Compress(pathToSave));
        }

        /// <summary>Load data of cloud.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="nameFile">Name file (default .JSON extension)</param>
        /// <param name="pathFile">Path file</param>
        /// <param name="user">The user.</param>
        /// <returns>Return the data in the correct format</returns>
        public static T LoadOfDropbox<T>(string nameFile, string pathFile, User user)
        {
            return JsonConvert.DeserializeObject<T>(new DropboxClient(user.AccessToken).Files.DownloadAsync(pathFile + "/" + nameFile + ".json").Result.GetContentAsStringAsync().Result);
        }

        /// <summary>Loads the zip of dropbox.</summary>
        /// <param name="nameFile">The name file.</param>
        /// <param name="pathFileInCloud">The path file in cloud.</param>
        /// <param name="pathDestination">The path destination.</param>
        /// <param name="user">The user.</param>
        public static void LoadZipOfDropbox(string nameFile, string pathFileInCloud, string pathDestination, User user)
        {
            Zip.Decompress(new DropboxClient(user.AccessToken).Files.DownloadAsync(pathFileInCloud + "/" + nameFile + ".zip").Result.GetContentAsStreamAsync().Result, pathDestination + ".zip", pathDestination);
        }
    }
}