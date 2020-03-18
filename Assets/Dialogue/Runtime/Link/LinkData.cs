//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="LinkData.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace DialogueSystem.DataContainers
{
    using System;
    using UnityEngine;

    /// <summary>Link of node.</summary>
    [Serializable]
    public class LinkData
    {
        /// <summary>The base node unique identifier</summary>
        [SerializeField]
        private string baseNodeGUID = string.Empty;

        /// <summary>The port name</summary>
        [SerializeField]
        private string portName = string.Empty;

        /// <summary>The target node unique identifier</summary>
        [SerializeField]
        private string targetNodeGUID = string.Empty;

        /// <summary>Gets or sets the base node unique identifier.</summary>
        /// <value>The base node unique identifier.</value>
        public string BaseNodeGUID { get => baseNodeGUID; set => baseNodeGUID = value; }

        /// <summary>Gets or sets the name of the port.</summary>
        /// <value>The name of the port.</value>
        public string PortName { get => portName; set => portName = value; }

        /// <summary>Gets or sets the target node unique identifier.</summary>
        /// <value>The target node unique identifier.</value>
        public string TargetNodeGUID { get => targetNodeGUID; set => targetNodeGUID = value; }
    }
}