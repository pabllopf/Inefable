//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Story.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
namespace DialogueSystem.Editor
{
    using UnityEditor;
    using UnityEditor.UIElements;
    using UnityEngine;
    using UnityEngine.UIElements;

    /// <summary>Story graph.</summary>
    public class Story : EditorWindow
    {
        /// <summary>The file name</summary>
        private string fileName = "New Dialogue";

        /// <summary>The graph view</summary>
        private StoryView graphView;

        #region Encapsulate Fields

        /// <summary>Gets or sets the name of the file.</summary>
        /// <value>The name of the file.</value>
        public string FileName { get => fileName; set => fileName = value; }

        /// <summary>Gets or sets the graph view.</summary>
        /// <value>The graph view.</value>
        public StoryView GraphView { get => graphView; set => graphView = value; }

        #endregion

        /// <summary>Creates the graph view window.</summary>
        [MenuItem("System/Dialogue")]
        public static void CreateGraphViewWindow()
        {
            Story window = GetWindow<Story>();
            window.titleContent = new GUIContent("Dialogue");
        }

        /// <summary>Constructs the graph view.</summary>
        private void ConstructGraphView()
        {
            graphView = new StoryView
            {
                name = "Dialogue Graph"
            };
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        /// <summary>Generates the toolbar.</summary>
        private void GenerateToolbar()
        {
            Toolbar toolbar = new Toolbar();

            TextField fileNameTextField = new TextField("File Name:");
            fileNameTextField.SetValueWithoutNotify(fileName);
            fileNameTextField.MarkDirtyRepaint();
            fileNameTextField.RegisterValueChangedCallback(evt => fileName = evt.newValue);
            toolbar.Add(fileNameTextField);

            toolbar.Add(new Button(() => RequestDataOperation(true)) { text = "Save Data" });

            toolbar.Add(new Button(() => RequestDataOperation(false)) { text = "Load Data" });
            toolbar.Add(new Button(() => graphView.CreateNewDialogueNode("Dialogue Node")) { text = "New Node", });

            toolbar.Add(new Button(() => ResetWindows()) { text = "Reset" });

            rootVisualElement.Add(toolbar);
        }

        /// <summary>Resets the windows.</summary>
        private void ResetWindows()
        {
            if (EditorUtility.DisplayDialog("Reset Dialogue", "Clicking 'Confirm' all unsaved changes will disappear.", "Confirm", "Cancel"))
            {
                rootVisualElement.Clear();
                ConstructGraphView();
                GenerateToolbar();
            }
        }

        /// <summary>Requests the data operation.</summary>
        /// <param name="save">if set to <c>true</c> [save].</param>
        private void RequestDataOperation(bool save)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                EditorUtility.DisplayDialog("Invalid File name", "Please Enter a valid filename", "OK");
            }
            else
            {
                DataUtility dataUtility = DataUtility.GetInstance(graphView);
                if (save)
                {
                    dataUtility.SaveNodes(fileName);
                }
                else
                {
                    dataUtility.LoadNarrative(fileName);
                }
            }
        }

        /// <summary>Called when [enable].</summary>
        private void OnEnable()
        {
            ConstructGraphView();
            GenerateToolbar();
        }

        /// <summary>Called when [disable].</summary>
        private void OnDisable()
        {
            rootVisualElement.Remove(graphView);
        }
    }
}