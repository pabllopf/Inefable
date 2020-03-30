//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Multiplayer.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using Mirror;
using Mirror.Discovery;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>Manage multiplayer of the game.</summary>
public class Multiplayer : MonoBehaviour
{
    /// <summary>The network manager</summary>
    private NetworkManager networkManager = null;

    /// <summary>The network discovery</summary>
    private NetworkDiscovery networkDiscovery = null;

    /// <summary>The discovered servers</summary>
    private Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();

    public NetworkManager NetworkManager { get => networkManager; set => networkManager = value; }
    public NetworkDiscovery NetworkDiscovery { get => networkDiscovery; set => networkDiscovery = value; }

    public Dictionary<long, ServerResponse> DiscoveredServers => discoveredServers;

    /// <summary>Called when [discovered server].</summary>
    /// <param name="info">The information.</param>
    public void OnDiscoveredServer(ServerResponse info)
    {
        discoveredServers[info.serverId] = info;
    }

    /// <summary>Plays the single dungeon.</summary>
    public void PlaySingleDungeon()
    {
        HostLocalGame("Dungeon", 1);
    }

    /// <summary>Plays the local dungeon.</summary>
    /// <returns>Return none</returns>
    public IEnumerator PlayLocalDungeon()
    {
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();

        yield return new WaitForSeconds(1f);
        networkManager.StopHost();
        networkManager.players.Clear();

        if (discoveredServers.Values.Count <= 0)
        {
            HostLocalGame("Dungeon", 4);
        }
        else
        {
            foreach (ServerResponse info in discoveredServers.Values)
            {
                Debug.Log("Connect to: " + info.EndPoint.Address.ToString());
                ClientLocalGame("Dungeon", info);
            }
        }
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        Config();

        if (SceneManager.GetActiveScene().name == "Town") 
        {
            HostLocalGame("Town", 1);
        }
    }

    /// <summary>Configurations this instance.</summary>
    private void Config()
    {
        networkManager = GetComponent<NetworkManager>();
        networkDiscovery = GetComponent<NetworkDiscovery>();
    }

    /// <summary>Hosts the local game.</summary>
    /// <param name="nameScene">The name scene.</param>
    /// <param name="maxConnections">The maximum connections.</param>
    private void HostLocalGame(string nameScene, int maxConnections)
    {
        discoveredServers.Clear();
        networkManager.offlineScene = "Runway";
        networkManager.onlineScene = nameScene;
        networkManager.maxConnections = maxConnections;
        networkManager.StartHost();
        networkDiscovery.AdvertiseServer();
    }

    /// <summary>Clients the local game.</summary>
    /// <param name="nameScene">The name scene.</param>
    /// <param name="info">The information.</param>
    private void ClientLocalGame(string nameScene, ServerResponse info)
    {
        networkManager.maxConnections = 4;
        networkManager.offlineScene = "Runway";
        networkManager.onlineScene = nameScene;
        networkManager.StartClient(info.uri);
    }
}