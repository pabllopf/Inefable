//-----------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Room.cs" company="UnMedioStudio">Open Source</copyright>
//-----------------------------------------------------------------------
using UnityEngine;

/// <summary>Class that define a Room of a Dungeon</summary>
public class Room
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

    /// <summary>Initializes a new instance of the <see cref="Room"/> class.</summary>
    public Room()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Room"/> class.</summary>
    /// <param name="xPos">The x position.</param>
    /// <param name="yPos">The y position.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public Room(int xPos, int yPos, int width, int height) 
    {
        this.xPos = xPos;
        this.yPos = yPos;
        this.width = width;
        this.height = height;
    }

    /// <summary>Initializes a new instance of the <see cref="Room"/> class.</summary>
    /// <param name="xPos">The x position.</param>
    /// <param name="yPos">The y position.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="direction">The direction.</param>
    public Room(int xPos, int yPos, int width, int height, Direction direction)
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
        int xPos = 0;
        int yPos = 0;

        switch (corridor.Direction)
        {
            case Direction.North:
                height = Mathf.Clamp(height, 1, boardWidth - corridor.EndPositionY);
                yPos = corridor.EndPositionY;
                xPos = Random.Range(corridor.EndPositionX - width + 1, corridor.EndPositionX);
                xPos = Mathf.Clamp(xPos, 0, boardHeight - width);
                break;

            case Direction.East:
                width = Mathf.Clamp(width, 1, boardHeight - corridor.EndPositionX);
                xPos = corridor.EndPositionX;
                yPos = Random.Range(corridor.EndPositionY - height + 1, corridor.EndPositionY);
                yPos = Mathf.Clamp(yPos, 0, boardWidth - height);
                break;

            case Direction.South:
                height = Mathf.Clamp(height, 1, corridor.EndPositionY);
                yPos = corridor.EndPositionY - height + 1;
                xPos = Random.Range(corridor.EndPositionX - width + 1, corridor.EndPositionX);
                xPos = Mathf.Clamp(xPos, 0, boardHeight - width);
                break;

            case Direction.West:
                width = Mathf.Clamp(width, 1, corridor.EndPositionX);
                xPos = corridor.EndPositionX - width + 1;
                yPos = Random.Range(corridor.EndPositionY - height + 1, corridor.EndPositionY);
                yPos = Mathf.Clamp(yPos, 0, boardWidth - height);
                break;
        }

        return new Room(xPos, yPos, width, height, corridor.Direction);
    }
}