using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public bool slide = false;

    // Update is called once per frame
    void Update()
    {
        verticalMove = Input.GetAxisRaw("Vertical");
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (verticalMove == 0f && horizontalMove == 0f)
        {
            runSpeed = 0f;
        }
        else
        {
            runSpeed = 40f;
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));


        if (Input.GetButton("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            animator.SetBool("IsCrouching", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            animator.SetBool("IsCrouching", false);
        } 
        else if (Input.GetButtonUp("Crouch")) 
        {
            crouch = false;
        }

        if (Input.GetButtonDown("Slide"))
        {
            slide = true;
            animator.SetBool("IsSlide", true);
        }
        else if (Input.GetButtonUp("Slide"))
        {
            slide = false;
            animator.SetBool("IsSlide", false);
        }

    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching (bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    public void OnSLide(bool IsSlide)
    {
        animator.SetBool("IsSlide", IsSlide);
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
