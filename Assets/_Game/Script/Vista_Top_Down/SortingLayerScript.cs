using UnityEngine;

public enum SortingLayer
{
    Player = 0,
    Tables = 1
}

public class SortingLayerScript : MonoBehaviour
{
    public Transform player;
    public Transform[] tables;

    private void Update()
    {
        float playerY = player.position.y;

        for (int i = 0; i < tables.Length; i++)
        {
            float tableY = tables[i].position.y;

            if (playerY > tableY)
            {
                player.GetComponent<Renderer>().sortingLayerID = (int)SortingLayer.Tables;
            }
            else
            {
                player.GetComponent<Renderer>().sortingLayerID = (int)SortingLayer.Player;
                break; // Esci dal ciclo se il player è inferiore a un tavolo
            }
        }
    }
}
