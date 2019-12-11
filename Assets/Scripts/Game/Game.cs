//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Game.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

/// <summary>Manage the game.</summary>
public class Game : MonoBehaviour
{
    /// <summary>The key</summary>
    private static readonly string key = "dgwn`SCN7342!/(&2-.MSDUOUQsdbasd3e435";

    /// <summary>Resets this instance.</summary>
    public static void Reset()
    {
        Settings.Current = new Settings();
        Game.Save();
    }

    /// <summary>Saves this instance.</summary>
    public static void Save()
    {
        Encrypt(JsonUtility.ToJson(Settings.Current), key, Application.persistentDataPath + "/Setting.json");
    }

    /// <summary>Loads this instance.</summary>
    public static void Load()
    {
        Settings.Current = new Settings();
        if (File.Exists(Application.persistentDataPath + "/Setting.json"))
        {
            Settings.Current = JsonUtility.FromJson<Settings>(Decrypt(key, Application.persistentDataPath + "/Setting.json"));
        }
        else
        {
            Encrypt(JsonUtility.ToJson(Settings.Current), key, Application.persistentDataPath + "/Setting.json");
            Settings.Current = JsonUtility.FromJson<Settings>(Decrypt(key, Application.persistentDataPath + "/Setting.json"));
        }
    }

    /// <summary>Encrypts the specified data.</summary>
    /// <param name="data">The data.</param>
    /// <param name="key">The key.</param>
    /// <param name="pathFile">The path file.</param>
    private static void Encrypt(string data, string key, string pathFile)
    {
        byte[] stringToEncrypt = UTF8Encoding.UTF8.GetBytes(data);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        byte[] keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        hashmd5.Clear();

        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform desEncrypt = tdes.CreateEncryptor();
        byte[] stringEncrypt = desEncrypt.TransformFinalBlock(stringToEncrypt, 0, stringToEncrypt.Length);

        File.WriteAllText(pathFile, Convert.ToBase64String(stringEncrypt));
        tdes.Clear();
    }

    /// <summary>Decrypts the specified key.</summary>
    /// <param name="key">The key.</param>
    /// <param name="pathFile">The path file.</param>
    /// <returns>The data decrypted</returns>
    private static string Decrypt(string key, string pathFile)
    {
        string stringEncrypt = File.ReadAllText(pathFile);

        byte[] arrayEncrypt = Convert.FromBase64String(stringEncrypt);

        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        byte[] keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        hashmd5.Clear();

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform desEncrypt = tdes.CreateDecryptor();
        string result = Encoding.UTF8.GetString(desEncrypt.TransformFinalBlock(arrayEncrypt, 0, arrayEncrypt.Length));
        tdes.Clear();

        return result;
    }
}
