﻿//-----------------------------------------------------------------------
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
    private string name = string.Empty;

    /// <summary>The items</summary>
    [SerializeField]
    private List<Item> items = null;

    /// <summary>The enemys</summary>
    [SerializeField]
    private List<Item> enemys = null;

    /// <summary>The pets</summary>
    [SerializeField]
    private List<Item> pets = null;

    /// <summary>The special floors</summary>
    [SerializeField]
    private List<Item> specialFloors = null;

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
        this.floorCenter = null;
        this.wallDown = null;
        this.wallLeft = null;
        this.wallRight = null;
        this.wallTop = null;
        this.cornerLeftDown = null;
        this.cornerRightDown = null;
        this.cornerRightUp = null;
        this.cornerLeftUp = null;
        this.cornerInternalLeftDown = null;
        this.cornerInternalLeftUp = null;
        this.cornerInternalRightDown = null;
        this.cornerInternalRightUp = null;
    }

    /// <summary>Selects the sprite</summary>
    /// <param name="sprite">The sprite</param>
    /// <returns>The sprite selected</returns>
    public GameObject SelectSprite(int sprite) 
    {
        switch (sprite) 
        {
            case 1:
                return this.floorCenter;
            case 2:
                return this.wallDown;
            case 3:
                return this.wallLeft;
            case 4:
                return this.wallRight;
            case 5:
                return this.wallTop;
            case 6:
                return this.cornerLeftDown;
            case 7:
                return this.cornerRightDown;
            case 8:
                return this.cornerLeftUp;
            case 9:
                return this.cornerRightUp;
            case 10:
                return this.cornerInternalLeftDown;
            case 11:
                return this.cornerInternalRightDown;
            case 12:
                return this.cornerInternalRightUp;
            case 13:
                return this.cornerInternalLeftUp;
            default:
                return null;
        }
    }

    /// <summary>Loads the sprites</summary>
    public void LoadSprites() 
    {
        this.floorCenter = (GameObject)Resources.Load("Dungeons/" + this.name + "/" + "Floor");

        this.wallDown = (GameObject)Resources.Load("Dungeons/" + this.name + "/" + "WallDown");
        this.wallLeft = (GameObject)Resources.Load("Dungeons/" + this.name + "/" + "WallLeft");
        this.wallRight = (GameObject)Resources.Load("Dungeons/" + this.name + "/" + "WallRigth");
        this.wallTop = (GameObject)Resources.Load("Dungeons/" + this.name + "/" + "WallTop");

        this.cornerLeftDown = (GameObject)Resources.Load("Dungeons/" + this.name + "/" + "CornerLD");
        this.cornerLeftUp = (GameObject)Resources.Load("Dungeons/" + this.name + "/" + "CornerLU");
        this.cornerRightDown = (GameObject)Resources.Load("Dungeons/" + this.name + "/" + "CornerRD");
        this.cornerRightUp = (GameObject)Resources.Load("Dungeons/" + this.name + "/" + "CornerRU");

        this.cornerInternalLeftDown = (GameObject)Resources.Load("Dungeons/" + this.name + "/" + "CornerILD");
        this.cornerInternalLeftUp = (GameObject)Resources.Load("Dungeons/" + this.name + "/" + "CornerILU");
        this.cornerInternalRightDown = (GameObject)Resources.Load("Dungeons/" + this.name + "/" + "CornerIRD");
        this.cornerInternalRightUp = (GameObject)Resources.Load("Dungeons/" + this.name + "/" + "CornerIRU");
    }

    /// <summary>Gets the name</summary>
    /// <returns>Return a string with the name of the style dungeon</returns>
    public string GetName() 
    {
        return this.name;
    }

    /// <summary>Gets the items.</summary>
    /// <returns>Return the items</returns>
    public List<Item> GetItems() 
    {
        return this.items;
    }

    /// <summary>Gets the enemys.</summary>
    /// <returns>Return the enemys</returns>
    public List<Item> GetEnemys()
    {
        return this.enemys;
    }

    /// <summary>Gets the pets.</summary>
    /// <returns>Return the pets</returns>
    public List<Item> GetPets()
    {
        return this.pets;
    }

    /// <summary>Gets the floors.</summary>
    /// <returns>Return floors</returns>
    public List<Item> GetFloors()
    {
        return this.specialFloors;
    }
}