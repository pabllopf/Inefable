//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Fox.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Manage the fox NPC.</summary>
public class Fox : MonoBehaviour
{
    /// <summary>The Vertical</summary>
    private static readonly string Vertical = "Vertical";

    /// <summary>The Horizontal</summary>
    private static readonly string Horizontal = "Horizontal";

    /// <summary>The animator</summary>
    private Animator animator;

    /// <summary>The direction</summary>
    private int direction;

    /// <summary>The current direction</summary>
    private int currentDirection;

    /// <summary>The speed</summary>
    private readonly float speed = 0.5f;

    /// <summary>Called when [collision enter2 d].</summary>
    /// <param name="collision">The collision.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = Random.Range(0, 8);
        if (direction != currentDirection)
        {
            currentDirection = direction;
        }
    }

    /// <summary>Called when [collision stay2 d].</summary>
    /// <param name="collision">The collision.</param>
    private void OnCollisionStay2D(Collision2D collision)
    {
        direction = Random.Range(0, 8);
        if (direction != currentDirection)
        {
            currentDirection = direction;
        }
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        animator = GetComponent<Animator>();
        currentDirection = Random.Range(0, 8);
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (currentDirection == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0.2f, 0), speed * Time.deltaTime);
            animator.SetFloat(Horizontal, 0);
            animator.SetFloat(Vertical, 1);
        }

        if (currentDirection == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, -0.2f, 0), speed * Time.deltaTime);
            animator.SetFloat(Horizontal, 0);
            animator.SetFloat(Vertical, -1);
        }

        if (currentDirection == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0.2f, 0, 0), speed * Time.deltaTime);
            animator.SetFloat(Horizontal, 1);
            animator.SetFloat(Vertical, 0);
        }

        if (currentDirection == 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-0.2f, 0, 0), speed * Time.deltaTime);
            animator.SetFloat(Horizontal, -1);
            animator.SetFloat(Vertical, 0);
        }

        if (currentDirection == 4)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-0.2f, 0.2f, 0), speed * Time.deltaTime);
            animator.SetFloat(Horizontal, -1);
            animator.SetFloat(Vertical, 0);
        }

        if (currentDirection == 5)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0.2f, -0.2f, 0), speed * Time.deltaTime);
            animator.SetFloat(Horizontal, 1);
            animator.SetFloat(Vertical, 0);
        }

        if (currentDirection == 6)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0.2f, 0.2f, 0), speed * Time.deltaTime);
            animator.SetFloat(Horizontal, 1);
            animator.SetFloat(Vertical, 0);
        }

        if (currentDirection == 7)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-0.2f, -0.2f, 0), speed * Time.deltaTime);
            animator.SetFloat(Horizontal, -1);
            animator.SetFloat(Vertical, 0);
        }
    }
}
