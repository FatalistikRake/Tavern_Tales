using UnityEngine;

public class PlayerMovementTopDown : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float idleThreshold = 0.01f;

    public Rigidbody2D rb;
    public Animator animator;
    public Animator animatorBibita;

    private Vector2 movement;
    private Vector2 lastMovement;
    
    private bool m_FacingDown = true;


    // Update is called once per frame
    void Update()
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

        

        /*bool isIdle = rb.velocity.magnitude < idleThreshold;
        if (isIdle)
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Horizontal", lastMovement.x);
            animator.SetFloat("Vertical", lastMovement.y);
            animator.SetFloat("Speed", 0);
        }*/

        // Impedisci il movimento diagonale
        if (movement.x != 0 && movement.y != 0)
        {
            // Zero out the vertical movement
            movement.y = 0f;
        }
        
        // Ruotare il personaggio insieme agli oggetti asse y
         if (movement.y > 0 /*&& !m_FacingDown*/)
        {
            FlipY();
        }
        // Otherwise if the input is moving the player left and the player is facing down...
        else if (movement.y < 0 /*&& m_FacingDown*/)
        {
            FlipY();
        }
        
        if (movement.x > 0 /*&& !m_FacingDown*/)
        {
            FlipX();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (movement.x < 0 /*&& m_FacingDown*/)
        {
            FlipX();
        }
        
        
    }


    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
    }
    
    private void FlipY()
    {
        // Switch the way the player is labelled as facing.
        /*m_FacingDown = !m_FacingDown;*/

        // Multiply the player's y local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.y *= -1;
        transform.localScale = theScale;
    }
    
    private void FlipX()
    {
        // Switch the way the player is labelled as facing.
        /*m_FacingRight = !m_FacingRight;*/

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
}
