using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class DepositInteraction : AbstractInteraction, IPersistentObject
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

    #region Serialization

    [Header("Serialization")] [SerializeField]
    private string _SaveId;

    public string SaveID => _SaveId;

    public void LoadSaveState(AbstractSaveState state)
    {
        _isStored = (state as SaveState)?.IsStored ?? false;
    }

    public AbstractSaveState GetSaveState() => new SaveState {IsStored = _isStored};

    [XmlType("DepositInteraction")]
    public class SaveState : AbstractSaveState
    {
        public bool IsStored;
    }

    #endregion
}