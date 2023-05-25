using System;
using System.Collections;
using UnityEngine;


public class BossCombact : MonoBehaviour
{
    //Serve per la logica del combattimento

    public Animator animator;
    public GameObject player;
    public Rigidbody2D rb;

    [Header("Vita")]
    public float maxHealth = 300;
    public float currentHealth;

    [Header("Scudo")]
    public float maxShield = 200;
    public float currentShield;
    public float shieldRegenerationRate = 5f;

    [Header("invulnerabilità")]
    public float invulnerabilityDuration = 3f; // Durata dell'invulnerabilità in secondi
    public bool isInvulnerable = false; // Flag per indicare se l'oggetto è invulnerabile
    public float invulnerabilityTimer = 0f; // Timer per tenere traccia della durata dell'invulnerabilità
    public bool canTakeDamage = true; // Flag per indicare se l'oggetto può prendere danno



    [Header("Attacco")]
    public Transform attackPoint;
    public LayerMask playerLayers;
    public GameObject bulletPrefab; // proiettile da sparare
    public Transform firePoint; // punto di partenza del proiettile
    public float speed = 5f;
    public float attackRange = 1f;
    public float attackRate = 2f;
    public float nextAttackTime = 1f;
    public bool isAttacking1 = false;
    public bool isAttacking2 = false;
    public float fireRate = 2f; // rateo di fuoco in secondi

    public bool isPlayerInsideCircle = false;

    [Header("Movimento")]
    private bool isTouchedByPlayer = false;
    public float moveSpeed = 1f; // velocità di movimento del boss
    public bool movingRight = true;  // Indica se il boss sta andando verso destra o sinistra
    public float moveDistance = 100f; // Distanza massima di movimento a destra e sinistra
    private Vector3 initialPosition;
    private Vector3 leftBoundary;
    private Vector3 rightBoundary;

    [Header("Entrata")]
    public bool entranceComplete = false; // Per indicare se l'entrata del boss è stata completata

    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;

        animator = GetComponent<Animator>();
        animator.Play("BossEntranceAnimation");

        AnimationClip entranceAnimationClip = animator.runtimeAnimatorController.animationClips[0];
        AnimationEvent entranceCompleteEvent = new()
        {
            time = entranceAnimationClip.length, // L'evento viene posizionato alla fine dell'animazione
            functionName = "EntranceComplete" // Il nome della funzione da chiamare quando si verifica l'evento
        };
        entranceAnimationClip.AddEvent(entranceCompleteEvent);

        // Memorizza la posizione iniziale del boss
        initialPosition = transform.position;
        leftBoundary = initialPosition + new Vector3(-moveDistance, 0f, 0f);
        rightBoundary = initialPosition + new Vector3(moveDistance, 0f, 0f);

    }

    void Update()
    {

        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayers);
        isPlayerInsideCircle = player != null && player.gameObject.CompareTag("Player");

        if (!entranceComplete || isTouchedByPlayer)
            return;

        if (player != null)
        {
            if (transform.position.x < player.transform.position.x)
                movingRight = true;
            else
                movingRight = false;
        }

        // Movimento orizzontale
        if (!isPlayerInsideCircle && entranceComplete && !isTouchedByPlayer)
        {
            if (movingRight)
            {
                transform.Translate(moveSpeed * Time.deltaTime * Vector2.right);
                transform.localScale = new Vector3(1, 1, 1);  // Flippa l'immagine a sinistra

                if (transform.position.x < rightBoundary.x)
                {
                    transform.Translate(moveSpeed * Time.deltaTime * Vector2.right);
                }
                else
                {
                    movingRight = false;
                }
            }
            else
            {
                transform.Translate(moveSpeed * Time.deltaTime * Vector2.left);
                transform.localScale = new Vector3(-1, 1, 1);  // Flippa l'immagine a destra

                if (transform.position.x > leftBoundary.x)
                {
                    transform.Translate(moveSpeed * Time.deltaTime * Vector2.left);
                }
                else
                {
                    movingRight = true;
                }
            }
        }

        if (isPlayerInsideCircle && canTakeDamage == true)
        {
            isAttacking1 = true;
            //isAttacking2 = true;
        }
        else if (!isPlayerInsideCircle)
        {
            isAttacking1 = false;
            //isAttacking2 = false;
        }

        if (currentShield < 0)
        {
            ActivateInvulnerability();
        }

        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime; // Aggiorna il timer dell'invulnerabilità

            if (invulnerabilityTimer <= 0f)
            {
                // Fine dell'invulnerabilità
                isInvulnerable = false;
                canTakeDamage = true;
                // Esegui qui eventuali azioni dopo la fine dell'invulnerabilità
                animator.SetBool("IsRegeneration", false);
            }
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
    public void TakeDamageBoss(int damage)
    {
        if (currentShield > 0)
        {
            currentShield -= damage;
            animator.SetTrigger("Hurt");
        }
        else if (currentShield <= 0)
        {
            currentHealth -= damage;
            animator.SetTrigger("Hurt");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy died");

        // Die animation
        animator.SetBool("IsDead", true);

        // Disable the enemy
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }

    public void ActivateInvulnerability()
    {
        isInvulnerable = true;
        invulnerabilityTimer = invulnerabilityDuration;
        canTakeDamage = false;
        // Esegui qui eventuali azioni all'attivazione dell'invulnerabilità

        animator.SetBool("IsRegeneration", true);
    }

    public void EntranceComplete()
    {
        //Questo metodo viene chiamato dall'animazione del boss una volta che l'entrata è stata completata
        entranceComplete = true;

        StartCoroutine(MoveBossUpCoroutine());
    }

    private IEnumerator MoveBossUpCoroutine()
    {
        // Memorizza la posizione iniziale del boss
        Vector3 initialPosition = transform.position;

        // Calcola la nuova posizione del boss spostando l'asse Y gradualmente verso l'alto
        Vector3 targetPosition = initialPosition + new Vector3(0, 1f, 0);
        float moveDuration = 13f; // Durata del movimento in secondi
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            yield return null;
        }

        // Assegna la posizione finale al transform del boss per assicurarsi che sia esattamente sulla nuova posizione
        transform.position = targetPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Controlla se la collisione è con un oggetto che il boss non deve attraversare
        if (collision.gameObject.CompareTag("Wall") /*|| collision.gameObject.CompareTag("Obstacle")*/ || collision.gameObject.CompareTag("Player"))
        {
            // Fermare il movimento del boss
            transform.Translate(Vector2.zero);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            isTouchedByPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            isTouchedByPlayer = false;
        }
    }


}