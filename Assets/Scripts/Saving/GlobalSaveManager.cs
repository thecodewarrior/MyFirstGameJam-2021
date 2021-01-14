using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public static class GlobalSaveManager
{
    public static string CurrentSaveName = null;
    public static SaveData Data = new SaveData();

    public static readonly string SaveDirectory = $"{Application.persistentDataPath}/saves";
    private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(SaveData));

    static GlobalSaveManager()
    {
        if (Application.isEditor)
            CurrentSaveName = "test"; // a default name when launching directly into the scene
    }

    public static void CreateSaveDirectory()
    {
        Directory.CreateDirectory(SaveDirectory);
    }

    public static string CurrentSaveFilePath()
    {
        if (CurrentSaveName == null)
            return null;
        var path = $"{SaveDirectory}/{CurrentSaveName}.xml";
        return path;
    }

    public static void Reset()
    {
        Data = new SaveData();
    }

    public static void Load()
    {
        if (CurrentSaveName == null)
        {
            Data = new SaveData();
        }
        else
        {
            var path = CurrentSaveFilePath();

            if (!File.Exists(path))
                Data = new SaveData();
            else
                Data = (SaveData) Serializer.Deserialize(new FileStream(path, FileMode.Open));
        }
    }

    public static void Save()
    {
        Serializer.Serialize(new FileStream(CurrentSaveFilePath(), FileMode.Create), Data);
    }
}