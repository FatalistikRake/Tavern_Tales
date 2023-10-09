using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public Transform target; // Trascina qui il GameObject che rappresenta il punto di destinazione dell'NPC.

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            // Imposta la destinazione dell'NPC al punto target.
            navMeshAgent.SetDestination(target.position);
        }
    }
}
