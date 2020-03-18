//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="NodeData.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace DialogueSystem.DataContainers
{
    using System;
    using UnityEngine;

    /// <summary>Node data of dialogue.</summary>
    [Serializable]
    public class NodeData
    {
        /// <summary>The node unique identifier</summary>
        [SerializeField]
        private string nodeGUID = string.Empty;

        /// <summary>The dialogue text</summary>
        [SerializeField]
        private string dialogueText = string.Empty;

        /// <summary>The position</summary>
        [SerializeField]
        private Vector2 position = Vector2.zero;

        /// <summary>Gets or sets the node unique identifier.</summary>
        /// <value>The node unique identifier.</value>
        public string NodeGUID { get => nodeGUID; set => nodeGUID = value; }

        /// <summary>Gets or sets the dialogue text.</summary>
        /// <value>The dialogue text.</value>
        public string DialogueText { get => dialogueText; set => dialogueText = value; }

        /// <summary>Gets or sets the position.</summary>
        /// <value>The position.</value>
        public Vector2 Position { get => position; set => position = value; }
    }
}