using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButtom;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.slots[i].isFull == false)
                {
                    // Item can be aded 
                    inventory.slots[i].isFull = true;
                    Instantiate(itemButtom, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
