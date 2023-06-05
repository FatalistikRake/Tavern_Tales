using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_AI : MonoBehaviour
{
    tavernamenager taverna;

    public float speed = 200f;
    public Transform npcGFX;
    public ChairStatus TargetChair;
    public Transform target;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    public Animator animator;
    bool isWaiting = false;
    private void Awake()
    {
        taverna = FindObjectOfType<tavernamenager>();
    }
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        TargetChair = taverna.getfreeseat();

        if (TargetChair == null && !isWaiting)
        {
            isWaiting = true;
            StartCoroutine (SetTargetExit()); 
        }
        else
        {
            target = TargetChair.transform;
            TargetChair.isOccupied = true;
        }

        InvokeRepeating(nameof(UpdatePath), 0f, .5f);

        //FindSedili();
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        direction = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y));
        Vector2 force = speed * Time.deltaTime * direction;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, target.position);

        if (force.x >= 0.01f)
        {
            npcGFX.localScale = new Vector2(-1f, 1f);
        }
        else if (force.y <= -0.01f)
        {
            npcGFX.localScale = new Vector2(1f, 1f);
        }

        // Aggiorna i parametri dell'animator in base al movimento dell'NPC
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
        
        
        if (distance < 1)
        {
            NPCSiSiede();
        }

        //if (reachedEndOfPath)
        //{
        //    NPCSiSiede();
        //}
    }

    //void FindSedili()
    //{
    //    GameObject[] sediliArray = GameObject.FindGameObjectsWithTag("Sedile");
    //    foreach (GameObject sedile in sediliArray)
    //    {
    //        sedili.Add(sedile);
    //    }

    //    foreach (GameObject sedile in sedili)
    //    {
    //        Debug.Log(sedile.name);
    //    }
    //}

    void NPCSiSiede()
    {
        transform.position = target.transform.position;

        
        Debug.Log("NPCSiSiede " + transform.position);
    }

    //int TrovaSedileDisponibile()
    //{
    //    for (int i = 0; i < sedili.Count; i++)
    //    {
    //        if (sedili[i].activeSelf)
    //        {
    //            target = sedili[i].transform;
    //            Debug.Log("TrovaSedileDisponibile " + target.name);

    //            return i;
    //        }
    //    }

    //    return -1;
    //}

    //void NPCSiAlzaDalSedile(int sedileIndex)
    //{
    //    // L'NPC si alza dal sedile
    //    Debug.Log("NPC si alza dal sedile " + sedileIndex);

    //    // Imposta il sedile come disponibile attivando il suo GameObject
    //    GameObject sedileDisponibile = sedili[sedileIndex];
    //    sedileDisponibile.SetActive(true);
    //}

    IEnumerator SetTargetExit()
    {
        yield return new WaitForSeconds(5);
        target = GameObject.Find("UscitaNPC").transform;
        isWaiting = false;
    }
}
