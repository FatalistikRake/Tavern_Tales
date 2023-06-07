using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TableStatus
{
    public int tableNumber;
    public bool IsAvailable;
    public List<ChairStatus> chairs = new();
}


public class TableManager : MonoBehaviour
{
    public TableStatus status;
}

