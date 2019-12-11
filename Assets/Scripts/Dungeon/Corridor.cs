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
    /// <summary>The start x position</summary>
    private int startXPos;

    /// <summary>The start y position</summary>
    private int startYPos;

    /// <summary>The width</summary>
    private int width;

    /// <summary>The height</summary>
    private int height;

    /// <summary>The direction</summary>
    private Direction direction;

    /// <summary>Gets the end position x.</summary>
    /// <value>The end position x.</value>
    public int EndPositionX
    {
        get
        {
            if (this.direction == Direction.North || this.direction == Direction.South) 
            {
                return this.startXPos;
            }

            if (this.direction == Direction.East)
            {
                return this.startXPos + this.height - 1;
            }

            return this.startXPos - this.height + 1;
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
                return this.startYPos;
            }

            if (this.direction == Direction.North)
            {
                return this.startYPos + this.height - 1;
            }

            return this.startYPos - this.height + 1;
        }
    }

    /// <summary>Setups the corridor.</summary>
    /// <param name="room">The room.</param>
    /// <param name="width">The width.</param>
    /// <param name="length">The length.</param>
    /// <param name="roomWidth">Width of the room.</param>
    /// <param name="roomHeight">Height of the room.</param>
    /// <param name="columns">The columns.</param>
    /// <param name="rows">The rows.</param>
    /// <param name="firstCorridor">if set to <c>true</c> [first corridor].</param>
    public void SetupCorridor(Room room, int width, int length, int roomWidth, int roomHeight, int columns, int rows, bool firstCorridor)
    {
        this.direction = (Direction)Random.Range(0, 4);
        Direction oppositeDirection = (Direction)(((int)room.GetDirection() + 2) % 4);
        
        if (!firstCorridor && this.direction == oppositeDirection)
        {
            int directionInt = (int)this.direction;
            directionInt++;
            directionInt = directionInt % 4;
            this.direction = (Direction)directionInt;
        }

        this.height = length;
        this.width = width;

        int maxLength = length;

        switch (this.direction)
        {
            case Direction.North:
                this.startXPos = Random.Range(room.GetXPos(), room.GetXPos() + room.GetWidth() - 1);
                this.startYPos = room.GetYPos() + room.GetHeight();
                maxLength = rows - this.startYPos - roomHeight;
                break;

            case Direction.East:
                this.startXPos = room.GetXPos() + room.GetWidth();
                this.startYPos = Random.Range(room.GetYPos(), room.GetYPos() + room.GetHeight() - 1);
                maxLength = columns - this.startXPos - roomWidth;
                break;

            case Direction.South:
                this.startXPos = Random.Range(room.GetXPos(), room.GetXPos() + room.GetWidth());
                this.startYPos = room.GetYPos();
                maxLength = this.startYPos - roomHeight;
                break;

            case Direction.West:
                this.startXPos = room.GetXPos();
                this.startYPos = Random.Range(room.GetYPos(), room.GetYPos() + room.GetHeight());
                maxLength = this.startXPos - roomWidth;
                break;
        }

        this.height = Mathf.Clamp(this.height, 1, maxLength);
        this.width = Mathf.Clamp(this.width, 1, maxLength);
    }

    /// <summary>Gets the start x position.</summary>
    /// <returns>The start x position</returns>
    public int GetStartXPos() 
    {
        return this.startXPos;
    }

    /// <summary>Gets the start y position.</summary>
    /// <returns>The start y position</returns>
    public int GetStartYPos()
    {
        return this.startYPos;
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
