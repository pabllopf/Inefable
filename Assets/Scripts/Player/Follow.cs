//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Follow.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Follow a target in a scene.</summary>
public class Follow : MonoBehaviour
{
    /// <summary>The target</summary>
    [SerializeField]
    private Transform target = null;

    /// <summary>The speed</summary>
    [SerializeField]
    private float speed = 1;

    /// <summary>The target position</summary>
    private Vector3 targetPos = Vector3.zero;

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        this.target = GameObject.FindWithTag("Player").transform;
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (Vector2.Distance(this.transform.position, this.target.position) >= 0.5f) 
        {
            this.targetPos = new Vector3(this.target.position.x, this.target.position.y, 0);
            this.transform.position = Vector3.LerpUnclamped(this.transform.position, this.targetPos, this.speed * Time.deltaTime);
        }
    }
}
