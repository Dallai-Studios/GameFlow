using GameFlow.Editors.DialogueSystem.Elements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using GameFlow.Editors.DialogueSystem.Enums;

namespace GameFlow.Editors.DialogueSystem
{
    public class DialogueSystemGraphView : GraphView
    {
        public DialogueSystemGraphView()
        {
            this.AddGridBackground();
            this.LoadGraphViewStyleSheet();
            this.AddManipulations();
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            this.Insert(0, gridBackground);
        }

        private void LoadGraphViewStyleSheet()
        {
            StyleSheet graphViewStyle = 
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/GameFlow/Editor/Dialogue System/DialogueSystemGraphViewStyle.uss");
            
            this.styleSheets.Add(graphViewStyle);
        }
        
        private void AddManipulations()
        {
            this.SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(this.CreateContextualMenu());
        }

        private IManipulator CreateContextualMenu()
        {
            ContextualMenuManipulator manipulator = new ContextualMenuManipulator(menuEvent =>
                {
                    menuEvent.menu.AppendAction("Add Node (Single Choice)",
                        actionEvent => AddElement(this.CreateNode(
                            actionEvent.eventInfo.mousePosition, DialogueSystemDialogueType.SINGLE_CHOICE)));
                    
                    menuEvent.menu.AppendAction("Add Node (Multiple Choices)",
                        actionEvent => AddElement(
                            this.CreateNode(actionEvent.eventInfo.mousePosition, DialogueSystemDialogueType.MULTIPLE_CHOICE)));
                }
            );
            
            return manipulator;
        }

        private DialogueSystemBaseNode CreateNode(Vector2 position, DialogueSystemDialogueType nodeType)
        {
            DialogueSystemBaseNode node = nodeType == DialogueSystemDialogueType.SINGLE_CHOICE ? 
                    (DialogueSystemBaseNode) new DialogueSystemSingleChoiceNode() :
                    (DialogueSystemBaseNode) new DialogueSystemMultipleChoiceNode(); 
            
            node.Setup(position);
            node.DrawNode();
            
            return node;
        }
    }
}