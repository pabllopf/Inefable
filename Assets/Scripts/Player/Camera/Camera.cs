//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Camera.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Control the main camera in the scene.</summary>
public class Camera : MonoBehaviour
{
    /// <summary>Speed of movement</summary>
    private const float Speed = 1.2f;

    /// <summary>Distance to stop</summary>
    private const float DistanceToStop = 0.5f;

    /// <summary>Target to follow</summary>
    private Transform target = null;

    /// <summary>Gets or sets the target.</summary>
    /// <value>The target.</value>
    public Transform Target
    {
        get => target;
        set => target = value;
    }

    /// <summary>Sets the current position.</summary>
    /// <value>The current position.</value>
    private Vector3 CurrentPosition
    {
        set => transform.position = value;
    }

    /// <summary>Gets the distance to target.</summary>
    /// <value>The distance to target.</value>
    private float DistanceToTarget => Vector2.Distance(transform.position, target.position);

    /// <summary>Gets the follow target.</summary>
    /// <value>The follow target.</value>
    private Vector2 FollowTarget => Vector2.LerpUnclamped(transform.position, new Vector2(target.position.x, target.position.y), Speed * Time.deltaTime);

    /// <summary>Gets the be quiet.</summary>
    /// <value>The be quiet.</value>
    private Vector2 BeQuiet => transform.position;

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        CurrentPosition = (DistanceToTarget >= DistanceToStop) ? FollowTarget : BeQuiet;
    }
}