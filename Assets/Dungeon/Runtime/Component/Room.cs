//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Room.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace DungeonGenerator.Component
{
    using DungeonGenerator.Enum;
    using UnityEngine;

    /// <summary>Generate a room of the dungeon.</summary>
    public class Room
    {
        public int XPos { get; }
        public int YPos { get; }
        public int Width { get; }
        public int Height { get; }

        public Direction Direction { get; }

        private Room(int xPos, int yPos, int width, int height)
        {
            XPos = xPos;
            YPos = yPos;
            Width = width;
            Height = height;
        }

        private Room(int xPos, int yPos, int width, int height, Direction direction)
        {
            XPos = xPos;
            YPos = yPos;
            Width = width;
            Height = height;
            Direction = direction;
        }

        public static Room SetUpFirstRoom(int xPos, int yPos, int width, int height)
        {
            return new Room(xPos, yPos, width, height);
        }

        public static Room SetUp(int width, int height, Corridor corridor)
        {
            Direction direction = corridor.Direction;

            int xPos = 0;
            int yPos = 0;

            int xWidth = 0;
            int yHeight = 0;

            switch (direction)
            {
                case Direction.North:
                    xPos = (corridor.XPos + Mathf.RoundToInt(corridor.Width / 2)) - Mathf.RoundToInt(width / 2);
                    yPos = corridor.YPos + corridor.Height;

                    xWidth = width;
                    yHeight = height;

                    break;

                case Direction.South:
                    xPos = (corridor.XPos + Mathf.RoundToInt(corridor.Width / 2)) - Mathf.RoundToInt(width / 2);
                    yPos = corridor.YPos - height;

                    xWidth = width;
                    yHeight = height;

                    break;

                case Direction.East:
                    xPos = corridor.XPos - height;
                    yPos = (corridor.YPos + Mathf.RoundToInt(corridor.Height / 2)) - Mathf.RoundToInt(height / 2);

                    xWidth = height;
                    yHeight = width;

                    break;

                case Direction.West:
                    xPos = corridor.XPos + corridor.Width;
                    yPos = (corridor.YPos + Mathf.RoundToInt(corridor.Height / 2)) - Mathf.RoundToInt(height / 2);

                    xWidth = height;
                    yHeight = width;
                    break;
            }

            return new Room(xPos, yPos, xWidth, yHeight, direction);
        }
    }
}