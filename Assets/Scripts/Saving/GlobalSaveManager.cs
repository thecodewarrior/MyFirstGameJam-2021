using System.IO;
using UnityEngine;

public static class GlobalSaveManager
{
    public static string CurrentSaveName = null;
    public static readonly string SaveDirectory = $"{Application.persistentDataPath}/saves";

    public static void CreateSaveDirectory()
    {
        Directory.CreateDirectory(SaveDirectory);
    }
    
    public static string CurrentSaveFilePath()
    {
        if (CurrentSaveName == null)
            return null;
        var path = $"{SaveDirectory}/{CurrentSaveName}.xml";
        if (!File.Exists(path))
            return null;
        return path;
    }
}