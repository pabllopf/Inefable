//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Witch.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------

using System.Collections;
using UnityEngine;

/// <summary>Control a Witch</summary>
public class Witch : MonoBehaviour, IEnemy
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
    private const float SpeedToMove = 2f;

    /// <summary>The vision radio</summary>
    private const float VisionRange = 5f;

    /// <summary>The attack range</summary>
    private const float AttackRange = 3f;

    /// <summary>The frequency to attack</summary>
    private const float FrequencyToAttack = 7f;

    /// <summary>The frequency to attack</summary>
    private const int NumMaxSuicides = 4;

    /// <summary>The thrust</summary>
    private const float Thrust = 4f;

    /// <summary>The knock time</summary>
    private const float KnockTime = 0.2f;

    /// <summary>The target</summary>
    private Transform target = null;

    /// <summary>The health</summary>
    private int health = 75;

    /// <summary>The bullet</summary>
    [SerializeField]
    private readonly GameObject suicide = null;

    /// <summary>The direction</summary>
    private Vector3 direction = Vector3.zero;

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
        StopAll();
        if (health <= 0 && !deading)
        {
            StartCoroutine(Die());
            return;
        }

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
                    StartCoroutine(AttackToTheTarget());
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
        spriteRenderer.color = Color.red;
        PlayClip(hitClip);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
        hitting = false;
    }

    /// <summary>Dies this instance.</summary>
    /// <returns>Return none</returns>
    public IEnumerator Die()
    {
        hitting = true;
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
        MonoBehaviour.Destroy(GetComponent<Soccer>());
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
        GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    /// <summary>Attacks to the target.</summary>
    /// <returns>Return none</returns>
    private IEnumerator AttackToTheTarget()
    {
        attacking = true;
        direction = Vector3.zero;
        animator.SetBool(Walk, false);
        rigid2D.isKinematic = true;
        spriteRenderer.sortingOrder = 5;

        yield return new WaitForSeconds(FrequencyToAttack / 2);
        animator.SetTrigger(Attack);

        for (int i = 0; i < Random.Range(1, NumMaxSuicides); i++)
        {
            MonoBehaviour.Instantiate(suicide, transform.position + new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-0.6f, 0.6f), 0), Quaternion.identity);
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

    /// <summary>Stops all.</summary>
    private void StopAll()
    {
        StopAllCoroutines();
        attacking = false;
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
    }
}