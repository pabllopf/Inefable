//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="PotionBlue.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>A potion blue in the game.</summary>
public class PotionBlue : MonoBehaviour, IItem
{
    /// <summary>The icon</summary>
    [SerializeField]
    private Sprite icon = null;

    /// <summary>Called when [trigger enter2 d].</summary>
    /// <param name="obj">The object.</param>
    public void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            Action(obj.gameObject);
        }
    }

    /// <summary>Actions this instance.</summary>
    /// <param name="obj">The object.</param>
    public void Action(GameObject obj)
    {
        obj.GetComponent<Shield>().SetFull();
        Command.CmdDestroy(gameObject);
    }
}