using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement _interactionContainer;
    private VisualElement _currentInteractionElement;
    private VisualElement _fadeElement;

    public float FadeAlpha
    {
        get => _fadeElement.style.backgroundColor.value.a;
        set
        {
            var color = _fadeElement.style.backgroundColor.value;
            color.a = Mathf.Clamp(value, 0, 1);
            _fadeElement.style.backgroundColor = color;
        }
    }
    
    private void Awake()
    {
        _document = GetComponent<UIDocument>();
    }
    
    private void OnEnable()
    {
        var root = _document.rootVisualElement;

        _interactionContainer = root.Q<VisualElement>("interaction_container");
        _interactionContainer.visible = false;
        _fadeElement = root.Q("fade");
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
