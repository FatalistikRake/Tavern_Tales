using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosLaser : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public Transform spawnPosLaser;
    public GameObject Laser;
    public bool IsAttacking;

    void Start()
    {
        InvokeRepeating(nameof(AttackPlayer), 0, 2);
    }

    void AttackPlayer()
    {
        bool isAttacking = GetComponentInParent<BossCombact>().isAttacking2;
        IsAttacking = isAttacking;

        //calcola la direzione del target
        Vector3 targetDirection = player.position - transform.position;

        //calcola l'angolo tra la direzione del target e l'asse di rotazione Y dell'arma
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        //ruota l'arma verso il player
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (IsAttacking)
        {
            animator.SetTrigger("Attack");

            //// Utilizzo LookAt per far puntare il proiettile nella direzione corretta
            //projectile.transform.LookAt(player.Position);

            //// Reimposto la rotazione del proiettile utilizzando Quaternion.Euler per mantenere l'orientamento  
            //projectile.transform.Rotation = Quaternion.Euler(new Vector3(0f, 0f, projectile.transform.Rotation.eulerAngles.z));

            //projectile.transform.Rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        }
    }
}
