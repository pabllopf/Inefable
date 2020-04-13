//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="MultiEditor.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEditor;
using UnityEngine;
using MultiPlayer;
using System.IO;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Mirror;

/// <summary>Editor multiplayer.</summary>
[CustomEditor(typeof(Multiplayer))]
public class MultiEditor : Editor
{
    /// <summary>Implement this function to make a custom inspector.</summary>
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Update Prefabs"))
        {
            Multiplayer multiplayer = target as Multiplayer;
            UpdateList(ref multiplayer);
        }
        base.OnInspectorGUI();
    }

    /// <summary>Shows the specified list.</summary>
    /// <param name="list">The list.</param>
    private static void Show(SerializedProperty list)
    {
        EditorGUILayout.PropertyField(list, false);
        EditorGUI.indentLevel += 1;
        if (list.isExpanded)
        {
            for (int i = 0; i < list.arraySize; i++)
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
            }
        }

        EditorGUI.indentLevel -= 1;
    }

    // 

    /// <summary>Updates the list</summary>
    /// <param name="style">The style.</param>
    private void UpdateList(ref Multiplayer multiplayer)
    {
        string pathPrefabs = Application.dataPath + "/Prefabs";
        Debug.Log("Directory to get network prefabs: " + pathPrefabs);
        List<GameObject> result = new List<GameObject>();

        if (Directory.Exists(pathPrefabs))
        {
            Directory.GetFiles(pathPrefabs, "*.prefab", SearchOption.AllDirectories)
                    .ToList()
                    .ForEach(filePath =>
                    {
                        string objPath = "Assets" + filePath.Substring(Application.dataPath.Length);
                        GameObject obj = (GameObject)AssetDatabase.LoadAssetAtPath(objPath, typeof(GameObject));
                        if (obj.GetComponent<NetworkIdentity>()) 
                        {
                            result.Add(obj);
                        }
                    });
        }
        else 
        {
            Directory.CreateDirectory(pathPrefabs);
            Debug.LogError("Directory is empty: " + pathPrefabs);
        }

        Debug.Log(result.Count + " have been added.");
        multiplayer.gameObject.GetComponent<NetworkManager>().spawnPrefabs.Clear();
        multiplayer.gameObject.GetComponent<NetworkManager>().spawnPrefabs.AddRange(result);
    }
}