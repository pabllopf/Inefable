//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Stats.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>Save the current stats of the player.</summary>
[System.Serializable]
public class Stats 
{
    /// <summary>The current</summary>
    public static Stats Current;

    /// <summary>The health</summary>
    public int Health;

    /// <summary>The shield</summary>
    public int Shield;

    /// <summary>The wallet</summary>
    public int Wallet;

    /// <summary>The keys</summary>
    public int Keys;

    /// <summary>The sprite item</summary>
    public List<Sprite> SpriteItem;

    /// <summary>The tag item</summary>
    public List<string> TagItem;

    /// <summary>The pet</summary>
    public string pet;

    /// <summary>The is day</summary>
    public bool isDay;

    /// <summary>Initializes a new instance of the <see cref="Stats"/> class.</summary>
    public Stats()
    {
        this.Health = 100;
        this.Shield = 0;
        this.Wallet = 0;
        this.Keys = 0;

        this.SpriteItem = new List<Sprite>
        {
            null,
            null,
            null
        };

        this.TagItem = new List<string>
        {
            "",
            "",
            ""
        };

        this.pet = "";
        this.isDay = true;
    }
}
