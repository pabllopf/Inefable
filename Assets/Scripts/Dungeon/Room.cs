//-----------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Room.cs" company="UnMedioStudio">Open Source</copyright>
//-----------------------------------------------------------------------
using UnityEngine;

/// <summary>Class that define a Room of a Dungeon</summary>
public class Room
{
    /// <summary>The x position</summary>
    private int xPos;

    /// <summary>The y position</summary>
    private int yPos;

    /// <summary>The width</summary>
    private int width;

    /// <summary>The height</summary>
    private int height;

    /// <summary>The direction</summary>
    private Direction direction;

    /// <summary>Setups the first room.</summary>
    /// <param name="roomWidth">Width of the room.</param>
    /// <param name="roomHeight">Height of the room.</param>
    /// <param name="boardWidth">Width of the board.</param>
    /// <param name="boardHeight">Height of the board.</param>
    public void SetupFirstRoom(int roomWidth, int roomHeight, int boardWidth, int boardHeight)
    {
        this.width = roomWidth;
        this.height = roomHeight;

        this.xPos = Mathf.RoundToInt((boardHeight / 2f) - (this.width / 2f));
        this.yPos = Mathf.RoundToInt((boardWidth / 2f) - (this.height / 2f));
    }

    /// <summary>Setups the room.</summary>
    /// <param name="roomWidth">Width of the room.</param>
    /// <param name="roomHeight">Height of the room.</param>
    /// <param name="boardWidth">Width of the board.</param>
    /// <param name="boardHeight">Height of the board.</param>
    /// <param name="corridor">The corridor.</param>
    public void SetupRoom(int roomWidth, int roomHeight, int boardWidth, int boardHeight, Corridor corridor)
    {
        this.width = roomWidth;
        this.height = roomHeight;
        this.direction = corridor.GetDirection();

        switch (corridor.GetDirection())
        {
            case Direction.North:
                this.height = Mathf.Clamp(this.height, 1, boardWidth - corridor.EndPositionY);
                this.yPos = corridor.EndPositionY;
                this.xPos = Random.Range(corridor.EndPositionX - this.width + 1, corridor.EndPositionX);
                this.xPos = Mathf.Clamp(this.xPos, 0, boardHeight - this.width);
                break;

            case Direction.East:
                this.width = Mathf.Clamp(this.width, 1, boardHeight - corridor.EndPositionX);
                this.xPos = corridor.EndPositionX;
                this.yPos = Random.Range(corridor.EndPositionY - this.height + 1, corridor.EndPositionY);
                this.yPos = Mathf.Clamp(this.yPos, 0, boardWidth - this.height);
                break;

            case Direction.South:
                this.height = Mathf.Clamp(this.height, 1, corridor.EndPositionY);
                this.yPos = corridor.EndPositionY - this.height + 1;
                this.xPos = Random.Range(corridor.EndPositionX - this.width + 1, corridor.EndPositionX);
                this.xPos = Mathf.Clamp(this.xPos, 0, boardHeight - this.width);
                break;

            case Direction.West:
                this.width = Mathf.Clamp(this.width, 1, corridor.EndPositionX);
                this.xPos = corridor.EndPositionX - this.width + 1;
                this.yPos = Random.Range(corridor.EndPositionY - this.height + 1, corridor.EndPositionY);
                this.yPos = Mathf.Clamp(this.yPos, 0, boardWidth - this.height);
                break;
        }
    }

    /// <summary>Gets the x position.</summary>
    /// <returns>The x position</returns>
    public int GetXPos() 
    {
        return this.xPos;
    }

    /// <summary>Gets the y position.</summary>
    /// <returns>The y position</returns>
    public int GetYPos()
    {
        return this.yPos;
    }

    /// <summary>Gets the width.</summary>
    /// <returns>The width</returns>
    public int GetWidth()
    {
        return this.width;
    }

    /// <summary>Gets the height.</summary>
    /// <returns>The height</returns>
    public int GetHeight()
    {
        return this.height;
    }

    /// <summary>Gets the direction.</summary>
    /// <returns>The direction</returns>
    public Direction GetDirection()
    {
        return this.direction;
    }
}