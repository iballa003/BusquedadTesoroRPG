using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void NewGameButton()
    {
        AudioManager.instance.PlayConfirmSound();
        FindObjectOfType<TransitionController>().ChangeScene("Main");
    }

    private IEnumerator Wait(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
    }
}
