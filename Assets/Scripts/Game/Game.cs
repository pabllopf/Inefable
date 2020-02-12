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
    private object var;
    private string folder = "";
    
    private Game(object var) 
    {
        this.var = var;
    }

    private Game(object var, string folder)
    {
        this.var = var;
        this.folder = folder;
    }

    public static Game SaveVar(object var) 
    {
        return new Game(var);
    }

    public Game InFolder(string folder) 
    {
        return new Game(var, folder);
    }

    public void WithName(string name) 
    {
        string path = Application.persistentDataPath + "/Data/" + folder;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/" + name + ".json", var.ToString());
    }

    public static Game LoadVar(string var) 
    {
        return new Game(var);
    }

    public Game OfFolder(string folder) 
    {
        string file = Application.persistentDataPath + "/Data/" + folder + "/" + var + ".json";

        if (File.Exists(file))
        {
            return new Game(File.ReadAllText(file));
        }
        else
        {
            string path = Application.persistentDataPath + "/Data/" + folder;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllText(file, "0");
            return new Game(File.ReadAllText(file));
        }
    }

    public int Int => int.Parse(var.ToString());



    private static readonly string key = "dgwn`SCN7342!/(&2-.MSDUOUQsdbasd3e435";

    public static void ResetSettings()
    {
        Settings.Current = new Settings();
        Game.SaveSettings();
    }

    public static void SaveSettings()
    {
        Encrypt(JsonUtility.ToJson(Settings.Current), key, Application.persistentDataPath + "/Settings.json");
    }

    public static void LoadSettings()
    {
        Settings.Current = new Settings();
        if (File.Exists(Application.persistentDataPath + "/Settings.json"))
        {
            Settings.Current = JsonUtility.FromJson<Settings>(Decrypt(key, Application.persistentDataPath + "/Settings.json"));
        }
        else
        {
            Encrypt(JsonUtility.ToJson(Settings.Current), key, Application.persistentDataPath + "/Settings.json");
            Settings.Current = JsonUtility.FromJson<Settings>(Decrypt(key, Application.persistentDataPath + "/Settings.json"));
        }
    }

    public static void ResetStats()
    {
        Stats.Current = new Stats();
        Game.SaveStats();
    }

    public static void SaveStats()
    {
        Encrypt(JsonUtility.ToJson(Stats.Current), key, Application.persistentDataPath + "/Stats.json");
    }

    public static object LoadData(string nameFile) 
    {
        if (File.Exists(Application.persistentDataPath + "/" + nameFile + ".json"))
        {
            return JsonUtility.FromJson<Settings>(Decrypt(key, Application.persistentDataPath + "/" + nameFile + ".json"));
        }
        else
        {
            Encrypt(JsonUtility.ToJson(Settings.Current), key, Application.persistentDataPath + "/" + nameFile + ".json");
            return JsonUtility.FromJson<Settings>(Decrypt(key, Application.persistentDataPath + "/" + nameFile + ".json"));
        }
    }

    public static void SaveData(object obj,  string nameFile)
    {
        Encrypt(JsonUtility.ToJson(obj), key, Application.persistentDataPath + "/"+ nameFile +".json");
    }

    public static void LoadStats()
    {
        Stats.Current = new Stats();
        if (File.Exists(Application.persistentDataPath + "/Stats.json"))
        {
            Stats.Current = JsonUtility.FromJson<Stats>(Decrypt(key, Application.persistentDataPath + "/Stats.json"));
        }
        else
        {
            Encrypt(JsonUtility.ToJson(Stats.Current), key, Application.persistentDataPath + "/Stats.json");
            Stats.Current = JsonUtility.FromJson<Stats>(Decrypt(key, Application.persistentDataPath + "/Stats.json"));
        }
    }

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

    private static string Decrypt(string key, string pathFile)
    {
        string result = "";
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
