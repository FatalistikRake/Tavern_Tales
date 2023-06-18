using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))]
public class ShopKeeper : MonoBehaviour, IInteractable
{
    [SerializeField] private ShopItemList _shopitemsHeld;
    private ShopSystem _shopSystem;

    private void Awake()
    {
        _shopSystem = new ShopSystem(_shopitemsHeld.Items.Count, _shopitemsHeld.MaxAllowedGold, _shopitemsHeld.BuyMarkUp, _shopitemsHeld.SellMarckUp);
        
        foreach (var item in _shopitemsHeld.Items)
        {
            Debug.Log
            _shopSystem.AddToShop(item.ItemData, item.Amount);
        }
    }

    public UnityAction<IInteractable> OnInteractionComplete { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        throw new System.NotImplementedException();
    }
}