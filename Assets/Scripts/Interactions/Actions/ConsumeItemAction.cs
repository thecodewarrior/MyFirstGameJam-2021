using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Consume Item")]
    public class ConsumeItemAction : AbstractAction
    {
        public InventoryItemStack Item;

        public bool ShowPrompt;

        public InteractionNode Fail;
        public InteractionNode Success;
    }
}