//-----------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Item.cs" company="UnMedioStudio">Open Source</copyright>
//-----------------------------------------------------------------------
using UnityEngine;

/// <summary>Define a item of a dungeon</summary>
[System.Serializable]
public class Item 
{
    /// <summary>The item</summary>
    [SerializeField]
    [Tooltip("Item to will be spawned.")]
    private GameObject item;

    /// <summary>The quantity minimum</summary>
    [SerializeField]
    [Range(1, 100)]
    [Tooltip("Quantity min of items will be spawned.")]
    private int quantityMin;

    /// <summary>The quantity maximum</summary>
    [SerializeField]
    [Range(1, 100)]
    [Tooltip("Quantity max of items will be spawned.")]
    private int quantityMax;

    /// <summary>The position</summary>
    [SerializeField]
    [Range(1, 15)]
    [Tooltip("Select the sprite position to spawn the item.")]
    private int position;

    /// <summary>Initializes a new instance of the <see cref="Item"/> class.</summary>
    /// <param name="item">The item.</param>
    /// <param name="quantityMin">The quantity minimum.</param>
    /// <param name="quantityMax">The quantity maximum.</param>
    /// <param name="position">The position.</param>
    /// <param name="dungeon">The dungeon.</param>
    public Item(GameObject item, int quantityMin, int quantityMax, int position)
    {
        this.item = item;
        this.quantityMin = quantityMin;
        this.quantityMax = quantityMax;
        this.position = position;
    }

    /// <summary>Gets the item.</summary>
    /// <returns>The item</returns>
    public GameObject GetItem()
    {
        return this.item;
    }

    /// <summary>Gets the quantity minimum.</summary>
    /// <returns>The quantityMin</returns>
    public int GetQuantityMin() 
    {
        return this.quantityMin;
    }

    /// <summary>Gets the quantity maximum.</summary>
    /// <returns>The quantityMax</returns>
    public int GetQuantityMax()
    {
        return this.quantityMax;
    }

    /// <summary>Gets the position.</summary>
    /// <returns>The position</returns>
    public int GetPosition()
    {
        return this.position;
    }
}