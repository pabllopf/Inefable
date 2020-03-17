//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="StyleView.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace DungeonGenerator.Editor
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using DungeonGenerator.Configuration;
    using UnityEditor;
    using UnityEngine;

    /// <summary>Style Editor</summary>
    [CustomEditor(typeof(Style))]
    public class StyleView : Editor
    {
        /// <summary>Implement this function to make a custom inspector.</summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (GUILayout.Button("Save"))
            {
                Style style = target as Style;
                UpdateStyle(ref style);

                UnityEditor.EditorApplication.delayCall += () =>
                {
                    string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
                    AssetDatabase.RenameAsset(assetPath, style.NameStyle);
                    AssetDatabase.SaveAssets();
                };

                UnityEditor.EditorApplication.delayCall += () =>
                {
                    AssetDatabase.Refresh();
                };
            }

            Show(serializedObject.FindProperty("nameStyle"));

            Show(serializedObject.FindProperty("floors"));

            Show(serializedObject.FindProperty("wallsDown"));
            Show(serializedObject.FindProperty("wallsLeft"));
            Show(serializedObject.FindProperty("wallsRight"));
            Show(serializedObject.FindProperty("wallsTop"));

            Show(serializedObject.FindProperty("cornersLeftDown"));
            Show(serializedObject.FindProperty("cornersRightDown"));
            Show(serializedObject.FindProperty("cornersLeftUp"));
            Show(serializedObject.FindProperty("cornersRightUp"));

            Show(serializedObject.FindProperty("cornersInternalLeftDown"));
            Show(serializedObject.FindProperty("cornersInternalLeftUp"));
            Show(serializedObject.FindProperty("cornersInternalRightDown"));
            Show(serializedObject.FindProperty("cornersInternalRightUp"));

            Show(serializedObject.FindProperty("decorations"));

            serializedObject.ApplyModifiedProperties();
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

        /// <summary>Updates the style.</summary>
        /// <param name="style">The style.</param>
        private void UpdateStyle(ref Style style) 
        {
            style.Floors = UpdateField(style.NameStyle + "/Board/Floor");

            style.WallsDown = UpdateField(style.NameStyle + "/Board/WallDown");
            style.WallsLeft = UpdateField(style.NameStyle + "/Board/WallLeft");
            style.WallsRight = UpdateField(style.NameStyle + "/Board/WallRight");
            style.WallsTop = UpdateField(style.NameStyle + "/Board/WallTop");

            style.CornersLeftDown = UpdateField(style.NameStyle + "/Board/CornerLD");
            style.CornersLeftUp = UpdateField(style.NameStyle + "/Board/CornerLU");
            style.CornersRightDown = UpdateField(style.NameStyle + "/Board/CornerRD");
            style.CornersRightUp = UpdateField(style.NameStyle + "/Board/CornerRU");

            style.CornersInternalLeftDown = UpdateField(style.NameStyle + "/Board/CornerILD");
            style.CornersInternalLeftUp = UpdateField(style.NameStyle + "/Board/CornerILU");
            style.CornersInternalRightDown = UpdateField(style.NameStyle + "/Board/CornerIRD");
            style.CornersInternalRightUp = UpdateField(style.NameStyle + "/Board/CornerIRU");

            style.Decorations = UpdateDecoration(style.NameStyle + "/Decoration", style.Decorations);
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

            string pathFiles = Application.dataPath + "/Prefabs/Dungeon/" + path;
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
                        string dirOfTiles = "Assets/Prefabs/Dungeon/" + path;
                        if (!Directory.Exists(dirOfTiles))
                        {
                            Directory.CreateDirectory(dirOfTiles);
                            Debug.Log("Directory created at: " + dirOfTiles);
                        }

                        string objPath = "Assets/Prefabs/Dungeon/" + path + "/" + Path.GetFileName(filePath);
                        GameObject obj = (GameObject)AssetDatabase.LoadAssetAtPath(objPath, typeof(GameObject));

                        if (!result.Contains(obj))
                        {
                            result.Add(obj);
                        }
                    });
            }

            if (result.Count <= 0) 
            {
                Debug.LogError("Directory of dungeon is empty: " + "Assets/Prefabs/Dungeon/" + path);
            }

            return result;
        }

        private List<DecoMenu> UpdateDecoration(string path, List<DecoMenu> oldList) 
        {
            string prefabDir = Application.dataPath + "/Prefabs";
            if (!Directory.Exists(prefabDir))
            {
                Directory.CreateDirectory(prefabDir);
                Debug.Log("Directory created at: " + prefabDir);
            }

            string pathFiles = Application.dataPath + "/Prefabs/Dungeon/" + path;
            if (!Directory.Exists(pathFiles))
            {
                Directory.CreateDirectory(pathFiles);
                Debug.Log("Directory created at: " + pathFiles);
            }

            List<DecoMenu> result = new List<DecoMenu>();

            if (Directory.Exists(pathFiles))
            {
                Directory.GetFiles(pathFiles)
                    .ToList()
                    .FindAll(file => Path.GetExtension(file) == ".prefab")
                    .ForEach(filePath =>
                    {
                        string dirOfTiles = "Assets/Prefabs/Dungeon/" + path;
                        if (!Directory.Exists(dirOfTiles))
                        {
                            Directory.CreateDirectory(dirOfTiles);
                            Debug.Log("Directory created at: " + dirOfTiles);
                        }

                        string objPath = "Assets/Prefabs/Dungeon/" + path + "/" + Path.GetFileName(filePath);
                        GameObject obj = (GameObject)AssetDatabase.LoadAssetAtPath(objPath, typeof(GameObject));

                        if (obj.GetComponent<Decoration>())
                        {
                            if (oldList.Any(i => i.Prefab == obj.GetComponent<Decoration>().Prefab))
                            {
                                DecoMenu decoMenu = oldList.Find(i => i.Prefab == obj.GetComponent<Decoration>().Prefab);
                                result.Add(decoMenu);

                                Decoration deco = obj.GetComponent<Decoration>();
                                deco.Prefab = decoMenu.Prefab;
                                deco.BoxToSpawn = decoMenu.BoxToSpawn;
                                deco.MinToSpawn = decoMenu.MinToSpawn;
                                deco.MaxToSpawn = decoMenu.MaxToSpawn;

                                return;
                            }
                            else
                            {
                                DecoMenu decoMenu = new DecoMenu();
                                Decoration deco = obj.GetComponent<Decoration>();

                                decoMenu.Prefab = deco.Prefab;
                                decoMenu.BoxToSpawn = deco.BoxToSpawn;
                                decoMenu.MinToSpawn = deco.MinToSpawn;
                                decoMenu.MaxToSpawn = deco.MaxToSpawn;

                                result.Add(decoMenu);
                            }
                        }
                    });
            }

            if (result.Count <= 0)
            {
                Debug.LogError("Directory of dungeon is empty: " + "Assets/Prefabs/Dungeon/" + path);
            }

            return result;
        }
    }
}