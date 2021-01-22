using System;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public struct InventoryItemStack
{
    public InventoryItem Item;
    public int Count;

    public SaveState GetSaveState()
    {
        return new SaveState
        {
            Item = Item.name,
            Count = Count,
        };
    }

    public static InventoryItemStack LoadSaveState(SaveState state)
    {
        return new InventoryItemStack
        {
            Item = Resources.Load<InventoryItem>($"items/{state.Item}"),
            Count = state.Count,
        };
    }

    [XmlType("ItemStack")]
    public struct SaveState
    {
        public string Item;
        public int Count;
    }
}