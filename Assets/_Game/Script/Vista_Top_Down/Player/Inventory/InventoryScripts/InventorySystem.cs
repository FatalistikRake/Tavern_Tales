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

    public UnityAction<InventorySlot> OnInvetorySlotChanged;

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
        /*inventorySlots[0] = new InventorySlot(itemToAdd, amountToAdd); return true;*/

        if(ContainsItem(itemToAdd, out List<InventorySlot> invSlot)) // Check whether item exists in inventory
        {
            foreach (var slot in invSlot)
            {
                if(slot.RoomleftInStack(amountToAdd))
                {
                    slot.AddTostack(amountToAdd);
                    OnInvetorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }
        
        if(HasFteeSlot(out InventorySlot freeSlot)) // Gests the first available slot
        {
            /*freeSlot = new InventorySlot(itemToAdd, amountToAdd);*/
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInvetorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        return false;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot)
    {
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        Debug.Log(invSlot.Count);
        return invSlot == null ? false : true;
    }

    public bool HasFteeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);

        return freeSlot == null ? false : true;
    }
}
