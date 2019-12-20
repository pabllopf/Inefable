//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Wallet.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manage the wallet of the player</summary>
public class Wallet : MonoBehaviour
{
    /// <summary>The open</summary>
    private const string Open = "Open";

    /// <summary>The wallet</summary>
    private int wallet = 0;

    /// <summary>The time counter</summary>
    private float timeCounter = 3f;

    /// <summary>The reset counter</summary>
    private float resetCounter = 3f;

    /// <summary>The wallet UI</summary>
    private Text walletUI = null;

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>The take coin sound</summary>
    [SerializeField]
    private AudioClip takeCoin = null;

    /// <summary>The spend coin</summary>
    [SerializeField]
    private AudioClip spendCoin = null;

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        this.wallet = Stats.Current.Wallet;

        this.animator = transform.Find("Interface/CounterCoins").GetComponent<Animator>();

        this.audioSource = this.GetComponent<AudioSource>();

        this.walletUI = transform.Find("Interface/CounterCoins/Text").GetComponent<Text>();
        this.walletUI.text = this.wallet.ToString() + " $";
        this.animator.SetBool(Open, true);
        this.timeCounter = this.resetCounter;
    }

    /// <summary>Updates this instance.</summary>
    public void Update()
    {
        if (this.animator.GetBool(Open))
        {
            this.timeCounter -= Time.deltaTime;
            if (this.timeCounter <= 0) 
            {
                this.timeCounter = this.resetCounter;
                this.animator.SetBool(Open, false);
            }
        }
    }

    /// <summary>Gets the wallet.</summary>
    /// <returns>The current amount of coins in the wallet.</returns>
    public int GetWallet()
    {
        return this.wallet;
    }

    /// <summary>Takes the coin.</summary>
    public void TakeCoin()
    {
        this.wallet++;
        Stats.Current.Wallet = this.wallet;
        this.walletUI.text = this.wallet.ToString() + " $";
        this.animator.SetBool(Open, true);
        this.timeCounter = this.resetCounter;
        this.PlayClip(this.takeCoin);
    }

    /// <summary>Takes the out.</summary>
    /// <param name="amount">The amount.</param>
    public void TakeOut(int amount)
    {
        this.wallet--;
        Stats.Current.Wallet = this.wallet;
        this.walletUI.text = this.wallet.ToString() + " $";
        this.animator.SetBool(Open, true);
        this.timeCounter = this.resetCounter;
        this.PlayClip(this.spendCoin);
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip) 
    {
        this.audioSource.clip = clip;
        this.audioSource.Play();
    }
}
