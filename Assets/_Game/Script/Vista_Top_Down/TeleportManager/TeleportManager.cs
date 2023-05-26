using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public TeleportZone[] teleportZones; // Array delle zone di teletrasporto

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (TeleportZone zone in teleportZones)
            {
                zone.TeleportPlayer();
                break;
            }
        }
    }
}
