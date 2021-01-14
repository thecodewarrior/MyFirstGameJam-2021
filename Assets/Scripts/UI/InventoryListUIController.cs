using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryListUIController : AbstractUIController
{
    private ListView _itemListView;

    public VisualTreeAsset ElementTemplate;
    public Inventory Inventory;

    protected override void Bind()
    {
        _itemListView = Root.Q<ListView>("item_list");
        _itemListView.itemHeight = 50;

        _itemListView.makeItem = () => ElementTemplate.Instantiate();
        _itemListView.bindItem = (element, i) => { BindingUtils.ItemStack.Bind(element, Inventory.Stacks[i]); };
        _itemListView.itemsSource = Inventory.Stacks;

        // root.Q<Button>("add_button").clicked += () => { Inventory.InsertItem(HolyWater, 1); };
        // root.Q<Button>("remove_button").clicked += () => { Inventory.ExtractItem(HolyWater, 1); };
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
        Inventory.OnChange += OnInventoryChange;
    }

    private void OnDisable()
    {
        Inventory.OnChange -= OnInventoryChange;
    }

    private void Update()
    {
        if (!Active && Input.GetButtonDown("Inventory"))
        {
            Manager.Push(this);
        }

        if (Active && Input.GetButtonDown("Cancel"))
        {
            Manager.Pop();
        }
    }
}