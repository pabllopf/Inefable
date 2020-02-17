//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Audio.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Manage sounds of game</summary>
public class Audio
{
    /// <summary>Plays the specified sound.</summary>
    /// <param name="sound">The sound.</param>
    /// <param name="audioSource">The audio source.</param>
    public static void Play(Sound sound, AudioSource audioSource)
    {
        audioSource.clip = (AudioClip)Resources.Load("Sounds/" + sound.ToString());
        audioSource.Play();
    }
}