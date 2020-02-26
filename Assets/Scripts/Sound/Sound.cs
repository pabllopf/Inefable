//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Sound.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Manage sounds of game</summary>
public class Sound
{
    /// <summary>Plays the specified sound.</summary>
    /// <param name="sound">The sound.</param>
    /// <param name="audioSource">The audio source.</param>
    public static void Play(AudioClip sound, AudioSource audioSource)
    {
        audioSource.clip = sound;
        audioSource.Play();
    }
}