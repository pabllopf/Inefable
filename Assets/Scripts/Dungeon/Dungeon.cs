//-----------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Dungeon.cs" company="UnMedioStudio">Open Source</copyright>
//-----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>Generate a dungeon</summary>
public class Dungeon : MonoBehaviour
{
    /// <summary>The board width</summary>
    private static readonly int BoardWidth = 500;

    /// <summary>The board height</summary>
    private static readonly int BoardHeight = 500;

    /// <summary>The minimum number rooms</summary>
    private static readonly int MinNumRooms = 25;

    /// <summary>The maximum number rooms</summary>
    private static readonly int MaxNumRooms = 35;

    /// <summary>The minimum room width</summary>
    private static readonly int MinRoomWidth = 10;

    /// <summary>The maximum room width</summary>
    private static readonly int MaxRoomWidth = 20;

    /// <summary>The minimum room height</summary>
    private static readonly int MinRoomHeight = 10;

    /// <summary>The maximum room height</summary>
    private static readonly int MaxRoomHeight = 20;

    /// <summary>The minimum corridor width</summary>
    private static readonly int MinCorridorWidth = 6;

    /// <summary>The maximum corridor width</summary>
    private static readonly int MaxCorridorWidth = 9;

    /// <summary>The minimum corridor length</summary>
    private static readonly int MinCorridorLength = 7;

    /// <summary>The maximum corridor length</summary>
    private static readonly int MaxCorridorLength = 10;

    /// <summary>The board.</summary>
    private int[,] board;

    /// <summary>The rooms.</summary>
    private Room[] rooms;

    /// <summary>The corridors</summary>
    private Corridor[] corridors;

    /// <summary>The room width</summary>
    private int roomWidth;

    /// <summary>The room height</summary>
    private int roomHeight;

    /// <summary>The corridor width</summary>
    private int corridorWidth;

    /// <summary>The corridor length</summary>
    private int corridorLength;

    /// <summary>The player</summary>
    [SerializeField]
    private GameObject player = null;

    /// <summary>The camera</summary>
    [SerializeField]
    private GameObject playerCamera = null;

    /// <summary>The items</summary>
    [SerializeField]
    private List<Item> items = null;

    /// <summary>The style maps</summary>
    [SerializeField]
    private List<StyleMap> styleMaps = null;

    /// <summary>The style map</summary>
    private StyleMap styleMap;

    /// <summary>The start interface</summary>
    private GameObject startInterface;

    /// <summary>The main camera</summary>
    private GameObject mainCamera;

    /// <summary>The pop up message</summary>
    private GameObject popUpMessage;

    /// <summary>The information</summary>
    private Text info;

    /// <summary>Run before all</summary>
    private void Awake()
    {
        Game.LoadSettings();
        Language.Translate();
    }

    /// <summary>Run when start the scene</summary>
    private void Start()
    {
        this.InitMainParameters();
        this.StartCoroutine(this.InitComponents(Language.GetSentence(Key.A26)));
    }

    /// <summary>Initializes the main parameters</summary>
    private void InitMainParameters()
    {
        this.board = new int[BoardWidth, BoardHeight];

        this.rooms = new Room[Random.Range(MinNumRooms, MaxNumRooms)];
        this.corridors = new Corridor[this.rooms.GetLength(0) - 1];

        this.roomWidth = Random.Range(MinRoomWidth, MaxRoomWidth);
        this.roomHeight = Random.Range(MinRoomHeight, MaxRoomHeight);

        this.corridorWidth = Random.Range(MinCorridorWidth, MaxCorridorWidth);
        this.corridorLength = Random.Range(MinCorridorLength, MaxCorridorLength);

        this.styleMap = this.styleMaps[Random.Range(0, this.styleMaps.Count)];
        this.styleMap.LoadSprites();

        this.startInterface = GameObject.Find("Start_Interface");
        this.mainCamera = GameObject.Find("Camera");

        this.popUpMessage = GameObject.Find("Start_Interface/PopUpMessage");
        this.popUpMessage.SetActive(true);
        this.popUpMessage.transform.Find("PopUp/NameDungeon").GetComponent<Text>().text = this.styleMap.GetName();

        this.info = GameObject.Find("Start_Interface/PopUpMessage/PopUp/Info").GetComponent<Text>();
    }

    /// <summary>Initialize the components.</summary>
    /// <param name="message">The message</param>
    /// <returns>Return None</returns>
    private IEnumerator InitComponents(string message)
    {
        this.info.text = string.Empty;
        foreach (char letter in message.ToCharArray())
        {
            this.info.text += letter;
            yield return null;
        }

        yield return null;

        this.GenerateRoomsAndCorridors();
        this.StartCoroutine(this.CreatedRoomsAndCorridors(Language.GetSentence(Key.A27)));
    }

    /// <summary>Load the shop.</summary>
    /// <param name="message">The message</param>
    /// <returns>Return None</returns>
    private IEnumerator LoadShop(string message)
    {
        this.info.text = string.Empty;
        foreach (char letter in message.ToCharArray())
        {
            this.info.text += letter;
            yield return null;
        }

        yield return null;

        SceneManager.LoadScene("Shop", LoadSceneMode.Single);
    }

    /// <summary>Create the rooms and corridors.</summary>
    /// <param name="message">The message</param>
    /// <returns>Return None</returns>
    private IEnumerator CreatedRoomsAndCorridors(string message)
    {
        this.info.text = string.Empty;
        foreach (char letter in message.ToCharArray())
        {
            this.info.text += letter;
            yield return null;
        }

        yield return null;

        this.PrintRoomsInBoard();
        this.PrintCorridorsInBoard();
        this.PrintWallsAndCornersInBoard();
        this.PrintBoardInGame();

        this.StartCoroutine(this.GeneratedDungeon(Language.GetSentence(Key.A28)));
    }

    /// <summary>Generate the dungeon.</summary>
    /// <param name="message">The message</param>
    /// <returns>Return None</returns>
    private IEnumerator GeneratedDungeon(string message)
    {
        this.info.text = string.Empty;
        foreach (char letter in message.ToCharArray())
        {
            this.info.text += letter;
            yield return null;
        }

        yield return null;

        this.SpawnItems();
        this.StartCoroutine(this.FinalDetails(Language.GetSentence(Key.A29)));
    }

    /// <summary>Finish The Final Details</summary>
    /// <param name="message">The message</param>
    /// <returns>Return None</returns>
    private IEnumerator FinalDetails(string message)
    {
        this.info.text = string.Empty;
        foreach (char letter in message.ToCharArray())
        {
            this.info.text += letter;
            yield return null;
        }

        yield return null;

        MonoBehaviour.Destroy(this.mainCamera);
        MonoBehaviour.Destroy(this.startInterface);
        this.SpawnPlayer(new Vector2(250, 250));
    }

    /// <summary>Generates the rooms and corridors</summary>
    private void GenerateRoomsAndCorridors()
    {
        this.rooms[0] = new Room();
        this.rooms[0].SetupFirstRoom(this.roomWidth, this.roomHeight, this.board.GetLength(0), this.board.GetLength(1));

        this.corridors[0] = new Corridor();
        this.corridors[0].SetupCorridor(this.rooms[0], this.corridorWidth, this.corridorLength, this.roomWidth, this.roomHeight, this.board.GetLength(0), this.board.GetLength(1), true);

        for (int i = 1; i < this.rooms.Length; i++)
        {
            this.roomWidth = Random.Range(MinRoomWidth, MaxRoomWidth);
            this.roomHeight = Random.Range(MinRoomHeight, MaxRoomHeight);

            this.rooms[i] = new Room();
            this.rooms[i].SetupRoom(this.roomWidth, this.roomHeight, this.board.GetLength(0), this.board.GetLength(1), this.corridors[i - 1]);

            if (i < this.corridors.Length)
            {
                this.corridorWidth = Random.Range(MinCorridorWidth, MaxCorridorWidth);
                this.corridorLength = Random.Range(MinCorridorLength, MaxCorridorLength);

                this.corridors[i] = new Corridor();
                this.corridors[i].SetupCorridor(this.rooms[i], this.corridorWidth, this.corridorLength, this.roomWidth, this.roomHeight, this.board.GetLength(0), this.board.GetLength(1), false);
            }
        }
    }

    /// <summary>Prints the rooms in board</summary>
    private void PrintRoomsInBoard()
    {
        foreach (Room room in this.rooms)
        {
            for (int x = 0; x < room.GetWidth(); x++)
            {
                for (int y = 0; y < room.GetHeight(); y++)
                {
                    int xCoord = room.GetXPos() + x;
                    int yCoord = room.GetYPos() + y;

                    if (this.board[xCoord, yCoord] == 0)
                    {
                        this.board[xCoord, yCoord] = 1;
                    }
                }
            }
        }
    }

    /// <summary>Prints the corridors in board</summary>
    private void PrintCorridorsInBoard()
    {
        foreach (Corridor corridor in this.corridors)
        {
            for (int y = 0; y < corridor.GetHeight(); y++)
            {
                for (int x = -corridor.GetWidth() / 2; x < corridor.GetWidth() / 2; x++)
                {
                    int xPos = corridor.GetStartXPos();
                    int yPos = corridor.GetStartYPos();

                    switch (corridor.GetDirection())
                    {
                        case Direction.North:
                            yPos += y;
                            break;
                        case Direction.East:
                            xPos += y;
                            break;
                        case Direction.South:
                            yPos -= y;
                            break;
                        case Direction.West:
                            xPos -= y;
                            break;
                    }

                    if (this.board[xPos, yPos + x] == 0)
                    {
                        this.board[xPos, yPos + x] = 1;
                    }

                    if (this.board[xPos + x, yPos] == 0)
                    {
                        this.board[xPos + x, yPos] = 1;
                    }
                }
            }
        }
    }

    /// <summary>Prints the walls and corners in board</summary>
    private void PrintWallsAndCornersInBoard()
    {
        for (int x = 0; x < BoardWidth; x++)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                if (this.board[x, y] == 1)
                {
                    if (this.board[x, y - 1] == 0) 
                    { 
                        this.board[x, y] = 2; 
                    }

                    if (this.board[x - 1, y] == 0) 
                    { 
                        this.board[x, y] = 3; 
                    }

                    if (this.board[x + 1, y] == 0) 
                    { 
                        this.board[x, y] = 4; 
                    }

                    if (this.board[x, y + 1] == 0) 
                    { 
                        this.board[x, y] = 5; 
                    }
                }
            }
        }

        for (int x = 0; x < BoardWidth; x++)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                if (this.board[x, y] != 0)
                {
                    if (this.board[x - 1, y] == 0 && this.board[x, y - 1] == 0) 
                    { 
                        this.board[x, y] = 6; 
                    }

                    if (this.board[x + 1, y] == 0 && this.board[x, y - 1] == 0) 
                    { 
                        this.board[x, y] = 7; 
                    }

                    if (this.board[x - 1, y] == 0 && this.board[x, y + 1] == 0) 
                    { 
                        this.board[x, y] = 8; 
                    }

                    if (this.board[x + 1, y] == 0 && this.board[x, y + 1] == 0) 
                    { 
                        this.board[x, y] = 9; 
                    }
                }
            }
        }

        for (int x = 0; x < BoardWidth; x++)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                if (this.board[x, y] == 1)
                {
                    if (this.board[x - 1, y - 1] == 0) 
                    { 
                        this.board[x, y] = 10; 
                    }

                    if (this.board[x + 1, y - 1] == 0) 
                    { 
                        this.board[x, y] = 11; 
                    }

                    if (this.board[x - 1, y + 1] == 0) 
                    { 
                        this.board[x, y] = 12; 
                    }

                    if (this.board[x + 1, y + 1] == 0) 
                    { 
                        this.board[x, y] = 13; 
                    }
                }
            }
        }
    }

    /// <summary>Prints the board in game</summary>
    private void PrintBoardInGame()
    {
        for (int x = 0; x < BoardWidth; x++)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                if (this.board[x, y] != 0) 
                {
                    GameObject gameObject = MonoBehaviour.Instantiate(this.styleMap.SelectSprite(this.board[x, y]), new Vector2(x, y), Quaternion.identity);
                    gameObject.transform.parent = this.transform;
                }
            }
        }
    }

    /// <summary>Spawns the items</summary>
    private void SpawnItems()
    {
        foreach (Item item in this.items)
        {
            if (item.GetDungeon() == this.styleMap.GetDungeon() || item.GetDungeon() == 0)
            {
                GameObject master = new GameObject();
                master.name = item.GetItem().name;
                int quantity = Random.Range(item.GetQuantityMin(), item.GetQuantityMax());
                int numSpawned = 0;

                while (numSpawned < quantity)
                {
                    for (int x = 0; x < BoardWidth; x++)
                    {
                        for (int y = 0; y < BoardHeight; y++)
                        {
                            if (this.board[x, y] == item.GetPosition() && numSpawned < quantity)
                            {
                                if (Random.Range(0, 100) == 1)
                                {
                                    this.board[x, y] = 255;
                                    numSpawned++;
                                    var itemSpawned = Instantiate(item.GetItem(), new Vector3(x, y, 0), Quaternion.identity);
                                    itemSpawned.transform.parent = master.transform;
                                    foreach (Behaviour behaviour in itemSpawned.GetComponents<Behaviour>())
                                    {
                                        behaviour.enabled = false;
                                    }

                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>Spawn a player in a specific position</summary>
    /// <param name="position">Position where be spawned the player</param>
    private void SpawnPlayer(Vector2 position)
    {
        GameObject playerSpawned = Instantiate(this.player, position, Quaternion.identity);
        playerSpawned.GetComponent<Player>().SetPosition();
        MonoBehaviour.Instantiate(this.playerCamera, position, Quaternion.identity);
    }
}
