using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotolaCheck : MonoBehaviour
{
    public Animator Anim;
    public Collider2D Teleport;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision)
        {
            Anim.SetBool("open", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Anim.SetBool("open", false);
    }
}