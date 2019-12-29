//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Settings.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------

/// <summary>Save the current settings of the game.</summary>
[System.Serializable]
public class Settings
{
    /// <summary>The current</summary>
    public static Settings Current;

    /// <summary>The has save game</summary>
    public bool HasSaveGame;

    /// <summary>The language</summary>
    public string Language;

    /// <summary>The language default</summary>
    public bool LanguageDefault;

    /// <summary>The plattform</summary>
    public string Plattform;

    /// <summary>Initializes a new instance of the <see cref="Settings"/> class.</summary>
    public Settings()
    {
        this.HasSaveGame = false;
        this.Language = "English";
        this.LanguageDefault = false;
        this.Plattform = "Computer";
    }
}
