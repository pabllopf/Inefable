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

    /// <summary>The items icon</summary>
    private List<Image> itemsIcon = null;

    /// <summary>The item1</summary>
    private Image item1 = null;

    /// <summary>The item2</summary>
    private Image item2 = null;

    /// <summary>The item3</summary>
    private Image item3 = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>The take item</summary>
    [SerializeField]
    private AudioClip takeItem = null;

    /// <summary>The use item</summary>
    [SerializeField] 
    private AudioClip useItem = null;

    public void Awake()
    {
        Game.LoadStats();
    }

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        this.inventory = new Dictionary<int, string>();
        this.inventory[0] = "";
        this.inventory[1] = "";
        this.inventory[2] = "";

        this.audioSource = this.GetComponent<AudioSource>();

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

        this.LoadInventory();
        this.CheckSlots();
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
                this.UseItem(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                this.UseItem(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                this.UseItem(2);
            }
        }
    }

    /// <summary>Adds the item.</summary>
    /// <param name="item">The item.</param>
    /// <param name="itemsIcon">The items icon.</param>
    public void AddItem(string item, Sprite itemsIcon) 
    {
        for (int i = 0; i < this.inventory.Count; i++)
        {
            if (this.inventory[i] == string.Empty)
            {
                this.PlayClip(this.takeItem);
                this.inventory[i] = item;
                this.itemsIcon[i].gameObject.SetActive(true);
                this.itemsIcon[i].sprite = itemsIcon;
                this.CheckSlots();
                this.SaveInventory();
                break;
            }
        }
    }

    /// <summary>Determines whether this instance has space.</summary>
    /// <returns>
    /// <c>true</c> if this instance has space; otherwise, <c>false</c>.</returns>
    public bool HasSpace()
    {
        for (int i = 0; i < this.inventory.Count; i++)
        {
            if (this.inventory[i] == string.Empty)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>Checks the slots.</summary>
    private void CheckSlots() 
    {
        if (this.inventory[0] == string.Empty)
        {
            this.slot1.StopEffect();
        }
        else 
        {
            this.slot1.StartEffect();
        }

        if (this.inventory[1] == string.Empty)
        {
            this.slot2.StopEffect();
        }
        else
        {
            this.slot2.StartEffect();
        }

        if (this.inventory[2] == string.Empty)
        {
            this.slot3.StopEffect();
        }
        else
        {
            this.slot3.StartEffect();
        }
    }

    /// <summary>Uses the item.</summary>
    /// <param name="position">The position.</param>
    private void UseItem(int position)
    {
        if (this.inventory[position] != string.Empty)
        {
            switch (this.inventory[position])
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

            this.PlayClip(this.useItem);
            this.inventory[position] = string.Empty;
            this.itemsIcon[position].sprite = null;
            this.itemsIcon[position].gameObject.SetActive(false);
            this.CheckSlots();
            this.SaveInventory();
        }
    }

    private void LoadInventory() 
    {
        this.inventory[0] = Stats.Current.Slot1;
        this.inventory[1] = Stats.Current.Slot2;
        this.inventory[2] = Stats.Current.Slot3;

        if (Stats.Current.Icon1 != null) 
        {
            this.itemsIcon[0].gameObject.SetActive(true);
            this.itemsIcon[0].sprite = Stats.Current.Icon1;
        }

        if (Stats.Current.Icon2 != null)
        {
            this.itemsIcon[1].gameObject.SetActive(true);
            this.itemsIcon[1].sprite = Stats.Current.Icon2;
        }

        if (Stats.Current.Icon3 != null)
        {
            this.itemsIcon[2].gameObject.SetActive(true);
            this.itemsIcon[2].sprite = Stats.Current.Icon3;
        }
    }

    private void SaveInventory() 
    {
        Stats.Current.Slot1 = this.inventory[0];
        Stats.Current.Slot2 = this.inventory[1];
        Stats.Current.Slot3 = this.inventory[2];

        if (this.itemsIcon[0].sprite != null)
        {
            Stats.Current.Icon1 = this.itemsIcon[0].sprite;
        }
        else 
        {
            Stats.Current.Icon1 = null;
        }

        if (this.itemsIcon[1].sprite != null)
        {
            Stats.Current.Icon2 = this.itemsIcon[1].sprite;
        }
        else
        {
            Stats.Current.Icon2 = null;
        }

        if (this.itemsIcon[2].sprite != null)
        {
            Stats.Current.Icon3 = this.itemsIcon[2].sprite;
        }
        else
        {
            Stats.Current.Icon3 = null;
        }

        Game.SaveStats();
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip)
    {
        this.audioSource.clip = clip;
        this.audioSource.Play();
    }
}
