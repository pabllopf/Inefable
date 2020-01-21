//-----------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Room.cs" company="UnMedioStudio">Open Source</copyright>
//-----------------------------------------------------------------------
using UnityEngine;

/// <summary>Class that define a Room of a Dungeon</summary>
public class Room
{
    /// <summary>Initializes a new instance of the <see cref="Room"/> class.</summary>
    public Room()
    {
        this.XPos = 0;
        this.YPos = 0;
        this.Width = 0;
        this.Height = 0;
        this.Direction = Direction.North;
    }

    /// <summary>Initializes a new instance of the <see cref="Room"/> class.</summary>
    /// <param name="xPos">The x position.</param>
    /// <param name="yPos">The y position.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public Room(int xPos, int yPos, int width, int height)
    {
        this.XPos = xPos;
        this.YPos = yPos;
        this.Width = width;
        this.Height = height;
    }

    /// <summary>Initializes a new instance of the <see cref="Room"/> class.</summary>
    /// <param name="xPos">The x position.</param>
    /// <param name="yPos">The y position.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="direction">The direction.</param>
    public Room(int xPos, int yPos, int width, int height, Direction direction)
    {
        this.XPos = xPos;
        this.YPos = yPos;
        this.Width = width;
        this.Height = height;
        this.Direction = direction;
    }

    /// <summary>Gets the x position.</summary>
    /// <value>The x position.</value>
    public int XPos { get; }

    /// <summary>Gets the y position.</summary>
    /// <value>The y position.</value>
    public int YPos { get; }

    /// <summary>Gets the width.</summary>
    /// <value>The width.</value>
    public int Width { get; }

    /// <summary>Gets the height.</summary>
    /// <value>The height.</value>
    public int Height { get; }

    /// <summary>Gets the position.</summary>
    /// <value>The position.</value>
    public Vector2 Position => new Vector2(this.XPos, this.YPos);

    /// <summary>Gets the direction.</summary>
    /// <value>The direction.</value>
    public Direction Direction { get; }

    /// <summary>Gets a value indicating whether [go north].</summary>
    /// <value>
    /// <c>true</c> if [go north]; otherwise, <c>false</c>.</value>
    public bool GoNorth => (this.Direction == Direction.North) ? true : false;

    /// <summary>Gets a value indicating whether [go east].</summary>
    /// <value>
    /// <c>true</c> if [go east]; otherwise, <c>false</c>.</value>
    public bool GoEast => (this.Direction == Direction.East) ? true : false;

    /// <summary>Gets a value indicating whether [go south].</summary>
    /// <value>
    /// <c>true</c> if [go south]; otherwise, <c>false</c>.</value>
    public bool GoSouth => (this.Direction == Direction.South) ? true : false;

    /// <summary>Gets a value indicating whether [go west].</summary>
    /// <value>
    /// <c>true</c> if [go west]; otherwise, <c>false</c>.</value>
    public bool GoWest => (this.Direction == Direction.West) ? true : false;

    /// <summary>Sets up.</summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="boardWidth">Width of the board.</param>
    /// <param name="boardHeight">Height of the board.</param>
    /// <returns>Return the first room</returns>
    public static Room SetUp(int width, int height, int boardWidth, int boardHeight)
    {
        int xPos = Mathf.RoundToInt((boardHeight / 2f) - (width / 2f));
        int yPos = Mathf.RoundToInt((boardWidth / 2f) - (height / 2f));
        return new Room(xPos, yPos, width, height);
    }

    /// <summary>Sets up.</summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="boardWidth">Width of the board.</param>
    /// <param name="boardHeight">Height of the board.</param>
    /// <param name="corridor">The corridor.</param>
    /// <returns>Return a room</returns>
    public static Room SetUp(int width, int height, int boardWidth, int boardHeight, Corridor corridor)
    {
        int xPos = (corridor.GoNorth || corridor.GoSouth) ? Mathf.Clamp(Random.Range(corridor.EndPositionX - width + 1, corridor.EndPositionX), 0, boardHeight - width) :
                corridor.GoWest ? corridor.EndPositionX - width + 1 :
                corridor.EndPositionX;

        int yPos = (corridor.GoEast || corridor.GoWest) ? Mathf.Clamp(Random.Range(corridor.EndPositionY - height + 1, corridor.EndPositionY), 0, boardWidth - height) :
                corridor.GoSouth ? corridor.EndPositionY - height + 1 :
                corridor.EndPositionY;

        width = (corridor.GoNorth || corridor.GoSouth) ? width :
                corridor.GoEast ? Mathf.Clamp(width, 1, boardHeight - corridor.EndPositionX) :
                Mathf.Clamp(width, 1, corridor.EndPositionX);

        height = (corridor.GoEast || corridor.GoWest) ? height :
               corridor.GoNorth ? Mathf.Clamp(height, 1, boardWidth - corridor.EndPositionY) :
               Mathf.Clamp(height, 1, corridor.EndPositionY);

        return new Room(xPos, yPos, width, height);
    }
}