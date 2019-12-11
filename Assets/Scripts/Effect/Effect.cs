//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Effect.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

/// <summary>Create effects in the game.</summary>
public class Effect : MonoBehaviour
{
    /// <summary>Plays the specified name.</summary>
    /// <param name="name">The name.</param>
    /// <param name="amount">The amount.</param>
    /// <param name="obj">The object.</param>
    public static void Play(string name, int amount, Transform obj)
    {
        GameObject effect = Instantiate((GameObject)Resources.Load("Particles/" + name), obj.position, Quaternion.Euler(0, 0, 0));
        effect.transform.SetParent(obj.Find("Interface"));
        effect.GetComponent<Text>().text = amount.ToString();
        effect.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0);
    }
}
