using UnityEngine;

namespace Interactions.Actions
{
    [AddComponentMenu("Interaction/Action/Deposit Item")]
    public class DepositItemAction : AbstractAction
    {
        public InventoryItemStack ItemStack;

        public InteractionNode Fail;
        public InteractionNode Success;

        protected override void OnEnterNode()
        {
            if (GlobalPlayerData.Inventory[ItemStack.Item].Count < ItemStack.Count)
            {
                AdvanceTo(Fail);
            }
            else
            {
                GlobalPlayerData.Inventory.ExtractItem(ItemStack.Item, ItemStack.Count);
                AdvanceTo(Success);
            }
        }
    }
}