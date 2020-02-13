//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Inventory.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manage the inventory of the player.</summary>
public class Inventory : MonoBehaviour
{
    /// <summary>The show UI</summary>
    private const string ShowUI = "ShowUI";

    /// <summary>The slots</summary>
    private List<Image> inventory = new List<Image>();

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>Gets a value indicating whether this instance has space.</summary>
    /// <value>
    /// <c>true</c> if this instance has space; otherwise, <c>false</c>.</value>
    public bool HasSpace => inventory.Where(i => i.sprite == null).Any() ? true : false;

    /// <summary>Adds the item.</summary>
    /// <param name="item">The item.</param>
    public void AddItem(IItem item)
    {
        Image slot = inventory.Find(i => i.sprite == null);
        
        slot.gameObject.SetActive(true);

        slot.tag = item.GetName();
        slot.sprite = item.GetIcon();
        slot.GetComponentInParent<Button>().onClick.AddListener(() => { item.Action(gameObject); });

        Game.SaveVar(slot.tag).InFolder("Inventory").WithName("Slot" + inventory.IndexOf(slot));
        Audio.Play(Sound.TakeItem, audioSource);
    }

    /// <summary>Awakes this instance.</summary>
    private void Awake()
    {
        Game.LoadSettings();
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = transform.Find("Interface/Inventory").GetComponent<Animator>();

        inventory = new List<Image>
        {
            transform.Find("Interface/Inventory/Slot1/Item").GetComponent<Image>(),
            transform.Find("Interface/Inventory/Slot2/Item").GetComponent<Image>(),
            transform.Find("Interface/Inventory/Slot3/Item").GetComponent<Image>()
        };

        foreach (Image image in inventory) 
        {
            string stringloaded = Game.LoadVar("Slot" + inventory.IndexOf(image)).OfFolder("Inventory").String;
            if (!stringloaded.Equals("0")) 
            {
                image.tag = stringloaded;
                image.sprite = Resources.Load<Sprite>("Icons/" + image.tag);
                GameObject item = Resources.Load<GameObject>("Items/" + image.tag);
                image.GetComponentInParent<Button>().onClick.AddListener(() => { item.GetComponent<IItem>().Action(gameObject); });
            }
        }

        inventory.FindAll(i => i.sprite == null || i.CompareTag("0")).ForEach(i => i.gameObject.SetActive(false));
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (Settings.Current.Platform.Equals("Computer") || Settings.Current.Platform.Equals("Xbox"))
        {
            if (Input.GetKeyDown(KeyCode.I) || Input.GetButtonDown("ButtonX"))
            {
                if (animator.GetBool(ShowUI))
                {
                    animator.SetBool(ShowUI, false);
                    return;
                }
                else 
                {
                    animator.SetBool(ShowUI, true);
                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetAxisRaw("PadY") > 0)
            {
                UseItem(0);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetAxisRaw("PadX") < 0)
            {
                UseItem(1);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetAxisRaw("PadX") > 0)
            {
                UseItem(2);
                return;
            }
        }
    }

    /// <summary>Uses the item.</summary>
    /// <param name="position">The position.</param>
    private void UseItem(int position) 
    {
        if (inventory[position].sprite != null) 
        {
            inventory[position].GetComponentInParent<Button>().onClick.Invoke();
            inventory[position].sprite = null;
            inventory[position].gameObject.SetActive(false);

            Audio.Play(Sound.TakeItem, audioSource);

            Game.SaveVar("0").InFolder("Inventory").WithName("Slot" + position);
        }
    }
}