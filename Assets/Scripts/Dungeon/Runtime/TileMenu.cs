//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="TileMenu.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Tile menu of dungeon.</summary>
[System.Serializable]
public class TileMenu 
{
    /// <summary>The decoration</summary>
    [SerializeField]
    private GameObject prefab = null;

    /// <summary>Gets or sets the prefab.</summary>
    /// <value>The prefab.</value>
    public GameObject Prefab { get => prefab; set => prefab = value; }
}