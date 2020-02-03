﻿//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="MainMenuOld.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>Main menu of the game.</summary>
public class MainMenuOld : MonoBehaviour
{
    /// <summary>The name scene to load</summary>
    [SerializeField]
    private string sceneToLoad = "House";

    /// <summary>The current controller</summary>
    private string currentController = string.Empty;

    /// <summary>The neutral stick</summary>
    private bool neutralStick = true;

    /// <summary>The start panel</summary>
    private GameObject startPanel = null;

    /// <summary>The language panel</summary>
    private GameObject languagePanel = null;

    /// <summary>The languages</summary>
    private List<string> languages = null;

    /// <summary>The index languages</summary>
    private int indexLanguages = 0;

    /// <summary>The main panel</summary>
    private GameObject mainPanel = null;

    /// <summary>The selectors</summary>
    private List<GameObject> selectors = null;

    /// <summary>The buttons panel</summary>
    private GameObject buttonsPanel = null;

    /// <summary>The pop up panel</summary>
    private GameObject popUpPanel = null;

    /// <summary>The pop up exit</summary>
    private GameObject popUpExit = null;

    /// <summary>The pop up new adventure</summary>
    private GameObject popUpNewAdventure = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>The accept clip</summary>
    [SerializeField]
    private AudioClip acceptClip = null;

    /// <summary>The cancel clip</summary>
    [SerializeField]
    private AudioClip cancelClip = null;

    /// <summary>Awakes this instance.</summary>
    public void Awake()
    {
        Game.LoadSettings();
        Game.LoadStats();
        Cursor.visible = false;

        this.startPanel = GameObject.FindGameObjectWithTag("StartPanel").gameObject;
        this.languagePanel = GameObject.FindGameObjectWithTag("LanguagePanel").gameObject;
        this.mainPanel = GameObject.FindGameObjectWithTag("MainPanel").gameObject;
        this.InitPopUpPanel();
        this.InitButtonsPanel();
        this.InitButtonsLanguage();
        this.audioSource = this.GetComponent<AudioSource>();
    }

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        Language.Translate();
        GameObject.FindGameObjectWithTag("NewButtons").SetActive(false);
        GameObject.FindGameObjectWithTag("NormalButtons").SetActive(false);
        this.popUpPanel.SetActive(false);
        this.mainPanel.SetActive(false);
        this.languagePanel.SetActive(false);

        this.languages = new List<string>();
        this.languages.Add("English");
        this.languages.Add("Español");
        this.languages.Add("French");
    }

    /// <summary>Creates new adventure.</summary>
    public void NewAdventure()
    {
        this.PlayClip(this.acceptClip);
        this.mainPanel.SetActive(false);
        this.popUpPanel.SetActive(true);
        this.popUpNewAdventure.SetActive(true);
        Language.Translate();
    }

    /// <summary>Continues the adventure.</summary>
    public void ContinueAdventure()
    {
        this.PlayClip(this.acceptClip);
        Game.LoadSettings();
        SceneManager.LoadScene(this.sceneToLoad);
    }

    /// <summary>Communities this instance.</summary>
    public void Community()
    {
        this.PlayClip(this.acceptClip);
    }

    /// <summary>Exits the game.</summary>
    public void ExitGame()
    {
        this.PlayClip(this.acceptClip);
        this.popUpPanel.SetActive(true);
        this.popUpExit.SetActive(true);
        this.mainPanel.SetActive(false);
        Language.Translate();
    }

    private void InitButtonsLanguage()
    {
        GameObject.Find("Interface/LanguagePanel/Background/Up").GetComponent<Button>().onClick.AddListener(() => { GoUpLanguage(); });
        GameObject.Find("Interface/LanguagePanel/Background/Down").GetComponent<Button>().onClick.AddListener(() => { GoDownLanguage(); });
        GameObject.Find("Interface/LanguagePanel/Background/Confirm").GetComponent<Button>().onClick.AddListener(() => { ConfirmLanguage(); });
    }

    /// <summary>Get the buttons panel.</summary>
    private void InitButtonsPanel()
    {
        if (Settings.Current.HasSaveGame)
        {
            this.buttonsPanel = GameObject.FindGameObjectWithTag("NormalButtons").gameObject;

            this.selectors = new List<GameObject>(4);
            this.selectors.Add(this.buttonsPanel.transform.Find("NewAdventure/Selector").gameObject);
            this.selectors.Add(this.buttonsPanel.transform.Find("Continue/Selector").gameObject);
            this.selectors.Add(this.buttonsPanel.transform.Find("Community/Selector").gameObject);
            this.selectors.Add(this.buttonsPanel.transform.Find("Exit/Selector").gameObject);

            this.buttonsPanel.transform.Find("NewAdventure").GetComponent<Button>().onClick.AddListener(() => { NewAdventure(); });
            this.buttonsPanel.transform.Find("Continue").GetComponent<Button>().onClick.AddListener(() => { ContinueAdventure(); });
            this.buttonsPanel.transform.Find("Community").GetComponent<Button>().onClick.AddListener(() => { Community(); });
            this.buttonsPanel.transform.Find("Exit").GetComponent<Button>().onClick.AddListener(() => { ExitGame(); });
        }
        else
        {
            this.buttonsPanel = GameObject.FindGameObjectWithTag("NewButtons").gameObject;

            this.selectors = new List<GameObject>(3);
            this.selectors.Add(this.buttonsPanel.transform.Find("NewAdventure/Selector").gameObject);
            this.selectors.Add(this.buttonsPanel.transform.Find("Community/Selector").gameObject);
            this.selectors.Add(this.buttonsPanel.transform.Find("Exit/Selector").gameObject);

            this.buttonsPanel.transform.Find("NewAdventure").GetComponent<Button>().onClick.AddListener(() => { NewAdventure(); });
            this.buttonsPanel.transform.Find("Community").GetComponent<Button>().onClick.AddListener(() => { Community(); });
            this.buttonsPanel.transform.Find("Exit").GetComponent<Button>().onClick.AddListener(() => { ExitGame(); });
        }
    }

    /// <summary>Initializes the pop up panel.</summary>
    private void InitPopUpPanel() 
    {
        this.popUpPanel = GameObject.FindGameObjectWithTag("PopUpPanel").gameObject;

        this.popUpExit = this.popUpPanel.transform.Find("ExitGame").gameObject;
        this.popUpExit.transform.Find("BackGround/Yes").GetComponent<Button>().onClick.AddListener(() => { YesExitGame(); });
        this.popUpExit.transform.Find("BackGround/No").GetComponent<Button>().onClick.AddListener(() => { NoExitGame(); });
        this.popUpExit.SetActive(false);

        this.popUpNewAdventure = this.popUpPanel.transform.Find("NewAdventure").gameObject;
        this.popUpNewAdventure.transform.Find("BackGround/Yes").GetComponent<Button>().onClick.AddListener(() => { YesNewAdventure(); });
        this.popUpNewAdventure.transform.Find("BackGround/No").GetComponent<Button>().onClick.AddListener(() => { NoNewAdventure(); });
        this.popUpNewAdventure.SetActive(false);
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (this.startPanel.activeSelf)
        {
            this.DetectController();
            return;
        }

        if (this.languagePanel.activeSelf)
        {
            this.DetectLanguage();
            Settings.Current.Language = this.languages[this.indexLanguages];
            Game.SaveSettings();
            Language.Translate();
            return;
        }

        if (this.mainPanel.activeSelf && !this.popUpPanel.activeSelf)
        {
            this.ShiftInMenu();
            return;
        }

        if (this.popUpPanel.activeSelf) 
        {
            this.DialoguePopUp();
        }
    }

    /// <summary>Detects the controller.</summary>
    private void DetectController()
    {
        if (this.startPanel.activeSelf)
        {
            if (Input.touchCount > 0)
            {
                this.SelectController("Mobile");
                return;
            }

            if (Input.GetJoystickNames().Length > 0 && Input.anyKey)
            {
                this.SelectController("Xbox");
                return;
            }

            if (Input.anyKey)
            {
                this.SelectController("Computer");
                return;
            }
        }

        return;
    }

    /// <summary>Detects the language.</summary>
    private void DetectLanguage()
    {
        if (this.currentController == "Computer")
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                this.GoUpLanguage();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                this.GoDownLanguage();
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                this.ConfirmLanguage();
            }
        }

        if (this.currentController == "Xbox")
        {
            if (Input.GetAxis("LeftJoystickY") > 0 && this.neutralStick == true)
            {
                this.neutralStick = false;
                this.GoUpLanguage();
            }

            if (Input.GetAxis("LeftJoystickY") < 0 && this.neutralStick == true)
            {
                this.neutralStick = false;
                this.GoDownLanguage();
            }

            if (Input.GetAxis("LeftJoystickY") == 0)
            {
                this.neutralStick = true;
            }

            if (Input.GetButtonDown("ButtonY"))
            {
                this.ConfirmLanguage();
            }
        }
    }

    public void GoUpLanguage() 
    {
        Text mainText = this.languagePanel.transform.Find("Background/Back/MainText").GetComponent<Text>();
        if (this.languages.IndexOf(mainText.text) >= 0)
        {
            this.indexLanguages++;
            if (this.indexLanguages >= this.languages.Count)
            {
                this.indexLanguages = 0;
            }

            mainText.text = this.languages[this.indexLanguages];
            this.PlayClip(this.acceptClip);
        }
        return;
    }

    public void GoDownLanguage() 
    {
        Text mainText = this.languagePanel.transform.Find("Background/Back/MainText").GetComponent<Text>();
        if (this.languages.IndexOf(mainText.text) >= 0)
        {
            this.indexLanguages--;
            if (this.indexLanguages < 0)
            {
                this.indexLanguages = this.languages.Count - 1;
            }

            mainText.text = this.languages[this.indexLanguages];

            this.PlayClip(this.acceptClip);
        }
        return;
    }

    public void ConfirmLanguage() 
    {
        Settings.Current.Language = this.languages[this.indexLanguages];
        Settings.Current.LanguageDefault = true;
        Game.SaveSettings();
        this.languagePanel.SetActive(false);
        this.mainPanel.SetActive(true);
        this.buttonsPanel.SetActive(true);
        Language.Translate();
        this.PlayClip(this.acceptClip);
        return;
    }


    /// <summary>Selects the controller.</summary>
    /// <param name="controller">The controller.</param>
    private void SelectController(string controller)
    {
        this.currentController = controller;

        Settings.Current.Plattform = controller;
        Game.SaveSettings();
        Language.Translate();

        if (Settings.Current.LanguageDefault)
        {
            this.startPanel.SetActive(false);
            this.mainPanel.SetActive(true);
            this.buttonsPanel.SetActive(true);
        }
        else 
        {
            this.startPanel.SetActive(false);
            this.languagePanel.SetActive(true);
            this.UpdateButtons(this.currentController);
        }

        this.PlayClip(this.acceptClip);
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

    /// <summary>Shifts the in menu.</summary>
    private void ShiftInMenu()
    {
        this.UpdateButtons(this.currentController);
        if (this.currentController == "Computer")
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.GoUp();
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.GoDown();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                this.ActionSelector();
            }
        }

        if (this.currentController == "Xbox")
        {
            if (Input.GetAxis("LeftJoystickY") > 0 && this.neutralStick == true)
            {
                this.neutralStick = false;
                this.GoUp();
            }

            if (Input.GetAxis("LeftJoystickY") < 0 && this.neutralStick == true)
            {
                this.neutralStick = false;
                this.GoDown();
            }

            if (Input.GetAxis("LeftJoystickY") == 0)
            {
                this.neutralStick = true;
            }

            if (Input.GetButtonDown("ButtonA"))
            {
                this.ActionSelector();
            }
        }

        if (this.currentController == "Mobile")
        {
            foreach (GameObject selector in this.selectors)
            {
                selector.SetActive(false);
            }
        }
    }

    /// <summary>Goes up in menu.</summary>
    private void GoUp()
    {
        for (int i = 0; i < this.selectors.Count; i++)
        {
            if (this.selectors[i].activeSelf)
            {
                if (this.selectors[i] != this.selectors[0])
                {
                    this.selectors[i].SetActive(false);
                    this.selectors[i - 1].SetActive(true);
                    this.PlayClip(this.acceptClip);
                    return;
                }
            }
        }
    }

    /// <summary>Goes down in menu.</summary>
    private void GoDown()
    {
        for (int i = 0; i < this.selectors.Count; i++)
        {
            if (this.selectors[i].activeSelf)
            {
                if (this.selectors[i] != this.selectors[this.selectors.Count - 1])
                {
                    this.selectors[i].SetActive(false);
                    this.selectors[i + 1].SetActive(true);
                    this.PlayClip(this.acceptClip);
                    return;
                }
            }
        }
    }

    /// <summary>Action the selector.</summary>
    private void ActionSelector() 
    {
        this.PlayClip(this.acceptClip);
        foreach (GameObject selector in this.selectors) 
        {
            if (selector.activeSelf) 
            {
                selector.transform.parent.GetComponent<Button>().onClick.Invoke();
            }
        }
    }

    /// <summary>Dialogues the pop up.</summary>
    private void DialoguePopUp() 
    {
        this.UpdateButtons(this.currentController);
        if (this.currentController == "Computer")
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                this.PlayClip(this.acceptClip);
                if (this.popUpNewAdventure.activeSelf)
                {
                    this.YesNewAdventure();
                }

                if (this.popUpExit.activeSelf) 
                {
                    this.YesExitGame();
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                this.PlayClip(this.cancelClip);
                if (this.popUpNewAdventure.activeSelf)
                {
                    this.NoNewAdventure();
                }

                if (this.popUpExit.activeSelf)
                {
                    this.NoExitGame();
                }
            }
        }

        if (this.currentController == "Xbox")
        {
            if (Input.GetButtonDown("ButtonY"))
            {
                this.PlayClip(this.acceptClip);
                
                if (this.popUpNewAdventure.activeSelf)
                {
                    this.YesNewAdventure();
                }

                if (this.popUpExit.activeSelf)
                {
                    this.YesExitGame();
                }
            }

            if (Input.GetButtonDown("ButtonB"))
            {
                this.PlayClip(this.cancelClip);
                
                if (this.popUpNewAdventure.activeSelf)
                {
                    this.NoNewAdventure();
                }

                if (this.popUpExit.activeSelf)
                {
                    this.NoExitGame();
                }
            }
        }
    }

    /// <summary>Yeses the new adventure.</summary>
    private void YesNewAdventure()
    {
        this.PlayClip(this.acceptClip);

        string language = Settings.Current.Language;
        Game.ResetSettings();
        Game.ResetStats();

        Settings.Current.HasSaveGame = true;
        Settings.Current.LanguageDefault = true;
        Settings.Current.Plattform = this.currentController;
        Settings.Current.Language = language;

        Game.SaveSettings();
        Game.SaveStats();
        SceneManager.LoadScene(this.sceneToLoad);
    }

    /// <summary>Noes the new adventure.</summary>
    private void NoNewAdventure()
    {
        this.PlayClip(this.acceptClip);
        this.mainPanel.SetActive(true);
        this.popUpPanel.SetActive(false);
        this.popUpNewAdventure.SetActive(false);
        Language.Translate();
    }

    /// <summary>Yeses the exit game.</summary>
    private void YesExitGame()
    {
        this.PlayClip(this.acceptClip);
        Application.Quit();
    }

    /// <summary>Noes the exit game.</summary>
    private void NoExitGame()
    {
        this.PlayClip(this.acceptClip);
        this.mainPanel.SetActive(true);
        this.popUpPanel.SetActive(false);
        this.popUpExit.SetActive(false);
        Language.Translate();
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="audioClip">The audio clip.</param>
    private void PlayClip(AudioClip audioClip)
    {
        this.audioSource.clip = audioClip;
        this.audioSource.Play();
    }
}