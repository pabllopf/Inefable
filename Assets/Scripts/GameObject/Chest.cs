//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Chest.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
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

    /// <summary>The hit</summary>
    private const string Hit = "Hit";

    /// <summary>The health</summary>
    private int health = 100;

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>The hit clip</summary>
    [SerializeField]
    private AudioClip hitClip = null;

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.audioSource = this.GetComponent<AudioSource>();
    }

    /// <summary>Opens up.</summary>
    /// <param name="damage">The damage.</param>
    public void OpenUp(int damage)
    {
        this.health -= damage;

        if (this.health <= 0)
        {
            this.animator.SetTrigger(Open);
        }
        else 
        {
            this.animator.SetTrigger(Hit);
            this.PlayClip(this.hitClip);
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
