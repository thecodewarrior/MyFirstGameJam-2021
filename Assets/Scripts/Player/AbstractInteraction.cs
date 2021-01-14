using System;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AbstractInteraction : MonoBehaviour, IInteraction, ISavable
{
    [SerializeField] private string _SaveID;
    public VisualTreeAsset Popup;

    public abstract void PerformInteraction(VisualElement element, PlayerInteractionManager manager);
    protected abstract void BindElement(VisualElement element, PlayerInteractionManager manager);
    
    // ISavable
    public string SaveID => _SaveID;
    public abstract void ResetSaveState();
    public abstract void WriteSaveState(XmlWriter writer);
    public abstract void ReadSaveState(XmlReader reader);

    private void Start()
    {
        if (_SaveID != "")
        {
            var saveManager = FindObjectOfType<SceneSaveManager>();
            saveManager.Register(this);
        }
    }

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
