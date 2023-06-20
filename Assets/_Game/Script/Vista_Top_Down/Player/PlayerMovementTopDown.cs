using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovementTopDown : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Vector2 lastMovement;

    private SpriteRenderer spriteRenderer;
    public Transform playerContainer;

    [HideInInspector]
    public Vector2 piattoPosition;
    public bool siPuoPosizionarePiatto;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Movement.performed += OnMovementPerformed;
        playerControls.Player.Movement.canceled += OnMovementCanceled;
    }

    private void OnEnable()
    {
        playerControls?.Enable();
    }

    private void OnDisable()
    {
        playerControls?.Disable();
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        movement = Vector2.zero;
    }

    private void Update()
    {
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

        // AdJustSortingLayer();
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

    private void AdJustSortingLayer()
    {
        spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
    }
}
