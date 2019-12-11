//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Settings.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------

/// <summary>Save the current settings of the game.</summary>
[System.Serializable]
public class Settings
{
    /// <summary>Initializes a new instance of the <see cref="Settings"/> class.</summary>
    public Settings()
    {
        this.HasSaveGame = false;
        this.Language = 1;
        this.Plattform = "Xbox";
    }

    /// <summary>Gets or sets the current.</summary>
    /// <value>The current.</value>
    public static Settings Current { get; set; }

    /// <summary>Gets or sets a value indicating whether this instance has save game.</summary>
    /// <value>
    /// <c>true</c> if this instance has save game; otherwise, <c>false</c>.</value>
    public bool HasSaveGame { get; set; }

    /// <summary>Gets or sets the language.</summary>
    /// <value>The language.</value>
    public int Language { get; set; }

    /// <summary>Gets or sets the platform.</summary>
    /// <value>The platform.</value>
    public string Plattform { get; set; }
}
