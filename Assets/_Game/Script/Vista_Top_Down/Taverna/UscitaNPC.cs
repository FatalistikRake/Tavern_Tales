using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UscitaNPC : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            Debug.Log("NPC esce");

            // Distruggi l'oggetto NPC_AI
            Destroy(collision.gameObject);
        }
    }
}
