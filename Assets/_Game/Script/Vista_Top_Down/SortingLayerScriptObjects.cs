using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SortingLayerScript : SingletonClass<SortingLayerScript>
{
    public List<Transform> objectsToSort;
    public List<Transform> characters;
    private Transform player;
    private Transform shop;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        shop = GameObject.FindWithTag("Shop").transform;
    }

    private void Start()
    {
        characters.Add(player);
        objectsToSort.Add(shop);

        GameObject[] tavoliObjects = GameObject.FindGameObjectsWithTag("Tavoli");
        foreach (GameObject tavoliObject in tavoliObjects)
        {
            objectsToSort.Add(tavoliObject.transform);
        }

        GameObject[] sedileObjects = GameObject.FindGameObjectsWithTag("Sedile");
        foreach (GameObject sedileObject in sedileObjects)
        {
            objectsToSort.Add(sedileObject.transform);
        }
    }


    private void Update()
    {
        // Controllo se spunta un nuovo NPC e lo aggiungo alla lista dei personaggi
        GameObject[] npcObjects = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npcObject in npcObjects)
        {
            if (!objectsToSort.Contains(npcObject.transform))
            {
                objectsToSort.Add(npcObject.transform);
            }
        }

        foreach (Transform obj in objectsToSort)
        {
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();

            int highestSortingOrder = int.MinValue;

            // Calcola il sorting order basato sulla posizione rispetto ai personaggi
            foreach (Transform character in characters)
            {
                float yPos = character.position.y;
                int sortingOrder = Mathf.RoundToInt(-yPos * 100);
                highestSortingOrder = Mathf.Max(highestSortingOrder, sortingOrder);
            }

            float objYPos = obj.position.y;
            int objSortingOrder = Mathf.RoundToInt(-objYPos * 100);

            // Applica un offset al sorting order dell'oggetto rispetto al sorting order pi� alto dei personaggi
            int finalSortingOrder = objSortingOrder - highestSortingOrder;

            spriteRenderer.sortingOrder = finalSortingOrder;
        }
    }
}
