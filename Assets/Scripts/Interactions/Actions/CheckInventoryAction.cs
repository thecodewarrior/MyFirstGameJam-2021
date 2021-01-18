using System.Collections.Generic;
using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Check Inventory")]
    public class CheckInventoryAction : AbstractAction
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
            
            AdvanceTo(Success);
        }
    }
}