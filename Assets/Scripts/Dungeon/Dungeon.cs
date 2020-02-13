//-----------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Dungeon.cs" company="UnMedioStudio">Open Source</copyright>
//-----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Generate a dungeon</summary>
public class Dungeon : MonoBehaviour
{
    /// <summary>The board width</summary>
    private static readonly int BoardWidth = 500;

    /// <summary>The board height</summary>
    private static readonly int BoardHeight = 500;

    /// <summary>The minimum number rooms</summary>
    private static readonly int MinNumRooms = 10;

    /// <summary>The maximum number rooms</summary>
    private static readonly int MaxNumRooms = 15;

    /// <summary>The minimum room width</summary>
    private static readonly int MinRoomWidth = 10;

    /// <summary>The maximum room width</summary>
    private static readonly int MaxRoomWidth = 15;

    /// <summary>The minimum room height</summary>
    private static readonly int MinRoomHeight = 10;

    /// <summary>The maximum room height</summary>
    private static readonly int MaxRoomHeight = 15;

    /// <summary>The minimum corridor width</summary>
    private static readonly int MinCorridorWidth = 6;

    /// <summary>The maximum corridor width</summary>
    private static readonly int MaxCorridorWidth = 8;

    /// <summary>The minimum corridor height</summary>
    private static readonly int MinCorridorHeight = 7;

    /// <summary>The maximum corridor height</summary>
    private static readonly int MaxCorridorHeight = 8;

    /// <summary>The rooms.</summary>
    private readonly List<Room> rooms = new List<Room>();

    /// <summary>The corridors</summary>
    private readonly List<Corridor> corridors = new List<Corridor>();

    /// <summary>The board</summary>
    private readonly int[,] board = new int[BoardWidth, BoardHeight];

    /// <summary>The player</summary>
    [SerializeField]
    private readonly GameObject player = null;

    /// <summary>The final boss</summary>
    [SerializeField]
    private readonly GameObject finalBoss = null;

    /// <summary>The items</summary>
    [SerializeField]
    private readonly List<Item> generalItems = null;

    /// <summary>The style maps</summary>
    [SerializeField]
    private readonly List<Style> dungeons = null;

    /// <summary>The style map</summary>
    private Style styleMap;

    /// <summary>The start interface</summary>
    private GameObject startInterface;

    /// <summary>The main camera</summary>
    private GameObject mainCamera;

    /// <summary>The information</summary>
    private Text info;


    /// <summary>Run when start the scene</summary>
    private void Start()
    {
        Language.Translate();
        StartCoroutine(Build());
    }

    /// <summary>Builds this instance.</summary>
    /// <returns>Return none</returns>
    private IEnumerator Build()
    {
        SearchObjects();

        info.text = string.Empty;
        foreach (char letter in Language.GetSentence(Clef.A26).ToCharArray())
        {
            info.text += letter;
            yield return null;
        }

        CreateRooms(Random.Range(MinNumRooms, MaxNumRooms));
        CreateCorridors(rooms.Count - 1);
        SetUpRoomsAndCorridors();
        yield return new WaitForSeconds(0.2f);

        info.text = string.Empty;
        foreach (char letter in Language.GetSentence(Clef.A26).ToCharArray())
        {
            info.text += letter;
            yield return null;
        }

        PrintRoomsInBoard();
        PrintCorridorsInBoard();
        PrintWallsInBoard();
        PrintOuterCornersInBoard();
        PrintInnerCornersInBoard();

        PrintBoardInGame();
        yield return new WaitForSeconds(0.2f);

        info.text = string.Empty;
        foreach (char letter in Language.GetSentence(Clef.A26).ToCharArray())
        {
            info.text += letter;
            yield return null;
        }

        SpawnListOf(generalItems);
        SpawnListOf(styleMap.GetLights());
        SpawnListOf(styleMap.GetFloors());
        SpawnListOf(styleMap.GetItems());
        SpawnListOf(styleMap.GetEnemys());
        SpawnListOf(styleMap.GetPets());
        yield return new WaitForSeconds(0.2f);

        info.text = string.Empty;
        foreach (char letter in Language.GetSentence(Clef.A26).ToCharArray())
        {
            info.text += letter;
            yield return null;
        }

        DestroyObject(startInterface);
        DestroyObject(mainCamera);
        SpawnBoss(new Vector2(rooms[rooms.Count - 1].XPos + (rooms[rooms.Count - 1].Width / 2), rooms[rooms.Count - 1].YPos + (rooms[rooms.Count - 1].Height / 2)), finalBoss);
        SpawnPlayer(new Vector2(250, 250), player);
        yield return new WaitForSeconds(0.2f);
    }

    /// <summary>Searches the objects.</summary>
    private void SearchObjects()
    {
        styleMap = dungeons[Random.Range(0, dungeons.Count)];
        styleMap.LoadSprites();

        startInterface = GameObject.Find("Start_Interface");
        mainCamera = GameObject.Find("Camera");

        GameObject.Find("Start_Interface/PopUpMessage/PopUp/NameDungeon").GetComponent<Text>().text = styleMap.GetName();
        info = GameObject.Find("Start_Interface/PopUpMessage/PopUp/Info").GetComponent<Text>();
    }

    /// <summary>Creates the rooms.</summary>
    /// <param name="amount">The amount.</param>
    private void CreateRooms(int amount)
    {
        rooms.AddRange(new Room[amount]);
    }

    /// <summary>Creates the corridors.</summary>
    /// <param name="amount">The amount.</param>
    private void CreateCorridors(int amount)
    {
        corridors.AddRange(new Corridor[amount]);
    }

    /// <summary>Sets up rooms and corridors.</summary>
    private void SetUpRoomsAndCorridors()
    {
        int roomWidth = Random.Range(MinRoomWidth, MaxRoomWidth);
        int roomHeight = Random.Range(MinRoomHeight, MaxRoomHeight);

        int corridorWidth = Random.Range(MinCorridorWidth, MaxCorridorWidth);
        int corridorLength = Random.Range(MinCorridorHeight, MaxCorridorHeight);

        rooms[0] = Room.SetUp(roomWidth, roomHeight, BoardWidth, BoardHeight);
        corridors[0] = Corridor.SetUp(rooms[0], corridorWidth, corridorLength, roomWidth, roomHeight, BoardWidth, BoardHeight, true);

        for (int i = 1; i < rooms.Count; i++)
        {
            roomWidth = Random.Range(MinRoomWidth, MaxRoomWidth);
            roomHeight = Random.Range(MinRoomHeight, MaxRoomHeight);

            rooms[i] = Room.SetUp(roomWidth, roomHeight, BoardWidth, BoardHeight, corridors[i - 1]);

            if (i < corridors.Count)
            {
                corridorWidth = Random.Range(MinCorridorWidth, MaxCorridorWidth);
                corridorLength = Random.Range(MinCorridorHeight, MaxCorridorHeight);

                corridors[i] = Corridor.SetUp(rooms[i], corridorWidth, corridorLength, roomWidth, roomHeight, BoardWidth, BoardHeight, false);
            }
        }
    }

    /// <summary>Prints the rooms in board.</summary>
    private void PrintRoomsInBoard()
    {
        foreach (Room room in rooms)
        {
            for (int x = (int)room.Position.x; x < (int)room.Position.x + room.Width; x++)
            {
                for (int y = (int)room.Position.y; y < (int)room.Position.y + room.Height; y++)
                {
                    board[x, y] = (board[x, y] == 0) ? 1 : board[x, y];
                }
            }
        }
    }

    /// <summary>Prints the corridors in board.</summary>
    private void PrintCorridorsInBoard()
    {
        foreach (Corridor corridor in corridors)
        {
            for (int y = 0; y < corridor.Height; y++)
            {
                for (int x = -corridor.Width / 2; x < corridor.Width / 2; x++)
                {
                    int xPos = corridor.GoEast ? (int)corridor.Position.x + y :
                               corridor.GoWest ? (int)corridor.Position.x - y :
                               (int)corridor.Position.x;

                    int yPos = corridor.GoNorth ? (int)corridor.Position.y + y :
                               corridor.GoSouth ? (int)corridor.Position.y - y :
                               (int)corridor.Position.y;

                    board[xPos, yPos + x] = (board[xPos, yPos + x] == 0) ? 1 : board[xPos, yPos + x];
                    board[xPos + x, yPos] = (board[xPos + x, yPos] == 0) ? 1 : board[xPos + x, yPos];
                }
            }
        }
    }

    /// <summary>Prints the walls in board.</summary>
    private void PrintWallsInBoard()
    {
        for (int x = 0; x < BoardWidth; x++)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                board[x, y] = (board[x, y] == 1 && board[x, y - 1] == 0) ? 2 :    // Wall Top
                                   (board[x, y] == 1 && board[x - 1, y] == 0) ? 3 :    // Wall Left    
                                   (board[x, y] == 1 && board[x + 1, y] == 0) ? 4 :    // Wall Right
                                   (board[x, y] == 1 && board[x, y + 1] == 0) ? 5 :    // Wall Down
                                   board[x, y];
            }
        }
    }

    /// <summary>Prints the outer corners in board.</summary>
    private void PrintOuterCornersInBoard()
    {
        for (int x = 0; x < BoardWidth; x++)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                board[x, y] = (board[x, y] != 0 && board[x - 1, y] == 0 && board[x, y - 1] == 0) ? 6 :    // Corner Outer Top Left
                                   (board[x, y] != 0 && board[x + 1, y] == 0 && board[x, y - 1] == 0) ? 7 :    // Corner Outer Top Right   
                                   (board[x, y] != 0 && board[x - 1, y] == 0 && board[x, y + 1] == 0) ? 8 :    // Corner Outer Down Left
                                   (board[x, y] != 0 && board[x + 1, y] == 0 && board[x, y + 1] == 0) ? 9 :    // Corner Outer Down Right
                                   board[x, y];
            }
        }
    }

    /// <summary>Prints the inner corners in board.</summary>
    private void PrintInnerCornersInBoard()
    {
        for (int x = 0; x < BoardWidth; x++)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                board[x, y] = (board[x, y] == 1 && board[x - 1, y - 1] == 0) ? 10 :   // Corner Inner Top Left
                                   (board[x, y] == 1 && board[x + 1, y - 1] == 0) ? 11 :   // Corner Inner Top Right
                                   (board[x, y] == 1 && board[x - 1, y + 1] == 0) ? 12 :   // Corner Inner Down Left
                                   (board[x, y] == 1 && board[x + 1, y + 1] == 0) ? 13 :   // Corner Inner Down Right
                                   board[x, y];
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
                if (board[x, y] != 0)
                {
                    MonoBehaviour.Instantiate(styleMap.SelectSprite(board[x, y]), new Vector2(x, y), Quaternion.identity, transform);
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
        GameObject master = new GameObject(name);

        while (quantity > 0)
        {
            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    if (board[x, y] == position && Random.Range(0, 1000) == 1)
                    {
                        board[x, y] = 255;
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
    private void SpawnListOf(List<Item> items)
    {
        items.ForEach(item => SpawnObject(item.Name, item.Quantity, item.Position, item.Object));
    }

    /// <summary>Spawns the boss.</summary>
    /// <param name="position">The position.</param>
    /// <param name="boss">The boss.</param>
    private void SpawnBoss(Vector2 position, GameObject boss)
    {
        MonoBehaviour.Instantiate(boss, position, Quaternion.identity);
    }

    /// <summary>Spawns the player.</summary>
    /// <param name="position">The position.</param>
    /// <param name="player">The player.</param>
    private void SpawnPlayer(Vector2 position, GameObject player)
    {
        MonoBehaviour.Instantiate(player, position, Quaternion.identity);
    }

    /// <summary>Destroys the object.</summary>
    /// <param name="obj">The object.</param>
    private void DestroyObject(GameObject obj)
    {
        MonoBehaviour.Destroy(obj);
    }
}