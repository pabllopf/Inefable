//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Command.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using Mirror;
using UnityEngine;

/// <summary>Destroy network objects.</summary>
public class Command : MonoBehaviour
{
    /// <summary>Commands the destroy.</summary>
    /// <param name="obj">The object.</param>
    [Command]
    public static void CmdDestroy(GameObject obj)
    {
        NetworkServer.Destroy(obj);
    }

    /// <summary>Commands the instantiate.</summary>
    /// <param name="obj">The object.</param>
    /// <param name="transform">The transform.</param>
    [Command]
    public static void CmdInstantiate(GameObject obj, Vector2 pos) 
    {
        var objSpawned = Instantiate(obj, pos, Quaternion.identity);
        NetworkServer.Spawn(objSpawned);
    }
}