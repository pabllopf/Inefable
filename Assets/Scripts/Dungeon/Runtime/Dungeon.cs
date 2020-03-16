//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Dungeon.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

/// <summary>Random dungeon generator.</summary>
public class Dungeon : MonoBehaviour
{
    #region Board Size

    /// <summary>The board width</summary>
    private const int BoardWidth = 500;

    /// <summary>The board height</summary>
    private const int BoardHeight = 500;

    /// <summary>The number of rooms</summary>
    private const int NumOfRooms = 20;

    #endregion

    #region Room Settings

    /// <summary>The first room width</summary>
    private const int FirstRoomWidth = 10;

    /// <summary>The first room height</summary>
    private const int FirstRoomHeight = 10;

    /// <summary>The room width</summary>
    private const int RoomWidth = 8;

    /// <summary>The room height</summary>
    private const int RoomHeight = 8;

    /// <summary>The boss room width</summary>
    private const int BossRoomWidth = 10;

    /// <summary>The boss room height</summary>
    private const int BossRoomHeight = 10;

    #endregion

    #region Corridor Settings
    /// <summary>The corridor width</summary>
    private const int CorridorWidth = 4;

    /// <summary>The corridor height</summary>
    private const int CorridorHeight = 4;

    #endregion

    #region Board Game

    /// <summary>The board</summary>
    private BoardBox[,] board = new BoardBox[BoardWidth, BoardHeight];

    /// <summary>The rooms</summary>
    private List<Room> rooms = new List<Room>();

    /// <summary>The corridors</summary>
    private List<Corridor> corridors = new List<Corridor>();

    #endregion

    /// <summary>The altar</summary>
    [SerializeField]
    private GameObject altar = null;

    #region Encapsulate Fields

    /// <summary>Gets or sets the board.</summary>
    /// <value>The board.</value>
    public BoardBox[,] Board { get => board; set => board = value; }

    /// <summary>Gets or sets the rooms.</summary>
    /// <value>The rooms.</value>
    public List<Room> Rooms { get => rooms; set => rooms = value; }

    /// <summary>Gets or sets the corridors.</summary>
    /// <value>The corridors.</value>
    public List<Corridor> Corridors { get => corridors; set => corridors = value; }

    /// <summary>Gets or sets the altar.</summary>
    /// <value>The altar.</value>
    public GameObject Altar { get => altar; set => altar = value; }

    /// <summary>Gets the random style.</summary>
    /// <value>The random style.</value>
    private Style RandomStyle => Resources.LoadAll<Style>("Dungeons")[Random.Range(0, Resources.LoadAll<Style>("Dungeons").Length)];

    #endregion

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        SetUpRoomsAndCorridors();

        ConfigInitialRoom();
        ConfigRoomsAndCorridors();

        CreateBoard();

        PrintDungeon(RandomStyle);
    }

    /// <summary>Sets up rooms and corridors.</summary>
    private void SetUpRoomsAndCorridors()
    {
        rooms.AddRange(new Room[NumOfRooms]);
        corridors.AddRange(new Corridor[rooms.Count - 1]);

        rooms[0] = Room.SetUpFirstRoom(BoardWidth / 2, BoardHeight / 2, FirstRoomWidth, FirstRoomHeight);
        corridors[0] = Corridor.SetUpFirstCorridor(CorridorWidth, CorridorHeight, rooms[0]);

        for (int index = 1; index < rooms.Count; index++)
        {
            rooms[index] = Room.SetUp(RoomWidth, RoomHeight, corridors[index - 1]);
            if (index < corridors.Count)
            {
                corridors[index] = Corridor.SetUp(CorridorWidth, CorridorHeight, rooms[index]);
            }
        }

        corridors[NumOfRooms - 2] = Corridor.SetUp(CorridorWidth, CorridorHeight, rooms[NumOfRooms - 2]);
        rooms[NumOfRooms - 1] = Room.SetUp(BossRoomWidth, BossRoomHeight, corridors[NumOfRooms - 2]);
    }

    /// <summary>Creates the rooms and corridors.</summary>
    private void ConfigRoomsAndCorridors()
    {
        rooms.ForEach(room =>
        {
            for (int x = room.XPos; x < room.XPos + room.Width; x++)
            {
                for (int y = room.YPos; y < room.YPos + room.Height; y++)
                {
                    board[x, y] = BoardBox.Floor;
                }
            }
        });

        corridors.ForEach(corridor =>
        {
            for (int x = corridor.XPos; x < corridor.XPos + corridor.Width; x++)
            {
                for (int y = corridor.YPos; y < corridor.YPos + corridor.Height; y++)
                {
                    board[x, y] = BoardBox.Floor;
                }
            }
        });
    }

    /// <summary>Creates the board.</summary>
    private void CreateBoard()
    {
        for (int x = 0; x < BoardWidth; x++)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                board[x, y] = (board[x, y].Equals(BoardBox.Floor) && board[x, y - 1].Equals(BoardBox.Empty)) ? BoardBox.WallDown : board[x, y];
                board[x, y] = (board[x, y].Equals(BoardBox.Floor) && board[x - 1, y].Equals(BoardBox.Empty)) ? BoardBox.WallLeft : board[x, y];
                board[x, y] = (board[x, y].Equals(BoardBox.Floor) && board[x + 1, y].Equals(BoardBox.Empty)) ? BoardBox.WallRight : board[x, y];
                board[x, y] = (board[x, y].Equals(BoardBox.Floor) && board[x, y + 1].Equals(BoardBox.Empty)) ? BoardBox.WallTop : board[x, y];

                board[x, y] = (!board[x, y].Equals(BoardBox.Empty) && board[x - 1, y].Equals(BoardBox.Empty) && board[x, y - 1].Equals(BoardBox.Empty)) ? BoardBox.CornerLeftDown : board[x, y];
                board[x, y] = (!board[x, y].Equals(BoardBox.Empty) && board[x + 1, y].Equals(BoardBox.Empty) && board[x, y - 1].Equals(BoardBox.Empty)) ? BoardBox.CornerRightDown : board[x, y];
                board[x, y] = (!board[x, y].Equals(BoardBox.Empty) && board[x - 1, y].Equals(BoardBox.Empty) && board[x, y + 1].Equals(BoardBox.Empty)) ? BoardBox.CornerLeftUp : board[x, y];
                board[x, y] = (!board[x, y].Equals(BoardBox.Empty) && board[x + 1, y].Equals(BoardBox.Empty) && board[x, y + 1].Equals(BoardBox.Empty)) ? BoardBox.CornerRightUp : board[x, y];

                board[x, y] = (board[x, y].Equals(BoardBox.Floor) && board[x - 1, y - 1].Equals(BoardBox.Empty)) ? BoardBox.CornerInternalLeftDown : board[x, y];
                board[x, y] = (board[x, y].Equals(BoardBox.Floor) && board[x + 1, y - 1].Equals(BoardBox.Empty)) ? BoardBox.CornerInternalRightDown : board[x, y];
                board[x, y] = (board[x, y].Equals(BoardBox.Floor) && board[x - 1, y + 1].Equals(BoardBox.Empty)) ? BoardBox.CornerInternalLeftUp : board[x, y];
                board[x, y] = (board[x, y].Equals(BoardBox.Floor) && board[x + 1, y + 1].Equals(BoardBox.Empty)) ? BoardBox.CornerInternalRightUp : board[x, y];
            }
        }
    }

    /// <summary>Configurations the initial room.</summary>
    private void ConfigInitialRoom()
    {
        Vector2 center = new Vector2((BoardWidth / 2) + (FirstRoomWidth / 2), (BoardHeight / 2) + (FirstRoomHeight / 2));

        GameObject master = new GameObject("Altar");

        Instantiate(altar, center + new Vector2(1.5f, 1.5f), Quaternion.identity, master.transform);
        Instantiate(altar, center + new Vector2(-1.5f, -1.5f), Quaternion.identity, master.transform);
        Instantiate(altar, center + new Vector2(-1.5f, 1.5f), Quaternion.identity, master.transform);
        Instantiate(altar, center + new Vector2(1.5f, -1.5f), Quaternion.identity, master.transform);
    }

    /// <summary>Prints the dungeon.</summary>
    /// <param name="style">The style.</param>
    private void PrintDungeon(Style style)
    {
        for (int x = 0; x < BoardWidth; x++)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                if (board[x, y] != BoardBox.Empty)
                {
                    Instantiate(style.GetTile(board[x, y]), new Vector2(x, y), Quaternion.identity, transform);
                }
            }
        }

        PrintDecoration(style);
    }
    /// <summary>Prints the items.</summary>
    private void PrintDecoration(Style style)
    {
        style.Decorations
            .FindAll(deco => (deco.MinToSpawn != 0 && deco.MaxToSpawn != 0 && deco.BoxToSpawn != BoardBox.Empty))
            .ForEach(deco =>
            {
                int quantity = Random.Range(deco.MinToSpawn, deco.MaxToSpawn);
                GameObject master = new GameObject(deco.Prefab.name + " (" + quantity + ")");

                while (quantity > 0)
                {
                    for (int x = 0; x < BoardWidth; x++)
                    {
                        for (int y = 0; y < BoardHeight; y++)
                        {
                            if (board[x, y] != BoardBox.Empty)
                            {
                                if (board[x, y] == deco.BoxToSpawn && Random.Range(0, 1000) == 1)
                                {
                                    board[x, y] = BoardBox.Empty;
                                    quantity--;

                                    Instantiate(deco.Prefab, new Vector2(x, y), Quaternion.identity, master.transform);
                                }
                            }
                        }
                    }
                }
            });
    }
}