using UnityEngine.SceneManagement;
using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 coordinationStage;

    public void TeleportPlayer()
    {
        SceneManager.LoadScene(sceneToLoad);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = coordinationStage;
        }
    }
}
