using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 coordinationStage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadScene());
        }
    }

    private IEnumerator LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
        yield return new  WaitForSeconds(0.1f); // aggiungo un ritardo di 0,5 secondi

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = coordinationStage;
        }
    }
}
