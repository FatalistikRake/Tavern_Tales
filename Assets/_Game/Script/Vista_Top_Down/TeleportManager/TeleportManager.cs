using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : MonoBehaviour
{
    public SceneStateManager sceneStateManager;
    public string sceneToLoad; // Il nome della scena da attivare
    public Vector2 coordinationStage; // Le coordinate per spostare il player

    public Transform objectToTransport; // L'oggetto da trasportare


    private void Update()
    {
        sceneStateManager.currentScene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sceneStateManager.count++;
            
            

            if (!SceneManager.GetAllScenes().Any(s => s.name == sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
                Debug.Log(sceneStateManager.currentScene.name);
                //SceneManager.SetActiveScene(sceneStateManager.currentScene);
            }

            collision.transform.position = coordinationStage;

            //SceneManager.SetActiveScene(sceneStateManager.currentScene);

            // Trova l'oggetto da trasportare nella scena corrente
            //GameObject objectToTransport = GameObject.FindWithTag("ObjectToTransport");

            if (objectToTransport != null)
            {
                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.MoveGameObjectToScene(objectToTransport.gameObject, currentScene);

            }
        }
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

                sceneStateManager.count = 0;
                sceneStateManager.previousScene = null;
            }

            sceneStateManager.previousScene = sceneStateManager.currentScene.name;
        }
    }
}
