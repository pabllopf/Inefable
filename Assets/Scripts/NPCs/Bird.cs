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
    private float speed = 2;

    /// <summary>The reset time</summary>
    private float resetTime;

    /// <summary>The direction</summary>
    private int direction;

    /// <summary>The last direction</summary>
    private int lastDirection;

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.resetTime = this.timeToChangeDirection;
        this.direction = Random.Range(0, 2);
        this.lastDirection = this.direction;
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (this.timeToChangeDirection > 0) 
        {
            this.timeToChangeDirection -= Time.deltaTime;
        }

        this.direction = Random.Range(0, 2);

        if (this.timeToChangeDirection <= 0 && this.lastDirection != this.direction) 
        {
            this.lastDirection = this.direction;
            this.timeToChangeDirection = this.resetTime;
        }

        if (this.lastDirection == 0)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(0, 0.2f, 0), this.speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, 0);
            this.animator.SetFloat(Vertical, 1);
        }

        if (this.lastDirection == 1)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(0, -0.2f, 0), this.speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, 0);
            this.animator.SetFloat(Vertical, -1);
        }
    }
}
