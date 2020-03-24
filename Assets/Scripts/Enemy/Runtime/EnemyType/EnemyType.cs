//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="EnemyType.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace EnemyIA.Configuration
{
    using UnityEngine;

    /// <summary>Define a enemy of the game.</summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Game/New Enemy")]
    public class EnemyType : ScriptableObject
    {
        [Header("Name:")]

        /// <summary>The name enemy</summary>
        [SerializeField]
        private string nameEnemy = string.Empty;

        [Header("Options")]

        /// <summary>The is static enemy</summary>
        [SerializeField]
        private bool isStaticEnemy = false;

        [Header("Configurations:")]

        /// <summary>The health</summary>
        [SerializeField]
        [Range(10, 250)]
        private int health = 100;

        /// <summary>The speed to move</summary>
        [SerializeField]
        [Range(1, 10)]
        private float speedToMove = 1f;

        /// <summary>The range of vision</summary>
        [SerializeField]
        [Range(1, 10)]
        private float rangeOfVision = 1f;

        /// <summary>The range of attack</summary>
        [SerializeField]
        [Range(0.5f, 10)]
        private float rangeOfAttack = 0.5f;

        /// <summary>The frequency to attack</summary>
        [SerializeField]
        [Range(0.1f, 1f)]
        private float frequencyToAttack = 1f;

        [Header("Attack of enemy:")]

        /// <summary>The attack</summary>
        [SerializeField]
        private AttackType attackType = AttackType.Nothing;

        /// <summary>The animator</summary>
        [Header("Animator controller:")]

        [SerializeField]
        private RuntimeAnimatorController animator = null;

        /// <summary>The target</summary>
        private GameObject target = null;

        /// <summary>The controller</summary>
        private Enemy controller = null;

        #region Encapsulate Fields

        /// <summary>Gets or sets the name enemy.</summary>
        /// <value>The name enemy.</value>
        public string NameEnemy { get => nameEnemy; set => nameEnemy = value; }

        /// <summary>Gets or sets the health.</summary>
        /// <value>The health.</value>
        public int Health { get => health; set => health = value; }

        /// <summary>Gets or sets a value indicating whether this instance is static enemy.</summary>
        /// <value>
        /// <c>true</c> if this instance is static enemy; otherwise, <c>false</c>.</value>
        public bool IsStaticEnemy { get => isStaticEnemy; set => isStaticEnemy = value; }

        /// <summary>Gets or sets the speed to move.</summary>
        /// <value>The speed to move.</value>
        public float SpeedToMove { get => speedToMove; set => speedToMove = value; }

        /// <summary>Gets or sets the range of vision.</summary>
        /// <value>The range of vision.</value>
        public float RangeOfVision { get => rangeOfVision; set => rangeOfVision = value; }
        
        /// <summary>Gets or sets the range of attack.</summary>
        /// <value>The range of attack.</value>
        public float RangeOfAttack { get => rangeOfAttack; set => rangeOfAttack = value; }

        /// <summary>Gets or sets the frequency to attack.</summary>
        /// <value>The frequency to attack.</value>
        public float FrequencyToAttack { get => frequencyToAttack; set => frequencyToAttack = value; }

        /// <summary>Gets or sets the attack1.</summary>
        /// <value>The attack1.</value>
        public AttackType AttackType { get => attackType; set => attackType = value; }

        /// <summary>Gets or sets the controller.</summary>
        /// <value>The controller.</value>
        public RuntimeAnimatorController Animator { get => animator; set => animator = value; }
        
        /// <summary>Gets or sets the target.</summary>
        /// <value>The target.</value>
        public GameObject Target { get => target; set => target = value; }
        
        /// <summary>Gets or sets the controller.</summary>
        /// <value>The controller.</value>
        public Enemy Controller { get => controller; set => controller = value; }

        #endregion

        #region Attack Target

        /// <summary>Attacks the now.</summary>
        public virtual void AttackNow()
        {
            Attack.Invoke(AttackType).In(Target, Controller);
        }
        #endregion
    }
}