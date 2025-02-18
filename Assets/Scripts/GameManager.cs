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
            Debug.Log("¡Conseguistes las llaves de la entrada!");
            AudioManager.instance.PlayKeysSound();
            PlayerPrefs.SetString("keys", "true");
        }
    }

    public void GetStorageDoorKey()
    {
        if (!PlayerPrefs.HasKey("storagekeys"))
        {
            Debug.Log("¡Conseguistes las llaves del trastero!");
            AudioManager.instance.PlayKeysSound();
            PlayerPrefs.SetString("storagekeys", "true");
        }
    }

    public void GetTreasure()
    {
            Debug.Log("¡Conseguistes el tesoro!");
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
                    obj.SetActive(true);
                }
            }
            Debug.Log(objects.Length + " objetos activados con tag");

        }
        else
        {
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