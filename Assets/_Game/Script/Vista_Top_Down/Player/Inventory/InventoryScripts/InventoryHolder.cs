using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int invetorySize;
    [SerializeField] protected InventorySystem primaryInventorySystem;
    [SerializeField] protected int offset = 8;

    public int Offset => offset;


    public InventorySystem PrimaryInventorySystem => primaryInventorySystem;

    public static UnityAction<InventorySystem, int> OnDynamicInventoryDisplayRequested;

    protected virtual void Awake()
    {
        SaveLoad.OnLoadGame += LoadInventory;

        primaryInventorySystem = new InventorySystem(invetorySize);
    }

    protected abstract void LoadInventory(SaveData saveData);
}


[System.Serializable]
public struct InventorySaveData
{
    public InventorySystem InvSystem;
    public Vector2 Position;
    public Quaternion Rotation;

    public InventorySaveData(InventorySystem _invSystem, Vector2 _position, Quaternion _rotation)
    {
        InvSystem = _invSystem;
        Position = _position;
        Rotation = _rotation;
    }

    public InventorySaveData(InventorySystem _invSystem)
    {
        InvSystem = _invSystem;
        Position = Vector2.zero;
        Rotation = Quaternion.identity;
    }
}
