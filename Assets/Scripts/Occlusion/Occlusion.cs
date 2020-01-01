//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Occlusion.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Linq;
using UnityEngine;

/// <summary>Enable or disable the components of the objects.</summary>
public class Occlusion : MonoBehaviour
{
    /// <summary>Called when [became visible].</summary>
    public void OnBecameVisible() => GetComponents<Behaviour>().ToList().ForEach(i => i.enabled = true);

    /// <summary>Called when [became invisible].</summary>
    public void OnBecameInvisible() => GetComponents<Behaviour>().ToList().ForEach(i => i.enabled = false);
}
