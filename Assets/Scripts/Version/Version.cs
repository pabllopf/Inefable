//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Version.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

/// <summary>Show the current version of the game.</summary>
public class Version : MonoBehaviour
{
    /// <summary>The version type</summary>
    private const string VersionType = "Alpha ";

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        GetComponent<Text>().text = VersionType + Application.version + " ";
    }
}