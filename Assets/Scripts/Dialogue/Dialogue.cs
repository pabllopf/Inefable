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
    private string name = string.Empty;

    /// <summary>The index</summary>
    private int index = 0;

    /// <summary>The sentences</summary>
    [SerializeField]
    private Key[] sentences = null;

    /// <summary>Get the name.</summary>
    /// <returns>The name.</returns>
    public string GetName() => this.name;

    /// <summary>Gets the sentence.</summary>
    /// <returns>the sentence</returns>
    public string GetSentence() => Language.GetSentence(this.sentences[this.index]);

    /// <summary>Determines whether this instance has next.</summary>
    /// <returns>
    /// <c>true</c> if this instance has next; otherwise, <c>false</c>.</returns>
    public bool HasNext() => (this.index < this.sentences.Length) ? true : false;

    /// <summary>Next this instance.</summary>
    public void Next() => this.index++;

    /// <summary>Resets this instance.</summary>
    public void Reset() => this.index = 0;
}
