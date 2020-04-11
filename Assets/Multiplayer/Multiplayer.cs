//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Multiplayer.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace MultiPlayer
{
    using Mirror;
    using Mirror.Discovery;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>Manage multi player game.</summary>
    [RequireComponent(typeof(NetworkManager))]
    [RequireComponent(typeof(TelepathyTransport))]
    [RequireComponent(typeof(NetworkDiscovery))]
    public class Multiplayer : MonoBehaviour
    {
        /// <summary>The offline scene</summary>
        [SerializeField]
        private string offlineScene = "Town";

        /// <summary>The runway scene</summary>
        [SerializeField]
        private string runwayScene = "Runway";

        /// <summary>The network manager</summary>
        private NetworkManager networkManager = null;

        /// <summary>The telepathy</summary>
        private TelepathyTransport telepathy = null;

        /// <summary>The discovery</summary>
        private NetworkDiscovery discovery = null;

        #region Encapsulate Fields

        /// <summary>Gets or sets the offline scene.</summary>
        /// <value>The offline scene.</value>
        public string OfflineScene { get => offlineScene; set => offlineScene = value; }

        /// <summary>Gets or sets the runway scene.</summary>
        /// <value>The runway scene.</value>
        public string RunwayScene { get => runwayScene; set => runwayScene = value; }

        /// <summary>Gets or sets the network manager.</summary>
        /// <value>The network manager.</value>
        public NetworkManager NetworkManager { get => networkManager; set => networkManager = value; }
        
        /// <summary>Gets or sets the telepathy.</summary>
        /// <value>The telepathy.</value>
        public TelepathyTransport Telepathy { get => telepathy; set => telepathy = value; }
        
        /// <summary>Gets or sets the discovery.</summary>
        /// <value>The discovery.</value>
        public NetworkDiscovery Discovery { get => discovery; set => discovery = value; }

        #endregion

        #region Unity Callbacks

        /// <summary>Starts this instance.</summary>
        public void Start()
        {
            if (!Application.isBatchMode && SceneManager.GetActiveScene().name == offlineScene)
            {
                networkManager = GetComponent<NetworkManager>();
                telepathy = GetComponent<TelepathyTransport>();
                discovery = GetComponent<NetworkDiscovery>();

                StartToHostALocalGame("Town", 1);
            }
        }

        #endregion

        /// <summary>Starts to host a local game.</summary>
        /// <param name="onlineScene">The online scene.</param>
        /// <param name="maxConnections">The maximum connections.</param>
        private void StartToHostALocalGame(string onlineScene, int maxConnections) 
        {
            networkManager.offlineScene = runwayScene;
            networkManager.onlineScene = onlineScene;
            networkManager.maxConnections = maxConnections;
            
            networkManager.StartHost();
        }
    }
}