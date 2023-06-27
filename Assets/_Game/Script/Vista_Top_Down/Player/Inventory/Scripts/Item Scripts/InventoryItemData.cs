using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// This is a scriptable object, that defines what an item is in our game.
/// It could be inherited from to have branched version of items, for example potions and equipment.
/// </summary>

[CreateAssetMenu(menuName = "InventoryM System/InventoryM Item")]
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

    public void UseItem(PlayerMovementTopDown _player, ChairStatus _chairStatus, ItemSlot AssignedInventorySlot)
    {
        Debug.Log($"Using {DisplayName}");

        if (ItemPrefab != null && _player.siPuoPosizionarePiatto && !_chairStatus.platePositionIsOccupied)
        {
            Debug.Log(_player.siPuoPosizionarePiatto);
            Debug.Log(_player.piattoPosition);

            _chairStatus.platePositionIsOccupied = true;
            Instantiate(ItemPrefab, _player.piattoPosition, Quaternion.identity);

            if (AssignedInventorySlot.StackSize > 1)
            {
                AssignedInventorySlot.AddToStack(-1);
            }

        }
        else
        {
            Debug.Log("Non puoi usare l'oggetto per qualche motivo");
        }
    }
}
