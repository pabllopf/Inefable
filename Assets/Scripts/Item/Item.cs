//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Item.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEditor;
using UnityEngine;
using Utils.Data.Cloud;
using Utils.Data.Local;

/// <summary>Item of the game. </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Game/New Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    [Header("Name:")]

    /// <summary>The name</summary>
    [SerializeField]
    private string nameItem = "New Item";

    [Header("Description:")]

    /// <summary>The description</summary>
    [SerializeField]
    private Clef description = Clef.A1;

    [Header("Prefab:")]

    /// <summary>The prefab</summary>
    [SerializeField]
    private GameObject prefab = null;

    [Header("Stats:")]

    /// <summary>The cost</summary>
    [SerializeField]
    [Range(0, 100)]
    private int cost = 0;

    [Header("Options:")]

    /// <summary>The save in inventory</summary>
    [SerializeField]
    private bool saveInInventory = false;

    [Header("Icon:")]

    /// <summary>The icon</summary>
    [SerializeField]
    private Sprite icon = null;

    [Header("Action of item:")]

    /// <summary>The action</summary>
    [SerializeField]
    private ActionType action = ActionType.Nothing;

    /// <summary>The target</summary>
    private GameObject target = null;

    [Header("Animator controller:")]

    /// <summary>The controller</summary>
    [SerializeField]
    private RuntimeAnimatorController controller = null;

    #region Encapsulate Fields

    /// <summary>Gets or sets the name item.</summary>
    /// <value>The name item.</value>
    public string NameItem { get => nameItem; set => nameItem = value; }

    /// <summary>Gets or sets the description.</summary>
    /// <value>The description.</value>
    public Clef Description { get => description; set => description = value; }

    /// <summary>Gets or sets the cost.</summary>
    /// <value>The cost.</value>
    public int Cost { get => cost; set => cost = value; }

    /// <summary>Gets or sets the icon.</summary>
    /// <value>The icon.</value>
    public Sprite Icon { get => icon; set => icon = value; }

    /// <summary>Gets or sets a value indicating whether [save in inventory].</summary>
    /// <value>
    /// <c>true</c> if [save in inventory]; otherwise, <c>false</c>.</value>
    public bool SaveInInventory { get => saveInInventory; set => saveInInventory = value; }

    /// <summary>Gets or sets the action.</summary>
    /// <value>The action.</value>
    public ActionType Action { get => action; set => action = value; }

    /// <summary>Gets or sets the target.</summary>
    /// <value>The target.</value>
    public GameObject Target { get => target; set => target = value; }

    /// <summary>Gets or sets the controller.</summary>
    /// <value>The controller.</value>
    public RuntimeAnimatorController Controller { get => controller; set => controller = value; }
    
    /// <summary>Gets or sets the prefab.</summary>
    /// <value>The prefab.</value>
    public GameObject Prefab { get => prefab; set => prefab = value; }

    #endregion

    #region Use Item
    /// <summary>Uses this item</summary>
    public virtual void Use()
    {
        global::Action.Invoke(Action).In(Target);
    }
    #endregion

    #region Validate Name
#if UNITY_EDITOR
    /// <summary>Called when [validate].</summary>
    public void OnValidate()
    {
        UnityEditor.EditorApplication.delayCall += () =>
        {
            string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, NameItem);
            AssetDatabase.SaveAssets();

            DataItem data = new DataItem(nameItem, description, cost, saveInInventory, action);
            LocalData.Save<DataItem>(data, name, Application.persistentDataPath + "/resources");
        };
    }
#endif
    #endregion
}