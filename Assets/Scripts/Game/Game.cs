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
public class Game
{
    /// <summary>The key</summary>
    private static readonly string key = "dgwn`SCN7342!/(&2-.MSDUOUQsdbasd3e435";

    /// <summary>The variable</summary>
    private readonly object var = null;

    /// <summary>The folder</summary>
    private readonly string folder = string.Empty;

    /// <summary>Gets the int.</summary>
    /// <value>The int.</value>
    public int Int => int.Parse(var.ToString());

    /// <summary>Gets the string.</summary>
    /// <value>The string.</value>
    public string String => var.ToString();

    /// <summary>Initializes a new instance of the <see cref="Game"/> class.</summary>
    /// <param name="var">The variable.</param>
    private Game(object var) => this.var = var;

    /// <summary>Initializes a new instance of the <see cref="Game"/> class.</summary>
    /// <param name="var">The variable.</param>
    /// <param name="folder">The folder.</param>
    private Game(object var, string folder)
    {
        this.var = var;
        this.folder = folder;
    }

    /// <summary>Saves the variable.</summary>
    /// <param name="var">The variable.</param>
    /// <returns>A game instance</returns>
    public static Game SaveVar(object var) => new Game(var);

    /// <summary>Ins the folder.</summary>
    /// <param name="folder">The folder.</param>
    /// <returns>A game instance</returns>
    public Game InFolder(string folder) => new Game(var, folder);

    /// <summary>Withes the name.</summary>
    /// <param name="name">The name.</param>
    public void WithName(string name)
    {
        string path = Application.persistentDataPath + "/Data/" + folder;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        Encrypt(var.ToString(), key, path + "/" + name + ".json");
    }

    /// <summary>Loads the variable.</summary>
    /// <param name="var">The variable.</param>
    /// <returns>A game instance</returns>
    public static Game LoadVar(string var) => new Game(var);

    /// <summary>Ofs the folder.</summary>
    /// <param name="folder">The folder.</param>
    /// <returns>A game instance</returns>
    public Game OfFolder(string folder)
    {
        string file = Application.persistentDataPath + "/Data/" + folder + "/" + var + ".json";

        if (File.Exists(file))
        {
            return new Game(Decrypt(key, file));
        }
        else
        {
            string path = Application.persistentDataPath + "/Data/" + folder;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Encrypt("0", key, file);
            return new Game(Decrypt(key, file));
        }
    }

    /// <summary>Loads the settings.</summary>
    public static void LoadSettings() 
    {
        string platform = LoadVar("Plattform").OfFolder("Settings").String;
        string language = LoadVar("Language").OfFolder("Settings").String;
        
        Settings.Current = new Settings(platform, language);
    }

    /// <summary>Saves the settings.</summary>
    public static void SaveSettings() 
    {
        SaveVar(Settings.Current.Platform).InFolder("Settings").WithName("Plattform");
        SaveVar(Settings.Current.Language).InFolder("Settings").WithName("Language");
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
    /// <returns>The string decrypted</returns>
    private static string Decrypt(string key, string pathFile)
    {
        string result = string.Empty;
        string stringEncrypt = File.ReadAllText(pathFile);

        byte[] arrayEncrypt = Convert.FromBase64String(stringEncrypt);

        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        byte[] keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        hashmd5.Clear();

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
        {
            Key = keyArray,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };

        ICryptoTransform desEncrypt = tdes.CreateDecryptor();
        result = UTF8Encoding.UTF8.GetString(desEncrypt.TransformFinalBlock(arrayEncrypt, 0, arrayEncrypt.Length));
        tdes.Clear();

        return result;
    }
}