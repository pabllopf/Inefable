//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Health.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>The health of the player</summary>
public class Health : MonoBehaviour
{
    /// <summary>The take</summary>
    [SerializeField]
    private AudioClip takeClip = null;

    /// <summary>The hit</summary>
    [SerializeField]
    private AudioClip hitClip = null;

    /// <summary>The red effect</summary>
    [SerializeField]
    private GameObject redHit = null;

    /// <summary>Gets a value indicating whether this instance is alive.</summary>
    /// <value>
    /// <c>true</c> if this instance is alive; otherwise, <c>false</c>.</value>
    public bool IsAlive => (Stats.Current.Health > 0) ? true : false;

    /// <summary>Gets the shield.</summary>
    /// <value>The shield.</value>
    private Shield Shield => this.GetComponent<Shield>();

    /// <summary>Gets the scrollbar.</summary>
    /// <value>The scrollbar.</value>
    private Scrollbar Scrollbar => this.transform.Find("Interface/Bar/Health").GetComponent<Scrollbar>();

    /// <summary>Gets the sprite renderer.</summary>
    /// <value>The sprite renderer.</value>
    private SpriteRenderer SpriteRenderer => this.GetComponent<SpriteRenderer>();

    /// <summary>Gets the audio source.</summary>
    /// <value>The audio source.</value>
    private AudioSource AudioSource => this.GetComponent<AudioSource>();

    /// <summary>Awakes this instance.</summary>
    public void Awake() => Game.LoadStats();

    /// <summary>Starts this instance.</summary>
    public void Start() => this.Scrollbar.size = (float)Stats.Current.Health / 100;

    /// <summary>Treats the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Treat(int amount)
    {
        if (Stats.Current.Health + amount > 100)
        {
            Stats.Current.Health = 100;
        }
        else 
        {
            Stats.Current.Health += amount;
        }
        Game.SaveStats();

        this.Scrollbar.size = (float)Stats.Current.Health / 100;
        this.PlayClip(this.takeClip);
    }

    /// <summary>Takes the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Take(int amount)
    {
        if (this.Shield.HasShield)
        {
            this.Shield.Take(amount);
            this.StartCoroutine(this.HitEffect(Color.blue));
        }
        else 
        {
            redHit.GetComponent<TextMeshPro>().text = amount.ToString();
            Instantiate(redHit, this.transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0), Quaternion.identity, this.transform);

            Stats.Current.Health -= amount;
            this.Scrollbar.size = (float)Stats.Current.Health / 100;
            this.StartCoroutine(this.HitEffect(Color.red));
            Game.SaveStats();
        }
    }

    /// <summary>Set full</summary>
    public void Full()
    {
        Stats.Current.Health = 100;
        Game.SaveStats();

        this.Scrollbar.size = (float)Stats.Current.Health / 100;
        this.PlayClip(this.takeClip);
    }

    /// <summary>Determines whether this instance can add the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    /// <returns>
    /// <c>true</c> if this instance can add the specified amount; otherwise, <c>false</c>.</returns>
    public bool CanAdd() => (Stats.Current.Health < 100) ? true : false;

    /// <summary>Hits the effect.</summary>
    /// <param name="color">The color.</param>
    /// <returns>Return none</returns>
    public IEnumerator HitEffect(Color color)
    {
        yield return new WaitForSeconds(0.25f);

        this.SpriteRenderer.color = color;
        this.PlayClip(this.hitClip);

        yield return new WaitForSeconds(0.1f);
        this.SpriteRenderer.color = Color.white;
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip)
    {
        this.AudioSource.clip = clip;
        this.AudioSource.Play();
    }
}