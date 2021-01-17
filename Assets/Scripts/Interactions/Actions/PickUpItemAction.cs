using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Pick Up Item")]
    public class PickUpItemAction : AbstractAction
    {
        public InventoryItemStack ItemStack;

        public InteractionNode Next;

        protected override void OnEnterNode()
        {
            GlobalPlayerData.Inventory.InsertItem(ItemStack.Item, ItemStack.Count);
            AdvanceTo(Next);
        }
    }
}