//-----------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Corridor.cs" company="UnMedioStudio">Open Source</copyright>
//-----------------------------------------------------------------------
using UnityEngine;

/// <summary>Indicate the direction of a corridor</summary>
public enum Direction
{
    /// <summary>The north</summary>
    North,

    /// <summary>The east</summary>
    East,

    /// <summary>The south</summary>
    South,

    /// <summary>The west</summary>
    West
}

/// <summary>Class that define a Corridor of a Dungeon</summary>
public class Corridor
{
    /// <summary>The x position</summary>
    private readonly int xPos;

    /// <summary>The y position</summary>
    private readonly int yPos;

    /// <summary>The width</summary>
    private readonly int width;

    /// <summary>The height</summary>
    private readonly int height;

    /// <summary>The direction</summary>
    private readonly Direction direction;

    /// <summary>Initializes a new instance of the <see cref="Corridor"/> class.</summary>
    public Corridor() 
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Corridor"/> class.</summary>
    /// <param name="xPos">The x position.</param>
    /// <param name="yPos">The y position.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="direction">The direction.</param>
    public Corridor(int xPos, int yPos, int width, int height, Direction direction)
    {
        this.xPos = xPos;
        this.yPos = yPos;
        this.width = width;
        this.height = height;
        this.direction = direction;
    }

    /// <summary>Gets the x position.</summary>
    /// <value>The x position.</value>
    public int XPos => this.xPos;

    /// <summary>Gets the y position.</summary>
    /// <value>The y position.</value>
    public int YPos => this.yPos;

    /// <summary>Gets the width.</summary>
    /// <value>The width.</value>
    public int Width => this.width;

    /// <summary>Gets the height.</summary>
    /// <value>The height.</value>
    public int Height => this.height;

    /// <summary>Gets the position.</summary>
    /// <value>The position.</value>
    public Vector2 Position => new Vector2(this.xPos, this.yPos);

    /// <summary>Gets the direction.</summary>
    /// <value>The direction.</value>
    public Direction Direction => this.direction;

    /// <summary>Gets the end position x.</summary>
    /// <value>The end position x.</value>
    public int EndPositionX
    {
        get
        {
            if (this.direction == Direction.North || this.direction == Direction.South) 
            {
                return this.xPos;
            }

            if (this.direction == Direction.East)
            {
                return this.xPos + this.height - 1;
            }

            return this.xPos - this.height + 1;
        }
    }

    /// <summary>Gets the end position y.</summary>
    /// <value>The end position y.</value>
    public int EndPositionY
    {
        get
        {
            if (this.direction == Direction.East || this.direction == Direction.West)
            {
                return this.yPos;
            }

            if (this.direction == Direction.North)
            {
                return this.yPos + this.height - 1;
            }

            return this.yPos - this.height + 1;
        }
    }

    /// <summary>Sets up.</summary>
    /// <param name="room">The room.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="roomWidth">Width of the room.</param>
    /// <param name="roomHeight">Height of the room.</param>
    /// <param name="columns">The columns.</param>
    /// <param name="rows">The rows.</param>
    /// <param name="firstCorridor">if set to <c>true</c> [first corridor].</param>
    /// <returns>Return the corridor</returns>
    public static Corridor SetUp(Room room, int width, int height, int roomWidth, int roomHeight, int columns, int rows, bool firstCorridor)
    {
        int xPos = 0;
        int yPos = 0;

        Direction direction = (Direction)Random.Range(0, 4);
        Direction oppositeDirection = (Direction)(((int)room.Direction + 2) % 4);
        
        if (!firstCorridor && direction == oppositeDirection)
        {
            int directionInt = (int)direction;
            directionInt++;
            directionInt = directionInt % 4;
            direction = (Direction)directionInt;
        }

        int maxLength = height;

        switch (direction)
        {
            case Direction.North:
                xPos = Random.Range(room.XPos, room.XPos + room.Width - 1);
                yPos = room.YPos + room.Height;
                maxLength = rows - yPos - roomHeight;
                break;

            case Direction.East:
                xPos = room.XPos + room.Width;
                yPos = Random.Range(room.YPos, room.YPos + room.Height - 1);
                maxLength = columns - xPos - roomWidth;
                break;

            case Direction.South:
                xPos = Random.Range(room.XPos, room.XPos + room.Width);
                yPos = room.YPos;
                maxLength = yPos - roomHeight;
                break;

            case Direction.West:
                xPos = room.XPos;
                yPos = Random.Range(room.YPos, room.YPos + room.Height);
                maxLength = xPos - roomWidth;
                break;
        }

        height = Mathf.Clamp(height, 1, maxLength);
        width = Mathf.Clamp(width, 1, maxLength);

        return new Corridor(xPos, yPos, width, height, direction);
    }
}