using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : MonoBehaviour
{
    public SceneStateManager sceneStateManager;
    public string sceneToLoad; // Il nome della scena da attivare
    public Vector2 coordinationStage; // Le coordinate per spostare il player

    public Transform objectToTransport; // L'oggetto da trasportare

    private void Start()
    {
        GameEvent.Instance.onPickupObject += OnItemPickup;
        sceneStateManager.resetData();
    }

    private void OnDestroy()
    {
        GameEvent.Instance.onPickupObject -= OnItemPickup;
    }

    private void Update()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        objectToTransport = player;
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sceneStateManager.count++;            

            if (!SceneManager.GetAllScenes().Any(s => s.name == sceneToLoad))
            {
                StopAllCoroutines();
                StartCoroutine(LoadSceneAsync());
            }

            collision.transform.position = coordinationStage;

            

            // Trova l'oggetto da trasportare nella scena correntesceneStateManager.currentScene
            //GameObject objectToTransport = GameObject.FindWithTag("ObjectToTransport");


        }
    }

    IEnumerator LoadSceneAsync()
    {
        yield return SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        sceneStateManager.currentScene = SceneManager.GetSceneByName(sceneToLoad);
        Debug.Log(sceneStateManager.currentScene.name);
        SceneManager.SetActiveScene(sceneStateManager.currentScene);        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sceneStateManager.currentScene = SceneManager.GetSceneByName(sceneToLoad);

            // Controlla il valore booleano nel SceneStateManager
            if (sceneStateManager.count >= 2)
            {
                SceneManager.UnloadSceneAsync(sceneStateManager.previousScene);

                sceneStateManager.resetData();
            }
            GameEvent.Instance.OnSceneLoaded(sceneToLoad);
            sceneStateManager.previousScene = sceneStateManager.currentScene.name;
        }
    }

    void OnItemPickup(GameObject item)
    {
        objectToTransport = item.transform;            
        if (objectToTransport != null)
        {
            Scene currentScene = SceneManager.GetSceneByName("ObjectPull");
            SceneManager.MoveGameObjectToScene(objectToTransport.gameObject, currentScene);
            item.GetComponent<OpenPersistem>().currentScene = sceneStateManager.previousScene;
        }
    }
}
