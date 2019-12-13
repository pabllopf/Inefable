//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Stats.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------

/// <summary>Save the current stats of the player.</summary>
[System.Serializable]
public class Stats 
{
    public static Stats Current;

    public int Health;

    public int Shield;

    public int Wallet;

    /// <summary>Initializes a new instance of the <see cref="Stats"/> class.</summary>
    public Stats()
    {
        this.Health = 100;
        this.Shield = 100;
        this.Wallet = 0;
    }
}
