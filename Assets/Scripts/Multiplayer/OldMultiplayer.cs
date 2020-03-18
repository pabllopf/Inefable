//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="OldMultiplayer.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using Mirror;
using Mirror.Discovery;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manage multiplayer of the game.</summary>
public class OldMultiplayer : MonoBehaviour
{
    /// <summary>The network manager</summary>
    private NetworkManager networkManager;

    public Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();

    public NetworkDiscovery networkDiscovery;

    /// <summary>My IP</summary>
    private readonly string myIP = null;

    /// <summary>My IP text</summary>
    private readonly Text myIPText = null;

    /// <summary>The scroll rect</summary>
    [SerializeField]
    private readonly Transform content = null;

    [SerializeField]
    private readonly GameObject matchButton = null;

    private void Connect(ServerResponse info)
    {
        networkManager.StartClient(info.uri);
    }

    /// <summary>Show my IP.</summary>
    public void ShowMyIp()
    {
        if (myIPText != null)
        {
            myIPText.text = myIP;
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

    public void FindLocalMacth()
    {
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
        StartCoroutine(CheckHostMatchs());
    }

    public void FindServers()
    {
        StopAllCoroutines();
        StartCoroutine(CheckServersMatchs());
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
        networkManager = GetComponent<NetworkManager>();
        networkDiscovery = GetComponent<NetworkDiscovery>();
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        /*numOfServers = GameObject.Find("Interface/Multiplayer/NumOfServers/Text").GetComponent<Text>();

        //this.networkDiscovery.OnServerFound.AddListener((info) => { OnDiscoveredServer(info); });

        content = GameObject.Find("Interface/Multiplayer/Scroll/Viewport/Content").transform;

        GameObject.Find("Interface/Multiplayer/Host").GetComponent<Button>().onClick.AddListener(() => { HostGame(); });
        GameObject.Find("Interface/Multiplayer/FindServers").GetComponent<Button>().onClick.AddListener(() => { FindServers(); });
        GameObject.Find("Interface/Multiplayer/FindLocalMatch").GetComponent<Button>().onClick.AddListener(() => { FindLocalMacth(); });
        */

        networkManager.onlineScene = "Town";
        FindLocalMacth();
        HostGame();
    }

    private void Update()
    {
        /*
        numOfServers.text = discoveredServers.Count.ToString();*/
    }


    private readonly List<GameObject> LocalRoomsButtons = new List<GameObject>();
    private IEnumerator CheckHostMatchs()
    {
        if (LocalRoomsButtons.Count > 0)
        {
            LocalRoomsButtons.ForEach(i => Destroy(i));
        }
        yield return new WaitForSeconds(1f);
        foreach (ServerResponse info in discoveredServers.Values)
        {
            if (!content.transform.Find(info.EndPoint.Address.ToString()))
            {
                Debug.Log(info.EndPoint.Address.ToString());
                Connect(info);
            }
        }
    }

    private readonly List<GameObject> portsButtons = new List<GameObject>();
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

        for (int i = 0; i < networkManager.ports.Count; i++)
        {
            if (!content.transform.Find(networkManager.ports[i].ToString()))
            {
                GameObject obj = Instantiate(matchButton, content);
                obj.name = networkManager.ports[i].ToString();
                obj.transform.Find("Text").GetComponent<Text>().text = "Room Server " + i;
                obj.GetComponent<Button>().onClick.AddListener(() => { ConnectToServerGame(obj.name); });
                obj.transform.Find("State").GetComponent<Image>().color = Color.red;

                //this.StartCoroutine(this.checkStatus(obj, this.networkManager.ports[i]));


                TcpClient client = new TcpClient();
                client.Connect("83.34.56.241", networkManager.ports[i]);

                if (client.Connected)
                {
                    obj.transform.Find("State").GetComponent<Image>().color = Color.green;
                    counter++;
                    client.Close();
                }
                sixe += 1f / networkManager.ports.Count;
                GameObject.Find("Interface/Multiplayer/Scrollbar").GetComponent<Scrollbar>().size = sixe;
                portsButtons.Add(obj);
                yield return new WaitForSeconds(0.5f);

            }
            yield return null;
        }


        yield return new WaitForSeconds(0f);
        GameObject.Find("Interface/Multiplayer/NumOfTestServers/Text").GetComponent<Text>().text = counter.ToString();
    }

    private IEnumerator checkStatus(GameObject obj, int port)
    {
        yield return null;


    }

}