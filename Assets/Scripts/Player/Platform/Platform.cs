//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Platform.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Detect the platform</summary>
public class Platform : MonoBehaviour
{
    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Settings.Current.Platform = "Mobile";
            return;
        }

        if (Input.GetAxisRaw("LeftJoystickX") != 0 || Input.GetAxisRaw("LeftJoystickY") != 0 || Input.GetButton("ButtonA") || Input.GetButton("ButtonB") || Input.GetButton("ButtonY") || Input.GetButton("ButtonX") || Input.GetButton("ButtonStart"))
        {
            Settings.Current.Platform = "Xbox";
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
        {
            Settings.Current.Platform = "Computer";
            return;
        }
    }
}