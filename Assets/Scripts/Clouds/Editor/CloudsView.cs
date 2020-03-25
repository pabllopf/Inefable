//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="CloudsView.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace CloudsGenerator.Editor
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    /// <summary>Clouds generator editor view</summary>
    [CustomEditor(typeof(Clouds))]
    public class CloudsView : Editor
    {
        /// <summary>Implement this function to make a custom inspector.</summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (GUILayout.Button("Update"))
            {
                Clouds clouds = target as Clouds;

                LoadClouds(ref clouds);

                UnityEditor.EditorApplication.delayCall += () =>
                {
                    AssetDatabase.Refresh();
                };
            }

            base.OnInspectorGUI();
        }

        /// <summary>Loads the clouds.</summary>
        /// <param name="clouds">The clouds.</param>
        private void LoadClouds(ref Clouds clouds) 
        {
            clouds.CloudsPrefabs = UpdateField("/Clouds");
        }

        /// <summary>Updates the field.</summary>
        /// <param name="path">The path.</param>
        /// <returns>List of objects</returns>
        private List<GameObject> UpdateField(string path)
        {
            string prefabDir = Application.dataPath + "/Prefabs";
            if (!Directory.Exists(prefabDir))
            {
                Directory.CreateDirectory(prefabDir);
                Debug.Log("Directory created at: " + prefabDir);
            }

            string pathFiles = Application.dataPath + "/Prefabs/" + path;
            if (!Directory.Exists(pathFiles))
            {
                Directory.CreateDirectory(pathFiles);
                Debug.Log("Directory created at: " + pathFiles);
            }

            List<GameObject> result = new List<GameObject>();
            if (Directory.Exists(pathFiles))
            {
                Directory.GetFiles(pathFiles)
                    .ToList()
                    .FindAll(file => Path.GetExtension(file) == ".prefab")
                    .ForEach(filePath =>
                    {
                        string dirOfTiles = "Assets/Prefabs/" + path;
                        if (!Directory.Exists(dirOfTiles))
                        {
                            Directory.CreateDirectory(dirOfTiles);
                            Debug.Log("Directory created at: " + dirOfTiles);
                        }

                        string objPath = "Assets/Prefabs/" + path + "/" + Path.GetFileName(filePath);
                        GameObject obj = (GameObject)AssetDatabase.LoadAssetAtPath(objPath, typeof(GameObject));

                        if (!result.Contains(obj))
                        {
                            result.Add(obj);
                        }
                    });
            }

            if (result.Count <= 0)
            {
                Debug.LogError("Directory of clouds is empty: " + "Assets/Prefabs/" + path);
            }

            return result;
        }
    }
}