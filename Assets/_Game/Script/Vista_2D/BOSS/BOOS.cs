using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHealt = 300;
    int currentHealt;

    void Start()
    {
        currentHealt = maxHealt;
    }

    public void TakeDamage(int damage)
    {
        currentHealt -= damage;

        // Play hurt enimation
        animator.SetTrigger("Hurt");

        if (currentHealt <= 0)
        {
            Die();
        }
    }
    void Die ()
    {
        Debug.Log("Enemy died");

        // Die animation
        animator.SetBool("IsDead", true);

        // Disable the enemy
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }
}
