//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Multiplayer.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
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
        this.networkManager.StartClient(info.uri);
    }

    /// <summary>Show my IP.</summary>
    public void ShowMyIp() 
    {
        if (this.myIPText != null) 
        {
            this.myIPText.text = this.myIP;
        }
    }

    /// <summary>Hosts the game.</summary>
    public void HostGame() 
    {
        discoveredServers.Clear();
        networkManager.StartHost();
        networkDiscovery.AdvertiseServer();
    }

    /// <summary>Connects to game.</summary>
    public void ConnectToServerGame(string port)
    {
        foreach (ushort portt in this.networkManager.ports) 
        {
            if (portt.ToString() == port) 
            {
                this.GetComponent<TelepathyTransport>().port = portt;
            }
        }
        this.networkManager.networkAddress = "83.34.56.241";
        this.networkManager.StartClient();
    }

    public void FindLocalMacth() 
    {
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
        this.StartCoroutine(this.CheckHostMatchs());
    }

    public void FindServers()
    {
        this.StopAllCoroutines();
        this.StartCoroutine(this.CheckServersMatchs());
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
        this.numOfServers = GameObject.Find("Interface/Multiplayer/NumOfServers/Text").GetComponent<Text>();

        //this.networkDiscovery.OnServerFound.AddListener((info) => { OnDiscoveredServer(info); });

        this.content = GameObject.Find("Interface/Multiplayer/Scroll/Viewport/Content").transform;

        GameObject.Find("Interface/Multiplayer/Host").GetComponent<Button>().onClick.AddListener(() => { HostGame(); });
        GameObject.Find("Interface/Multiplayer/FindServers").GetComponent<Button>().onClick.AddListener(() => { FindServers(); });
        GameObject.Find("Interface/Multiplayer/FindLocalMatch").GetComponent<Button>().onClick.AddListener(() => { FindLocalMacth(); });


        
    }

    private void Update()
    {

        this.numOfServers.text = discoveredServers.Count.ToString();
    }

    private IEnumerator CheckHostMatchs() 
    {
        yield return new WaitForSeconds(1f);
        foreach (ServerResponse info in discoveredServers.Values)
        {
            if (!content.transform.Find(info.EndPoint.Address.ToString()))
            {
                GameObject obj = Instantiate(matchButton, content);
                obj.name = info.EndPoint.Address.ToString();
                obj.GetComponent<Button>().onClick.AddListener(() => { Connect(info); });
                obj.transform.Find("Text").GetComponent<Text>().text = info.EndPoint.Address.ToString();
            }
        }
    }

    private List<GameObject> portsButtons = new List<GameObject>();
    private int counter = 0;
    private float sixe = 0f;

    private IEnumerator CheckServersMatchs()
    {
        if (portsButtons.Count > 0) 
        { 
            portsButtons.ForEach(i => Destroy(i)); 
        }

        yield return null;
        GameObject.Find("Interface/Multiplayer/Scrollbar").GetComponent<Scrollbar>().size = 0;
        counter = 0;
        sixe = 0;

        for (int i = 0; i < this.networkManager.ports.Count; i++)
        {
            if (!content.transform.Find(this.networkManager.ports[i].ToString()))
            {
                GameObject obj = Instantiate(matchButton, content);
                obj.name = this.networkManager.ports[i].ToString();
                obj.transform.Find("Text").GetComponent<Text>().text = "Room Server " + i;
                obj.GetComponent<Button>().onClick.AddListener(() => { ConnectToServerGame(obj.name); });
                obj.transform.Find("State").GetComponent<Image>().color = Color.red;

                //this.StartCoroutine(this.checkStatus(obj, this.networkManager.ports[i]));


                TcpClient client = new TcpClient();
                client.Connect("83.34.56.241", this.networkManager.ports[i]);

                if (client.Connected)
                {
                    obj.transform.Find("State").GetComponent<Image>().color = Color.green;
                    counter++;
                    client.Close();
                }
                sixe += 1f / this.networkManager.ports.Count;
                GameObject.Find("Interface/Multiplayer/Scrollbar").GetComponent<Scrollbar>().size = sixe;
                portsButtons.Add(obj);
                yield return new WaitForSeconds(0.5f);

            }
            yield return null;
        }

       
        yield return new WaitForSeconds(0f);
        GameObject.Find("Interface/Multiplayer/NumOfTestServers/Text").GetComponent<Text>().text = counter.ToString();
    }

    IEnumerator checkStatus(GameObject obj, int port)
    {
        yield return null;

        
    }

}