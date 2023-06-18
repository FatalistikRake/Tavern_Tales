using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MouseItemData : MonoBehaviour
{
    ///<summary>
    ///se non voglio eliminare gli oggetti dev andare su mouseObject
    ///è nei child ci metto Raucast target true
    ///così non verrà eliminato nella scermata

    public Image ItemSprite;
    public TextMeshProUGUI ItemCount;
    public InventorySlot AssignedInventorySlot;

    private Transform _playerTransform;

    [HideInInspector]
    public Transform piattoPosition;
    Collider2D collision;

    private void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemSprite.preserveAspect = true; 
        ItemCount.text = "";

        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (_playerTransform == null) Debug.Log("Player not found");
    }

    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        AssignedInventorySlot.AssignItem(invSlot);
        UpdateMouseSlot();
    }
    public void UpdateMouseSlot()
    {
        ItemSprite.sprite = AssignedInventorySlot.ItemData.Icon;
        ItemCount.text = AssignedInventorySlot.StackSize.ToString();
        ItemSprite.color = Color.white;
    }

    private void Update()
    {
        if (AssignedInventorySlot.ItemData != null)
        {
            transform.position = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame && !isPointerOverUIObject() /*&& collision.CompareTag("PosizionePiatto")*/)
            {
                piattoPosition = collision.transform;
                if (AssignedInventorySlot.ItemData.ItemPrefab != null) Instantiate(AssignedInventorySlot.ItemData.ItemPrefab, piattoPosition);
                Debug.Log("PosizionePiatto" + piattoPosition);


                if(AssignedInventorySlot.StackSize > 1)
                {
                    AssignedInventorySlot.AddTostack(-1);
                    UpdateMouseSlot();
                }
                else
                {
                    ClearSlot();
                }
                
            }
        }
    }

    public void ClearSlot()
    {
        AssignedInventorySlot.ClearSlot();
        ItemCount.text = "";
        ItemSprite.color = Color.clear;
        ItemSprite.sprite = null;
    }

    public static bool isPointerOverUIObject()
    {
        PointerEventData eventDaraCurrentPosition = new PointerEventData(EventSystem.current);
        eventDaraCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDaraCurrentPosition, results);
        return results.Count > 0;
    }
}
