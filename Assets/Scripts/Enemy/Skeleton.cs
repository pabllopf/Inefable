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
public class Skeleton : MonoBehaviour
{
    /// <summary>The die</summary>
    private const string Die = "Die";

    /// <summary>The walk</summary>
    private const string Walk = "Walk";

    /// <summary>The attack</summary>
    private const string Attack = "Attack";

    /// <summary>The vertical</summary>
    private const string Vertical = "Vertical";

    /// <summary>The horizontal</summary>
    private const string Horizontal = "Horizontal";

    /// <summary>The hit</summary>
    private static readonly float FrecuencyHit = 1f;

    /// <summary>The speed</summary>
    private static readonly float Speed = 2.5f;

    /// <summary>The vision radio</summary>
    private static readonly float VisionRadio = 5f;

    /// <summary>The attack radio</summary>
    private static readonly float AttackRadio = 1f;

    /// <summary>The hit radio</summary>
    private static readonly float HitRadio = 0.8f;

    /// <summary>The health</summary>
    private int health = 100;

    /// <summary>The attacking</summary>
    private bool attacking = false;

    /// <summary>The target</summary>
    private Transform target = null;

    /// <summary>The direction</summary>
    private Vector3 direction = Vector3.zero;

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>The rigid2 d</summary>
    private Rigidbody2D rigid2D = null;

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.rigid2D = this.GetComponent<Rigidbody2D>();

        this.rigid2D.isKinematic = false;
        this.rigid2D.simulated = true;

        this.target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <summary>Updates this instance.</summary>
    public void Update()
    {
        if (this.DistanceToTarget() > VisionRadio) 
        {
            this.HasNotTarget(); 
            return; 
        }

        if (this.DistanceToTarget() <= AttackRadio)
        {
            this.AttactToTarget(); 
            return; 
        }

        this.FollowTarget(); 
    }

    /// <summary>Update every frame.</summary>
    public void FixedUpdate()
    {
        this.rigid2D.MovePosition((this.transform.position + this.direction) * Speed * Time.deltaTime);
    }

    /// <summary>Takes the damage.</summary>
    /// <param name="damage">The damage.</param>
    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0) 
        {
            this.animator.SetTrigger(Die);
            MonoBehaviour.Destroy(this.gameObject);
        }
    }

    /// <summary>Distances to target.</summary>
    /// <returns>Return the distance</returns>
    private float DistanceToTarget()
    {
        return Vector2.Distance(this.transform.position, this.target.position);
    }

    /// <summary>Determines whether [has not target].</summary>
    private void HasNotTarget()
    {
        this.direction = Vector3.zero;
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
    }

    /// <summary>Attack to target.</summary>
    private void AttactToTarget()
    {
        if ((this.DistanceToTarget() <= AttackRadio + HitRadio) && !this.attacking)
        {
            this.StartCoroutine(this.HitTarget());
        }
    }

    /// <summary>Hits the target.</summary>
    /// <returns>Return none</returns>
    private IEnumerator HitTarget() 
    {
        this.target.GetComponent<Health>().Take(4);
        this.rigid2D.isKinematic = true;
        this.direction = Vector3.zero;
        
        this.animator.SetBool(Walk, false);
        this.animator.SetTrigger(Attack);
        this.attacking = true;

        yield return new WaitForSeconds(FrecuencyHit);

        this.attacking = false;
    }
}
