//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Data.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.IO;
using UnityEngine;

/// <summary>Manage the game.</summary>
public class Data
{
    /// <summary>The variable</summary>
    private readonly object variable = null;

    /// <summary>The name variable</summary>
    private readonly string nameVar = null;

    /// <summary>Initializes a new instance of the <see cref="Data"/> class.</summary>
    /// <param name="variable">The variable.</param>
    private Data(object variable)
    {
        this.variable = variable;
    }

    /// <summary>Initializes a new instance of the <see cref="Data"/> class.</summary>
    /// <param name="variable">The variable.</param>
    /// <param name="nameVar">The name variable.</param>
    private Data(object variable, string nameVar)
    {
        this.variable = variable;
        this.nameVar = nameVar;
    }

    /// <summary>Gets the double.</summary>
    /// <value>The double.</value>
    public double Double => double.Parse(variable.ToString());

    /// <summary>Gets the float.</summary>
    /// <value>The float.</value>
    public float Float => float.Parse(variable.ToString());

    /// <summary>Get the integer.</summary>
    /// <value>The integer</value>
    public int Int => int.Parse(variable.ToString());

    /// <summary>Gets the string.</summary>
    /// <value>The string.</value>
    public string String => variable.ToString();

    /// <summary>Saves the variable.</summary>
    /// <param name="obj">The object.</param>
    /// <returns>Return data</returns>
    public static Data SaveVar(object obj)
    {
        return new Data(obj);
    }

    /// <summary>Loads the variable.</summary>
    /// <param name="variable">The variable.</param>
    /// <returns>Return data</returns>
    public static Data LoadVar(string variable)
    {
        return new Data(variable);
    }

    /// <summary>Saves the settings.</summary>
    public static void SaveSettings()
    {
        SaveVar(Settings.Current.Platform).WithName("Plattform").InFolder("Settings");
        SaveVar(Settings.Current.Language).WithName("Language").InFolder("Settings");
    }

    /// <summary>Loads this instance.</summary>
    public static void LoadSettings()
    {
        string platform = LoadVar("Plattform").FromFolder("Settings").String;
        string language = LoadVar("Language").FromFolder("Settings").String;

        Settings.Current = new Settings(platform, language);
    }

    /// <summary>Withes the name.</summary>
    /// <param name="nameVar">The name variable.</param>
    /// <returns>Return data</returns>
    public Data WithName(string nameVar)
    {
        return new Data(variable, nameVar);
    }

    /// <summary>Ins the folder.</summary>
    /// <param name="folder">The folder.</param>
    public void InFolder(string folder)
    {
        string path = Application.persistentDataPath + "/Data/" + folder;
        string file = "/" + nameVar + ".json";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        Security.Encrypt(path + file, variable.ToString());
    }

    /// <summary>From the folder.</summary>
    /// <param name="folder">The folder.</param>
    /// <returns>Return Data</returns>
    public Data FromFolder(string folder)
    {
        string path = Application.persistentDataPath + "/Data/" + folder;
        string file = "/" + variable.ToString() + ".json";

        if (Directory.Exists(path))
        {
            if (File.Exists(file))
            {
                return new Data(Security.Decrypt(path + file));
            }
            else
            {
                Security.Encrypt(path + file, "0");
                return new Data(Security.Decrypt(path + file));
            }
        }
        else
        {
            Directory.CreateDirectory(path);
            Security.Encrypt(path + file, "0");
            return new Data(Security.Decrypt(path + file));
        }
    }
}