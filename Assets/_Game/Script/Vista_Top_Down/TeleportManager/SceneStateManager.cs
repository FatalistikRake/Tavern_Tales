using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneState", menuName = "ScriptableObject/Scene State")]
public class SceneStateManager : ScriptableObject
{
    public bool shouldUnloadPreviousScene;
    public string previousScene;
    public int count;

    public Scene currentScene;
    
    public void resetData()
    {
        count = 0;
        previousScene = null;
    }
}
