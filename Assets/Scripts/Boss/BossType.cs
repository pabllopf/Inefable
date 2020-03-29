//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="BossType.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEditor;
using UnityEngine;

/// <summary>Define a boss of the game.</summary>
[System.Serializable]
[CreateAssetMenu(fileName = "New Boss", menuName = "Game/New Boss")]
public class BossType : ScriptableObject
{
    [Header("Name:")]

    /// <summary>The name style</summary>
    [SerializeField]
    private string nameBoss = string.Empty;

    #region Encapsulate Fields

    /// <summary>Gets or sets the name boss.</summary>
    /// <value>The name boss.</value>
    public string NameBoss { get => nameBoss; set => nameBoss = value; }

    #endregion

    #region Validate Name
    /// <summary>Called when [validate].</summary>
    public void OnValidate()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall += () =>
        {
            string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, NameBoss);
            AssetDatabase.SaveAssets();
        };
#endif
    }
    #endregion
}