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
    private readonly object valueVar = null;

    /// <summary>The name variable</summary>
    private readonly string nameVar = null;

    /// <summary>Initializes a new instance of the <see cref="Data"/> class.</summary>
    /// <param name="variable">The variable.</param>
    private Data(object variable)
    {
        valueVar = variable;
    }

    /// <summary>Initializes a new instance of the <see cref="Data"/> class.</summary>
    /// <param name="variable">The variable.</param>
    /// <param name="nameVar">The name variable.</param>
    private Data(object variable, string nameVar)
    {
        valueVar = variable;
        this.nameVar = nameVar;
    }

    /// <summary>Gets the double.</summary>
    /// <value>The double.</value>
    public double Double => double.Parse(valueVar.ToString());

    /// <summary>Gets the float.</summary>
    /// <value>The float.</value>
    public float Float => float.Parse(valueVar.ToString());

    /// <summary>Get the integer.</summary>
    /// <value>The integer</value>
    public int Int => int.Parse(valueVar.ToString());

    /// <summary>Gets the string.</summary>
    /// <value>The string.</value>
    public string String => valueVar.ToString();

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

    /// <summary>Withes the name.</summary>
    /// <param name="nameVar">The name variable.</param>
    /// <returns>Return data</returns>
    public Data WithName(string nameVar)
    {
        return new Data(valueVar, nameVar);
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

        Security.Encrypt(path + file, valueVar.ToString());
    }

    /// <summary>From the folder.</summary>
    /// <param name="folder">The folder.</param>
    /// <returns>Return Data</returns>
    public Data FromFolder(string folder)
    {
        string path = Application.persistentDataPath + "/Data/" + folder;
        string file = "/" + valueVar + ".json";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        if (!File.Exists(path + file))
        {
            Security.Encrypt(path + file, "0");
        }

        return new Data(Security.Decrypt(path + file));
    }

    /// <summary>Converts to item.</summary>
    /// <returns>The item</returns>
    public Item ToItem()
    {
        Item item = new Item();
        JsonUtility.FromJsonOverwrite(valueVar.ToString(), item);
        return item;
    }
}