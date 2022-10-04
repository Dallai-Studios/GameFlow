using GameFlow.Editor.DialogueSystem.Enums;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GameFlow.Editor.DialogueSystem.Elements
{
    public class DialogueSystemSingleChoiceNode : DialogueSystemBaseNode
    {
        public override void Setup(Vector2 position)
        {
            base.Setup(position);
            
            this.DialogueType = DialogueSystemDialogueType.SINGLE_CHOICE;
            this.DialogueChoices.Add("Next Dialogue");
        }

        public override void DrawNode()
        {
            base.DrawNode();
            
            this.DialogueChoices.ForEach(choice =>
            {
                Port exitPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
                exitPort.portName = choice;
                this.outputContainer.Add(exitPort);
            });
            
            this.RefreshExpandedState();
        }
    }
}