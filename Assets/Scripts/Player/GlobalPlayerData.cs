using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Inventory;

public static class GlobalPlayerData
{
    public static int MaxHealth = 3;
    public static int Health { get; private set; }
    private static BasicInventory _inventory = new BasicInventory();
    public static IInventory Inventory => _inventory;

    public static void DoDamage(int amount)
    {
        Health -= amount;
        if (Health < 0)
            Health = 0;
    }

    private static void ResetSaveState()
    {
        Health = MaxHealth;
        _inventory.Clear();
    }
    
    static GlobalPlayerData()
    {
        ResetSaveState(); // initialize by resetting
    }
    
    #region Serialization
    
    private static SaveState GetSaveState()
    {
        return new SaveState
        {
            Health = Health,
            Inventory = _inventory.Stacks.Select(e => e).ToList() // copy the list
        };
    }

    private static void LoadSaveState(SaveState state)
    {
        Health = state.Health;
        _inventory.Clear();
        foreach (var stack in state.Inventory)
        {
            _inventory.InsertItem(stack.Item, stack.Count);
        }
    }

    public static void Persist() => GlobalSaveManager.Data.SetState("global", GetSaveState());

    public static void Load()
    {
        var rawState = GlobalSaveManager.Data.GetState("global");
        if (rawState != null && rawState is SaveState state)
        {
            LoadSaveState(state);
        }
        else
        {
            ResetSaveState();
        }
    }

    [XmlType("GlobalPlayerData")]
    public class SaveState : AbstractSaveState
    {
        public int Health;
        public List<InventoryItemStack> Inventory;
    }

    #endregion
}