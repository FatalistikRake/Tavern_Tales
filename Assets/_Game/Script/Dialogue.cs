using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    //fields
    //window
    public GameObject window;
    //indicator
    public GameObject button;
    //text component
    public TMP_Text dialogueText;
    //dialogues list
    public List<string> dialogues;
    //writing speed
    public float writingSpeed;
    //index on dialogue
    private int index;
    //character index
    private int charindex;
    //starded boolean
    private bool Started;
    //wait for nect bolean
    private bool WaitForNext;

    private void Awake()
    {
        ToggleButton(false);
        ToggleWindow(false);
    }

    //show window
    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }

    //hide indicator
    public void ToggleButton(bool show)
    {
        button.SetActive(show);
    }

    //stard dialogue
    public void StartDialogue()
    {
        if (Started)
            return;
        
        //bolean start point
        Started = true;
        //show the window
        ToggleWindow(true);
        //hide the indicator
        ToggleButton(false);
        //start whit first dialogue
        GetDialogue(0);

    }

    private void GetDialogue(int i)
    {
        //start index at 0;
        index = i;
        //reset the character index
        charindex = 0;
        //clear the dialogue component text
        dialogueText.text = string.Empty;
        //start writing
        StartCoroutine(Writing());
    }

    //end dialogue
    public void EndDialogue()
    {
        //hide the dialogue
        ToggleWindow(false);

    }

    //writing logic
    IEnumerator Writing()
    {
        yield return new WaitForSeconds(writingSpeed);

        string CurrentDialogue = dialogues[index];
        //write the chharacter
        dialogueText.text += CurrentDialogue[charindex];
        //increase the character index
        charindex++;
        //end of sentence
        if(charindex < CurrentDialogue.Length)
        {
            //wait n seconds
            yield return new WaitForSeconds(writingSpeed);
            //restart the same process
            StartCoroutine(Writing());
        }
        else
        {
            //end sentence and start next one
            WaitForNext = true;

        }
    }

    private void Uptide()
    {
        if (!Started)
            return;

        if(WaitForNext && Input.GetKeyDown(KeyCode.K))
        {
            WaitForNext = false;
            index++;

            if(index < dialogues.Count)
            {
                GetDialogue(index);
            }
            else
            {
                // end dialogue
                EndDialogue();
            }
        }
    }
}
