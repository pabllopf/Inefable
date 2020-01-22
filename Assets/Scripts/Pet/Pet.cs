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
    /// <summary>The Vertical</summary>
    private static readonly string Vertical = "Vertical";

    /// <summary>The Horizontal</summary>
    private static readonly string Horizontal = "Horizontal";

    /// <summary>The Distance</summary>
    private static readonly int Distance = 1;

    /// <summary>The name</summary>
    [SerializeField]
    private string nameOfPet = string.Empty;
   
    /// <summary>The owner</summary>
    private GameObject owner;

    /// <summary>The speed</summary>
    private int speed = 2;

    /// <summary>The direction</summary>
    private Vector2 direction;

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    public string Name => this.nameOfPet;

    /// <summary>Gets the animator.</summary>
    /// <value>The animator.</value>
    private Animator Animator => this.GetComponent<Animator>();

    /// <summary>Gets the box collider2 d.</summary>
    /// <value>The box collider2 d.</value>
    private BoxCollider2D BoxCollider2D => this.GetComponent<BoxCollider2D>();

    /// <summary>Sets the speed.</summary>
    /// <param name="speed">The speed.</param>
    public void SetSpeed(int speed) => this.speed = speed;

    /// <summary>Sets the owner.</summary>
    /// <param name="owner">The owner.</param>
    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
        this.BoxCollider2D.enabled = false;
    }

    /// <summary>Leaves the owner.</summary>
    public void LeaveOwner()
    {
        this.owner = null;
        this.BoxCollider2D.enabled = true;
    }

    /// <summary>Starts this instance.</summary>
    private void Start() => this.tag = "Pet";

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

        this.Animator.SetFloat(Horizontal, this.direction.x);
        this.Animator.SetFloat(Vertical, this.direction.y);

        this.transform.position = Vector3.LerpUnclamped(this.transform.position, this.owner.transform.position, this.speed * Time.deltaTime);
    }
}