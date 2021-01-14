using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stump : HidingPlace
{

    public override void HidePlayer()
    {
        if (playerMovement.isCrouching)
        {
            spriteRenderer.color = hiddenColor;
            playerMovement.HidePlayer();
        } else
        {
            spriteRenderer.color = normalColor;
            playerMovement.UnHidePlayer();
        }
        
    }
}
