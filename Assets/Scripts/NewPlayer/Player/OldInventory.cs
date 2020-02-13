//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Inventory.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manage the inventory of the player.</summary>
public class OldInventory : MonoBehaviour
{
    /// <summary>The open</summary>
    private const string Open = "Open";

    /// <summary>The inventory</summary>
    private List<Image> inventory;

    /// <summary>The press effects</summary>
    private List<PressEffect> pressEffects;

    /// <summary>The UI animator</summary>
    private Animator uiAnimator = null;

    /// <summary>The main button</summary>
    private GameObject mainButton = null;

    /// <summary>The slot 1 button</summary>
    private GameObject slot1Button = null;

    /// <summary>The slot 2 button</summary>
    private GameObject slot2Button = null;

    /// <summary>The slot 3 button</summary>
    private GameObject slot3Button = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

        uiAnimator = transform.Find("Interface/Inventory").GetComponent<Animator>();


        mainButton = transform.Find("Interface/CircleInventory").gameObject;
        slot1Button = transform.Find("Interface/Inventory/Slot1").gameObject;
        slot2Button = transform.Find("Interface/Inventory/Slot2").gameObject;
        slot3Button = transform.Find("Interface/Inventory/Slot3").gameObject;

        mainButton.GetComponent<Button>().onClick.AddListener(() => { ControlInventory(); });
        slot1Button.GetComponent<Button>().onClick.AddListener(() => { UseItem(0); });
        slot2Button.GetComponent<Button>().onClick.AddListener(() => { UseItem(1); });
        slot3Button.GetComponent<Button>().onClick.AddListener(() => { UseItem(2); });

        pressEffects = new List<PressEffect>
        {
            transform.Find("Interface/Inventory/Slot1/Image").GetComponent<PressEffect>(),
            transform.Find("Interface/Inventory/Slot2/Image").GetComponent<PressEffect>(),
            transform.Find("Interface/Inventory/Slot3/Image").GetComponent<PressEffect>()
        };

        inventory = new List<Image>
        {
            transform.Find("Interface/Inventory/Slot1/Item").GetComponent<Image>(),
            transform.Find("Interface/Inventory/Slot2/Item").GetComponent<Image>(),
            transform.Find("Interface/Inventory/Slot3/Item").GetComponent<Image>()
        };

        inventory[0].gameObject.SetActive(false);
        inventory[1].gameObject.SetActive(false);
        inventory[2].gameObject.SetActive(false);

        LoadInventory();
        CheckSlots();
    }

    /// <summary>Updates this instance.</summary>
    public void Update()
    {
        if (Settings.Current.Platform == "Computer")
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (uiAnimator.GetBool(Open))
                {
                    uiAnimator.SetBool(Open, false);
                    transform.Find("Interface/InventoryButton").GetComponent<PressEffect>().StartEffect();
                    return;
                }
                else
                {
                    uiAnimator.SetBool(Open, true);
                    transform.Find("Interface/InventoryButton").GetComponent<PressEffect>().StopEffect();
                    CheckSlots();
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

        if (Settings.Current.Platform == "Xbox")
        {
            if (Input.GetButtonDown("ButtonX"))
            {
                if (uiAnimator.GetBool(Open))
                {
                    uiAnimator.SetBool(Open, false);
                    transform.Find("Interface/InventoryButton").GetComponent<PressEffect>().StartEffect();
                    return;
                }
                else
                {
                    uiAnimator.SetBool(Open, true);
                    transform.Find("Interface/InventoryButton").GetComponent<PressEffect>().StopEffect();
                    CheckSlots();
                    return;
                }
            }

            if (Input.GetAxisRaw("PadY") > 0)
            {
                UseItem(0);
                return;
            }

            if (Input.GetAxisRaw("PadX") < 0)
            {
                UseItem(1);
                return;
            }

            if (Input.GetAxisRaw("PadX") > 0)
            {
                UseItem(2);
                return;
            }
        }
    }

    /// <summary>Adds the item.</summary>
    /// <param name="tag">The tag.</param>
    /// <param name="item">The item.</param>
    public void AddItem(string tag, Sprite item)
    {
        foreach (Image slot in inventory)
        {
            if (slot.sprite == null)
            {
                slot.gameObject.SetActive(true);
                slot.sprite = item;
                slot.tag = tag;

                CheckSlots();
                Audio.Play(Sound.TakeItem, audioSource);
                SaveInventory();
                break;
            }
        }
    }

    /// <summary>Determines whether this instance has space.</summary>
    /// <returns>
    /// <c>true</c> if this instance has space; otherwise, <c>false</c>.</returns>
    public bool HasSpace()
    {
        return inventory.Where(i => i.sprite == null).Any() ? true : false;
    }

    /// <summary>Controls the inventory.</summary>
    public void ControlInventory()
    {
        if (Settings.Current.Platform == "Mobile")
        {
            if (uiAnimator.GetBool(Open))
            {
                uiAnimator.SetBool(Open, false);
                return;
            }
            else
            {
                uiAnimator.SetBool(Open, true);
                return;
            }
        }
    }

    /// <summary>Uses the item.</summary>
    /// <param name="position">The position.</param>
    public void UseItem(int position)
    {
        if (inventory[position].sprite != null)
        {
            switch (inventory[position].tag)
            {
                case "PotionRed":
                    GetComponent<Health>().TreatFull();
                    break;

                case "PotionBlue":
                    GetComponent<Shield>().SetFull();
                    break;

                case "PotionPurple":
                    break;

                case "PotionYellow":
                    break;
            }

            inventory[position].sprite = null;
            inventory[position].tag = "Untagged";
            inventory[position].gameObject.SetActive(false);

            CheckSlots();
            Audio.Play(Sound.TakeItem, audioSource);
            SaveInventory();
        }
    }

    /// <summary>Checks the slots.</summary>
    private void CheckSlots()
    {
        if (Settings.Current.Platform == "Xbox" || Settings.Current.Platform == "Computer")
        {
            for (int i = 0; i < 3; i++)
            {
                if (inventory[i].sprite == null)
                {
                    pressEffects[i].StopEffect();
                }
                else
                {
                    pressEffects[i].StartEffect();
                }
            }
        }
    }

    /// <summary>Loads the inventory.</summary>
    private void LoadInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (Stats.Current.SpriteItem[i] != null)
            {
                inventory[i].gameObject.SetActive(true);
                inventory[i].sprite = Stats.Current.SpriteItem[i];
                inventory[i].tag = Stats.Current.TagItem[i];
            }
        }
    }

    /// <summary>Saves the inventory.</summary>
    private void SaveInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            Stats.Current.SpriteItem[i] = inventory[i].sprite;
            Stats.Current.TagItem[i] = inventory[i].tag;
        }

        //Game.SaveStats();
    }
}