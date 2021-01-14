using System.Collections.Generic;

public interface IInventory
{
    /**
     * <summary>The list of stacks in this inventory. Prefer using the IInventory methods over directly accessing
     * this list.</summary>
     */
    public List<InventoryItemStack> Stacks { get; }

    /**
     * <summary>Add items to this inventory</summary>
     */
    public void InsertItem(InventoryItem item, int count);

    /**
     * <summary>Removes items from this inventory</summary>
     *
     * <returns>The actual count removed</returns>
     */
    public int ExtractItem(InventoryItem item, int count);

    /**
     * <summary>Remove all of the specified item from this inventory</summary>
     *
     * <returns>The actual count removed</returns>
     */
    public int RemoveItem(InventoryItem item);

    /**
     * <returns>True if the inventory contains any of the given item</returns>
     */
    public bool Contains(InventoryItem item);

    /**
     * <summary>Get the stack for the given item, or creates one if none exist</summary>
     */
    public InventoryItemStack GetStack(InventoryItem item);
    
    /**
     * <summary>Set the stack for the given item, overwriting any other stack for the given item. Putting a stack with
     * a count of zero removes the stack.</summary>
     */
    public void PutStack(InventoryItemStack stack);

    /**
     * <summary>Get the stack for the given item, or an empty stack if there is none. The setter should raise an
     * exception if the passed item and the stack's item property don't match.</summary>
     */
    public InventoryItemStack this[InventoryItem item] { get; set; }

    public ChangeListener OnChange { get; set; }

    public delegate void ChangeListener();
}