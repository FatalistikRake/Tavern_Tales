using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 coordinationStage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.CompareTag("Player"))
        {
            /*PersistenData.instance.PlayerStartPosition = coordinationStage;*/
            if (!SceneManager.GetAllScenes().Any(s => s.name == sceneToLoad))
            {SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);}

            collision.transform.position = coordinationStage;
        }
    }
}
