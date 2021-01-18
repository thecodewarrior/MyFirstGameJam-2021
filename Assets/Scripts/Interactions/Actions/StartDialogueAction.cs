using System.Collections.Generic;
using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Start Dialogue")]
    public class StartDialogueAction : AbstractAction
    {
        public List<DialogueLine> Dialogue;

        public InteractionNode Next;

        private DialogueUIController _dialogueController;

        protected override void InitializeNode()
        {
            _dialogueController = FindObjectOfType<DialogueUIController>();
        }

        protected override void OnEnterNode()
        {
            _dialogueController.StartDialogue(this);
        }

        public void EndDialogue()
        {
            AdvanceTo(Next);
        }
    }
}