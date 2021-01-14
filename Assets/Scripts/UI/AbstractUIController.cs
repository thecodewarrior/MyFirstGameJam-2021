using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AbstractUIController : MonoBehaviour
{
    public VisualTreeAsset UITemplate;
    public bool Active { get; private set; } = false;
    
    protected UIManager Manager;
    protected VisualElement Root;

    private void Start()
    {
        Manager = FindObjectOfType<UIManager>();
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