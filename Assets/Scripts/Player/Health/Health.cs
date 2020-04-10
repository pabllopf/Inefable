//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Health.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

/// <summary>Manage the health of the player.</summary>
public class Health : MonoBehaviour
{
    /// <summary>The time of effect</summary>
    private const float TimeOfEffect = 0.25f;

    /// <summary>The health</summary>
    private int health = 100;

    /// <summary>The sprite renderer</summary>
    private SpriteRenderer spriteRenderer = null;

    /// <summary>The health marker</summary>
    private Text marker = null;

    /// <summary>The health bar</summary>
    private Scrollbar bar = null;

    /// <summary>The treat clip</summary>
    [SerializeField]
    private AudioClip treatClip = null;

    /// <summary>The take clip</summary>
    [SerializeField]
    private AudioClip takeClip = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>The popup text</summary>
    private PopupText popupText = null;

    /// <summary>Gets a value indicating whether this instance is alive.</summary>
    /// <value>
    /// <c>true</c> if this instance is alive; otherwise, <c>false</c>.</value>
    public bool IsAlive => (health > 0) ? true : false;

    /// <summary>Gets or sets the treat clip.</summary>
    /// <value>The treat clip.</value>
    public AudioClip TreatClip { get => treatClip; set => treatClip = value; }

    /// <summary>Gets or sets the audio source.</summary>
    /// <value>The audio source.</value>
    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }

    /// <summary>Gets or sets the take clip.</summary>
    /// <value>The take clip.</value>
    public AudioClip TakeClip { get => takeClip; set => takeClip = value; }
    
    /// <summary>Gets or sets the popup text.</summary>
    /// <value>The popup text.</value>
    public PopupText PopupText { get => popupText; set => popupText = value; }

    /// <summary>Treat the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Treat(int amount)
    {
        health = ((health + amount) > 100) ? 100 : health + amount;
        bar.size = (float)health / 100;
        marker.text = health.ToString();

        Sound.Play(TreatClip, AudioSource);
        //Data.SaveVar(health).WithName("Health").InFolder("Player");
    }

    /// <summary>Treat full.</summary>
    public void TreatFull()
    {
        health = 100;
        bar.size = (float)health / 100;
        marker.text = health.ToString();

        Sound.Play(TreatClip, AudioSource);
        //Data.SaveVar(health).WithName("Health").InFolder("Player");
    }

    /// <summary>Takes the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Take(int amount)
    {
        health = ((health - amount) <= 0) ? 0 : health - amount;
        bar.size = (float)health / 100;
        marker.text = health.ToString();

        StartCoroutine(TakeAHitEffect(TimeOfEffect, amount));

        Sound.Play(TakeClip, AudioSource);
        //Data.SaveVar(health).WithName("Health").InFolder("Player");
    }

    /// <summary>Awakes this instance.</summary>
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        //health = Data.LoadVar("Health").FromFolder("Player").Int;
        health = (health == 0) ? 100 : health;

        bar = transform.Find("Interface/Bar/Health").GetComponent<Scrollbar>();
        bar.size = (float)health / 100;

        marker = transform.Find("Interface/Bar/Health/Text").GetComponent<Text>();
        marker.text = health.ToString();

        popupText = GetComponent<PopupText>();
    }

    /// <summary>Takes a hit effect.</summary>
    /// <param name="time">The time.</param>
    /// <returns>Return none</returns>
    private IEnumerator TakeAHitEffect(float time, int amount)
    {
        spriteRenderer.color = Color.red;
        popupText.Play(amount.ToString(), Color.red);
        yield return new WaitForSeconds(time);
        spriteRenderer.color = Color.white;
    }
}