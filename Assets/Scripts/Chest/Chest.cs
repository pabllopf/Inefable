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

    /// <summary>The hit</summary>
    private const string Hit = "Hit";

    /// <summary>The interaction distance</summary>
    [SerializeField]
    [Range(1, 10)]
    private int interactionDistance = 1;

    /// <summary>The maximum number of spawn items</summary>
    [SerializeField]
    [Range(2, 5)]
    private int maxNumOfSpawnItems = 4;

    /// <summary>The key chain</summary>
    private Keychain keyChain = null;

    /// <summary>The warning need key</summary>
    private GameObject warningNeedKey = null;

    /// <summary>The is open</summary>
    private bool isOpen = false;

    /// <summary>The can open</summary>
    private bool canOpen = false;

    /// <summary>The circle collider2 d</summary>
    private CircleCollider2D circleCollider2D = null;

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>Called when [trigger stay2 d].</summary>
    /// <param name="obj">The object.</param>
    public void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Player") && !isOpen)
        {
            keyChain = obj.GetComponent<Keychain>();
            if (keyChain.CanSpendAKey())
            {
                canOpen = true;
            }

            keyChain.ActiveUI();
            warningNeedKey.SetActive(true);
        }
        else
        {
            canOpen = false;
        }
    }

    /// <summary>Called when [trigger exit2 d].</summary>
    /// <param name="obj">The object.</param>
    public void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            warningNeedKey.SetActive(false);
            canOpen = false;
            keyChain = null;
        }
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        warningNeedKey = transform.Find("NeedKey").gameObject;
        warningNeedKey.SetActive(false);

        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.isTrigger = true;
        circleCollider2D.radius = interactionDistance;

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (isOpen)
        {
            return;
        }

        if (canOpen && (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("ButtonA")))
        {
            OpenChest();
            return;
        }

        if (!canOpen && (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("ButtonA"))) 
        {
            HitChest();
            return;
        }
    }

    /// <summary>Opens the chest.</summary>
    private void OpenChest()
    {
        isOpen = true;
        canOpen = false;

        keyChain.SpendAKey();
        warningNeedKey.SetActive(false);
        animator.SetTrigger(Open);

        SpawnObjects();
    }

    /// <summary>Hits the chest.</summary>
    private void HitChest() 
    {
        animator.SetTrigger(Hit);
    }

    /// <summary>Spawns the objects.</summary>
    private void SpawnObjects()
    {
        int amount = Random.Range(1, maxNumOfSpawnItems);
        List<Vector3> vectors = new List<Vector3>();
        for (int i = 0; i < amount; i++)
        {
            Vector3 posToSpawn = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), 0);
            Vector3 size = new Vector3(0.5f, 0.5f, 0);

            while (Physics2D.OverlapBoxAll(posToSpawn, size, LayerMask.GetMask("Player")).ToList().Any(j => j.CompareTag("Player")) || vectors.Contains(posToSpawn))
            {
                posToSpawn = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), 0);
            }

            vectors.Add(posToSpawn);

            GameObject obj = Resources.LoadAll<Item>("Items")[Random.Range(0, Resources.LoadAll<Item>("Items").Length - 1)].Prefab;
            Instantiate(obj, posToSpawn, Quaternion.identity);
        }
    }
}