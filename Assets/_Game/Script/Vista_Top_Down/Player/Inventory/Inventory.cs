using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public float scrollSpeed = 1f; // Velocità di scorrimento tra gli slot

    private int currentSlotIndex = 0;
    public Slot[] slots;

    private PlayerMovementTopDown piattoPos;


    private void Start()
    {
        slots = FindObjectsOfType<Slot>().OrderBy(x => x.name).ToArray();
        piattoPos = FindObjectOfType<PlayerMovementTopDown>();
    }

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
        // Aggiorna l'aspetto visuale degli slot dell'inventario ed evidenzia lo slot corrente
        for (int i = 0; i < slots.Length; i++)
        {
            Color slotColor = (i == currentSlotIndex) ? new Color(1f, 1f, 1f, 0.7f) : Color.white;
            slots[i].GetComponent<Image>().color = slotColor;
        }
    }

    private void DropItemFromCurrentSlot()
    {
         // Rilascia l'oggetto dallo slot corrente ed chiama la funzione dello slot
        Slot currentSlot = slots[currentSlotIndex];
        if (piattoPos.siPuoPosizionarePiatto)
        {
            currentSlot.DropItem();
        }
        else
        {
            Debug.Log("Non puoi posizionare nulla");
        }

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
