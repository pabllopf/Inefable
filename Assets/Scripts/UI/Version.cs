//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Version.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

/// <summary>Show the current version of the game.</summary>
public class Version : MonoBehaviour
{
    /// <summary>The version</summary>
    private Text version = null;

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        this.version = this.transform.Find("Text").GetComponent<Text>();
        this.version.text = Application.version;
    }
}
