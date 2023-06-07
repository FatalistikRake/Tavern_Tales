using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    public GameObject[] prefabs;
    Collider2D colliderComponent;

    private void Start()
    {

        colliderComponent = GetComponent<Collider2D>();

        // Effettua il controllo per verificare se hai assegnato i prefab all'array
        if (prefabs.Length == 0)
        {
            Debug.LogError("Nessun prefab assegnato allo Spawner!");
            return;
        }

        InvokeRepeating(nameof(SpawnPrefabs), 0, 5f);
    }

    private void SpawnPrefabs()
    {
        int r = Random.Range(0, prefabs.Length);
        Instantiate(prefabs[r], transform.position, Quaternion.identity);

        Debug.Log(prefabs[r].name);
    }

}
