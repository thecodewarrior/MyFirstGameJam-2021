using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement _interactionContainer;
    private VisualElement _currentInteractionElement;
    
    private void Awake()
    {
        _document = GetComponent<UIDocument>();
    }
    
    private void OnEnable()
    {
        var root = _document.rootVisualElement;

        _interactionContainer = root.Q<VisualElement>("interaction_container");
        _interactionContainer.visible = false;
    }

    public void ShowInteraction(VisualElement interactionElement)
    {
        _interactionContainer.Clear();
        _interactionContainer.Add(interactionElement);
        _currentInteractionElement = interactionElement;
        _interactionContainer.visible = true;
    }
    
    public void ClearInteraction()
    {
        _interactionContainer.Clear();
        _currentInteractionElement = null;
        _interactionContainer.visible = false;
    }
}
