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
    private GameObject sequence = null;

    /// <summary>The key</summary>
    [SerializeField]
    private Key key = Key.A1;

    /// <summary>The sentence</summary>
    private GameObject sentence;

    /// <summary>Active the sequence.</summary>
    public void Active() 
    {
        this.sequence.SetActive(true);
        this.sentence = this.sequence.transform.Find("BlackArea/Text").gameObject;
        this.sentence.GetComponent<Text>().text = Language.GetSentence(this.key);
    }

    /// <summary>Disable the sequence.</summary>
    public void Disable() => this.sequence.SetActive(false);
}
