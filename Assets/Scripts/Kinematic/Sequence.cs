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
    private GameObject sequenceObj = null;

    /// <summary>The key</summary>
    [SerializeField]
    private Clef key = Clef.A1;

    /// <summary>Gets or sets the key.</summary>
    /// <value>The key.</value>
    public Clef Key { get => key; set => key = value; }

    /// <summary>Gets or sets the sequence.</summary>
    /// <value>The sequence.</value>
    public GameObject SequenceObj { get => sequenceObj; set => sequenceObj = value; }

    /// <summary>Sets up.</summary>
    public void SetUp()
    {
        sequenceObj.transform.Find("BlackArea/Text").GetComponent<Text>().text = Language.GetSentence(key);
    }

    /// <summary>Active the sequence.</summary>
    public void Active()
    {
        sequenceObj.SetActive(true);
    }

    /// <summary>Disable the sequence.</summary>
    public void Disable()
    {
        sequenceObj.SetActive(false);
    }
}
