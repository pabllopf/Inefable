//------------------------------------------------------------------------------------------
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

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>Sets the full.</summary>
    public void SetFull()
    {
        shield = 50;
        bar.size = (float)shield * 2 / 100;
        marker.text = shield.ToString();
        shieldObj.SetActive((shield > 0) ? true : false);

        Audio.Play(Sound.TakeItem, audioSource);
        Game.Save(shield).InFolder("Player").WithName("Shield");
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

        Audio.Play(Sound.TakeItem, audioSource);
        Game.Save(shield).InFolder("Player").WithName("Shield");
    }

    /// <summary>Awakes this instance.</summary>
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();

        shield = Game.Load("Shield").OfFolder("Player").Int;

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