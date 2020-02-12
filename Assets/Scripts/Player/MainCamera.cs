//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="MainCamera.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Follow a target in a scene.</summary>
public class MainCamera : MonoBehaviour
{
    /// <summary>The speed</summary>
    private const float Speed = 1.2f;

    /// <summary>The target</summary>
    private Transform target = null;

    /// <summary>Sets the target.</summary>
    /// <param name="target">The target.</param>
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) >= 0.5f)
        {
            transform.position = Vector3.LerpUnclamped(transform.position, new Vector2(target.position.x, target.position.y), Speed * Time.deltaTime);
        }
    }
}