//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Slot.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Manage slot of inventory.</summary>
[System.Serializable]
public class Slot
{
    /// <summary>The name item</summary>
    [SerializeField]
    private string nameItem = string.Empty;

    /// <summary>Initializes a new instance of the <see cref="Slot"/> class.</summary>
    /// <param name="nameItem">The name item.</param>
    public Slot(string nameItem)
    {
        this.nameItem = nameItem;
    }

    /// <summary>Gets or sets the name item.</summary>
    /// <value>The name item.</value>
    public string NameItem { get => nameItem; set => nameItem = value; }
}
