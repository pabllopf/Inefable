//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Skeleton.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;

/// <summary>Control a Skeleton to attack melee.</summary>
public class Skeleton : MonoBehaviour, IEnemy
{
    /// <summary>The eXIT</summary>
    private const string Exit = "Exit";

    /// <summary>The Dead</summary>
    private const string Dead = "Dead";

    /// <summary>The walk</summary>
    private const string Walk = "Walk";

    /// <summary>The attack</summary>
    private const string Attack = "Attack";

    /// <summary>The vertical</summary>
    private const string Vertical = "Vertical";

    /// <summary>The horizontal</summary>
    private const string Horizontal = "Horizontal";

    /// <summary>The speed</summary>
    private const float SpeedToMove = 1f;

    /// <summary>The vision radio</summary>
    private const float VisionRange = 4f;

    /// <summary>The attack range</summary>
    private const float AttackRange = 1.5f;

    /// <summary>The attack circle</summary>
    private const float AttackRadius = 0.2f;

    /// <summary>The frequency to attack</summary>
    private const float FrequencyToAttack = 1.5f;

    /// <summary>The thrust</summary>
    private const float Thrust = 3f;

    /// <summary>The knock time</summary>
    private const float KnockTime = 0.25f;

    /// <summary>The target</summary>
    private Transform target = null;

    /// <summary>The health</summary>
    private int health = 100;

    /// <summary>The direction</summary>
    private Vector3 direction = Vector3.zero;

    /// <summary>The attack position</summary>
    private Vector3 attackPosition = Vector3.zero;

    /// <summary>The sprite renderer</summary>
    private SpriteRenderer spriteRenderer = null;

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>The rigid2 d</summary>
    private Rigidbody2D rigid2D = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>The hit clip</summary>
    [SerializeField]
    private AudioClip hitClip = null;

    /// <summary>The attacking</summary>
    private bool attacking = false;

    /// <summary>The dead</summary>
    private bool deading = false;

    /// <summary>The hitting</summary>
    private bool hitting = false;

    /// <summary>Takes the damage.</summary>
    /// <param name="damage">The damage.</param>
    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0 && !this.deading)
        {
            this.StartCoroutine(this.Die());
            return;
        }

        this.StartCoroutine(this.Hit());
    }

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.animator = this.GetComponent<Animator>();
        this.rigid2D = this.GetComponent<Rigidbody2D>();
        this.audioSource = this.GetComponent<AudioSource>();

        this.target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <summary>Updates this instance.</summary>
    public void Update()
    {
        if (this.health > 0) 
        {
            if (this.DistanceToTarget() <= VisionRange) 
            {
                if (!this.attacking)
                {
                    this.FollowTarget();
                }

                if (this.DistanceToTarget() <= AttackRange && !this.attacking)
                {
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position + (this.direction / 3), AttackRadius, LayerMask.GetMask("Player"));
                    foreach (Collider2D collider in colliders)
                    {
                        if (collider.CompareTag("Player"))
                        {
                            this.attackPosition = this.transform.position + (this.direction / 3);
                            this.StartCoroutine(this.AttackToTheTarget());
                            return;
                        }
                    }
                } 
            }
            else
            {
                this.direction = Vector3.zero;
                this.animator.SetBool(Walk, false);
            }
        }
    }

    /// <summary>Update every frame.</summary>
    public void FixedUpdate()
    {
        if (!this.hitting) 
        {
            this.rigid2D.MovePosition(this.transform.position + (this.direction * SpeedToMove * Time.deltaTime));
        }
    }

    /// <summary>Hits this instance.</summary>
    /// <returns>Return none</returns>
    public IEnumerator Hit()
    {
        this.rigid2D.isKinematic = false;
        this.hitting = true;
        this.rigid2D.AddForce((this.transform.position - this.target.position).normalized * Thrust, ForceMode2D.Impulse);
        this.StartCoroutine(this.HitEffect(this.rigid2D));
        
        yield return new WaitForSeconds(0.1f);
        this.attackPosition = this.transform.position;
        this.spriteRenderer.color = Color.red;
        this.PlayClip(this.hitClip);
        yield return new WaitForSeconds(0.1f);
        this.spriteRenderer.color = Color.white;
        this.rigid2D.isKinematic = true;
    }

    /// <summary>Dies this instance.</summary>
    /// <returns>Return none</returns>
    public IEnumerator Die()
    {
        this.animator.SetBool(Exit, true);
        this.animator.SetTrigger(Dead);
        this.spriteRenderer.sortingOrder = 2;
        this.hitting = true;
        this.deading = true;
        this.spriteRenderer.color = Color.white;

        MonoBehaviour.Destroy(this.GetComponent<Occlusion>());
        MonoBehaviour.Destroy(this.GetComponent<BoxCollider2D>());
        MonoBehaviour.Destroy(this.GetComponent<AudioSource>());
        MonoBehaviour.Destroy(this.GetComponent<Rigidbody2D>());

        yield return new WaitForSeconds(3f);

        MonoBehaviour.Destroy(this.GetComponent<Animator>());
        MonoBehaviour.Destroy(this.GetComponent<Skeleton>());
    }

    /// <summary>Distances to target.</summary>
    /// <returns>Return the distance</returns>
    public float DistanceToTarget()
    {
        return Vector2.Distance(this.transform.position, this.target.position);
    }

    /// <summary>Follows the target.</summary>
    private void FollowTarget()
    {
        this.direction = this.target.position - this.transform.position;
        this.direction.Normalize();

        this.animator.SetFloat(Horizontal, this.direction.x);
        this.animator.SetFloat(Vertical, this.direction.y);

        this.animator.SetBool(Walk, true);
        this.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    /// <summary>Attacks to the target.</summary>
    /// <returns>Return none</returns>
    private IEnumerator AttackToTheTarget()
    {
        this.attacking = true;
        this.direction = Vector3.zero;
        this.animator.SetBool(Walk, false);
        this.spriteRenderer.sortingOrder = 5;

        yield return new WaitForSeconds(FrequencyToAttack / 2);
        this.animator.SetTrigger(Attack);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.attackPosition, AttackRadius, LayerMask.GetMask("Player"));
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                this.target.GetComponent<Health>().Take(4);
            }
        }

        yield return new WaitForSeconds(FrequencyToAttack / 2);

        this.attacking = false;
    }

    /// <summary>Hits the effect.</summary>
    /// <param name="enemy">The enemy.</param>
    /// <returns>Return none</returns>
    private IEnumerator HitEffect(Rigidbody2D enemy)
    {
        yield return new WaitForSeconds(KnockTime);
        enemy.velocity = Vector2.zero;
        enemy.isKinematic = true;
        this.hitting = false;
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip)
    {
        this.audioSource.clip = clip;
        this.audioSource.Play();
    }

    /// <summary>Called when [draw gizmos selected].</summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, VisionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, AttackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + (this.direction / 3), AttackRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.attackPosition, AttackRadius);
    }
}