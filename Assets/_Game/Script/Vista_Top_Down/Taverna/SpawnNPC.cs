using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    public GameObject[] prefabs;

    private void Start()
    {
        // Effettua il controllo per verificare se hai assegnato i prefab all'array
        if (prefabs.Length == 0)
        {
            Debug.LogError("Nessun prefab assegnato allo Spawner!");
            return;
        }


        // Spawn dei prefab casualmente con un time rate di 3 secondi
        InvokeRepeating(nameof(SpawnPrefabs), 0f, 3f);
    }

    private void SpawnPrefabs()
    {
        foreach (var _ in Enumerable.Range(0,13))
        {
            Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform.position, Quaternion.identity);
        }
    }

}
