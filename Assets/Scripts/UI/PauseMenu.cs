//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="PauseMenu.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>Manage the pause menu of the game.</summary>
public class PauseMenu : MonoBehaviour
{
    /// <summary>The name scene to load</summary>
    [SerializeField]
    private string sceneToLoad = "Start";

    /// <summary>The neutral stick</summary>
    private bool neutralStick = true;

    /// <summary>The pause menu panel</summary>
    private GameObject pauseMenuPanel = null;

    /// <summary>The panel center graphics</summary>
    private GameObject panelCenterGraphics = null;

    /// <summary>The selectors</summary>
    private List<GameObject> leftSelectors = null;

    /// <summary>The selectors</summary>
    private List<GameObject> centerSelectors = null;

    /// <summary>The selectors</summary>
    private List<GameObject> rightSelectors = null;

    /// <summary>The pause button</summary>
    private GameObject pauseButton = null;

    /// <summary>The continue button</summary>
    private GameObject continueButton = null;

    /// <summary>The graphics button</summary>
    private GameObject graphicsButton = null;

    /// <summary>The synchronize button</summary>
    private GameObject vSyncButton = null;

    /// <summary>The continue button</summary>
    private GameObject returnGraphicsButton = null;

    /// <summary>The exit button</summary>
    private GameObject exitButton = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>The accept clip</summary>
    [SerializeField]
    private AudioClip acceptClip = null;

    /// <summary>The cancel clip</summary>
    [SerializeField]
    private AudioClip cancelClip = null;

    /// <summary>Pauses this instance.</summary>
    public void Pause()
    {
        if (pauseMenuPanel.activeSelf)
        {
            this.pauseMenuPanel.SetActive(false);
            return;
        }
        else 
        {
            this.pauseMenuPanel.SetActive(true);
            return;
        }
    }

    /// <summary>Continues this instance.</summary>
    public void Continue() 
    {
        this.Pause(false);
        this.pauseMenuPanel.SetActive(false);
        Game.SaveSettings();
    }

    /// <summary>Graphics this instance.</summary>
    public void Graphics() 
    {
        this.panelCenterGraphics.SetActive(true);
    }

    public void VSyncButtonAction() 
    {
        QualitySettings.vSyncCount = 0;
    }

    public void ReturnGraphics()
    {
        this.panelCenterGraphics.SetActive(false);
    }

    /// <summary>Exits this instance.</summary>
    public void Exit()
    {
        Game.SaveSettings();
        SceneManager.LoadScene(this.sceneToLoad);
    }

    /// <summary>Awakes this instance.</summary>
    private void Awake()
    {
        Game.LoadSettings();
        Language.Translate();
        Cursor.visible = false;
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        Debug.Log(Settings.Current.Plattform);
        Debug.Log(Application.persistentDataPath);
        this.InitMainParameters();
        this.DetectController();
    }

    /// <summary>Initializes the main parameters.</summary>
    private void InitMainParameters() 
    {
        this.pauseMenuPanel = this.transform.Find("Interface/PauseMenu").gameObject;

        this.pauseButton = this.transform.Find("Interface/PauseButton").gameObject;
        this.pauseButton.GetComponent<Button>().onClick.AddListener(() => { Pause(); });
        this.pauseButton.SetActive(false);

        this.centerSelectors = new List<GameObject>(1);
        
        this.panelCenterGraphics = this.transform.Find("Interface/PauseMenu/PanelCenterGraphics").gameObject;

        this.vSyncButton = this.panelCenterGraphics.transform.Find("VSync").gameObject;
        this.centerSelectors.Add(this.vSyncButton.transform.Find("Selector").gameObject);
        this.vSyncButton.GetComponent<Button>().onClick.AddListener(() => { VSyncButtonAction(); });

        this.returnGraphicsButton = this.panelCenterGraphics.transform.Find("Return").gameObject;
        this.centerSelectors.Add(this.returnGraphicsButton.transform.Find("Selector").gameObject);
        this.returnGraphicsButton.GetComponent<Button>().onClick.AddListener(() => { ReturnGraphics(); });
        
        this.leftSelectors = new List<GameObject>(2);

        this.continueButton = this.transform.Find("Interface/PauseMenu/LeftPanel/Continue").gameObject;
        this.leftSelectors.Add(this.continueButton.transform.Find("Selector").gameObject);
        this.continueButton.GetComponent<Button>().onClick.AddListener(() => { Continue(); });

        this.graphicsButton = this.transform.Find("Interface/PauseMenu/LeftPanel/Graphics").gameObject;
        this.leftSelectors.Add(this.graphicsButton.transform.Find("Selector").gameObject);
        this.graphicsButton.GetComponent<Button>().onClick.AddListener(() => { Graphics(); });

        this.exitButton = this.transform.Find("Interface/PauseMenu/LeftPanel/Exit").gameObject;
        this.leftSelectors.Add(this.exitButton.transform.Find("Selector").gameObject);
        this.exitButton.GetComponent<Button>().onClick.AddListener(() => { Exit(); });

        this.audioSource = this.GetComponent<AudioSource>();

        this.panelCenterGraphics.SetActive(false);
        this.pauseMenuPanel.SetActive(false);
    }

    /// <summary>Detects the input.</summary>
    private void DetectController()
    {
        if (Settings.Current.Plattform == "Computer")
        {
            this.UpdateButtons("Computer");
        }

        if (Settings.Current.Plattform == "Xbox")
        {
            this.UpdateButtons("Xbox");
        }

        if (Settings.Current.Plattform == "Mobile")
        {
            this.pauseButton.SetActive(true);
        }
    }

    /// <summary>Updates the buttons.</summary>
    /// <param name="controller">The controller.</param>
    private void UpdateButtons(string controller)
    {
        Image[] objs = GameObject.FindObjectsOfType<Image>();
        foreach (Image img in objs)
        {
            if (img.gameObject.GetComponent<PressEffect>())
            {
                img.gameObject.GetComponent<PressEffect>().LoadSprites(controller);
            }
        }
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        this.DetectInput();

        if (this.pauseMenuPanel.activeSelf && !panelCenterGraphics.activeSelf) 
        {
            this.ShiftInMenu(leftSelectors);
            return;
        }

        if (this.pauseMenuPanel.activeSelf && panelCenterGraphics.activeSelf)
        {
            this.ShiftInMenu(centerSelectors);
            return;
        }
    }

    /// <summary>Detects the input.</summary>
    private void DetectInput() 
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            this.Pause();
        }
    }

    /// <summary>Shifts the in menu.</summary>
    private void ShiftInMenu(List<GameObject> selectors)
    {
        this.UpdateButtons(Settings.Current.Plattform);
        if (Settings.Current.Plattform == "Computer")
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.GoUp(selectors);
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.GoDown(selectors);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                this.ActionSelector(selectors);
            }
        }

        if (Settings.Current.Plattform == "Xbox")
        {
            if (Input.GetAxis("LeftJoystickY") < 0 && this.neutralStick == true)
            {
                this.neutralStick = false;
                this.GoUp(selectors);
            }

            if (Input.GetAxis("LeftJoystickY") > 0 && this.neutralStick == true)
            {
                this.neutralStick = false;
                this.GoDown(selectors);
            }

            if (Input.GetAxis("LeftJoystickY") == 0)
            {
                this.neutralStick = true;
            }

            if (Input.GetButtonDown("ButtonA"))
            {
                this.ActionSelector(selectors);
            }
        }

        if (Settings.Current.Plattform == "Mobile")
        {
            foreach (GameObject selector in selectors)
            {
                selector.SetActive(false);
            }
        }
    }

    /// <summary>Goes up in menu.</summary>
    private void GoUp(List<GameObject> selectors)
    {
        for (int i = 0; i < selectors.Count; i++)
        {
            if (selectors[i].activeSelf)
            {
                if (selectors[i] != selectors[0])
                {
                    selectors[i].SetActive(false);
                    selectors[i - 1].SetActive(true);
                    this.PlayClip(this.acceptClip);
                    return;
                }
            }
        }
    }

    /// <summary>Goes down in menu.</summary>
    private void GoDown(List<GameObject> selectors)
    {
        for (int i = 0; i < selectors.Count; i++)
        {
            if (selectors[i].activeSelf)
            {
                if (selectors[i] != selectors[selectors.Count - 1])
                {
                    selectors[i].SetActive(false);
                    selectors[i + 1].SetActive(true);
                    this.PlayClip(this.acceptClip);
                    return;
                }
            }
        }
    }

    /// <summary>Action the selector.</summary>
    private void ActionSelector(List<GameObject> selectors)
    {
        this.PlayClip(this.acceptClip);
        foreach (GameObject selector in selectors)
        {
            if (selector.activeSelf)
            {
                selector.transform.parent.GetComponent<Button>().onClick.Invoke();
            }
        }
    }

    /// <summary>Pauses the specified state.</summary>
    /// <param name="state">if set to <c>true</c> [state].</param>
    private void Pause(bool state)
    {
        if (state == true)
        {
            this.GetComponent<Player>().Move(false);
        }
        else 
        {
            this.GetComponent<Player>().Move(true);
        }
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="audioClip">The audio clip.</param>
    private void PlayClip(AudioClip audioClip)
    {
        this.audioSource.clip = audioClip;
        this.audioSource.Play();
    }
}
