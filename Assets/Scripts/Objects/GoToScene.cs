//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="GoToScene.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>Load scene with a collision</summary>
public class GoToScene : MonoBehaviour
{
    /// <summary>The scene</summary>
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
