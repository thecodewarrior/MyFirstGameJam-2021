using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryListUIController : AbstractUIController
{
    private ListView _itemListView;

    public VisualTreeAsset ElementTemplate;

    protected override void Bind()
    {
        _itemListView = Root.Q<ListView>("item_list");
        _itemListView.itemHeight = 50;

        _itemListView.makeItem = () => ElementTemplate.Instantiate();
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
        if (!UIManager.HasInputFocus && Input.GetButtonDown("Inventory"))
        {
            Manager.Push(this);
        }

        if (Active && Input.GetButtonDown("Cancel"))
        {
            Manager.Pop();
        }
    }
}