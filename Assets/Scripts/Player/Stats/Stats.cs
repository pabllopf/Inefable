//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Player.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manage the stats of the player.</summary>
public class Stats : MonoBehaviour
{
    /// <summary>The show UI</summary>
    private const string ShowUI = "ShowUI";

    /// <summary>The time to disable speed</summary>
    private float timeToDisableSpeed = 0;

    /// <summary>The seconds</summary>
    private int seconds = 0;

    private bool speedIsUp = false;

    private bool damageIsUp = false;

    /// <summary>The player</summary>
    private Player player = null;

    /// <summary>The counter time</summary>
    private Text counterTime = null;

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>Awakes this instance.</summary>
    private void Awake()
    {
        player = GetComponent<Player>();
        counterTime = transform.Find("Interface/CounterEffect/Text").GetComponent<Text>();
        counterTime.text = "";
        animator = transform.Find("Interface/CounterEffect").GetComponent<Animator>();
    }

    /// <summary>Increaseses the speed.</summary>
    /// <param name="time">The time.</param>
    public void IncreasesSpeed(float time) 
    {
        animator.SetBool(ShowUI, true);
        speedIsUp = true;
        timeToDisableSpeed = time;
        player.SpeedOfMove *= 2;
        StopAllCoroutines();
        StartCoroutine(ActiveUI(time));
    }

    public void IncreasesDamage(float time) 
    {
        animator.SetBool(ShowUI, true);
        damageIsUp = true;
        timeToDisableSpeed = time;
        player.DamageOfAttack *= 2;
        StopAllCoroutines();
        StartCoroutine(ActiveUI(time));
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (speedIsUp || damageIsUp) 
        {
            timeToDisableSpeed -= Time.deltaTime;
            if (timeToDisableSpeed > 0)
            {
                seconds = (int)(timeToDisableSpeed % 60);
                counterTime.text = seconds.ToString() + " s";
            }
        }
    }

    /// <summary>Actives the UI.</summary>
    /// <param name="time">The time.</param>
    /// <returns></returns>
    private IEnumerator ActiveUI(float time) 
    {
        yield return new WaitForSeconds(time);
        animator.SetBool(ShowUI, false);
        if (damageIsUp) 
        {
            player.DamageOfAttack /= 2;
        }
        if (speedIsUp) 
        {
            player.SpeedOfMove /= 2;
        }
        
        speedIsUp = false;
        damageIsUp = false;
        
    }
}
