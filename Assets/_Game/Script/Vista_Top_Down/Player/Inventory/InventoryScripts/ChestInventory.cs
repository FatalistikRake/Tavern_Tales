using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

[RequireComponent(typeof(UniqueID))]
public class ChestInventory : InventoryHolder, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    protected override void Awake()
    {
        base.Awake();

        SaveLoad.OnLoadGame += LoadInventory;
    }

    protected override void LoadInventory(SaveData saveData)
    {
        // Check the save data for this specific chest's inventory, and if it exists, load it.
        if (SaveGameManager.data.chestDictionary.TryGetValue(GetComponent<UniqueID>().ID, out InventorySaveData chestData))
        {
            primaryInventorySystem = chestData.InvSystem;
            transform.position = chestData.Position;
            transform.rotation = chestData.Rotation;
        }
    }


    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem, 0);
        interactSuccessful = true;
    }

    public void EndInteraction()
    {

    }
}



