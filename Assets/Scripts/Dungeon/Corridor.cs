//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Corridor.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Generate a corridor of the dungeon.</summary>
public class Corridor
{
    public int XPos { get; }
    public int YPos { get; }
    public int Width { get; }
    public int Height { get; }
    public Direction Direction { get; }

    private Corridor(int xPos, int yPos, int width, int height, Direction direction)
    {
        XPos = xPos;
        YPos = yPos;
        Width = width;
        Height = height;
        Direction = direction;
    }

    public static Corridor SetUpFirstCorridor(int width, int height, Room room)
    {
        Direction direction = (Direction)Random.Range(0, 4);

        int xPos = 0;
        int yPos = 0;

        int xWidth = 0;
        int yHeight = 0;

        switch (direction)
        {
            case Direction.North:
                xPos = (room.XPos + Mathf.RoundToInt(room.Width / 2)) - Mathf.RoundToInt(width / 2);
                yPos = room.YPos + room.Height;

                xWidth = width;
                yHeight = height;
                break;
            case Direction.South:
                xPos = (room.XPos + Mathf.RoundToInt(room.Width / 2)) - Mathf.RoundToInt(width / 2);
                yPos = room.YPos - height;

                xWidth = width;
                yHeight = height;
                break;
            case Direction.East:
                xPos = room.XPos - height;
                yPos = (room.YPos + Mathf.RoundToInt(room.Height / 2)) - Mathf.RoundToInt(height / 2);

                xWidth = height;
                yHeight = width;
                break;
            case Direction.West:
                xPos = room.XPos + room.Width;
                yPos = (room.YPos + Mathf.RoundToInt(room.Height / 2)) - Mathf.RoundToInt(height / 2);

                xWidth = height;
                yHeight = width;
                break;
        }

        return new Corridor(xPos, yPos, xWidth, yHeight, direction);
    }

    public static Corridor SetUp(int width, int height, Room room)
    {
        Direction direction = (Direction)Random.Range(0, 4);
        Direction oppositeDirection = (Direction)(((int)room.Direction + 2) % 4);

        direction = (direction == oppositeDirection) ? (Direction)((int)direction++ % 4) : direction;

        int xPos = 0;
        int yPos = 0;

        int xWidth = 0;
        int yHeight = 0;

        switch (direction)
        {
            case Direction.North:
                xPos = (room.XPos + Mathf.RoundToInt(room.Width / 2)) - Mathf.RoundToInt(width / 2);
                yPos = room.YPos + room.Height;

                xWidth = width;
                yHeight = height;
                break;
            case Direction.South:
                xPos = (room.XPos + Mathf.RoundToInt(room.Width / 2)) - Mathf.RoundToInt(width / 2);
                yPos = room.YPos - height;

                xWidth = width;
                yHeight = height;
                break;
            case Direction.East:
                xPos = room.XPos - height;
                yPos = (room.YPos + Mathf.RoundToInt(room.Height / 2)) - Mathf.RoundToInt(height / 2);

                xWidth = height;
                yHeight = width;
                break;
            case Direction.West:
                xPos = room.XPos + room.Width;
                yPos = (room.YPos + Mathf.RoundToInt(room.Height / 2)) - Mathf.RoundToInt(height / 2);

                xWidth = height;
                yHeight = width;
                break;
        }


        return new Corridor(xPos, yPos, xWidth, yHeight, direction);
    }
}