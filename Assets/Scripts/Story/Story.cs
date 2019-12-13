//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Story.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>Define the story of the game.</summary>
public class Story : MonoBehaviour
{
    /// <summary>The scene name to load</summary>
    [SerializeField]
    private string sceneNameToLoad = string.Empty;

    /// <summary>The frequency to change of sequence</summary>
    [Range(2, 20)]
    [SerializeField]
    private float frequencyToChangeOfSequence = 2f;

    /// <summary>The sequences</summary>
    [SerializeField]
    private List<Sequence> sequences = null;

    /// <summary>The sound track</summary>
    private AudioSource soundTrack;

    /// <summary>The list audio clips</summary>
    [SerializeField]
    private List<AudioClip> listAudioClips = null;

    /// <summary>Awakes this instance.</summary>
    private void Awake()
    {
        Game.LoadSettings();
        Language.Translate();
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        this.InitMainParameters();
        this.DisableAllSequences();
        this.StartCoroutine(this.RunSequences());
    }

    /// <summary>Initializes the main parameters.</summary>
    private void InitMainParameters() 
    {
        this.soundTrack = GameObject.Find("SoundTrack").GetComponent<AudioSource>();
    }

    /// <summary>Disables all sequences.</summary>
    private void DisableAllSequences() 
    {
        foreach (Sequence sequence in this.sequences) 
        {
            sequence.Disable();
        }
    }

    /// <summary>Runs the sequences.</summary>
    /// <returns>Return None</returns>
    private IEnumerator RunSequences() 
    {
        foreach (Sequence sequence in this.sequences) 
        {
            if (this.soundTrack.clip != this.listAudioClips[sequence.GetAudio()]) 
            {
                this.soundTrack.clip = this.listAudioClips[sequence.GetAudio()];
                this.soundTrack.Play();
            }

            sequence.Active();
            yield return new WaitForSeconds(this.frequencyToChangeOfSequence);
            sequence.Disable();
        }

        SceneManager.LoadScene(this.sceneNameToLoad);
    }
}
