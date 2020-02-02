//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="NewDungeon.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>Generate a dungeon</summary>
public class NewDungeon : MonoBehaviour
{
    private const int BoardWidth = 750;
    private const int BoardHeight = 750;

    private const int NumOfRooms = 15;

    private const int FirstRoomWidth = 10;
    private const int FirstRoomHeight = 10;

    private const int RoomWidth = 8;
    private const int RoomHeight = 8;

    private const int LastRoomWidth = 10;
    private const int LastRoomHeight = 10;

    private const int CorridorWidth = 4;
    private const int CorridorHeight = 4;

    private int[,] board = new int[BoardWidth, BoardHeight];
    private List<NewRoom> rooms = new List<NewRoom>();
    private List<NewCorridor> corridors = new List<NewCorridor>();

    [SerializeField]
    private GameObject player = null;

    [SerializeField]
    private GameObject altar = null;

    [SerializeField]
    private GameObject boss = null;

    private List<Vector2> positionsToSpawnThePlayers = new List<Vector2>();

    [SerializeField]
    private List<Style> dungeons = null;
    private Style style = null;

    private void Start()
    {
        this.SetUpDungeonStyle();

        this.CreateRooms();
        this.CreateCorridors();

        this.SetUpFirstRoom();
        this.SetUpRoomsAndCorridors();
        

        this.GetPositionsToSpawn();

        this.PrintRoomsInBoard();
        this.PrintCorridorsInBoard();

        this.SetUpTheLastRoom();

        this.PrintRoomsInBoard();
        this.PrintCorridorsInBoard();

        this.PrintWallsInBoard();
        this.PrintOuterCornersInBoard();
        this.PrintInnerCornersInBoard();

        this.PrintBoardInGame();

        this.SpawnListOf(this.style.GetLights());

        this.PrintTheBoss();
        
        this.PrintAltarsInGame();
        this.PrintPlayersInGame();
    }

    /// <summary>Set up dungeon style.</summary>
    private void SetUpDungeonStyle()
    {
        this.style = this.dungeons[Random.Range(0, this.dungeons.Count)];
        this.style.LoadSprites();
    }

    /// <summary>Creates the rooms.</summary>
    private void CreateRooms() => this.rooms.AddRange(new NewRoom[NumOfRooms]);

    /// <summary>Creates the corridors.</summary>
    private void CreateCorridors() => this.corridors.AddRange(new NewCorridor[rooms.Count - 1]);

    private void SetUpFirstRoom() 
    {
        this.rooms[0] = NewRoom.SetUpFirstRoom(BoardWidth / 2, BoardHeight / 2, FirstRoomWidth, FirstRoomHeight);
        this.corridors[0] = NewCorridor.SetUpFirstCorridor(CorridorWidth, CorridorHeight, this.rooms[0]);
    }

    private void SetUpRoomsAndCorridors() 
    {
        for (int i = 1; i < this.rooms.Count; i++)
        {
            this.rooms[i] = NewRoom.SetUp(RoomWidth, RoomHeight, this.corridors[i - 1]);

            if (i < this.corridors.Count)
            {
                this.corridors[i] = NewCorridor.SetUp(CorridorWidth, CorridorHeight, this.rooms[i]);
            }
        }
    }

    private void SetUpTheLastRoom() 
    {
        int i = NumOfRooms - 1;
        this.rooms[i] = NewRoom.SetUp(LastRoomWidth, LastRoomHeight, this.corridors[i - 1]);


        while (board[this.rooms[i].XPos, this.rooms[i].YPos] == 1 && board[this.rooms[i].XPos + this.rooms[i].Width, this.rooms[i].YPos + this.rooms[i].Height] == 1) 
        {
            this.corridors[i - 1] = NewCorridor.SetUp(CorridorWidth, CorridorHeight, this.rooms[i - 1]);
            this.rooms[i] = NewRoom.SetUp(LastRoomWidth, LastRoomHeight, this.corridors[i - 1]);
        }
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
        foreach (NewRoom room in this.rooms)
        {
            for (int x = room.XPos; x < room.XPos + room.Width; x++)
            {
                for (int y = room.YPos; y < room.YPos + room.Height; y++)
                {
                    this.board[x, y] = (this.board[x, y] == 0) ? 1 : this.board[x, y];
                }
            }
        }
    }

    private void PrintCorridorsInBoard()
    {
        foreach (NewCorridor corridor in this.corridors)
        {
            for (int x = corridor.XPos; x < corridor.XPos + corridor.Width; x++)
            {
                for (int y = corridor.YPos; y < corridor.YPos + corridor.Height; y++)
                {
                    this.board[x, y] = (this.board[x, y] == 0) ? 1 : this.board[x, y];
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
                this.board[x, y] = (this.board[x, y] == 1 && this.board[x, y - 1] == 0) ? 2 :    // Wall Top
                                   (this.board[x, y] == 1 && this.board[x - 1, y] == 0) ? 3 :    // Wall Left    
                                   (this.board[x, y] == 1 && this.board[x + 1, y] == 0) ? 4 :    // Wall Right
                                   (this.board[x, y] == 1 && this.board[x, y + 1] == 0) ? 5 :    // Wall Down
                                   this.board[x, y];
            }
        }
    }

    private void PrintOuterCornersInBoard()
    {
        for (int x = 0; x < BoardWidth; x++)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                this.board[x, y] = (this.board[x, y] != 0 && this.board[x - 1, y] == 0 && this.board[x, y - 1] == 0) ? 6 :    // Corner Outer Top Left
                                   (this.board[x, y] != 0 && this.board[x + 1, y] == 0 && this.board[x, y - 1] == 0) ? 7 :    // Corner Outer Top Right   
                                   (this.board[x, y] != 0 && this.board[x - 1, y] == 0 && this.board[x, y + 1] == 0) ? 8 :    // Corner Outer Down Left
                                   (this.board[x, y] != 0 && this.board[x + 1, y] == 0 && this.board[x, y + 1] == 0) ? 9 :    // Corner Outer Down Right
                                   this.board[x, y];
            }
        }
    }

    private void PrintInnerCornersInBoard()
    {
        for (int x = 0; x < BoardWidth; x++)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                this.board[x, y] = (this.board[x, y] == 1 && this.board[x - 1, y - 1] == 0) ? 10 :   // Corner Inner Top Left
                                   (this.board[x, y] == 1 && this.board[x + 1, y - 1] == 0) ? 11 :   // Corner Inner Top Right
                                   (this.board[x, y] == 1 && this.board[x - 1, y + 1] == 0) ? 12 :   // Corner Inner Down Left
                                   (this.board[x, y] == 1 && this.board[x + 1, y + 1] == 0) ? 13 :   // Corner Inner Down Right
                                   this.board[x, y];
            }
        }
    }

    private void PrintBoardInGame()
    {
        for (int x = 0; x < BoardWidth; x++)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                if (this.board[x, y] != 0)
                {
                    MonoBehaviour.Instantiate(this.style.SelectSprite(this.board[x, y]), new Vector2(x, y), Quaternion.identity, this.transform);
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


    private void PrintTheBoss()
    {
        int x = this.rooms[NumOfRooms - 1].XPos + this.rooms[NumOfRooms - 1].Width / 2;
        int y = this.rooms[NumOfRooms - 1].YPos + this.rooms[NumOfRooms - 1].Height / 2;
        Vector2 pos = new Vector2(x + 0.5f, y + 0.5f);
        GameObject master = new GameObject();
        master.name = "Boss";
        Instantiate(this.boss, pos, Quaternion.identity, master.transform);
    }

    private void PrintAltarsInGame() 
    {
        GameObject master = new GameObject();
        master.name = "Altars";
        positionsToSpawnThePlayers.ForEach(pos => Instantiate(altar, pos, Quaternion.identity, master.transform));
    }

    private void PrintPlayersInGame() 
    {
        Vector2 pos = this.positionsToSpawnThePlayers[Random.Range(0, this.positionsToSpawnThePlayers.Count)];
        GameObject master = new GameObject();
        master.name = "Players";
        Instantiate(player, pos + new Vector2(0, 0.25f), Quaternion.identity, master.transform);
    }
}