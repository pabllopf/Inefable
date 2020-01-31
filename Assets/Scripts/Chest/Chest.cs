//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Chest.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>Manage a chest of the game.</summary>
public class Chest : MonoBehaviour
{
    /// <summary>The open</summary>
    private const string OpenOn = "Open";

    /// <summary>The hit</summary>
    private const string Hit = "Hit";

    /// <summary>The delay</summary>
    private const float Delay = 5f;

    /// <summary>The items</summary>
    [SerializeField]
    private List<Item> items = new List<Item>();

    /// <summary>The delay active</summary>
    private bool delayActive = false;

    /// <summary>The opened</summary>
    private bool opened = false;

    /// <summary>The hit clip</summary>
    [SerializeField]
    private AudioClip hitClip = null;

    /// <summary>Gets a value indicating whether this instance is open.</summary>
    /// <value>
    /// <c>true</c> if this instance is open; otherwise, <c>false</c>.</value>
    public bool isOpen => (opened) ? true : false;

    /// <summary>Gets the need key.</summary>
    /// <value>The need key.</value>
    private GameObject NeedKey => this.transform.Find("NeedKey").gameObject;

    /// <summary>Gets the audio source.</summary>
    /// <value>The audio source.</value>
    private AudioSource AudioSource => this.GetComponent<AudioSource>();

    /// <summary>Gets the animator.</summary>
    /// <value>The animator.</value>
    private Animator Animator => this.GetComponent<Animator>();

    /// <summary>Starts this instance.</summary>
    public void Start() => this.NeedKey.SetActive(false);

    public void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Player")) 
        {
            if (!this.delayActive && !isOpen)
            {
                this.StartCoroutine(this.DelayToQuitNeedKey());
            }
            obj.gameObject.GetComponent<KeyPack>().ActiveUI();
        }
    }

    public void OnCollisionStay2D(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            if (!this.delayActive && !isOpen)
            {
                this.StartCoroutine(this.DelayToQuitNeedKey());
            }
            obj.gameObject.GetComponent<KeyPack>().ActiveUI();
        }
    }


    /// <summary>Opens this instance.</summary>
    public void Open()
    {
        this.StopAllCoroutines();
        this.Animator.SetTrigger(OpenOn);
        this.opened = true;
        this.SpawnObjects();

        this.StartCoroutine(this.AutoDestroy());
    }

    /// <summary>Takes the hit.</summary>
    public void TakeHit() 
    {
        if (!this.delayActive && !isOpen)
        {
            this.StartCoroutine(this.DelayToQuitNeedKey());
        }
        if (!isOpen) 
        {
            this.Animator.SetTrigger(Hit);
            this.PlayClip(this.hitClip);
        }
    }

    
    /// <summary>Spawns the objects.</summary>
    private void SpawnObjects() 
    {
        int amount = Random.Range(3, 5);
        List<Vector3> vectors = new List<Vector3>();
        for (int i = 0; i < amount;i++) 
        {
            Vector3 vector = new Vector3(this.transform.position.x + Random.Range(-0.5f, 0.5f), this.transform.position.y + Random.Range(-0.5f, 0.5f), 0);
            Vector3 size = new Vector3(0.5f, 0.5f, 0);

            while (Physics2D.OverlapBoxAll(vector, size, LayerMask.GetMask("Player")).ToList().Any(j => j.CompareTag("Player")) || vectors.Contains(vector)) 
            {
                vector = new Vector3(this.transform.position.x + Random.Range(-0.5f, 0.5f), this.transform.position.y + Random.Range(-0.5f, 0.5f), 0);
            }

            vectors.Add(vector);
            Item item = items[Random.Range(0, items.Count)];
            Instantiate(item.Object, vector, Quaternion.identity);
        }        
    }

    /// <summary>Delays to quit need key.</summary>
    /// <returns>Return none</returns>
    private IEnumerator DelayToQuitNeedKey() 
    {
        this.delayActive = true;
        this.NeedKey.SetActive(true);

        yield return new WaitForSeconds(Delay);

        this.delayActive = false;
        this.NeedKey.SetActive(false);
    }

    private IEnumerator AutoDestroy() 
    {
        this.tag = "Untagged";
        yield return new WaitForSeconds(0.1f);

        MonoBehaviour.Destroy(NeedKey);
        MonoBehaviour.Destroy(AudioSource);
        MonoBehaviour.Destroy(Animator);

        yield return new WaitForSeconds(1f);

        MonoBehaviour.Destroy(this.GetComponent<Chest>());
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="clip">The clip.</param>
    private void PlayClip(AudioClip clip)
    {
        this.AudioSource.clip = clip;
        this.AudioSource.Play();
    }
}