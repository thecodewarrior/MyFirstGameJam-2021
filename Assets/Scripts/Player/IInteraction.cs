using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IInteraction
{
    public VisualElement CreateElement(PlayerInteractionManager manager);
    /**
     * Returns true when the operation was successful
     */
    public bool PerformInteraction(VisualElement element, PlayerInteractionManager manager);
}
