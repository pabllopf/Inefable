//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Setting.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Save the current settings of the game.</summary>
[System.Serializable]
public class Setting
{
    /// <summary>The current</summary>
    private static Setting current;

    /// <summary>The has save game</summary>
    private bool hasSaveGame;

    /// <summary>The language</summary>
    private int language;

    /// <summary>The platform</summary>
    private string platform;

    /// <summary>Initializes a new instance of the <see cref="Setting"/> class.</summary>
    public Setting()
    {
        this.HasSaveGame = false;
        this.Language = 1;
        this.Plattform = "Xbox";
    }

    /// <summary>Gets or sets the current.</summary>
    /// <value>The current.</value>
    public static Setting Current { get => current; set => current = value; }

    /// <summary>Gets or sets a value indicating whether this instance has save game.</summary>
    /// <value>
    /// <c>true</c> if this instance has save game; otherwise, <c>false</c>.</value>
    public bool HasSaveGame { get => this.hasSaveGame; set => this.hasSaveGame = value; }

    /// <summary>Gets or sets the language.</summary>
    /// <value>The language.</value>
    public int Language { get => this.language; set => this.language = value; }

    /// <summary>Gets or sets the platform.</summary>
    /// <value>The platform.</value>
    public string Plattform { get => this.platform; set => this.platform = value; }
}
