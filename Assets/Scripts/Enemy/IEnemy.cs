//-----------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="IEnemy.cs" company="UnMedioStudio">Open Source</copyright>
//-----------------------------------------------------------------------
using UnityEngine;

/// <summary>Interface to define a enemy.</summary>
public interface IEnemy 
{
    /// <summary>Starts this instance.</summary>
    void Start();

    /// <summary>Called when [trigger stay2 d].</summary>
    /// <param name="collider2D">The collider2 d.</param>
    void OnTriggerStay2D(Collider2D collider2D);

    /// <summary>Updates this instance.</summary>
    void Update();

    /// <summary>Update every frame.</summary>
    void FixedUpdate();

    /// <summary>Takes the damage.</summary>
    /// <param name="amount">Amount to take health</param>
    void TakeDamage(int amount);

    /// <summary>Dies this instance.</summary>
    void Die();
}
