using System.Collections.Generic;
using GameFlow.Editors.DialogueSystem.Enums;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameFlow.Editors.DialogueSystem.Elements
{
    public class DialogueSystemBaseNode : Node
    {
        public string DialogueName { get; set; }
        public List<string> DialogueChoices { get; set; }
        public string DialogueContent { get; set; }
        public DialogueSystemDialogueType DialogueType { get; set; }

        public virtual void Setup(Vector2 position)
        {
            this.DialogueName = "Dialogue Name";
            this.DialogueChoices = new List<string>();
            this.DialogueContent = "Dialogue Content";
            this.SetPosition(new Rect(position, Vector2.zero));
        }

        public virtual void DrawNode()
        {
            // Title Container
            TextField dialogueNameTextField = new TextField() { value = this.DialogueName };
            this.titleContainer.Insert(0, dialogueNameTextField);

            // Input Container
            Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
            inputPort.portName = "Dialogue Connection";
            this.inputContainer.Add(inputPort);

            // Extension container
            VisualElement customDataContainer = new VisualElement();
            Foldout textFoldout = new Foldout() { text = "Dialogue Text" };
            TextField dialogueContentTextField = new TextField() { value = this.DialogueContent };
            textFoldout.Add(dialogueContentTextField);
            customDataContainer.Add(textFoldout);
            this.extensionContainer.Add(customDataContainer);
        }
    }
}