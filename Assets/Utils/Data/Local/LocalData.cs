//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="LocalData.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace Utils.Data.Local
{
    using System.IO;
    using Newtonsoft.Json;
    using Utils.Security;

    /// <summary>Manage data</summary>
    public static class LocalData 
    {
        /// <summary>Saves the specified data in a file.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">Data to save</param>
        /// <param name="nameFile">Name file (default .JSON extension)</param>
        /// <param name="pathFile">Path file</param>
        public static void Save<T>(ref T data, string nameFile, string pathFile)
        {
            File.WriteAllText(pathFile + "/" + nameFile + ".json", JsonConvert.SerializeObject(data));
        }

        /// <summary>Saves the specified data in a file.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">Data to save</param>
        /// <param name="nameFile">Name file (default .JSON extension)</param>
        /// <param name="pathFile">Path file</param>
        /// <param name="encrypted">if set to <c>true</c> encrypt the data.</param>
        public static void Save<T>(ref T data, string nameFile, string pathFile, bool encrypted)
        {
            File.WriteAllText(pathFile + "/" + nameFile + ".json", encrypted ? Crypto.Encrypt(JsonConvert.SerializeObject(data)) : JsonConvert.SerializeObject(data));
        }

        /// <summary>Saves the specified data in a file.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">Data to save</param>
        /// <param name="nameFile">Name file (default .JSON extension)</param>
        /// <param name="pathFile">Path file</param>
        /// <param name="encrypted">if set to <c>true</c> encrypt the data.</param>
        /// <param name="key">Key to encrypt</param>
        public static void Save<T>(ref T data, string nameFile, string pathFile, bool encrypted, string key)
        {
            File.WriteAllText(pathFile + "/" + nameFile + ".json", encrypted ? Crypto.Encrypt(JsonConvert.SerializeObject(data), key) : JsonConvert.SerializeObject(data));
        }

        /// <summary>Saves the specified data.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="nameFile">Name file (without extension)</param>
        /// <param name="pathFile">Path file.</param>
        /// <param name="extension">Extension file (.JSON, .txt, etc)</param>
        public static void Save<T>(ref T data, string nameFile, string pathFile, string extension)
        {
            File.WriteAllText(pathFile + "/" + nameFile + extension, JsonConvert.SerializeObject(data));
        }

        /// <summary>Saves the specified data.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="nameFile">Name file (without extension)</param>
        /// <param name="pathFile">Path file.</param>
        /// <param name="extension">Extension file (.JSON, .txt, etc)</param>
        /// <param name="encrypted">if set to <c>true</c> encrypt the data.</param>
        public static void Save<T>(ref T data, string nameFile, string pathFile, string extension, bool encrypted)
        {
            File.WriteAllText(pathFile + "/" + nameFile + extension, encrypted ? Crypto.Encrypt(JsonConvert.SerializeObject(data)) : JsonConvert.SerializeObject(data));
        }

        /// <summary>Saves the specified data.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="nameFile">Name file (without extension)</param>
        /// <param name="pathFile">Path file.</param>
        /// <param name="extension">Extension file (.JSON, .txt, etc)</param>
        /// <param name="encrypted">if set to <c>true</c> encrypt the data.</param>
        /// <param name="key">Key to encrypt</param>
        public static void Save<T>(ref T data, string nameFile, string pathFile, string extension, bool encrypted, string key)
        {
            File.WriteAllText(pathFile + "/" + nameFile + extension, encrypted ? Crypto.Encrypt(JsonConvert.SerializeObject(data), key) : JsonConvert.SerializeObject(data));
        }

        /// <summary>Loads the specified name file.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="nameFile">Name file (default .JSON extension)</param>
        /// <param name="pathFile">Path file</param>
        /// <returns>Return the data in the correct format</returns>
        public static T Load<T>(string nameFile, string pathFile) 
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(pathFile + "/" + nameFile + ".json"));
        }

        /// <summary>Loads the specified name file.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="nameFile">Name file (default .JSON extension)</param>
        /// <param name="pathFile">Path file</param>
        /// <param name="encrypted">if set to <c>true</c> decrypt data</param>
        /// <returns>Return the data in the correct format</returns>
        public static T Load<T>(string nameFile, string pathFile, bool encrypted)
        {
            return JsonConvert.DeserializeObject<T>(encrypted ? Crypto.Decrypt(File.ReadAllText(pathFile + "/" + nameFile + ".json")) : File.ReadAllText(pathFile + "/" + nameFile + ".json"));
        }

        /// <summary>Loads the specified name file.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="nameFile">Name file (default .JSON extension)</param>
        /// <param name="pathFile">Path file</param>
        /// <param name="encrypted">if set to <c>true</c> decrypt data</param>
        /// <param name="key">Key to encrypt</param>
        /// <returns>Return the data in the correct format</returns>
        public static T Load<T>(string nameFile, string pathFile, bool encrypted, string key)
        {
            return JsonConvert.DeserializeObject<T>(encrypted ? Crypto.Decrypt(File.ReadAllText(pathFile + "/" + nameFile + ".json"), key) : File.ReadAllText(pathFile + "/" + nameFile + ".json"));
        }

        /// <summary>Loads the specified name file.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="nameFile">Name file (without extension)</param>
        /// <param name="pathFile">Path file.</param>
        /// <param name="extension">Extension file (.JSON, .txt, etc)</param>
        /// <returns>Return the data in the correct format</returns>
        public static T Load<T>(string nameFile, string pathFile, string extension)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(pathFile + "/" + nameFile + extension));
        }

        /// <summary>Loads the specified name file.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="nameFile">Name file (without extension)</param>
        /// <param name="pathFile">Path file.</param>
        /// <param name="extension">Extension file (.JSON, .txt, etc)</param>
        /// <param name="encrypted">if set to <c>true</c> [encrypted].</param>
        /// <returns>Return the data in the correct format</returns>
        public static T Load<T>(string nameFile, string pathFile, string extension, bool encrypted)
        {
            return JsonConvert.DeserializeObject<T>(encrypted ? Crypto.Decrypt(File.ReadAllText(pathFile + "/" + nameFile + extension)) : File.ReadAllText(pathFile + "/" + nameFile + extension));
        }

        /// <summary>Loads the specified name file.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="nameFile">Name file (without extension)</param>
        /// <param name="pathFile">Path file.</param>
        /// <param name="extension">Extension file (.JSON, .txt, etc)</param>
        /// <param name="encrypted">if set to <c>true</c> [encrypted].</param>
        /// <param name="key">Key to encrypt</param>
        /// <returns>Return the data in the correct format</returns>
        public static T Load<T>(string nameFile, string pathFile, string extension, bool encrypted, string key)
        {
            return JsonConvert.DeserializeObject<T>(encrypted ? Crypto.Decrypt(File.ReadAllText(pathFile + "/" + nameFile + extension)) : File.ReadAllText(pathFile + "/" + nameFile + extension));
        }
    }
}