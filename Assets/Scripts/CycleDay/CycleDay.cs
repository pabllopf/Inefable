//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="CycleDay.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Manage the cycle of day and night</summary>
public class CycleDay : MonoBehaviour
{
    /// <summary>Starts this instance.</summary>
    private void Start() => GameObject.Find("Camera").GetComponent<Camera>().backgroundColor = new Color(192, 192, 192, 0);
}