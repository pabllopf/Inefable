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
    private float speed = 0.5f;

    /// <summary>Called when [collision enter2 d].</summary>
    /// <param name="collision">The collision.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.direction = Random.Range(0, 8);
        if (this.direction != this.currentDirection)
        {
            this.currentDirection = this.direction;
        }
    }

    /// <summary>Called when [collision stay2 d].</summary>
    /// <param name="collision">The collision.</param>
    private void OnCollisionStay2D(Collision2D collision)
    {
        this.direction = Random.Range(0, 8);
        if (this.direction != this.currentDirection)
        {
            this.currentDirection = this.direction;
        }
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.currentDirection = Random.Range(0, 8);
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (this.currentDirection == 0) 
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(0, 0.2f, 0), this.speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, 0);
            this.animator.SetFloat(Vertical, 1);
        }

        if (this.currentDirection == 1)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(0, -0.2f, 0), this.speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, 0);
            this.animator.SetFloat(Vertical, -1);
        }

        if (this.currentDirection == 2)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(0.2f, 0, 0), this.speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, 1);
            this.animator.SetFloat(Vertical, 0);
        }

        if (this.currentDirection == 3)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(-0.2f, 0, 0), this.speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, -1);
            this.animator.SetFloat(Vertical, 0);
        }

        if (this.currentDirection == 4)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(-0.2f, 0.2f, 0), this.speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, -1);
            this.animator.SetFloat(Vertical, 0);
        }

        if (this.currentDirection == 5)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(0.2f, -0.2f, 0), this.speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, 1);
            this.animator.SetFloat(Vertical, 0);
        }

        if (this.currentDirection == 6)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(0.2f, 0.2f, 0), this.speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, 1);
            this.animator.SetFloat(Vertical, 0);
        }

        if (this.currentDirection == 7)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(-0.2f, -0.2f, 0), this.speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, -1);
            this.animator.SetFloat(Vertical, 0);
        }
    }
}
