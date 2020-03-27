//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Attack.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace EnemyIA.Configuration
{
    using System.Collections;
    using System.Linq;
    using UnityEngine;

    /// <summary>Attack of enemys</summary>
    public class Attack
    {
        /// <summary>The attack type</summary>
        private readonly AttackType attackType;

        /// <summary>Initializes a new instance of the <see cref="Attack"/> class.</summary>
        /// <param name="attackType">Type of the action.</param>
        private Attack(AttackType attackType)
        {
            this.attackType = attackType;
        }

        /// <summary>Invokes the specified action type.</summary>
        /// <param name="attackType">Type of the action.</param>
        /// <returns>Return none</returns>
        public static Attack Invoke(AttackType attackType)
        {
            return new Attack(attackType);
        }

        /// <summary>Ins the specified target.</summary>
        /// <param name="target">The target.</param>
        /// <param name="controller">The controller.</param>
        public void In(GameObject target, Enemy controller)
        {
            switch (attackType)
            {
                case AttackType.Melee:
                    Melee(controller);
                    break;

                case AttackType.Shoot:
                    Shoot(target, controller);
                    break;

                case AttackType.Nothing:
                    Nothing();
                    break;
            }
        }

        /// <summary>Melees the specified target.</summary>
        /// <param name="controller">The controller.</param>
        private void Melee(Enemy controller)
        {
            Physics2D.OverlapCircleAll(controller.AttackPosition + (controller.Direction / 3), 0.6f, LayerMask.GetMask("Player"))
            .ToList()
            .FindAll(i => i.CompareTag("Player"))
            .ForEach(i => i.GetComponent<Health>().Take(Random.Range(5, 15)));
        }

        /// <summary>Shoots the specified target.</summary>
        /// <param name="target">The target.</param>
        /// <param name="controller">The controller.</param>
        private void Shoot(GameObject target, Enemy controller)
        {
        }

        /// <summary>Nothings this instance.</summary>
        private void Nothing()
        {
            Debug.LogError("Nothing Action !!!");
        }
    }
}