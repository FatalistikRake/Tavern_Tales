using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class NPC_AI : MonoBehaviour
{
    List<GameObject> sedili = new List<GameObject>();

    public float speed = 200f;
    public Transform npcGFX;
    public Transform target;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    public Animator animator;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        target = GameObject.Find("Tavolo 1").transform;

        InvokeRepeating(nameof(UpdatePath), 0f, .5f);

        FindSedili();
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

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

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


        if (reachedEndOfPath)
        {
            NPCSiSiede();
        }
    }

    void FindSedili()
    {
        GameObject[] sediliArray = GameObject.FindGameObjectsWithTag("Sedile");
        foreach (GameObject sedile in sediliArray)
        {
            sedili.Add(sedile);
        }

        foreach (GameObject sedile in sedili)
        {
            Debug.Log(sedile.name);
        }
    }

    void NPCSiSiede()
    {
        int sedileIndex = TrovaSedileDisponibile();

        if (sedileIndex != -1)
        {
            Debug.Log("NPC si siede al sedile " + sedileIndex);

            GameObject sedileOccupato = sedili[sedileIndex];
            sedileOccupato.SetActive(false);

            // Imposta la posizione dell'NPC sulla posizione del sedile
            transform.position = sedileOccupato.transform.position;

            Debug.Log("NPCSiSiede " + transform.position);
        }
        else
        {
            Debug.Log("Non ci sono sedili disponibili");
            // Nessun sedile disponibile, sposta il GameObject dell'NPC su un altro oggetto a tua scelta
            SetNextTarget();
        }
    }

    int TrovaSedileDisponibile()
    {
        for (int i = 0; i < sedili.Count; i++)
        {
            if (sedili[i].activeSelf)
            {
                target = sedili[i].transform;
                Debug.Log("TrovaSedileDisponibile " + target.name);

                return i;
            }
        }

        return -1;
    }

    void NPCSiAlzaDalSedile(int sedileIndex)
    {
        // L'NPC si alza dal sedile
        Debug.Log("NPC si alza dal sedile " + sedileIndex);

        // Imposta il sedile come disponibile attivando il suo GameObject
        GameObject sedileDisponibile = sedili[sedileIndex];
        sedileDisponibile.SetActive(true);
    }

    void SetNextTarget()
    {
        int sedileIndex = TrovaSedileDisponibile();

        if (sedileIndex != -1)
        {
            target = sedili[sedileIndex].transform;
            Debug.Log("SetNextTarget " + target.name);
        }
        else
        {
            // Nessun sedile disponibile, sposta il GameObject dell'NPC su un altro oggetto a tua scelta
            target = GameObject.Find("Uscita").transform;
        }
    }
}
