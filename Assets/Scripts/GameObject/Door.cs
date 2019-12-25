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
    private SpriteRenderer spriteRenderer = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>The open door sound</summary>
    [SerializeField]
    private AudioClip openDoor = null;

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.audioSource = this.GetComponent<AudioSource>();

        this.spriteRenderer.sprite = this.doorClose;
        this.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    /// <summary>Called when [trigger stay2 d].</summary>
    /// <param name="obj">The object.</param>
    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            this.spriteRenderer.sprite = this.doorOpen;
            this.spriteRenderer.sortingOrder = this.orderLayerOpen;
            this.PlayClip(this.openDoor);
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

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip)
    {
        this.audioSource.clip = clip;
        this.audioSource.Play();
    }
}
