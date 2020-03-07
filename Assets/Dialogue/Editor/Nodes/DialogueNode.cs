//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="DialogueNode.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace DialogueSystem.Editor
{
    using UnityEditor.Experimental.GraphView;

    /// <summary>Dialogue of node.</summary>
    /// <seealso cref="UnityEditor.Experimental.GraphView.Node" />
    public class DialogueNode : Node
    {
        /// <summary>The dialogue text</summary>
        private string dialogueText = null;

        /// <summary>The unique identifier</summary>
        private string guid = null;

        /// <summary>The empty point</summary>
        private bool emptyPoint = false;

        /// <summary>Gets or sets the dialogue text.</summary>
        /// <value>The dialogue text.</value>
        public string DialogueText { get => dialogueText; set => dialogueText = value; }

        /// <summary>Gets or sets the unique identifier.</summary>
        /// <value>The unique identifier.</value>
        public string GUID { get => guid; set => guid = value; }

        /// <summary>Gets or sets a value indicating whether [empty point].</summary>
        /// <value>
        /// <c>true</c> if [empty point]; otherwise, <c>false</c>.</value>
        public bool EmptyPoint { get => emptyPoint; set => emptyPoint = value; }
    }
}