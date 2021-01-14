using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Custom Asset/Inventory Item", order = 100)]
public class InventoryItem : ScriptableObject
{
    [SerializeField]
    private string _ItemName;
    [TextArea]
    [SerializeField]
    private string _ItemDescription;

    // using properties to future-proof for localization
    public string ItemName => _ItemName;
    public string ItemDescription => _ItemDescription;

    public Sprite ItemIcon;
}
