using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPartita : MonoBehaviour
{
    private InventorySystem inventorySystem;

    void Start()
    {
        inventorySystem.GainGold(5000);
    }
}

