//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Teleport.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>Teleport player to a scene with a collision.</summary>
public class Teleport : MonoBehaviour
{
    /// <summary>The scene</summary>
    [SerializeField]
    private string scene = string.Empty;

    /// <summary>Called when [collision enter].</summary>
    /// <param name="collision2D">The collision</param>
    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.collider.CompareTag("Player")) 
        {
            SceneManager.LoadScene(this.scene, LoadSceneMode.Single);
        }
    }
}
