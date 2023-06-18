using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_AI : MonoBehaviour
{
    tavernamenager taverna;
    ChairStatus sittingPosition;
    private Collider2D colliderComponent;

    public float speed = 20f;
    public Transform npcGFX;
    private ChairStatus TargetChair;
    private Transform target;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    bool isWaiting = false;
    public Animator animator;


    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        taverna = FindObjectOfType<tavernamenager>();
    }
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliderComponent = GetComponent<Collider2D>();

        TargetChair = taverna.getfreeseat();

        if (TargetChair == null /*&& !isWaiting*/)
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
        if (seeker.IsDone() && target != null)
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            Debug.Log(p);
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
        //rb.MovePosition((Vector2)path.vectorPath[currentWaypoint] * speed);

        float distance = Vector2.Distance(rb.position, target.position);

        //if (force.x >= 0.01f)
        //{
        //    npcGFX.localScale = new Vector2(-1f, 1f);
        //}
        //else if (force.y <= -0.01f)
        //{
        //    npcGFX.localScale = new Vector2(1f, 1f);
        //}

        // Aggiorna i parametri dell'animator in base al movimento dell'NPC
        animator.SetFloat("Horizontal", -direction.x);
        animator.SetFloat("Vertical", -direction.y);
        animator.SetFloat("Speed", reachedEndOfPath? 0 : 1 );


        if (distance < 1.2 && TargetChair != null)
        {
            NPCSiSiede();
        }
    }

    void NPCSiSiede()
    {
        colliderComponent.enabled = false;
        transform.position = TargetChair.SittingPosition.position;
        Vector2 directionplate = ((Vector2)TargetChair.PlatePosition.position - rb.position).normalized;

        animator.SetFloat("Horizontal", directionplate.x);
        animator.SetFloat("Vertical", directionplate.y);
        animator.SetFloat("Speed", 0);


        spriteRenderer.sortingOrder = 1;

        Debug.Log("NPCSiSiede " + transform.position);
    }
    
    void NPCSialza()
    {
        colliderComponent.enabled = true;
        spriteRenderer.sortingOrder = 0;

        Debug.Log("NPCSialza " + transform.position);
    }

    IEnumerator SetTargetExit()
    {
        yield return new WaitForSeconds(5);
        target = GameObject.Find("UscitaNPC").transform;
        isWaiting = false;
    }
}