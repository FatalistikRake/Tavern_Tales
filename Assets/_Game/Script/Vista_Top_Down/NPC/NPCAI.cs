using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Runtime.CompilerServices;

public class NPCAI : MonoBehaviour
{
    private Enemy pathfindingMovement;
    private Vector3 startingPosition;
    private Vector3 roamPosition;

    private void Awake()
    {
        pathfindingMovement = GetComponent<Enemy>();
    }

    private void Start()
    {
        startingPosition = transform.position;
        //roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        //pathfindingMovement.MoveTo(roamPosition);
    }


}
