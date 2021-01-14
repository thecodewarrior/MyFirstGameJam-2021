using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    protected Camera cameraHolder;
    protected float startPos;
    protected float length;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        cameraHolder = Camera.main;
        startPos = transform.position.x;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = ((startPos - cameraHolder.transform.position.x) * parallaxEffect * -1);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
    }
}
