//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Shield.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

/// <summary>The shield of the player</summary>
public class Shield : MonoBehaviour
{
    /// <summary>The take</summary>
    [SerializeField]
    private AudioClip take = null;

    /// <summary>Gets a value indicating whether this instance has shield.</summary>
    /// <value>
    /// <c>true</c> if this instance has shield; otherwise, <c>false</c>.</value>
    public bool HasShield => (Stats.Current.Shield > 0) ? true : false;

    /// <summary>Gets the scrollbar.</summary>
    /// <value>The scrollbar.</value>
    private Scrollbar Scrollbar => this.transform.Find("Interface/Bar/Shield").GetComponent<Scrollbar>();

    /// <summary>Gets the audio source.</summary>
    /// <value>The audio source.</value>
    private AudioSource AudioSource => this.GetComponent<AudioSource>();

    /// <summary>Awakes this instance.</summary>
    public void Awake() => Game.LoadStats();

    /// <summary>Starts this instance.</summary>
    public void Start() => this.UpdateShield();

    /// <summary>Takes the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Take(int amount)
    {
        Stats.Current.Shield -= amount;
        Game.SaveStats();

        this.UpdateShield();
        this.PlayClip(this.take);
    }

    /// <summary>Set full</summary>
    public void Full()
    {
        Stats.Current.Shield = 50;
        Game.SaveStats();

        this.UpdateShield();
        this.PlayClip(this.take);
    }

    /// <summary>Updates the shield.</summary>
    private void UpdateShield()
    {
        this.Scrollbar.size = (float)Stats.Current.Shield * 2 / 100;
        this.Scrollbar.gameObject.SetActive((Stats.Current.Shield > 0) ? true : false);
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip)
    {
        this.AudioSource.clip = clip;
        this.AudioSource.Play();
    }
}