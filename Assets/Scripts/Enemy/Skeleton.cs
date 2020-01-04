//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Skeleton.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Occlusion))]
[RequireComponent(typeof(Animator))]

/// <summary>Control a Skeleton to attack melee.</summary>
public class Skeleton : MonoBehaviour, IEnemy
{
    /// <summary>The die</summary>
    private const string Dead = "Dead";

    /// <summary>The walk</summary>
    private const string Walk = "Walk";

    /// <summary>The attack</summary>
    private const string Attack = "Attack";

    /// <summary>The vertical</summary>
    private const string Vertical = "Vertical";

    /// <summary>The horizontal</summary>
    private const string Horizontal = "Horizontal";

    /// <summary>The hit</summary>
    private const float FrecuencyToHit = 1.5f;

    /// <summary>The speed</summary>
    private const float SpeedToMove = 1f;

    /// <summary>The vision radio</summary>
    private const float VisionRadio = 4f;

    /// <summary>The attack radio</summary>
    private const float AttackRadio = 0.7f;

    /// <summary>The health</summary>
    private int health = 100;

    /// <summary>The attacking</summary>
    private bool attacking = false;

    /// <summary>The dead</summary>
    private bool deading = false;

    /// <summary>The target</summary>
    private Transform target = null;

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
    private AudioClip hitClip = null;

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
            if (this.DistanceToTarget() > VisionRadio)
            {
                this.direction = Vector3.zero;
                this.animator.SetBool(Walk, false);
                return;
            }

            if (this.DistanceToTarget() <= AttackRadio && !this.attacking)
            {
                this.StartCoroutine(this.HitTarget());
                return;
            }

            if (!this.attacking)
            {
                this.FollowTarget();
            }
        }
    }

    /// <summary>Update every frame.</summary>
    public void FixedUpdate()
    {
        this.rigid2D.MovePosition(this.transform.position + (this.direction * SpeedToMove * Time.deltaTime));
    }

    /// <summary>Takes the damage.</summary>
    /// <param name="damage">The damage.</param>
    public void TakeDamage(int damage)
    {
        this.health -= damage;
        this.StartCoroutine(this.Hit());
        if (this.health <= 0 && !this.deading) 
        {
            this.StartCoroutine(this.Die());
        }
    }

    /// <summary>Hits this instance.</summary>
    /// <returns>Return none</returns>
    public IEnumerator Hit() 
    {
        yield return new WaitForSeconds(0.2f);
        this.spriteRenderer.color = Color.red;
        this.PlayClip(this.hitClip);
        yield return new WaitForSeconds(0.1f);
        this.spriteRenderer.color = Color.white;
    }

    /// <summary>Dies this instance.</summary>
    /// <returns>Return none</returns>
    public IEnumerator Die()
    {
        this.animator.SetTrigger(Dead);
        this.GetComponent<SpriteRenderer>().sortingOrder = 2;
        this.deading = true;

        yield return new WaitForSeconds(0.8f);

        MonoBehaviour.Destroy(this.GetComponent<Skeleton>());
        MonoBehaviour.Destroy(this.GetComponent<Occlusion>());
        MonoBehaviour.Destroy(this.GetComponent<Animator>());
        MonoBehaviour.Destroy(this.GetComponent<BoxCollider2D>());
        MonoBehaviour.Destroy(this.GetComponent<Rigidbody2D>());
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
        this.rigid2D.isKinematic = false;

        this.direction = this.target.position - this.transform.position;
        this.direction.Normalize();

        this.animator.SetFloat(Horizontal, this.direction.x);
        this.animator.SetFloat(Vertical, this.direction.y);

        this.animator.SetBool(Walk, true);
        this.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    /// <summary>Hits the target.</summary>
    /// <returns>Return none</returns>
    private IEnumerator HitTarget() 
    {
        this.attacking = true;

        this.target.GetComponent<Health>().Take(4);
        this.direction = Vector3.zero;
        this.animator.SetBool(Walk, false);
        this.animator.SetTrigger(Attack);
        this.rigid2D.isKinematic = true;
        this.GetComponent<SpriteRenderer>().sortingOrder = 5;

        yield return new WaitForSeconds(FrecuencyToHit);

        this.attacking = false;
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
        Gizmos.DrawWireSphere(this.transform.position, VisionRadio);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, AttackRadio);
    }
}
