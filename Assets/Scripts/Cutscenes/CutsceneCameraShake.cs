using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CutsceneCameraShake : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;

    // Start is called before the first frame update
    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // print("shaking camera");
            // ShakeCamera();
        }
    }
    public void ShakeCamera()
    {
        impulseSource.GenerateImpulse();
    }
}
