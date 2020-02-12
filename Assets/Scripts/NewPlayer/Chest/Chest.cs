//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Chest.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>Manage a chest of the game.</summary>
public class Chest : MonoBehaviour
{
    /// <summary>The open</summary>
    private const string Open = "Open";

    /// <summary>The radio of collision</summary>
    private const int RadioOfCollision = 1;

    /// <summary>The key chain</summary>
    private KeyChain keyChain = null;

    /// <summary>The can open</summary>
    private bool canOpen = false;

    /// <summary>The is opened</summary>
    private bool isOpened = false;

    /// <summary>The items</summary>
    [SerializeField]
    private List<Item> items = new List<Item>();

    /// <summary>The need key</summary>
    private GameObject needKey = null;

    /// <summary>The circle collider</summary>
    private CircleCollider2D circle2D = null;

    /// <summary>The open clip</summary>
    [SerializeField]
    private AudioClip openClip = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>Called when [trigger stay2 d].</summary>
    /// <param name="obj">The object.</param>
    public void OnTriggerStay2D(Collider2D obj)
    {
        if (!isOpened)
        {
            if (obj.CompareTag("Player"))
            {
                needKey.SetActive(true);

                keyChain = obj.GetComponent<KeyChain>();
                if (keyChain.CanSpendAKey())
                {
                    keyChain.ActiveUI();
                    canOpen = true;
                }
                else 
                {
                    canOpen = false;
                }
            }
        }
    }

    /// <summary>Called when [trigger exit2 d].</summary>
    /// <param name="obj">The object.</param>
    public void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            needKey.SetActive(false);
            canOpen = false;
        }
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        needKey = transform.Find("NeedKey").gameObject;
        needKey.SetActive(false);

        circle2D = GetComponent<CircleCollider2D>();
        circle2D.radius = RadioOfCollision;
        circle2D.isTrigger = true;

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (!isOpened)
        {
            if (Input.GetKeyDown(KeyCode.E) && canOpen)
            {
                OpenThisChest();
            }
        }
    }

    /// <summary>Opens the this chest.</summary>
    private void OpenThisChest() 
    {
        keyChain.SpendAKey();
        needKey.SetActive(false);
        animator.SetTrigger(Open);
        isOpened = true;
        Sound.Play(openClip, audioSource);
        SpawnObjects();
        return;
    }

    /// <summary>Spawns the objects.</summary>
    private void SpawnObjects()
    {
        int amount = Random.Range(3, 5);
        List<Vector3> vectors = new List<Vector3>();
        for (int i = 0; i < amount; i++)
        {
            Vector3 vector = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), 0);
            Vector3 size = new Vector3(0.5f, 0.5f, 0);

            while (Physics2D.OverlapBoxAll(vector, size, LayerMask.GetMask("Player")).ToList().Any(j => j.CompareTag("Player")) || vectors.Contains(vector))
            {
                vector = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), 0);
            }

            vectors.Add(vector);
            Item item = items[Random.Range(0, items.Count)];
            Command.CmdInstantiate(item.Object, vector);
        }
    }
}