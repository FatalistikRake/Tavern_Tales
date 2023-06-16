using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class tavernamenager : MonoBehaviour
{
    public List <TableManager> tavoli;

    public ChairStatus getfreeseat ()
    {
        return tavoli.FirstOrDefault(t => t.status.IsAvailable)?.status.chairs.FirstOrDefault(c => c.isOccupied == false);
    }
}
