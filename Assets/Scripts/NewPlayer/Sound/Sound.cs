//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Sound.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Manage sounds of game</summary>
public class Sound
{
    /// <summary>Play the clip.</summary>
    /// <param name="audioClip">The audio clip.</param>
    /// <param name="audioSource">The audio source.</param>
    public static void Play(AudioClip audioClip, AudioSource audioSource)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}