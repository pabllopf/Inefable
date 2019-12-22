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

    /// <summary>The panel center controls</summary>
    private GameObject panelCenterControls = null;

    /// <summary>The panel center audio</summary>
    private GameObject panelCenterAudio = null;

    /// <summary>The panel center graphics</summary>
    private GameObject panelCenterGraphics = null;

    /// <summary>The selectors</summary>
    private List<GameObject> leftListOfSelectors = null;

    /// <summary>The selectors</summary>
    private List<GameObject> centerSelectors = null;

    /// <summary>The pause button</summary>
    private GameObject pauseButton = null;

    /// <summary>The continue button</summary>
    private GameObject continueButton = null;

    /// <summary>The controls button</summary>
    private GameObject controlsButton = null;

    /// <summary>The audio button</summary>
    private GameObject audioButton = null;

    /// <summary>The graphics button</summary>
    private GameObject graphicsButton = null;

    /// <summary>The exit button</summary>
    private GameObject exitButton = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>The accept clip</summary>
    [SerializeField]
    private AudioClip acceptClip = null;

    /// <summary>Pauses this instance.</summary>
    public void Pause()
    {
        if (this.pauseMenuPanel.activeSelf)
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
        this.pauseMenuPanel.SetActive(false);
        Game.SaveSettings();
    }

    /// <summary>Controls this instance.</summary>
    public void Controls() 
    {
    }

    /// <summary>Audios this instance.</summary>
    public void Audio() 
    {
    }

    /// <summary>Graphics this instance.</summary>
    public void Graphics() 
    {
        this.panelCenterGraphics.SetActive(true);
    }

    /// <summary>the synchronize button action.</summary>
    public void VSyncButtonAction() 
    {
        QualitySettings.vSyncCount = 0;
    }

    /// <summary>Returns the graphics.</summary>
    public void ReturnGraphics()
    {
        this.panelCenterGraphics.SetActive(false);
    }

    /// <summary>Exits this instance.</summary>
    public void Exit()
    {
        Game.SaveSettings();
        Game.SaveStats();
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

        this.SetLeftPanel();

        this.SetPanelCenterControls();
        this.SetPanelCenterAudio();
        this.SetPanelCenterGraphics();

        this.audioSource = this.GetComponent<AudioSource>();

        this.pauseMenuPanel.SetActive(false);
    }

    /// <summary>Sets the left panel.</summary>
    private void SetLeftPanel() 
    {
        this.continueButton = this.transform.Find("Interface/PauseMenu/LeftPanel/Continue").gameObject;
        this.controlsButton = this.transform.Find("Interface/PauseMenu/LeftPanel/Controls").gameObject;
        this.audioButton = this.transform.Find("Interface/PauseMenu/LeftPanel/Audio").gameObject;
        this.graphicsButton = this.transform.Find("Interface/PauseMenu/LeftPanel/Graphics").gameObject;
        this.exitButton = this.transform.Find("Interface/PauseMenu/LeftPanel/Exit").gameObject;

        this.leftListOfSelectors = new List<GameObject>(5)
        {
            this.continueButton.transform.Find("Selector").gameObject,
            this.controlsButton.transform.Find("Selector").gameObject,
            this.audioButton.transform.Find("Selector").gameObject,
            this.graphicsButton.transform.Find("Selector").gameObject,
            this.exitButton.transform.Find("Selector").gameObject
        };

        this.continueButton.GetComponent<Button>().onClick.AddListener(()   => { Continue();    });
        this.controlsButton.GetComponent<Button>().onClick.AddListener(()   => { Controls();    });
        this.audioButton.GetComponent<Button>().onClick.AddListener(()      => { Audio();       });
        this.graphicsButton.GetComponent<Button>().onClick.AddListener(()   => { Graphics();    });
        this.exitButton.GetComponent<Button>().onClick.AddListener(()       => { Exit();        });
    }

    /// <summary>Sets the panel center controls.</summary>
    private void SetPanelCenterControls()
    {
        this.panelCenterControls = this.transform.Find("Interface/PauseMenu/PanelCenterControls").gameObject;

        this.panelCenterControls.SetActive(false);
    }

    /// <summary>Sets the panel center audio.</summary>
    private void SetPanelCenterAudio() 
    {
        this.panelCenterAudio = this.transform.Find("Interface/PauseMenu/PanelCenterAudio").gameObject;

        this.panelCenterAudio.SetActive(false);
    }

    /// <summary>Sets the panel center graphics.</summary>
    private void SetPanelCenterGraphics()
    {
        this.panelCenterGraphics = this.transform.Find("Interface/PauseMenu/PanelCenterGraphics").gameObject;

        this.panelCenterGraphics.SetActive(false);
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

        if (this.pauseMenuPanel.activeSelf && !this.panelCenterGraphics.activeSelf) 
        {
            this.ShiftInMenu(this.leftListOfSelectors);
            return;
        }

        if (this.pauseMenuPanel.activeSelf && this.panelCenterGraphics.activeSelf)
        {
            this.ShiftInMenu(this.centerSelectors);
            return;
        }
    }

    /// <summary>Detects the input.</summary>
    private void DetectInput() 
    {
        if (Settings.Current.Plattform == "Computer")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this.Pause();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this.Pause();
                return;
            }
        }

        if (Settings.Current.Plattform == "Xbox")
        {
        }
    }

    /// <summary>Shifts the in menu.</summary>
    /// <param name="selectors">The selectors.</param>
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

    /// <summary>Goes up.</summary>
    /// <param name="selectors">The selectors.</param>
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

    /// <summary>Goes down.</summary>
    /// <param name="selectors">The selectors.</param>
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

    /// <summary>Actions the selector.</summary>
    /// <param name="selectors">The selectors.</param>
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

    /// <summary>Plays the clip.</summary>
    /// <param name="audioClip">The audio clip.</param>
    private void PlayClip(AudioClip audioClip)
    {
        this.audioSource.clip = audioClip;
        this.audioSource.Play();
    }
}
