//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="PauseMenu.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using Mirror;
using MultiPlayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>Manage the pause menu.</summary>
public class PauseMenu : NetworkBehaviour
{
    /// <summary>The scene of new adventure</summary>
    [SerializeField]
    private string sceneMainMenu = "MainMenu";

    /// <summary>The neutral stick</summary>
    private bool neutralStick = true;

    /// <summary>The selectors</summary>
    private List<GameObject> selectors = new List<GameObject>();

    /// <summary>The pause menu panel</summary>
    private GameObject pauseMenuPanel = null;

    /// <summary>The accept clip</summary>
    [SerializeField]
    private AudioClip acceptClip = null;

    /// <summary>The cancel clip</summary>
    [SerializeField]
    private AudioClip cancelClip = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    #region Encapsulate Fields
    
    /// <summary>Gets or sets the scene main menu.</summary>
    /// <value>The scene main menu.</value>
    public string SceneMainMenu { get => sceneMainMenu; set => sceneMainMenu = value; }

    /// <summary>Gets or sets the pause menu panel.</summary>
    /// <value>The pause menu panel.</value>
    public GameObject PauseMenuPanel { get => pauseMenuPanel; set => pauseMenuPanel = value; }
    
    /// <summary>Gets or sets the audio source.</summary>
    /// <value>The audio source.</value>
    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }
    
    /// <summary>Gets or sets the cancel clip.</summary>
    /// <value>The cancel clip.</value>
    public AudioClip CancelClip { get => cancelClip; set => cancelClip = value; }
    
    /// <summary>Gets or sets the accept clip.</summary>
    /// <value>The accept clip.</value>
    public AudioClip AcceptClip { get => acceptClip; set => acceptClip = value; }
    
    /// <summary>Gets or sets a value indicating whether [neutral stick].</summary>
    /// <value>
    /// <c>true</c> if [neutral stick]; otherwise, <c>false</c>.</value>
    public bool NeutralStick { get => neutralStick; set => neutralStick = value; }

    #endregion

    /// <summary>Uses the pause menu.</summary>
    public void UsePauseMenu() 
    {
        if (pauseMenuPanel.activeSelf)
        {
            pauseMenuPanel.SetActive(false);
            return;
        }
        else
        {
            pauseMenuPanel.SetActive(true);
            Sound.Play(acceptClip, audioSource);
            return;
        }
    }

    /// <summary>Continues this instance.</summary>
    public void Continue() 
    {
        pauseMenuPanel.SetActive(false);
        return;
    }

    /// <summary>Options this instance.</summary>
    public void Options() 
    {
        Debug.LogError("Options of pause menu not implemented!!!");
        return;
    }

    /// <summary>Exits this instance.</summary>
    public void Exit() 
    {
        GameObject.FindWithTag("Network").GetComponent<Multiplayer>().GoToMainMenu();
    }

    public void GoToTown()
    {
        GameObject.FindWithTag("Network").GetComponent<Multiplayer>().GoToMainMenu();
    }

    /// <summary>Awakes this instance.</summary>
    private void Awake()
    {
        Settings.Load();
        Language.Translate();
        Cursor.visible = false;
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        pauseMenuPanel = transform.Find("Interface/PauseMenu").gameObject;
        pauseMenuPanel.SetActive(false);

        selectors = ConfigSelectors();

        transform.Find("Interface/Mobile/PauseButton").GetComponent<Button>().onClick.AddListener(() => { UsePauseMenu(); });

        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (Settings.Current.Platform.Equals("Mobile"))
        {
            selectors.ForEach(i => i.SetActive(false));
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("ButtonStart"))
        {
            UsePauseMenu();
            return;
        }

        if (pauseMenuPanel.activeSelf) 
        {
            UseTheMenu();
            return;
        }
    }

    /// <summary>Configurations the selectors.</summary>
    /// <returns>Return the selectors</returns>
    private List<GameObject> ConfigSelectors()
    {
        pauseMenuPanel.transform.Find("Continue").GetComponent<Button>().onClick.AddListener(() => { Continue(); });
        pauseMenuPanel.transform.Find("Options").GetComponent<Button>().onClick.AddListener(() => { Options(); });
        pauseMenuPanel.transform.Find("Exit").GetComponent<Button>().onClick.AddListener(() => { Exit(); });

        return new List<GameObject>
        {
            pauseMenuPanel.transform.Find("Continue/Selector").gameObject,
            pauseMenuPanel.transform.Find("Options/Selector").gameObject,
            pauseMenuPanel.transform.Find("Exit/Selector").gameObject
        };
    }

    /// <summary>Uses the menu.</summary>
    private void UseTheMenu()
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