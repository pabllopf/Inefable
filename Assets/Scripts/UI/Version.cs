//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Version.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

/// <summary>Show the current version of the game.</summary>
[RequireComponent(typeof(Text))]
public class Version : MonoBehaviour
{
    /// <summary>The version type</summary>
    [SerializeField]
    private string versionType = "Alpha ";

    /// <summary>Gets or sets the type of the version.</summary>
    /// <value>The type of the version.</value>
    public string VersionType { get => versionType; set => versionType = value; }

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        GetComponent<Text>().text = versionType + Application.version + " ";
    }
}
