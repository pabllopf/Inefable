//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Icon.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Default icon of a object.</summary>
public class Icon : MonoBehaviour
{
    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    public string Name => gameObject.tag;

    /// <summary>Gets the icon sprite.</summary>
    /// <value>The icon sprite.</value>
    public Sprite Sprite => Resources.Load<Sprite>("Icons/" + Name);
}