//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="PC.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Occlusion))]

/// <summary>PC NPC</summary>
public class PC : MonoBehaviour
{
    /// <summary>The target</summary>
    private GameObject target = null;

    /// <summary>The dialogue</summary>
    private GameObject dialogue = null; 

    /// <summary>The exclamation</summary>
    private GameObject exclamation = null;

    /// <summary>Called when [trigger stay2 d].</summary>
    /// <param name="obj">The object.</param>
    public void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Player")) 
        {
            this.exclamation.SetActive(true);
            if (!this.target) 
            {
                this.target = obj.gameObject;
            }
        }
    }

    /// <summary>Called when [trigger exit2 d].</summary>
    /// <param name="obj">The object.</param>
    public void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            this.exclamation.SetActive(false);
            this.target = null;

            if (this.dialogue && this.dialogue.activeSelf) 
            {
                this.dialogue.SetActive(false);
                this.dialogue = null;
            }
        }
    }

    /// <summary>Awakes this instance.</summary>
    private void Awake()
    {
        Game.LoadSettings();
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        this.exclamation = this.transform.Find("Exclamation").gameObject;
        this.exclamation.SetActive(false);
        this.GetComponent<CircleCollider2D>().isTrigger = true;
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (Settings.Current.Plattform == "Computer")
        {
            if (Input.anyKey)
            {
                if (Input.GetKeyDown(KeyCode.E) && this.target)
                {
                    this.dialogue = this.target.transform.Find("Interface/Dialogue").gameObject;
                    if (this.dialogue.activeSelf)
                    {
                        this.dialogue.SetActive(false);
                        return;
                    }
                    else
                    {
                        this.dialogue.SetActive(true);
                        return;
                    }
                }
            }
        }

        if (Settings.Current.Plattform == "Xbox")
        {
        }

        if (Settings.Current.Plattform == "Mobile")
        {
        }
    }
}
