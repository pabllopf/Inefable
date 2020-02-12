//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Bird.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Simple bird</summary>
public class Bird : MonoBehaviour
{
    /// <summary>The Vertical</summary>
    private static readonly string Vertical = "Vertical";

    /// <summary>The Horizontal</summary>
    private static readonly string Horizontal = "Horizontal";

    /// <summary>The animator</summary>
    private Animator animator;

    /// <summary>The time to change direction</summary>
    [SerializeField]
    private float timeToChangeDirection = 1f;

    /// <summary>The speed</summary>
    [SerializeField]
    private readonly float speed = 2;

    /// <summary>The reset time</summary>
    private float resetTime;

    /// <summary>The direction</summary>
    private int direction;

    /// <summary>The last direction</summary>
    private int lastDirection;

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        animator = GetComponent<Animator>();
        resetTime = timeToChangeDirection;
        direction = Random.Range(0, 2);
        lastDirection = direction;
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (timeToChangeDirection > 0)
        {
            timeToChangeDirection -= Time.deltaTime;
        }

        direction = Random.Range(0, 2);

        if (timeToChangeDirection <= 0 && lastDirection != direction)
        {
            lastDirection = direction;
            timeToChangeDirection = resetTime;
        }

        if (lastDirection == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0.2f, 0), speed * Time.deltaTime);
            animator.SetFloat(Horizontal, 0);
            animator.SetFloat(Vertical, 1);
        }

        if (lastDirection == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, -0.2f, 0), speed * Time.deltaTime);
            animator.SetFloat(Horizontal, 0);
            animator.SetFloat(Vertical, -1);
        }
    }
}
