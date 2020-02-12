//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="TotemBuff.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>Control a TotemBuff</summary>
public class TotemBuff : MonoBehaviour, IEnemy
{
    /// <summary>The restart</summary>
    private const string Restart = "Restart";

    /// <summary>The eXIT</summary>
    private const string Exit = "Exit";

    /// <summary>The Dead</summary>
    private const string Dead = "Dead";

    /// <summary>The attack</summary>
    private const string Attack = "Attack";

    /// <summary>The vertical</summary>
    private const string Vertical = "Vertical";

    /// <summary>The horizontal</summary>
    private const string Horizontal = "Horizontal";

    /// <summary>The vision radio</summary>
    private const float VisionRange = 8f;

    /// <summary>The attack range</summary>
    private const float AttackRange = 4f;

    /// <summary>The frequency to attack</summary>
    private const float FrequencyToAttack = 0.2f;

    /// <summary>The target</summary>
    private Transform target = null;

    /// <summary>The bullet</summary>
    [SerializeField]
    private readonly GameObject bullet = null;

    /// <summary>The health</summary>
    private int health = 75;

    /// <summary>The red effect</summary>
    [SerializeField]
    private readonly GameObject redHit = null;


    /// <summary>The direction</summary>
    private Vector3 direction = Vector3.zero;

    /// <summary>The sprite renderer</summary>
    private SpriteRenderer spriteRenderer = null;

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>The hit clip</summary>
    [SerializeField]
    private readonly AudioClip hitClip = null;

    /// <summary>The attacking</summary>
    private bool attacking = false;

    /// <summary>The dead</summary>
    private bool deading = false;

    /// <summary>Takes the damage.</summary>
    /// <param name="damage">The damage.</param>
    public void TakeDamage(int damage)
    {
        StopAllCoroutines();
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
        audioSource = GetComponent<AudioSource>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <summary>Called when [enable].</summary>
    public void OnEnable()
    {
        GetComponent<Animator>().SetTrigger(Restart);
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
        }
    }

    /// <summary>Hits this instance.</summary>
    /// <returns>Return none</returns>
    public IEnumerator Hit()
    {
        yield return new WaitForSeconds(0.1f);
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
        deading = true;
        spriteRenderer.color = Color.white;

        MonoBehaviour.Destroy(GetComponent<Occlusion>());
        MonoBehaviour.Destroy(GetComponent<BoxCollider2D>());
        MonoBehaviour.Destroy(GetComponent<AudioSource>());

        yield return new WaitForSeconds(3f);

        MonoBehaviour.Destroy(GetComponent<Animator>());
        MonoBehaviour.Destroy(GetComponent<TotemBuff>());
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
        direction = target.position - transform.position;
        direction.Normalize();

        animator.SetFloat(Horizontal, direction.x);
        animator.SetFloat(Vertical, direction.y);
    }

    /// <summary>Attacks to the target.</summary>
    /// <returns>Return none</returns>
    private IEnumerator AttackToTheTarget()
    {
        attacking = true;
        direction = Vector3.zero;

        yield return new WaitForSeconds(FrequencyToAttack / 2);
        animator.SetTrigger(Attack);

        GameObject bulletSpawned = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletSpawned.GetComponent<Bullet>().SetTarget(target.position);

        yield return new WaitForSeconds(FrequencyToAttack / 2);

        attacking = false;
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
