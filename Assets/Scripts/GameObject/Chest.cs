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
    private const string Open = "Open";

    /// <summary>The hit</summary>
    private const string Hit = "Hit";

    /// <summary>The delay</summary>
    private const float Delay = 5f;

    /// <summary>The health</summary>
    private int health = 100;

    /// <summary>The need key</summary>
    private GameObject needKey = null;

    /// <summary>The delay active</summary>
    private bool delayActive = false;

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
        this.needKey = this.transform.Find("NeedKey").gameObject;
        this.needKey.SetActive(false);

        this.animator = this.GetComponent<Animator>();
        this.audioSource = this.GetComponent<AudioSource>();
    }

    /// <summary>Opens up.</summary>
    /// <param name="damage">The damage.</param>
    public void OpenUp(int damage)
    {
        this.health -= damage;
        if (!this.delayActive) 
        {
            this.StartCoroutine(this.DelayToQuitNeedKey());
        }

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

    /// <summary>Delays to quit need key.</summary>
    /// <returns>Return none</returns>
    private IEnumerator DelayToQuitNeedKey() 
    {
        this.delayActive = true;
        this.needKey.SetActive(true);

        yield return new WaitForSeconds(Delay);

        this.delayActive = false;
        this.needKey.SetActive(false);
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip)
    {
        this.audioSource.clip = clip;
        this.audioSource.Play();
    }
}
