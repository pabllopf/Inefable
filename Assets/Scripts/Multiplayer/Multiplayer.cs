//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Multiplayer.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace MultiPlayer
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using Mirror;
    using Mirror.Discovery;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>Manage multi player game.</summary>
    public class Multiplayer : MonoBehaviour
    {
        /// <summary>The offline scene</summary>
        [SerializeField]
        private string offlineScene = "Town";

        /// <summary>The runway scene</summary>
        [SerializeField]
        private string runwayScene = "Runway";

        /// <summary>My ip</summary>
        private string myIP = null;

        /// <summary>The network manager</summary>
        private NetworkManager networkManager = null;

        /// <summary>The telepathy</summary>
        private TelepathyTransport telepathy = null;

        /// <summary>The discovery</summary>
        private NetworkDiscovery discovery = null;

        /// <summary>The discovered servers</summary>
        private Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();

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

        /// <summary>Gets or sets the discovered servers.</summary>
        /// <value>The discovered servers.</value>
        public Dictionary<long, ServerResponse> DiscoveredServers { get => discoveredServers; set => discoveredServers = value; }
        
        /// <summary>Gets or sets my ip.</summary>
        /// <value>My ip.</value>
        public string MyIP { get => myIP; set => myIP = value; }

        #endregion

        /// <summary>Plays the dungeon local in single mode.</summary>
        public void PlayLocalInSingleMode()
        {
            StartCoroutine(LocalGame("Dungeon", 4));
        }

        /// <summary>Plays the local mode.</summary>
        public void PlayLocalMode()
        {
            StartCoroutine(LocalGame("Dungeon", 4));
        }

        /// <summary>Plays the server mode.</summary>
        public void PlayServerMode()
        {
            StartCoroutine(MultiMode());
        }

        /// <summary>Called when [discovered server].</summary>
        /// <param name="info">The information.</param>
        public void OnDiscoveredServer(ServerResponse info)
        {
            discoveredServers[info.serverId] = info;
        }

        /// <summary>Goes to main menu.</summary>
        public void GoToMainMenu() 
        {
            StartCoroutine(Disconnect());
        }

        /// <summary>Starts this instance.</summary>
        private void Start()
        {
            if (!Application.isBatchMode && SceneManager.GetActiveScene().name == offlineScene)
            {
                Config();
                discovery.OnServerFound.AddListener((ServerResponse response) => OnDiscoveredServer(response));

                //PlayServerMode();
                StartToHostALocalGame("Town", 1);
            }
        }

        private void Config() 
        {
            networkManager = GameObject.FindWithTag("Network").GetComponent<NetworkManager>();
            telepathy = GameObject.FindWithTag("Network").GetComponent<TelepathyTransport>();
            discovery = GameObject.FindWithTag("Network").GetComponent<NetworkDiscovery>();
        }

        /// <summary>Starts to host a local game.</summary>
        /// <param name="onlineScene">The online scene.</param>
        /// <param name="maxConnections">The maximum connections.</param>
        private void StartToHostALocalGame(string onlineScene, int maxConnections)
        {
            string port = "7779";
            foreach (ushort portt in networkManager.ports)
            {
                if (portt.ToString() == port)
                {
                    GetComponent<TelepathyTransport>().port = portt;
                }
            }

            discoveredServers.Clear();

            networkManager.offlineScene = runwayScene;
            networkManager.onlineScene = onlineScene;
            networkManager.maxConnections = maxConnections;

            networkManager.StartHost();

            discovery.AdvertiseServer();
        }

        /// <summary>Starts the local client.</summary>
        /// <param name="onlineScene">The name scene.</param>
        /// <param name="info">The information.</param>
        private void StartLocalClient(string onlineScene, ServerResponse info)
        {
            string port = "7779";
            foreach (ushort portt in networkManager.ports)
            {
                if (portt.ToString() == port)
                {
                    GetComponent<TelepathyTransport>().port = portt;
                }
            }

            networkManager.offlineScene = runwayScene;
            networkManager.onlineScene = onlineScene;
            networkManager.maxConnections = 4;

            networkManager.StartClient(info.uri);
        }

        /// <summary>Locals the game finder.</summary>
        /// <returns>Return none</returns>
        private IEnumerator LocalGame(string onlineScene, int maxConnections)
        {
            discoveredServers.Clear();
            discovery.StartDiscovery();

            yield return new WaitForSeconds(1f);

            networkManager.StopHost();
            networkManager.players.Clear();

            yield return new WaitForSeconds(1f);

            if (discoveredServers.Values.Count <= 0)
            {
                StartToHostALocalGame(onlineScene, maxConnections);
            }
            else
            {
                foreach (ServerResponse info in discoveredServers.Values)
                {
                    Debug.Log("Connect to: " + info.EndPoint.Address.ToString());
                    StartLocalClient(onlineScene, info);
                }
            }
        }

        /// <summary>Multis the mode.</summary>
        /// <returns>Return none</returns>
        private IEnumerator MultiMode()
        {
            for (int i = 0; i < networkManager.ports.Count; i++)
            {
                bool connectNow = false;
                TcpClient client = new TcpClient();
                client.Connect("83.34.56.241", networkManager.ports[i]);

                if (client.Connected)
                {
                    connectNow = true;
                    client.Close();
                }

                if (connectNow) 
                {
                    ConnectToServerGame(networkManager.ports[i].ToString());
                }
            }
            yield return null;
        }

        /// <summary>Connects to server game.</summary>
        /// <param name="port">The port.</param>
        private void ConnectToServerGame(string port)
        {
            foreach (ushort portt in networkManager.ports)
            {
                if (portt.ToString() == port)
                {
                    GetComponent<TelepathyTransport>().port = portt;
                }
            }

            networkManager.networkAddress = "83.34.56.241";
            networkManager.StartClient();
        }

        /// <summary>Disconnects this instance.</summary>
        /// <returns></returns>
        private IEnumerator Disconnect() 
        {
            discoveredServers.Clear();
            networkManager.offlineScene = "MainMenu";

            yield return new WaitForSeconds(0.1f);
            
            discovery.StartDiscovery();

            yield return new WaitForSeconds(0.1f);

            networkManager.StopHost();
            networkManager.players.Clear();
        }

        /// <summary>Gets the IP address.</summary>
        /// <returns>My IP</returns>
        private static string GetIPAddress()
        {
            string address = string.Empty;
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }
    }
}