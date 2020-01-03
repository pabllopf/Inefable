//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Icon.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Default icon of a object.</summary>
public class Icon : MonoBehaviour
{
    /// <summary>The icon</summary>
    private Sprite iconSprite = null;

    /// <summary>Gets the name of the icon.</summary>
    /// <returns>Return the icon name.</returns>
    public string Name => this.gameObject.tag;

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        this.iconSprite = Resources.Load<Sprite>("Icons/" + this.Name);
    }

    /// <summary>Gets the get.</summary>
    /// <returns>The get.</returns>
    public Sprite Get()
    {
        return this.iconSprite;
    }
}