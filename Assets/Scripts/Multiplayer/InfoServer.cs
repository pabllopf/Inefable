//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="InfoServer.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using Mirror;
using UnityEngine;

/// <summary>Info of the current server.</summary>
public class InfoServer : NetworkBehaviour
{
    /// <summary>The players connected</summary>
    [SerializeField]
    [SyncVar]
    private int playersConnected;

    /// <summary>Gets or sets the players connected.</summary>
    /// <value>The players connected.</value>
    public int PlayersConnected { get => playersConnected; set => playersConnected = value; }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (isServer)
        {
            playersConnected = NetworkServer.connections.Count;
        }
    }
}
