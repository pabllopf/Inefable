//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="PressEffect.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

/// <summary>Press effect of a button.</summary>
[RequireComponent(typeof(Image))]
public class PressEffect : MonoBehaviour
{
    /// <summary>The frame for seconds</summary>
    private readonly float frameForSeconds = 2;

    /// <summary>The computer</summary>
    [SerializeField]
    private readonly Sprite[] computer = new Sprite[2];

    /// <summary>The xbox</summary>
    [SerializeField]
    private readonly Sprite[] xbox = new Sprite[2];

    /// <summary>The type</summary>
    private bool active = true;

    /// <summary>The type</summary>
    private string type = "Computer";

    /// <summary>The sprites</summary>
    private Sprite[] sprites = null;

    /// <summary>The current sprite</summary>
    private Image currentImage;

    /// <summary>The index</summary>
    private int index;

    public void Awake()
    {
        //Game.LoadSettings();
    }

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        if (Settings.Current.Platform == "Mobile")
        {
            gameObject.SetActive(false);
            return;
        }
        else
        {
            currentImage = GetComponent<Image>();
            LoadSprites(type);
        }
    }

    /// <summary>Updates this instance.</summary>
    public void Update()
    {
        if (Settings.Current.Platform == "Mobile")
        {
            gameObject.SetActive(false);
            return;
        }

        if (active)
        {
            index = (int)(Time.timeSinceLevelLoad * frameForSeconds);
            index %= sprites.Length;
            currentImage.sprite = sprites[index];
        }
    }

    /// <summary>Loads the sprites.</summary>
    /// <param name="controller">The controller.</param>
    public void LoadSprites(string controller)
    {
        type = controller;
        if (computer.Length >= 2 && xbox.Length >= 2)
        {
            switch (type)
            {
                case "Computer":
                    sprites = computer;
                    break;
                case "Xbox":
                    sprites = xbox;
                    break;
            }
        }
        else
        {
            Debug.Log("Falta animaciones en " + gameObject.name);
        }
    }

    /// <summary>Starts the effect.</summary>
    public void StartEffect()
    {
        if (Settings.Current.Platform != "Mobile")
        {
            active = true;
        }
    }

    /// <summary>Stops the effect.</summary>
    public void StopEffect()
    {
        if (Settings.Current.Platform != "Mobile")
        {
            if (currentImage != null && sprites != null)
            {
                currentImage.sprite = sprites[1];
                active = false;
            }
        }
    }
}
