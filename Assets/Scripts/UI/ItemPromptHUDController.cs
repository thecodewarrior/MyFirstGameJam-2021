using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemPromptHUDController : AbstractTrackingHUDController
{
    protected override string TemplateName => "item_prompt";

    [Tooltip("The items to display")] public List<InventoryItemStack> Items;

    [Tooltip("Whether to display as a requirement (e.g. '<amount in inventory>/<count>')")]
    public bool ShowAsConsume;

    private VisualTreeAsset _itemTemplate;

    protected override void Start()
    {
        base.Start();
        _itemTemplate = UITemplates.GetTemplate("item_stack");
    }

    public override void OnShow()
    {
        var itemContainer = Root.Q("item_container");
        foreach (var oldElement in itemContainer.Children().ToList())
        {
            itemContainer.Remove(oldElement);
        }

        foreach (var stack in Items)
        {
            var itemElement = _itemTemplate.Instantiate();
            
            var stackInInventory = GlobalPlayerData.Inventory[stack.Item];
            BindingUtils.ItemStack.Bind(itemElement, stack);

            if (ShowAsConsume)
            {
                var countElement = BindingUtils.ItemStack.GetCount(itemElement);
                countElement.text = $"{stackInInventory.Count}/{stack.Count}";
                if (stackInInventory.Count < stack.Count)
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
            
            itemContainer.Add(itemElement);
        }
    }
}