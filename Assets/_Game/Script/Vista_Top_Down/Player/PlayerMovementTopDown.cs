using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sequences;
using static Unity.VisualScripting.Metadata;

public class PlayerMovementTopDown : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public Animator animatorBibita;

    private Vector2 movement;
    private Vector2 lastMovement;

    private SpriteRenderer spriteRenderer;
    private List<Transform> children;
    public Transform playerContainer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        children = GetChildren(transform);


        foreach (Transform child in children)
        {
                Debug.Log(child.name);
        }

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

        AdJustSortingLayer();

        foreach (Transform child in children)
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
                scale.x = -.9f;
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

    List<Transform> GetChildren(Transform parent)
    {
        List<Transform> children = new();

        foreach (Transform child in parent)
        {
            children.Add(child);
        }
        return children;
    }
}
