using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;

public class SceneSaveManager : MonoBehaviour
{
    private Dictionary<string, ISavable> _idMap;
    private string savePath;

    public void Register(ISavable savable)
    {
        var id = savable.SaveID;
        if(_idMap.ContainsKey(id)) 
            throw new ArgumentException($"Duplicate save ID '{id}'");
        _idMap[id] = savable;
    }

    private void Start()
    {
        savePath = GlobalSaveManager.CurrentSaveFilePath();
        if(savePath != null)
            Load();
    }

    public void Save()
    {
        var writer = XmlWriter.Create(savePath);
        writer.WriteStartDocument();
        writer.WriteStartElement("SaveData");
        foreach(var entry in _idMap)
        {
            writer.WriteStartElement(entry.Value.GetType().Name);
            writer.WriteAttributeString("id", entry.Key);
            entry.Value.WriteSaveState(writer);
            writer.WriteEndElement();
        }
        writer.WriteEndElement();
        writer.WriteEndDocument();
    }
    
    public void Load()
    {
        var reader = XmlReader.Create(savePath);
        var lineInfoReader = (IXmlLineInfo) reader;
        reader.ReadStartElement("SaveData");
        
        foreach (var savable in _idMap.Values)
        {
            savable.ResetSaveState();
        }
        
        while (reader.IsStartElement())
        {
            var location = $"{lineInfoReader.LineNumber}:{lineInfoReader.LinePosition}";
            reader.ReadStartElement();
            var id = reader.GetAttribute("id");
            if (id == null)
            {
                throw new Exception($"Save entry missing id attribute at {location}");
            }
            _idMap[id].ReadSaveState(reader);
            reader.ReadEndElement();
        }
        reader.ReadEndElement();
    }
}