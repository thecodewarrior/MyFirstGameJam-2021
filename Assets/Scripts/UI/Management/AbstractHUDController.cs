using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AbstractHUDController : MonoBehaviour
{
    [SerializeField]
    private VisualTreeAsset UITemplate;
    
    public VisualElement Root { get; private set; }
    
    protected HUDManager Manager;
    public bool Visible => Manager.IsVisible(this);

    protected virtual void Awake()
    {
        Root = UITemplate.Instantiate();
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