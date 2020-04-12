//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Settings.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using Utils.Data.Local;

/// <summary>Save the current settings of the game.</summary>
[System.Serializable]
public class Settings
{
    /// <summary>The current</summary>
    private static Settings current;

    /// <summary>The platform</summary>
    [SerializeField]
    private string platform;

    /// <summary>The language</summary>
    [SerializeField]
    private string language;

    /// <summary>The is the first time</summary>
    [SerializeField]
    private bool firstTime;

    #region Constructor

    /// <summary>Initializes a new instance of the <see cref="Settings"/> class.</summary>
    /// <param name="platform">The platform.</param>
    /// <param name="language">The language.</param>
    /// <param name="firstTime">if set to <c>true</c> [is the first time].</param>
    public Settings(string platform, string language, bool firstTime)
    {
        this.platform = platform;
        this.language = language;
        this.firstTime = firstTime;
    }

    #endregion

    #region Encapsulate Fields

    /// <summary>Gets or sets the current.</summary>
    /// <value>The current.</value>
    public static Settings Current { get => current; set => current = value; }

    /// <summary>Gets or sets the platform.</summary>
    /// <value>The platform.</value>
    public string Platform { get => platform; set => platform = value; }
    
    /// <summary>Gets or sets the language.</summary>
    /// <value>The language.</value>
    public string Language { get => language; set => language = value; }
    
    /// <summary>Gets or sets a value indicating whether this instance is the first time.</summary>
    /// <value>
    /// <c>true</c> if this instance is the first time; otherwise, <c>false</c>.</value>
    public bool FirstTime { get => firstTime; set => firstTime = value; }

    #endregion

    /// <summary>Loads this instance.</summary>
    public static void Load() 
    {
        string dataPath = Application.persistentDataPath + "/Data";
        Current = (LocalData.Exits("Settings", dataPath)) ? LocalData.Load<Settings>("Settings", dataPath) : new Settings("Computer", "English", true);
    }

    /// <summary>Saves this instance.</summary>
    public static void Save() 
    {
        string dataPath = Application.persistentDataPath + "/Data";
        LocalData.Save<Settings>(data: current, nameFile: "Settings", pathFile: dataPath);
    }
}