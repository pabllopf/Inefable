//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Pet.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Control a pet.</summary>
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
    private readonly string nameOfPet = string.Empty;

    /// <summary>The owner</summary>
    private GameObject owner;

    /// <summary>The speed</summary>
    private int speed = 2;

    /// <summary>The direction</summary>
    private Vector2 direction;

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    public string Name => nameOfPet;

    /// <summary>Gets the animator.</summary>
    /// <value>The animator.</value>
    private Animator Animator => GetComponent<Animator>();

    /// <summary>Gets the box collider2 d.</summary>
    /// <value>The box collider2 d.</value>
    private BoxCollider2D BoxCollider2D => GetComponent<BoxCollider2D>();

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
        BoxCollider2D.enabled = false;
    }

    /// <summary>Leaves the owner.</summary>
    public void LeaveOwner()
    {
        owner = null;
        BoxCollider2D.enabled = true;
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        tag = "Pet";
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (owner && DistanceToOwner() >= Distance)
        {
            FollowOwner();
        }
    }

    /// <summary>Distances to owner.</summary>
    /// <returns>The distance of current position and owner position</returns>
    private float DistanceToOwner()
    {
        return Vector2.Distance(transform.position, owner.transform.position);
    }

    /// <summary>Follows the owner.</summary>
    private void FollowOwner()
    {
        direction = owner.transform.position - transform.position;

        Animator.SetFloat(Horizontal, direction.x);
        Animator.SetFloat(Vertical, direction.y);

        transform.position = Vector3.LerpUnclamped(transform.position, owner.transform.position, speed * Time.deltaTime);
    }
}