//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="UpdaterUI.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils.Updater;

/// <summary>Auto update interface</summary>
public class UpdaterUI : MonoBehaviour
{
    /// <summary>The main menu scene</summary>
    private string mainMenuScene = "MainMenu";

    /// <summary>The message</summary>
    private Text message = null;

    /// <summary>The load bar text</summary>
    private Text loadBarText = null;

    /// <summary>The load bar</summary>
    private Scrollbar loadBar = null;

    /// <summary>The checking update</summary>
    [SerializeField]
    private Clef checkingUpdate = Clef.A53;

    /// <summary>The updated</summary>
    [SerializeField]
    private Clef updated = Clef.A54;

    /// <summary>The file</summary>
    [SerializeField]
    private Clef fileSentence = Clef.A55;

    /// <summary>The finish update</summary>
    [SerializeField]
    private Clef finishUpdate = Clef.A56;

    /// <summary>Gets or sets the message.</summary>
    /// <value>The message.</value>
    public Text Message { get => message; set => message = value; }

    /// <summary>Gets or sets the load bar.</summary>
    /// <value>The load bar.</value>
    public Scrollbar LoadBar { get => loadBar; set => loadBar = value; }

    /// <summary>Gets or sets the checking update.</summary>
    /// <value>The checking update.</value>
    public Clef CheckingUpdate { get => checkingUpdate; set => checkingUpdate = value; }

    /// <summary>Gets or sets the main menu scene.</summary>
    /// <value>The main menu scene.</value>
    public string MainMenuScene { get => mainMenuScene; set => mainMenuScene = value; }

    /// <summary>Gets or sets the updated.</summary>
    /// <value>The updated.</value>
    public Clef Updated { get => updated; set => updated = value; }

    /// <summary>Gets or sets the file sentence.</summary>
    /// <value>The file sentence.</value>
    public Clef FileSentence { get => fileSentence; set => fileSentence = value; }

    /// <summary>Gets or sets the finish update.</summary>
    /// <value>The finish update.</value>
    public Clef FinishUpdate { get => finishUpdate; set => finishUpdate = value; }

    /// <summary>Gets or sets the load bar text.</summary>
    /// <value>The load bar text.</value>
    public Text LoadBarText { get => loadBarText; set => loadBarText = value; }

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        message = GameObject.Find("Interface/Panel/Start/Back/Message").GetComponent<Text>();
        loadBar = GameObject.Find("Interface/Panel/Start/Scrollbar").GetComponent<Scrollbar>();
        loadBarText = GameObject.Find("Interface/Panel/Start/Scrollbar/SlidingArea/Process").GetComponent<Text>();

        AutoUpdate autoUpdate = new AutoUpdate("/resources", Application.persistentDataPath);

        if (autoUpdate.NeedUpdate)
        {
            StartCoroutine(UpdateNow(autoUpdate));
        }
        else 
        {
            SceneManager.LoadScene(mainMenuScene);
        }
    }

    /// <summary>Updates the now.</summary>
    /// <param name="autoUpdate">The automatic update.</param>
    /// <returns>Return none</returns>
    private IEnumerator UpdateNow(AutoUpdate autoUpdate) 
    {
        message.text = "Checking Update";

        yield return new WaitForSeconds(1f);

        float totalFile = autoUpdate.NumOfFiles("/resources");
        float total = totalFile;
        float numFile = 1;

        while (total > 0)
        {
            message.text = "File " + numFile + " of " + totalFile;

            loadBar.size = numFile / totalFile;
            loadBarText.text = (int)((numFile / totalFile) * 100) + "%";

            numFile++;
            total--;

            yield return new WaitForSeconds(0.5f);
        }

        message.text = "Finishing update.. ";

        yield return new WaitForSeconds(0.5f);

        autoUpdate.Now();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(mainMenuScene);
    }
}