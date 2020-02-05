//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Multiplayer.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.IO;
using System.Net;
using Mirror;
using Mirror.Discovery;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manage multiplayer of the game.</summary>
public class Multiplayer : MonoBehaviour
{
    /// <summary>The network manager</summary>
    private NetworkManager networkManager;

    public readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();

    public NetworkDiscovery networkDiscovery;

    /// <summary>My IP</summary>
    private string myIP = null;

    /// <summary>My IP text</summary>
    private Text myIPText = null;

    /// <summary>The number of servers</summary>
    private Text numOfServers = null;

    /// <summary>The scroll rect</summary>
    [SerializeField]
    private Transform content = null;

    [SerializeField]
    private GameObject matchButton = null;

    private bool needUpdate = false;

    void Connect(ServerResponse info)
    {
        NetworkManager.singleton.StartClient(info.uri);
    }

    /// <summary>Show my IP.</summary>
    public void ShowMyIp() 
    {
        if (this.myIPText != null) 
        {
            this.myIPText.text = this.myIP;
        }
    }

    public void StartServer()
    {
        NetworkManager.singleton.networkAddress = "83.34.56.241";
        NetworkManager.singleton.StartServer();
    }

    /// <summary>Hosts the game.</summary>
    public void HostGame() 
    {
        discoveredServers.Clear();
        networkManager.StartHost();
        networkDiscovery.AdvertiseServer();
    }

    /// <summary>Connects to game.</summary>
    public void ConnectToGame()
    {
        //NetworkManager.singleton.networkAddress = GameObject.Find("Interface/Multiplayer/InputIP").GetComponent<InputField>().text;
        NetworkManager.singleton.networkAddress = "83.34.56.241";
        NetworkManager.singleton.StartClient();
    }

    public void FindServers() 
    {
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
        this.needUpdate = true;
    }

    public void OnDiscoveredServer(ServerResponse info)
    {
        discoveredServers[info.serverId] = info;
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

    /// <summary>Awakes this instance.</summary>
    private void Awake()
    {
        this.networkManager = this.GetComponent<NetworkManager>();
        this.networkDiscovery = this.GetComponent<NetworkDiscovery>();
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        this.myIP = GetIPAddress();
        this.myIPText = GameObject.Find("Interface/Multiplayer/MyIP/Text").GetComponent<Text>();
        this.ShowMyIp();

        this.numOfServers = GameObject.Find("Interface/Multiplayer/NumOfServers/Text").GetComponent<Text>();

        //this.networkDiscovery.OnServerFound.AddListener((info) => { OnDiscoveredServer(info); });

        this.content = GameObject.Find("Interface/Multiplayer/Scroll/Viewport/Content").transform;

        GameObject.Find("Interface/Multiplayer/Host").GetComponent<Button>().onClick.AddListener(() => { StartServer(); });
        GameObject.Find("Interface/Multiplayer/Connect").GetComponent<Button>().onClick.AddListener(() => { ConnectToGame(); });
        GameObject.Find("Interface/Multiplayer/FindServers").GetComponent<Button>().onClick.AddListener(() => { FindServers(); });
    }

    private void Update()
    {
        this.numOfServers.text = discoveredServers.Count.ToString();

        foreach (ServerResponse info in discoveredServers.Values)
        {
            if (!content.transform.Find(info.EndPoint.Address + "(Clone)")) 
            {
                matchButton.name = info.EndPoint.Address.ToString();
                matchButton.GetComponent<Button>().onClick.AddListener(() => { Connect(info); });
                matchButton.transform.Find("Text").GetComponent<Text>().text = info.EndPoint.Address.ToString();
                Instantiate(matchButton, content);
            }
        }
    }
}