using UnityEngine;

public class PlayerMovementTopDown : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public Animator animatorBibita;

    private Vector2 movement;
    private Vector2 lastMovement;

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



    }


    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
    }
}
