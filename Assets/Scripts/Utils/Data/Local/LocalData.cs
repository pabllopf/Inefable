//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="LocalData.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace Utils.Data.Local
{
    using System.ComponentModel;
    using System.IO;
    using System.Text;
    using UnityEngine;
    using Utils.Security;

    /// <summary>Manage data</summary>
    public static class LocalData 
    {
        /// <summary>Saves the specified data in a file.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">Data to save</param>
        /// <param name="nameFile">Name file (default .JSON extension)</param>
        /// <param name="pathFile">Path file</param>
        public static void Save<T>(T data, string nameFile, string pathFile)
        {
            if (!Directory.Exists(pathFile)) 
            {
                Directory.CreateDirectory(pathFile);
            }

            File.WriteAllText(pathFile + "/" + nameFile + ".json", JsonUtility.ToJson(data), Encoding.UTF8);
        }

        /// <summary>Saves the specified data in a file.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">Data to save</param>
        /// <param name="nameFile">Name file (default .JSON extension)</param>
        /// <param name="pathFile">Path file</param>
        /// <param name="encrypted">if set to <c>true</c> encrypt the data.</param>
        public static void Save<T>(T data, string nameFile, string pathFile, bool encrypted)
        {
            if (!Directory.Exists(pathFile))
            {
                Directory.CreateDirectory(pathFile);
            }

            File.WriteAllText(pathFile + "/" + nameFile + ".json", encrypted ? Crypto.Encrypt(JsonUtility.ToJson(data)) : JsonUtility.ToJson(data));
        }

        /// <summary>Saves the specified data in a file.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">Data to save</param>
        /// <param name="nameFile">Name file (default .JSON extension)</param>
        /// <param name="pathFile">Path file</param>
        /// <param name="encrypted">if set to <c>true</c> encrypt the data.</param>
        /// <param name="key">Key to encrypt</param>
        public static void Save<T>(T data, string nameFile, string pathFile, bool encrypted, string key)
        {
            if (!Directory.Exists(pathFile))
            {
                Directory.CreateDirectory(pathFile);
            }

            File.WriteAllText(pathFile + "/" + nameFile + ".json", encrypted ? Crypto.Encrypt(JsonUtility.ToJson(data), key) : JsonUtility.ToJson(data));
        }

        /// <summary>Saves the specified data.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="nameFile">Name file (without extension)</param>
        /// <param name="pathFile">Path file.</param>
        /// <param name="extension">Extension file (.JSON, .txt, etc)</param>
        public static void Save<T>(T data, string nameFile, string pathFile, string extension)
        {
            if (!Directory.Exists(pathFile))
            {
                Directory.CreateDirectory(pathFile);
            }

            File.WriteAllText(pathFile + "/" + nameFile + extension, JsonUtility.ToJson(data));
        }

        /// <summary>Saves the specified data.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="nameFile">Name file (without extension)</param>
        /// <param name="pathFile">Path file.</param>
        /// <param name="extension">Extension file (.JSON, .txt, etc)</param>
        /// <param name="encrypted">if set to <c>true</c> encrypt the data.</param>
        public static void Save<T>(T data, string nameFile, string pathFile, string extension, bool encrypted)
        {
            if (!Directory.Exists(pathFile))
            {
                Directory.CreateDirectory(pathFile);
            }

            File.WriteAllText(pathFile + "/" + nameFile + extension, encrypted ? Crypto.Encrypt(JsonUtility.ToJson(data)) : JsonUtility.ToJson(data));
        }

        /// <summary>Saves the specified data.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="nameFile">Name file (without extension)</param>
        /// <param name="pathFile">Path file.</param>
        /// <param name="extension">Extension file (.JSON, .txt, etc)</param>
        /// <param name="encrypted">if set to <c>true</c> encrypt the data.</param>
        /// <param name="key">Key to encrypt</param>
        public static void Save<T>(T data, string nameFile, string pathFile, string extension, bool encrypted, string key)
        {
            if (!Directory.Exists(pathFile))
            {
                Directory.CreateDirectory(pathFile);
            }

            File.WriteAllText(pathFile + "/" + nameFile + extension, encrypted ? Crypto.Encrypt(JsonUtility.ToJson(data), key) : JsonUtility.ToJson(data));
        }

        /// <summary>Loads the specified name file.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="nameFile">Name file (default .JSON extension)</param>
        /// <param name="pathFile">Path file</param>
        /// <returns>Return the data in the correct format</returns>
        public static T Load<T>(string nameFile, string pathFile) 
        {
            return JsonUtility.FromJson<T>(File.ReadAllText(pathFile + "/" + nameFile + ".json"));
        }

        /// <summary>Loads the specified name file.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="nameFile">Name file (default .JSON extension)</param>
        /// <param name="pathFile">Path file</param>
        /// <param name="encrypted">if set to <c>true</c> decrypt data</param>
        /// <returns>Return the data in the correct format</returns>
        public static T Load<T>(string nameFile, string pathFile, bool encrypted)
        {
            return JsonUtility.FromJson<T>(encrypted ? Crypto.Decrypt(File.ReadAllText(pathFile + "/" + nameFile + ".json")) : File.ReadAllText(pathFile + "/" + nameFile + ".json"));
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
            return JsonUtility.FromJson<T>(encrypted ? Crypto.Decrypt(File.ReadAllText(pathFile + "/" + nameFile + ".json"), key) : File.ReadAllText(pathFile + "/" + nameFile + ".json"));
        }

        /// <summary>Loads the specified name file.</summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="nameFile">Name file (without extension)</param>
        /// <param name="pathFile">Path file.</param>
        /// <param name="extension">Extension file (.JSON, .txt, etc)</param>
        /// <returns>Return the data in the correct format</returns>
        public static T Load<T>(string nameFile, string pathFile, string extension)
        {
            return JsonUtility.FromJson<T>(File.ReadAllText(pathFile + "/" + nameFile + extension));
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
            return JsonUtility.FromJson<T>(encrypted ? Crypto.Decrypt(File.ReadAllText(pathFile + "/" + nameFile + extension)) : File.ReadAllText(pathFile + "/" + nameFile + extension));
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
            return JsonUtility.FromJson<T>(encrypted ? Crypto.Decrypt(File.ReadAllText(pathFile + "/" + nameFile + extension)) : File.ReadAllText(pathFile + "/" + nameFile + extension));
        }

        /// <summary>Exit the specified name file.</summary>
        /// <param name="nameFile">The name file.</param>
        /// <param name="pathFile">The path file.</param>
        /// <returns>Return true if exit a local data.</returns>
        public static bool Exits(string nameFile, string pathFile) 
        {
            return File.Exists(pathFile + "/" + nameFile + ".json");
        }

        /// <summary>Exit the specified name file.</summary>
        /// <param name="nameFile">The name file.</param>
        /// <param name="pathFile">The path file.</param>
        /// <param name="extension">The extension.</param>
        /// <returns>Return true if exit a local data.</returns>
        public static bool Exits(string nameFile, string pathFile, string extension)
        {
            return File.Exists(pathFile + "/" + nameFile + extension);
        }
    }
}