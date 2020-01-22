//-----------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Corridor.cs" company="UnMedioStudio">Open Source</copyright>
//-----------------------------------------------------------------------
using UnityEngine;

/// <summary>Class that define a Corridor of a Dungeon</summary>
public class Corridor
{
    /// <summary>Initializes a new instance of the <see cref="Corridor"/> class.</summary>
    public Corridor()
    {
        this.XPos = 0;
        this.YPos = 0;
        this.Width = 0;
        this.Height = 0;
        this.Direction = Direction.North;
    }

    /// <summary>Initializes a new instance of the <see cref="Corridor"/> class.</summary>
    /// <param name="xPos">The x position.</param>
    /// <param name="yPos">The y position.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="direction">The direction.</param>
    public Corridor(int xPos, int yPos, int width, int height, Direction direction)
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

    /// <summary>Gets the end position x.</summary>
    /// <value>The end position x.</value>
    public int EndPositionX =>
        (this.GoNorth || this.GoSouth) ? this.XPos :
        this.GoEast ? this.XPos + this.Height - 1 :
        this.XPos - this.Height + 1;

    /// <summary>Gets the end position y.</summary>
    /// <value>The end position y.</value>
    public int EndPositionY =>
        (this.GoEast || this.GoWest) ? this.YPos :
        this.GoNorth ? this.YPos + this.Height - 1 :
        this.YPos - this.Height + 1;

    /// <summary>Sets up.</summary>
    /// <param name="room">The room.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="roomWidth">Width of the room.</param>
    /// <param name="roomHeight">Height of the room.</param>
    /// <param name="columns">The columns.</param>
    /// <param name="rows">The rows.</param>
    /// <param name="isFirst">if set to <c>true</c> [is first].</param>
    /// <returns>Return a corridor</returns>
    public static Corridor SetUp(Room room, int width, int height, int roomWidth, int roomHeight, int columns, int rows, bool isFirst)
    {
        Direction direction = (Direction)Random.Range(0, 4);
        Direction oppositeDirection = (Direction)(((int)room.Direction + 2) % 4);

        direction = (!isFirst && direction == oppositeDirection) ? (Direction)((int)direction++ % 4) : direction;

        int xPos = (direction == Direction.North) ? Random.Range(room.XPos, room.XPos + room.Width - 1) :
            (direction == Direction.South) ? Random.Range(room.XPos, room.XPos + room.Width) :
            (direction == Direction.East) ? room.XPos + room.Width : 
            room.XPos;

        int yPos = (direction == Direction.East) ? Random.Range(room.YPos, room.YPos + room.Height - 1) :
            (direction == Direction.West) ? Random.Range(room.YPos, room.YPos + room.Height) :
            (direction == Direction.North) ? room.YPos + room.Height :
             room.YPos;

        int maxLength = (direction == Direction.North) ? rows - yPos - roomHeight :
            (direction == Direction.East) ? columns - xPos - roomWidth :
            (direction == Direction.South) ? yPos - roomHeight : 
            xPos - roomWidth;

        height = Mathf.Clamp(height, 1, maxLength);
        width = Mathf.Clamp(width, 1, maxLength);

        return new Corridor(xPos, yPos, width, height, direction);
    }
}