//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="PickUp.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Pick up a item of the game. </summary>
public class PickUp : MonoBehaviour
{
    /// <summary>The item</summary>
    [SerializeField]
    private Item item = null;

    /// <summary>Gets or sets the item.</summary>
    /// <value>The item.</value>
    public Item Item
    {
        get => item;
        set => item = value;
    }

    /// <summary>Called when [trigger enter2 d].</summary>
    /// <param name="obj">The object.</param>
    public void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            Item.Target = obj.gameObject;
            if (Item.SaveInInventory)
            {
                if (obj.GetComponent<Inventory>().HasSpace)
                {
                    obj.GetComponent<Inventory>().AddItem(Item);
                    Destroy(gameObject);
                }
            }
            else
            {
                Item.Use();
                Destroy(gameObject);
            }
        }
    }
}