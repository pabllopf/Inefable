//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Chest.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Occlusion))]

/// <summary>Manage a chest of the game.</summary>
public class Chest : MonoBehaviour
{
    /// <summary>The open</summary>
    private const string OpenOn = "Open";

    /// <summary>The hit</summary>
    private const string Hit = "Hit";

    /// <summary>The delay</summary>
    private const float Delay = 5f;

    /// <summary>The has key</summary>
    private bool hasKey = false;

    /// <summary>The delay active</summary>
    private bool delayActive = false;

    /// <summary>The hit clip</summary>
    [SerializeField]
    private AudioClip hitClip = null;

    /// <summary>Gets the need key.</summary>
    /// <value>The need key.</value>
    private GameObject NeedKey => this.transform.Find("NeedKey").gameObject;

    /// <summary>Gets the audio source.</summary>
    /// <value>The audio source.</value>
    private AudioSource AudioSource => this.GetComponent<AudioSource>();

    /// <summary>Gets the animator.</summary>
    /// <value>The animator.</value>
    private Animator Animator => this.GetComponent<Animator>();

    /// <summary>Starts this instance.</summary>
    public void Start() => this.NeedKey.SetActive(false);

    /// <summary>Opens this instance.</summary>
    public void Open()
    {
        if (!this.delayActive) 
        {
            this.StartCoroutine(this.DelayToQuitNeedKey());
        }

        if (this.hasKey)
        {
            this.Animator.SetTrigger(OpenOn);
        }
        else 
        {
            this.Animator.SetTrigger(Hit);
            this.PlayClip(this.hitClip);
        }
    }

    /// <summary>Delays to quit need key.</summary>
    /// <returns>Return none</returns>
    private IEnumerator DelayToQuitNeedKey() 
    {
        this.delayActive = true;
        this.NeedKey.SetActive(true);

        yield return new WaitForSeconds(Delay);

        this.delayActive = false;
        this.NeedKey.SetActive(false);
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip)
    {
        this.AudioSource.clip = clip;
        this.AudioSource.Play();
    }
}