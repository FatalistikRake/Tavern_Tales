using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class ItemPickUp : MonoBehaviour
{
    public Vector2 PickUpSize = new(1f, 1f);
    public InventoryItemData itemData;

    private CapsuleCollider2D myCollider;

    private void Awake()
    {
        myCollider = GetComponent<CapsuleCollider2D>();
        myCollider.isTrigger = true;
        myCollider.size = PickUpSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var inventory = collision.transform.GetComponent<InventoryHolder>();

        if(!inventory) return;

        if (inventory.PrimaryInventorySystem.AddToInventory(itemData, 1))
        {
            Destroy(this.gameObject);
        }
    }
}
