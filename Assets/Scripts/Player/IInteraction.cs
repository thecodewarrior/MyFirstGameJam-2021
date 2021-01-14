using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IInteraction
{
    public VisualElement CreateElement(PlayerInteractionManager manager);
    public void PerformInteraction(VisualElement element, PlayerInteractionManager manager);
}
