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

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        if (GameObject.FindWithTag("Player").gameObject != null) 
        {
            this.target = GameObject.FindWithTag("Player").transform;
        }
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (Vector2.Distance(this.transform.position, this.target.position) >= 0.5f) 
        {
            this.transform.position = Vector3.LerpUnclamped(this.transform.position, new Vector2(this.target.position.x, this.target.position.y), Speed * Time.deltaTime);
        }
    }
}