using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float scrollSpeed = 1f; // Velocità di scorrimento tra gli slot

    private int currentSlotIndex = 0;
    public Slot[] slots;

    private void Update()
    {
        float scrollInput = Input.mouseScrollDelta.y;
        if (scrollInput > 0)
        {
            ScrollToNextSlot();
        }
        else if (scrollInput < 0)
        {
            ScrollToPreviousSlot();
        }

        // Rilascio dell'oggetto con un tasto
        if (Input.GetKeyDown(KeyCode.E))
        {
            DropItemFromCurrentSlot();
        }
    }
    private void UpdateSlotVisuals()
    {
        // Aggiorna l'aspetto visuale degli slot dell'inventario ed evidenzia lo slot corrente)
        for (int i = 0; i < slots.Length; i++)
        {
            Color slotColor = (i == currentSlotIndex) ? Color.red : Color.white;
            slots[i].GetComponent<SpriteRenderer>().color = slotColor;
        }
    }

    private void DropItemFromCurrentSlot()
    {
        // Rilascia l'oggetto dallo slot corrente ed chiama la funzione dello slot)
        Slot currentSlot = slots[currentSlotIndex];
        currentSlot.GetComponent<Spawn>().SpawnDroppedItem();
    }


    private void ScrollToNextSlot()
    {
        currentSlotIndex = (currentSlotIndex + 1) % slots.Length;  // Incrementa l'indice e ripristina se si supera la lunghezza
        UpdateSlotVisuals();
    }

    private void ScrollToPreviousSlot()
    {
        currentSlotIndex = (currentSlotIndex - 1 + slots.Length) % slots.Length;  // Decrementa l'indice e ripristina se si va al di sotto di zero
        UpdateSlotVisuals();
    }

    
}
