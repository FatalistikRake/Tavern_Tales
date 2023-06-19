using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour
{
    [SerializeField] private Image _itemsprite;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemCount;
    [SerializeField] private ShopSlot _assingnedItemSlot;

    public ShopSlot AssingnedItemSlot => _assingnedItemSlot;

    [SerializeField] private Button _addItemToCartButton;
    [SerializeField] private Button _removeItemFromCartButton;

    private int _tempAmount;

    private ShopKeeperDisplay ParentDisplay { get; set; }
    public float MarkUp { get; private set; }


    private void Awake()
    {
        _itemsprite.sprite = null;
        _itemsprite.preserveAspect = true;
        _itemsprite.color = Color.clear;
        _itemName.text = "";
        _itemCount.text = "";

        _addItemToCartButton?.onClick.AddListener(AddItemToCart);
        _removeItemFromCartButton?.onClick.AddListener(RemoveItemFromCart);
        ParentDisplay = transform.parent.GetComponentInParent<ShopKeeperDisplay>();

    }

    public void Init(ShopSlot slot, float markUp)
    {
        _assingnedItemSlot = slot;
        MarkUp = markUp;
        _tempAmount = slot.StackSize;
        UpdateUISlot();
    }

    private void UpdateUISlot()
    {
        if (_assingnedItemSlot.ItemData != null)
        {
            _itemsprite.sprite = _assingnedItemSlot.ItemData.Icon;
            _itemsprite.color = Color.white;
            _itemCount.text = _assingnedItemSlot.StackSize.ToString();
            _itemName.text = $"{_assingnedItemSlot.ItemData.DislayName} - {_assingnedItemSlot.ItemData.GoldValue} G";
        }
        else
        {
            _itemsprite.sprite = null;
            _itemsprite.color = Color.clear;
            _itemName.text = "";
            _itemCount.text = "";
        }
    }

    private void RemoveItemFromCart()
    {
        if (_tempAmount == _assingnedItemSlot.StackSize) return;

        _tempAmount++;
        ParentDisplay.RemoveItemFromCart(this);
        _itemCount.text = _tempAmount.ToString();

    }

    private void AddItemToCart()
    {
        if (_tempAmount > 0)
        {
            _tempAmount--;
            ParentDisplay.AddItemCart(this);
            _itemCount.text = _tempAmount.ToString();
        }
    }
}