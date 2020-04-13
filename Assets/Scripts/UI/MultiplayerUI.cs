//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="MultiplayerUI.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace MultiPlayer
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>Manage multiplayer interface.</summary>
    public class MultiplayerUI : MonoBehaviour
    {
        /// <summary>The multiplayer</summary>
        private Multiplayer multiplayer = null;

        /// <summary>The player</summary>
        private Player player = null;

        /// <summary>The neutral stick</summary>
        private bool neutralStick = true;

        /// <summary>The selectors</summary>
        private List<GameObject> selectors = new List<GameObject>();

        /// <summary>The main panel</summary>
        private GameObject mainPanel = null;

        /// <summary>The single player button</summary>
        private Button singlePlayerButton = null;

        /// <summary>The local game button</summary>
        private Button localGameButton = null;

        /// <summary>The find server</summary>
        private Button findServerButton = null;

        /// <summary>The exit button</summary>
        private Button exitButton = null;

        /// <summary>The accept clip</summary>
        [SerializeField]
        private AudioClip acceptClip = null;

        /// <summary>The cancel clip</summary>
        [SerializeField]
        private AudioClip cancelClip = null;

        /// <summary>The audio source</summary>
        private AudioSource audioSource = null;

        #region Encapsulate Fields

        /// <summary>Gets or sets the multiplayer.</summary>
        /// <value>The multiplayer.</value>
        public Multiplayer Multiplayer { get => multiplayer; set => multiplayer = value; }

        /// <summary>Gets or sets the player.</summary>
        /// <value>The player.</value>
        public Player Player { get => player; set => player = value; }

        /// <summary>Gets or sets a value indicating whether [neutral stick].</summary>
        /// <value>
        /// <c>true</c> if [neutral stick]; otherwise, <c>false</c>.</value>
        public bool NeutralStick { get => neutralStick; set => neutralStick = value; }

        /// <summary>Gets or sets the selectors.</summary>
        /// <value>The selectors.</value>
        public List<GameObject> Selectors { get => selectors; set => selectors = value; }

        /// <summary>Gets or sets the main panel.</summary>
        /// <value>The main panel.</value>
        public GameObject MainPanel { get => mainPanel; set => mainPanel = value; }

        /// <summary>Gets or sets the single player button.</summary>
        /// <value>The single player button.</value>
        public Button SinglePlayerButton { get => singlePlayerButton; set => singlePlayerButton = value; }
        
        /// <summary>Gets or sets the local game button.</summary>
        /// <value>The local game button.</value>
        public Button LocalGameButton { get => localGameButton; set => localGameButton = value; }

        /// <summary>Gets or sets the find server button.</summary>
        /// <value>The find server button.</value>
        public Button FindServerButton { get => findServerButton; set => findServerButton = value; }
        
        /// <summary>Gets or sets the exit button.</summary>
        /// <value>The exit button.</value>
        public Button ExitButton { get => exitButton; set => exitButton = value; }
        
        /// <summary>Gets or sets the accept clip.</summary>
        /// <value>The accept clip.</value>
        public AudioClip AcceptClip { get => acceptClip; set => acceptClip = value; }
        
        /// <summary>Gets or sets the cancel clip.</summary>
        /// <value>The cancel clip.</value>
        public AudioClip CancelClip { get => cancelClip; set => cancelClip = value; }
        
        /// <summary>Gets or sets the audio source.</summary>
        /// <value>The audio source.</value>
        public AudioSource AudioSource { get => audioSource; set => audioSource = value; }

        #endregion

        /// <summary>Called when [collision enter2 d].</summary>
        /// <param name="collision">The collision.</param>
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Portal")) 
            {
                if (!mainPanel.activeSelf) 
                {
                    mainPanel.SetActive(true);
                    player.StopPlayer();
                }
            }
        }

        /// <summary>Shows the servers.</summary>
        public void ShowServers() 
        {
        }

        /// <summary>Exits the of menu.</summary>
        public void ExitOfMenu() 
        {
            if (mainPanel.activeSelf)
            {
                mainPanel.SetActive(false);
                player.enabled = true;
            }
        }

        /// <summary>Starts this instance.</summary>
        private void Start()
        {
            player = GetComponent<Player>();

            multiplayer = GameObject.FindWithTag("Network").GetComponent<Multiplayer>();

            mainPanel = transform.Find("Interface/Multiplayer").gameObject;
            mainPanel.SetActive(false);

            selectors = ConfigSelectors();

            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>Configurations the selectors.</summary>
        /// <returns>Return the selectors</returns>
        private List<GameObject> ConfigSelectors()
        {
            mainPanel.transform.Find("SinglePlayer").GetComponent<Button>().onClick.AddListener(() => { multiplayer.PlayLocalInSingleMode(); });
            mainPanel.transform.Find("LocalGame").GetComponent<Button>().onClick.AddListener(() => { multiplayer.PlayLocalMode(); });
            mainPanel.transform.Find("FindServer").GetComponent<Button>().onClick.AddListener(() => { multiplayer.PlayServerMode(); });
            mainPanel.transform.Find("Exit").GetComponent<Button>().onClick.AddListener(() => { ExitOfMenu(); });

            return new List<GameObject>
            {
                mainPanel.transform.Find("SinglePlayer/Selector").gameObject,
                mainPanel.transform.Find("LocalGame/Selector").gameObject,
                mainPanel.transform.Find("FindServer/Selector").gameObject,
                mainPanel.transform.Find("Exit/Selector").gameObject
            };
        }

        /// <summary>Updates this instance.</summary>
        private void Update()
        {
            if (Settings.Current.Platform.Equals("Mobile"))
            {
                selectors.ForEach(i => i.SetActive(false));
            }

            if (mainPanel.activeSelf)
            {
                UseMenu();
                return;
            }
        }

        /// <summary>Uses menu.</summary>
        private void UseMenu()
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