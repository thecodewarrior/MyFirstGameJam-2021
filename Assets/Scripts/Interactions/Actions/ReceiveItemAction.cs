using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Receive Item")]
    public class ReceiveItemAction : AbstractAction
    {
        public InventoryItemStack Item;

        public InteractionNode Next;
    }
}