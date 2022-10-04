using GameFlow.Editor.DialogueSystem.Enums;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameFlow.Editor.DialogueSystem.Elements
{
    public class DialogueSystemMultipleChoiceNode : DialogueSystemBaseNode
    {
        public override void Setup(Vector2 position)
        {
            base.Setup(position);

            this.DialogueType = DialogueSystemDialogueType.MULTIPLE_CHOICE;
            this.DialogueChoices.Add("New Choice");
        }

        public override void DrawNode()
        {
            base.DrawNode();

            Button addNewChoiceButton = new Button(() => this.AddDialogueChoice("New Choice"));
            addNewChoiceButton.text = "Add new Dialogue Choice";
            this.mainContainer.Insert(1, addNewChoiceButton);
            
            this.DialogueChoices.ForEach(choice =>
            {
                Port choicePort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
                choicePort.portName = string.Empty;

                Button deleteChoiceButton = new Button(() => this.RemoveChoice(choice));
                deleteChoiceButton.text = "X";
                
                TextField choiceTextField = new TextField();
                choiceTextField.value = choice;
                
                //  Elements are drawn from right to left
                choicePort.Add(choiceTextField);
                choicePort.Add(deleteChoiceButton);
                this.outputContainer.Add(choicePort);
            });
            
            this.RefreshExpandedState();
        }

        private void AddDialogueChoice(string choice)
        {
            this.DialogueChoices.Add(choice);
        }
        
        private void RemoveChoice(string choice)
        {
            this.DialogueChoices.Remove(choice);
        }
    }
}