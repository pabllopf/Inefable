//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="KeyPack.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manage the oack of keys.</summary>
public class KeyPack : MonoBehaviour
{
    /// <summary>The open</summary>
    private const string Open = "Open";

    /// <summary>The time to reset</summary>
    private const float TimeToReset = 4f;

    /// <summary>The active</summary>
    private bool active = false;

    /// <summary>The take coin sound</summary>
    [SerializeField]
    private AudioClip takeClip = null;

    /// <summary>The spend coin</summary>
    [SerializeField]
    private AudioClip spendClip = null;

    /// <summary>Gets a value indicating whether this instance has keys.</summary>
    /// <value>
    /// <c>true</c> if this instance has keys; otherwise, <c>false</c>.</value>
    public bool HasKeys => (Stats.Current.Keys > 0) ? true : false;

    /// <summary>The audio source</summary>
    private AudioSource AudioSource => this.GetComponent<AudioSource>();

    /// <summary>The wallet UI</summary>
    private Text CounterKeys => this.transform.Find("Interface/CounterKeys/Text").GetComponent<Text>();

    /// <summary>The animator</summary>
    private Animator Animator => this.transform.Find("Interface/CounterKeys").GetComponent<Animator>();

    /// <summary>Awakes this instance.</summary>
    public void Awake() => Game.LoadStats();

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        if (!active)
        {
            this.StartCoroutine(this.ControlUI(TimeToReset));
        }
    }

    /// <summary>Adds the coin.</summary>
    public void AddKey()
    {
        Stats.Current.Keys++;
        this.CounterKeys.text = "x" + Stats.Current.Keys;
        Game.SaveStats();

        this.PlayClip(this.takeClip);
        if (!active)
        {
            this.StartCoroutine(this.ControlUI(TimeToReset));
        }
        else
        {
            this.StopAllCoroutines();
            this.StartCoroutine(this.ControlUI(TimeToReset));
        }
    }

    /// <summary>Spends the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Spend(int amount)
    {
        Stats.Current.Keys -= amount;
        this.CounterKeys.text = "x" + Stats.Current.Keys;
        Game.SaveStats();

        this.PlayClip(this.spendClip);
        if (!active)
        {
            this.StartCoroutine(this.ControlUI(TimeToReset));
        }
        else
        {
            this.StopAllCoroutines();
            this.StartCoroutine(this.ControlUI(TimeToReset));
        }
    }

    /// <summary>Actives the UI.</summary>
    public void ActiveUI() 
    {
        if (!active)
        {
            this.StartCoroutine(this.ControlUI(TimeToReset));
        }
        else
        {
            this.StopAllCoroutines();
            this.StartCoroutine(this.ControlUI(TimeToReset));
        }
    }

    /// <summary>Controls the UI.</summary>
    /// <param name="time">The time.</param>
    /// <returns>Return none</returns>
    private IEnumerator ControlUI(float time)
    {
        this.active = true;
        this.CounterKeys.text = "x" + Stats.Current.Keys;
        this.Animator.SetBool(Open, true);

        yield return new WaitForSeconds(time);

        this.Animator.SetBool(Open, false);
        this.active = false;
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip)
    {
        this.AudioSource.clip = clip;
        this.AudioSource.Play();
    }
}