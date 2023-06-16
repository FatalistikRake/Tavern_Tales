using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class TableStatus
{
    public int tableNumber;
    public bool IsAvailable => chairs.Any(c => !c.isOccupied);
    public List<ChairStatus> chairs = new();
}


public class TableManager : MonoBehaviour
{
    public TableStatus status;
}

