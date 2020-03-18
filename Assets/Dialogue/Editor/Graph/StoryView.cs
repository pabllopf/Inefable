//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="StoryView.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace DialogueSystem.Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;
    using UnityEngine.UIElements;

    /// <summary>View story graph.</summary>
    public class StoryView : GraphView
    {
        /// <summary>The default node size</summary>
        private Vector2 defaultNodeSize = new Vector2(200, 150);

        /// <summary>The entry point node</summary>
        private StoryNode entryPointNode = null;

        #region Constructor

        /// <summary>Initializes a new instance of the <see cref="StoryView"/> class.</summary>
        public StoryView()
        {
            styleSheets.Add(Resources.Load<StyleSheet>("StoryView"));
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new FreehandSelector());

            GridBackground grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();

            AddElement(GetEntryPointNodeInstance());
        }

        #endregion

        #region Encapsulate Fields

        /// <summary>Gets or sets the default size of the node.</summary>
        /// <value>The default size of the node.</value>
        public Vector2 DefaultNodeSize { get => defaultNodeSize; set => defaultNodeSize = value; }

        /// <summary>Gets or sets the entry point node.</summary>
        /// <value>The entry point node.</value>
        public StoryNode EntryPointNode { get => entryPointNode; set => entryPointNode = value; }

        #endregion

        /// <summary>Get all ports compatible with given port.</summary>
        /// <param name="startPort">Start port to validate against.</param>
        /// <param name="nodeAdapter">Node adapter.</param>
        /// <returns>List of compatible ports.</returns>
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();
            Port startPortView = startPort;

            ports.ForEach((port) =>
            {
                Port portView = port;
                if (startPortView != portView && startPortView.node != portView.node)
                {
                    compatiblePorts.Add(port);
                }
            });

            return compatiblePorts;
        }

        /// <summary>Creates the new dialogue node.</summary>
        /// <param name="nodeName">Name of the node.</param>
        public void CreateNewDialogueNode(string nodeName)
        {
            AddElement(CreateNode(nodeName));
        }

        /// <summary>Creates the node.</summary>
        /// <param name="nodeName">Name of the node.</param>
        /// <returns>Return a new node.</returns>
        public StoryNode CreateNode(string nodeName)
        {
            StoryNode tempDialogueNode = new StoryNode()
            {
                title = nodeName,
                DialogueText = nodeName,
                GUID = Guid.NewGuid().ToString()
            };

            Port inputPort = GetPortInstance(tempDialogueNode, Direction.Input, Port.Capacity.Multi);
            inputPort.portName = "Input";
            tempDialogueNode.inputContainer.Add(inputPort);
            tempDialogueNode.RefreshExpandedState();
            tempDialogueNode.RefreshPorts();

            tempDialogueNode.SetPosition(new Rect(Vector2.zero, DefaultNodeSize));

            TextField textField = new TextField(string.Empty);
            textField.RegisterValueChangedCallback(evt =>
            {
                tempDialogueNode.DialogueText = evt.newValue;
                tempDialogueNode.title = evt.newValue;
            });
            textField.SetValueWithoutNotify(tempDialogueNode.title);
            tempDialogueNode.mainContainer.Add(textField);

            Button button = new Button(() => { AddChoicePort(tempDialogueNode); })
            {
                text = "Add Choice"
            };
            tempDialogueNode.titleButtonContainer.Add(button);
            return tempDialogueNode;
        }

        /// <summary>Adds the choice port.</summary>
        /// <param name="nodeCache">The node cache.</param>
        /// <param name="overriddenPortName">Name of the overridden port.</param>
        public void AddChoicePort(StoryNode nodeCache, string overriddenPortName = "")
        {
            Port generatedPort = GetPortInstance(nodeCache, Direction.Output);
            Label portLabel = generatedPort.contentContainer.Q<Label>("type");
            generatedPort.contentContainer.Remove(portLabel);

            int outputPortCount = nodeCache.outputContainer.Query("connector").ToList().Count();
            string outputPortName = string.IsNullOrEmpty(overriddenPortName)
                ? $"Option {outputPortCount + 1}"
                : overriddenPortName;

            TextField textField = new TextField()
            {
                name = string.Empty,
                value = outputPortName
            };

            textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
            generatedPort.contentContainer.Add(new Label("  "));
            generatedPort.contentContainer.Add(textField);

            Button deleteButton = new Button(() => RemovePort(nodeCache, generatedPort))
            {
                text = "X"
            };

            generatedPort.contentContainer.Add(deleteButton);
            generatedPort.portName = outputPortName;
            nodeCache.outputContainer.Add(generatedPort);
            nodeCache.RefreshPorts();
            nodeCache.RefreshExpandedState();
        }

        /// <summary>Removes the port.</summary>
        /// <param name="node">The node.</param>
        /// <param name="socket">The socket.</param>
        private void RemovePort(Node node, Port socket)
        {
            IEnumerable<Edge> targetEdge = edges.ToList()
                .Where(x => x.output.portName == socket.portName && x.output.node == socket.node);
            if (targetEdge.Any())
            {
                Edge edge = targetEdge.First();
                edge.input.Disconnect(edge);
                RemoveElement(targetEdge.First());
            }

            node.outputContainer.Remove(socket);
            node.RefreshPorts();
            node.RefreshExpandedState();
        }

        /// <summary>Gets the port instance.</summary>
        /// <param name="node">The node.</param>
        /// <param name="nodeDirection">The node direction.</param>
        /// <param name="capacity">The capacity.</param>
        /// <returns>Get the port</returns>
        private Port GetPortInstance(StoryNode node, Direction nodeDirection, Port.Capacity capacity = Port.Capacity.Single)
        {
            return node.InstantiatePort(Orientation.Horizontal, nodeDirection, capacity, typeof(float));
        }

        /// <summary>Gets the entry point node instance.</summary>
        /// <returns>Get a Dialogue Node</returns>
        private StoryNode GetEntryPointNodeInstance()
        {
            StoryNode nodeCache = new StoryNode()
            {
                title = "Start Point",
                GUID = Guid.NewGuid().ToString(),
                DialogueText = string.Empty,
                EmptyPoint = true
            };

            Port generatedPort = GetPortInstance(nodeCache, Direction.Output);
            generatedPort.portName = "Next";
            nodeCache.outputContainer.Add(generatedPort);

            nodeCache.capabilities &= ~Capabilities.Movable;
            nodeCache.capabilities &= ~Capabilities.Deletable;

            nodeCache.RefreshExpandedState();
            nodeCache.RefreshPorts();
            nodeCache.SetPosition(new Rect(100, 200, 100, 150));
            return nodeCache;
        }
    }
}