using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))]
public class ShopKeeper : MonoBehaviour, IInteractable
{
    [SerializeField] private ShopItemList _shopitemsHeld;
    [SerializeField] private ShopSystem _shopSystem;

    public static UnityAction<ShopSystem, PlayerInventoryHolder> OnShopWindowsRequest;
    private UnityAction<IInteractable> onInteractionComplete;

    private void Awake()
    {
        _shopSystem = new ShopSystem(_shopitemsHeld.Items.Count, _shopitemsHeld.MaxAllowedGold, _shopitemsHeld.BuyMarkUp, _shopitemsHeld.SellMarckUp);
        
        foreach (var item in _shopitemsHeld.Items)
        {
            Debug.Log($"{item.ItemData.DislayName}: {item.Amount}");
            _shopSystem.AddToShop(item.ItemData, item.Amount);
        }
    }

    public UnityAction<IInteractable> OnInteractionComplete { get => onInteractionComplete; set => onInteractionComplete = value; }
    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        var playerInv = interactor.GetComponent<PlayerInventoryHolder>();

        if (playerInv != null)
        {
            OnShopWindowsRequest?.Invoke(_shopSystem, playerInv);
            interactSuccessful = true;
        }
        else
        {
            interactSuccessful = false;
            Debug.LogError("Player Inventory cot found");
        }
    }

    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }
}