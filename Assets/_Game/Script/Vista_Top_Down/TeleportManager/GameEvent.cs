using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameEvent : SingletonClass<GameEvent>
{    

    public UnityAction<GameObject> onPickupObject;
    public void OnPikupObject(GameObject pikupObject)
    {
        onPickupObject.Invoke(pikupObject);
    }

    public UnityAction<string> onSceneloaded;
    public void OnSceneLoaded(string current)
    {
        onSceneloaded?.Invoke(current);
    }


}
