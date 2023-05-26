using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenData : MonoBehaviour
{
    public Vector2 PlayerStartPosition;
    public static PersistenData instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

}
