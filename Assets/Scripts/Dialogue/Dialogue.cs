//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Dialogue.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Dialogue class</summary>
[System.Serializable]
public class Dialogue
{
    /// <summary>The name</summary>
    [SerializeField]
    private readonly string name = string.Empty;

    /// <summary>The index</summary>
    private int index = 0;

    /// <summary>The sentences</summary>
    [SerializeField]
    private readonly Clef[] sentences = null;

    /// <summary>Get the name.</summary>
    /// <returns>The name.</returns>
    public string GetName()
    {
        return name;
    }

    /// <summary>Gets the sentence.</summary>
    /// <returns>the sentence</returns>
    public string GetSentence()
    {
        return Language.GetSentence(sentences[index]);
    }

    /// <summary>Determines whether this instance has next.</summary>
    /// <returns>
    /// <c>true</c> if this instance has next; otherwise, <c>false</c>.</returns>
    public bool HasNext()
    {
        return (index < sentences.Length) ? true : false;
    }

    /// <summary>Next this instance.</summary>
    public void Next()
    {
        index++;
    }

    /// <summary>Resets this instance.</summary>
    public void Reset()
    {
        index = 0;
    }
}
