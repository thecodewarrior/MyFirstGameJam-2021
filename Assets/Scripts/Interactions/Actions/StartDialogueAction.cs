using System.Collections.Generic;
using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Start Dialogue")]
    public class StartDialogueAction : AbstractAction
    {
        public List<DialogueLine> Dialogue;

        public InteractionNode Next;
    }
}