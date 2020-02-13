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
    private readonly Dialogue dialogue = null;

    /// <summary>The exclamation</summary>
    private GameObject exclamation = null;

    /// <summary>Called when [trigger stay2 d].</summary>
    /// <param name="obj">The object.</param>
    public void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            exclamation.SetActive(true);
            if (!player)
            {
                player = obj.gameObject;
                dialoguePanel = player.transform.Find("Interface/Dialogue").gameObject;
                dialogueName = dialoguePanel.transform.Find("Name/Sentence").GetComponent<Text>();
                dialogueText = dialoguePanel.transform.Find("BackGround/Sentence").GetComponent<Text>();
                dialoguePanel.transform.Find("BackGround/Continue").GetComponent<Button>().onClick.AddListener(() => { ManageDialogue(); });
                dialoguePanel.transform.Find("BackGround/Continue/Image").GetComponent<PressEffect>().LoadSprites(Settings.Current.Platform);
            }
        }
    }

    /// <summary>Called when [trigger exit2 d].</summary>
    /// <param name="obj">The object.</param>
    public void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            exclamation.SetActive(false);
            player = null;

            if (dialoguePanel && dialoguePanel.activeSelf)
            {
                dialoguePanel.SetActive(false);
                dialogue.Reset();
                dialoguePanel = null;
                dialogueName = null;
                dialogueText = null;
            }
        }
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        exclamation = transform.Find("Exclamation").gameObject;
        exclamation.SetActive(false);
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (Settings.Current.Platform == "Computer")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ManageDialogue();
            }
        }

        if (Settings.Current.Platform == "Xbox")
        {
            if (Input.GetButtonDown("ButtonA"))
            {
                ManageDialogue();
            }
        }
    }

    /// <summary>Manages the dialogue.</summary>
    private void ManageDialogue()
    {
        if (player)
        {
            if (!dialoguePanel.activeSelf)
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(PrintSentence(dialogue.GetSentence()));
                return;
            }
            else
            {
                if (!printing)
                {
                    if (dialogue.HasNext())
                    {
                        StartCoroutine(PrintSentence(dialogue.GetSentence()));
                        return;
                    }
                    else
                    {
                        dialoguePanel.SetActive(false);
                        dialogue.Reset();
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
        dialogueName.text = dialogue.GetName();
        dialogueText.text = string.Empty;
        printing = true;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

        dialogue.Next();
        printing = false;
    }
}
