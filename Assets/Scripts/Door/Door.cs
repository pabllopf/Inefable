//-----------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Door.cs" company="UnMedioStudio">Open Source</copyright>
//-----------------------------------------------------------------------
using UnityEngine;

/// <summary>Class to control a door.</summary>
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Occlusion))]
public class Door : MonoBehaviour
{
    /// <summary>The door close</summary>
    [SerializeField]
    private Sprite doorClose = null;

    /// <summary>The door open</summary>
    [SerializeField]
    private Sprite doorOpen = null;

    /// <summary>The order layer close</summary>
    [Range(1, 10)]
    [SerializeField]
    private int orderLayerClose = 2;

    /// <summary>The order layer open</summary>
    [Range(1, 10)]
    [SerializeField]
    private int orderLayerOpen = 5;

    /// <summary>The open door sound</summary>
    [SerializeField]
    private AudioClip openDoor = null;

    /// <summary>The audio source</summary>
    private AudioSource AudioSource => this.GetComponent<AudioSource>();

    /// <summary>Gets the sprite renderer.</summary>
    /// <value>The sprite renderer.</value>
    private SpriteRenderer SpriteRenderer => this.GetComponent<SpriteRenderer>();

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        this.SpriteRenderer.sprite = this.doorClose;
        this.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    /// <summary>Called when [trigger stay2 d].</summary>
    /// <param name="obj">The object.</param>
    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            this.SpriteRenderer.sprite = this.doorOpen;
            this.SpriteRenderer.sortingOrder = this.orderLayerOpen;
            this.PlayClip(this.openDoor);
        }
    }

    /// <summary>Called when [trigger exit2 d].</summary>
    /// <param name="obj">The object.</param>
    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            this.SpriteRenderer.sprite = this.doorClose;
            this.SpriteRenderer.sortingOrder = this.orderLayerClose;
        }
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip)
    {
        this.AudioSource.clip = clip;
        this.AudioSource.Play();
    }
}
