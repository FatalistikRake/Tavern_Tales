using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int invetorySize;
    [SerializeField] protected InventorySystem primaryInventorySystem;

    public InventorySystem InventorySystem => primaryInventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;

    private void Awake()
    {
        primaryInventorySystem = new InventorySystem(invetorySize);
    }
}
