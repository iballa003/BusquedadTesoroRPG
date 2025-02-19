using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            AudioManager.instance.PlayAmbientMusic();
        }

            if (PlayerPrefs.HasKey("treasure"))
        {
            string hasTreasure = PlayerPrefs.GetString("treasure");
            if (hasTreasure == "true") {
                GameObject shovelObject = GameObject.FindGameObjectWithTag("Treasure");
                if (shovelObject != null)
                {
                    shovelObject.SetActive(false);
                }
            }
        }
    }

    public void GetDoorKey()
    {
        if (!PlayerPrefs.HasKey("keys")) {
            GuidanceTextManager.instance.ShowMessage("¡Conseguistes las llaves de la entrada principal! Ahora dirígete a secretaría.");
            Debug.Log("¡Conseguistes las llaves de la entrada!");
            AudioManager.instance.PlayKeysSound();
            PlayerPrefs.SetString("keys", "true");
        }
    }

    public void GetStorageDoorKey()
    {
        if (!PlayerPrefs.HasKey("storagekeys"))
        {
            GuidanceTextManager.instance.ShowMessage("¡Conseguistes las llaves del trastero! Ahora ve al exterior y encuentra la puerta al noreste.");
            Debug.Log("¡Conseguistes las llaves del trastero!");
            AudioManager.instance.PlayKeysSound();
            PlayerPrefs.SetString("storagekeys", "true");
        }
    }

    public void GetTreasure()
    {
            Debug.Log("¡Conseguistes el tesoro!");
        GuidanceTextManager.instance.ShowMessage("¡Conseguistes el tesoro! ¡Ahora escapa del centro antes de que se acabe el tiempo!");
        AudioManager.instance.PlayKeysSound();
            GameObject treasureObject = GameObject.FindGameObjectWithTag("Treasure");
            if (treasureObject != null)
            {
                treasureObject.SetActive(false);
                PlayerPrefs.SetString("treasure", "true");
            }
    }

    public void InteractionDoor()
    {
        if (!PlayerPrefs.HasKey("keys"))
        {
            AudioManager.instance.PlayCloseDoorSound();
            GuidanceTextManager.instance.ShowMessage("Cerrado. La llave debe estar en alguna papelera.");
            Debug.Log("Cerrado. Encuentra la llave en alguna papelera");
        }
        else
        {
            EnterMainHall();
        }
    }

    public void EnterMainHall()
    {
        AudioManager.instance.PlayOpenDoorSound();
        StartCoroutine(Wait(1f));
        FindObjectOfType<TransitionController>().ChangeScene("Main_inside", new Vector2(11.57f, 18.92f));
    }

    public void ExitMainHall()
    {
        AudioManager.instance.PlayOpenDoorSound();
        StartCoroutine(Wait(1f));
        FindObjectOfType<TransitionController>().ChangeScene("Main", new Vector2(16.05f, 8.559999f));
    }

    public void EnterDoorSecretary()
    {
        AudioManager.instance.PlayOpenDoorSound();
        StartCoroutine(Wait(1f));
        FindObjectOfType<TransitionController>().ChangeScene("Main_secretary", new Vector2(16.37f, 27.72f));
    }

    public void ExitDoorSecretary()
    {
        AudioManager.instance.PlayOpenDoorSound();
        StartCoroutine(Wait(1f));
        FindObjectOfType<TransitionController>().ChangeScene("Main_inside", new Vector2(17.57f, 26.92f));
    }

    public void EnterDoorStorage()
    {
        if (PlayerPrefs.HasKey("storagekeys")) { 
            AudioManager.instance.PlayOpenDoorSound();
            StartCoroutine(Wait(1f));
            FindObjectOfType<TransitionController>().ChangeScene("storage", new Vector2(17.32f, 25.67f));
        }
        else
        {
            AudioManager.instance.PlayCloseDoorSound();
            GuidanceTextManager.instance.ShowMessage("Está cerrado. Necesito la llaves del trastero que están en secretaría.");
            Debug.Log("Está cerrado. Necesito la llaves del trastero que están en secretaría.");
        }
    }

    public void ExitDoorStorage()
    {
        AudioManager.instance.PlayOpenDoorSound();
        StartCoroutine(Wait(1f));
        FindObjectOfType<TransitionController>().ChangeScene("Main", new Vector2(32.05f, 52.56f));
    }

    public void ExitSchool()
    {
        if (PlayerPrefs.HasKey("treasure"))
        {
            Debug.Log("Salir");
            GameObject[] objects = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (GameObject obj in objects)
            {
                if (obj.CompareTag("EndGame")) // Verifica si el objeto tiene el tag correcto
                {
                    CountdownTimer.instance.PauseTimer();
                    AudioManager.instance.StopMusic();
                    obj.SetActive(true);
                }
            }

        }
        else
        {
            GuidanceTextManager.instance.ShowMessage("Todavía no has encontrado el tesoro. Explora el instituto para encontrarlo.");
            Debug.Log("Todavía no has encontrado el tesoro");
        }
    }

    public void ComeBackTitle()
    {
        SceneManager.LoadScene("Title");
    }
    private IEnumerator Wait(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
    }

}