
using System.Collections;

/// <summary>Interface to define a enemy.</summary>
public interface IEnemy
{
    /// <summary>Starts this instance.</summary>
    void Start();

    /// <summary>Updates this instance.</summary>
    void Update();

    /// <summary>Takes the damage.</summary>
    /// <param name="amount">Amount to take health</param>
    void TakeDamage(int amount);

    /// <summary>Distances to target.</summary>
    /// <returns>Return the distance to the target</returns>
    float DistanceToTarget();

    /// <summary>Dies this instance.</summary>
    /// <returns>Return none</returns>
    IEnumerator Die();
}
