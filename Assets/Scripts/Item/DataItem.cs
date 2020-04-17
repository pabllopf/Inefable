//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="DataItem.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Data Item of the game</summary>
[System.Serializable]
public class DataItem 
{
    /// <summary>The name</summary>
    [SerializeField]
    private string nameItem = "New Item";

    /// <summary>The description</summary>
    [SerializeField]
    private Clef description = Clef.A1;

    /// <summary>The cost</summary>
    [SerializeField]
    private int cost = 0;

    /// <summary>The save in inventory</summary>
    [SerializeField]
    private bool saveInInventory = false;

    /// <summary>The action</summary>
    [SerializeField]
    private ActionType action = ActionType.Nothing;

    public DataItem(string nameItem, Clef description, int cost, bool saveInInventory, ActionType action)
    {
        this.nameItem = nameItem;
        this.description = description;
        this.cost = cost;
        this.saveInInventory = saveInInventory;
        this.action = action;
    }

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

    /// <summary>Gets or sets a value indicating whether [save in inventory].</summary>
    /// <value>
    /// <c>true</c> if [save in inventory]; otherwise, <c>false</c>.</value>
    public bool SaveInInventory { get => saveInInventory; set => saveInInventory = value; }

    /// <summary>Gets or sets the action.</summary>
    /// <value>The action.</value>
    public ActionType Action { get => action; set => action = value; }

    #endregion
}