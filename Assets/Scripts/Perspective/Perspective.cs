//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Perspective.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Class to control the perspective of a object.</summary>
public class Perspective : MonoBehaviour
{
    /// <summary>The order layer front</summary>
    [Range(1, 10)]
    [SerializeField]
    private int orderLayerFront = 2;

    /// <summary>The order layer back</summary>
    [Range(1, 10)]
    [SerializeField]
    private int orderLayerBack = 5;

    /// <summary>Gets or sets the order layer front.</summary>
    /// <value>The order layer front.</value>
    public int OrderLayerFront
    {
        get => orderLayerFront;
        set => orderLayerFront = value;
    }

    /// <summary>Gets or sets the order layer back.</summary>
    /// <value>The order layer back.</value>
    public int OrderLayerBack
    {
        get => orderLayerBack;
        set => orderLayerBack = value;
    }

    /// <summary>The sprite renderer</summary>
    private SpriteRenderer SpriteRenderer => GetComponent<SpriteRenderer>();

    /// <summary>Called when [trigger stay2 d].</summary>
    /// <param name="obj">The object.</param>
    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Player") || obj.CompareTag("Enemy"))
        {
            SpriteRenderer.sortingOrder = OrderLayerBack;
            SpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    /// <summary>Called when [trigger exit2 d].</summary>
    /// <param name="obj">The object.</param>
    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player") || obj.CompareTag("Enemy"))
        {
            SpriteRenderer.sortingOrder = OrderLayerFront;
            SpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}