using System;
using System.Xml.Serialization;

public static class GlobalPlayerData
{
    public static int Health { get; private set; }

    public static void DoDamage(int amount)
    {
        Health -= amount;
        if (Health < 0)
            Health = 0;
    }

    private static void ResetSaveState()
    {
        Health = 3;
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
            Health = Health
        };
    }

    private static void LoadSaveState(SaveState state)
    {
        Health = state.Health;
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
    }

    #endregion
}