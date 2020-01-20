//-----------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Dungeon.cs" company="UnMedioStudio">Open Source</copyright>
//-----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    /// <summary>The minimum corridor height</summary>
    private static readonly int MinCorridorHeight = 7;

    /// <summary>The maximum corridor height</summary>
    private static readonly int MaxCorridorHeight = 10;

    /// <summary>The board</summary>
    private int[,] board = new int[BoardWidth, BoardHeight];

    /// <summary>The rooms.</summary>
    private List<Room> rooms = new List<Room>();

    /// <summary>The corridors</summary>
    private List<Corridor> corridors = new List<Corridor>();

    /// <summary>The player</summary>
    [SerializeField]
    private GameObject player = null;

    /// <summary>The final boss</summary>
    [SerializeField]
    private GameObject finalBoss = null;

    /// <summary>The items</summary>
    [SerializeField]
    private List<Item> generalItems = null;

    /// <summary>The style maps</summary>
    [SerializeField]
    private List<StyleMap> dungeons = null;

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

    /// <summary>Gets the number of rooms.</summary>
    /// <value>The number of rooms.</value>
    private int NumOfRooms => Random.Range(MinNumRooms, MaxNumRooms);

    /// <summary>Run before all</summary>
    private void Awake()
    {
        Game.LoadSettings();
        Language.Translate();
    }

    /// <summary>Run when start the scene</summary>
    private void Start()
    {
        this.StartCoroutine(this.Build());
    }

    /// <summary>Builds this instance.</summary>
    /// <returns>Return none</returns>
    private IEnumerator Build()
    {
        this.SearchObjects();

        this.info.text = string.Empty;
        foreach (char letter in Language.GetSentence(Key.A26).ToCharArray())
        {
            this.info.text += letter;
            yield return null;
        }

        this.CreateRooms(this.NumOfRooms);
        this.CreateCorridors(this.rooms.Count - 1);
        this.SetUpRoomsAndCorridors();
        yield return new WaitForSeconds(0.2f);

        this.info.text = string.Empty;
        foreach (char letter in Language.GetSentence(Key.A26).ToCharArray())
        {
            this.info.text += letter;
            yield return null;
        }

        this.PrintRoomsInBoard();
        this.PrintCorridorsInBoard();
        this.PrintWallsAndCornersInBoard();
        this.PrintBoardInGame();
        yield return new WaitForSeconds(0.2f);

        this.info.text = string.Empty;
        foreach (char letter in Language.GetSentence(Key.A26).ToCharArray())
        {
            this.info.text += letter;
            yield return null;
        }

        this.SpawnListOf(this.generalItems);
        this.SpawnListOf(this.styleMap.GetFloors());
        this.SpawnListOf(this.styleMap.GetItems());
        this.SpawnListOf(this.styleMap.GetEnemys());
        this.SpawnListOf(this.styleMap.GetPets());
        yield return new WaitForSeconds(0.2f);

        this.info.text = string.Empty;
        foreach (char letter in Language.GetSentence(Key.A26).ToCharArray())
        {
            this.info.text += letter;
            yield return null;
        }

        this.DestroyObject(this.startInterface);
        this.DestroyObject(this.mainCamera);
        this.SpawnBoss(new Vector2(this.rooms[this.rooms.Count - 1].XPos + (this.rooms[this.rooms.Count - 1].Width / 2), this.rooms[this.rooms.Count - 1].YPos + (this.rooms[this.rooms.Count - 1].Height / 2)), this.finalBoss);
        this.SpawnPlayer(new Vector2(250, 250), this.player);
        yield return new WaitForSeconds(0.2f);
    }

    /// <summary>Searches the objects.</summary>
    private void SearchObjects()
    {
        this.styleMap = this.dungeons[Random.Range(0, this.dungeons.Count)];
        this.styleMap.LoadSprites();

        this.startInterface = GameObject.Find("Start_Interface");
        this.mainCamera = GameObject.Find("Camera");

        this.popUpMessage = GameObject.Find("Start_Interface/PopUpMessage");
        this.popUpMessage.SetActive(true);
        this.popUpMessage.transform.Find("PopUp/NameDungeon").GetComponent<Text>().text = this.styleMap.GetName();

        this.info = GameObject.Find("Start_Interface/PopUpMessage/PopUp/Info").GetComponent<Text>();
    }

    /// <summary>Creates the rooms.</summary>
    /// <param name="amount">The amount.</param>
    private void CreateRooms(int amount) 
    {
        for (int i = 0; i < amount; i++) 
        {
            this.rooms.Add(new Room());
        }
    }

    /// <summary>Creates the corridors.</summary>
    /// <param name="amount">The amount.</param>
    private void CreateCorridors(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            this.corridors.Add(new Corridor());
        }
    }

    /// <summary>Sets up rooms and corridors.</summary>
    private void SetUpRoomsAndCorridors()
    {
        int roomWidth = Random.Range(MinRoomWidth, MaxRoomWidth);
        int roomHeight = Random.Range(MinRoomHeight, MaxRoomHeight);

        int corridorWidth = Random.Range(MinCorridorWidth, MaxCorridorWidth);
        int corridorLength = Random.Range(MinCorridorHeight, MaxCorridorHeight);

        this.rooms[0] = Room.SetUp(roomWidth, roomHeight, BoardWidth, BoardHeight);
        this.corridors[0] = Corridor.SetUp(this.rooms[0], corridorWidth, corridorLength, roomWidth, roomHeight, BoardWidth, BoardHeight, true);

        for (int i = 1; i < this.rooms.Count; i++)
        {
            roomWidth = Random.Range(MinRoomWidth, MaxRoomWidth);
            roomHeight = Random.Range(MinRoomHeight, MaxRoomHeight);

            this.rooms[i] = Room.SetUp(roomWidth, roomHeight, BoardWidth, BoardHeight, this.corridors[i - 1]);

            if (i < this.corridors.Count)
            {
                corridorWidth = Random.Range(MinCorridorWidth, MaxCorridorWidth);
                corridorLength = Random.Range(MinCorridorHeight, MaxCorridorHeight);

                this.corridors[i] = Corridor.SetUp(this.rooms[i], corridorWidth, corridorLength, roomWidth, roomHeight, BoardWidth, BoardHeight, false);
            }
        }
    }

    /// <summary>Prints the rooms in board.</summary>
    private void PrintRoomsInBoard()
    {
        this.rooms.ToList().ForEach(room => this.PrintInBoard(room.Width, room.Height, room.Position));
    }

    /// <summary>Prints the corridors in board.</summary>
    private void PrintCorridorsInBoard()
    {
        this.corridors.ToList().ForEach(corridor => this.PrintInBoard(corridor.Width, corridor.Height, corridor.Position, corridor.Direction));
    }

    /// <summary>Prints the in board.</summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="position">The position.</param>
    private void PrintInBoard(int width, int height, Vector2 position) 
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int xCoord = (int)position.x + x;
                int yCoord = (int)position.y + y;

                if (this.board[xCoord, yCoord] == 0)
                {
                    this.board[xCoord, yCoord] = 1;
                }
            }
        }
    }

    /// <summary>Prints the in board.</summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="position">The position.</param>
    /// <param name="direction">The direction.</param>
    private void PrintInBoard(int width, int height, Vector2 position, Direction direction)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = -width / 2; x < width / 2; x++)
            {
                int xPos = (int)position.x;
                int yPos = (int)position.y;

                switch (direction)
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

    /// <summary>Prints the walls and corners in board.</summary>
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

    /// <summary>Prints the board in game.</summary>
    private void PrintBoardInGame()
    {
        for (int x = 0; x < BoardWidth; x++)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                if (this.board[x, y] != 0) 
                {
                    MonoBehaviour.Instantiate(this.styleMap.SelectSprite(this.board[x, y]), new Vector2(x, y), Quaternion.identity, this.transform);
                }
            }
        }
    }

    /// <summary>Spawns the object.</summary>
    /// <param name="name">The name.</param>
    /// <param name="quantity">The quantity.</param>
    /// <param name="position">The position.</param>
    /// <param name="item">The item.</param>
    private void SpawnObject(string name, int quantity, int position, GameObject item) 
    {
        GameObject master = new GameObject()
        {
            name = name,
        };

        while (quantity > 0)
        {
            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    if (this.board[x, y] == position && Random.Range(0, 1000) == 1) 
                    {
                        this.board[x, y] = 255;
                        quantity--;

                        GameObject itemSpawned = MonoBehaviour.Instantiate(item, new Vector2(x, y), Quaternion.identity, master.transform);
                        itemSpawned.GetComponents<Behaviour>().ToList().ForEach(i => i.enabled = false);

                        break;
                    }
                }
            }
        }
    }

    /// <summary>Spawns the list of.</summary>
    /// <param name="items">The items.</param>
    private void SpawnListOf(List<Item> items) => items.ForEach(item => this.SpawnObject(item.Name, item.Quantity, item.Position, item.Object));

    /// <summary>Spawns the boss.</summary>
    /// <param name="position">The position.</param>
    /// <param name="boss">The boss.</param>
    private void SpawnBoss(Vector2 position, GameObject boss) => MonoBehaviour.Instantiate(boss, position, Quaternion.identity);

    /// <summary>Spawns the player.</summary>
    /// <param name="position">The position.</param>
    /// <param name="player">The player.</param>
    private void SpawnPlayer(Vector2 position, GameObject player) => MonoBehaviour.Instantiate(player, position, Quaternion.identity);

    /// <summary>Destroys the object.</summary>
    /// <param name="obj">The object.</param>
    private void DestroyObject(GameObject obj) => MonoBehaviour.Destroy(obj);
}