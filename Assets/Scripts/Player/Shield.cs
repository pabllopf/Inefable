//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Shield.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

/// <summary>The shield of the player</summary>
public class Shield : MonoBehaviour
{
    /// <summary>The shield UI</summary>
    private Scrollbar scrollbar = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>The take</summary>
    [SerializeField]
    private AudioClip take = null;

    /// <summary>Gets a value indicating whether this instance has shield.</summary>
    /// <value>
    /// <c>true</c> if this instance has shield; otherwise, <c>false</c>.</value>
    public bool HasShield => (Stats.Current.Shield > 0) ? true : false;

    /// <summary>Awakes this instance.</summary>
    public void Awake()
    {
        Game.LoadStats();
    }

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        this.scrollbar = this.transform.Find("Interface/Bar/Shield").GetComponent<Scrollbar>();
        this.scrollbar.size = (float)Stats.Current.Shield * 2 / 100;
        this.audioSource = this.GetComponent<AudioSource>();
        if (Stats.Current.Shield <= 0)
        {
            this.scrollbar.gameObject.SetActive(false);
        }
    }

    /// <summary>Takes the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Take(int amount)
    {
        Stats.Current.Shield -= amount * 2;
        this.scrollbar.size = (float)Stats.Current.Shield * 2 / 100;
        this.PlayClip(this.take);
        if (Stats.Current.Shield <= 0) 
        {
            this.scrollbar.gameObject.SetActive(false);
        }
        Game.SaveStats();
    }

    /// <summary>Set full</summary>
    public void Full()
    {
        Stats.Current.Shield = 50;
        this.scrollbar.gameObject.SetActive(true);
        this.scrollbar.size = (float)Stats.Current.Shield * 2 / 100;
        this.PlayClip(this.take);
        Game.SaveStats();
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip)
    {
        this.audioSource.clip = clip;
        this.audioSource.Play();
    }
}