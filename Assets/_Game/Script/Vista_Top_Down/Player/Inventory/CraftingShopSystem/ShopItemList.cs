using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop System/Shop Item List")]
public class ShopItemList : ScriptableObject
{
    [SerializeField] private List<ShopInvetoryItem> _items;
    [SerializeField] private int _maxAllowedGold;
    [SerializeField] private float _sellMarckUp;
    [SerializeField] private float _buyMarkUp;

    public List<ShopInvetoryItem> Items => _items;
    public int MaxAllowedGold => _maxAllowedGold;
    public float SellMarckUp => _sellMarckUp;
    public float BuyMarkUp => _buyMarkUp;

}

[System.Serializable]
public struct ShopInvetoryItem
{
    public InventoryItemData ItemData;
    public int Amount;
}
    
