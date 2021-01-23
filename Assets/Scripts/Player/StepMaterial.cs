using System;
using System.Collections.Generic;
using UnityEngine;

public class StepMaterial : MonoBehaviour
{
    public bool alertsWolf;
    public AudioClip LandSFX;
    public List<AudioClip> StepSFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var typeCheck = other.GetComponent<GroundTypeCheck>();
        if (typeCheck != null)
        {
            typeCheck.StepMaterialStack.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var typeCheck = other.GetComponent<GroundTypeCheck>();
        if (typeCheck != null)
        {
            typeCheck.StepMaterialStack.Remove(this);
        }
    }
}