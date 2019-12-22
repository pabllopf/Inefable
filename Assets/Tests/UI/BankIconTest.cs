//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="BankIconTest.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

/// <summary>Test the icon class</summary>
public class BankIconTest 
{
    /// <summary>Tests the load icon.</summary>
    /// <returns>Return none.</returns>
    [UnityTest]
    public IEnumerator TestToLoadIconOfResources()
    {
        SceneManager.LoadScene("House");
        yield return null;
        Icon[] icons = GameObject.FindObjectsOfType<Icon>();
        foreach (Icon icon in icons)
        {
            Assert.IsNotNull(icon.GetIcon(), "Icon of " + icon.gameObject.name + " is null.");
            Assert.AreEqual(icon.GetIcon().name, icon.GetIconName());
        }
    }
}
