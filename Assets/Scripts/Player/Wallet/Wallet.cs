//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Wallet.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utils;

/// <summary>Manage the wallet of a player.</summary>
public class Wallet : MonoBehaviour
{
    /// <summary>The show UI</summary>
    private const string ShowUI = "ShowUI";

    /// <summary>The time to hide UI</summary>
    private const float TimeToHideUI = 4f;

    /// <summary>The money</summary>
    private int money = 0;

    /// <summary>The counter coins</summary>
    private Text counterCoins = null;

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>The treat clip</summary>
    [SerializeField]
    private AudioClip addCoinClip = null;

    /// <summary>The take clip</summary>
    [SerializeField]
    private AudioClip spendCoinClip = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>Gets or sets the audio source.</summary>
    /// <value>The audio source.</value>
    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }

    /// <summary>Gets or sets the spend coin clip.</summary>
    /// <value>The spend coin clip.</value>
    public AudioClip SpendCoinClip { get => spendCoinClip; set => spendCoinClip = value; }

    /// <summary>Gets or sets the add coin clip.</summary>
    /// <value>The add coin clip.</value>
    public AudioClip AddCoinClip { get => addCoinClip; set => addCoinClip = value; }

    /// <summary>Determines whether this instance can spend the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    /// <returns>
    /// <c>true</c> if this instance can spend the specified amount; otherwise, <c>false</c>.</returns>
    public bool CanSpend(int amount)
    {
        return ((money -= amount) >= 0) ? true : false;
    }

    /// <summary>Add a coin.</summary>
    public void AddCoin()
    {
        money++;
        counterCoins.text = money.ToString();

        if (animator.GetBool(ShowUI))
        {
            StopAllCoroutines();
        }

        StartCoroutine(ShowUINow(TimeToHideUI));

        Sound.Play(AddCoinClip, AudioSource);
        //Data.SaveVar(money).WithName("Money").InFolder("Player");
    }

    /// <summary>Spends the specified amount.</summary>
    /// <param name="amount">The amount.</param>
    public void Spend(int amount)
    {
        money -= amount;
        counterCoins.text = money.ToString();

        if (animator.GetBool(ShowUI))
        {
            StopAllCoroutines();
        }

        StartCoroutine(ShowUINow(TimeToHideUI));

        Sound.Play(SpendCoinClip, AudioSource);
        //Data.SaveVar(money).WithName("Money").InFolder("Player");
    }

    /// <summary>Awakes this instance.</summary>
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        money = 1;// Data.LoadVar("Money").FromFolder("Player").Int;

        animator = transform.Find("Interface/CounterCoins").GetComponent<Animator>();

        counterCoins = transform.Find("Interface/CounterCoins/Text").GetComponent<Text>();
        counterCoins.text = money.ToString();
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        if (!animator.GetBool(ShowUI))
        {
            StartCoroutine(ShowUINow(TimeToHideUI));
        }
    }

    /// <summary>Controls the UI.</summary>
    /// <param name="time">The time.</param>
    /// <returns>Return none</returns>
    private IEnumerator ShowUINow(float time)
    {
        animator.SetBool(ShowUI, true);
        yield return new WaitForSeconds(time);
        animator.SetBool(ShowUI, false);
    }
}