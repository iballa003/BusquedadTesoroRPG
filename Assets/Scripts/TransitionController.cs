using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour
{
    // Start is called before the first frame update
    public Image fadePanel; // Referencia al panel oscuro
    public float fadeDuration = 2f; // Duración de la transición

    private void Start()
    {
        // Iniciar la transición de entrada (de oscuro a claro)
        if (SceneManager.GetActiveScene().name != "Title")
        {
            StartCoroutine(FadeIn());
        }
    }

    public void ChangeScene(string sceneName)
    {
        // Iniciar la transición de salida (de claro a oscuro) y cambiar de escena
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color panelColor = fadePanel.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            panelColor.a = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            fadePanel.color = panelColor;
            yield return null;
        }
    }

    private IEnumerator FadeOut(string sceneName)
    {
        float elapsedTime = 0f;
        Color panelColor = fadePanel.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            panelColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadePanel.color = panelColor;
            yield return null;
        }

        // Cambiar de escena después de la transición
        SceneManager.LoadScene(sceneName);
    }
}
