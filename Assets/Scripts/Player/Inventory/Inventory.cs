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

    /// <summary>The use item</summary>
    private const SoundClip UseItemSound = SoundClip.TakeCoin;

    /// <summary>The add item sound</summary>
    private const SoundClip AddItemSound = SoundClip.TakeCoin;

    /// <summary>The slots</summary>
    private List<Image> inventory = new List<Image>();

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>Gets a value indicating whether this instance has space.</summary>
    /// <value>
    /// <c>true</c> if this instance has space; otherwise, <c>false</c>.</value>
    public bool HasSpace => inventory.Any(slot => slot.sprite == null) ? true : false;

    /// <summary>Adds the item.</summary>
    /// <param name="item">The item.</param>
    public void AddItem(IItem item)
    {
        Image slot = inventory.Find(i => i.sprite == null);

        slot.gameObject.SetActive(true);

        slot.tag = item.GetName();
        slot.sprite = item.GetIcon();
        slot.GetComponentInParent<Button>().onClick.AddListener(() => { item.Action(gameObject); });

        Data.SaveVar(slot.tag).WithName("Slot" + inventory.IndexOf(slot)).InFolder("Inventory");
        Sound.Play(AddItemSound, audioSource);
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

        inventory.ForEach(i => LoadItem(i));
        inventory.FindAll(i => !i.sprite || i.CompareTag("0")).ForEach(i => i.gameObject.SetActive(false));
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (Settings.Current.Platform.Equals("Computer") || Settings.Current.Platform.Equals("Xbox"))
        {
            if (Input.GetKeyDown(KeyCode.I) || Input.GetButtonDown("ButtonX"))
            {
                animator.SetBool(ShowUI, animator.GetBool(ShowUI) ? false : true);
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

            Sound.Play(UseItemSound, audioSource);
            Data.SaveVar("0").WithName("Slot" + position).InFolder("Inventory");
        }
    }

    /// <summary>Loads the item.</summary>
    /// <param name="image">The image.</param>
    private void LoadItem(Image image)
    {
        string slot = Data.LoadVar("Slot" + inventory.IndexOf(image)).FromFolder("Inventory").String;
        if (!slot.Equals("0"))
        {
            image.tag = slot;
            image.sprite = Resources.Load<Sprite>("Icons/" + image.tag);
            image.GetComponentInParent<Button>().onClick.AddListener(() => { Resources.Load<GameObject>("Items/" + image.tag).GetComponent<IItem>().Action(gameObject); });
        }
    }
}