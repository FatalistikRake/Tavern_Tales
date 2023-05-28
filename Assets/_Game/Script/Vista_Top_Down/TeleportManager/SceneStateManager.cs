using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : ScriptableObject
{
    public bool shouldUnloadPreviousScene;
    public string previousScene = null;
    public int count = 0;

    public Scene currentScene;
}
