//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="CloudData.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace Utils.Data.Cloud
{
    using System.IO;
    using System.Text;
    using Dropbox.Api;
    using Dropbox.Api.Files;
    using UnityEngine;

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
    }
}