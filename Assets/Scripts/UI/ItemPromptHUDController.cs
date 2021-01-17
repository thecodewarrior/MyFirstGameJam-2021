using UnityEngine;
using UnityEngine.UIElements;

public class ItemPromptHUDController : AbstractTrackingHUDController
{
    [Tooltip("The item and quantity to display")]
    public InventoryItemStack ItemStack;

    [Tooltip("Whether to display as a requirement (e.g. '<amount in inventory>/<count>')")]
    public bool ShowAsConsume;

    public override void OnShow()
    {
        var stackInInventory = GlobalPlayerData.Inventory[ItemStack.Item];
        BindingUtils.ItemStack.Bind(Root, ItemStack);

        if (ShowAsConsume)
        {
            var countElement = BindingUtils.ItemStack.GetCount(Root);
            countElement.text = $"{stackInInventory.Count}/{ItemStack.Count}";
            if (stackInInventory.Count < ItemStack.Count)
            {
                countElement.RemoveFromClassList("item-count-requirement-met");
                countElement.AddToClassList("item-count-requirement-unmet");
            }
            else
            {
                countElement.RemoveFromClassList("item-count-requirement-unmet");
                countElement.AddToClassList("item-count-requirement-met");
            }
        }
    }
}