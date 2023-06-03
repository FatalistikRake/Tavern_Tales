using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    public GameObject npcPrefab;
    private GameObject newNPC;

    private void Start()
    {
        CloneNPC();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SpawnerNPC();
        }
    }

    public void SpawnerNPC()
    {
        if (newNPC != null)
        {
            Vector2 spawnPosition = transform.position;
            Instantiate(newNPC, spawnPosition, Quaternion.identity);
        }
    }

    public void CloneNPC()
    {
        newNPC = Instantiate(npcPrefab, transform.position, Quaternion.identity);
        newNPC.name = "NPC(Clone)";
    }
}
