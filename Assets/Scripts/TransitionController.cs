using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour
{
    // Start is called before the first frame update
    public Image fadePanel;
    public float fadeDuration = 2f;
    public Vector2 spawnPosition;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Title")
        {
            StartCoroutine(FadeIn());
            if (PlayerPrefs.HasKey("SpawnX") && PlayerPrefs.HasKey("SpawnY"))
            {
                float x = PlayerPrefs.GetFloat("SpawnX");
                float y = PlayerPrefs.GetFloat("SpawnY");

                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.transform.position = new Vector2(x, y);
                }
            }
        }
    }

    public void ChangeScene(string sceneName, Vector2 newSpawnPosition)
    {
        PlayerPrefs.SetFloat("SpawnX", newSpawnPosition.x);
        PlayerPrefs.SetFloat("SpawnY", newSpawnPosition.y);
        PlayerPrefs.Save(); // Guardar los datos
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
