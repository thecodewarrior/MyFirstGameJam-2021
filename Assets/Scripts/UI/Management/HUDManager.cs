using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDManager : MonoBehaviour
{
    private UIDocument _document;
    private VisualElement _overlayContainer;
    private VisualElement _fadeElement;

    private HashSet<AbstractHUDController> _visibleControllers = new HashSet<AbstractHUDController>();

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

        _overlayContainer = root.Q("overlay_container");
        _fadeElement = root.Q("fade");
    }

    public bool IsVisible(AbstractHUDController controller)
    {
        return _visibleControllers.Contains(controller);
    }

    public void ShowController(AbstractHUDController controller)
    {
        if (_visibleControllers.Contains(controller))
            return;
        
        _visibleControllers.Add(controller);
        _overlayContainer.Add(controller.Root);
        controller.OnShow();
    }

    public void HideController(AbstractHUDController controller)
    {
        if (!_visibleControllers.Contains(controller))
            return;
        _visibleControllers.Remove(controller);

        controller.OnHide();
        _overlayContainer.Remove(controller.Root);
    }
}