using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public List <GameObject> views;
    public void showpanel(GameObject panel)
    {
        foreach (GameObject p in views)
        {
            if (p.name == panel.name)
                p.SetActive(true);
            else
                p.SetActive(false);
        }
    }

    public void loadsceene()
    {
        SceneManager.LoadScene("Taverna");
    }
}
