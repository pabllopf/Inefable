//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Actions.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Actions of items</summary>
public class Actions 
{
    /// <summary>The action type</summary>
    private readonly ActionType actionType;

    /// <summary>Initializes a new instance of the <see cref="Actions"/> class.</summary>
    /// <param name="actionType">Type of the action.</param>
    private Actions(ActionType actionType) 
    {
        this.actionType = actionType;
    }

    /// <summary>Invokes the specified action type.</summary>
    /// <param name="actionType">Type of the action.</param>
    /// <returns>Return none</returns>
    public static Actions Invoke(ActionType actionType) 
    {
        return new Actions(actionType);
    }

    /// <summary>Ins the specified target.</summary>
    /// <param name="target">The target.</param>
    public void In(GameObject target)
    {
        switch (actionType) 
        {
            case ActionType.SetFullShield:
                target.GetComponent<Shield>().SetFull();
                break;

            case ActionType.SetFullHealth:
                target.GetComponent<Health>().TreatFull();
                break;
        }
    }
}
