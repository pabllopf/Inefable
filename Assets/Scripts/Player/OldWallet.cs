//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Wallet.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;

/// <summary>Manage the wallet of the player</summary>
public class OldWallet : MonoBehaviour
{
    /// <summary>The show UI</summary>
    private const string ShowUI = "ShowUI";

    /// <summary>The time to reset</summary>
    private const float TimeToReset = 4f;

    /// <summary>The active</summary>
    private bool active = false;

    /// <summary>The take coin sound</summary>
    [SerializeField]
    private readonly AudioClip takeClip = null;

    /// <summary>The spend coin</summary>
    [SerializeField]
    private readonly AudioClip spendClip = null;

    /// <summary>Gets a value indicating whether this instance has coins.</summary>
    /// <value>
    /// <c>true</c> if this instance has coins; otherwise, <c>false</c>.</value>
    public bool HasCoins => (Stats.Current.Wallet > 0) ? true : false;

    /// <summary>The audio source</summary>
    //private AudioSource AudioSource => GetComponent<AudioSource>();

    /// <summary>The wallet UI</summary>
    //private Text CounterCoins => transform.Find("Interface/CounterCoins/Text").GetComponent<Text>();

    /// <summary>The animator</summary>
    //private Animator Animator => transform.Find("Interface/CounterCoins").GetComponent<Animator>();

    /// <summary>Awakes this instance.</summary>


    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        if (!active)
        {
            StartCoroutine(ControlUI(TimeToReset));
        }
    }

    /// <summary>Adds the coin.</summary>
    public void AddCoin()
    {
        Stats.Current.Wallet++;
        //CounterCoins.text = "x" + Stats.Current.Wallet;

        PlayClip(takeClip);
        if (!active)
        {
            StartCoroutine(ControlUI(TimeToReset));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(ControlUI(TimeToReset));
        }

    }

    /// <summary>Spends the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Spend(int amount)
    {
        Stats.Current.Wallet -= amount;
        //CounterCoins.text = "x" + Stats.Current.Wallet;

        PlayClip(spendClip);
        if (!active)
        {
            StartCoroutine(ControlUI(TimeToReset));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(ControlUI(TimeToReset));
        }
    }

    /// <summary>Controls the UI.</summary>
    /// <param name="time">The time.</param>
    /// <returns>Return none</returns>
    private IEnumerator ControlUI(float time)
    {
        active = true;
        //CounterCoins.text = "x" + Stats.Current.Wallet;
        //Animator.SetBool(Open, true);

        yield return new WaitForSeconds(time);

        //Animator.SetBool(Open, false);
        active = false;
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip)
    {
        //AudioSource.clip = clip;
        //AudioSource.Play();
    }
}
