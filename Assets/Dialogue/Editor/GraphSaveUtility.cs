//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="GraphSaveUtility.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace DialogueSystem.Editor
{
    using System.Collections.Generic;
    using System.Linq;
    using DialogueSystem.DataContainers;
    using UnityEditor;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;
    using UnityEngine.UIElements;

    /// <summary>Save and load the graph.</summary>
    public class GraphSaveUtility
    {
        /// <summary>The edges</summary>
        private List<Edge> edges = new List<Edge>();

        /// <summary>The nodes</summary>
        private List<DialogueNode> nodes = new List<DialogueNode>();

        /// <summary>The dialogue container</summary>
        private DialogueContainer dialogueContainer;

        /// <summary>The graph view</summary>
        private StoryGraphView graphView;

        #region Encapsulate Fields

        /// <summary>Gets or sets the edges.</summary>
        /// <value>The edges.</value>
        public List<Edge> Edges { get => graphView.edges.ToList(); set => edges = value; }

        /// <summary>Gets or sets the nodes.</summary>
        /// <value>The nodes.</value>
        public List<DialogueNode> Nodes { get => graphView.nodes.ToList().Cast<DialogueNode>().ToList(); set => nodes = value; }

        /// <summary>Gets or sets the dialogue container.</summary>
        /// <value>The dialogue container.</value>
        public DialogueContainer DialogueContainer { get => dialogueContainer; set => dialogueContainer = value; }

        /// <summary>Gets or sets the graph view.</summary>
        /// <value>The graph view.</value>
        public StoryGraphView GraphView { get => graphView; set => graphView = value; }

        #endregion

        /// <summary>Gets the instance.</summary>
        /// <param name="graphView">The graph view.</param>
        /// <returns>Return a new Graph Save Utility</returns>
        public static GraphSaveUtility GetInstance(StoryGraphView graphView)
        {
            return new GraphSaveUtility
            {
                graphView = graphView
            };
        }

        /// <summary>Saves the nodes.</summary>
        /// <param name="fileName">Name of the file.</param>
        public void SaveNodes(string fileName)
        {
            if (Edges.Any())
            {
                DialogueContainer dialogueContainerObj = ScriptableObject.CreateInstance<DialogueContainer>();
                Edge[] connectedSockets = Edges.Where(edge => edge.input.node != null).ToArray();

                for (int i = 0; i < connectedSockets.Count(); i++)
                {
                    DialogueNode outputNode = (DialogueNode)connectedSockets[i].output.node;
                    DialogueNode inputNode = (DialogueNode)connectedSockets[i].input.node;

                    dialogueContainerObj.NodeLinks.Add(new NodeLinkData
                    {
                        BaseNodeGUID = outputNode.GUID,
                        PortName = connectedSockets[i].output.portName,
                        TargetNodeGUID = inputNode.GUID
                    });
                }

                foreach (DialogueNode node in Nodes.Where(node => !node.EmptyPoint))
                {
                    dialogueContainerObj.DialogueNodeData.Add(new DialogueNodeData
                    {
                        NodeGUID = node.GUID,
                        DialogueText = node.DialogueText,
                        Position = node.GetPosition().position
                    });
                }

                if (!AssetDatabase.IsValidFolder("Assets/Resources/Dialogues"))
                {
                    AssetDatabase.CreateFolder("Resources", "Dialogues");
                }

                AssetDatabase.CreateAsset(dialogueContainerObj, $"Assets/Resources/Dialogues/{fileName}.asset");
                AssetDatabase.SaveAssets();
            }
        }

        /// <summary>Loads the narrative.</summary>
        /// <param name="fileName">Name of the file.</param>
        public void LoadNarrative(string fileName)
        {
            dialogueContainer = Resources.Load<DialogueContainer>("Dialogues/" + fileName);

            if (dialogueContainer == null)
            {
                EditorUtility.DisplayDialog("File Not Found", "Target Narrative Data does not exist!", "OK");
                return;
            }

            ClearGraph();
            GenerateDialogueNodes();
            ConnectDialogueNodes();
        }

        /// <summary>Clears the graph.</summary>
        private void ClearGraph()
        {
            Nodes.Find(node => node.EmptyPoint).GUID = dialogueContainer.NodeLinks[0].BaseNodeGUID;

            foreach (DialogueNode perNode in Nodes)
            {
                if (perNode.EmptyPoint)
                {
                    continue;
                }

                Edges
                    .Where(x => x.input.node == perNode)
                    .ToList()
                    .ForEach(edge => graphView.RemoveElement(edge));

                graphView.RemoveElement(perNode);
            }
        }

        /// <summary>Generates the dialogue nodes.</summary>
        private void GenerateDialogueNodes()
        {
            foreach (DialogueNodeData perNode in dialogueContainer.DialogueNodeData)
            {
                DialogueNode tempNode = graphView.CreateNode(perNode.DialogueText);
                tempNode.GUID = perNode.NodeGUID;
                graphView.AddElement(tempNode);

                List<NodeLinkData> nodePorts = dialogueContainer.NodeLinks.Where(x => x.BaseNodeGUID == perNode.NodeGUID).ToList();
                nodePorts.ForEach(x => graphView.AddChoicePort(tempNode, x.PortName));
            }
        }

        /// <summary>Connects the dialogue nodes.</summary>
        private void ConnectDialogueNodes()
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                int k = i;
                List<NodeLinkData> connections = dialogueContainer.NodeLinks.Where(x => x.BaseNodeGUID == Nodes[k].GUID).ToList();
                for (int j = 0; j < connections.Count(); j++)
                {
                    string targetNodeGUID = connections[j].TargetNodeGUID;
                    DialogueNode targetNode = Nodes.First(x => x.GUID == targetNodeGUID);
                    LinkNodesTogether(Nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);

                    targetNode.SetPosition(new Rect(
                        dialogueContainer.DialogueNodeData.First(x => x.NodeGUID == targetNodeGUID).Position,
                        graphView.DefaultNodeSize));
                }
            }
        }

        /// <summary>Links the nodes together.</summary>
        /// <param name="outputSocket">The output socket.</param>
        /// <param name="inputSocket">The input socket.</param>
        private void LinkNodesTogether(Port outputSocket, Port inputSocket)
        {
            Edge tempEdge = new Edge()
            {
                output = outputSocket,
                input = inputSocket
            };

            tempEdge?.input.Connect(tempEdge);
            tempEdge?.output.Connect(tempEdge);

            graphView.Add(tempEdge);
        }
    }
}