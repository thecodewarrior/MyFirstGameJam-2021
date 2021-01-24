using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfWakeGround : MonoBehaviour
{

    private bool wolfIsAwake;
    public Slider slider;

    public float wolfWakeMeterNumber = 0;
    public float numberToWake;
    public float numberToAdd;
    public float speedOfDecrease;
    
    private Health _playerHealth;

    private void Start()
    {
        _playerHealth = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseMeter();
    }

    public void DecreaseMeter()
    {
        if (wolfIsAwake)
        {
            return;
        }

        if (wolfWakeMeterNumber > 0)
        {
            wolfWakeMeterNumber -= speedOfDecrease * Time.deltaTime;
        }

        if(wolfWakeMeterNumber < 0)
        {
            wolfWakeMeterNumber = 0;
        }

        slider.value = wolfWakeMeterNumber;
    }

    public void AddToWolfWakeMeter()
    {
        wolfWakeMeterNumber += numberToAdd;
        if(wolfWakeMeterNumber > numberToWake)
        {
            wolfWakeMeterNumber = numberToWake;
            WakeWolf();
        }
        slider.value = wolfWakeMeterNumber;
    }

    public void WakeWolf()
    {
        wolfIsAwake = true;
        wolfWakeMeterNumber = numberToWake;
        if(!_playerHealth.isDead)
            _playerHealth.InstantKillPlayer();
    }
}
