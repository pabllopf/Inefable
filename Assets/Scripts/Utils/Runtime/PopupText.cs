﻿//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="PopupText.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace Utils.Effect
{
    using System.Collections;
    using TMPro;
    using UnityEngine;

    /// <summary>Popup Text effect.</summary>
    public class PopupText : MonoBehaviour
    {
        /// <summary>The damage text</summary>
        [SerializeField]
        private GameObject damageText = null;

        /// <summary>The time</summary>
        [SerializeField]
        [Range(0, 10)]
        private float time = 1f;

        /// <summary>The effect UI</summary>
        private GameObject effectUI = null;

        /// <summary>Gets or sets the damage text.</summary>
        /// <value>The damage text.</value>
        public GameObject DamageText { get => damageText; set => damageText = value; }
        
        /// <summary>Plays the specified text.</summary>
        /// <param name="text">The text.</param>
        public void Play(string text)
        {
            if (effectUI.activeSelf)
            {
                effectUI.transform.position = this.transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
                effectUI.GetComponent<TextMeshPro>().text = text;
            }
            else 
            {
                effectUI.SetActive(true);
                effectUI.transform.position = this.transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
                effectUI.GetComponent<TextMeshPro>().text = text;
            }

            StopAllCoroutines();
            this.StartCoroutine(this.DisableInTime(this.time));
        }

        /// <summary>Disables the in time.</summary>
        /// <param name="time">The time.</param>
        /// <returns>Return none</returns>
        private IEnumerator DisableInTime(float time)
        {
            yield return new WaitForSeconds(time);
            effectUI.SetActive(false);
        }

        /// <summary>Starts this instance.</summary>
        private void Start() => effectUI = Instantiate(damageText, Vector3.zero, Quaternion.identity, transform);
    }
}