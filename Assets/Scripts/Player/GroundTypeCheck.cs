using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTypeCheck : MonoBehaviour
{

    public string groundType { get; private set; }
    public bool isOnWolfWakeGround { get; private set; }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.layer == 7)
        {
            SetGroundType(collision.tag);
        }

        if(collision.gameObject.layer == 14)
        {
            isOnWolfWakeGround = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            isOnWolfWakeGround = false;
        }
    }

    public void SetGroundType(string groundTag)
    {
        switch (groundTag)
        {
            case "Grass":
                groundType = "Grass";
                break;
            case "Stone":
                groundType = "Stone";
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
