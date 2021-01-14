using System.Xml;
using System.Xml.Serialization;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PickupInteraction : AbstractInteraction
{
    private bool _isTaken;
    public InventoryItemStack Contents;
    
    public override void PerformInteraction(VisualElement element, PlayerInteractionManager manager)
    {
        if (_isTaken)
            return;
        manager.Inventory.InsertItem(Contents.Item, Contents.Count);
        _isTaken = true;
        BindElement(element, manager);
    }

    protected override void BindElement(VisualElement element, PlayerInteractionManager manager)
    {
        BindingUtils.ItemStack.Bind(element, Contents);
        var countElement = BindingUtils.ItemStack.GetCount(element);
        countElement.text = _isTaken ? "0" : $"{Contents.Count}";
    }

    public override void ResetSaveState()
    {
        _isTaken = false;
    }

    public override void WriteSaveState(XmlWriter writer)
    {
        writer.WriteStartElement("IsTaken");
        writer.WriteValue(_isTaken);
        writer.WriteEndElement();
    }

    public override void ReadSaveState(XmlReader reader)
    {
        reader.ReadStartElement("IsTaken");
        _isTaken = reader.ReadContentAsBoolean();
        reader.ReadEndElement();
    }
}