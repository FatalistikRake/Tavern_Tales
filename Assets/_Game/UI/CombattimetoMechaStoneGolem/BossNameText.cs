using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossNameText : MonoBehaviour
{
    public float timeToDisplay = 5f; // Tempo di visualizzazione del testo
    public float fadeSpeed = 0.5f; // Velocità di dissolvenza del testo

    private TextMeshProUGUI bossNameText; // Riferimento al componente TextMeshProUGUI
    private Color textColor; // Colore originale del testo

    private void Start()
    {
        bossNameText = GetComponent<TextMeshProUGUI>();
        textColor = bossNameText.color;
        bossNameText.color = new Color(textColor.r, textColor.g, textColor.b, 0f); // Imposta l'opacità del testo a 0 all'inizio
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DisplayText());
        }
    }

    IEnumerator DisplayText()
    {
        bossNameText.color = new Color(textColor.r, textColor.g, textColor.b, 1f); // Mostra il testo impostando l'opacità al massimo
        yield return new WaitForSeconds(timeToDisplay); // Attendi per il tempo di visualizzazione
        while (bossNameText.color.a > 0)
        {
            bossNameText.color = new Color(textColor.r, textColor.g, textColor.b, bossNameText.color.a - (fadeSpeed * Time.deltaTime)); // Riduci gradualmente l'opacità del testo
            yield return null;
        }
    }
}
