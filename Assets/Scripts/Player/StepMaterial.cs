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
        var playerMovement= other.GetComponent<PlayerMovement>();
        if (playerMovement != null && !playerMovement.StepMaterialStack.Contains(this))
        {
            playerMovement.StepMaterialStack.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var playerMovement = other.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.StepMaterialStack.Remove(this);
        }
    }
}