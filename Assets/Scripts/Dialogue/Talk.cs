//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Talk.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Occlusion))]

/// <summary>Talk with this object.</summary>
public class Talk : MonoBehaviour
{
    /// <summary>The target</summary>
    private GameObject player = null;

    /// <summary>The dialogue</summary>
    private GameObject dialoguePanel = null;

    /// <summary>The dialogue name</summary>
    private Text dialogueName = null;

    /// <summary>The dialogue text</summary>
    private Text dialogueText = null;

    /// <summary>The print</summary>
    private bool printing = false;

    /// <summary>The dialogue</summary>
    [SerializeField]
    private Dialogue dialogue = null;

    /// <summary>The exclamation</summary>
    private GameObject exclamation = null;

    /// <summary>Called when [trigger stay2 d].</summary>
    /// <param name="obj">The object.</param>
    public void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Player")) 
        {
            this.exclamation.SetActive(true);
            if (!this.player) 
            {
                this.player = obj.gameObject;
                this.dialoguePanel = this.player.transform.Find("Interface/Dialogue").gameObject;
                this.dialogueName = this.dialoguePanel.transform.Find("Name/Sentence").GetComponent<Text>();
                this.dialogueText = this.dialoguePanel.transform.Find("BackGround/Sentence").GetComponent<Text>();
                this.dialoguePanel.transform.Find("BackGround/Continue").GetComponent<Button>().onClick.AddListener(() => { this.ManageDialogue(); });
                this.dialoguePanel.transform.Find("BackGround/Continue/Image").GetComponent<PressEffect>().LoadSprites(Settings.Current.Plattform);
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
            this.player = null;

            if (this.dialoguePanel && this.dialoguePanel.activeSelf) 
            {
                this.dialoguePanel.SetActive(false);
                this.dialogue.Reset();
                this.dialoguePanel = null;
                this.dialogueName = null;
                this.dialogueText = null;
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.ManageDialogue();
            }
        }

        if (Settings.Current.Plattform == "Xbox")
        {
            if (Input.GetButtonDown("ButtonA"))
            {
                this.ManageDialogue();
            }
        }
    }

    /// <summary>Manages the dialogue.</summary>
    private void ManageDialogue() 
    {
        if (this.player) 
        {
            if (!this.dialoguePanel.activeSelf)
            {
                this.dialoguePanel.SetActive(true);
                this.StartCoroutine(this.PrintSentence(this.dialogue.GetSentence()));
                return;
            }
            else
            {
                if (!this.printing)
                {
                    if (this.dialogue.HasNext())
                    {
                        this.StartCoroutine(this.PrintSentence(this.dialogue.GetSentence()));
                        return;
                    }
                    else
                    {
                        this.dialoguePanel.SetActive(false);
                        this.dialogue.Reset();
                        return;
                    }
                }
            }
        }
    }

    /// <summary>Prints the sentence.</summary>
    /// <param name="sentence">The sentence.</param>
    /// <returns>Return none</returns>
    private IEnumerator PrintSentence(string sentence) 
    {
        this.dialogueName.text = this.dialogue.GetName();
        this.dialogueText.text = string.Empty;
        this.printing = true;
        foreach (char letter in sentence.ToCharArray()) 
        {
            this.dialogueText.text += letter;
            yield return null;
        }

        this.dialogue.Next();
        this.printing = false;
    }
}
