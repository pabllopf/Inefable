//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Perspective.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Class to control the perspective of a object.</summary>
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Occlusion))]
public class Perspective : MonoBehaviour
{
    /// <summary>The order layer front</summary>
    [SerializeField]
    private int orderLayerFront = 2;

    /// <summary>The order layer back</summary>
    [SerializeField]
    private int orderLayerBack = 5;

    /// <summary>The sprite renderer</summary>
    private SpriteRenderer spriteRenderer;

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    /// <summary>Called when [trigger stay2 d].</summary>
    /// <param name="obj">The object.</param>
    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            this.spriteRenderer.sortingOrder = this.orderLayerBack;
        }
    }

    /// <summary>Called when [trigger exit2 d].</summary>
    /// <param name="obj">The object.</param>
    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            this.spriteRenderer.sortingOrder = this.orderLayerFront;
        }
    }
}