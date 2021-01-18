using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AbstractHUDController : MonoBehaviour
{
    /**
     * The address to load the uxml template from
     */
    protected abstract string TemplateName { get; }
    
    public VisualElement Root { get; private set; }
    
    protected HUDManager Manager;
    public bool Visible => Manager.IsVisible(this);

    protected virtual void Awake()
    {
        var template = UITemplates.GetTemplate(TemplateName);
        Root = template.Instantiate();
        Root.style.position = new StyleEnum<Position>(Position.Absolute);
        Root.style.left = new StyleLength(0f);
        Root.style.right = new StyleLength(0f);
        Root.style.top = new StyleLength(0f);
        Root.style.bottom = new StyleLength(0f);
    }

    protected virtual void Start()
    {
        Manager = FindObjectOfType<HUDManager>();
    }
    
    public virtual void OnShow()
    {
    }

    public virtual void OnHide()
    {
    }
}