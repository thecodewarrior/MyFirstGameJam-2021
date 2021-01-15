using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Inventory : MonoBehaviour, IInventory, ISerializationCallbackReceiver//, IPersistentObject // TODO: ItemStack serialization
{
    [SerializeField] private List<InventoryItemStack> _stacks = new List<InventoryItemStack>();

    public List<InventoryItemStack> Stacks
    {
        get
        {
            Clean();
            return _stacks;
        }
    }

    public InventoryItemStack this[InventoryItem item]
    {
        get => GetStack(item);
        set
        {
            if (item != value.Item)
                throw new ArgumentException($"Passed item ({item.name}) doesn't match the stack's item ({value.Item})");
            PutStack(value);
        }
    }

    public InventoryItemStack GetStack(InventoryItem item)
    {
        var index = IndexOf(item);
        return index >= 0 ? _stacks[index] : new InventoryItemStack {Item = item, Count = 0};
    }

    public void PutStack(InventoryItemStack stack)
    {
        var index = IndexOf(stack.Item);
        if (index >= 0)
        {
            if (stack.Count > 0)
                _stacks[index] = stack;
            else
                _stacks.RemoveAt(index);
        }
        else
        {
            if (stack.Count > 0)
                _stacks.Add(stack);
        }

        OnChange();
    }

    public void InsertItem(InventoryItem item, int count)
    {
        var stack = GetStack(item);
        stack.Count += count;
        PutStack(stack);
    }

    public int ExtractItem(InventoryItem item, int count)
    {
        var stack = GetStack(item);
        var actualCount = Math.Min(count, stack.Count);
        stack.Count -= actualCount;
        PutStack(stack);
        return actualCount;
    }

    public int RemoveItem(InventoryItem item)
    {
        var stack = GetStack(item);
        var actualCount = stack.Count;
        stack.Count = 0;

        Clean();
        return actualCount;
    }

    public bool Contains(InventoryItem item) => GetStack(item).Count > 0;

    public IInventory.ChangeListener OnChange { get; set; }

    private void Clean()
    {
#if !UNITY_EDITOR
        _stacks.RemoveAll(stack => stack.Count <= 0);
#endif
    }

    private int IndexOf(InventoryItem item)
    {
        return _stacks.FindIndex(stack => stack.Item == item);
    }

    public void OnBeforeSerialize()
    {
        Clean();
    }

    public void OnAfterDeserialize()
    {
    }

    #region Serialization

    [Header("Serialization")] [SerializeField]
    private string _SaveId;

    public string SaveID => _SaveId;

    public void LoadSaveState(AbstractSaveState state)
    {
        if (!(state is SaveState _state))
            return;
        _stacks.Clear();
        _stacks.AddRange(_state.Stacks);
        Clean();
    }

    public AbstractSaveState GetSaveState()
    {
        var stacksCopy = new List<InventoryItemStack>();
        stacksCopy.AddRange(_stacks);
        return new SaveState {Stacks = stacksCopy};
    }

    [XmlType("Inventory")]
    public class SaveState : AbstractSaveState
    {
        public List<InventoryItemStack> Stacks;
    }

    #endregion
}