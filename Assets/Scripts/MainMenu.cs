using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        PlayerPrefs.DeleteAll();
    }
    public void NewGameButton()
    {
        AudioManager.instance.PlayConfirmSound();
        FindObjectOfType<TransitionController>().ChangeScene("Main", new Vector2(-0.95f, -7.44f));
    }

    public void ExitButton()
    {
        AudioManager.instance.PlayConfirmSound();
        Application.Quit();
    }

    


}
