using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{

    public AudioSource audioSource;
    

    public void PlayButtonSwitchSFX()
    {
        audioSource.Play();
    } 
}
