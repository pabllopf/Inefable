//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Health.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

/// <summary>The health of the player</summary>
public class Health : MonoBehaviour
{
    /// <summary>The scrollbar</summary>
    private Scrollbar scrollbar = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>The take</summary>
    [SerializeField]
    private AudioClip take = null;

    /// <summary>Gets a value indicating whether this instance is alive.</summary>
    /// <value>
    /// <c>true</c> if this instance is alive; otherwise, <c>false</c>.</value>
    public bool IsAlive => (Stats.Current.Health > 0) ? true : false;

    /// <summary>Awakes this instance.</summary>
    public void Awake()
    {
        Game.LoadStats();
    }

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        this.scrollbar = this.transform.Find("Interface/Bar/Health").GetComponent<Scrollbar>();
        this.audioSource = this.GetComponent<AudioSource>();
    }

    /// <summary>Treats the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Treat(int amount)
    {
        Stats.Current.Health += amount;
        this.scrollbar.size = (float)Stats.Current.Health / 100;
        this.PlayClip(this.take);
    }

    /// <summary>Takes the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Take(int amount)
    {
        Stats.Current.Health -= amount;
        this.scrollbar.size = (float)Stats.Current.Health / 100;
        this.PlayClip(this.take);
    }

    /// <summary>Set full</summary>
    public void Full()
    {
        Stats.Current.Health = 100;
        this.scrollbar.size = (float)Stats.Current.Health / 100;
        this.PlayClip(this.take);
    }

    /// <summary>Determines whether this instance can add the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    /// <returns>
    /// <c>true</c> if this instance can add the specified amount; otherwise, <c>false</c>.</returns>
    public bool CanAdd(int amount) => ((Stats.Current.Health + amount) < 100) ? true : false;

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip)
    {
        this.audioSource.clip = clip;
        this.audioSource.Play();
    }
}