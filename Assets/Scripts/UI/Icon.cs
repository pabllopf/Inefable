//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Icon.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

/// <summary>Default icon of a object.</summary>
public class Icon : MonoBehaviour
{
    /// <summary>The icon name</summary>
    [SerializeField] 
    private string iconName;

    /// <summary>The icon</summary>
    [SerializeField]
    private Sprite iconSprite = null;

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        this.iconSprite = Resources.Load<Sprite>("Icons/" + this.iconName);
    }

    /// <summary>Gets the icon.</summary>
    /// <returns>Return the icon.</returns>
    public Sprite GetIcon() 
    {
        return this.iconSprite;
    }

    /// <summary>Gets the name of the icon.</summary>
    /// <returns>Return the icon name.</returns>
    public string GetIconName()
    {
        return this.iconName;
    }
}