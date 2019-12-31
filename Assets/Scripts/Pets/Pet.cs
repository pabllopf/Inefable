//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Pet.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Control a pet.</summary>
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Occlusion))]
public class Pet : MonoBehaviour
{
    /// <summary>The Distance</summary>
    private static readonly int Distance = 1;

    /// <summary>The Vertical</summary>
    private static readonly string Vertical = "Vertical";

    /// <summary>The Horizontal</summary>
    private static readonly string Horizontal = "Horizontal";

    /// <summary>The name</summary>
    [SerializeField]
    private string typePet = "";

    /// <summary>The owner</summary>
    private GameObject owner;

    /// <summary>The speed</summary>
    private int speed = 2;

    /// <summary>The direction</summary>
    private Vector2 direction;

    /// <summary>The animator</summary>
    private Animator animator;

    /// <summary>The box Collider2D</summary>
    private BoxCollider2D boxCollider2D;

    /// <summary>Gets the name.</summary>
    /// <returns>Return name pet</returns>
    public string GetName() 
    {
        return this.typePet;
    }

    /// <summary>Sets the speed.</summary>
    /// <param name="speed">The speed.</param>
    public void SetSpeed(int speed)
    {
        this.speed = speed;
    }

    /// <summary>Sets the owner.</summary>
    /// <param name="owner">The owner.</param>
    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    /// <summary>Leaves the owner.</summary>
    public void LeaveOwner()
    {
        this.owner = null;
        this.boxCollider2D.enabled = true;
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        this.tag = "Pet";
        this.animator = this.GetComponent<Animator>();
        this.boxCollider2D = this.GetComponent<BoxCollider2D>();
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (this.owner && this.DistanceToOwner() >= Distance) 
        {
            this.FollowOwner();
        }
    }

    /// <summary>Distances to owner.</summary>
    /// <returns>The distance of current position and owner position</returns>
    private float DistanceToOwner()
    {
        return Vector2.Distance(this.transform.position, this.owner.transform.position);
    }

    /// <summary>Follows the owner.</summary>
    private void FollowOwner() 
    {
        this.direction = this.owner.transform.position - this.transform.position;

        this.animator.SetFloat(Horizontal, this.direction.x);
        this.animator.SetFloat(Vertical, this.direction.y);

        this.transform.position = Vector3.LerpUnclamped(this.transform.position, this.owner.transform.position, this.speed * Time.deltaTime);
    }

}
