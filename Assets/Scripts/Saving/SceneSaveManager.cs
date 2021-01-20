using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        var scene = gameObject.scene.name;
        foreach (var persistentObject in FindPersistentObjects())
        {
            var id = persistentObject.SaveID;
            if (!string.IsNullOrEmpty(id))
            {
                GlobalSaveManager.Data.SetState(scene + "_" + id, persistentObject.GetSaveState());
            }
        }
    }

    /**
     * <summary>Load data from the global save manager. Note, this does <i>not</i> load the file. To do that, call the
     * Load method on the GlobalSaveManager</summary>
     */
    public void Load()
    {
        var scene = gameObject.scene.name;
        foreach (var persistentObject in FindPersistentObjects())
        {
            var id = persistentObject.SaveID;
            if (!string.IsNullOrEmpty(id))
            {
                persistentObject.LoadSaveState(GlobalSaveManager.Data.GetState(scene + "_" + id));
            }
        }
    }
}