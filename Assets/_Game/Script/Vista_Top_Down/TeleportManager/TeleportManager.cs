using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : MonoBehaviour
{
    public SceneStateManager sceneStateManager;
    public string sceneToLoad;
    public Vector2 coordinationStage;


    private void Update()
    {
        sceneStateManager.currentScene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sceneStateManager.count++;
            sceneStateManager.currentScene = SceneManager.GetSceneByName(sceneToLoad);
            Debug.Log(sceneStateManager.currentScene.name);

            if (!SceneManager.GetAllScenes().Any(s => s.name == sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
                SceneManager.SetActiveScene(sceneStateManager.currentScene);
            }

            collision.transform.position = coordinationStage;

            //SceneManager.SetActiveScene(sceneStateManager.currentScene);
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

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Controlla il valore booleano nel SceneStateManager
            if (sceneStateManager.count == 2)
            {
                SceneManager.UnloadSceneAsync(sceneStateManager.previousScene);

                sceneStateManager.count = 0;
                sceneStateManager.previousScene = null;
            }
            sceneStateManager.previousScene = sceneStateManager.currentScene.name;
        }
    }*/
}
