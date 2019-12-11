//-----------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Sequence.cs" company="UnMedioStudio">Open Source</copyright>
//-----------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

/// <summary>Define a Sequence of the story.</summary>
[System.Serializable]
public class Sequence
{
    /// <summary>The sequence</summary>
    [SerializeField]
    private GameObject sequence = null;

    /// <summary>The sentence</summary>
    private GameObject sentence;

    /// <summary>The audio</summary>
    [Range(0, 50)]
    [SerializeField]
    private int audio = 0;

    /// <summary>Active the sequence.</summary>
    public void Active() 
    {
        this.sequence.SetActive(true);
        this.sentence = sequence.transform.Find("BlackArea/Text").gameObject;
        this.sentence.GetComponent<Text>().text = Language.GetSentence(sentence.tag);
    }

    /// <summary>Disable the sequence.</summary>
    public void Disable()
    {
        this.sequence.SetActive(false);
    }

    /// <summary>Gets the audio.</summary>
    /// <returns>The number audio to play.</returns>
    public int GetAudio() 
    {
        return this.audio;
    }
}
