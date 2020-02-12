//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Blob.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>Control a Blob</summary>
public class Blob : MonoBehaviour, IEnemy
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
    private const float SpeedToMove = 1.5f;

    /// <summary>The vision radio</summary>
    private const float VisionRange = 5f;

    /// <summary>The attack range</summary>
    private const float AttackRange = 2f;

    /// <summary>The attack circle</summary>
    private const float AttackRadius = 0.5f;

    /// <summary>The frequency to attack</summary>
    private const float FrequencyToAttack = 1f;

    /// <summary>The thrust</summary>
    private const float Thrust = 3f;

    /// <summary>The knock time</summary>
    private const float KnockTime = 0.20f;

    /// <summary>The target</summary>
    private Transform target = null;

    /// <summary>The health</summary>
    private int health = 200;

    /// <summary>The red effect</summary>
    [SerializeField]
    private readonly GameObject redHit = null;

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
    private readonly AudioClip hitClip = null;

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
        health -= damage;
        if (health <= 0 && !deading)
        {
            StartCoroutine(Die());
            return;
        }

        redHit.GetComponent<TextMeshPro>().text = damage.ToString();
        Instantiate(redHit, transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0), Quaternion.identity, transform);


        StartCoroutine(Hit());
    }

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <summary>Updates this instance.</summary>
    public void Update()
    {
        if (health > 0)
        {
            if (DistanceToTarget() <= VisionRange)
            {
                if (!attacking)
                {
                    FollowTarget();
                }

                if (DistanceToTarget() <= AttackRange && !attacking)
                {
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + (direction / 3), AttackRadius, LayerMask.GetMask("Player"));
                    foreach (Collider2D collider in colliders)
                    {
                        if (collider.CompareTag("Player"))
                        {
                            attackPosition = transform.position + (direction / 3);
                            StartCoroutine(AttackToTheTarget());
                            return;
                        }
                    }
                }
            }
            else
            {
                direction = Vector3.zero;
                animator.SetBool(Walk, false);
            }
        }
    }

    /// <summary>Update every frame.</summary>
    public void FixedUpdate()
    {
        if (!hitting)
        {
            rigid2D.MovePosition(transform.position + (direction * SpeedToMove * Time.deltaTime));
        }
    }

    /// <summary>Hits this instance.</summary>
    /// <returns>Return none</returns>
    public IEnumerator Hit()
    {
        rigid2D.isKinematic = false;
        hitting = true;
        rigid2D.AddForce((transform.position - target.position).normalized * Thrust, ForceMode2D.Impulse);
        StartCoroutine(HitEffect(rigid2D));

        yield return new WaitForSeconds(0.1f);
        attackPosition = transform.position;
        spriteRenderer.color = Color.red;
        PlayClip(hitClip);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    /// <summary>Dies this instance.</summary>
    /// <returns>Return none</returns>
    public IEnumerator Die()
    {
        animator.SetBool(Exit, true);
        animator.SetTrigger(Dead);
        spriteRenderer.sortingOrder = 2;
        deading = true;
        spriteRenderer.color = Color.white;

        MonoBehaviour.Destroy(GetComponent<Occlusion>());
        MonoBehaviour.Destroy(GetComponent<BoxCollider2D>());
        MonoBehaviour.Destroy(GetComponent<AudioSource>());
        MonoBehaviour.Destroy(GetComponent<Rigidbody2D>());

        yield return new WaitForSeconds(3f);

        MonoBehaviour.Destroy(GetComponent<Animator>());
        MonoBehaviour.Destroy(GetComponent<Blob>());
    }

    /// <summary>Distances to target.</summary>
    /// <returns>Return the distance</returns>
    public float DistanceToTarget()
    {
        return Vector2.Distance(transform.position, target.position);
    }

    /// <summary>Follows the target.</summary>
    private void FollowTarget()
    {
        rigid2D.isKinematic = false;

        direction = target.position - transform.position;
        direction.Normalize();

        animator.SetFloat(Horizontal, direction.x);
        animator.SetFloat(Vertical, direction.y);

        animator.SetBool(Walk, true);
    }

    /// <summary>Attacks to the target.</summary>
    /// <returns>Return none</returns>
    private IEnumerator AttackToTheTarget()
    {
        attacking = true;
        direction = Vector3.zero;
        animator.SetBool(Walk, false);
        rigid2D.isKinematic = true;

        yield return new WaitForSeconds(FrequencyToAttack / 2);
        animator.SetTrigger(Attack);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPosition, AttackRadius, LayerMask.GetMask("Player"));
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                target.GetComponent<Health>().Take(4);
            }
        }

        yield return new WaitForSeconds(FrequencyToAttack / 2);

        attacking = false;
    }

    /// <summary>Hits the effect.</summary>
    /// <param name="enemy">The enemy.</param>
    /// <returns>Return none</returns>
    private IEnumerator HitEffect(Rigidbody2D enemy)
    {
        yield return new WaitForSeconds(KnockTime);
        enemy.velocity = Vector2.zero;
        enemy.isKinematic = true;
        hitting = false;
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    /// <summary>Called when [draw gizmos selected].</summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, VisionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AttackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (direction / 3), AttackRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackPosition, AttackRadius);
    }
}
