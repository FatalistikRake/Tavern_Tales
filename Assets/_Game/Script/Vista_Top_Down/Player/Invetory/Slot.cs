using System;
using Unity.VisualScripting;
using UnityEngine;

public class Slot : MonoBehaviour
{
    Inventory inventory;

    public bool isFull;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void DropItem()
    {
        foreach (Slot child in transform)
        {
            child.GetComponent<Spawn>().SpawnDroppedItem();
            Destroy(child.gameObject);
        }
    }
}
