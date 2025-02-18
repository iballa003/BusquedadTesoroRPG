using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    public static CountdownTimer instance;

    public float countdownTime = 300f; // Tiempo inicial en segundos
    public string sceneToLoad = "GameOver"; // Escena donde termina el juego
    public Text timerText;
    public TMP_Text tmpTimerText;

    private bool isCounting = true;
    private bool isPaused = false; // Nueva variable para pausar

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        StartCoroutine(StartCountdown());
        SceneManager.sceneLoaded += OnSceneLoaded; // Detectar cambio de escena
    }

    private IEnumerator StartCountdown()
    {
        while (countdownTime > 0 && isCounting)
        {
            if (!isPaused)
            {
                UpdateTimerUI();
                yield return new WaitForSeconds(1f);
                countdownTime--;
            }
            else
            {
                yield return null;
            }
        }

        if (countdownTime <= 0)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timerText != null)
        {
            timerText.text = formattedTime;
            if (countdownTime <= 10) timerText.color = Color.red;
        }

        if (tmpTimerText != null)
        {
            tmpTimerText.text = formattedTime;
            if (countdownTime <= 10) tmpTimerText.color = Color.red;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == sceneToLoad || scene.name == "Title")
        {
            Destroy(gameObject); // Destruye el temporizador si es GameOver
        }
        else
        {
            // Buscar específicamente el texto del temporizador sin afectar otras UI
            if (timerText == null)
            {
                GameObject timerObject = GameObject.Find("TimerText");
                if (timerObject != null)
                {
                    timerText = timerObject.GetComponent<Text>();
                }
            }

            if (tmpTimerText == null)
            {
                GameObject tmpTimerObject = GameObject.Find("TMP_TimerText");
                if (tmpTimerObject != null)
                {
                    tmpTimerText = tmpTimerObject.GetComponent<TMP_Text>();
                }
            }

            UpdateTimerUI();
        }
    }

    public void PauseTimer()
    {
        isPaused = true;
    }
}





