//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Tools.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utils.Data.Cloud;

/// <summary>Tools Editor</summary>
public class Tools
{
    /// <summary>Updates the cloud.</summary>
    [MenuItem("Tools/Resources To Cloud")]
    public static void UpdateCloud()
    {
        UnityEditor.EditorApplication.delayCall += () =>
        {
            CloudData.SaveInDropboxAFolder("/resources", Application.persistentDataPath + "/resources", new User(), new List<string>(new string[] { ".json", ".csv" }));
        };
    }
}