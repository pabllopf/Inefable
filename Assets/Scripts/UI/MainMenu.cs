//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="MainMenu.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>Manage the main manu of the game. </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>The scene of new adventure</summary>
    [SerializeField]
    private string sceneOfNewAdventure = "Foreword";

    /// <summary>The scene of continue adventure</summary>
    [SerializeField]
    private string sceneOfContinueAdventure = "Town";

    /// <summary>The neutral stick</summary>
    private bool neutralStick = true;

    /// <summary>The is updated</summary>
    private bool isUpdated = true;

    /// <summary>The selectors</summary>
    private List<GameObject> selectors = new List<GameObject>();

    /// <summary>The accept clip</summary>
    [SerializeField]
    private AudioClip acceptClip = null;

    /// <summary>The cancel clip</summary>
    [SerializeField]
    private AudioClip cancelClip = null;

    /// <summary>The audio source</summary>
    private AudioSource audioSource = null;

    /// <summary>The question start new adventure</summary>
    private GameObject questionStartNewAdventure = null;

    /// <summary>The question exit of the game</summary>
    private GameObject questionExitOfTheGame = null;

    /// <summary>The question last update</summary>
    private GameObject questionLastUpdate = null;

    /// <summary>The pop up panel</summary>
    private GameObject popUpPanel = null;

    /// <summary>Creates new buttons.</summary>
    private GameObject newButtons = null;

    /// <summary>The normal buttons</summary>
    private GameObject normalButtons = null;

    /// <summary>The start panel</summary>
    private GameObject startPanel = null;

    /// <summary>The main panel</summary>
    private GameObject mainPanel = null;

    /// <summary>Gets or sets the scene to load is new adventure.</summary>
    /// <value>The scene to load is new adventure.</value>
    public string SceneOfNewAdventure { get => sceneOfNewAdventure; set => sceneOfNewAdventure = value; }

    /// <summary>Gets or sets the scene of continue adventure.</summary>
    /// <value>The scene of continue adventure.</value>
    public string SceneOfContinueAdventure { get => sceneOfContinueAdventure; set => sceneOfContinueAdventure = value; }

    /// <summary>Gets or sets a value indicating whether [neutral stick].</summary>
    /// <value>
    /// <c>true</c> if [neutral stick]; otherwise, <c>false</c>.</value>
    public bool NeutralStick { get => neutralStick; set => neutralStick = value; }

    /// <summary>Gets or sets the accept clip.</summary>
    /// <value>The accept clip.</value>
    public AudioClip AcceptClip { get => acceptClip; set => acceptClip = value; }

    /// <summary>Gets or sets the audio source.</summary>
    /// <value>The audio source.</value>
    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }

    /// <summary>Gets or sets the cancel clip.</summary>
    /// <value>The cancel clip.</value>
    public AudioClip CancelClip { get => cancelClip; set => cancelClip = value; }

    /// <summary>Creates new adventure.</summary>
    public void NewAdventure()
    {
        popUpPanel.SetActive(true);
        questionStartNewAdventure.SetActive(true);
        mainPanel.SetActive(false);
    }

    /// <summary>Continues the adventure.</summary>
    public void ContinueAdventure()
    {
        SceneManager.LoadScene(SceneOfContinueAdventure);
    }

    /// <summary>Exits to the game.</summary>
    public void ExitToTheGame()
    {
        popUpPanel.SetActive(true);
        questionExitOfTheGame.SetActive(true);
        mainPanel.SetActive(false);
    }

    /// <summary>Awakes this instance.</summary>
    private void Awake()
    {
        if (Application.isBatchMode)
        {
            SceneManager.LoadScene("Town");
        }

        Settings.Load();
        Language.Translate();
        Cursor.visible = false;
    }

    /// <summary>Checks the version.</summary>
    private void CheckVersion() 
    {
        try
        {
            WebClient client = new WebClient();

            string versionServer = client.DownloadString("https://pabllopf.github.io/Game-Inefable/version.html");
            if (versionServer != Application.version) 
            {
                isUpdated = false;
            }
        }
        catch (WebException webEx)
        {
            Console.WriteLine(webEx.ToString());
            if (webEx.Status == WebExceptionStatus.ConnectFailure)
            {
                Debug.Log("You havent got connection.");
                isUpdated = true;
            }
        }
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        startPanel = GameObject.FindWithTag("StartPanel");
        startPanel.SetActive(true);

        newButtons = GameObject.FindWithTag("NewButtons");
        newButtons.SetActive(Settings.Current.IsTheFirstTime ? true : false);

        normalButtons = GameObject.FindWithTag("NormalButtons");
        normalButtons.SetActive((!Settings.Current.IsTheFirstTime) ? true : false);

        selectors = ConfigSelectors();

        mainPanel = GameObject.FindWithTag("MainPanel");
        mainPanel.SetActive(false);

        questionStartNewAdventure = GameObject.FindWithTag("QuestionNewAdventure");
        questionStartNewAdventure.transform.Find("Question/Yes").GetComponent<Button>().onClick.AddListener(() => { YesNewAdventure(); });
        questionStartNewAdventure.transform.Find("Question/No").GetComponent<Button>().onClick.AddListener(() => { NoNewAdventure(); });
        questionStartNewAdventure.SetActive(false);

        questionExitOfTheGame = GameObject.FindWithTag("QuestionExitGame");
        questionExitOfTheGame.transform.Find("Question/Yes").GetComponent<Button>().onClick.AddListener(() => { YesExitToTheGame(); });
        questionExitOfTheGame.transform.Find("Question/No").GetComponent<Button>().onClick.AddListener(() => { NoExitToTheGame(); });
        questionExitOfTheGame.SetActive(false);

        popUpPanel = GameObject.FindWithTag("PopUpPanel");
        popUpPanel.SetActive(false);

        if (GameObject.Find("NetworkManager")) 
        {
            Destroy(GameObject.Find("NetworkManager").gameObject);
        }
    }

    /// <summary>Gets the selectors.</summary>
    /// <returns>Return the selectors.</returns>
    private List<GameObject> ConfigSelectors()
    {
        List<GameObject> result = new List<GameObject>();

        if (Settings.Current.IsTheFirstTime)
        {
            result = new List<GameObject>
            {
                newButtons.transform.Find("NewAdventure/Selector").gameObject,
                newButtons.transform.Find("Exit/Selector").gameObject
            };

            newButtons.transform.Find("NewAdventure").GetComponent<Button>().onClick.AddListener(() => { NewAdventure(); });
            newButtons.transform.Find("Exit").GetComponent<Button>().onClick.AddListener(() => { ExitToTheGame(); });
        }
        else
        {
            result = new List<GameObject>
            {
                normalButtons.transform.Find("NewAdventure/Selector").gameObject,
                normalButtons.transform.Find("ContinueAdventure/Selector").gameObject,
                normalButtons.transform.Find("Exit/Selector").gameObject
            };

            normalButtons.transform.Find("NewAdventure").GetComponent<Button>().onClick.AddListener(() => { NewAdventure(); });
            normalButtons.transform.Find("ContinueAdventure").GetComponent<Button>().onClick.AddListener(() => { ContinueAdventure(); });
            normalButtons.transform.Find("Exit").GetComponent<Button>().onClick.AddListener(() => { ExitToTheGame(); });
        }

        return result;
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (isUpdated == true)
        {
            if (Settings.Current.Platform.Equals("Mobile"))
            {
                selectors.ForEach(i => i.SetActive(false));
            }

            if (startPanel.activeSelf)
            {
                if (Input.anyKey || Input.touchCount > 0)
                {
                    startPanel.SetActive(false);
                    mainPanel.SetActive(true);
                    Sound.Play(AcceptClip, AudioSource);
                }

                return;
            }

            if (mainPanel.activeSelf)
            {
                UseTheMenu();
                return;
            }

            if (popUpPanel.activeSelf)
            {
                CheckAnswer();
            }
        }
        else
        {
            if (Settings.Current.Platform.Equals("Mobile"))
            {
                selectors.ForEach(i => i.SetActive(false));
            }

            if (startPanel.activeSelf)
            {
                startPanel.SetActive(false);
                popUpPanel.SetActive(true);
                questionLastUpdate.SetActive(true);
                Sound.Play(AcceptClip, AudioSource);
                return;
            }

            if (popUpPanel.activeSelf)
            {
                CheckAnswer();
                return;
            }
        }
    }

    /// <summary>Uses the menu.</summary>
    private void UseTheMenu()
    {
        if (Input.GetAxis("LeftJoystickY") == 0)
        {
            NeutralStick = true;
        }

        if (Input.GetKeyDown(KeyCode.W) || (Input.GetAxis("LeftJoystickY") > 0 && NeutralStick == true))
        {
            NeutralStick = false;
            GoUpInTheMenu();
            return;
        }

        if (Input.GetKeyDown(KeyCode.S) || (Input.GetAxis("LeftJoystickY") < 0 && NeutralStick == true))
        {
            NeutralStick = false;
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

            Sound.Play(AcceptClip, AudioSource);
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

            Sound.Play(AcceptClip, AudioSource);
        }
    }

    /// <summary>Presses the button selected.</summary>
    private void PressButtonSelected()
    {
        selectors
            .Find(i => i.activeSelf)
            .transform.parent.GetComponent<Button>()
            .onClick.Invoke();

        Sound.Play(AcceptClip, AudioSource);
    }

    /// <summary>Checks the answer.</summary>
    private void CheckAnswer()
    {
        if (Input.GetKeyDown(KeyCode.Y) || Input.GetButtonDown("ButtonY"))
        {
            if (questionStartNewAdventure.activeSelf)
            {
                YesNewAdventure();
                return;
            }

            if (questionExitOfTheGame.activeSelf)
            {
                YesExitToTheGame();
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("ButtonB"))
        {
            if (questionStartNewAdventure.activeSelf)
            {
                NoNewAdventure();
                return;
            }

            if (questionExitOfTheGame.activeSelf)
            {
                NoExitToTheGame();
                return;
            }

            if (questionLastUpdate.activeSelf) 
            {
                YesExitToTheGame();
                return;
            }
        }
    }

    /// <summary>Yeses the new adventure.</summary>
    private void YesNewAdventure()
    {
        Settings.Current.IsTheFirstTime = false;
        Settings.Save();
        Sound.Play(AcceptClip, AudioSource);
        SceneManager.LoadScene(SceneOfNewAdventure);
    }

    /// <summary>Noes the new adventure.</summary>
    private void NoNewAdventure()
    {
        questionStartNewAdventure.SetActive(false);
        popUpPanel.SetActive(false);
        mainPanel.SetActive(true);

        Sound.Play(CancelClip, AudioSource);
    }

    /// <summary>Yeses the exit to the game.</summary>
    private void YesExitToTheGame()
    {
        Sound.Play(AcceptClip, AudioSource);
        Application.Quit();
    }

    /// <summary>Noes the exit to the game.</summary>
    private void NoExitToTheGame()
    {
        questionExitOfTheGame.SetActive(false);
        popUpPanel.SetActive(false);
        mainPanel.SetActive(true);

        Sound.Play(CancelClip, AudioSource);
    }
}