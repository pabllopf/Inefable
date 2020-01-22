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

    /// <summary>The sound track</summary>
    private AudioSource SoundTrack => this.GetComponent<AudioSource>();

    /// <summary>Awakes this instance.</summary>
    private void Awake()
    {
        Game.LoadSettings();
        Cursor.visible = false;
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        Language.Translate();

        this.SoundTrack.clip = this.audioClip;
        this.SoundTrack.Play();

        this.StartCoroutine(this.PlayKinematic(this.scene, this.sequences, this.timeBetweenSequences));
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("ButtonStart") || Input.touchCount > 0)
        {
            this.LoadScene(scene);
            return;
        }
    }

    /// <summary>Loads the scene.</summary>
    /// <param name="scene">The scene.</param>
    private void LoadScene(string scene) => SceneManager.LoadScene(scene);

    /// <summary>Plays the sequences.</summary>
    /// <param name="scene">The scene.</param>
    /// <param name="sequences">The sequences.</param>
    /// <param name="timeBetweenSequences">The time between sequences.</param>
    /// <returns>Return none</returns>
    private IEnumerator PlayKinematic(string scene, List<Sequence> sequences, float timeBetweenSequences)
    {
        sequences.ForEach(sequence => sequence.SetUp());
        sequences.ForEach(sequence => sequence.Disable());

        foreach (Sequence sequence in sequences)
        {
            sequence.Active();
            yield return new WaitForSeconds(timeBetweenSequences);
            sequence.Disable();
        }

        this.LoadScene(scene);
    }
}