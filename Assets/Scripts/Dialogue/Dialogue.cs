//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Dialogue.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace DialogueSystem.DataContainers
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>Player controller by game..</summary>
    public class Dialogue : MonoBehaviour
    {
        /// <summary>The name NPC</summary>
        [SerializeField]
        private string nameNpc = string.Empty;

        /// <summary>The target</summary>
        private Player target = null;

        /// <summary>The neutral stick</summary>
        private bool neutralStick = true;

        /// <summary>The dialogue UI</summary>
        private GameObject dialogueUI = null;

        /// <summary>The exclamation</summary>
        private GameObject exclamation = null;

        /// <summary>The dialogue container</summary>
        [SerializeField] 
        private StoryData dialogueContainer = null;

        /// <summary>The selectors</summary>
        [SerializeField]
        private List<GameObject> selectors = new List<GameObject>();

        /// <summary>The sentence</summary>
        private Text sentence = null;

        /// <summary>The choice prefab</summary>
        private Button choicePrefab = null;

        /// <summary>The options container</summary>
        private Transform optionsContainer = null;

        /// <summary>The accept clip</summary>
        [SerializeField]
        private AudioClip acceptClip = null;

        /// <summary>The cancel clip</summary>
        [SerializeField]
        private AudioClip cancelClip = null;

        /// <summary>The audio source</summary>
        private AudioSource audioSource = null;

        /// <summary>Gets or sets the dialogue UI.</summary>
        /// <value>The dialogue UI.</value>
        public GameObject DialogueUI { get => dialogueUI; set => dialogueUI = value; }

        /// <summary>Gets or sets the dialogue container.</summary>
        /// <value>The dialogue container.</value>
        public StoryData DialogueContainer { get => dialogueContainer; set => dialogueContainer = value; }
        
        /// <summary>Gets or sets the choice prefab.</summary>
        /// <value>The choice prefab.</value>
        public Button ChoicePrefab { get => choicePrefab; set => choicePrefab = value; }
        
        /// <summary>Gets or sets the exclamation.</summary>
        /// <value>The exclamation.</value>
        public GameObject Exclamation { get => exclamation; set => exclamation = value; }
        
        /// <summary>Gets or sets the sentence.</summary>
        /// <value>The sentence.</value>
        public Text Sentence { get => sentence; set => sentence = value; }
        
        /// <summary>Gets or sets the options container.</summary>
        /// <value>The options container.</value>
        public Transform OptionsContainer { get => optionsContainer; set => optionsContainer = value; }

        /// <summary>Gets or sets the name NPC.</summary>
        /// <value>The name NPC.</value>
        public string NameNpc { get => nameNpc; set => nameNpc = value; }
        public Player Target { get => target; set => target = value; }

        /// <summary>Called when [trigger enter2 d].</summary>
        /// <param name="collision">The collision.</param>
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) 
            {
                if (target == null)
                {
                    Init(collision);
                }
            }
        }

        /// <summary>Called when [trigger stay2 d].</summary>
        /// <param name="collision">The collision.</param>
        public void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) 
            {
                if (target == null) 
                {
                    Init(collision);
                }
            }
        }

        /// <summary>Called when [trigger exit2 d].</summary>
        /// <param name="collision">The collision.</param>
        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Stop();
            }
        }

        /// <summary>Starts this instance.</summary>
        private void Start()
        {
            choicePrefab = Resources.Load<GameObject>("Dialogue/Choice").GetComponent<Button>();

            exclamation = transform.Find("Exclamation").gameObject;
            exclamation.SetActive(false);

            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>Initializes the specified collision.</summary>
        /// <param name="collision">The collision.</param>
        private void Init(Collider2D collision) 
        {
            target = collision.GetComponent<Player>();

            target.CanSpeak = true;

            exclamation.SetActive(true);

            target.gameObject.transform.Find("Interface/Dialogue/Name/Text").GetComponent<Text>().text = nameNpc;
            sentence = target.gameObject.transform.Find("Interface/Dialogue/Sentence/Text").GetComponent<Text>();
            optionsContainer = target.gameObject.transform.Find("Interface/Dialogue/Options/Viewport/Content");

            dialogueUI = target.gameObject.transform.Find("Interface/Dialogue").gameObject;
            dialogueUI.SetActive(false);
        }

        /// <summary>Cancels this instance.</summary>
        private void Stop() 
        {
            target.PressButtonA = false;
            target.CanSpeak = false;
            target.enabled = true;
            target = null;

            exclamation.SetActive(false);
            dialogueUI.SetActive(false);

            sentence = null;
            optionsContainer = null;
            dialogueUI = null;

            selectors.Clear();
        }

        /// <summary>Updates this instance.</summary>
        private void Update()
        {
            if (target) 
            {
                if (!dialogueUI.activeSelf)
                {
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("ButtonA") || target.PressButtonA)
                    {
                        Talk();
                    }
                }
                else 
                {
                    ChooseOption();
                }
            }
        }

        /// <summary>Talks this instance.</summary>
        private void Talk() 
        {
            dialogueUI.SetActive(true);
            target.StopPlayer();
            LinkData nodeLink = dialogueContainer.NodeLinks.First();
            Converse(nodeLink.TargetNodeGUID);
            
            Settings.Load();
            Language.Translate();
            Cursor.visible = false;
        }

        /// <summary>Converses the specified target node unique identifier.</summary>
        /// <param name="targetNodeGUID">The target node unique identifier.</param>
        private void Converse(string targetNodeGUID) 
        {
            string text = dialogueContainer.DialogueNodeData.Find(node => node.NodeGUID == targetNodeGUID).DialogueText;
            IEnumerable<LinkData> choices = dialogueContainer.NodeLinks.Where(node => node.BaseNodeGUID == targetNodeGUID);

            sentence.text = Language.Translate(text);

            selectors.Clear();
            optionsContainer.GetComponentsInChildren<Button>().ToList().ForEach(button => Destroy(button.gameObject));
            if (choices.Any())
            {
                foreach (var choice in choices)
                {
                    var button = Instantiate(choicePrefab, optionsContainer);
                    button.GetComponentInChildren<Text>().text = Language.Translate(choice.PortName);
                    button.onClick.AddListener(() => Converse(choice.TargetNodeGUID));
                    selectors.Add(button.gameObject.transform.Find("Image").gameObject);
                }
            }
            else 
            {
                var button = Instantiate(choicePrefab, optionsContainer);
                button.GetComponentInChildren<Text>().text = Language.GetSentence(Clef.A40);
                button.onClick.AddListener(() => Stop());
                selectors.Add(button.gameObject.transform.Find("Image").gameObject);
            }

            selectors.ForEach(i => i.SetActive(false));
            selectors[0].SetActive(true);
        }

        /// <summary>Chooses the option.</summary>
        private void ChooseOption()
        {
            if (Input.GetAxis("LeftJoystickY") == 0)
            {
                neutralStick = true;
            }

            if (Input.GetKeyDown(KeyCode.W) || (Input.GetAxis("LeftJoystickY") > 0 && neutralStick == true))
            {
                neutralStick = false;
                GoUpInTheMenu();
                return;
            }

            if (Input.GetKeyDown(KeyCode.S) || (Input.GetAxis("LeftJoystickY") < 0 && neutralStick == true))
            {
                neutralStick = false;
                GoDownInTheMenu();
                return;
            }

            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("ButtonA"))
            {
                PressButtonSelected();
                return;
            }
        }


        /// <summary>Goes up in the menu.</summary>
        private void GoUpInTheMenu()
        {
            GameObject selector = selectors.Find(i => i.activeSelf);

            if (selector != selectors[0])
            {
                selector.SetActive(false);
                selectors[selectors.IndexOf(selector) - 1].SetActive(true);

                Sound.Play(acceptClip, audioSource);
            }
        }

        /// <summary>Goes down in the menu.</summary>
        private void GoDownInTheMenu()
        {
            GameObject selector = selectors.Find(i => i.activeSelf);

            if (selector != selectors[selectors.Count - 1])
            {
                selector.SetActive(false);
                selectors[selectors.IndexOf(selector) + 1].SetActive(true);

                Sound.Play(acceptClip, audioSource);
            }
        }

        /// <summary>Presses the button selected.</summary>
        private void PressButtonSelected()
        {
            selectors
                .Find(i => i.activeSelf)
                .transform.parent.GetComponent<Button>()
                .onClick.Invoke();

            Sound.Play(acceptClip, audioSource);
        }
    }
}