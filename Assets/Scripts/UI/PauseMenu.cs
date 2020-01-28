//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="PauseMenu.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>Manage the pause menu of the game.</summary>
public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad = "Start";

    private bool neutralStick = true;

    private List<GameObject> selectors = null;


    [SerializeField]
    private AudioClip pressButton = null;

    private GameObject PauseMenuPanel => this.transform.Find("Interface/PauseMenu").gameObject;
    
    private GameObject PauseMenuButton => this.transform.Find("Interface/Mobile/PauseButton").gameObject;
    private GameObject ContinueButton => this.transform.Find("Interface/PauseMenu/Continue").gameObject;
    private GameObject ExitButton => this.transform.Find("Interface/PauseMenu/Exit").gameObject;

    private AudioSource AudioSource => this.transform.GetComponent<AudioSource>();


    public void Pause() 
    {
        this.PlayClip(this.pressButton);

        if (PauseMenuPanel.activeSelf)
        {
            PauseMenuPanel.SetActive(false);
        }
        else 
        {
            PauseMenuPanel.SetActive(true);
        }

        return;
    }

    public void Continue() 
    {
        this.PlayClip(this.pressButton);

        if (PauseMenuPanel.activeSelf)
        {
            PauseMenuPanel.SetActive(false);
        }
        
        return;
    }

    public void Exit()
    {
        this.PlayClip(this.pressButton);

        if (PauseMenuPanel.activeSelf)
        {
            Game.SaveSettings();
            Game.SaveStats();
            SceneManager.LoadScene(this.sceneToLoad);
        }
        
        return;
    }

    /// <summary>Awakes this instance.</summary>
    private void Awake() => Game.LoadSettings();

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        Language.Translate();
        this.UpdateButtons(Settings.Current.Plattform);

        this.AddActionsToButtons();
        this.AddTheSelectors();

        PauseMenuPanel.SetActive(false);
    }

    private void AddActionsToButtons() 
    {
        PauseMenuButton.GetComponent<Button>().onClick.AddListener(() => { Pause(); });
        ContinueButton.GetComponent<Button>().onClick.AddListener(() => { Continue(); });
        ExitButton.GetComponent<Button>().onClick.AddListener(() => { Exit(); });
    }

    private void AddTheSelectors()
    {
        this.selectors = new List<GameObject>(2)
        {
            ContinueButton.transform.Find("Selector").gameObject,
            ExitButton.transform.Find("Selector").gameObject
        };

        if (Settings.Current.Plattform == "Mobile") 
        {
            this.DisableSelectors(this.selectors);
        }
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (Settings.Current.Plattform == "Computer") 
        {
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                this.Pause();
            }

            if (PauseMenuPanel.activeSelf) 
            {
                this.UpdateButtons(Settings.Current.Plattform);

                if (Input.GetKeyDown(KeyCode.W))
                {
                    this.GoUp(selectors);
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    this.GoDown(selectors);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    this.Action(selectors);
                }
            }
        }

        if (Settings.Current.Plattform == "Xbox")
        {
            if (Input.GetButtonDown("ButtonStart"))
            {
                this.Pause();
            }

            if (PauseMenuPanel.activeSelf)
            {
                this.UpdateButtons(Settings.Current.Plattform);

                if (Input.GetAxis("LeftJoystickY") > 0 && this.neutralStick == true)
                {
                    this.neutralStick = false;
                    this.GoUp(selectors);
                }

                if (Input.GetAxis("LeftJoystickY") < 0 && this.neutralStick == true)
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
                    this.Action(selectors);
                }
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
                    return;
                }
            }
        }
    }

    /// <summary>Actions the specified selectors.</summary>
    /// <param name="selectors">The selectors.</param>
    private void Action(List<GameObject> selectors) => selectors.Find(i => i.activeSelf).transform.parent.GetComponent<Button>().onClick.Invoke();

    /// <summary>Disables the selectors.</summary>
    private void DisableSelectors(List<GameObject> selectors) => selectors.ForEach(i => i.SetActive(false));

    /// <summary>Updates the buttons.</summary>
    /// <param name="controller">The controller.</param>
    private void UpdateButtons(string controller) => FindObjectsOfType<PressEffect>().ToList().ForEach(i => i.GetComponent<PressEffect>().LoadSprites(controller));

    /// <summary>Plays the clip.</summary>
    /// <param name="audioClip">The audio clip.</param>
    private void PlayClip(AudioClip audioClip)
    {
        AudioSource.clip = audioClip;
        AudioSource.Play();
    }
}