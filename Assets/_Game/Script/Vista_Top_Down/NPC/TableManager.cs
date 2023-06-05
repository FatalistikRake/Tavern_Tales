using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TableStatus
{
    public int tableNumber;
    public bool IsAvailable;
    public List<ChairStatus> chairs = new();
}

[System.Serializable]
public class ChairStatus
{
    public int chairNumber;
    public bool isOccupied = false;
}
public class TableManager : MonoBehaviour
{
    public Collider2D colliderSediaS;
    public Collider2D colliderSediaD;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other == colliderSediaS)
        {
            Debug.Log("Trigger attivato con Collider 1");
        }
        else if (other.CompareTag("Player") && other == colliderSediaD)
        {
            Debug.Log("Trigger attivato con Collider 2");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other == colliderSediaS)
        {
            Debug.Log("Trigger disattivato con Collider 1");
        }
        else if (other.CompareTag("Player") && other == colliderSediaD)
        {
            Debug.Log("Trigger disattivato con Collider 2");
        }
    }



    void TeleportToChair(ChairStatus chair)
    {
        // Logica per teletrasportarsi sulla sedia
        // ...
    }

}

