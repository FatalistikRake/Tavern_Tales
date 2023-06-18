/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_GFX : MonoBehaviour
{
    private Vector2 movementDirection;
    public float speed = 200f;
    public Animator animator;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // Imposta la direzione iniziale del movimento dell'NPC
        movementDirection = new Vector2(1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(1f, 0f);
    }

    void FixedUpdate()
    {
        Vector2 movement = movementDirection.normalized * speed;
        rb.MovePosition(rb.Position + movement * Time.fixedDeltaTime);

        // Aggiorna i parametri dell'animator in base al movimento dell'NPC
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }
}*/