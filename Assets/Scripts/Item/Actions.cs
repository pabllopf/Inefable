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
                SetFullShield(target);
                break;

            case ActionType.SetFullHealth:
                SetFullHealth(target);
                break;

            case ActionType.TakeCoin:
                TakeCoin(target);
                break;

            case ActionType.TakeKey:
                TakeKey(target);
                break;

            case ActionType.Treat:
                Treat(target);
                break;

            case ActionType.SpeedIncrease:
                SpeedIncrease(target);
                break;

            case ActionType.DamageIncrease:
                DamageIncrease(target);
                break;

            case ActionType.Nothing:
                Nothing();
                break;
        }
    }

    /// <summary>Sets the full shield.</summary>
    /// <param name="target">The target.</param>
    private void SetFullShield(GameObject target)
    {
        target.GetComponent<Shield>().SetFull();
    }

    /// <summary>Sets the full health.</summary>
    /// <param name="target">The target.</param>
    private void SetFullHealth(GameObject target)
    {
        target.GetComponent<Health>().TreatFull();
    }

    /// <summary>Takes the coin.</summary>
    /// <param name="target">The target.</param>
    private void TakeCoin(GameObject target)
    {
        target.GetComponent<Wallet>().AddCoin();
    }

    /// <summary>Takes the key.</summary>
    /// <param name="target">The target.</param>
    private void TakeKey(GameObject target)
    {
        target.GetComponent<Keychain>().AddKey();
    }

    /// <summary>Treats the specified target.</summary>
    /// <param name="target">The target.</param>
    private void Treat(GameObject target)
    {
        target.GetComponent<Health>().Treat(Random.Range(3, 10));
    }

    /// <summary>Speeds the increase.</summary>
    /// <param name="target">The target.</param>
    private void SpeedIncrease(GameObject target)
    {
        Debug.LogError("SpeedIncrease Action Not Implemented !!!");
    }

    /// <summary>Damages the increase.</summary>
    /// <param name="target">The target.</param>
    private void DamageIncrease(GameObject target)
    {
        Debug.LogError("DamageIncrease Action Not Implemented !!!");
    }

    /// <summary>Nothings this instance.</summary>
    private void Nothing()
    {
        Debug.LogError("Nothing Action !!!");
    }
}