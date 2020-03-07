//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="DialogueContainer.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace DialogueSystem.DataContainers
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>Container that save the info of dialogue.</summary>
    /// <seealso cref="UnityEngine.ScriptableObject" />
    [Serializable]
    public class DialogueContainer : ScriptableObject
    {
        /// <summary>The node links</summary>
        [SerializeField]
        private List<NodeLinkData> nodeLinks = new List<NodeLinkData>();

        /// <summary>The dialogue node data</summary>
        [SerializeField]
        private List<DialogueNodeData> dialogueNodeData = new List<DialogueNodeData>();

        /// <summary>Gets or sets the node links.</summary>
        /// <value>The node links.</value>
        public List<NodeLinkData> NodeLinks { get => nodeLinks; set => nodeLinks = value; }

        /// <summary>Gets or sets the dialogue node data.</summary>
        /// <value>The dialogue node data.</value>
        public List<DialogueNodeData> DialogueNodeData { get => dialogueNodeData; set => dialogueNodeData = value; }
    }
}