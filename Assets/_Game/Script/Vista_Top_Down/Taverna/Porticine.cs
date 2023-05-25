using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porticine : MonoBehaviour
{
    public GameObject door;
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("open", true);
            door.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("open", false);
        door.GetComponent<BoxCollider2D>().enabled = true;
    }
}
