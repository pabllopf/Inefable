//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Chest.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Occlusion))]

/// <summary>Manage a chest of the game.</summary>
public class Chest : MonoBehaviour
{
    /// <summary>The open</summary>
    private const string Open = "Open";

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>The items</summary>
    [SerializeField]
    private List<GameObject> items;

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }

    /// <summary>Opens up.</summary>
    /// <param name="player">The player.</param>
    public void OpenUp(Transform player)
    {
        this.animator.SetBool(Open, true);
    }
}
