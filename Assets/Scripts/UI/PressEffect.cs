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
    /// <summary>The computer</summary>
    [SerializeField]
    private Sprite[] computer = new Sprite[2];

    /// <summary>The xbox</summary>
    [SerializeField]
    private Sprite[] xbox = new Sprite[2];

    /// <summary>The type</summary>
    public bool active = true;

    /// <summary>The type</summary>
    private string type = "Computer";

    /// <summary>The sprites</summary>
    private Sprite[] sprites = null;

    /// <summary>The frame for seconds</summary>
    private float frameForSeconds = 2;

    /// <summary>The current sprite</summary>
    private Image currentImage;

    /// <summary>The index</summary>
    private int index;

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        if (Settings.Current.Plattform == "Mobile") 
        {
            this.gameObject.SetActive(false);
            return;
        }

        this.currentImage = this.GetComponent<Image>();
        this.LoadSprites(this.type);
    }

    /// <summary>Updates this instance.</summary>
    public void Update()
    {
        if (this.active) 
        {
            this.index = (int)(Time.timeSinceLevelLoad * this.frameForSeconds);
            this.index %= this.sprites.Length;
            this.currentImage.sprite = this.sprites[this.index];
        }
    }

    /// <summary>Loads the sprites.</summary>
    /// <param name="controller">The controller.</param>
    public void LoadSprites(string controller)
    {
        this.type = controller;
        if (this.computer.Length >= 2 && this.xbox.Length >= 2)
        {
            switch (this.type)
            {
                case "Computer":
                    this.sprites = this.computer;
                    break;
                case "Xbox":
                    this.sprites = this.xbox;
                    break;
            }
        }
        else
        {
            Debug.Log("Falta animaciones en " + this.gameObject.name);
        }
    }

    /// <summary>Starts the effect.</summary>
    public void StartEffect() 
    {
        this.active = true;
    }

    /// <summary>Stops the effect.</summary>
    public void StopEffect()
    {
        this.active = false;
        this.currentImage.sprite = this.sprites[1];
    }
}
