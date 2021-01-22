using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryListUIController : AbstractUIController
{
    protected override string TemplateName => "inventory";
    private VisualTreeAsset _slotTemplate;
    
    private ListView _itemListView;


    protected override void Start()
    {
        base.Start();
        _slotTemplate = UITemplates.GetTemplate("inventory_slot");
    }

    protected override void Bind()
    {
        _itemListView = Root.Q<ListView>("item_list");
        _itemListView.itemHeight = 75;

        _itemListView.makeItem = () => _slotTemplate.Instantiate();
        _itemListView.bindItem = (element, i) => { BindingUtils.ItemStack.Bind(element, GlobalPlayerData.Inventory.Stacks[i]); };
        _itemListView.itemsSource = GlobalPlayerData.Inventory.Stacks;
    }

    protected override void Unbind()
    {
        _itemListView = null;
    }

    private void OnInventoryChange()
    {
        if (Active)
        {
            _itemListView.Refresh();
        }
    }

    private void OnEnable()
    {
        if(GlobalPlayerData.Inventory == null)
            Debug.LogWarning("InventoryListUIController doesn't have an Inventory specified");
        else
            GlobalPlayerData.Inventory.OnChange += OnInventoryChange;
    }

    private void OnDisable()
    {
        if(GlobalPlayerData.Inventory == null)
            Debug.LogWarning("InventoryListUIController doesn't have an Inventory specified");
        else
            GlobalPlayerData.Inventory.OnChange -= OnInventoryChange;
    }

    private void Update()
    {
        if (Active && (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Inventory")))
        {
            Manager.Pop();
        }
    
        if (!UIManager.HasInputFocus && Input.GetButtonDown("Inventory"))
        {
            Manager.Push(this);
        }
    }
}