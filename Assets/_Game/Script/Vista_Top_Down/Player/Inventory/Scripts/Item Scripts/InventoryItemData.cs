using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// This is a scriptable object, that defines what an item is in our game.
/// It could be inherited from to have branched version of items, for example potions and equipment.
/// </summary>

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public int ID = -1;
    public string DisplayName;
    [TextArea(4, 4)]
    public string Description;
    public Sprite Icon;
    public int MaxStackSize;
    public int GoldValue;
    public GameObject ItemPrefab;

    private PlayerInventoryHolder _playerInventory;
    private InventorySlot AssignedInventorySlot;
    private Transform _playerTransform;
    private float _dropOffset = 0.5f;

    public void UseItem()
    {
        Debug.Log($"Using {DisplayName}");
        if (AssignedInventorySlot.ItemData.ItemPrefab != null && _playerInventory.siPuoPosizionarePiatto)
        {
            Instantiate(AssignedInventorySlot.ItemData.ItemPrefab, _playerInventory.piattoPosition, Quaternion.identity);

            if (AssignedInventorySlot.StackSize > 1)
            {
                AssignedInventorySlot.AddToStack(-1);
            }
        }

        

    }
}
