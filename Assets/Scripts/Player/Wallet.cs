//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Wallet.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manage the wallet of the player</summary>
public class Wallet : MonoBehaviour
{
    /// <summary>The open</summary>
    private const string Open = "Open";

    /// <summary>The time to reset</summary>
    private const float TimeToReset = 3f;

    /// <summary>The take coin sound</summary>
    [SerializeField]
    private AudioClip takeClip = null;

    /// <summary>The spend coin</summary>
    [SerializeField]
    private AudioClip spendClip = null;

    /// <summary>The audio source</summary>
    private AudioSource AudioSource => this.GetComponent<AudioSource>();

    /// <summary>The wallet UI</summary>
    private Text CounterCoins => this.transform.Find("Interface/CounterCoins/Text").GetComponent<Text>();

    /// <summary>The animator</summary>
    private Animator Animator => this.transform.Find("Interface/CounterCoins").GetComponent<Animator>();

    /// <summary>Awakes this instance.</summary>
    public void Awake() => Game.LoadStats();

    /// <summary>Starts this instance.</summary>
    public void Start() => this.StartCoroutine(this.ControlUI(TimeToReset));

    /// <summary>Adds the coin.</summary>
    public void AddCoin()
    {
        Stats.Current.Wallet++;
        this.CounterCoins.text = Stats.Current.Wallet + " $";

        this.PlayClip(this.takeClip);
        this.StartCoroutine(this.ControlUI(TimeToReset));
    }

    /// <summary>Spends the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Spend(int amount)
    {
        Stats.Current.Wallet -= amount;
        this.CounterCoins.text = Stats.Current.Wallet + " $";

        this.PlayClip(this.spendClip);
        this.StartCoroutine(this.ControlUI(TimeToReset));
    }

    /// <summary>Controls the UI.</summary>
    /// <param name="time">The time.</param>
    /// <returns>Return none</returns>
    private IEnumerator ControlUI(float time)
    {
        this.CounterCoins.text = Stats.Current.Wallet + " $";
        this.Animator.SetBool(Open, true);
        yield return new WaitForSeconds(time);
        this.Animator.SetBool(Open, false);
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip)
    {
        this.AudioSource.clip = clip;
        this.AudioSource.Play();
    }
}
