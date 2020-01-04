//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Suicide.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Occlusion))]

/// <summary>Control a Suicide enemy</summary>
public class Suicide : MonoBehaviour, IEnemy
{
    /// <summary>The attack</summary>
    private const string Attack = "Attack";

    /// <summary>The vertical</summary>
    private const string Vertical = "Vertical";

    /// <summary>The horizontal</summary>
    private const string Horizontal = "Horizontal";

    /// <summary>The speed</summary>
    private const float Speed = 1f;

    /// <summary>The vision radio</summary>
    private const float VisionRadio = 5f;

    /// <summary>The attack radio</summary>
    private const float AttackRadio = 1f;

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
        this.target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <summary>Updates this instance.</summary>
    public void Update()
    {
        if (this.DistanceToTarget() > VisionRadio)
        {
            this.direction = Vector3.zero;
            return; 
        }

        if (this.DistanceToTarget() <= AttackRadio)
        { 
            this.StartCoroutine(this.AttackToTarget()); 
            return; 
        }

        this.FollowTarget();
    }

    /// <summary>Update every frame.</summary>
    public void FixedUpdate()
    {
        this.rigid2D.MovePosition(this.transform.position + (this.direction * Speed * Time.deltaTime));
    }

    /// <summary>Distances to target.</summary>
    /// <returns>Return the distance to the target</returns>
    public float DistanceToTarget()
    {
        return Vector2.Distance(this.transform.position, this.target.position);
    }

    /// <summary>Follows the target.</summary>
    public void FollowTarget()
    {
        this.direction = this.target.position - this.transform.position;
        this.direction.Normalize();

        this.animator.SetFloat(Horizontal, this.direction.x);
        this.animator.SetFloat(Vertical, this.direction.y);
    }

    /// <summary>Takes the damage.</summary>
    /// <param name="amount">Amount to take health</param>
    public void TakeDamage(int amount)
    {
        MonoBehaviour.Destroy(this.gameObject);
    }

    /// <summary>Dies this instance.</summary>
    /// <returns>Return none</returns>
    public IEnumerator Die() 
    {
        return null;
    }

    /// <summary>Attack to target.</summary>
    /// <returns>Return none</returns>
    private IEnumerator AttackToTarget()
    {
        this.animator.SetBool(Attack, true);
        this.direction = Vector3.zero;
        
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        
        if (this.DistanceToTarget() <= AttackRadio) 
        { 
            this.target.GetComponent<Health>().Take(1); 
        }

        MonoBehaviour.Destroy(this.gameObject);
    }
}