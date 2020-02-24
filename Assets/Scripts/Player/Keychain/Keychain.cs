//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Keychain.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manage the Key Chain of a player.</summary>
public class Keychain : MonoBehaviour
{
    /// <summary>The show UI</summary>
    private const string ShowUI = "ShowUI";

    /// <summary>The time to hide UI</summary>
    private const float TimeToHideUI = 4f;

    /// <summary>The number of keys</summary>
    private int numOfKeys = 0;

    /// <summary>The counter keys</summary>
    private Text counterKeys = null;

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>Determines whether this instance [can spend a key].</summary>
    /// <returns>
    /// <c>true</c> if this instance [can spend a key]; otherwise, <c>false</c>.</returns>
    public bool CanSpendAKey()
    {
        return (numOfKeys > 0) ? true : false;
    }

    /// <summary>Add a key.</summary>
    public void AddKey()
    {
        numOfKeys++;
        counterKeys.text = numOfKeys.ToString();

        if (animator.GetBool(ShowUI))
        {
            StopAllCoroutines();
            StartCoroutine(ShowUINow(TimeToHideUI));
        }
        else
        {
            StartCoroutine(ShowUINow(TimeToHideUI));
        }

        Sound.Play(SoundClip.TakeItem, audioSource);
        Data.SaveVar(numOfKeys).WithName("NumOfKeys").InFolder("Player");
    }

    /// <summary>Spend a key.</summary>
    public void SpendAKey()
    {
        numOfKeys--;
        counterKeys.text = numOfKeys.ToString();

        if (animator.GetBool(ShowUI))
        {
            StopAllCoroutines();
            StartCoroutine(ShowUINow(TimeToHideUI));
        }
        else
        {
            StartCoroutine(ShowUINow(TimeToHideUI));
        }

        Sound.Play(SoundClip.TakeItem, audioSource);
        Data.SaveVar(numOfKeys).WithName("NumOfKeys").InFolder("Player");
    }

    /// <summary>Actives the UI.</summary>
    public void ActiveUI()
    {
        StopAllCoroutines();
        StartCoroutine(ShowUINow(TimeToHideUI));
    }

    /// <summary>Awakes this instance.</summary>
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        numOfKeys = Data.LoadVar("NumOfKeys").FromFolder("Player").Int;

        animator = transform.Find("Interface/CounterKeys").GetComponent<Animator>();

        counterKeys = transform.Find("Interface/CounterKeys/Text").GetComponent<Text>();
        counterKeys.text = numOfKeys.ToString();
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