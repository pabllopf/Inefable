//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Wallet.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manage the wallet of the player</summary>
public class Wallet : MonoBehaviour
{
    /// <summary>The wallet</summary>
    private int wallet;

    /// <summary>The wallet UI</summary>
    private Text walletUI;

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        this.wallet = Stats.Current.Wallet;
        this.walletUI = transform.Find("Interface").Find("CounterCoins").GetComponent<Text>();
        this.walletUI.text = this.wallet.ToString();
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
        this.walletUI.text = this.wallet.ToString();
    }

    /// <summary>Takes the out.</summary>
    /// <param name="amount">The amount.</param>
    public void TakeOut(int amount)
    {
        this.wallet--;
        Stats.Current.Wallet = this.wallet;
        this.walletUI.text = this.wallet.ToString();
    }
}
