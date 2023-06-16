using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButtom;
    public bool isStacable;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    /*private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.slots[i].isFull == false)
                {
                    // Item can be aded 
                    inventory.slots[i].isFull = true;
                    Instantiate(itemButtom, inventory.slots[i].transform, false);
                    SortingLayerScript.Instance.objectsToSort.Remove(transform);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }*/

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                for (int i = 0; i < inventory.slots.Length; i++)
                {
                    if (inventory.slots[i].isFull == false || (isStacable && inventory.slots[i].item?.name == collision.gameObject.name))
                    {
                        // Item can be aded 
                        inventory.slots[i].countStack++;
                        inventory.slots[i].isFull = true;
                        inventory.slots[i].item = collision.gameObject;
                        Instantiate(itemButtom, inventory.slots[i].transform, false);
                        SortingLayerScript.Instance.objectsToSort.Remove(transform);
                        Destroy(gameObject);
                        break;
                    }
                }
            }
        }
    }
}
