using System.Collections.Generic;
using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Deposit Items")]
    public class DepositItemAction : AbstractAction
    {
        public List<InventoryItemStack> Items;

        public InteractionNode Fail;
        public InteractionNode Success;

        protected override void OnEnterNode()
        {
            foreach (var stack in Items)
            {
                if (GlobalPlayerData.Inventory[stack.Item].Count < stack.Count)
                {
                    AdvanceTo(Fail);
                    return;
                }
            }
            
            foreach (var stack in Items)
            {
                GlobalPlayerData.Inventory.ExtractItem(stack.Item, stack.Count);
            }
            AdvanceTo(Success);
        }
    }
}