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

    public string Slot1;
    public string Slot2;
    public string Slot3;

    public Sprite Icon1;
    public Sprite Icon2;
    public Sprite Icon3;

    /// <summary>Initializes a new instance of the <see cref="Stats"/> class.</summary>
    public Stats()
    {
        this.Health = 100;
        this.Shield = 0;
        this.Wallet = 0;

        this.Slot1 = "";
        this.Slot2 = "";
        this.Slot3 = "";

        this.Icon1 = null;
        this.Icon2 = null;
        this.Icon3 = null;
    }
}
