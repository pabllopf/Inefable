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
    private const Sound UseItemSound = Sound.TakeCoin;

    /// <summary>The add item sound</summary>
    private const Sound AddItemSound = Sound.TakeCoin;

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

    /// <summary>Gets or sets the animator.</summary>
    /// <value>The animator.</value>
    private Animator Animator 
    {
        get => animator;
        set => animator = value;
    }

    /// <summary>Gets or sets the audio source.</summary>
    /// <value>The audio source.</value>
    private AudioSource AudioSource 
    {
        get => audioSource;
        set => audioSource = value;
    }

    /// <summary>Adds the item.</summary>
    /// <param name="item">The item.</param>
    public void AddItem(IItem item)
    {
        Image slot = inventory.Find(i => i.sprite == null);

        slot.gameObject.SetActive(true);

        slot.tag = item.GetName();
        slot.sprite = item.GetIcon();
        slot.GetComponentInParent<Button>().onClick.AddListener(() => { item.Action(gameObject); });

        Game.Save(slot.tag).InFolder("Inventory").WithName("Slot" + inventory.IndexOf(slot));
        Audio.Play(AddItemSound, AudioSource);
    }

    /// <summary>Awakes this instance.</summary>
    private void Awake()
    {
        Game.LoadSettings();
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        Animator = transform.Find("Interface/Inventory").GetComponent<Animator>();

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

            Audio.Play(UseItemSound, AudioSource);
            Game.Save("0").InFolder("Inventory").WithName("Slot" + position);
        }
    }

    /// <summary>Loads the item.</summary>
    /// <param name="image">The image.</param>
    private void LoadItem(Image image) 
    {
        string slot = Game.Load("Slot" + inventory.IndexOf(image)).OfFolder("Inventory").String;
        if (!slot.Equals("0"))
        {
            image.tag = slot;
            image.sprite = Resources.Load<Sprite>("Icons/" + image.tag);
            image.GetComponentInParent<Button>().onClick.AddListener(() => { Resources.Load<GameObject>("Items/" + image.tag).GetComponent<IItem>().Action(gameObject); });
        }
    }
}