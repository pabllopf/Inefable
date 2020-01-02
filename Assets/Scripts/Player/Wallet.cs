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

    /// <summary>The wallet UI</summary>
    private Text counterCoins = null;

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>The take coin sound</summary>
    [SerializeField]
    private AudioClip takeClip = null;

    /// <summary>The spend coin</summary>
    [SerializeField]
    private AudioClip spendClip = null;

    /// <summary>Awakes this instance.</summary>
    public void Awake()
    {
        Game.LoadStats();
    }

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        this.counterCoins = this.transform.Find("Interface/CounterCoins/Text").GetComponent<Text>();
        this.animator = this.transform.Find("Interface/CounterCoins").GetComponent<Animator>();
        this.audioSource = this.GetComponent<AudioSource>();

        this.counterCoins.text = Stats.Current.Wallet + " $";

        this.StartCoroutine(this.ControlUI(TimeToReset));
    }

    /// <summary>Adds the coin.</summary>
    public void AddCoin()
    {
        Stats.Current.Wallet++;
        this.counterCoins.text = Stats.Current.Wallet + " $";

        this.PlayClip(this.takeClip);
        this.StartCoroutine(this.ControlUI(TimeToReset));
    }

    /// <summary>Spends the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Spend(int amount)
    {
        Stats.Current.Wallet -= amount;
        this.counterCoins.text = Stats.Current.Wallet + " $";

        this.PlayClip(this.spendClip);
        this.StartCoroutine(this.ControlUI(TimeToReset));
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip) 
    {
        this.audioSource.clip = clip;
        this.audioSource.Play();
    }

    /// <summary>Controls the UI.</summary>
    /// <param name="time">The time.</param>
    /// <returns>Return none</returns>
    private IEnumerator ControlUI(float time)
    {
        this.animator.SetBool(Open, true);
        yield return new WaitForSeconds(time);
        this.animator.SetBool(Open, false);
    }
}
