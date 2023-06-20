using UnityEngine;
using UnityEngine.Events;
public class PlayerInventoryHolder : InventoryHolder
{

    
    public static UnityAction OnPlayerInventoryChanged;
    
    public static UnityAction<InventorySystem, int> OnPlayerInventoryDisplayRequested;


    private void Start()
    {
        SaveGameManager.data.playerInventory = new InventorySaveData(primaryInventorySystem);
    }

    protected override void LoadInventory(SaveData data)
    {
        // Check the save data for this specific chests inventory, and if it exists, load it in.
        if (data.playerInventory.InvSystem != null)
        {
            this.primaryInventorySystem = data.playerInventory.InvSystem;
            OnPlayerInventoryChanged?.Invoke();
        }
    }
    

    void Update()
    {
        //if (Keyboard.current.bKey.wasPressedThisFrame) OnPlayerInventoryDisplayRequested?.Invoke(primaryInventorySystem, offset);
    }

    public bool AddToInventory(InventoryItemData data, int amount)
    {
        if (primaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }

        return false;
    }

    [HideInInspector]
    public Vector2 piattoPosition;
    [HideInInspector]
    public bool siPuoPosizionarePiatto;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PosizionePiatto"))
        {
            siPuoPosizionarePiatto = true;
            // Ottenere la posizione del piatto
            piattoPosition = collision.transform.position;
            Debug.Log("PosizionePiatto" + piattoPosition);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PosizionePiatto"))
        {
            siPuoPosizionarePiatto = false;
        }
    }
}
