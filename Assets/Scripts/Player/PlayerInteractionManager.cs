using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteractionManager : MonoBehaviour
{
    public IInventory Inventory;
    private IInteractionSource _openSource;
    private IInteraction _openInteraction;
    private VisualElement _openInteractionElement;
    private List<IInteractionSource> _sources = new List<IInteractionSource>();

    private HUDManager _hud;

    void Start()
    {
        _hud = FindObjectOfType<HUDManager>();
        Inventory = GlobalPlayerData.Inventory;
    }

    public void AddSource(IInteractionSource source)
    {
        if (!_sources.Contains(source))
            _sources.Add(source);
    }

    public void RemoveSource(IInteractionSource source)
    {
        _sources.Remove(source);
    }

    private (IInteractionSource, IInteraction) GetInteraction()
    {
        for (var i = _sources.Count - 1; i >= 0; i--)
        {
            var source = _sources[i];
            var interaction = source.GetInteraction(this);
            if (interaction != null)
                return (source, interaction);
        }

        return (null, null);
    }

    public void Update()
    {
        var (source, interaction) = GetInteraction();
        if (interaction != _openInteraction)
        {
            if (interaction == null)
            {
                _hud.ClearInteraction();
                _openSource = null;
                _openInteraction = null;
                _openInteractionElement = null;
            }
            else
            {
                _openSource = source;
                _openInteractionElement = interaction.CreateElement(this);
                _hud.ShowInteraction(_openInteractionElement);
                _openInteraction = interaction;
            }
        }
        
        if (_openInteraction != null && Input.GetButtonDown("Fire1") && !UIManager.HasInputFocus)
        {
            if (_openInteraction.PerformInteraction(_openInteractionElement, this))
            {
                _openSource.PerformInteraction(_openInteraction, this);
            }
        }
    }
}