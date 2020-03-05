//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Boss.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Manage a boss of the dungeon.</summary>
public class Boss : MonoBehaviour
{
    /// <summary>The type boss</summary>
    [SerializeField]
    private BossType typeBoss = null;

    #region Encapsulate Fields
    
    /// <summary>Gets or sets the type boss.</summary>
    /// <value>The type boss.</value>
    public BossType TypeBoss { get => typeBoss; set => typeBoss = value; }
    
    #endregion
}