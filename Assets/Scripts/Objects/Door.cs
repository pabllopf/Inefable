//-----------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Door.cs" company="UnMedioStudio">Open Source</copyright>
//-----------------------------------------------------------------------
using UnityEngine;

/// <summary>Class to control a door.</summary>
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Occlusion))]
public class Door : MonoBehaviour
{
    /// <summary>The door close</summary>
    [SerializeField]
    private Sprite doorClose = null;

    /// <summary>The order layer close</summary>
    [SerializeField]
    private int orderLayerClose = 2;

    /// <summary>The door open</summary>
    [SerializeField]
    private Sprite doorOpen = null;

    /// <summary>The order layer open</summary>
    [SerializeField]
    private int orderLayerOpen = 5;

    /// <summary>The sprite renderer</summary>
    private SpriteRenderer spriteRenderer;

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = this.doorClose;
    }

    /// <summary>Called when [trigger stay2 d].</summary>
    /// <param name="obj">The object.</param>
    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            this.spriteRenderer.sprite = this.doorOpen;
            this.spriteRenderer.sortingOrder = this.orderLayerOpen;
        }
    }

    /// <summary>Called when [trigger exit2 d].</summary>
    /// <param name="obj">The object.</param>
    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            this.spriteRenderer.sprite = this.doorClose;
            this.spriteRenderer.sortingOrder = this.orderLayerClose;
        }
    }
}
