using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Para usar TextMeshPro

public class GuidanceTextManager : MonoBehaviour
{
    public static GuidanceTextManager instance; // Singleton para accederlo desde otros scripts
    public TMP_Text guidanceText; // Referencia al TextMeshPro en la UI
    private float messageDuration = 3f; // Duración del mensaje en pantalla

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No destruir al cambiar de escena
            SceneManager.sceneLoaded += OnSceneLoaded; // Suscribirse al evento
        }
        else
        {
            Destroy(gameObject);
        }

        // Asegurarse de que el texto comienza vacío
        if (guidanceText != null)
        {
            guidanceText.text = "";
        }
    }

    private void Start()
    {
        AssignGuidanceText();
        // Llamar al mensaje de bienvenida después de 1 segundo
        messageDuration = 5f;
        if (!PlayerPrefs.HasKey("firstNotification")){ 
            Invoke(nameof(ShowStartNotification), 1f);
            messageDuration = 3f;
            PlayerPrefs.SetString("firstNotification", "true");
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento cuando el objeto se destruye
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignGuidanceText(); // Reasignar el texto cuando cambia la escena
    }

    private void AssignGuidanceText()
    {
        if (guidanceText == null)
        {
            GameObject textObject = GameObject.Find("GuidanceText");
            if (textObject != null)
            {
                guidanceText = textObject.GetComponent<TMP_Text>();
            }
        }
    }

    private void ShowStartNotification()
    {
        ShowMessage("¡Bienvenido! Encuentra el tesoro antes de que se acabe el tiempo. La primera llave se encuentra en una papelera del patio.");
    }

    public void ShowMessage(string message)
    {
        AssignGuidanceText();
        if (guidanceText != null)
        {
            guidanceText.text = message;
            CancelInvoke(nameof(HideMessage)); // Reiniciar temporizador si ya hay un mensaje activo
            Invoke(nameof(HideMessage), messageDuration); // Ocultar el mensaje después de X segundos
        }
    }

    private void HideMessage()
    {
        if (guidanceText != null)
        {
            guidanceText.text = "";
        }
    }
}
