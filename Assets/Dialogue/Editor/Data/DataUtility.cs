//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="DataUtility.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
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
    public class DataUtility
    {
        /// <summary>The edges</summary>
        private List<Edge> edges = new List<Edge>();

        /// <summary>The nodes</summary>
        private List<StoryNode> nodes = new List<StoryNode>();

        /// <summary>The dialogue container</summary>
        private StoryData dialogueContainer;

        /// <summary>The graph view</summary>
        private StoryView graphView;

        #region Encapsulate Fields

        /// <summary>Gets or sets the edges.</summary>
        /// <value>The edges.</value>
        public List<Edge> Edges { get => graphView.edges.ToList(); set => edges = value; }

        /// <summary>Gets or sets the nodes.</summary>
        /// <value>The nodes.</value>
        public List<StoryNode> Nodes { get => graphView.nodes.ToList().Cast<StoryNode>().ToList(); set => nodes = value; }

        /// <summary>Gets or sets the dialogue container.</summary>
        /// <value>The dialogue container.</value>
        public StoryData DialogueContainer { get => dialogueContainer; set => dialogueContainer = value; }

        /// <summary>Gets or sets the graph view.</summary>
        /// <value>The graph view.</value>
        public StoryView GraphView { get => graphView; set => graphView = value; }

        #endregion

        /// <summary>Gets the instance.</summary>
        /// <param name="graphView">The graph view.</param>
        /// <returns>Return a new Graph Save Utility</returns>
        public static DataUtility GetInstance(StoryView graphView)
        {
            return new DataUtility
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
                StoryData dialogueContainerObj = ScriptableObject.CreateInstance<StoryData>();
                Edge[] connectedSockets = Edges.Where(edge => edge.input.node != null).ToArray();

                for (int i = 0; i < connectedSockets.Count(); i++)
                {
                    StoryNode outputNode = (StoryNode)connectedSockets[i].output.node;
                    StoryNode inputNode = (StoryNode)connectedSockets[i].input.node;

                    dialogueContainerObj.NodeLinks.Add(new LinkData
                    {
                        BaseNodeGUID = outputNode.GUID,
                        PortName = connectedSockets[i].output.portName,
                        TargetNodeGUID = inputNode.GUID
                    });
                }

                foreach (StoryNode node in Nodes.Where(node => !node.EmptyPoint))
                {
                    dialogueContainerObj.DialogueNodeData.Add(new NodeData
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
            dialogueContainer = Resources.Load<StoryData>("Dialogues/" + fileName);

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

            foreach (StoryNode perNode in Nodes)
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
            foreach (NodeData perNode in dialogueContainer.DialogueNodeData)
            {
                StoryNode tempNode = graphView.CreateNode(perNode.DialogueText);
                tempNode.GUID = perNode.NodeGUID;
                graphView.AddElement(tempNode);

                List<LinkData> nodePorts = dialogueContainer.NodeLinks.Where(x => x.BaseNodeGUID == perNode.NodeGUID).ToList();
                nodePorts.ForEach(x => graphView.AddChoicePort(tempNode, x.PortName));
            }
        }

        /// <summary>Connects the dialogue nodes.</summary>
        private void ConnectDialogueNodes()
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                int k = i;
                List<LinkData> connections = dialogueContainer.NodeLinks.Where(x => x.BaseNodeGUID == Nodes[k].GUID).ToList();
                for (int j = 0; j < connections.Count(); j++)
                {
                    string targetNodeGUID = connections[j].TargetNodeGUID;
                    StoryNode targetNode = Nodes.First(x => x.GUID == targetNodeGUID);
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