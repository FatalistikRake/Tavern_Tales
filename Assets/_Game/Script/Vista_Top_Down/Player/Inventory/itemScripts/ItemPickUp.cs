using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(UniqueID))]

public class ItemPickUp : MonoBehaviour
{
    public Vector2 PickUpSize = new(1f, 1f);
    public InventoryItemData itemData;

    private CapsuleCollider2D myCollider;

    [SerializeField] private ItemPickUpSaveData itemSaveData;
    private string id;

    private void Awake()
    {
        id = GetComponent<UniqueID>().ID;
        SaveLoad.OnLoadGame += LoadGame;
        itemSaveData = new ItemPickUpSaveData(itemData, transform.position, transform.rotation);

        myCollider = GetComponent<CapsuleCollider2D>();
        myCollider.isTrigger = true;
        myCollider.size = PickUpSize;
    }

    private void Start()
    {
        SaveGameManager.data.activeItems.Add(id, itemSaveData);
    }


    private void LoadGame(SaveData data)
    {
        if (data.collectedItems.Contains(id)) Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (SaveGameManager.data.activeItems.ContainsKey(id)) SaveGameManager.data.activeItems.Remove(id);
        SaveLoad.OnLoadGame -= LoadGame;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var inventory = collision.transform.GetComponent<PlayerInventoryHolder>();

        if(!inventory) return;

        if (inventory.AddToInventory(itemData, 1))
        {
            SaveGameManager.data.collectedItems.Add(id);
            Destroy(this.gameObject);
        }
    }
}

[System.Serializable]
public struct ItemPickUpSaveData
{
    public InventoryItemData itemData;
    public Vector2 Position;
    public Quaternion Rotation;

    public ItemPickUpSaveData(InventoryItemData _data, Vector2 _position, Quaternion _rotation)
    {
        itemData = _data;
        Position = _position;
        Rotation = _rotation;
    }
}
