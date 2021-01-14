using System.Xml;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class DepositInteraction : AbstractInteraction
{
    private bool _isStored;
    public InventoryItemStack Requirement;

    public override void PerformInteraction(VisualElement element, PlayerInteractionManager manager)
    {
        if (_isStored)
            return;
        if (manager.Inventory[Requirement.Item].Count < Requirement.Count)
            return;
        manager.Inventory.ExtractItem(Requirement.Item, Requirement.Count);
        _isStored = true;
        BindElement(element, manager);
    }

    protected override void BindElement(VisualElement element, PlayerInteractionManager manager)
    {
        var stackInInventory = manager.Inventory[Requirement.Item];
        BindingUtils.ItemStack.Bind(element, Requirement);
        var countElement = BindingUtils.ItemStack.GetCount(element);
        if (_isStored)
        {
            countElement.RemoveFromClassList("item-count-requirement-unmet");
            countElement.RemoveFromClassList("item-count-requirement-met");
        }
        else
        {
            countElement.text = $"{stackInInventory.Count}/{Requirement.Count}";
            if (stackInInventory.Count < Requirement.Count)
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
    
    public override void ResetSaveState()
    {
        _isStored = false;
    }

    public override void WriteSaveState(XmlWriter writer)
    {
        writer.WriteStartElement("IsStored");
        writer.WriteValue(_isStored);
        writer.WriteEndElement();
    }

    public override void ReadSaveState(XmlReader reader)
    {
        reader.ReadStartElement("IsStored");
        _isStored = reader.ReadContentAsBoolean();
        reader.ReadEndElement();
    }
}