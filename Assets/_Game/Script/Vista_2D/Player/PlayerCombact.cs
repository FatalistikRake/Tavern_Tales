using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCombact : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public int maxHealt = 300;
    public int currentHealt;

    public float attackRange = 0.5f;
    public int attackDamage1 = 10;
    public int attackDamage2 = 20;
    public int attackDamage3 = 30;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public bool onGuard = false;

    void Start()
    {
        currentHealt = maxHealt;
    }

    void Update()
    {
        ControllEntranceBossComplete();

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                Attack1();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Attack2();
                nextAttackTime = Time.time + 1f / attackRate;

            }
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack3();
                nextAttackTime = Time.time + 1f / attackRate;

            }
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            onGuard = true;
            animator.SetBool("IsOnGuard", true);
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            onGuard = false;
            animator.SetBool("IsOnGuard", false);
        }
    }

    void Attack1()
    {
        // Play an attack animator
        animator.SetTrigger("Attack1");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<BossCombact>().TakeDamageBoss(attackDamage1);
        }
    }

    void Attack2()
    {
        // Play an attack animator
        animator.SetTrigger("Attack2");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<BossCombact>().TakeDamageBoss(attackDamage2);
        }
    }

    void Attack3()
    {
        // Play an attack animator
        animator.SetTrigger("Attack3");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<BossCombact>().TakeDamageBoss(attackDamage3);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamagePlayer(int damage)
    {
        currentHealt -= damage;

        // Play hurt enimation
        animator.SetTrigger("Hurt");

        if (currentHealt <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Player died");

        // Die animation
        animator.SetBool("IsDead", true);
        animator.SetBool("IsDead", false);
    }

    void ControllEntranceBossComplete()
    {
        
        if (TryGetComponent<BossCombact>(out var bossCombact))
        {
            bool entranceComplete = bossCombact.entranceComplete;

            if (!entranceComplete)
            {
                attackDamage1 = 0;
                attackDamage2 = 0;
                attackDamage3 = 0;
            }
            else
            {
                attackDamage1 = 10;
                attackDamage2 = 20;
                attackDamage3 = 30;
            }
        }
        else
        {
/*            Debug.Log("Non esiste questo componente");
*/        }

    }
}
