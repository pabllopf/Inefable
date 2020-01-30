//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Shader.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Manage the effect</summary>
public class Shader : MonoBehaviour
{
    /// <summary>Awakes this instance.</summary>
    private void Awake() => Game.LoadStats();

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (Stats.Current.isDay)
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
        else 
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
