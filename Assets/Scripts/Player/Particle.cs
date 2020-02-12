//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Particle.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;

/// <summary>Destroy in a time.</summary>
public class Particle : MonoBehaviour
{
    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        StartCoroutine(DestroyInTime());
    }

    /// <summary>Destroys the in time.</summary>
    /// <param name="time">The time.</param>
    /// <returns>Return none</returns>
    private IEnumerator DestroyInTime()
    {
        Color color = GetComponent<SpriteRenderer>().color;

        while (color.a > 0f)
        {
            color.a -= 0.15f;
            GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.1f);

        MonoBehaviour.Destroy(gameObject);
    }
}
