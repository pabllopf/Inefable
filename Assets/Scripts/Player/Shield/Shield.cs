﻿//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Shield.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>The shield of the player</summary>
public class Shield : MonoBehaviour
{
    /// <summary>The time of effect</summary>
    private const float TimeOfEffect = 0.25f;

    /// <summary>The shield</summary>
    private int shield = 50;

    /// <summary>The sprite renderer</summary>
    private SpriteRenderer spriteRenderer = null;

    /// <summary>The shield object</summary>
    private GameObject shieldObj = null;

    /// <summary>The health marker</summary>
    private Text marker = null;

    /// <summary>The health bar</summary>
    private Scrollbar bar = null;

    /// <summary>The treat clip</summary>
    [SerializeField]
    private AudioClip fullShieldClip = null;

    /// <summary>The take clip</summary>
    [SerializeField]
    private AudioClip takeClip = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>Gets or sets the take clip.</summary>
    /// <value>The take clip.</value>
    public AudioClip TakeClip { get => takeClip; set => takeClip = value; }

    /// <summary>Gets or sets the full shield clip.</summary>
    /// <value>The full shield clip.</value>
    public AudioClip FullShieldClip { get => fullShieldClip; set => fullShieldClip = value; }

    /// <summary>Gets or sets the audio source.</summary>
    /// <value>The audio source.</value>
    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }

    /// <summary>Sets the full.</summary>
    public void SetFull()
    {
        shield = 50;
        bar.size = (float)shield * 2 / 100;
        marker.text = shield.ToString();
        shieldObj.SetActive((shield > 0) ? true : false);

        Sound.Play(FullShieldClip, AudioSource);
        Data.SaveVar(shield).WithName("Shield").InFolder("Player");
    }

    /// <summary>Takes the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Take(int amount)
    {
        shield = ((shield - amount) <= 0) ? 0 : shield - amount;
        bar.size = (float)shield * 2 / 100;
        marker.text = shield.ToString();
        shieldObj.SetActive((shield > 0) ? true : false);

        StartCoroutine(TakeAHitEffect(TimeOfEffect));

        Sound.Play(TakeClip, AudioSource);
        Data.SaveVar(shield).WithName("Shield").InFolder("Player");
    }

    /// <summary>Awakes this instance.</summary>
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();

        shield = Data.LoadVar("Shield").FromFolder("Player").Int;

        bar = transform.Find("Interface/Bar/Shield").GetComponent<Scrollbar>();
        bar.size = (float)shield * 2 / 100;

        marker = transform.Find("Interface/Bar/Shield/Text").GetComponent<Text>();
        marker.text = shield.ToString();

        shieldObj = transform.Find("Interface/Bar/Shield").gameObject;
        shieldObj.SetActive((shield > 0) ? true : false);
    }

    /// <summary>Takes a hit effect.</summary>
    /// <param name="time">The time.</param>
    /// <returns>Return none</returns>
    private IEnumerator TakeAHitEffect(float time)
    {
        spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(time);
        spriteRenderer.color = Color.white;
    }
}