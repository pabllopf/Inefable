//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Icon.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Default icon of a object.</summary>
public class Icon : MonoBehaviour
{
    /// <summary>The icon</summary>
    [SerializeField]
    private Sprite icon = null;

    /// <summary>Gets the icon.</summary>
    /// <returns>Return the icon.</returns>
    public Sprite GetIcon() 
    {
        return this.icon;
    }
}
