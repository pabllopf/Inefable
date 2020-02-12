//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="IItem.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Interface to define a item.</summary>
public interface IItem
{
    /// <summary>Called when [trigger enter2 d].</summary>
    /// <param name="obj">The object.</param>
    void OnTriggerEnter2D(Collider2D obj);

    /// <summary>Actions the specified object.</summary>
    /// <param name="obj">The object.</param>
    void Action(GameObject obj);
}