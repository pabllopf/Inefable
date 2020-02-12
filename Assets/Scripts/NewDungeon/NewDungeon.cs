//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="NewDungeon.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>Generate a dungeon</summary>
public class NewDungeon : NetworkBehaviour
{
    private const int BoardWidth = 500;
    private const int BoardHeight = 500;

    private const int NumOfRooms = 15;

    private const int FirstRoomWidth = 10;
    private const int FirstRoomHeight = 10;

    private const int RoomWidth = 8;
    private const int RoomHeight = 8;

    private const int LastRoomWidth = 10;
    private const int LastRoomHeight = 10;

    private const int CorridorWidth = 4;
    private const int CorridorHeight = 4;

    private readonly int[,] board = new int[BoardWidth, BoardHeight];
    private readonly List<NewRoom> rooms = new List<NewRoom>();
    private readonly List<NewCorridor> corridors = new List<NewCorridor>();

    [SerializeField]
    private readonly GameObject player = null;

    [SerializeField]
    private readonly GameObject altar = null;

    [SerializeField]
    private readonly GameObject boss = null;

    [SerializeField]
    private readonly List<Item> generalItems = null;

    private readonly List<Vector2> positionsToSpawnThePlayers = new List<Vector2>();

    [SerializeField]
    private readonly List<Style> dungeons = null;
    private Style style = null;

    private void Start()
    {
        if (!isServer)
        {
            return;
        }

        SetUpDungeonStyle();

        CreateRooms();
        CreateCorridors();

        SetUpFirstRoom();
        SetUpRoomsAndCorridors();


        GetPositionsToSpawn();

        PrintRoomsInBoard();
        PrintCorridorsInBoard();

        SetUpTheLastRoom();

        PrintRoomsInBoard();
        PrintCorridorsInBoard();

        PrintWallsInBoard();
        PrintOuterCornersInBoard();
        PrintInnerCornersInBoard();

        PrintBoardInGame();

        SpawnListOf(generalItems);
        SpawnListOf(style.GetLights());
        SpawnListOf(style.GetItems());
        SpawnListOf(style.GetEnemys());
        SpawnListOf(style.GetPets());
        SpawnListOf(style.GetFloors());

        PrintTheBoss();

        PrintAltarsInGame();
        //this.PrintPlayersInGame();

        transform.position = new Vector2(-255, -255);
    }

    /// <summary>Set up dungeon style.</summary>
    private void SetUpDungeonStyle()
    {
        style = dungeons[Random.Range(0, dungeons.Count)];
        style.LoadSprites();
    }

    /// <summary>Creates the rooms.</summary>
    private void CreateRooms()
    {
        rooms.AddRange(new NewRoom[NumOfRooms]);
    }

    /// <summary>Creates the corridors.</summary>
    private void CreateCorridors()
    {
        corridors.AddRange(new NewCorridor[rooms.Count - 1]);
    }

    private void SetUpFirstRoom()
    {
        rooms[0] = NewRoom.SetUpFirstRoom(BoardWidth / 2, BoardHeight / 2, FirstRoomWidth, FirstRoomHeight);
        corridors[0] = NewCorridor.SetUpFirstCorridor(CorridorWidth, CorridorHeight, rooms[0]);
    }

    private void SetUpRoomsAndCorridors()
    {
        for (int i = 1; i < rooms.Count; i++)
        {
            rooms[i] = NewRoom.SetUp(RoomWidth, RoomHeight, corridors[i - 1]);

            if (i < corridors.Count)
            {
                corridors[i] = NewCorridor.SetUp(CorridorWidth, CorridorHeight, rooms[i]);
            }
        }
    }

    private void SetUpTheLastRoom()
    {
        int i = NumOfRooms - 1;
        rooms[i] = NewRoom.SetUp(LastRoomWidth, LastRoomHeight, corridors[i - 1]);


        while (hasRoom(rooms[i]))
        {
            corridors[i - 1] = NewCorridor.SetUp(CorridorWidth, CorridorHeight, rooms[i - 1]);
            rooms[i] = NewRoom.SetUp(LastRoomWidth, LastRoomHeight, corridors[i - 1]);
        }
    }

    private bool hasRoom(NewRoom room)
    {
        for (int x = room.XPos; x < room.XPos + room.Width; x++)
        {
            for (int y = room.YPos; y < room.YPos + room.Height; y++)
            {
                if (board[x, y] == 1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void GetPositionsToSpawn()
    {
        Vector2 center = new Vector2((BoardWidth / 2) + (FirstRoomWidth / 2), (BoardHeight / 2) + (FirstRoomHeight / 2));
        positionsToSpawnThePlayers.Add(center + new Vector2(1.5f, 1.5f));
        positionsToSpawnThePlayers.Add(center + new Vector2(-1.5f, -1.5f));
        positionsToSpawnThePlayers.Add(center + new Vector2(-1.5f, 1.5f));
        positionsToSpawnThePlayers.Add(center + new Vector2(1.5f, -1.5f));
    }

    private void PrintRoomsInBoard()
    {
        foreach (NewRoom room in rooms)
        {
            for (int x = room.XPos; x < room.XPos + room.Width; x++)
            {
                for (int y = room.YPos; y < room.YPos + room.Height; y++)
                {
                    board[x, y] = (board[x, y] == 0) ? 1 : board[x, y];
                }
            }
        }
    }

    private void PrintCorridorsInBoard()
    {
        foreach (NewCorridor corridor in corridors)
        {
            for (int x = corridor.XPos; x < corridor.XPos + corridor.Width; x++)
            {
                for (int y = corridor.YPos; y < corridor.YPos + corridor.Height; y++)
                {
                    board[x, y] = (board[x, y] == 0) ? 1 : board[x, y];
                }
            }
        }
    }

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

    private void PrintBoardInGame()
    {
        for (int x = 0; x < BoardWidth; x++)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                if (board[x, y] != 0)
                {
                    MonoBehaviour.Instantiate(style.SelectSprite(board[x, y]), new Vector2(x, y), Quaternion.identity, transform);
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

    private void PrintTheBoss()
    {
        int x = rooms[NumOfRooms - 1].XPos + rooms[NumOfRooms - 1].Width / 2;
        int y = rooms[NumOfRooms - 1].YPos + rooms[NumOfRooms - 1].Height / 2;
        Vector2 pos = new Vector2(x + 0.5f, y + 0.5f);
        GameObject master = new GameObject
        {
            name = "Boss"
        };
        Instantiate(boss, pos, Quaternion.identity, master.transform);
    }

    private void PrintAltarsInGame()
    {
        GameObject master = new GameObject
        {
            name = "Altars"
        };
        positionsToSpawnThePlayers.ForEach(pos => Instantiate(altar, pos, Quaternion.identity, master.transform));
    }

    private void PrintPlayersInGame()
    {
        Vector2 pos = positionsToSpawnThePlayers[Random.Range(0, positionsToSpawnThePlayers.Count)];
        GameObject master = new GameObject
        {
            name = "Players"
        };
        Instantiate(player, pos + new Vector2(0, 0.25f), Quaternion.identity, master.transform);
    }
}