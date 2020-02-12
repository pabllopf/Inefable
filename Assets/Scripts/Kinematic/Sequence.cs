//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Sequence.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

/// <summary>Define a Sequence of the story.</summary>
[System.Serializable]
public class Sequence
{
    /// <summary>The sequence</summary>
    [SerializeField]
    private readonly GameObject sequence = null;

    /// <summary>The key</summary>
    [SerializeField]
    private readonly Clef key = Clef.A1;

    /// <summary>Sets up.</summary>
    public void SetUp()
    {
        sequence.transform.Find("BlackArea/Text").GetComponent<Text>().text = Language.GetSentence(key);
    }

    /// <summary>Active the sequence.</summary>
    public void Active()
    {
        sequence.SetActive(true);
    }

    /// <summary>Disable the sequence.</summary>
    public void Disable()
    {
        sequence.SetActive(false);
    }
}
