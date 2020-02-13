//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="MainMenu.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>Main menu of the game.</summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>The name scene to load</summary>
    [SerializeField]
    private readonly string sceneToLoad = "House";

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
    private readonly AudioClip acceptClip = null;

    /// <summary>The cancel clip</summary>
    [SerializeField]
    private readonly AudioClip cancelClip = null;

    /// <summary>Awakes this instance.</summary>
    public void Awake()
    {
        //Game.LoadSettings();
        //Game.LoadStats();
        Cursor.visible = false;

        startPanel = GameObject.FindGameObjectWithTag("StartPanel").gameObject;
        languagePanel = GameObject.FindGameObjectWithTag("LanguagePanel").gameObject;
        mainPanel = GameObject.FindGameObjectWithTag("MainPanel").gameObject;
        InitPopUpPanel();
        InitButtonsPanel();
        InitButtonsLanguage();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        Language.Translate();
        GameObject.FindGameObjectWithTag("NewButtons").SetActive(false);
        GameObject.FindGameObjectWithTag("NormalButtons").SetActive(false);
        popUpPanel.SetActive(false);
        mainPanel.SetActive(false);
        languagePanel.SetActive(false);

        languages = new List<string>
        {
            "English",
            "Español",
            "French"
        };
    }

    /// <summary>Creates new adventure.</summary>
    public void NewAdventure()
    {
        PlayClip(acceptClip);
        mainPanel.SetActive(false);
        popUpPanel.SetActive(true);
        popUpNewAdventure.SetActive(true);
        Language.Translate();
    }

    /// <summary>Continues the adventure.</summary>
    public void ContinueAdventure()
    {
        PlayClip(acceptClip);
        //Game.LoadSettings();
        SceneManager.LoadScene(sceneToLoad);
    }

    /// <summary>Communities this instance.</summary>
    public void Community()
    {
        PlayClip(acceptClip);
    }

    /// <summary>Exits the game.</summary>
    public void ExitGame()
    {
        PlayClip(acceptClip);
        popUpPanel.SetActive(true);
        popUpExit.SetActive(true);
        mainPanel.SetActive(false);
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
        //if (Settings.Current.HasSaveGame)
        if (true)
        {
            buttonsPanel = GameObject.FindGameObjectWithTag("NormalButtons").gameObject;

            selectors = new List<GameObject>(4)
            {
                buttonsPanel.transform.Find("NewAdventure/Selector").gameObject,
                buttonsPanel.transform.Find("Continue/Selector").gameObject,
                buttonsPanel.transform.Find("Community/Selector").gameObject,
                buttonsPanel.transform.Find("Exit/Selector").gameObject
            };

            buttonsPanel.transform.Find("NewAdventure").GetComponent<Button>().onClick.AddListener(() => { NewAdventure(); });
            buttonsPanel.transform.Find("Continue").GetComponent<Button>().onClick.AddListener(() => { ContinueAdventure(); });
            buttonsPanel.transform.Find("Community").GetComponent<Button>().onClick.AddListener(() => { Community(); });
            buttonsPanel.transform.Find("Exit").GetComponent<Button>().onClick.AddListener(() => { ExitGame(); });
        }
        else
        {
            buttonsPanel = GameObject.FindGameObjectWithTag("NewButtons").gameObject;

            selectors = new List<GameObject>(3)
            {
                buttonsPanel.transform.Find("NewAdventure/Selector").gameObject,
                buttonsPanel.transform.Find("Community/Selector").gameObject,
                buttonsPanel.transform.Find("Exit/Selector").gameObject
            };

            buttonsPanel.transform.Find("NewAdventure").GetComponent<Button>().onClick.AddListener(() => { NewAdventure(); });
            buttonsPanel.transform.Find("Community").GetComponent<Button>().onClick.AddListener(() => { Community(); });
            buttonsPanel.transform.Find("Exit").GetComponent<Button>().onClick.AddListener(() => { ExitGame(); });
        }
    }

    /// <summary>Initializes the pop up panel.</summary>
    private void InitPopUpPanel()
    {
        popUpPanel = GameObject.FindGameObjectWithTag("PopUpPanel").gameObject;

        popUpExit = popUpPanel.transform.Find("ExitGame").gameObject;
        popUpExit.transform.Find("BackGround/Yes").GetComponent<Button>().onClick.AddListener(() => { YesExitGame(); });
        popUpExit.transform.Find("BackGround/No").GetComponent<Button>().onClick.AddListener(() => { NoExitGame(); });
        popUpExit.SetActive(false);

        popUpNewAdventure = popUpPanel.transform.Find("NewAdventure").gameObject;
        popUpNewAdventure.transform.Find("BackGround/Yes").GetComponent<Button>().onClick.AddListener(() => { YesNewAdventure(); });
        popUpNewAdventure.transform.Find("BackGround/No").GetComponent<Button>().onClick.AddListener(() => { NoNewAdventure(); });
        popUpNewAdventure.SetActive(false);
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (startPanel.activeSelf)
        {
            DetectController();
            return;
        }

        if (languagePanel.activeSelf)
        {
            DetectLanguage();
            //Settings.Current.Language = languages[indexLanguages];
            //Game.SaveSettings();
            Language.Translate();
            return;
        }

        if (mainPanel.activeSelf && !popUpPanel.activeSelf)
        {
            ShiftInMenu();
            return;
        }

        if (popUpPanel.activeSelf)
        {
            DialoguePopUp();
        }
    }

    /// <summary>Detects the controller.</summary>
    private void DetectController()
    {
        if (startPanel.activeSelf)
        {
            if (Input.touchCount > 0)
            {
                SelectController("Mobile");
                return;
            }

            if (Input.GetJoystickNames().Length > 0 && Input.anyKey)
            {
                SelectController("Xbox");
                return;
            }

            if (Input.anyKey)
            {
                SelectController("Computer");
                return;
            }
        }

        return;
    }

    /// <summary>Detects the language.</summary>
    private void DetectLanguage()
    {
        if (currentController == "Computer")
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                GoUpLanguage();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                GoDownLanguage();
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                ConfirmLanguage();
            }
        }

        if (currentController == "Xbox")
        {
            if (Input.GetAxis("LeftJoystickY") > 0 && neutralStick == true)
            {
                neutralStick = false;
                GoUpLanguage();
            }

            if (Input.GetAxis("LeftJoystickY") < 0 && neutralStick == true)
            {
                neutralStick = false;
                GoDownLanguage();
            }

            if (Input.GetAxis("LeftJoystickY") == 0)
            {
                neutralStick = true;
            }

            if (Input.GetButtonDown("ButtonY"))
            {
                ConfirmLanguage();
            }
        }
    }

    public void GoUpLanguage()
    {
        Text mainText = languagePanel.transform.Find("Background/Back/MainText").GetComponent<Text>();
        if (languages.IndexOf(mainText.text) >= 0)
        {
            indexLanguages++;
            if (indexLanguages >= languages.Count)
            {
                indexLanguages = 0;
            }

            mainText.text = languages[indexLanguages];
            PlayClip(acceptClip);
        }
        return;
    }

    public void GoDownLanguage()
    {
        Text mainText = languagePanel.transform.Find("Background/Back/MainText").GetComponent<Text>();
        if (languages.IndexOf(mainText.text) >= 0)
        {
            indexLanguages--;
            if (indexLanguages < 0)
            {
                indexLanguages = languages.Count - 1;
            }

            mainText.text = languages[indexLanguages];

            PlayClip(acceptClip);
        }
        return;
    }

    public void ConfirmLanguage()
    {
        Settings.Current.Language = languages[indexLanguages];
        //Settings.Current.LanguageDefault = true;
        //Game.SaveSettings();
        languagePanel.SetActive(false);
        mainPanel.SetActive(true);
        buttonsPanel.SetActive(true);
        Language.Translate();
        PlayClip(acceptClip);
        return;
    }


    /// <summary>Selects the controller.</summary>
    /// <param name="controller">The controller.</param>
    private void SelectController(string controller)
    {
        currentController = controller;

        Game.SaveVar(controller).InFolder("Settings").WithName("Platform");
        //Game.SaveSettings();
        Language.Translate();

        //if (Settings.Current.LanguageDefault)
        if(true)
        {
            startPanel.SetActive(false);
            mainPanel.SetActive(true);
            buttonsPanel.SetActive(true);
        }
        else
        {
            startPanel.SetActive(false);
            languagePanel.SetActive(true);
            UpdateButtons(currentController);
        }

        PlayClip(acceptClip);
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
        UpdateButtons(currentController);
        if (currentController == "Computer")
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                GoUp();
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                GoDown();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                ActionSelector();
            }
        }

        if (currentController == "Xbox")
        {
            if (Input.GetAxis("LeftJoystickY") > 0 && neutralStick == true)
            {
                neutralStick = false;
                GoUp();
            }

            if (Input.GetAxis("LeftJoystickY") < 0 && neutralStick == true)
            {
                neutralStick = false;
                GoDown();
            }

            if (Input.GetAxis("LeftJoystickY") == 0)
            {
                neutralStick = true;
            }

            if (Input.GetButtonDown("ButtonA"))
            {
                ActionSelector();
            }
        }

        if (currentController == "Mobile")
        {
            foreach (GameObject selector in selectors)
            {
                selector.SetActive(false);
            }
        }
    }

    /// <summary>Goes up in menu.</summary>
    private void GoUp()
    {
        for (int i = 0; i < selectors.Count; i++)
        {
            if (selectors[i].activeSelf)
            {
                if (selectors[i] != selectors[0])
                {
                    selectors[i].SetActive(false);
                    selectors[i - 1].SetActive(true);
                    PlayClip(acceptClip);
                    return;
                }
            }
        }
    }

    /// <summary>Goes down in menu.</summary>
    private void GoDown()
    {
        for (int i = 0; i < selectors.Count; i++)
        {
            if (selectors[i].activeSelf)
            {
                if (selectors[i] != selectors[selectors.Count - 1])
                {
                    selectors[i].SetActive(false);
                    selectors[i + 1].SetActive(true);
                    PlayClip(acceptClip);
                    return;
                }
            }
        }
    }

    /// <summary>Action the selector.</summary>
    private void ActionSelector()
    {
        PlayClip(acceptClip);
        foreach (GameObject selector in selectors)
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
        UpdateButtons(currentController);
        if (currentController == "Computer")
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                PlayClip(acceptClip);
                if (popUpNewAdventure.activeSelf)
                {
                    YesNewAdventure();
                }

                if (popUpExit.activeSelf)
                {
                    YesExitGame();
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                PlayClip(cancelClip);
                if (popUpNewAdventure.activeSelf)
                {
                    NoNewAdventure();
                }

                if (popUpExit.activeSelf)
                {
                    NoExitGame();
                }
            }
        }

        if (currentController == "Xbox")
        {
            if (Input.GetButtonDown("ButtonY"))
            {
                PlayClip(acceptClip);

                if (popUpNewAdventure.activeSelf)
                {
                    YesNewAdventure();
                }

                if (popUpExit.activeSelf)
                {
                    YesExitGame();
                }
            }

            if (Input.GetButtonDown("ButtonB"))
            {
                PlayClip(cancelClip);

                if (popUpNewAdventure.activeSelf)
                {
                    NoNewAdventure();
                }

                if (popUpExit.activeSelf)
                {
                    NoExitGame();
                }
            }
        }
    }

    /// <summary>Yeses the new adventure.</summary>
    private void YesNewAdventure()
    {
        PlayClip(acceptClip);

        string language = Settings.Current.Language;
        //Game.ResetSettings();
        //Game.ResetStats();

        //Settings.Current.HasSaveGame = true;
        //Settings.Current.LanguageDefault = true;
        Settings.Current.Platform = currentController;
        Settings.Current.Language = language;

        //Game.SaveSettings();
        //Game.SaveStats();
        SceneManager.LoadScene(sceneToLoad);
    }

    /// <summary>Noes the new adventure.</summary>
    private void NoNewAdventure()
    {
        PlayClip(acceptClip);
        mainPanel.SetActive(true);
        popUpPanel.SetActive(false);
        popUpNewAdventure.SetActive(false);
        Language.Translate();
    }

    /// <summary>Yeses the exit game.</summary>
    private void YesExitGame()
    {
        PlayClip(acceptClip);
        Application.Quit();
    }

    /// <summary>Noes the exit game.</summary>
    private void NoExitGame()
    {
        PlayClip(acceptClip);
        mainPanel.SetActive(true);
        popUpPanel.SetActive(false);
        popUpExit.SetActive(false);
        Language.Translate();
    }

    /// <summary>Plays the clip.</summary>
    /// <param name="audioClip">The audio clip.</param>
    private void PlayClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}