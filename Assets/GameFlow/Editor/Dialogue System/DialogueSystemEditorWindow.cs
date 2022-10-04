using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace GameFlow.Editor.DialogueSystem
{
    public class DialogueSystemEditorWindow : EditorWindow
    {
        [MenuItem("Game Flow/Dialogue System Graph")]
        public static void Open()
        {
            GetWindow<DialogueSystemEditorWindow>("Game Flow :: Dialogue System Graph");
        }

        private void OnEnable()
        {
            AddGraphView();
        }

        private void AddGraphView()
        {
            DialogueSystemGraphView graphView = new DialogueSystemGraphView();
            graphView.StretchToParentSize();
            this.rootVisualElement.Add(graphView);
        }
    }
}