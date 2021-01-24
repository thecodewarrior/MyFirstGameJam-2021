using System;
using Cinemachine;
using UnityEngine;

public class CinemachineTimescaleFixer : MonoBehaviour
{
    private CinemachineBrain _brain;

    private void Start()
    {
        _brain = GetComponent<CinemachineBrain>();
    }

    private void Update()
    {
        _brain.m_UpdateMethod = Time.timeScale == 0
            ? CinemachineBrain.UpdateMethod.LateUpdate
            : CinemachineBrain.UpdateMethod.SmartUpdate;
    }
}