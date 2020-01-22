//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Kinematic.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>Define the story of the game.</summary>
public class Kinematic : MonoBehaviour
{
    /// <summary>The scene</summary>
    [SerializeField]
    private string scene;

    /// <summary>The frequency to change of sequence</summary>
    [Range(2, 20)]
    [SerializeField]
    private float timeBetweenSequences = 2f;

    /// <summary>The audio clip</summary>
    [SerializeField]
    private AudioClip audioClip = null;

    /// <summary>The sequences</summary>
    [SerializeField]
    private List<Sequence> sequences = null;

    /// <summary>The index</summary>
    private int index = 0;

    /// <summary>The sound track</summary>
    private AudioSource SoundTrack => this.GetComponent<AudioSource>();

    /// <summary>Awakes this instance.</summary>
    private void Awake() => Game.LoadSettings();

    /// <summary>Starts this instance.</summary>
    private void Start() => this.StartCoroutine(this.PlaySequences(this.scene, this.sequences, this.timeBetweenSequences));

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (Settings.Current.Plattform == "Computer") 
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this.SkipKinematic(this.scene);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                if (Time.timeScale == 1)
                {
                    this.PauseSequences();
                }
                else
                {
                    this.StartCoroutine(this.ContinueSequences(this.scene, this.sequences, this.timeBetweenSequences));
                }
            }
        }
    }

    /// <summary>Skips the kinematic.</summary>
    /// <param name="scene">The scene.</param>
    private void SkipKinematic(string scene) => SceneManager.LoadScene(scene);

    /// <summary>Disables all sequences.</summary>
    /// <param name="sequences">The sequences.</param>
    private void DisableAllSequences(List<Sequence> sequences) => sequences.ForEach(i => i.Disable());

    /// <summary>Plays the sequences.</summary>
    /// <param name="scene">The scene.</param>
    /// <param name="sequences">The sequences.</param>
    /// <param name="timeBetweenSequences">The time between sequences.</param>
    /// <returns>Return none</returns>
    private IEnumerator PlaySequences(string scene, List<Sequence> sequences, float timeBetweenSequences) 
    {
        Language.Translate();
        this.SoundTrack.clip = this.audioClip;
        this.SoundTrack.Play();
        this.DisableAllSequences(sequences);

        foreach (Sequence sequence in sequences) 
        {
            sequence.Active();
            this.index++;
            yield return new WaitForSeconds(timeBetweenSequences);
            sequence.Disable();
        }

        SceneManager.LoadScene(scene);
    }

    /// <summary>Pauses the sequences.</summary>
    private void PauseSequences() 
    {
        Time.timeScale = 0;
        this.StopAllCoroutines();
    }

    /// <summary>Continues the sequences.</summary>
    /// <param name="scene">The scene.</param>
    /// <param name="sequences">The sequences.</param>
    /// <param name="timeBetweenSequences">The time between sequences.</param>
    /// <returns>Return none</returns>
    private IEnumerator ContinueSequences(string scene, List<Sequence> sequences, float timeBetweenSequences) 
    {
        Time.timeScale = 1;
        Language.Translate();
        this.DisableAllSequences(sequences);

        for (int i = this.index; i < sequences.Count; i++) 
        {
            sequences[i].Active();
            this.index++;
            yield return new WaitForSeconds(timeBetweenSequences);
            sequences[i].Disable();
        }

        SceneManager.LoadScene(scene);
    }
}
