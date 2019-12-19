//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Inventory.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Manage the inventory of the player.</summary>
public class Inventory : MonoBehaviour
{
    /// <summary>The open</summary>
    private const string Open = "Open";

    /// <summary>The UI animator</summary>
    private Animator uiAnimator = null;

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        this.uiAnimator = this.transform.Find("Interface/Inventory").GetComponent<Animator>();
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (Input.anyKey) 
        {
            if (Input.GetKeyDown(KeyCode.I)) 
            {
                if (this.uiAnimator.GetBool(Open))
                {
                    this.uiAnimator.SetBool(Open, false);
                    return;
                }
                else 
                {
                    this.uiAnimator.SetBool(Open, true);
                    return;
                }
            }
        }
    }
}
