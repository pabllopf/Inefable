//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Stats.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------

/// <summary>Save the current stats of the player.</summary>
[System.Serializable]
public class Stats 
{
    /// <summary>Initializes a new instance of the <see cref="Stats"/> class.</summary>
    public Stats()
    {
        this.Health = 100;
        this.Shield = 100;
        this.Wallet = 0;
    }

    /// <summary>Gets or sets the current.</summary>
    /// <value>The current.</value>
    public static Stats Current { get; set; }

    /// <summary>Gets or sets the health.</summary>
    /// <value>The health.</value>
    public int Health { get; set; }

    /// <summary>Gets or sets the shield.</summary>
    /// <value>The shield.</value>
    public int Shield { get; set; }

    /// <summary>Gets or sets the wallet.</summary>
    /// <value>The wallet.</value>
    public int Wallet { get; set; }
}
