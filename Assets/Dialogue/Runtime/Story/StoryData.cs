//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="StoryData.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace DialogueSystem.DataContainers
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>Container that save the info of dialogue.</summary>
    /// <seealso cref="UnityEngine.ScriptableObject" />
    [Serializable]
    public class StoryData : ScriptableObject
    {
        /// <summary>The node links</summary>
        [SerializeField]
        private List<LinkData> nodeLinks = new List<LinkData>();

        /// <summary>The dialogue node data</summary>
        [SerializeField]
        private List<NodeData> dialogueNodeData = new List<NodeData>();

        /// <summary>Gets or sets the node links.</summary>
        /// <value>The node links.</value>
        public List<LinkData> NodeLinks { get => nodeLinks; set => nodeLinks = value; }

        /// <summary>Gets or sets the dialogue node data.</summary>
        /// <value>The dialogue node data.</value>
        public List<NodeData> DialogueNodeData { get => dialogueNodeData; set => dialogueNodeData = value; }
    }
}