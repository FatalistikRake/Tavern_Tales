using UnityEngine.InputSystem;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay InventoryPanel;
    public DynamicInventoryDisplay playerBackpackPanel;

    private void Awake()
    {
        InventoryPanel.gameObject.SetActive(false);
        playerBackpackPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
    }

    private void Update()
    {
        // if (Keyboard.current.bKey.wasPressedThisFrame) DisplayInventory(new PrimaryInventorySystem(Random.Range(20, 30)));

        //si apre con e
        if (InventoryPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            InventoryPanel.gameObject.SetActive(false);

        // si apre con b
        if (playerBackpackPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            playerBackpackPanel.gameObject.SetActive(false);
    }

    void DisplayInventory(InventorySystem invToDisplay, int offset)
    {
        InventoryPanel.gameObject.SetActive(true);
        InventoryPanel.RefreshDynamicInventory(invToDisplay, offset);
    }
}