using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ExampleClass : MonoBehaviour
{
    void Start()
    {
        var textures = Resources.LoadAll("Textures", typeof(Texture2D)).Cast<Texture2D>().ToArray();
        foreach (var t in textures)
            Debug.Log(t.name);
    }
}