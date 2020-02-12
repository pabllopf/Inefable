//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Player.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;

/// <summary>Manage the player of the game.</summary>
public class Player : NetworkBehaviour
{
    /// <summary>The run</summary>
    private const string Run = "Run";

    /// <summary>The attack</summary>
    private const string Attack = "Attack";

    /// <summary>The vertical</summary>
    private const string Vertical = "Vertical";

    /// <summary>The horizontal</summary>
    private const string Horizontal = "Horizontal";

    /// <summary>The speed to move</summary>
    private const float SpeedOfMovement = 3f;

    /// <summary>The frequency to attack</summary>
    private const float FrequencyToAttack = 0.20f;

    /// <summary>The radius attack</summary>
    private const float RadiusAttack = 0.5f;

    /// <summary>The is attacking</summary>
    private bool isAttacking = false;

    /// <summary>The position</summary>
    private Vector3 position = Vector3.zero;

    /// <summary>The direction</summary>
    private Vector3 direction = Vector3.zero;

    /// <summary>The attack vector</summary>
    private Vector3 attackVector = Vector3.zero;

    /// <summary>The main camera</summary>
    [SerializeField]
    private GameObject mainCamera = null;

    /// <summary>The Rigid body 2D</summary>
    private Rigidbody2D rigbody2D = null;

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        if (isLocalPlayer)
        {
            rigbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            GameObject obj = Instantiate(mainCamera, transform.position, Quaternion.identity);
            obj.GetComponent<MainCamera>().SetTarget(transform);
        }
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
            {
                position = rigbody2D.position;

                direction.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
                direction.Normalize();

                animator.SetFloat(Horizontal, direction.x);
                animator.SetFloat(Vertical, direction.y);
                animator.SetBool(Run, true);
            }
            else
            {
                animator.SetBool(Run, false);
            }

            if (Input.GetKeyDown(KeyCode.E) && !isAttacking)
            {
                StartCoroutine(AttackNow());
            }
        }
    }

    /// <summary>Fixed the update.</summary>
    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            rigbody2D.MovePosition(position + (direction * (SpeedOfMovement * Time.fixedDeltaTime)));
        }
    }

    /// <summary>Attacks the now.</summary>
    /// <returns>Return none</returns>
    private IEnumerator AttackNow() 
    {
        animator.SetBool(Attack, true);
        isAttacking = true;

        Physics2D.OverlapCircleAll(attackVector, RadiusAttack, LayerMask.GetMask("Enemy"))
            .ToList()
            .FindAll(i => i.CompareTag("Enemy"))
            .ForEach(i => i.GetComponent<IEnemy>().TakeDamage(5));

        yield return new WaitForSeconds(FrequencyToAttack);
        isAttacking = false;
    }
}