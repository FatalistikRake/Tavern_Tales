using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ShopSystem
{
    [SerializeField] private List<ShopSlot> _shopInventory;
    [SerializeField] private int _avaiableGold;
    [SerializeField] private float _buyMarckUp;
    [SerializeField] private float _sellMarckUp;

    public List<ShopSlot> ShopInventory => _shopInventory;

    public int AvaiableGold => _avaiableGold;

    public float BuyMarkUP => _buyMarckUp;
    public float SellMarkUP => _sellMarckUp;

    public ShopSystem(int Size, int gold, float buyMarkUp, float sellMarckUp)
    {
        _avaiableGold = gold;
        _buyMarckUp = buyMarkUp;
        _sellMarckUp = sellMarckUp;

        SetShopSize(Size);
    }

    private void SetShopSize(int size)
    {
        _shopInventory = new List<ShopSlot>(size);

        for (int i = 0; i < size; i++)
        {
            _shopInventory.Add(new ShopSlot());
        }
    }

    public void AddToShop(InventoryItemData data, int amount)
    {
        if (ContainsItem(data, out ShopSlot shopSlot))
        {
            shopSlot.AddTostack(amount);
        }

        var freeSlot = GetFreeSlot();
        freeSlot.AssignItem(data, amount);
    }

    private ShopSlot GetFreeSlot()
    {
        var freeSlot = _shopInventory.FirstOrDefault(i => i.ItemData == null);

        if (freeSlot == null)
        {
            freeSlot = new ShopSlot();
            _shopInventory.Add(freeSlot);
        }
        return freeSlot;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out ShopSlot shopSlot)
    {
        shopSlot = _shopInventory.Find(i => i.ItemData == itemToAdd);
        return shopSlot != null;
    }
}
