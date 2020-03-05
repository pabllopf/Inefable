//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Portal.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using Mirror;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manage a portal</summary>
public class Portal : MonoBehaviour
{
    /// <summary>The name of scene to play</summary>
    private const string nameOfSceneToPlay = "Dungeon";

    /// <summary>The network</summary>
    private GameObject network = null;

    private readonly List<GameObject> localRoomsButtons = new List<GameObject>();

    /// <summary>The start game menu</summary>
    private GameObject startGameMenu = null;

    private GameObject mainPanel = null;
    private GameObject multiPanel = null;

    /// <summary>The match button</summary>
    [SerializeField]
    private GameObject matchButton = null;

    private GameObject listGames = null;
    private GameObject listGamesContent = null;

    /// <summary>Gets or sets the network.</summary>
    /// <value>The network.</value>
    public GameObject Network { get => network; set => network = value; }

    /// <summary>Gets or sets the start game menu.</summary>
    /// <value>The start game menu.</value>
    public GameObject StartGameMenu { get => startGameMenu; set => startGameMenu = value; }

    /// <summary>Gets or sets the match button.</summary>
    /// <value>The match button.</value>
    public GameObject MatchButton { get => matchButton; set => matchButton = value; }

    /// <summary>Gets or sets the local rooms buttons1.</summary>
    /// <value>The local rooms buttons1.</value>
    public List<GameObject> LocalRoomsButtons { get => LocalRoomsButtons; set => LocalRoomsButtons = value; }

    /// <summary>Called when [trigger enter2 d].</summary>
    /// <param name="collision">The collision.</param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            startGameMenu.SetActive(true);
        }
    }

    /// <summary>Called when [trigger exit2 d].</summary>
    /// <param name="collision">The collision.</param>
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            startGameMenu.SetActive(false);
        }
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        network = GameObject.FindGameObjectWithTag("Network").gameObject;

        listGames = transform.Find("Interface/ListGames").gameObject;
        listGamesContent = transform.Find("Interface/ListGames/Scroll View/Viewport/Content").gameObject;

        mainPanel = transform.Find("Interface/MainPanel").gameObject;
        multiPanel = transform.Find("Interface/MultiPanel").gameObject;




        startGameMenu = transform.Find("Interface").gameObject;

        startGameMenu.transform.Find("MainPanel/SinglePlayer").GetComponent<Button>().onClick.AddListener(() => { PlaySingleDungeon(); });
        startGameMenu.transform.Find("MainPanel/MultiPlayer").GetComponent<Button>().onClick.AddListener(() => { MultiPlayer(); });

        startGameMenu.transform.Find("MainPanel").gameObject.SetActive(true);
        startGameMenu.transform.Find("MultiPanel").gameObject.SetActive(false);

        startGameMenu.transform.Find("MultiPanel/LocalGame").GetComponent<Button>().onClick.AddListener(() => { PlayLocalDungeon(); });
        startGameMenu.transform.Find("MultiPanel/ServerGame").GetComponent<Button>().onClick.AddListener(() => { ServerGame(); });

        listGames.SetActive(false);
        startGameMenu.SetActive(false);
    }

    /// <summary>Singles the player.</summary>
    private void PlaySingleDungeon()
    {
        network.GetComponent<NetworkManager>().StopHost();
        network.GetComponent<NetworkManager>().players.Clear();

        network.GetComponent<Multiplayer>().PlaySingleDungeon();
    }

    /// <summary>Hosts the local game.</summary>
    private void PlayLocalDungeon()
    {
        multiPanel.SetActive(false);
        StartCoroutine(network.GetComponent<Multiplayer>().PlayLocalDungeon());
    }

    /// <summary>Servers the game.</summary>
    private void ServerGame()
    {
        multiPanel.SetActive(false);
        listGames.SetActive(true);
    }

    /// <summary>Multis the player.</summary>
    private void MultiPlayer()
    {
        mainPanel.SetActive(false);
        multiPanel.SetActive(true);
    }
}