using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPersistem : MonoBehaviour
{
    public string currentScene;
    public bool canDisable = true;
    private void Start()
    {
        GameEvent.Instance.onSceneloaded += sceneLoaded;
    }

    private void OnDestroy()
    {
        GameEvent.Instance.onSceneloaded -= sceneLoaded;
    }

    void sceneLoaded(string sceneName)
    {
        if (!canDisable) return;

        gameObject.SetActive(sceneName == currentScene);
    }
}
