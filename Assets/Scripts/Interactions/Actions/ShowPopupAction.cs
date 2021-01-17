using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Show Popup")]
    public class ShowPopupAction : AbstractAction
    {
        [TextArea(3, 5)]
        public string Description;

        public InteractionNode Next;
    }
}