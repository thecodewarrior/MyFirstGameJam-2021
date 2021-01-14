using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;

public class SceneSaveManager : MonoBehaviour
{
    private void Start()
    {
        Load();
    }

    private List<IPersistentObject> FindPersistentObjects()
    {
        // https://stackoverflow.com/a/65495834/1541907
        var persistentObjects = new List<IPersistentObject>();
        var rootObjects = gameObject.scene.GetRootGameObjects();
        foreach (var root in rootObjects)
        {
            // Pass in "true" to include inactive and disabled children
            persistentObjects.AddRange(root.GetComponentsInChildren<IPersistentObject>(true));
        }

        return persistentObjects;
    }

    /**
     * <summary>Persist data into the global save manager. Note, this does <i>not</i> save the file. To do that, call
     * the Save method on the GlobalSaveManager</summary>
     */
    public void Persist()
    {
        foreach (var persistentObject in FindPersistentObjects())
        {
            var id = persistentObject.SaveID;
            if (id == null)
            {
                Debug.Log($"Object has null id: {persistentObject}");
            }
            else
            {
                GlobalSaveManager.Data.SetState(id, persistentObject.GetSaveState());
            }
        }
    }

    /**
     * <summary>Load data from the global save manager. Note, this does <i>not</i> load the file. To do that, call the
     * Load method on the GlobalSaveManager</summary>
     */
    public void Load()
    {
        foreach (var persistentObject in FindPersistentObjects())
        {
            persistentObject.LoadSaveState(GlobalSaveManager.Data.GetState(persistentObject.SaveID));
        }
    }
}