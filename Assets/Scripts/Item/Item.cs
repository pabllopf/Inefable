//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Item.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEditor;
using UnityEngine;

/// <summary>Item of the game. </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    /// <summary>The name</summary>
    [SerializeField]
    private string nameItem = "New Item";

    /// <summary>The icon</summary>
    [SerializeField]
    private Sprite icon = null;

    /// <summary>The save in inventory</summary>
    [SerializeField]
    private bool saveInInventory = false;

    /// <summary>The target</summary>
    private GameObject target = null;

    /// <summary>The action</summary>
    [SerializeField]
    private ActionType action = ActionType.SetFullHealth;

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    public string NameItem 
    {
        get => nameItem;
        set => nameItem = value;
    }

    /// <summary>Gets or sets the icon.</summary>
    /// <value>The icon.</value>
    public Sprite Icon 
    { 
        get => icon; 
        set => icon = value; 
    }

    /// <summary>Gets or sets a value indicating whether [save in inventory].</summary>
    /// <value>
    /// <c>true</c> if [save in inventory]; otherwise, <c>false</c>.</value>
    public bool SaveInInventory 
    { 
        get => saveInInventory; 
        set => saveInInventory = value; 
    }

    /// <summary>Gets or sets the target.</summary>
    /// <value>The target.</value>
    public GameObject Target 
    { 
        get => target; 
        set => target = value; 
    }

    /// <summary>Gets or sets the action.</summary>
    /// <value>The action.</value>
    public ActionType Action
    { 
        get => action; 
        set => action = value; 
    }

    /// <summary>Uses this item</summary>
    public virtual void Use() 
    {
        Actions.Invoke(Action).In(Target);
    }

    /// <summary>Called when [validate].</summary>
    public void OnValidate()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall += () =>
        {
            string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, NameItem);
            AssetDatabase.SaveAssets();
        };
        #endif
    }
}