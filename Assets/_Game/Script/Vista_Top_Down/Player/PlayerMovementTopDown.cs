using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTopDown : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    //public Animator animatorBibita;

    private Vector2 movement;
    private Vector2 lastMovement;

    private SpriteRenderer spriteRenderer;
/*    private List<Transform> children;*/
    public Transform playerContainer;

    [HideInInspector]
    public Vector2 piattoPosition;
    public bool siPuoPosizionarePiatto;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       /* children = GetChildren(transform);


        foreach (Transform child in children)
        {
            Debug.Log(child.name);
        }*/

    }

    private void Update()
    {
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement != Vector2.zero)
        {
            lastMovement = movement;
            rb.velocity = movement.normalized * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Horizontal", lastMovement.x);
            animator.SetFloat("Vertical", lastMovement.y);
            animator.SetFloat("Speed", 0);
        }

        // Impedisci il movimento diagonale
        if (movement.x != 0 && movement.y != 0)
        {
            // Zero out the vertical movement
            movement.y = 0f;
        }

        //AdJustSortingLayer();


        /// Non funziona più perché ho ingrandito il personaggio
        /*foreach (Transform child in children)
        {
            Vector3 scale = child.localScale;

            if (movement.y > 0f)
            {
                // Va verso sopra
                scale.x = -.9f;
                scale.y = .8f;
            }
            else if (movement.y < 0f)
            {
                // Va verso sotto
                scale.x = .9f;
                scale.y = .8f;
            }
            else if (movement.x > 0f)
            {
                // Va verso destra
                scale.x = .4f;
                scale.y = 1f;
            }
            else if (movement.x < 0f)
            {
                // Va verso sinistra
                scale.x = 0f;
                scale.y = .4f;
            }
            child.localScale = scale;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PosizionePiatto"))
        {
            siPuoPosizionarePiatto = true;
            // Ottenere la posizione del piatto
            piattoPosition = collision.transform.position;
            Debug.Log("PosizionePiatto" + piattoPosition);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PosizionePiatto"))
        {
            siPuoPosizionarePiatto = false;
        }
    }


    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
    }

    private void AdJustSortingLayer()
    {
        spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
    }

    /*List<Transform> GetChildren(Transform parent)
    {
        List<Transform> children = new();

        foreach (Transform child in parent)
        {
            children.Add(child);
        }
        return children;
    }*/
}
