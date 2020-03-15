//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Dialogue.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace DialogueSystem.DataContainers
{
    using System.Collections.Generic;
    using System.Linq;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>Player controller by game..</summary>
    public class Dialogue : MonoBehaviour
    {
        /// <summary>The dialogue UI</summary>
        private GameObject dialogueUI = null;

        /// <summary>The dialogue container</summary>
        [SerializeField] 
        private DialogueContainer dialogueContainer = null;

        /// <summary>The dialogue text</summary>
        [SerializeField] 
        private TextMeshProUGUI dialogueText = null;

        /// <summary>The choice prefab</summary>
        [SerializeField] 
        private Button choicePrefab = null;

        /// <summary>The button container</summary>
        [SerializeField] 
        private Transform buttonContainer = null;

        /// <summary>Gets or sets the dialogue UI.</summary>
        /// <value>The dialogue UI.</value>
        public GameObject DialogueUI { get => dialogueUI; set => dialogueUI = value; }

        /// <summary>Gets or sets the dialogue container.</summary>
        /// <value>The dialogue container.</value>
        public DialogueContainer DialogueContainer { get => dialogueContainer; set => dialogueContainer = value; }
        
        /// <summary>Gets or sets the button container.</summary>
        /// <value>The button container.</value>
        public Transform ButtonContainer { get => buttonContainer; set => buttonContainer = value; }
        
        /// <summary>Gets or sets the dialogue text.</summary>
        /// <value>The dialogue text.</value>
        public TextMeshProUGUI DialogueText { get => dialogueText; set => dialogueText = value; }
        
        /// <summary>Gets or sets the choice prefab.</summary>
        /// <value>The choice prefab.</value>
        public Button ChoicePrefab { get => choicePrefab; set => choicePrefab = value; }

        /// <summary>Called when [trigger enter2 d].</summary>
        /// <param name="collision">The collision.</param>
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) 
            {
                dialogueUI.SetActive(true);
                NodeLinkData nodeLink = dialogueContainer.NodeLinks.First();
                Converse(nodeLink.TargetNodeGUID);
            }
        }

        /// <summary>Called when [trigger exit2 d].</summary>
        /// <param name="collision">The collision.</param>
        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                dialogueUI.SetActive(false);
            }
        }

        /// <summary>Starts this instance.</summary>
        private void Start()
        {
            dialogueUI = transform.Find("Dialogue").gameObject;
            dialogueUI.SetActive(false);
        }

        /// <summary>Converses the specified target node unique identifier.</summary>
        /// <param name="targetNodeGUID">The target node unique identifier.</param>
        private void Converse(string targetNodeGUID) 
        {
            string text = dialogueContainer.DialogueNodeData.Find(node => node.NodeGUID == targetNodeGUID).DialogueText;
            IEnumerable<NodeLinkData> choices = dialogueContainer.NodeLinks.Where(node => node.BaseNodeGUID == targetNodeGUID);

            dialogueText.text = text;

            buttonContainer.GetComponentsInChildren<Button>().ToList().ForEach(button => Destroy(button.gameObject));
            if (choices.Any())
            {
                foreach (var choice in choices)
                {
                    var button = Instantiate(choicePrefab, buttonContainer);
                    button.GetComponentInChildren<Text>().text = choice.PortName;
                    button.onClick.AddListener(() => Converse(choice.TargetNodeGUID));
                }
            }
            else 
            {
                var button = Instantiate(choicePrefab, buttonContainer);
                button.GetComponentInChildren<Text>().text = "Adios";
                button.onClick.AddListener(() => ExitOfDialogue());
            }
        }

        /// <summary>Exits the of dialogue.</summary>
        private void ExitOfDialogue() 
        {
            dialogueUI.SetActive(false);
        }
    }
}