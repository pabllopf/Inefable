//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Particle.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;

/// <summary>Destroy in a time.</summary>
public class Particle : MonoBehaviour
{
    /// <summary>The time</summary>
    [SerializeField]
    [Range(0, 10)]
    private float time = 0.2f;

    /// <summary>Starts this instance.</summary>
    private void Start() => this.StartCoroutine(this.DestroyInTime(this.time));

    /// <summary>Destroys the in time.</summary>
    /// <param name="time">The time.</param>
    /// <returns>Return none</returns>
    private IEnumerator DestroyInTime(float time) 
    {
        Color color = this.GetComponent<SpriteRenderer>().color;
       
        while (color.a > 0f) 
        {
            color.a -= 0.15f;
            this.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(time);

        MonoBehaviour.Destroy(this.gameObject);
    }
}
