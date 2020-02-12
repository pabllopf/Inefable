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
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <summary>Updates this instance.</summary>
    public void Update()
    {
        if (DistanceToTarget() > VisionRadio)
        {
            direction = Vector3.zero;
            return;
        }

        if (DistanceToTarget() <= AttackRadio)
        {
            StartCoroutine(AttackToTarget());
            return;
        }

        FollowTarget();
    }

    /// <summary>Update every frame.</summary>
    public void FixedUpdate()
    {
        rigid2D.MovePosition(transform.position + (direction * Speed * Time.deltaTime));
    }

    /// <summary>Distances to target.</summary>
    /// <returns>Return the distance to the target</returns>
    public float DistanceToTarget()
    {
        return Vector2.Distance(transform.position, target.position);
    }

    /// <summary>Follows the target.</summary>
    public void FollowTarget()
    {
        direction = target.position - transform.position;
        direction.Normalize();

        animator.SetFloat(Horizontal, direction.x);
        animator.SetFloat(Vertical, direction.y);
    }

    /// <summary>Takes the damage.</summary>
    /// <param name="amount">Amount to take health</param>
    public void TakeDamage(int amount)
    {
        MonoBehaviour.Destroy(gameObject);
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
        animator.SetBool(Attack, true);
        direction = Vector3.zero;

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        if (DistanceToTarget() <= AttackRadio)
        {
            target.GetComponent<Health>().Take(1);
        }

        MonoBehaviour.Destroy(gameObject);
    }
}