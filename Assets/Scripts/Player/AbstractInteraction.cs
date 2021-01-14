using System;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AbstractInteraction : MonoBehaviour, IInteraction
{
    public VisualTreeAsset Popup;

    public abstract void PerformInteraction(VisualElement element, PlayerInteractionManager manager);
    protected abstract void BindElement(VisualElement element, PlayerInteractionManager manager);

    public VisualElement CreateElement(PlayerInteractionManager manager)
    {
        var element = Popup.Instantiate();
        BindElement(element, manager);
        return element;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var manager = other.GetComponent<PlayerInteractionManager>();
        if (manager)
        {
            manager.BeginInteraction(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var manager = other.GetComponent<PlayerInteractionManager>();
        if (manager)
        {
            manager.EndInteraction(this);
        }
    }
}
