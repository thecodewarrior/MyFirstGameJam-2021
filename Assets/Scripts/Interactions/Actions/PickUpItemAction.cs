using System.Collections.Generic;
using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Pick Up Items")]
    public class PickUpItemAction : AbstractAction
    {
        public List<InventoryItemStack> Items;

        public InteractionNode Next;

        protected override void OnEnterNode()
        {
            foreach (var stack in Items)
            {
                GlobalPlayerData.Inventory.InsertItem(stack.Item, stack.Count);
            }
            AdvanceTo(Next);
        }
    }
}