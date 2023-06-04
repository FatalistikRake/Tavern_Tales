using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    [System.Serializable]
    public class TableStatus
    {
        public GameObject tableObject;
        public bool isAvailable;
        public List<GameObject> sedili;
    }

    public List<TableStatus> tables; // Lista delle informazioni sui tavoli

    private void Start()
    {
        tables = new List<TableStatus>();

        // Aggiungi tavoli alla lista e cerca i sedili associati
        for (int i = 0; i < 10; i++)
        {
            GameObject tableObject = /* ... logic per creare il GameObject del tavolo ... */;

            TableStatus tableStatus = new TableStatus
            {
                tableObject = tableObject,
                isAvailable = true,
                sedili = new List<GameObject>()
            };

            // Aggiungi i sedili associati al tavolo corrente
            FindSedili(tableObject, tableStatus.sedili);

            tables.Add(tableStatus);
        }
    }

    void FindSedili(GameObject tableObject, List<GameObject> sediliList)
    {
        GameObject[] sediliArray = GameObject.FindGameObjectsWithTag("Sedile");
        foreach (GameObject sedile in sediliArray)
        {
            // Verifica se il sedile è associato al tavolo corrente
            if (sedile.transform.parent == tableObject.transform)
            {
                sediliList.Add(sedile);
            }
        }
    }

    // Resto del codice...
}
