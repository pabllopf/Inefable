//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="PressEffect.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manage the buttons effects of the interface. </summary>
public class PressEffect : MonoBehaviour
{
    /// <summary>The frame for seconds</summary>
    private const int FrameForSeconds = 2;

    /// <summary>The image</summary>
    private Image image = null;

    /// <summary>The computer</summary>
    [SerializeField]
    private Sprite[] computer = new Sprite[2];

    /// <summary>The xbox</summary>
    [SerializeField]
    private Sprite[] xbox = new Sprite[2];

    /// <summary>Gets or sets the computer.</summary>
    /// <value>The computer.</value>
    public Sprite[] Computer
    {
        get => computer;
        set => computer = value;
    }

    /// <summary>Gets or sets the xbox.</summary>
    /// <value>The xbox.</value>
    public Sprite[] Xbox
    {
        get => xbox;
        set => xbox = value;
    }

    /// <summary>Gets the play in computer.</summary>
    /// <value>The play in computer.</value>
    private Sprite PlayInComputer => Computer[(int)(Time.timeSinceLevelLoad * FrameForSeconds) % Computer.Length];

    /// <summary>Gets the play in xbox.</summary>
    /// <value>The play in xbox.</value>
    private Sprite PlayInXbox => Xbox[(int)(Time.timeSinceLevelLoad * FrameForSeconds) % Xbox.Length];

    /// <summary>Start this instance.</summary>
    private void Start()
    {
        image = GetComponent<Image>();
    }

    /// <summary>Update this instance.</summary>
    private void Update()
    {
        if (Settings.Current != null)
        {
            if (Settings.Current.Platform.Equals("Mobile"))
            {
                image.enabled = false;
                return;
            }

            if (Settings.Current.Platform.Equals("Computer"))
            {
                image.enabled = true;
                image.sprite = PlayInComputer;
                return;
            }

            if (Settings.Current.Platform.Equals("Xbox"))
            {
                image.enabled = true;
                image.sprite = PlayInXbox;
                return;
            }
        }
    }
}