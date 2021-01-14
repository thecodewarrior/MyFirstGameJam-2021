using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteractionManager : MonoBehaviour
{
    public IInventory Inventory;
    private IInteraction _currentInteraction;
    private VisualElement _currentInteractionElement;

    private HUDController _hud;
    
    void Start()
    {
        _hud = FindObjectOfType<HUDController>();
        Inventory = GetComponent<Inventory>();
    }
    
    public void BeginInteraction(IInteraction interaction)
    {
        if (_currentInteraction != interaction)
        {
            _currentInteractionElement = interaction.CreateElement(this);
            _hud.ShowInteraction(_currentInteractionElement);
            _currentInteraction = interaction;
        }
    }

    public void EndInteraction(IInteraction interaction)
    {
        if (interaction == _currentInteraction)
        {
            _hud.ClearInteraction();
            _currentInteraction = null;
            _currentInteractionElement = null;
        }
    }

    public void Update()
    {
        if (_currentInteraction != null && Input.GetButton("Submit"))
        {
            _currentInteraction.PerformInteraction(_currentInteractionElement, this);
        }
    }
}
