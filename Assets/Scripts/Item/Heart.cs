//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Heart.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using Mirror;
using UnityEngine;

/// <summary>A heart in the game.</summary>
public class Heart : MonoBehaviour, IItem
{
    /// <summary>Called when [trigger enter2 d].</summary>
    /// <param name="obj">The object.</param>
    public void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            Action(obj.gameObject);
        }
    }

    /// <summary>Actions the specified object.</summary>
    /// <param name="obj">The object.</param>
    public void Action(GameObject obj)
    {
        obj.GetComponent<Health>().Treat(Random.Range(3, 10));
        NetworkServer.Destroy(gameObject);
    }

    public string GetName()
    {
        throw new System.NotImplementedException();
    }

    public Sprite GetIcon()
    {
        throw new System.NotImplementedException();
    }
}