﻿//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="PotionPurple.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using Mirror;
using UnityEngine;

/// <summary>A potion purple in the game.</summary>
public class PotionPurple : MonoBehaviour, IItem
{
    /// <summary>The icon</summary>
    [SerializeField]
    private readonly Sprite icon = null;

    /// <summary>The item</summary>
    private const string ItemName = "PotionPurple";

    /// <summary>Gets the icon.</summary>
    /// <value>The icon.</value>
    private Sprite Icon => Resources.Load<Sprite>("Icons/" + ItemName);

    /// <summary>Gets the name.</summary>
    /// <returns>The name</returns>
    public string GetName()
    {
        return ItemName;
    }

    /// <summary>Gets the icon.</summary>
    /// <returns>The icon</returns>
    public Sprite GetIcon()
    {
        return Icon;
    }

    /// <summary>Called when [trigger enter2 d].</summary>
    /// <param name="obj">The object.</param>
    public void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            Inventory inventory = obj.GetComponent<Inventory>();
            if (inventory.HasSpace)
            {
                inventory.AddItem(GetComponent<PotionPurple>());
                NetworkServer.Destroy(gameObject);
            }
        }
    }

    /// <summary>Actions this instance.</summary>
    /// <param name="obj">The object.</param>
    public void Action(GameObject obj)
    {
    }
}