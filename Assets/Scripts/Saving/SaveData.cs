using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

[Serializable]
public class SaveData
{
    [XmlArray("States")]
    [XmlArrayItem(typeof(DepositInteraction.SaveState))]
    [XmlArrayItem(typeof(PickupInteraction.SaveState))]
    [XmlArrayItem(typeof(GlobalPlayerData.SaveState))]
    public List<AbstractSaveState> XmlStates;


    public void DidRead()
    {
        _states.Clear();
        foreach (var state in XmlStates)
        {
            _states[state.Id] = state;
        }
    }

    public void WillWrite()
    {
        var xmlStates = from state in _states.Values
            orderby state.Id
            select state;
        XmlStates = xmlStates.ToList();
    }

    private Dictionary<string, AbstractSaveState> _states = new Dictionary<string, AbstractSaveState>();

    public AbstractSaveState GetState(string id) => _states.ContainsKey(id) ? _states[id] : null;

    public void SetState(string id, AbstractSaveState state)
    {
        state.Id = id;
        _states[id] = state;
    }
}

[Serializable]
public abstract class AbstractSaveState
{
    [XmlAttribute] public string Id = "";
}