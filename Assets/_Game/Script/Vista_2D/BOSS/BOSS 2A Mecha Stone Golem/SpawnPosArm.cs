using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosArm : MonoBehaviour
{
    // Serve per far spawnare il proiettile ed spararlo verso il player

    public Animator animator;
    public Transform player;
    public Transform spawnPosArm;
    public GameObject bullet;
    public bool IsAttacking;


    public float bulletSpeed = 10f;
    public int bulletCountMax = 20;
    public int bulletCount = 0;


    void Start()
    {
        InvokeRepeating(nameof(AttackPlayer1), 0, 3);
    }

    void AttackPlayer1()
    {
        bool IsAttacking = GetComponentInParent<BossCombact>().isAttacking1;


        //calcola la direzione del target
        Vector3 targetDirection = player.position - transform.position;

        //calcola l'angolo tra la direzione del target e l'asse di rotazione Y dell'arma
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        //ruota l'arma verso il player
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (IsAttacking)
        {
            StartCoroutine(PlayAnimationAndWait(animator, "Attack"));

            // Creazione del proiettile
            GameObject projectile = Instantiate(bullet, spawnPosArm.position, Quaternion.Euler(spawnPosArm.right));

            // Calcolo la direzione del proiettile
            Vector3 direction = (player.position - spawnPosArm.position).normalized;

            projectile.GetComponent<ArmShot>().playerTransform = direction;

            //// Utilizzo LookAt per far puntare il proiettile nella direzione corretta
            //projectile.transform.LookAt(player.Position);

            //// Reimposto la rotazione del proiettile utilizzando Quaternion.Euler per mantenere l'orientamento  
            //projectile.transform.Rotation = Quaternion.Euler(new Vector3(0f, 0f, projectile.transform.Rotation.eulerAngles.z));

            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Applicazione della velocità del proiettile
            Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
            projectileRigidbody.velocity = direction * bulletSpeed;

            bulletCount++;
        }

        if (IsAttacking && bulletCount >= bulletCountMax)
        {
            ArmShot[] bullets = FindObjectsOfType<ArmShot>();

            foreach (ArmShot item in bullets)
            {
                Destroy(item.gameObject);
            }
            bulletCount = 0;
        }

    }

    public IEnumerator PlayAnimationAndWait(Animator animator, string animationName)
    {
        // Avvia l'animazione
        animator.Play(animationName);

        // Ottieni la durata dell'animazione corrente
        AnimationClip clip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        float animationDuration = clip.length - 3;

        // Aspetta che l'animazione sia completata
        yield return new WaitForSeconds(animationDuration);

        // Il codice successivo viene eseguito dopo la fine dell'animazione
        // Aggiungi qui il codice che vuoi eseguire dopo che l'animazione è stata completata
        Debug.Log("L'animazione è stata completata. Eseguiamo altro codice...");
    }
}