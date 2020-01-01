//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Health.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

/// <summary>The health of the player</summary>
public class Health : MonoBehaviour
{
    /// <summary>Gets or sets the health UI.</summary>
    /// <value>The health UI.</value>
    private Scrollbar healthUI = null;

    /// <summary>Awakes this instance.</summary>
    public void Awake() => Game.LoadStats();

    /// <summary>Starts this instance.</summary>
    public void Start() => this.healthUI = this.transform.Find("Interface/Bar/Health").GetComponent<Scrollbar>();

    /// <summary>Updates this instance.</summary>
    public void Update() => this.healthUI.size = (float)Stats.Current.Health / 100;

    /// <summary>Adds the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Add(int amount) => Stats.Current.Health += amount;

    /// <summary>Takes the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Take(int amount) => Stats.Current.Health -= amount;

    /// <summary>Set full</summary>
    public void Full() => Stats.Current.Health = 100;

    /// <summary>Determines whether this instance can add the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    /// <returns>
    /// <c>true</c> if this instance can add the specified amount; otherwise, <c>false</c>.</returns>
    public bool CanAdd(int amount) => ((Stats.Current.Health + amount) < 100) ? true : false;

    /// <summary>Determines whether this instance is alive.</summary>
    /// <returns>
    /// <c>true</c> if this instance is alive; otherwise, <c>false</c>.</returns>
    public bool IsAlive() => (Stats.Current.Health > 0) ? true : false;
}