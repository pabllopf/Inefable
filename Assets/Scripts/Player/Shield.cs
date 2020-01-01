//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Shield.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

/// <summary>The shield of the player</summary>
public class Shield : MonoBehaviour
{
    /// <summary>Gets or sets the health UI.</summary>
    /// <value>The health UI.</value>
    private Scrollbar shieldUI = null;

    /// <summary>Awakes this instance.</summary>
    public void Awake() => Game.LoadStats();

    /// <summary>Starts this instance.</summary>
    public void Start() => this.shieldUI = this.transform.Find("Interface/Bar/Shield").GetComponent<Scrollbar>();

    /// <summary>Updates this instance.</summary>
    public void Update() => this.shieldUI.size = (float)Stats.Current.Shield / 100;

    /// <summary>Takes the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Take(int amount) => Stats.Current.Shield -= amount;

    /// <summary>Set full</summary>
    public void Full() => Stats.Current.Shield = 100;

    /// <summary>Determines whether this instance has shield.</summary>
    /// <returns>
    /// <c>true</c> if this instance has shield; otherwise, <c>false</c>.</returns>
    public bool HasShield() => (Stats.Current.Shield > 0) ? true : false;
}
