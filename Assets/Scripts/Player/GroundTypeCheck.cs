using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTypeCheck : MonoBehaviour
{

    public string groundType { get; private set; }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.layer == 7)
        {
            SetGroundType(collision.tag);
        }
    }

    public void SetGroundType(string groundTag)
    {
        switch (groundTag)
        {
            case "Grass":
                groundType = "Grass";
                break;
            case "Gravel":
                groundType = "Gravel";
                break;
            case "Swamp":
                groundType = "Swamp";
                break;
            case "Wood":
                groundType = "Wood";
                break;
            default:
                break;
        }
    }
}
