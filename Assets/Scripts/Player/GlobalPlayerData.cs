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
    public static string SceneName;
    public static string SceneEntrance;

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
        SceneName = null;
        SceneEntrance = null;
    }

    public static void ResetHealth()
    {
        Health = MaxHealth;
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
            Inventory = _inventory.Stacks.Select(e => e.GetSaveState()).ToList(), // copy the list
            SceneName = SceneName,
            SceneEntrance = SceneEntrance,
        };
    }

    private static void LoadSaveState(SaveState state)
    {
        Health = state.Health;
        _inventory.Clear();
        foreach (var stackState in state.Inventory)
        {
            var stack = InventoryItemStack.LoadSaveState(stackState);
            _inventory.InsertItem(stack.Item, stack.Count);
        }

        SceneName = state.SceneName;
        SceneEntrance = state.SceneEntrance;
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
        public List<InventoryItemStack.SaveState> Inventory;
        public string SceneName;
        public string SceneEntrance;
    }

    #endregion
}