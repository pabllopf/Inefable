//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Attack.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using EnemyIA;
using System.Linq;
using UnityEngine;

/// <summary>Attacks of the players</summary>
public class Attack
{
    /// <summary>The attack type</summary>
    private readonly AttackType attackType;

    /// <summary>Initializes a new instance of the <see cref="Attack"/> class.</summary>
    /// <param name="attackType">Type of the attack.</param>
    private Attack(AttackType attackType)
    {
        this.attackType = attackType;
    }

    /// <summary>Invokes the specified attack type.</summary>
    /// <param name="attackType">Type of the attack.</param>
    /// <returns>Return new attack</returns>
    public static Attack Invoke(AttackType attackType)
    {
        return new Attack(attackType);
    }

    /// <summary>Of this.</summary>
    /// <param name="obj">The object.</param>
    public void OfThis(GameObject obj)
    {
        switch (attackType)
        {
            case AttackType.Melee:
                Melee(obj);
                break;
        }
    }

    /// <summary>Melees the specified object.</summary>
    /// <param name="obj">The object.</param>
    private void Melee(GameObject obj)
    {
        Player player = obj.GetComponent<Player>();

        player.Animator.SetBool("Attack", true);

        Physics2D.OverlapCircleAll(player.AttackVector, player.TypePlayer.RadiusAttack, LayerMask.GetMask("Enemy"))
            .ToList()
            .FindAll(i => i.CompareTag("Enemy"))
            .ForEach(i => i.GetComponent<Enemy>().TakeDamage(5));
    }
}