//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Inventory.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manage the inventory of the player.</summary>
public class Inventory : MonoBehaviour
{
    /// <summary>The open</summary>
    private const string Open = "Open";

    /// <summary>The inventory</summary>
    private Dictionary<int, string> inventory = new Dictionary<int, string>();

    /// <summary>The size</summary>
    private int size = 3;

    /// <summary>The UI animator</summary>
    private Animator uiAnimator = null;

    /// <summary>The slot 1</summary>
    private PressEffect slot1 = null;

    /// <summary>The slot 2</summary>
    private PressEffect slot2 = null;

    /// <summary>The slot 3</summary>
    private PressEffect slot3 = null;

    private List<Image> itemsIcon = null;

    /// <summary>The item1</summary>
    private Image item1 = null;

    /// <summary>The item2</summary>
    private Image item2 = null;

    /// <summary>The item3</summary>
    private Image item3 = null;

    /// <summary>The potion red</summary>
    [SerializeField]
    private Sprite PotionRed = null;

    /// <summary>The potion blue</summary>
    [SerializeField]
    private Sprite PotionBlue = null;

    /// <summary>The potion purple</summary>
    [SerializeField] 
    private Sprite PotionPurple = null;

    /// <summary>The potion yellow</summary>
    [SerializeField] 
    private Sprite PotionYellow = null;

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        this.CreateEmptyInventory();

        this.uiAnimator = this.transform.Find("Interface/Inventory").GetComponent<Animator>();

        this.slot1 = this.transform.Find("Interface/Inventory/Slot1/Image").GetComponent<PressEffect>();
        this.slot2 = this.transform.Find("Interface/Inventory/Slot2/Image").GetComponent<PressEffect>();
        this.slot3 = this.transform.Find("Interface/Inventory/Slot3/Image").GetComponent<PressEffect>();


        this.itemsIcon = new List<Image>();
        
        this.item1 = this.transform.Find("Interface/Inventory/Slot1/Item").GetComponent<Image>();
        this.item1.gameObject.SetActive(false);
        this.itemsIcon.Add(this.item1);

        this.item2 = this.transform.Find("Interface/Inventory/Slot2/Item").GetComponent<Image>();
        this.item2.gameObject.SetActive(false);
        this.itemsIcon.Add(this.item2);

        this.item3 = this.transform.Find("Interface/Inventory/Slot3/Item").GetComponent<Image>();
        this.item3.gameObject.SetActive(false);
        this.itemsIcon.Add(this.item3);
    }

    /// <summary>Updates this instance.</summary>
    public void Update()
    {
        if (Input.anyKey) 
        {
            if (Input.GetKeyDown(KeyCode.I)) 
            {
                if (this.uiAnimator.GetBool(Open))
                {
                    this.uiAnimator.SetBool(Open, false);
                    this.transform.Find("Interface/InventoryButton").GetComponent<PressEffect>().StartEffect();
                    return;
                }
                else 
                {
                    this.uiAnimator.SetBool(Open, true);
                    this.transform.Find("Interface/InventoryButton").GetComponent<PressEffect>().StopEffect();
                    this.CheckSlots();
                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UseItem(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UseItem(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UseItem(2);
            }
        }
    }

    /// <summary>Adds the item.</summary>
    /// <param name="obj">The object.</param>
    public void AddItem(string item, Sprite itemsIcon) 
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (this.inventory[i] == "")
            {
                this.inventory[i] = item;
                this.itemsIcon[i].gameObject.SetActive(true);
                this.itemsIcon[i].sprite = itemsIcon;
                this.CheckSlots();
                break;
            }
        }
    }
    /// <summary>Determines whether this instance has space.</summary>
    /// <returns>
    ///   <c>true</c> if this instance has space; otherwise, <c>false</c>.</returns>
    public bool HasSpace()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == "")
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>Checks the slots.</summary>
    private void CheckSlots() 
    {
        if (this.inventory[0] == "")
        {
            this.slot1.StopEffect();
        }
        else 
        {
            this.slot1.StartEffect();
        }

        if (this.inventory[1] == "")
        {
            this.slot2.StopEffect();
        }
        else
        {
            this.slot2.StartEffect();
        }

        if (this.inventory[2] == "")
        {
            this.slot3.StopEffect();
        }
        else
        {
            this.slot3.StartEffect();
        }
    }

    /// <summary>Creates the empty inventory.</summary>
    private void CreateEmptyInventory()
    {
        for (int i = 0; i < size; i++)
        {
            inventory.Add(i, "");
        }
    }

    /// <summary>Uses the item.</summary>
    /// <param name="position">The position.</param>
    private void UseItem(int position)
    {
        if (inventory[position] != "")
        {
            switch (inventory[position])
            {
                case "PotionRed":
                    this.GetComponent<Health>().FullHealth();
                    break;

                case "PotionBlue":
                    this.GetComponent<Health>().FullShield();
                    break;

                case "PotionPurple":
                    break;

                case "PotionYellow":
                    break;
            }
            inventory[position] = "";
            this.itemsIcon[position].gameObject.SetActive(false);
            this.CheckSlots();
        }
    }
}
