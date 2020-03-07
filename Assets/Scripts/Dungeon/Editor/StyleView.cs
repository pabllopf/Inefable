//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="StyleView.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
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

        if (GUILayout.Button("Update"))
        {
            Style test = target as Style;
            test.OnValidate();
        }

        Show(serializedObject.FindProperty("nameStyle"));

        Show(serializedObject.FindProperty("floor"));

        Show(serializedObject.FindProperty("wallDown"));
        Show(serializedObject.FindProperty("wallLeft"));
        Show(serializedObject.FindProperty("wallRight"));
        Show(serializedObject.FindProperty("wallTop"));

        Show(serializedObject.FindProperty("cornerLeftDown"));
        Show(serializedObject.FindProperty("cornerRightDown"));
        Show(serializedObject.FindProperty("cornerLeftUp"));
        Show(serializedObject.FindProperty("cornerRightUp"));

        Show(serializedObject.FindProperty("cornerInternalLeftDown"));
        Show(serializedObject.FindProperty("cornerInternalLeftUp"));
        Show(serializedObject.FindProperty("cornerInternalRightDown"));
        Show(serializedObject.FindProperty("cornerInternalRightUp"));

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
}