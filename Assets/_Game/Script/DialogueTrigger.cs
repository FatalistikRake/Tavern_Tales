using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogueScript;
    private bool playerDetected;

    //detect trigger whit player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerDetected = true;
            dialogueScript.ToggleButton(playerDetected);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            playerDetected = false;
            dialogueScript.ToggleButton(playerDetected);

        }
    }

    public void Uptade()
    {
        if(playerDetected && Input.GetKeyDown(KeyCode.K))
        {
            dialogueScript.StartDialogue();
        }
    }
}
