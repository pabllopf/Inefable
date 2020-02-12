//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="TextEffect.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;

/// <summary>Destroy in a time.</summary>
public class TextEffect : MonoBehaviour
{
    /// <summary>The time</summary>
    [SerializeField]
    [Range(0, 10)]
    private readonly float time = 1f;

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        StartCoroutine(DestroyInTime(time));
    }

    /// <summary>Destroys the in time.</summary>
    /// <param name="time">The time.</param>
    /// <returns>Return none</returns>
    private IEnumerator DestroyInTime(float time)
    {
        yield return new WaitForSeconds(time);

        MonoBehaviour.Destroy(gameObject);
    }
}
