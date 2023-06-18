using System;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlot_UI[] slots;

    private void OnEnable()
    {
        PlayerInventoryHolder.OnPlayerInventoryChanged += RefreshStaticDisplay;
    }

    private void OnDisable()
    {
        PlayerInventoryHolder.OnPlayerInventoryChanged -= RefreshStaticDisplay;
    }

    private void RefreshStaticDisplay()
    {
        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.PrimaryInventorySystem;
            inventorySystem.OnInventorySlotChanged -= UpdateSlot;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        else
        {
            Debug.LogWarning($"No inventory assigned to {gameObject.name}");
        }

        AssignSlot(inventorySystem, 0);
    }

    protected override void Start()
    {
        base.Start();

        RefreshStaticDisplay();
    }

    public override void AssignSlot(InventorySystem invToDisplay, int offset)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        int inventorySlotCount = invToDisplay.InventorySlots.Count;

        for (int i = 0; i < slots.Length; i++)
        {
            int inventoryIndex = i + offset;

            if (inventoryIndex >= 0 && inventoryIndex < inventorySlotCount)
            {
                slotDictionary.Add(slots[i], invToDisplay.InventorySlots[inventoryIndex]);
                slots[i].Init(invToDisplay.InventorySlots[inventoryIndex]);
            }
            else
            {
                Debug.LogWarning($"No inventory slot found for index {inventoryIndex}");
            }
        }
    }

}
