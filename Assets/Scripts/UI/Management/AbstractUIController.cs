using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AbstractUIController : MonoBehaviour
{
    public VisualTreeAsset UITemplate { get; private set; }
    public bool Active { get; private set; } = false;

    /**
     * The address to load the uxml template from
     */
    protected abstract string TemplateName { get; }
    protected UIManager Manager;
    protected VisualElement Root;

    protected virtual void Start()
    {
        Manager = FindObjectOfType<UIManager>();
        UITemplate = UITemplates.GetTemplate(TemplateName);
    }

    public void Open(VisualElement root)
    {
        Root = root;
        Active = true;
        Bind();
    }

    public void Close()
    {
        Unbind();
        Active = false;
        Root = null;
    }

    protected virtual void Unbind()
    {
    }

    protected abstract void Bind();
}