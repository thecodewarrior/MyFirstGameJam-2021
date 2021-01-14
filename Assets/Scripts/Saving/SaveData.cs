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
    public List<AbstractSaveState> XmlStates
    {
        get
        {
            var xmlStates = from state in _states.Values
                orderby state.Id
                select state;
                
            return xmlStates.ToList();
        }
        set
        {
            _states.Clear();
            foreach (var state in value)
            {
                _states[state.Id] = state;
            }
        }
    }

    private Dictionary<string, AbstractSaveState> _states = new Dictionary<string, AbstractSaveState>();
    public AbstractSaveState GetState(string id) => _states[id];

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