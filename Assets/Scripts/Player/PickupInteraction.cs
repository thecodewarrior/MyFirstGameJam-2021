using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PickupInteraction : AbstractOldInteraction, IPersistentObject
{
    private bool _isTaken;
    public InventoryItemStack Contents;

    public override bool PerformInteraction(VisualElement element, PlayerInteractionManager manager)
    {
        if (!_isTaken)
        {
            manager.Inventory.InsertItem(Contents.Item, Contents.Count);
            _isTaken = true;
            BindElement(element, manager);
        }

        return true;
    }

    protected override void BindElement(VisualElement element, PlayerInteractionManager manager)
    {
        BindingUtils.ItemStack.Bind(element, Contents);
        var countElement = BindingUtils.ItemStack.GetCount(element);
        countElement.text = _isTaken ? "0" : $"{Contents.Count}";
    }

    #region Serialization

    [Header("Serialization")] [SerializeField]
    private string _SaveId;

    public string SaveID => _SaveId;

    public void LoadSaveState(AbstractSaveState state)
    {
        _isTaken = (state as SaveState)?.IsTaken ?? false;
    }

    public AbstractSaveState GetSaveState() => new SaveState {IsTaken = _isTaken};

    [XmlType("PickupInteraction")]
    public class SaveState : AbstractSaveState
    {
        public bool IsTaken;
    }

    #endregion
}