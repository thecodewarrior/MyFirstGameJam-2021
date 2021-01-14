using UnityEngine.UIElements;

public static class BindingUtils
{
    public static class ItemStack
    {
        public static void Bind(VisualElement element, InventoryItemStack stack)
        {
            GetCount(element).text = stack.Count == 1 ? "" : stack.Count.ToString();
            GetName(element).text = stack.Item.ItemName;
            GetDescription(element).text = stack.Item.ItemDescription;
            element.Q("item_icon").style.backgroundImage = new StyleBackground(stack.Item.ItemIcon);
        }

        public static Label GetCount(VisualElement element) => element.Q<Label>("item_count");
        public static Label GetName(VisualElement element) => element.Q<Label>("item_name");
        public static Label GetDescription(VisualElement element) => element.Q<Label>("item_description");
    }
}