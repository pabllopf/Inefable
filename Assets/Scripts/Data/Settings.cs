//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Settings.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------

/// <summary>Save the current settings of the game.</summary>
[System.Serializable]
public class Settings
{
    /// <summary>The current</summary>
    private static Settings current;

    /// <summary>The platform</summary>
    private string platform;

    /// <summary>The language</summary>
    private string language;

    /// <summary>Initializes a new instance of the <see cref="Settings"/> class.</summary>
    /// <param name="platform">The platform.</param>
    /// <param name="language">The language.</param>
    public Settings(string platform, string language)
    {
        this.platform = platform.Equals("0") ? "Computer" : platform;
        this.language = language.Equals("0") ? "English" : language;
    }

    /// <summary>Gets or sets the current.</summary>
    /// <value>The current.</value>
    public static Settings Current
    {
        get => current;
        set => current = value;
    }

    /// <summary>Gets or sets the platform.</summary>
    /// <value>The platform.</value>
    public string Platform
    {
        get => platform;
        set => platform = value;
    }

    /// <summary>Gets or sets the language.</summary>
    /// <value>The language.</value>
    public string Language
    {
        get => language;
        set => language = value;
    }
}
