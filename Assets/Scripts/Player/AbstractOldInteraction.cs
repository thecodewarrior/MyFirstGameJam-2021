using System;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AbstractOldInteraction : MonoBehaviour, IInteraction
{
    public VisualTreeAsset Popup;

    public abstract bool PerformInteraction(VisualElement element, PlayerInteractionManager manager);
    protected abstract void BindElement(VisualElement element, PlayerInteractionManager manager);

    public VisualElement CreateElement(PlayerInteractionManager manager)
    {
        var element = Popup.Instantiate();
        BindElement(element, manager);
        return element;
    }

}
