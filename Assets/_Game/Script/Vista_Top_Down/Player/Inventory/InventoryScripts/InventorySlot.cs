using System;
using UnityEngine;

[System.Serializable]
public class InventorySlot : ISerializationCallbackReceiver
{
    [NonSerialized] private InventoryItemData itemData;
    [SerializeField] private int _itemID = -1;
    [SerializeField] private int stackSize;

    public InventoryItemData ItemData => itemData;

    public int StackSize => stackSize;

    public InventorySlot(InventoryItemData source, int amount)
    {
        itemData = source;
        _itemID = itemData.ID;
        stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        _itemID = -1;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot)
    {
        if (itemData == invSlot.itemData) AddTostack(invSlot.stackSize);
        else
        {
            itemData = invSlot.itemData;
            _itemID = itemData.ID;
            stackSize = 0;
            AddTostack(invSlot.stackSize);
        }
    }

    public void UpdateInventorySlot(InventoryItemData data, int amount)
    {
        itemData = data;
        stackSize = amount;
    }

    public bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = itemData.MaxStackSize - stackSize;

        return EnoughRoomLeftInStack(amountToAdd);
    }

    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        if (itemData == null || itemData != null && stackSize + amountToAdd <= itemData.MaxStackSize) return true;
        else return false;
    }

    public void AddTostack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if (stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(itemData, halfStack);
        return true;
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        if (_itemID == -1) return;

        var db = Resources.Load<Database>(path: "Database");
        itemData = db.GetItem(_itemID);
    }
}
