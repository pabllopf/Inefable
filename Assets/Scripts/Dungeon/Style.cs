//-----------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Style.cs" company="UnMedioStudio">Open Source</copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

/// <summary>Define and load a style to the dungeon</summary>
[System.Serializable]
public class Style
{
    /// <summary>The name</summary>
    [SerializeField]
    private readonly string name = string.Empty;

    /// <summary>The items</summary>
    [SerializeField]
    private readonly List<Item> items = null;

    /// <summary>The enemys</summary>
    [SerializeField]
    private readonly List<Item> enemys = null;

    /// <summary>The pets</summary>
    [SerializeField]
    private readonly List<Item> pets = null;

    /// <summary>The special floors</summary>
    [SerializeField]
    private readonly List<Item> floors = null;

    /// <summary>The special floors</summary>
    [SerializeField]
    private readonly List<Item> lights = null;

    /// <summary>The floor center</summary>
    private GameObject floorCenter;

    /// <summary>The wall down</summary>
    private GameObject wallDown;

    /// <summary>The wall left</summary>
    private GameObject wallLeft;

    /// <summary>The wall right</summary>
    private GameObject wallRight;

    /// <summary>The wall top</summary>
    private GameObject wallTop;

    /// <summary>The corner left down</summary>
    private GameObject cornerLeftDown;

    /// <summary>The corner right down</summary>
    private GameObject cornerRightDown;

    /// <summary>The corner right up</summary>
    private GameObject cornerRightUp;

    /// <summary>The corner left up</summary>
    private GameObject cornerLeftUp;

    /// <summary>The corner internal left down</summary>
    private GameObject cornerInternalLeftDown;

    /// <summary>The corner internal left up</summary>
    private GameObject cornerInternalLeftUp;

    /// <summary>The corner internal right down</summary>
    private GameObject cornerInternalRightDown;

    /// <summary>The corner internal right up</summary>
    private GameObject cornerInternalRightUp;

    /// <summary>Initializes a new instance of the <see cref="Style"/> class</summary>
    public Style()
    {
        floorCenter = null;
        wallDown = null;
        wallLeft = null;
        wallRight = null;
        wallTop = null;
        cornerLeftDown = null;
        cornerRightDown = null;
        cornerRightUp = null;
        cornerLeftUp = null;
        cornerInternalLeftDown = null;
        cornerInternalLeftUp = null;
        cornerInternalRightDown = null;
        cornerInternalRightUp = null;
    }

    /// <summary>Selects the sprite</summary>
    /// <param name="sprite">The sprite</param>
    /// <returns>The sprite selected</returns>
    public GameObject SelectSprite(int sprite)
    {
        switch (sprite)
        {
            case 1:
                return floorCenter;
            case 2:
                return wallDown;
            case 3:
                return wallLeft;
            case 4:
                return wallRight;
            case 5:
                return wallTop;
            case 6:
                return cornerLeftDown;
            case 7:
                return cornerRightDown;
            case 8:
                return cornerLeftUp;
            case 9:
                return cornerRightUp;
            case 10:
                return cornerInternalLeftDown;
            case 11:
                return cornerInternalRightDown;
            case 12:
                return cornerInternalRightUp;
            case 13:
                return cornerInternalLeftUp;
            default:
                return null;
        }
    }

    /// <summary>Loads the sprites</summary>
    public void LoadSprites()
    {
        floorCenter = (GameObject)Resources.Load("Dungeons/" + name + "/" + "Floor");

        wallDown = (GameObject)Resources.Load("Dungeons/" + name + "/" + "WallDown");
        wallLeft = (GameObject)Resources.Load("Dungeons/" + name + "/" + "WallLeft");
        wallRight = (GameObject)Resources.Load("Dungeons/" + name + "/" + "WallRigth");
        wallTop = (GameObject)Resources.Load("Dungeons/" + name + "/" + "WallTop");

        cornerLeftDown = (GameObject)Resources.Load("Dungeons/" + name + "/" + "CornerLD");
        cornerLeftUp = (GameObject)Resources.Load("Dungeons/" + name + "/" + "CornerLU");
        cornerRightDown = (GameObject)Resources.Load("Dungeons/" + name + "/" + "CornerRD");
        cornerRightUp = (GameObject)Resources.Load("Dungeons/" + name + "/" + "CornerRU");

        cornerInternalLeftDown = (GameObject)Resources.Load("Dungeons/" + name + "/" + "CornerILD");
        cornerInternalLeftUp = (GameObject)Resources.Load("Dungeons/" + name + "/" + "CornerILU");
        cornerInternalRightDown = (GameObject)Resources.Load("Dungeons/" + name + "/" + "CornerIRD");
        cornerInternalRightUp = (GameObject)Resources.Load("Dungeons/" + name + "/" + "CornerIRU");
    }

    /// <summary>Gets the name</summary>
    /// <returns>Return a string with the name of the style dungeon</returns>
    public string GetName()
    {
        return name;
    }

    /// <summary>Gets the items.</summary>
    /// <returns>Return the items</returns>
    public List<Item> GetItems()
    {
        return items;
    }

    /// <summary>Gets the enemys.</summary>
    /// <returns>Return the enemys</returns>
    public List<Item> GetEnemys()
    {
        return enemys;
    }

    /// <summary>Gets the pets.</summary>
    /// <returns>Return the pets</returns>
    public List<Item> GetPets()
    {
        return pets;
    }

    /// <summary>Gets the floors.</summary>
    /// <returns>Return floors</returns>
    public List<Item> GetFloors()
    {
        return floors;
    }

    /// <summary>Gets the lights.</summary>
    /// <returns></returns>
    public List<Item> GetLights()
    {
        return lights;
    }
}
