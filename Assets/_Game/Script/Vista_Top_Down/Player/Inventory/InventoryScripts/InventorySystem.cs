using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;

    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);
        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        /*inventorySlots[0] = new AssignedInventorySlot(itemToAdd, amountToAdd); return true;*/

        if(ContainsItem(itemToAdd, out List<InventorySlot> invSlot)) // Check whether item exists in inventory
        {
            foreach (var slot in invSlot)
            {
                if(slot.EnoughRoomLeftInStack(amountToAdd))
                {
                    slot.AddTostack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }
        
        if(HasFreeSlot(out InventorySlot freeSlot)) // Gests the first available slot
        {
            /*freeSlot = new AssignedInventorySlot(itemToAdd, amountToAdd);*/
            if (freeSlot.EnoughRoomLeftInStack(amountToAdd))
            {
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                OnInventorySlotChanged?.Invoke(freeSlot);
                return true;
            }
            
        }

        return false;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot)
    {
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        Debug.Log(invSlot.Count);
        return invSlot == null ? false : true;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);

        return freeSlot == null ? false : true;
    }
}
