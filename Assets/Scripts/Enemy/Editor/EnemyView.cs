//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="EnemyView.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace EnemyIA.Editor
{
    using EnemyIA.Configuration;
    using UnityEditor;
    using UnityEngine;

    /// <summary>Enemy view</summary>
    /// <seealso cref="UnityEditor.Editor" />
    [CustomEditor(typeof(EnemyType))]
    public class EnemyView : Editor
    {
        /// <summary>Implement this function to make a custom inspector.</summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (GUILayout.Button("Save"))
            {
                EnemyType enemy = target as EnemyType;
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
                    AssetDatabase.RenameAsset(assetPath, enemy.NameEnemy);
                    AssetDatabase.SaveAssets();
                };

                UnityEditor.EditorApplication.delayCall += () =>
                {
                    AssetDatabase.Refresh();
                };
            }

            Show(serializedObject.FindProperty("nameEnemy"));

            Show(serializedObject.FindProperty("isStaticEnemy"));

            Show(serializedObject.FindProperty("health"));
            Show(serializedObject.FindProperty("speedToMove"));
            Show(serializedObject.FindProperty("thrust"));
            Show(serializedObject.FindProperty("knockTime"));

            Show(serializedObject.FindProperty("rangeOfVision"));
            Show(serializedObject.FindProperty("rangeOfAttack"));
            Show(serializedObject.FindProperty("frequencyToAttack"));

            Show(serializedObject.FindProperty("attackType"));
            
            Show(serializedObject.FindProperty("animator"));

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
    }
}