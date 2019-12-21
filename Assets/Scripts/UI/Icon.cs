//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Icon.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using NUnit.Framework;
using System;
using System.Collections;
using UnityEditor;
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

    /// <summary>Awakes this instance.</summary>
    public void Awake()
    {
        this.iconSprite = Resources.Load<Sprite>("Icons/" + this.iconName);
    }

    /// <summary>Gets the icon.</summary>
    /// <returns>Return the icon.</returns>
    public Sprite GetIcon() 
    {
        return this.iconSprite;
    }

    public String GetIconName()
    {
        return this.iconName;
    }
}

public class IconTests
{
    [UnityTest]
    public IEnumerator Test_Load_Icon()
    {
        SceneManager.LoadScene("House");
        yield return null;
        Icon[] icons = GameObject.FindObjectsOfType<Icon>();
        foreach (Icon icon in icons) 
        {
            Assert.IsNotNull(icon.GetIcon(), "Icon of "+ icon.gameObject.name + " is null.");
            Assert.AreEqual(icon.GetIcon().name, icon.GetIconName());
        }
    }
}
