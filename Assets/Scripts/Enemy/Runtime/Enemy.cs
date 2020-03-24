//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Enemy.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace EnemyIA
{
    using System.Collections;
    using EnemyIA.Configuration;
    using Mirror;
    using UnityEngine;

    /// <summary>Control a enemy of the game.</summary>
    public class Enemy : MonoBehaviour
    {
        /// <summary>The vertical</summary>
        private const string Vertical = "Vertical";

        /// <summary>The horizontal</summary>
        private const string Horizontal = "Horizontal";

        /// <summary>The walk</summary>
        private const string Walk = "Walk";

        /// <summary>The attack</summary>
        private const string Attack = "Attack";

        /// <summary>The eXIT</summary>
        private const string Exit = "Exit";

        /// <summary>The Dead</summary>
        private const string Dead = "Dead";

        /// <summary>The enemy</summary>
        [SerializeField]
        private EnemyType enemy = null;

        /// <summary>The health</summary>
        private int health = 1;

        /// <summary>The attacking</summary>
        private bool attacking = false;

        /// <summary>The is alive</summary>
        private bool isAlive = true;

        /// <summary>The target</summary>
        private Transform target = null;

        /// <summary>The direction</summary>
        private Vector3 direction = Vector3.zero;

        /// <summary>The rigid body 2d</summary>
        private Rigidbody2D rigid2D = null;

        /// <summary>The circle collider</summary>
        private CircleCollider2D circleCollider = null;

        /// <summary>The animator</summary>
        private Animator animator = null;

        #region Encapsulate Fields
        
        /// <summary>Gets or sets the type enemy.</summary>
        /// <value>The type enemy.</value>
        public EnemyType TypeEnemy { get => enemy; set => enemy = value; }

        /// <summary>Gets or sets the target.</summary>
        /// <value>The target.</value>
        public Transform Target { get => target; set => target = value; }

        /// <summary>Gets or sets the direction.</summary>
        /// <value>The direction.</value>
        public Vector3 Direction { get => direction; set => direction = value; }

        /// <summary>Gets or sets the rigid2 d.</summary>
        /// <value>The rigid2 d.</value>
        public Rigidbody2D Rigid2D { get => rigid2D; set => rigid2D = value; }

        /// <summary>Gets or sets the circle collider.</summary>
        /// <value>The circle collider.</value>
        public CircleCollider2D CircleCollider { get => circleCollider; set => circleCollider = value; }

        /// <summary>Gets or sets the animator.</summary>
        /// <value>The animator.</value>
        public Animator Animator { get => animator; set => animator = value; }

        /// <summary>Gets the distance to target.</summary>
        /// <value>The distance to target.</value>
        private float DistanceToTarget => Vector2.Distance(transform.position, target.position);

        #endregion

        /// <summary>Called when [trigger stay2 d].</summary>
        /// <param name="obj">The object.</param>
        public void OnTriggerStay2D(Collider2D obj)
        {
            if (obj.CompareTag("Player")) 
            {
                if (!target) 
                {
                    target = obj.transform;
                }
            }
        }

        /// <summary>Called when [trigger exit2 d].</summary>
        /// <param name="obj">The collision.</param>
        public void OnTriggerExit2D(Collider2D obj)
        {
            if (obj.CompareTag("Player"))
            {
                target = null;
                direction = Vector3.zero;
                animator.SetBool(Walk, false);
            }
        }

        /// <summary>Takes the damage.</summary>
        /// <param name="damage">The damage.</param>
        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0 && isAlive) 
            {
                StopAllCoroutines();
                StartCoroutine(Die());
                return;
            }
        }

        /// <summary>Starts this instance.</summary>
        private void Start()
        {
            health = enemy.Health;

            rigid2D = GetComponent<Rigidbody2D>();

            if (enemy.IsStaticEnemy)
            {
                rigid2D.isKinematic = true;
            }


            circleCollider = GetComponent<CircleCollider2D>();
            circleCollider.isTrigger = true;
            circleCollider.radius = enemy.RangeOfVision;

            animator = GetComponent<Animator>();
        }

        /// <summary>Updates this instance.</summary>
        private void Update()
        {
            if (isAlive)
            {
                if (!target)
                {
                    return;
                }

                if (DistanceToTarget <= enemy.RangeOfVision)
                {
                    if (DistanceToTarget <= enemy.RangeOfAttack)
                    {
                        if (!attacking)
                        {
                            StartCoroutine(AttackTarget());
                        }
                    }
                    else
                    {
                        FollowTarget();
                    }
                }
            }
        }

        /// <summary>Fixed the update.</summary>
        private void FixedUpdate()
        {
            if (isAlive && !enemy.IsStaticEnemy) 
            {
                rigid2D.MovePosition(transform.position + (direction * enemy.SpeedToMove * Time.deltaTime));
            }
        }

        /// <summary>Follows the target.</summary>
        private void FollowTarget() 
        {
            direction = target.position - transform.position;
            direction.Normalize();

            animator.SetFloat(Horizontal, direction.x);
            animator.SetFloat(Vertical, direction.y);

            if (!enemy.IsStaticEnemy) 
            {
                animator.SetBool(Walk, true);
            }
        }

        /// <summary>Attacks the target.</summary>
        /// <returns>Return none</returns>
        private IEnumerator AttackTarget() 
        {
            attacking = true;
            direction = Vector2.zero;
            animator.SetBool(Walk, false);

            yield return new WaitForSeconds(enemy.FrequencyToAttack / 2);
            animator.SetTrigger(Attack);

            enemy.Target = target.gameObject;
            enemy.Controller = this;
            enemy.AttackNow();

            yield return new WaitForSeconds(enemy.FrequencyToAttack / 2);
            attacking = false;
        }

        /// <summary>Dies this instance.</summary>
        /// <returns>Return none</returns>
        private IEnumerator Die() 
        {
            isAlive = false;
            animator.SetBool(Exit, true);
            animator.SetTrigger(Dead);

            Destroy(GetComponent<CircleCollider2D>());
            Destroy(GetComponent<CapsuleCollider2D>());

            yield return new WaitForSeconds(1f);

            GameObject obj = new GameObject(enemy.NameEnemy);
            obj.transform.position = this.transform.position;
            obj.AddComponent<SpriteRenderer>();
            obj.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
            obj.AddComponent<NetworkIdentity>();

            yield return new WaitForSeconds(1f);

            Destroy(gameObject);
        }

        #region Gizmos Selected

        /// <summary>Called when [draw gizmos selected].</summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, enemy.RangeOfVision);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, enemy.RangeOfAttack);
        }

        #endregion
    }
}